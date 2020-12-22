using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MusicExplorer.Old.Common {
    public static class Utils {
        private const int RetriesCount = 5;

        /*public static T WithRetry<T>(Func<T> func, int retries = RetriesCount) {
            Exception ex = null;

            foreach (var i in Enumerable.Range(1, retries)) {
                try {
                    return func();
                } catch (Exception e) //when (ex == null i < retries)
                {
                    if (ex == null)
                        ex = e;
                }
            }

            throw ex;
        }*/

        public static async Task<T> WithRetry<T>(Func<Task<T>> func, CancellationToken token = default,
            int retries = RetriesCount) {
            Exception ex = null;

            foreach (var i in Enumerable.Range(1, retries)) {
                try {
                    return await Task.Run(func, token);
                }
                catch (Exception e) /*when ( i < retries)*/ {
                    if (ex == null)
                        ex = e;
                }
            }

            return await Task.FromException<T>(ex);
        }
    }
}