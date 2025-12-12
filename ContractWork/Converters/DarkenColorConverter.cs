namespace ContractWork.Converters;

public class DarkenColorConverter : IValueConverter {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        if (value == null) return null;
        double percentage = 0.8;

        Brush newDarkerColor;
        SolidColorBrush scb = new SolidColorBrush();

        if (value is SolidColorBrush solidColorBrush) {
            scb.Color = (value as SolidColorBrush).Color;
            Color scbcolor = scb.Color;

            newDarkerColor = new SolidColorBrush(
            Color.FromRgb((byte)(scbcolor.R * percentage), (byte)(scbcolor.G * percentage), (byte)(scbcolor.B * percentage)));

            return newDarkerColor;
        }

        if (value is Color color) {
            //SolidColorBrush scb = new SolidColorBrush();

            newDarkerColor = new SolidColorBrush(
            Color.FromRgb((byte)(color.R * percentage), (byte)(color.G * percentage), (byte)(color.B * percentage)));



            //    (
            //Color.FromRgb((byte)(color.R * percentage), (byte)(color.G * percentage), (byte)(color.B * percentage)));

            return newDarkerColor;
        }

        //if (value is Brush brush) {
        //    if (value is LinearGradientBrush linGB) {
        //        LinearGradientBrush newLinGB= new LinearGradientBrush();
        //        Color newGradientColor;

        //        foreach (GradientStop color in linGB.GradientStops) {

        //            newGradientColor = new Color(
        //            Color.FromRgb((byte)(color.Color.R * percentage), 
        //                (byte)(color.Color.G * percentage), 
        //                (byte)(color.Color.B * percentage)));


        //            newLinGB.GradientStops.
        //                //.Add(newGradientColor);

        //        }

        //        return newLinGB;


        //    }

        //    if (value is RadialGradientBrush radGB) {
        //        return value;
        //    }

        //}

        if (value is string stringBrush) return value;


        //(value as SolidColorBrush).Color;


        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }
}
