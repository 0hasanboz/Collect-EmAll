using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UI;

namespace Core
{
    public class Stopwatch
    {
        private readonly StopwatchView _stopwatchView;

        private Action _onTimerEnd;
        private int _remainingTime;
        private CancellationTokenSource _timerCts;

        public Stopwatch(StopwatchView stopwatchView)
        {
            _stopwatchView = stopwatchView;
        }

        public async void StartStopwatch(int duration, Action onTimerEnd)
        {
            _onTimerEnd = onTimerEnd;
            _timerCts = new CancellationTokenSource();
            _remainingTime = duration;
            var oneSecond = new TimeSpan(TimeSpan.TicksPerSecond);
            _stopwatchView.UpdateTimer(_remainingTime);
            while (_remainingTime > 0)
            {
                var cancelled = await UniTask
                    .Delay(oneSecond, cancellationToken: _timerCts.Token)
                    .SuppressCancellationThrow();
                if (cancelled) return;
                _remainingTime--;
                _stopwatchView.UpdateTimer(_remainingTime);
            }

            _onTimerEnd?.Invoke();
            _timerCts = null;
        }

        public void StopTimer()
        {
            _timerCts.Cancel();
        }

        public void ResumeTimer()
        {
            _timerCts = new CancellationTokenSource();
            StartStopwatch(_remainingTime, _onTimerEnd);
        }

        public void Reset()
        {
            _onTimerEnd = null;
            _stopwatchView.UpdateTimer(0);
            _remainingTime = 0;
            _timerCts?.Cancel();
            _timerCts = null;
        }
    }
}