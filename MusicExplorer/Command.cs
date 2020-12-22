using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;

namespace MusicExplorer.Old {
    public sealed class DelegateCommand : ICommand {
        private readonly Func<bool> _canExecute;
        private readonly Action _execute;
        private List<WeakReference> _canExecuteChangedHandlers;

        public DelegateCommand(Action execute, Func<bool> canExecute = null) {
            this._execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this._canExecute = canExecute;
        }

        [DebuggerStepThrough]
        bool ICommand.CanExecute(object parameter) {
            return this._canExecute == null || this._canExecute();
        }

        void ICommand.Execute(object parameter) {
            this._execute();
        }

        public event EventHandler CanExecuteChanged {
            add => WeakEventHandlerManager.AddWeakReferenceHandler(ref this._canExecuteChangedHandlers, value);
            remove => WeakEventHandlerManager.RemoveWeakReferenceHandler(this._canExecuteChangedHandlers, value);
        }

        public void InvalidateCanExecute() {
            WeakEventHandlerManager.CallWeakReferenceHandlers(this, this._canExecuteChangedHandlers);
        }

        public bool CanExecute() {
            return this._canExecute == null || this._canExecute();
        }

        public void Execute() {
            this._execute();
        }

        public bool ExecuteIfCan() {
            if (!this.CanExecute())
                return false;

            this.Execute();

            return true;
        }
    }

    public sealed class DelegateCommand<T> : ICommand {
        private readonly Predicate<T> _canExecute;
        private readonly Action<T> _execute;
        private List<WeakReference> _canExecuteChangedHandlers;

        public DelegateCommand(Action<T> execute, Predicate<T> canExecute = null) {
            this._execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this._canExecute = canExecute;
        }

        [DebuggerStepThrough]
        bool ICommand.CanExecute(object parameter) {
            return this._canExecute == null || this._canExecute((T) parameter);
        }

        void ICommand.Execute(object parameter) {
            this._execute((T) parameter);
        }

        public event EventHandler CanExecuteChanged {
            add => WeakEventHandlerManager.AddWeakReferenceHandler(ref this._canExecuteChangedHandlers, value);
            remove => WeakEventHandlerManager.RemoveWeakReferenceHandler(this._canExecuteChangedHandlers, value);
        }

        public void InvalidateCanExecute() {
            WeakEventHandlerManager.CallWeakReferenceHandlers(this, this._canExecuteChangedHandlers);
        }

        public bool CanExecute(T parameter) {
            return this._canExecute == null || this._canExecute(parameter);
        }

        public void Execute(T parameter) {
            this._execute(parameter);
        }

        public bool ExecuteIfCan(T parameter) {
            if (!this.CanExecute(parameter))
                return false;

            this.Execute(parameter);

            return true;
        }
    }
}