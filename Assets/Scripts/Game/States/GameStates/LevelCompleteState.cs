using Base;
using Cysharp.Threading.Tasks;
using Enums;
using UI;

namespace Game.States.GameStates
{
    public class LevelCompleteState : StateMachine
    {
        private readonly LevelCompleteView _levelCompleteView;
        private readonly LoadingView _loadingView;

        public LevelCompleteState(UIComponent uiComponent)
        {
            _levelCompleteView = uiComponent.GetCanvas<InGameView>().LevelCompleteView;
            _loadingView = uiComponent.GetCanvas<LoadingView>();
        }

        protected override void OnEnter()
        {
            _levelCompleteView.ResumeButton.onClick.AddListener(ReturnToLobby);
            _levelCompleteView.Show();
        }

        private void ReturnToLobby()
        {
            _loadingView.FakeLoading().Forget();
            SendTrigger((int)StateTriggers.GoToLobbyRequest);
        }

        protected override void OnExit()
        {
            _levelCompleteView.ResumeButton.onClick.RemoveListener(ReturnToLobby);
            _levelCompleteView.Hide();
        }
    }
}