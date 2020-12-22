using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using PropertyChanged;

namespace MusicExplorer.Old.Common {
    [AddINotifyPropertyChangedInterface]
    public sealed class AsyncDelegateCommand : ICommand, INotifyPropertyChanged {
        private readonly Func<bool> _canExecute;
        private readonly Func<CancellationToken, Task> _execute;
        private List<WeakReference> _canExecuteChangedHandlers;

        public AsyncAction AsyncAction { get; private set; }

        public AsyncDelegateCommand(Func<CancellationToken, Task> execute, Func<bool> canExecute = null) {
            this._execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this._canExecute = canExecute;

            this.AsyncAction = new AsyncAction();

            this.AsyncAction.Started += () => {
                this.IsBusy = true;
                this.InvalidateCanExecute();
            };

            this.AsyncAction.Completed += _ => {
                this.IsBusy = false;
                this.InvalidateCanExecute();
            };
        }

        public bool IsBusy { get; private set; }

        [DebuggerStepThrough]
        bool ICommand.CanExecute(object parameter) {
            return this.CanExecute();
        }

        void ICommand.Execute(object parameter) {
            this.Execute();
        }

        public event EventHandler CanExecuteChanged {
            add => WeakEventHandlerManager.AddWeakReferenceHandler(ref this._canExecuteChangedHandlers, value);
            remove => WeakEventHandlerManager.RemoveWeakReferenceHandler(this._canExecuteChangedHandlers, value);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void InvalidateCanExecute() {
            WeakEventHandlerManager.CallWeakReferenceHandlers(this, this._canExecuteChangedHandlers);
        }

        public bool CanExecute() {
            return !this.IsBusy && (this._canExecute == null || this._canExecute());
        }

        public void Execute() {
            this.AsyncAction.Execute(this._execute);
        }
    }

    [AddINotifyPropertyChangedInterface]
    public sealed class AsyncDelegateCommand<T> : ICommand, INotifyPropertyChanged {
        private readonly AsyncAction _asyncAction;
        private readonly Func<T, bool> _canExecute;
        private readonly Func<T, CancellationToken, Task> _execute;
        private List<WeakReference> _canExecuteChangedHandlers;

        public AsyncDelegateCommand(Func<T, CancellationToken, Task> execute, Func<T, bool> canExecute = null) {
            this._execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this._canExecute = canExecute;

            this._asyncAction = new AsyncAction();

            this._asyncAction.Started += () => {
                this.IsBusy = true;
                this.InvalidateCanExecute();
            };

            this._asyncAction.Completed += _ => {
                this.IsBusy = false;
                this.InvalidateCanExecute();
            };
        }

        public bool IsBusy { get; private set; }

        [DebuggerStepThrough]
        bool ICommand.CanExecute(object parameter) {
            return this.CanExecute((T) parameter);
        }

        void ICommand.Execute(object parameter) {
            this.Execute((T) parameter);
        }

        public event EventHandler CanExecuteChanged {
            add => WeakEventHandlerManager.AddWeakReferenceHandler(ref this._canExecuteChangedHandlers, value);
            remove => WeakEventHandlerManager.RemoveWeakReferenceHandler(this._canExecuteChangedHandlers, value);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void InvalidateCanExecute() {
            WeakEventHandlerManager.CallWeakReferenceHandlers(this, this._canExecuteChangedHandlers);
        }

        public bool CanExecute(T parameter) {
            return !this.IsBusy && (this._canExecute == null || this._canExecute(parameter));
        }

        public void Execute(T parameter) {
            this._asyncAction.Execute(token => this._execute(parameter, token));
        }
    }
}