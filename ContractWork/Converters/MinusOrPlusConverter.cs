namespace ContractWork.Converters;

public class MinusOrPlusConverter : IValueConverter {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {

        if (parameter == null) return (double)value;

        if (value is double) {
            return (double)value + (int)parameter;
        }
        else if (value is int) {
            return (int)value + (int)parameter; ;
        }

        return (double)value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }
}
//intVal = System.Convert.ToInt32(System.Math.Floor((double)value));