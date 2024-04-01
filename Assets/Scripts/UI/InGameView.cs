using Base;
using TMPro;
using UnityEngine;

namespace UI
{
    public class InGameView : GameView
    {
        [SerializeField] private LevelCompleteView _levelCompleteView;
        [SerializeField] private LevelFailView _levelFailView;
        [SerializeField] private StopwatchView stopwatchView;
        [SerializeField] private CustomButton _backButton;
        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private Transform _goalContainer;
        [SerializeField] private GameObject _goalPrefab;

        public LevelCompleteView LevelCompleteView => _levelCompleteView;
        public LevelFailView LevelFailView => _levelFailView;
        public StopwatchView StopwatchView => stopwatchView;
        public CustomButton BackButton => _backButton;

        public GameObject GoalPrefab => _goalPrefab;
        public Transform GoalContainer => _goalContainer;


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
            _levelText.text = $"Level {level}";
        }
    }
}