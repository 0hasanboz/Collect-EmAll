using Base;
using Cysharp.Threading.Tasks;
using Enums;
using UI;

namespace Game.States.LoadingStates
{
    public class LoadingState : StateMachine
    {
        private UIComponent _uiComponent;
        private LoadingView _loadingView;

        public LoadingState(IContainer container)
        {
            _uiComponent = container.GetComponent<UIComponent>();
            _loadingView = _uiComponent.GetCanvas<LoadingView>();
        }

        protected override void OnEnter()
        {
            _loadingView.FakeLoading(() => SendTrigger((int)StateTriggers.GoToLobbyRequest)).Forget();
        }
    }
}