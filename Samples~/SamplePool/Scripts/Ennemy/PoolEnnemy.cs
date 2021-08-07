using PoolSystem.Runtime;
using System.Collections.Generic;
using UnityEngine;

namespace PoolSystem.Sample
{
    public class PoolEnnemy : MonoBehaviour
    {
        [SerializeField]
        private int _numberToSpawn;
        [SerializeField]
        private List<Ennemy> _ennemies;

        private Stack<Ennemy> _ennemyBrute = new Stack<Ennemy>();

        private void Start()
        {
            for (int i = 0; i < _ennemies.Count; i++)
            {
                PoolManager.instance.QueueIntoPool<Ennemy>(_ennemies[i], _numberToSpawn);
            }
        }

        private void Update()
        {
            // Add ennemy brute
            if(Input.GetKeyDown(KeyCode.S))
            {
                _ennemyBrute.Push(PoolManager.instance.GetAvailable<EnnemyBrute>());
                _ennemyBrute.Peek().gameObject.SetActive(true);
            }

            // Add ennnemy runners
            if (Input.GetKeyDown(KeyCode.F))
            {
                _ennemyBrute.Push(PoolManager.instance.GetAvailable<EnnemyRunner>());
                _ennemyBrute.Peek().gameObject.SetActive(true);
            }

            // Remove last ennemy added
            if (Input.GetKeyDown(KeyCode.D))
            {
                PoolManager.instance.PutBack(_ennemyBrute.Pop());
            }
        }
    }
}
