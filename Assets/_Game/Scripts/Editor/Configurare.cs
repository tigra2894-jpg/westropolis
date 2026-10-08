using System.Linq;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Localization;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Westropolis.EditorTools
{
    /// <summary>
    /// Setările fixe ale proiectului (PLAN.md, etapele 0.5 și 0.11), aplicate prin cod ca să fie aceleași pe Mac și pe
    /// GitHub. Se poate rula oricând (meniul Westropolis → Configurează proiectul); nu strică nimic dacă e deja aplicat.
    /// </summary>
    public static class Configurare
    {
        public const string NumeJoc = "Westropolis";
        public const string Companie = "Tigra2805";
        public const string Pachet = "com.tigra2805.westropolis";

        public const string FolderSetari = "Assets/_Game/Settings";
        public const string CaleUrp = FolderSetari + "/URP_Westropolis.asset";
        public const string CaleRenderer = FolderSetari + "/URP_Westropolis_Renderer.asset";
        public const string FolderLocalizare = "Assets/_Game/Localization";
        public const string TabelInterfata = "Interfata";

        /// <summary>Straturile proiectului, de la indexul 8 (0.11). Ordinea nu se schimbă (notă în DECIZII.md).</summary>
        public static readonly string[] Straturi =
        {
            "Player", "Vehicul", "Cal", "NPC", "Teren", "Cladiri", "Apa", "Interactiune", "Proiectil"
        };

        public const int PrimulStrat = 8;

        [MenuItem("Westropolis/Configureaza proiectul")]
        public static void Aplica()
        {
            AplicaJucator();
            AplicaEditor();
            AplicaUrp();
            AplicaStraturi();
            AplicaLocalizare();
            AssetDatabase.SaveAssets();
            Debug.Log("[Westropolis] Configurarea proiectului aplicată.");
        }

        static void AplicaJucator()
        {
            PlayerSettings.productName = NumeJoc;
            PlayerSettings.companyName = Companie;
            PlayerSettings.SetApplicationIdentifier(NamedBuildTarget.Android, Pachet);
            PlayerSettings.SetApplicationIdentifier(NamedBuildTarget.Standalone, Pachet);

            // Android: IL2CPP + ARM64 (obligatorii pentru Google Play), Vulkan cu OpenGL ES 3 ca rezervă
            PlayerSettings.SetScriptingBackend(NamedBuildTarget.Android, ScriptingImplementation.IL2CPP);
            PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64;
            PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel26;
            PlayerSettings.Android.targetSdkVersion = AndroidSdkVersions.AndroidApiLevelAuto;
            PlayerSettings.SetUseDefaultGraphicsAPIs(BuildTarget.Android, false);
            PlayerSettings.SetGraphicsAPIs(BuildTarget.Android,
                new[] { GraphicsDeviceType.Vulkan, GraphicsDeviceType.OpenGLES3 });

            // doar pe orizontală (Landscape Left + Right)
            PlayerSettings.defaultInterfaceOrientation = UIOrientation.AutoRotation;
            PlayerSettings.allowedAutorotateToLandscapeLeft = true;
            PlayerSettings.allowedAutorotateToLandscapeRight = true;
            PlayerSettings.allowedAutorotateToPortrait = false;
            PlayerSettings.allowedAutorotateToPortraitUpsideDown = false;

            // Active Input Handling = Input System (1). Nu are API public: se scrie direct în ProjectSettings.
            var setari = Unsupported.GetSerializedAssetInterfaceSingleton("PlayerSettings");
            var so = new SerializedObject(setari);
            var intrare = so.FindProperty("activeInputHandler");
            if (intrare != null && intrare.intValue != 1)
            {
                intrare.intValue = 1;
                so.ApplyModifiedPropertiesWithoutUndo();
                Debug.Log("[Westropolis] Active Input Handling = Input System (se aplică după repornirea Unity).");
            }
        }

        static void AplicaEditor()
        {
            EditorSettings.serializationMode = SerializationMode.ForceText;
            VersionControlSettings.mode = "Visible Meta Files";
        }

        static void AplicaUrp()
        {
            AsiguraFolder(FolderSetari);

            var renderer = AssetDatabase.LoadAssetAtPath<UniversalRendererData>(CaleRenderer);
            if (renderer == null)
            {
                renderer = ScriptableObject.CreateInstance<UniversalRendererData>();
                AssetDatabase.CreateAsset(renderer, CaleRenderer);
            }

            var urp = AssetDatabase.LoadAssetAtPath<UniversalRenderPipelineAsset>(CaleUrp);
            if (urp == null)
            {
                urp = UniversalRenderPipelineAsset.Create(renderer);
                AssetDatabase.CreateAsset(urp, CaleUrp);
            }

            // Render Scale-ul pe telefon îl pune jocul la pornire (SetariGrafice); aici rămâne 1 pentru Mac
            GraphicsSettings.defaultRenderPipeline = urp;
            var nivelCurent = QualitySettings.GetQualityLevel();
            for (var i = 0; i < QualitySettings.names.Length; i++)
            {
                QualitySettings.SetQualityLevel(i, false);
                QualitySettings.renderPipeline = urp;
            }
            QualitySettings.SetQualityLevel(nivelCurent, false);

            EditorUtility.SetDirty(urp);
        }

        static void AplicaStraturi()
        {
            var tm = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
            var straturi = tm.FindProperty("layers");
            for (var i = 0; i < Straturi.Length; i++)
                straturi.GetArrayElementAtIndex(PrimulStrat + i).stringValue = Straturi[i];
            tm.ApplyModifiedPropertiesWithoutUndo();

            // matricea de coliziuni: zona de interacțiune atinge doar jucătorul; proiectilele nu ating zonele de
            // interacțiune și nici alte proiectile
            var interactiune = PrimulStrat + System.Array.IndexOf(Straturi, "Interactiune");
            var player = PrimulStrat + System.Array.IndexOf(Straturi, "Player");
            var proiectil = PrimulStrat + System.Array.IndexOf(Straturi, "Proiectil");
            for (var strat = 0; strat < 32; strat++)
                Physics.IgnoreLayerCollision(interactiune, strat, strat != player);
            Physics.IgnoreLayerCollision(proiectil, proiectil, true);
        }

        static void AplicaLocalizare()
        {
            AsiguraFolder(FolderLocalizare);

            var setari = LocalizationEditorSettings.ActiveLocalizationSettings;
            if (setari == null)
            {
                setari = ScriptableObject.CreateInstance<LocalizationSettings>();
                AssetDatabase.CreateAsset(setari, FolderLocalizare + "/Setari_Localizare.asset");
                LocalizationEditorSettings.ActiveLocalizationSettings = setari;
            }

            // limbile de bază; celelalte se adaugă la 11.4
            foreach (var cod in new[] { "en", "ro" })
            {
                if (LocalizationEditorSettings.GetLocales().Any(l => l.Identifier.Code == cod))
                    continue;
                var limba = Locale.CreateLocale(new LocaleIdentifier(cod));
                AssetDatabase.CreateAsset(limba, $"{FolderLocalizare}/Limba_{cod}.asset");
                LocalizationEditorSettings.AddLocale(limba);
            }

            // ordinea alegerii limbii la pornire: cea salvată de jucător → limba telefonului → engleza
            var selectori = setari.GetStartupLocaleSelectors();
            if (!selectori.Any(s => s is PlayerPrefLocaleSelector))
                selectori.Insert(0, new PlayerPrefLocaleSelector { PlayerPreferenceKey = "westropolis-limba" });
            if (!selectori.Any(s => s is SystemLocaleSelector))
                selectori.Add(new SystemLocaleSelector());
            if (!selectori.Any(s => s is SpecificLocaleSelector))
                selectori.Add(new SpecificLocaleSelector { LocaleId = new LocaleIdentifier("en") });
            EditorUtility.SetDirty(setari);

            if (LocalizationEditorSettings.GetStringTableCollection(TabelInterfata) == null)
                LocalizationEditorSettings.CreateStringTableCollection(TabelInterfata, FolderLocalizare + "/Tabele");
        }

        public static void AsiguraFolder(string cale)
        {
            if (AssetDatabase.IsValidFolder(cale))
                return;
            var parinte = System.IO.Path.GetDirectoryName(cale)?.Replace('\\', '/');
            if (!string.IsNullOrEmpty(parinte))
                AsiguraFolder(parinte);
            AssetDatabase.CreateFolder(parinte, System.IO.Path.GetFileName(cale));
        }
    }

    /// <summary>La prima deschidere pe Mac (proiect fără setările jocului), configurarea se aplică singură.</summary>
    [InitializeOnLoad]
    static class ConfigurarePrimaDeschidere
    {
        static ConfigurarePrimaDeschidere()
        {
            if (Application.isBatchMode || PlayerSettings.productName == Configurare.NumeJoc)
                return;
            EditorApplication.delayCall += Configurare.Aplica;
        }
    }
}
