using System.Collections.Generic;
using UnityEngine;

namespace PoolSystem.Runtime
{
    public class PoolManager : MonoBehaviour
    {
        public static PoolManager instance;

        private Dictionary<string, Queue<object>> _dictionnaryQueue = new Dictionary<string, Queue<object>>();

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void QueueIntoPool<T>(MonoBehaviour prefabToQueue, int numberToQueue) where T : MonoBehaviour
        {
            var queuePool = new Queue<object>();
            for (int i = 0; i < numberToQueue; i++)
            {
                var o = (T)Instantiate(prefabToQueue, Vector2.zero, Quaternion.identity, gameObject.transform);
                o.gameObject.SetActive(false);

                queuePool.Enqueue(o);
            }

            _dictionnaryQueue.Add(prefabToQueue.GetType().Name, queuePool);
        }

        public T GetAvailable<T>()
        {
            if (_dictionnaryQueue.TryGetValue(typeof(T).Name, out var available))
            {
                if (available.Count <= 1)
                {
                    _AddMoreIntoPool<MonoBehaviour>(available, 5);
                }

                return (T)available.Dequeue();
            }

            return default(T);
        }

        private void _AddMoreIntoPool<T>(Queue<object> available, int numberToQueue) where T : MonoBehaviour
        {
            var objectToQueue = (MonoBehaviour)available.Peek();
            var objectTypeName = objectToQueue.GetType().Name;

            for (int i = 0; i < numberToQueue; i++)
            {
                var o = (T)Instantiate(objectToQueue, Vector2.zero, Quaternion.identity, gameObject.transform);
                o.name = objectTypeName;
                o.gameObject.SetActive(false);

                available.Enqueue(o);
            }

            _dictionnaryQueue[objectTypeName] = available;
        }

        public void PutBack(MonoBehaviour o)
        {
            if (o == null)
            {
                Debug.LogError("Type null");
                return;
            }

            o.transform.gameObject.SetActive(false);

            if (_dictionnaryQueue.TryGetValue(o.GetType().Name, out var available))
            {
                available.Enqueue(o);
            }
        }
    }
}