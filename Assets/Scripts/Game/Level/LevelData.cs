using System;
using UnityEngine;

namespace Game.Level
{
    [Serializable]
    public class LevelData
    {
        public int prize = 100;
        public int duration;
        public GoalObjectData[] goals;
        public OptionalObjectData[] optionals;
        [HideInInspector] public int totalCollectableObjectCount;
    }
}