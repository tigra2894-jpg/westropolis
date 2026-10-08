using UnityEngine;

namespace Westropolis.Test
{
    /// <summary>
    /// Comutatorul unic „mod test” (PLAN.md 6.4): activ doar în versiunile de test (Development Build). În versiunea
    /// publică panoul de test, contorul de FPS și butoanele de test sunt ascunse, nu șterse.
    /// </summary>
    public static class ModTest
    {
        public static bool Activ => Debug.isDebugBuild;
    }
}
