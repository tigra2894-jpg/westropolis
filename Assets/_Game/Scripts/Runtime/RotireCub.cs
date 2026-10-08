using UnityEngine;

namespace Westropolis
{
    /// <summary>Cubul din scena de test (etapa 0.16): se rotește continuu, ca să se vadă că jocul rulează.</summary>
    public class RotireCub : MonoBehaviour
    {
        [SerializeField] Vector3 vitezaGradePeSecunda = new Vector3(15f, 45f, 0f);

        void Update()
        {
            transform.Rotate(vitezaGradePeSecunda * Time.deltaTime, Space.Self);
        }
    }
}
