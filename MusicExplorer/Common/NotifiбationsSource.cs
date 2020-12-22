using System;

namespace MusicExplorer.Old.Common {
    public struct NotificationsSource<T> : IDisposable {
        public event Action<T> NotificationTriggered;

        public void Notify(T arg) {
            this.NotificationTriggered?.Invoke(arg);
        }

        public void Dispose() {
            this.NotificationTriggered = null;
        }
    }
}