using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Westropolis
{
    /// <summary>
    /// Setările grafice aplicate la pornirea jocului, înaintea primei scene (PLAN.md 3.1): 30 FPS și Render Scale redus
    /// pe telefon, 60 FPS și rezoluție întreagă pe Mac. Valorile finale se reglează la 3.7 și din setări (11.6).
    /// </summary>
    public static class SetariGrafice
    {
        public const int FpsTelefon = 30;
        public const int FpsMac = 60;
        public const float RenderScaleTelefon = 0.7f;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void LaPornire()
        {
            Aplica(Application.isMobilePlatform);
        }

        public static void Aplica(bool telefon)
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = telefon ? FpsTelefon : FpsMac;

            if (GraphicsSettings.currentRenderPipeline is UniversalRenderPipelineAsset urp)
                urp.renderScale = telefon ? RenderScaleTelefon : 1f;

            // ecranul nu se stinge în timpul jocului
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }
    }
}
