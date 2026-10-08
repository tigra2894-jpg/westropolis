using NUnit.Framework;
using UnityEditor;
using UnityEditor.Build;
using Westropolis.EditorTools;
using Westropolis.Test;

namespace Westropolis.Tests
{
    /// <summary>Testele de bază: datele fixe ale proiectului și setările aplicate de Configurare (etapele 0.5, 0.11).</summary>
    public class TesteBaza
    {
        [OneTimeSetUp]
        public void Pregatire() => Configurare.Aplica();

        [Test]
        public void PachetulAndroidEsteCelHotarat()
        {
            Assert.AreEqual("com.tigra2805.westropolis",
                PlayerSettings.GetApplicationIdentifier(NamedBuildTarget.Android));
            Assert.AreEqual("Westropolis", PlayerSettings.productName);
        }

        [Test]
        public void AndroidFolosesteIl2cppSiArm64()
        {
            Assert.AreEqual(ScriptingImplementation.IL2CPP,
                PlayerSettings.GetScriptingBackend(NamedBuildTarget.Android));
            Assert.AreEqual(AndroidArchitecture.ARM64, PlayerSettings.Android.targetArchitectures);
        }

        [Test]
        public void StraturileSuntLaLoculLor()
        {
            for (var i = 0; i < Configurare.Straturi.Length; i++)
                Assert.AreEqual(Configurare.Straturi[i],
                    UnityEngine.LayerMask.LayerToName(Configurare.PrimulStrat + i));
        }

        [Test]
        public void PanoulFormeazaNumereleCorect()
        {
            Assert.AreEqual("12 MB", PanouTest.FormateazaMegaocteti(12L * 1024 * 1024));
            Assert.AreEqual("1.50 mil.", PanouTest.FormateazaNumar(1_500_000).Replace(',', '.'));
            Assert.AreEqual("999", PanouTest.FormateazaNumar(999));
        }
    }
}
