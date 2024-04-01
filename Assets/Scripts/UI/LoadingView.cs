using System;
using System.Threading;
using Base;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LoadingView : GameView
    {
        [SerializeField] private Slider _progressBar;
        private CancellationToken _destroyToken;

        private void Awake()
        {
            _destroyToken = this.GetCancellationTokenOnDestroy();
        }

        public float GetProgress()
        {
            return _progressBar.value;
        }

        public override void Show()
        {
            gameObject.SetActive(true);
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
            Reset();
        }

        public void SetProgress(float progress)
        {
            _progressBar.value = progress;
        }

        public void Reset()
        {
            _progressBar.value = 0;
        }

        public async UniTask<bool> FakeLoading(Action onLoaded = null, float time = 1)
        {
            Reset();
            Show();
            bool cancelled = false;
            var startTime = Time.time;
            while (Time.time - startTime < time)
            {
                SetProgress((Time.time - startTime) / time);
                cancelled = await UniTask.Yield(cancellationToken: _destroyToken).SuppressCancellationThrow();
                if (cancelled) return true;
            }

            Hide();
            onLoaded?.Invoke();
            return false;
        }
    }
}