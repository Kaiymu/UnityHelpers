using UnityEngine;

namespace PoolSystem.Sample
{
    public class EnnemyBrute : Ennemy
    {
        private void OnEnable()
        {
            Debug.Log("Ennemy brute spawned");
        }

        private void OnDisable()
        {
            Debug.Log("Ennemy brute despawner");
        }
    }
}
