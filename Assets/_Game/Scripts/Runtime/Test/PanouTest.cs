using Unity.Profiling;
using UnityEngine;
using UnityEngine.Profiling;

namespace Westropolis.Test
{
    /// <summary>
    /// Panoul de test (PLAN.md 0.17): FPS, memorie (totală și texturi), triunghiuri, apeluri de desenare, ultimele erori
    /// și butonul „Trimite jurnalul”. Apare singur în fiecare scenă, doar în mod test. Textele sunt tehnice, pentru
    /// dezvoltator, și de aceea nu trec prin Localization (excepție notată în DECIZII.md).
    /// </summary>
    public class PanouTest : MonoBehaviour
    {
        const float InaltimeReferinta = 1080f;
        const float IntervalActualizare = 0.5f;

        ProfilerRecorder triunghiuri;
        ProfilerRecorder apeluriDesenare;
        ProfilerRecorder memorieTexturi;

        float timpAcumulat;
        int cadreAcumulate;
        float fps;
        float timpCadruMaxMs;
        bool restrans;
        GUIStyle stil;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        static void Creeaza()
        {
            if (!ModTest.Activ || FindAnyObjectByType<PanouTest>() != null)
                return;
            var obiect = new GameObject("PanouTest");
            DontDestroyOnLoad(obiect);
            obiect.AddComponent<PanouTest>();
        }

        void OnEnable()
        {
            triunghiuri = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Triangles Count");
            apeluriDesenare = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Draw Calls Count");
            memorieTexturi = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "Texture Memory");
        }

        void OnDisable()
        {
            triunghiuri.Dispose();
            apeluriDesenare.Dispose();
            memorieTexturi.Dispose();
        }

        void Update()
        {
            timpAcumulat += Time.unscaledDeltaTime;
            cadreAcumulate++;
            timpCadruMaxMs = Mathf.Max(timpCadruMaxMs, Time.unscaledDeltaTime * 1000f);
            if (timpAcumulat < IntervalActualizare)
                return;
            fps = cadreAcumulate / timpAcumulat;
            timpAcumulat = 0f;
            cadreAcumulate = 0;
        }

        void OnGUI()
        {
            var scara = Screen.height / InaltimeReferinta;
            var zona = Screen.safeArea;
            GUI.matrix = Matrix4x4.TRS(new Vector3(zona.x, Screen.height - zona.yMax, 0f), Quaternion.identity,
                new Vector3(scara, scara, 1f));

            if (stil == null)
                stil = new GUIStyle(GUI.skin.box) { alignment = TextAnchor.UpperLeft, fontSize = 22, wordWrap = true };

            if (GUI.Button(new Rect(10, 10, 120, 44), restrans ? "Test ▸" : "Test ▾"))
                restrans = !restrans;
            if (restrans)
                return;

            var text =
                $"FPS {fps:0} (cadru max {timpCadruMaxMs:0} ms)\n" +
                $"Memorie {FormateazaMegaocteti(Profiler.GetTotalAllocatedMemoryLong())} · " +
                $"texturi {FormateazaMegaocteti(memorieTexturi.Valid ? memorieTexturi.LastValue : 0)}\n" +
                $"Triunghiuri {FormateazaNumar(triunghiuri.Valid ? triunghiuri.LastValue : 0)} · " +
                $"draw calls {(apeluriDesenare.Valid ? apeluriDesenare.LastValue : 0)}\n" +
                $"Erori: {JurnalErori.NumarErori}";
            foreach (var eroare in JurnalErori.UltimeleErori)
                text += "\n" + Scurteaza(eroare, 160);

            GUI.Box(new Rect(10, 60, 620, 220), text, stil);

            if (GUI.Button(new Rect(10, 290, 260, 50), "Trimite jurnalul"))
                TrimiteJurnalul();
            if (GUI.Button(new Rect(280, 290, 200, 50), "Reset max"))
                timpCadruMaxMs = 0f;
        }

        public static string FormateazaMegaocteti(long octeti) => $"{octeti / (1024f * 1024f):0} MB";

        public static string FormateazaNumar(long numar) =>
            numar >= 1_000_000 ? $"{numar / 1_000_000f:0.00} mil." :
            numar >= 1_000 ? $"{numar / 1_000f:0.0} mii" : numar.ToString();

        static string Scurteaza(string text, int maxim) =>
            text.Length <= maxim ? text : text.Substring(0, maxim) + "…";

        /// <summary>Deschide meniul de partajare Android cu ultimele mesaje din jurnal (în editor: îl copiază).</summary>
        static void TrimiteJurnalul()
        {
            var text = JurnalErori.Text();
#if UNITY_ANDROID && !UNITY_EDITOR
            using (var intent = new AndroidJavaObject("android.content.Intent"))
            using (var clasaIntent = new AndroidJavaClass("android.content.Intent"))
            using (var unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            using (var activitate = unity.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                intent.Call<AndroidJavaObject>("setAction", clasaIntent.GetStatic<string>("ACTION_SEND"));
                intent.Call<AndroidJavaObject>("setType", "text/plain");
                intent.Call<AndroidJavaObject>("putExtra", clasaIntent.GetStatic<string>("EXTRA_SUBJECT"),
                    "Jurnal Westropolis " + Application.version);
                intent.Call<AndroidJavaObject>("putExtra", clasaIntent.GetStatic<string>("EXTRA_TEXT"), text);
                using (var alegere = clasaIntent.CallStatic<AndroidJavaObject>("createChooser", intent,
                           "Trimite jurnalul"))
                    activitate.Call("startActivity", alegere);
            }
#else
            GUIUtility.systemCopyBuffer = text;
            Debug.Log("[Westropolis] Jurnalul a fost copiat (" + JurnalErori.CaleFisier + ").");
#endif
        }
    }
}
