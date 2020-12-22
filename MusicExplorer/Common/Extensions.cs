using System;

namespace MusicExplorer.Old.Common {
    public static class Extensions {
        public static TResult Let<T, TResult>(
            this T obj,
            Func<T, TResult> transform
        ) {
            return transform(obj);
        }
    }
}
