namespace ContractWork.Converters;
public class ComplementaryColorsConverter : IValueConverter {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        Brush ComplementaryColor = new SolidColorBrush();
        Brush BackGroundColor = (Brush)value;

        if (BackGroundColor == null) ComplementaryColor = BackGroundColor;

        if (BackGroundColor == Brushes.White) ComplementaryColor = Brushes.Black;
        if (BackGroundColor == Brushes.Black) ComplementaryColor = Brushes.White;

        return ComplementaryColor;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }
}
