using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace Westropolis.Test
{
    /// <summary>
    /// Jurnalul de erori al versiunilor de test (PLAN.md 6.4): ține ultimele mesaje în memorie (pentru panou) și le
    /// scrie într-un fișier pe telefon, ca să poată fi trimis cu „Trimite jurnalul”.
    /// </summary>
    public static class JurnalErori
    {
        public const int MesajePastrate = 200;
        public const string NumeFisier = "jurnal_westropolis.txt";

        static readonly Queue<string> mesaje = new Queue<string>();
        static readonly Queue<string> erori = new Queue<string>();
        static StreamWriter fisier;

        public static string CaleFisier => Path.Combine(Application.persistentDataPath, NumeFisier);
        public static IEnumerable<string> UltimeleErori => erori;
        public static int NumarErori { get; private set; }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        static void Porneste()
        {
            mesaje.Clear();
            erori.Clear();
            NumarErori = 0;
            if (!ModTest.Activ)
                return;
            try
            {
                fisier = new StreamWriter(CaleFisier, false, Encoding.UTF8) { AutoFlush = true };
                fisier.WriteLine($"Westropolis {Application.version} · {SystemInfo.deviceModel} · {SystemInfo.graphicsDeviceName}");
            }
            catch (IOException)
            {
                fisier = null;
            }
            Application.logMessageReceivedThreaded -= LaMesaj;
            Application.logMessageReceivedThreaded += LaMesaj;
        }

        static void LaMesaj(string text, string stiva, LogType tip)
        {
            var linie = $"[{System.DateTime.Now:HH:mm:ss}] {tip}: {text}";
            lock (mesaje)
            {
                Adauga(mesaje, linie);
                if (tip == LogType.Error || tip == LogType.Exception || tip == LogType.Assert)
                {
                    NumarErori++;
                    Adauga(erori, linie);
                    linie += "\n" + stiva;
                }
                fisier?.WriteLine(linie);
            }
        }

        static void Adauga(Queue<string> coada, string linie)
        {
            coada.Enqueue(linie);
            while (coada.Count > MesajePastrate)
                coada.Dequeue();
        }

        /// <summary>Ultimele mesaje, ca text (pentru trimitere).</summary>
        public static string Text()
        {
            lock (mesaje)
                return string.Join("\n", mesaje);
        }
    }
}
