using Base;
using Controllers;
using Core;
using Enums;
using UI;

namespace Game.States.LobbyStates
{
    public class LobbyState : StateMachine
    {
        private readonly AccountController _accountController;
        private readonly LobbyView _lobbyView;
        private readonly CustomButton _levelStartButton;

        public LobbyState(ComponentContainer container)
        {
            var uiComponent = container.GetComponent<UIComponent>();
            _lobbyView = uiComponent.GetCanvas<LobbyView>();
            _levelStartButton = _lobbyView.LevelStartButton;
            _accountController = container.GetComponent<AccountController>();
        }

        protected override void OnEnter()
        {
            _accountController.OnCoinsChanged += _lobbyView.UpdateCoinText;
            _levelStartButton.onClick.AddListener(GoToLevel);
            
            _lobbyView.UpdateLevelText(_accountController.GetLevel());
            _lobbyView.UpdateCoinText(_accountController.GetCoinAmount());
            
            _lobbyView.Show();
        }

        private void GoToLevel()
        {
            SendTrigger((int)StateTriggers.GoToGameStateRequest);
        }

        protected override void OnExit()
        {
            _accountController.OnCoinsChanged -= _lobbyView.UpdateCoinText;
            _levelStartButton.onClick.RemoveListener(GoToLevel);
            
            _lobbyView.Hide();
        }
    }
}