namespace ContractWork.CustomControls;

public partial class ExcelPMListDueDoneControl : UserControl
{
    public SolidColorBrush PMCellDueBackground {
        get { return (SolidColorBrush)GetValue(PMCellDueBackgroundProperty); }
        set { SetValue(PMCellDueBackgroundProperty, value); }
    }
    public static readonly DependencyProperty PMCellDueBackgroundProperty =
        DependencyProperty.Register(nameof(PMCellDueBackground), typeof(SolidColorBrush), typeof(ExcelPMListDueDoneControl),
            new PropertyMetadata((SolidColorBrush)new BrushConverter().ConvertFrom(Colors.MediumPurple.ToString())));

    public SolidColorBrush PMCellDoneBackground {
        get { return (SolidColorBrush)GetValue(PMCellDoneBackgroundProperty); }
        set { SetValue(PMCellDoneBackgroundProperty, value); }
    }
    public static readonly DependencyProperty PMCellDoneBackgroundProperty =
        DependencyProperty.Register(nameof(PMCellDoneBackground), typeof(SolidColorBrush), typeof(ExcelPMListDueDoneControl),
            new PropertyMetadata((SolidColorBrush)new BrushConverter().ConvertFrom(Colors.White.ToString())));

    public SolidColorBrush PMCellBorderBrush {
        get { return (SolidColorBrush)GetValue(PMCellBorderBrushProperty); }
        set { SetValue(PMCellBorderBrushProperty, value); }
    }
    public static readonly DependencyProperty PMCellBorderBrushProperty =
        DependencyProperty.Register(nameof(PMCellBorderBrush), typeof(SolidColorBrush), typeof(ExcelPMListDueDoneControl),
            new PropertyMetadata((SolidColorBrush)new BrushConverter().ConvertFrom(Colors.Black.ToString())));

    public Thickness PMCellBorderThickness {
        get { return (Thickness)GetValue(PMCellBorderThicknessProperty); }
        set { SetValue(PMCellBorderThicknessProperty, value); }
    }
    public static readonly DependencyProperty PMCellBorderThicknessProperty =
        DependencyProperty.Register(nameof(PMCellBorderThickness), typeof(Thickness), typeof(ExcelPMListDueDoneControl),
            new PropertyMetadata(new Thickness(1, 1, 1, 1)));

    public string PMCellDueText {
        get { return (string)GetValue(PMCellDueTextProperty); }
        set { SetValue(PMCellDueTextProperty, value); }
    }
    public static readonly DependencyProperty PMCellDueTextProperty =
        DependencyProperty.Register(nameof(PMCellDueText), typeof(string), typeof(ExcelPMListDueDoneControl),
            new PropertyMetadata(string.Empty));

    public string PMCellDoneText {
        get { return (string)GetValue(PMCellDoneTextProperty); }
        set { SetValue(PMCellDoneTextProperty, value); }
    }
    public static readonly DependencyProperty PMCellDoneTextProperty =
        DependencyProperty.Register(nameof(PMCellDoneText), typeof(string), typeof(ExcelPMListDueDoneControl),
            new PropertyMetadata(string.Empty));

    public ICommand PMCellDoneCommand {
        get { return (ICommand)GetValue(PMCellDoneCommandProperty); }
        set { SetValue(PMCellDoneCommandProperty, value); }
    }
    public static readonly DependencyProperty PMCellDoneCommandProperty =
          DependencyProperty.Register(nameof(PMCellDoneCommand), typeof(ICommand), typeof(ExcelPMListDueDoneControl),
             new PropertyMetadata(null));

    public object PMCellDoneCmdParameter {
        get { return (object)GetValue(PMCellDoneCmdParameterProperty); }
        set { SetValue(PMCellDoneCmdParameterProperty, value); }
    }
    public static readonly DependencyProperty PMCellDoneCmdParameterProperty =
        DependencyProperty.Register(nameof(PMCellDoneCmdParameter), typeof(object), typeof(ExcelPMListDueDoneControl),
            new PropertyMetadata(null));





    public ExcelPMListDueDoneControl()
    {
        InitializeComponent();
    }
}
