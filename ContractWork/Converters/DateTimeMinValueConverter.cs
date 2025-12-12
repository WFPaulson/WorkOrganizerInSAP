namespace ContractWork.Converters;
public class DateTimeMinValueConverter : IValueConverter {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {

        if (value is DateTime && (DateTime)value == DateTime.MinValue)
        {
            return null;
        }
        
        else return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }
}
