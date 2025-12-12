namespace ContractWork.Converters;

public class CornerRadiusPartialConverter : IValueConverter {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        double tl;
        CornerRadius cr = new CornerRadius();

        cr = (CornerRadius)value;
        tl = cr.TopLeft;

        return new CornerRadius(tl, 0, 0, tl);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }
}
