using Base;
using Controllers;
using Core;
using Cysharp.Threading.Tasks;
using Enums;
using Game.Level.Objects;
using Managers;
using UI;

namespace Game.States.GameStates.InGameStates
{
    public class InGameState : StateMachine
    {
        private readonly LevelManager _levelManager;
        private readonly InGameView _inGameView;
        private readonly LoadingView _loadingView;
        private readonly Stopwatch _timer;

        private readonly IdleState _idleState;
        private readonly OnMouseDownState _onMouseDownState;
        private readonly OnMouseUpState _onMouseUpState;

        private readonly CollectableController _collectableController;

        public InGameState(IContainer container)
        {
            var uiComponent = container.GetComponent<UIComponent>();
            _inGameView = uiComponent.GetCanvas<InGameView>();
            _loadingView = uiComponent.GetCanvas<LoadingView>();
            var timerView = _inGameView.StopwatchView;

            _timer = new Stopwatch(timerView);
            _levelManager = container.GetComponent<LevelManager>();
            _collectableController = new CollectableController();
            container.Resolve(_collectableController);

            _idleState = new IdleState(container);
            _onMouseDownState = new OnMouseDownState(container);
            _onMouseUpState = new OnMouseUpState(container);

            SetupSubStatesAndTransitions();
        }

        private void SetupSubStatesAndTransitions()
        {
            AddSubState(_idleState);
            AddSubState(_onMouseDownState);
            AddSubState(_onMouseUpState);

            AddTransition(_idleState, _onMouseDownState, (int)StateTriggers.OnMouseDownTrigger);
            AddTransition(_onMouseDownState, _onMouseUpState, (int)StateTriggers.OnMouseUpTrigger);
            AddTransition(_onMouseUpState, _idleState, (int)StateTriggers.IdleTrigger);
        }

        protected override void OnEnter()
        {
            _levelManager.onLevelCompleted += OnLevelCompleted;
            _levelManager.onLevelFailed += OnLevelFailed;
            _inGameView.BackButton.onClick.AddListener(ReturnToLobby);

            _timer.StartStopwatch(_levelManager.GetCurrentLevelDuration(),
                OnLevelFailed);


            _inGameView.Show();
        }

        private void ReturnToLobby()
        {
            _loadingView.FakeLoading().Forget();
            SendTrigger((int)StateTriggers.GoToLobbyRequest);
        }

        private void OnLevelCompleted()
        {
            SendTrigger((int)StateTriggers.LevelSuccessTrigger);
        }

        private void OnLevelFailed()
        {
            SendTrigger((int)StateTriggers.LevelFailTrigger);
        }

        protected override void OnExit()
        {
            _levelManager.onLevelCompleted -= OnLevelCompleted;
            _levelManager.onLevelFailed -= OnLevelFailed;
            _inGameView.BackButton.onClick.RemoveListener(ReturnToLobby);

            _timer.Reset();
            _levelManager.Reset();
            _collectableController.Reset();
        }
    }
}