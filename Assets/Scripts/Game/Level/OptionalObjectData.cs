using System;
using UnityEngine;

namespace Game.Level
{
    [Serializable]
    public class OptionalObjectData
    {
        [SerializeField] private ObjectData _object;
        public int amount;
        public ObjectData Object => _object;
    }
}