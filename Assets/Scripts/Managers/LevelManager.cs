using System;
using System.Collections.Generic;
using Base;
using Controllers;
using Core;
using Game.Level;
using Game.Level.Objects;
using UI;

namespace Managers
{
    public class LevelManager : IInitializable
    {
        private const int MaxGoalCount = 5;

        public Action onLevelCompleted;
        public Action onLevelFailed;

        private PoolManager _poolManager;

        private AccountController _accountController;

        private Dictionary<ObjectData, GoalController> _goalDictionary;

        private List<ObjectPool> _levelPools = new();


        private LevelContainer _levelContainer;

        private GameArea _gameArea;
        private InGameView _inGameView;


        public void Initialize(IContainer container)
        {
            _accountController = container.GetComponent<AccountController>();
            _levelContainer = container.GetComponent<LevelContainer>();
            _inGameView = container.GetComponent<UIComponent>().GetCanvas<InGameView>();
            _goalDictionary = new Dictionary<ObjectData, GoalController>(MaxGoalCount);
            _gameArea = container.GetComponent<GameArea>();
            _poolManager = container.GetComponent<PoolManager>();
        }

        public void PrepareLevel()
        {
            PrepareUI();
            CreateCollectableObjects();
        }

        private void PrepareUI()
        {
            var levelData = GetLevelData();
            var goalPool = _poolManager.CreatePool(_inGameView.GoalPrefab, _inGameView.GoalContainer, MaxGoalCount);
            if (!_levelPools.Contains(goalPool)) _levelPools.Add(goalPool);

            foreach (var goal in levelData.goals)
            {
                if (_goalDictionary.ContainsKey(goal.desiredObject)) continue;
                GoalController goalController =
                    goalPool.SpawnObject<GoalController>(parent: _inGameView.GoalContainer);
                goalController.SetGoal(goal);
                _goalDictionary.Add(goal.desiredObject, goalController);
                goalController.Show();
            }
        }

        private void CreateCollectableObjects()
        {
            var levelData = GetLevelData();
            var points = _gameArea.GenerateRandomPoints(levelData.totalCollectableObjectCount);
            foreach (var goalObjectData in levelData.goals)
            {
                var pool = _poolManager.GetPool(goalObjectData.desiredObject.Prefab);
                if (!_levelPools.Contains(pool)) _levelPools.Add(pool);
                for (int i = 0; i < goalObjectData.desiredAmount; i++)
                {
                    var point = points[^1];
                    points.RemoveAt(points.Count - 1);
                    var collectable = pool.SpawnObject<Collectable>(point);
                    collectable.SetObjectData(goalObjectData.desiredObject);
                    collectable.TurnOnPhysics();
                }
            }

            foreach (var objectData in levelData.optionals)
            {
                var pool = _poolManager.GetPool(objectData.Object.Prefab);
                if (!_levelPools.Contains(pool)) _levelPools.Add(pool);
                for (int i = 0; i < objectData.amount; i++)
                {
                    var point = points[^1];
                    points.RemoveAt(points.Count - 1);
                    var collectable = pool.SpawnObject<Collectable>(point);
                    collectable.SetObjectData(objectData.Object);
                    collectable.TurnOnPhysics();
                }
            }
        }

        public void DecreaseGoalAmount(ObjectData objectData)
        {
            if (_goalDictionary.TryGetValue(objectData, out var goalController))
            {
                goalController.DecreaseTargetAmount();
                if (goalController.IsGoalCompleted())
                {
                    _goalDictionary.Remove(objectData);
                    goalController.Hide();
                }
            }

            CheckLevelCompletion();
        }

        private void CheckLevelCompletion()
        {
            if (_goalDictionary.Count > 0) return;
            _accountController.AddCoins(GetLevelData().prize);
            _accountController.LevelUp();
            onLevelCompleted?.Invoke();
        }

        public void InvokeLevelFailed()
        {
            onLevelFailed?.Invoke();
        }

        private LevelData GetLevelData()
        {
            return _levelContainer.GetLevelData(_accountController.GetLevel() - 1);
        }

        public int GetCurrentLevelDuration()
        {
            return GetLevelData().duration;
        }

        public void Reset()
        {
            _goalDictionary.Clear();

            foreach (var objectPool in _levelPools)
            {
                objectPool.DestroyAllObjects();
            }

            onLevelCompleted = null;
            onLevelFailed = null;
        }
    }
}