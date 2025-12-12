namespace ContractWork.Converters;

public class CornerRadiusRatioConverter : IValueConverter {

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        int divideby;
        object tmpreturn;

        if (parameter == null) return (double)value / 2;
        else {
            int.TryParse(value.ToString(), out divideby);

            tmpreturn = (object)((double)value / (int)parameter);
            //return (object)((double)value / (int)parameter);
            return tmpreturn;
        }
    }


    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }
}