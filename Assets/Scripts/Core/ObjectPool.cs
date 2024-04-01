using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class ObjectPool
    {
        private GameObject _prefab;
        private Transform _parent;
        private Vector3 _defaultScale;
        private List<GameObject> _pool;
        private List<GameObject> _activeObjects = new();

        public ObjectPool(GameObject prefab, Transform parent, int initialSize)
        {
            _defaultScale = prefab.transform.localScale;
            _prefab = prefab;
            _parent = parent;
            _pool = new List<GameObject>();

            for (int i = 0; i < initialSize; i++)
            {
                CreateNewObject();
            }
        }

        private void CreateNewObject()
        {
            var instance = Object.Instantiate(_prefab, _parent);
            instance.gameObject.SetActive(false);
            _pool.Add(instance);
        }

        private T GetObject<T>() where T : MonoBehaviour
        {
            if (_pool.Count == 0)
            {
                CreateNewObject();
            }

            GameObject obj = _pool[0];
            _pool.Remove(obj);
            return obj.GetComponent<T>();
        }

        public T SpawnObject<T>(Vector3 position = new(), Transform parent = null) where T : MonoBehaviour
        {
            var obj = GetObject<T>();
            obj.transform.position = position;
            if (parent != null) obj.transform.SetParent(parent);
            obj.gameObject.SetActive(true);
            _activeObjects.Add(obj.gameObject);
            return obj;
        }

        public void DestroyObject(GameObject obj)
        {
            obj.transform.localScale = _defaultScale;
            obj.transform.SetParent(_parent);
            _pool.Add(obj);
            obj.gameObject.SetActive(false);
        }

        public void DestroyAllObjects()
        {
            foreach (var obj in _activeObjects)
            {
                obj.transform.localScale = _defaultScale;
                obj.transform.SetParent(_parent);
                obj.gameObject.SetActive(false);
                _pool.Add(obj);
            }
        }
    }
}