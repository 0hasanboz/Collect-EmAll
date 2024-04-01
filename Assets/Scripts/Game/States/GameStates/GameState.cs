using Base;
using Core;
using Enums;
using Game.States.GameStates.InGameStates;
using UI;
using UnityEngine;

namespace Game.States.GameStates
{
    public class GameState : StateMachine
    {
        private readonly InGameView _inGameView;

        private readonly PrepareGameState _prepareGameState;
        private readonly InGameState _inGameState;
        private readonly LevelCompleteState _levelCompleteState;
        private readonly LevelFailState _levelFailState;

        public GameState(ComponentContainer container)
        {
            var uiComponent = container.GetComponent<UIComponent>();
            _inGameView = uiComponent.GetCanvas<InGameView>();

            _prepareGameState = new PrepareGameState(container);
            _inGameState = new InGameState(container);
            _levelCompleteState = new LevelCompleteState(uiComponent);
            _levelFailState = new LevelFailState(uiComponent);

            SetupSubStatesAndTransitions();
        }

        private void SetupSubStatesAndTransitions()
        {
            AddSubState(_prepareGameState);
            AddSubState(_inGameState);
            AddSubState(_levelCompleteState);
            AddSubState(_levelFailState);

            AddTransition(_prepareGameState, _inGameState, (int)StateTriggers.LevelStartTrigger);
            AddTransition(_inGameState, _levelCompleteState, (int)StateTriggers.LevelSuccessTrigger);
            AddTransition(_inGameState, _levelFailState, (int)StateTriggers.LevelFailTrigger);
            AddTransition(_levelFailState, _prepareGameState, (int)StateTriggers.GoToGameStateRequest);
        }

        protected override void OnExit()
        {
            _inGameView.Hide();
        }
    }
}