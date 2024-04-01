using System.Collections.Generic;
using Base;
using Core;
using UnityEngine;

namespace Managers
{
    public class PoolManager
    {
        private Dictionary<GameObject, ObjectPool> _poolDictionary;

        private readonly Transform _defaultParent;

        public PoolManager()
        {
            _poolDictionary = new Dictionary<GameObject, ObjectPool>();
            _defaultParent = new GameObject(nameof(PoolManager)).transform;
        }

        public ObjectPool GetPool(GameObject prefab)
        {
            if (!_poolDictionary.ContainsKey(prefab))
            {
                CreatePool(prefab, _defaultParent, 1);
            }

            return _poolDictionary[prefab];
        }

        public ObjectPool CreatePool(GameObject prefab, Transform parent, int initialSize)
        {
            if (!_poolDictionary.ContainsKey(prefab))
                _poolDictionary.Add(prefab, new ObjectPool(prefab, parent, initialSize));

            return _poolDictionary[prefab];
        }
    }
}