namespace ContractWork.Converters;
public class DateTimeConverter : IValueConverter {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {

        if (value is DateTime) {
            DateTime test = (DateTime)value;
            string date = test.ToString("MM/dd/yyyy");            //"d/M/yyyy");    //"MM/dd/yyyy"
            return date;
        }
        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }
}
