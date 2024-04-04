using Base;
using Controllers;
using Core;
using Game.Level;
using Game.Level.Objects;
using Game.States;
using Managers;
using UI;
using UnityEngine;

namespace Game
{
    public class GameInstaller : MonoBehaviour
    {
        [SerializeField] private Showcase _showcase;
        [SerializeField] private GameArea _gameArea;
        [SerializeField] private UIComponent _uiComponent;
        [SerializeField] private LevelContainer _levelContainer;

        private ComponentContainer _componentContainer;
        private AccountController _accountController;
        private AppState _appState;

        private void Awake()
        {
            Application.targetFrameRate = 60;
            CreateComponentContainer();
            CreatePoolManager();
            RegisterSceneObjects();
            CreateIDatabase();
            CreateAccountController();
            CreateLevelComponent();

            CreateAppState();
        }

        private void CreateComponentContainer()
        {
            _componentContainer = new ComponentContainer();
        }

        private void CreatePoolManager()
        {
            var poolManager = new PoolManager();
            _componentContainer.AddComponent(poolManager);
        }

        private void RegisterSceneObjects()
        {
            _componentContainer.AddComponent(_showcase);
            _componentContainer.AddComponent(_gameArea);
            _componentContainer.AddComponent(_uiComponent);
        }

        private void CreateIDatabase()
        {
            IDatabase database = new LocalDatabase();
            _componentContainer.AddComponent(database);
        }
        
        private void CreateAccountController()
        {
            _accountController = new AccountController();
            _componentContainer.AddComponent(_accountController);
        }

        private void CreateLevelComponent()
        {
            _componentContainer.AddComponent(_levelContainer);
            var levelComponent = new LevelManager();
            _componentContainer.AddComponent(levelComponent);
        }

        private void CreateAppState()
        {
            _appState = new AppState(_componentContainer);
        }

        private void Start()
        {
            _appState.Enter();
        }

        private void Update()
        {
            _appState.Update();
        }
        
        private void OnApplicationQuit()
        {
            _appState.Exit();
        }
    }
}