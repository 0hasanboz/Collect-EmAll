using System;
using System.Collections.Generic;
using Base;
using Statics;
using UnityEngine;

namespace Core
{
    public class LocalDatabase : IInitializable, IDatabase, IDisposable
    {
        public Dictionary<string, object> _data = new();

        public void Initialize(IContainer container)
        {
            Load();
        }

        public void Save()
        {
            foreach (var key in _data.Keys)
            {
                var data = _data[key];
                var stringData = JsonUtility.ToJson(data);
                PlayerPrefs.SetString(key, stringData);
            }
        }

        public void Load()
        {
            foreach (var values in DatabaseKeys.DataKeyDictionary.Keys)
            {
                var key = values.Item1;
                var type = values.Item2;
                if (PlayerPrefs.HasKey(values.Item1))
                {
                    var stringData = PlayerPrefs.GetString(key);
                    var data = JsonUtility.FromJson(stringData, type);
                    _data.Add(key, data);
                }
                else
                {
                    _data.Add(key, DatabaseKeys.DataKeyDictionary[values]());
                }
            }
        }

        public T GetData<T>(string key)
        {
            if (!_data.ContainsKey(key))
            {
                Logger.Log("Key not found: " + key);
                return default;
            }

            var data = _data[key];
            if (data is T)
                return (T)data;

            Logger.Log("Data type mismatch for key: " + key, Color.red);
            return default;
        }

        public void Dispose()
        {
            Save();
        }
    }
}