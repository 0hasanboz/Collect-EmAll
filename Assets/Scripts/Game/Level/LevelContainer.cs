using UnityEngine;

namespace Game.Level
{
    [CreateAssetMenu(fileName = "LevelContainer", menuName = "Level/LevelContainer")]
    public class LevelContainer : ScriptableObject
    {
        [SerializeField] private ObjectData[] _objects;
        [SerializeField] private LevelData[] _levels;
        public LevelData[] Levels => _levels;

        public LevelData GetLevelData(int levelIndex)
        {
            levelIndex = Mathf.Clamp(levelIndex, 0, _levels.Length - 1);
            
            if (levelIndex < 0 || levelIndex >= _levels.Length) return null;
            
            var levelData = _levels[levelIndex];
            CheckLevelData(levelData);
            return levelData;

        }

        private void CheckLevelData(LevelData level)
        {
            var totalCollectableObjects = 0;
            for (var i = 0; i < level.goals.Length; i++)
            {
                var goal = level.goals[i];
                var mod = goal.desiredAmount % 3;
                if (mod != 0)
                {
                    level.goals[i].desiredAmount += 3 - mod;
                }

                totalCollectableObjects += goal.desiredAmount;
            }

            for (var i = 0; i < level.optionals.Length; i++)
            {
                var optional = level.optionals[i];
                var mod = optional.amount % 3;
                if (mod != 0)
                {
                    level.optionals[i].amount += 3 - mod;
                }

                totalCollectableObjects += optional.amount;
            }

            level.totalCollectableObjectCount = totalCollectableObjects;
        }
    }
}