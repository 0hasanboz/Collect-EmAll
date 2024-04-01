using Base;
using Controllers;
using Core;
using Cysharp.Threading.Tasks;
using Enums;
using Managers;
using UI;

namespace Game.States.GameStates
{
    public class PrepareGameState : StateMachine
    {
        private readonly LoadingView _loadingView;
        private readonly InGameView _inGameView;
        private readonly LevelManager _levelManager;
        private readonly AccountController _accountController;

        public PrepareGameState(ComponentContainer container)
        {
            var uiComponent = container.GetComponent<UIComponent>();
            _loadingView = uiComponent.GetCanvas<LoadingView>();
            _inGameView = uiComponent.GetCanvas<InGameView>();
            _levelManager = container.GetComponent<LevelManager>();
            _accountController = container.GetComponent<AccountController>();
        }

        protected override void OnEnter()
        {
            _inGameView.UpdateLevelText(_accountController.GetLevel());
            _levelManager.PrepareLevel();
            _loadingView.Show();
            _loadingView.FakeLoading(OnLoadingComplete).Forget();
        }

        protected override void OnExit()
        {
            _loadingView.Hide();
        }

        private void OnLoadingComplete()
        {
            SendTrigger((int)StateTriggers.LevelStartTrigger);
        }
    }
}