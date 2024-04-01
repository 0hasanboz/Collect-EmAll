using Base;
using UnityEngine;

namespace UI
{
    public class LevelCompleteView : GameView
    {
        [SerializeField] private CustomButton _resumeButton;
        public CustomButton ResumeButton => _resumeButton;
        
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