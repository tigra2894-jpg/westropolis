using System;
using System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Westropolis.EditorTools
{
    /// <summary>
    /// Construirea APK-ului (etapa 0.12), apelată de GitHub Actions și din meniul Westropolis pe Mac.
    /// Versiunile de test sunt Development Build (mod test: panoul de test vizibil).
    /// </summary>
    public static class Construire
    {
        public const string CaleScenaTest = "Assets/_Game/Scenes/Test.unity";
        public const string CaleApk = "Build/Westropolis.apk";

        /// <summary>Scena de test (0.16): cerul, lumina, o podea și un cub care se rotește. Se creează doar dacă lipsește.</summary>
        [MenuItem("Westropolis/Creeaza scena de test")]
        public static void AsiguraScenaTest()
        {
            if (!File.Exists(CaleScenaTest))
            {
                Configurare.AsiguraFolder("Assets/_Game/Scenes");
                var scena = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);

                var camera = Camera.main;
                if (camera != null)
                    camera.transform.SetPositionAndRotation(new Vector3(0f, 1.6f, -4f), Quaternion.Euler(10f, 0f, 0f));

                var podea = GameObject.CreatePrimitive(PrimitiveType.Plane);
                podea.name = "Podea";
                podea.transform.localScale = new Vector3(2f, 1f, 2f);

                var cub = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cub.name = "Cub";
                cub.transform.position = new Vector3(0f, 1f, 0f);
                cub.AddComponent<RotireCub>();

                EditorSceneManager.SaveScene(scena, CaleScenaTest);
                Debug.Log("[Westropolis] Scena de test creată: " + CaleScenaTest);
            }

            EditorBuildSettings.scenes = new[] { new EditorBuildSettingsScene(CaleScenaTest, true) };
        }

        /// <summary>Apelat de GitHub Actions: -executeMethod Westropolis.EditorTools.Construire.ApkAndroid</summary>
        public static void ApkAndroid()
        {
            try
            {
                Configurare.Aplica();
                AsiguraScenaTest();

                var numar = Environment.GetEnvironmentVariable("GITHUB_RUN_NUMBER");
                var cod = int.TryParse(numar, out var n) ? n : 1;
                PlayerSettings.Android.bundleVersionCode = cod;
                PlayerSettings.bundleVersion = "0.0." + cod;

                Directory.CreateDirectory(Path.GetDirectoryName(CaleApk));
                var optiuni = new BuildPlayerOptions
                {
                    scenes = new[] { CaleScenaTest },
                    locationPathName = CaleApk,
                    target = BuildTarget.Android,
                    targetGroup = BuildTargetGroup.Android,
                    options = BuildOptions.Development
                };
                EditorUserBuildSettings.buildAppBundle = false;

                var raport = BuildPipeline.BuildPlayer(optiuni);
                var rezumat = raport.summary;
                Debug.Log($"[Westropolis] Construire: {rezumat.result}, {rezumat.totalErrors} erori, " +
                          $"{rezumat.totalSize / (1024 * 1024)} MB, {rezumat.totalTime}");
                if (Application.isBatchMode)
                    EditorApplication.Exit(rezumat.result == BuildResult.Succeeded ? 0 : 1);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                if (Application.isBatchMode)
                    EditorApplication.Exit(1);
            }
        }

        [MenuItem("Westropolis/Construieste APK (test)")]
        static void ApkDinMeniu() => ApkAndroid();
    }
}
