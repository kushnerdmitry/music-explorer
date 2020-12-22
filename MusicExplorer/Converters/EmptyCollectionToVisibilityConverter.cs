using System;
using System.Collections;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MusicExplorer.Old.Converters {
    public sealed class EmptyCollectionToVisibilityConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is string || !(value is IEnumerable ienumerable))
                return Visibility.Collapsed;

            return ienumerable.GetEnumerator().MoveNext()
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}