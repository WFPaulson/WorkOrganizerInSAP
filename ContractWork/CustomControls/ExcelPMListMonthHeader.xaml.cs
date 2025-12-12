namespace ContractWork.CustomControls;


public partial class ExcelPMListMonthHeader : UserControl
{

    public string mnthDate {
        get { return (string)GetValue(mnthDateProperty); }
        set { SetValue(mnthDateProperty, value); }
    }
    public static readonly DependencyProperty mnthDateProperty =
        DependencyProperty.Register(nameof(mnthDate), typeof(string), typeof(ExcelPMListMonthHeader),
            new PropertyMetadata("Jan"));


    public SolidColorBrush cellColor {
        get { return (SolidColorBrush)GetValue(cellColorProperty); }
        set { SetValue(cellColorProperty, value); }
    }
    public static readonly DependencyProperty cellColorProperty =
        DependencyProperty.Register(nameof(cellColor), typeof(SolidColorBrush), typeof(ExcelPMListMonthHeader),
            new PropertyMetadata((SolidColorBrush)new BrushConverter().ConvertFrom("#CC99FF")));         //"#CC99FF"


    public SolidColorBrush borderColor {
        get { return (SolidColorBrush)GetValue(borderColorProperty); }
        set { SetValue(borderColorProperty, value); }
    }
    public static readonly DependencyProperty borderColorProperty =
        DependencyProperty.Register(nameof(borderColor), typeof(SolidColorBrush), typeof(ExcelPMListMonthHeader),
            new PropertyMetadata((SolidColorBrush)new BrushConverter().ConvertFrom(Colors.Purple.ToString())));

    public int totalDue {
        get { return (int)GetValue(totalDueProperty); }
        set { SetValue(totalDueProperty, value);  }
    }
    public static readonly DependencyProperty totalDueProperty =
        DependencyProperty.Register(nameof(totalDue), typeof(int), typeof(ExcelPMListMonthHeader),
            new PropertyMetadata(0));

    public int totalDone {
        get { return (int)GetValue(totalDoneProperty); }
        set { SetValue(totalDoneProperty, value); }
    }
    public static readonly DependencyProperty totalDoneProperty =
        DependencyProperty.Register(nameof(totalDone), typeof(int), typeof(ExcelPMListMonthHeader),
            new PropertyMetadata(0));



    public ExcelPMListMonthHeader()
    {
        InitializeComponent();
    }
}
