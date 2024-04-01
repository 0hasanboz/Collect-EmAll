using Base;
using TMPro;
using UnityEngine;

namespace UI
{
    public class LobbyView : GameView
    {
        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private TMP_Text _coinText;
        [SerializeField] private CustomButton _levelStartButton;
        public CustomButton LevelStartButton => _levelStartButton;

        public override void Show()
        {
            gameObject.SetActive(true);
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
        }

        public void UpdateLevelText(int level)
        {
            _levelText.text = $"{level}";
        }

        public void UpdateCoinText(int coinAmount)
        {
            _coinText.text = $"{coinAmount}";
        }
    }
}