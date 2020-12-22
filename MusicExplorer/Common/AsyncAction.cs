using System;
using System.Threading;
using System.Threading.Tasks;
using PropertyChanged;

namespace MusicExplorer.Old.Common {
    [AddINotifyPropertyChangedInterface]
    public sealed class AsyncAction {
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public bool IsBusy { get; private set; }

        public event Action<bool> Completed;
        public event Action Started;

        private void OnStarted() {
            this.Started?.Invoke();
        }

        private void OnCompleted(bool succeeded) {
            this.Completed?.Invoke(succeeded);
        }

        public void Execute(Func<CancellationToken, Task> action, bool cancelIfExecuting = true) {
            this.ExecuteInternal(action, cancelIfExecuting);
        }

        private async void ExecuteInternal(Func<CancellationToken, Task> action, bool cancelIfExecuting) {
            if (cancelIfExecuting)
                this.CancelIfBusy();

            this._cancellationTokenSource = new CancellationTokenSource();

            var token = this._cancellationTokenSource.Token;

            this.IsBusy = true;

            this.OnStarted();

            var isCompleted = false;

            try {
                var task = action(token);

                await task;

                token.ThrowIfCancellationRequested();

                isCompleted = true;
            } catch (OperationCanceledException) when (token.IsCancellationRequested) {
            } finally {
                this.IsBusy = false;
                this.OnCompleted(isCompleted);
            }
        }

        private void CancelIfBusy() {
            this._cancellationTokenSource.Cancel();
        }

        public void Cancel() {
            this.CancelIfBusy();
        }
    }
}