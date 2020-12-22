using System;
using System.Collections.Generic;
using System.Windows.Threading;

namespace MusicExplorer.Old {
    internal static class WeakEventHandlerManager {
        public static void AddWeakReferenceHandler(ref List<WeakReference> handlers, EventHandler handler) {
            if (handlers == null)
                handlers = new List<WeakReference>();
            handlers.Add(new WeakReference(handler));
        }

        public static void RemoveWeakReferenceHandler(List<WeakReference> handlers, EventHandler handler) {
            if (handlers == null)
                return;
            for (var index = handlers.Count - 1; index >= 0; --index) {
                if (!(handlers[index].Target is EventHandler eventHandler) || eventHandler == handler)
                    handlers.RemoveAt(index);
            }
        }

        public static void CallWeakReferenceHandlers(object sender, List<WeakReference> handlers) {
            if (handlers == null)
                return;
            var callees = new EventHandler[handlers.Count];

            var num = CleanupOldHandlers(handlers, callees, 0);

            for (var index = 0; index < num; ++index)
                CallHandler(sender, callees[index]);
        }

        private static void CallHandler(object sender, EventHandler eventHandler) {
            if (eventHandler == null)
                return;

            var currentDispatcher = Dispatcher.CurrentDispatcher;

            if (!currentDispatcher.CheckAccess())
                currentDispatcher.BeginInvoke(new Action<object, EventHandler>(CallHandler), sender, eventHandler);
            else
                eventHandler(sender, EventArgs.Empty);
        }

        private static int CleanupOldHandlers(IList<WeakReference> handlers, IList<EventHandler> callees, int count) {
            for (var index = handlers.Count - 1; index >= 0; --index) {
                var weakReference = handlers[index];
                if (!(weakReference.Target is EventHandler eventHandler) || !weakReference.IsAlive)
                    handlers.RemoveAt(index);
                else {
                    callees[count] = eventHandler;
                    ++count;
                }
            }

            return count;
        }
    }
}