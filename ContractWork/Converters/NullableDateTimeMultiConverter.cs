namespace ContractWork.Converters;

public class NullableDateTimeMultiConverter : IMultiValueConverter {
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
        DateTime? date = values[0] as DateTime?;
        string? status = values[1] as string;

        if (date.HasValue) {
            return date.Value.ToString("M/dd/yy");
        }
        else {
            return status;
        }
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }
}
