using Base;
using TMPro;
using UnityEngine;

namespace UI
{
    public class StopwatchView : GameView
    {
        [SerializeField] private TMP_Text _timerText;

        public override void Show()
        {
            gameObject.SetActive(true);
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
        }

        public void UpdateTimer(int duration)
        {
            _timerText.text = GetMinuteSecondFormat(duration);
        }

        private string GetMinuteSecondFormat(int duration)
        {
            int minutes = duration / 60;
            int seconds = duration % 60;
            return $"{minutes:00}:{seconds:00}";
        }
    }
}