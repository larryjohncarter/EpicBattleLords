using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : SingletonBehaviour<ObjectPool>
{
    [System.Serializable]
    public class Pool
    {
        public int id;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    private Dictionary<int, Queue<GameObject>> _poolDictionary = new();

    private void Start()
    {
        _poolDictionary = new Dictionary<int, Queue<GameObject>>();
        foreach (var pool in pools)
        {
            var objectPool = new Queue<GameObject>();
            for (var i = 0; i < pool.size; i++)
            {
                var obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            _poolDictionary.Add(pool.id, objectPool);
        }
    }

    public GameObject SpawnFromPool(int id, Vector3 position, Quaternion rotation)
    {
        if (!_poolDictionary.TryGetValue(id, out var queue))
        {
            Debug.LogWarning($"Pool with index {id} doesn't exist.");
            return null;
        }

        var objectToSpawn = queue.Dequeue();

        if (objectToSpawn.activeSelf || objectToSpawn ==  null)
        {
            var pool = pools.SingleOrDefault(x => x.id == id);
            if (pool != null)
            {
                objectToSpawn = Instantiate(pool.prefab);
                queue.Enqueue(objectToSpawn);
            }else return null;
        }
        objectToSpawn.SetActive(true);
        var t = objectToSpawn.transform;
        t.SetPositionAndRotation(position, rotation);
        queue.Enqueue(objectToSpawn);
        return objectToSpawn;
    }
}
