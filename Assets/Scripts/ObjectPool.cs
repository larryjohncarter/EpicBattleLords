using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : SingletonBehaviour<ObjectPool>
{
    [System.Serializable]
    public class Pool
    {
        public int index;
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

            _poolDictionary.Add(pool.index, objectPool);
        }
    }

    public GameObject SpawnFromPool(int index, Vector3 position, Quaternion rotation)
    {
        if (!_poolDictionary.ContainsKey(index))
        {
            Debug.LogWarning("Pool with index " + index + " doesn't exist.");
            return null;
        }


        var queue = _poolDictionary[index];
        var objectToSpawn = queue.Dequeue();
        if (objectToSpawn == null) return null;
        if (index == 1 && objectToSpawn.activeSelf)
        {
            objectToSpawn = Instantiate(pools[index].prefab);
            queue.Enqueue(objectToSpawn);
        }

        objectToSpawn.SetActive(true);
        var t = objectToSpawn.transform;
        t.SetParent(null);
        t.SetPositionAndRotation(position, rotation);

        queue.Enqueue(objectToSpawn);
        return objectToSpawn;
    }
}
