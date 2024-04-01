using Base;
using Core;
using Enums;
using Game.States.GameStates;
using Game.States.LoadingStates;
using Game.States.LobbyStates;

namespace Game.States
{
    public class AppState : StateMachine
    {
        private readonly ComponentContainer _container;
        private readonly LoadingState _loadingState;
        private readonly LobbyState _lobbyState;
        private readonly GameState _gameState;

        public AppState(ComponentContainer container)
        {
            _container = container;
            _loadingState = new LoadingState(container);
            _lobbyState = new LobbyState(container);
            _gameState = new GameState(container);

            SetupSubStatesAndTransitions();
            container.InitializeComponents();
        }

        private void SetupSubStatesAndTransitions()
        {
            AddSubState(_loadingState);
            AddSubState(_lobbyState);
            AddSubState(_gameState);

            AddTransition(_loadingState, _lobbyState, (int)StateTriggers.GoToLobbyRequest);
            AddTransition(_lobbyState, _gameState, (int)StateTriggers.GoToGameStateRequest);
            AddTransition(_gameState, _lobbyState, (int)StateTriggers.GoToLobbyRequest);
        }

        protected override void OnEnter()
        {
            _container.StartComponents();
        }

        protected override void OnUpdate()
        {
            _container.Update();
        }

        protected override void OnExit()
        {
            _container.Dispose();
        }
    }
}