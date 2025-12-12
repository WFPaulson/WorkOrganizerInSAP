namespace ContractWork.CustomControls;

public partial class btnRoundedGelControl : UserControl {
   
    private static CornerRadius defaultCornerRadius = new CornerRadius(20);

    public int RectRadiusX {
        get { return (int)GetValue(RectRadiusXProperty); }
        set { SetValue(RectRadiusXProperty, value); }
    }
    public static readonly DependencyProperty RectRadiusXProperty =
        DependencyProperty.Register(nameof(RectRadiusX), typeof(int), typeof(btnRoundedGelControl), 
            new PropertyMetadata(18));

    public int RectRadiusY {
        get { return (int)GetValue(RectRadiusYProperty); }
        set { SetValue(RectRadiusYProperty, value); }
    }
    public static readonly DependencyProperty RectRadiusYProperty =
        DependencyProperty.Register(nameof(RectRadiusY), typeof(int), typeof(btnRoundedGelControl),
            new PropertyMetadata(18));



    public int btngelWidth {
        get { return (int)GetValue(btngelWidthProperty); }
        set { SetValue(btngelWidthProperty, value); }
    }
    public static readonly DependencyProperty btngelWidthProperty =
        DependencyProperty.Register(nameof(btngelWidth), typeof(int), typeof(btnRoundedGelControl),
            new PropertyMetadata(150));

    public int btngelHeight {
        get { return (int)GetValue(btngelHeightProperty); }
        set { SetValue(btngelHeightProperty, value); }
    }
    public static readonly DependencyProperty btngelHeightProperty =
        DependencyProperty.Register(nameof(btngelHeight), typeof(int), typeof(btnRoundedGelControl),
            new PropertyMetadata(40));

    public Thickness btngelMargin {
        get { return (Thickness)GetValue(btngelMarginProperty); }
        set { SetValue(btngelMarginProperty, value); }
    }
    public static readonly DependencyProperty btngelMarginProperty =
          DependencyProperty.Register(nameof(btngelMargin), typeof(Thickness), typeof(btnRoundedGelControl),
             new PropertyMetadata(new Thickness(0, 0, 0, 0)));



    public Thickness textPadding {
        get { return (Thickness)GetValue(textPaddingProperty); }
        set { SetValue(textPaddingProperty, value); }
    }
    public static readonly DependencyProperty textPaddingProperty =
        DependencyProperty.Register(nameof(textPadding), typeof(Thickness), typeof(btnRoundedGelControl),
            new PropertyMetadata(new Thickness(0, 0, 0, 0)));


    public string textContent {
        get { return (string)GetValue(textContentProperty); }
        set { SetValue(textContentProperty, value); }
    }
    public static readonly DependencyProperty textContentProperty =
        DependencyProperty.Register(nameof(textContent), typeof(string), typeof(btnRoundedGelControl),
            new PropertyMetadata(string.Empty));

    public int textFontSize {
        get { return (int)GetValue(textFontSizeProperty); }
        set { SetValue(textFontSizeProperty, value); }
    }
    public static readonly DependencyProperty textFontSizeProperty =
          DependencyProperty.Register(nameof(textFontSize), typeof(int), typeof(btnRoundedGelControl),
             new PropertyMetadata(14));



    public Brush btnForeground {
        get { return (Brush)GetValue(btnForegroundProperty); }
        set { SetValue(btnForegroundProperty, value); }
    }
    public static readonly DependencyProperty btnForegroundProperty =
        DependencyProperty.Register(nameof(btnForeground), typeof(Brush), typeof(btnRoundedGelControl), 
            new PropertyMetadata(new BrushConverter().ConvertFrom("#000000")));





    /// <summary>
    /// *********************************************************
    /// </summary>
    #region color section

    public static LinearGradientBrush gelBrush = new LinearGradientBrush(
       (Color)ColorConverter.ConvertFromString("#FFFFFFFF"),
       (Color)ColorConverter.ConvertFromString("#00000000"),
       new Point(0.5, 0),
       new Point(0.5, 1));

    public static Color defaultDarkerBlueColor = 0xFF5C83B4.ToColor();
    public static Color defaultBlueColor = 0xFFB6C7DC.ToColor();
    public static Color defaultOffsetBlueColor = 0xFFE6ECF3.ToColor();

    public static Color defaultGreenColor = 0xFF5DAF65.ToColor();

    public Color mainColor
    {
        get { return (Color)GetValue(mainColorProperty); }
        set { SetValue(mainColorProperty, value); }
    }
    public static readonly DependencyProperty mainColorProperty =
        DependencyProperty.Register(nameof(mainColor), typeof(Color),
            typeof(btnRoundedGelControl), new PropertyMetadata(Colors.Blue));

    public Color offsetColor
    {
        get { return (Color)GetValue(offsetColorProperty); }
        set { SetValue(offsetColorProperty, value); }
    }
    public static readonly DependencyProperty offsetColorProperty =
        DependencyProperty.Register(nameof(offsetColor), typeof(Color),
            typeof(btnRoundedGelControl), new PropertyMetadata(Colors.LightBlue));


    public LinearGradientBrush transparentColor {
        get { return (LinearGradientBrush)GetValue(transparentColorProperty); }
        set { SetValue(transparentColorProperty, value); }
    }
    public static readonly DependencyProperty transparentColorProperty =
        DependencyProperty.Register(nameof(transparentColor), typeof(LinearGradientBrush),
            typeof(btnRoundedGelControl), new PropertyMetadata(gelBrush));



    #endregion


    #region Command and event Area

    public ICommand btngelCommand {
        get { return (ICommand)GetValue(btngelCommandProperty); }
        set { SetValue(btngelCommandProperty, value); }
    }
    public static readonly DependencyProperty btngelCommandProperty =
          DependencyProperty.Register(nameof(btngelCommand), typeof(ICommand), typeof(btnRoundedGelControl),
             new PropertyMetadata(null));

    public object btngelCmdParameter {
        get { return (object)GetValue(btngelCmdParameterProperty); }
        set { SetValue(btngelCmdParameterProperty, value); }
    }
    public static readonly DependencyProperty btngelCmdParameterProperty =
        DependencyProperty.Register(nameof(btngelCmdParameter), typeof(object), typeof(btnRoundedGelControl),
            new PropertyMetadata(null));

    public event RoutedEventHandler btnClick {
        add { AddHandler(btnClickEvent, value); }
        remove { RemoveHandler(btnClickEvent, value); }
    }
    public static readonly RoutedEvent btnClickEvent =
        EventManager.RegisterRoutedEvent(nameof(btnClick), RoutingStrategy.Bubble, typeof(RoutedEventHandler),
            typeof(btnRoundedGelControl));

    private void OnBtnClick(object sender, RoutedEventArgs e) {
        RaiseEvent(new RoutedEventArgs(btnClickEvent));
    }


    #endregion




    public btnRoundedGelControl() {
        InitializeComponent();
    }


}
public static class Extensions {
    public static SolidColorBrush ToBrush(this string HexColorString) {
        return (SolidColorBrush)(new BrushConverter().ConvertFrom(HexColorString));
    }

    public static Color ToColor(this uint argb) {
        return Color.FromArgb((byte)((argb & -16777216) >> 0x18),
                              (byte)((argb & 0xff0000) >> 0x10),
                              (byte)((argb & 0xff00) >> 8),
                              (byte)(argb & 0xff));
    }

}
