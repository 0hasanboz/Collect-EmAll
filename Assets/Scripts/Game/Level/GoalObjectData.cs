using System;

namespace Game.Level
{
    [Serializable]
    public struct GoalObjectData
    {
        public int desiredAmount;
        public ObjectData desiredObject;
    }
}