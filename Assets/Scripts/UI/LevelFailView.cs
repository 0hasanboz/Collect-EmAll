using Base;
using UnityEngine;

namespace UI
{
    public class LevelFailView : GameView
    {
        [SerializeField] private CustomButton _quitButton;
        [SerializeField] private CustomButton _retryButton;
        public CustomButton QuitButton => _quitButton;
        public CustomButton RetryButton => _retryButton;

        public override void Show()
        {
            gameObject.SetActive(true);
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}