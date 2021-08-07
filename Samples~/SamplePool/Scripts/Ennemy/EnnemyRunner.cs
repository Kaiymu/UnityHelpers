using UnityEngine;

namespace PoolSystem.Sample
{
    public class EnnemyRunner : Ennemy
    {
        private void OnEnable()
        {
            Debug.Log("Ennemy runner spawned");
        }

        private void OnDisable()
        {
            Debug.Log("Ennemy runner despawned");
        }
    }
}
