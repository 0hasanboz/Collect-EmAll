using Base;
using Cysharp.Threading.Tasks;
using Enums;
using UI;

namespace Game.States.GameStates
{
    public class LevelFailState : StateMachine
    {
        private readonly LevelFailView _levelFailView;
        private readonly LoadingView _loadingView;

        public LevelFailState(UIComponent uiComponent)
        {
            _levelFailView = uiComponent.GetCanvas<InGameView>().LevelFailView;
            _loadingView = uiComponent.GetCanvas<LoadingView>();
        }

        protected override void OnEnter()
        {
            _levelFailView.RetryButton.onClick.AddListener(RetryLevel);
            _levelFailView.QuitButton.onClick.AddListener(ReturnToLobby);
            _levelFailView.Show();
        }

        private void ReturnToLobby()
        {
            _loadingView.FakeLoading().Forget();
            SendTrigger((int)StateTriggers.GoToLobbyRequest);
        }

        private void RetryLevel()
        {
            SendTrigger((int)StateTriggers.GoToGameStateRequest);
        }

        protected override void OnExit()
        {
            _levelFailView.RetryButton.onClick.RemoveListener(RetryLevel);
            _levelFailView.Hide();
        }
    }
}