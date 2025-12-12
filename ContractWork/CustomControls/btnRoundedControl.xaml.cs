namespace ContractWork.CustomControls;

public partial class btnRoundedControl : UserControl {

    private static CornerRadius _defaultCornerRadius = new CornerRadius(20);

    public SolidColorBrush btnRDBackground {
        get { return (SolidColorBrush)GetValue(btnRDBackgroundProperty); }
        set { SetValue(btnRDBackgroundProperty, value); }
    }
    public static readonly DependencyProperty btnRDBackgroundProperty =
          DependencyProperty.Register(nameof(btnRDBackground), typeof(SolidColorBrush), typeof(btnRoundedControl),
             new PropertyMetadata((SolidColorBrush)new BrushConverter().ConvertFrom("#B6C7DC")));

    //new PropertyMetadata((SolidColorBrush)new BrushConverter().ConvertFrom(Colors.OrangeRed.ToString())));
    //new PropertyMetadata(new BrushConverter().ConvertFrom("#B6C7DC")));

    public Brush btnRDForeground {
        get { return (Brush)GetValue(btnRDForegroundProperty); }
        set { SetValue(btnRDForegroundProperty, value); }
    }
    public static readonly DependencyProperty btnRDForegroundProperty =
        DependencyProperty.Register(nameof(btnRDForeground), typeof(Brush), typeof(btnRoundedControl), 
            new PropertyMetadata(new BrushConverter().ConvertFrom("#000000")));

    public Thickness btnRDMargin {
        get { return (Thickness)GetValue(btnRDMarginProperty); }
        set { SetValue(btnRDMarginProperty, value); }
    }
    public static readonly DependencyProperty btnRDMarginProperty =
          DependencyProperty.Register(nameof(btnRDMargin), typeof(Thickness), typeof(btnRoundedControl),
             new PropertyMetadata(new Thickness(2, 5, 0, 0)));

    public int btnRDWidth {  // string int
        get { return (int)GetValue(btnRDWidthProperty); }  // string int
        set { SetValue(btnRDWidthProperty, value); }
    }
    public static readonly DependencyProperty btnRDWidthProperty =
          DependencyProperty.Register(nameof(btnRDWidth), typeof(int), typeof(btnRoundedControl),  // string int
             new PropertyMetadata(145));  // "Auto" 125

    public int btnRDHeight {
        get { return (int)GetValue(btnRDHeightProperty); }
        set { SetValue(btnRDHeightProperty, value); }
    }
    public static readonly DependencyProperty btnRDHeightProperty =
          DependencyProperty.Register(nameof(btnRDHeight), typeof(int), typeof(btnRoundedControl),
             new PropertyMetadata(40));

    public string btnRDHorizAlign {
        get { return (string)GetValue(btnRDHorizAlignProperty); }
        set { SetValue(btnRDHorizAlignProperty, value); }
    }
    public static readonly DependencyProperty btnRDHorizAlignProperty =
          DependencyProperty.Register(nameof(btnRDHorizAlign), typeof(string), typeof(btnRoundedControl),
             new PropertyMetadata("Left"));

    public string btnRDVertAlign {
        get { return (string)GetValue(btnRDVertAlignProperty); }
        set { SetValue(btnRDVertAlignProperty, value); }
    }
    public static readonly DependencyProperty btnRDVertAlignProperty =
          DependencyProperty.Register(nameof(btnRDVertAlign), typeof(string), typeof(btnRoundedControl),
             new PropertyMetadata("Center"));

    public ICommand btnRDCommand {
        get { return (ICommand)GetValue(btnRDCommandProperty); }
        set { SetValue(btnRDCommandProperty, value); }
    }
    public static readonly DependencyProperty btnRDCommandProperty =
          DependencyProperty.Register(nameof(btnRDCommand), typeof(ICommand), typeof(btnRoundedControl),
             new PropertyMetadata(null));

    public object btnRDCmdParameter {
        get { return (object)GetValue(btnRDCmdParameterProperty); }
        set { SetValue(btnRDCmdParameterProperty, value); }
    }
    public static readonly DependencyProperty btnRDCmdParameterProperty = 
        DependencyProperty.Register(nameof(btnRDCmdParameter), typeof(object), typeof(btnRoundedControl),
            new PropertyMetadata(null));
    

    public int btnRDFontSize {
        get { return (int)GetValue(btnRDFontSizeProperty); }
        set { SetValue(btnRDFontSizeProperty, value); }
    }
    public static readonly DependencyProperty btnRDFontSizeProperty =
          DependencyProperty.Register(nameof(btnRDFontSize), typeof(int), typeof(btnRoundedControl),
             new PropertyMetadata(14));

    public string btnRDContent {
        get { return (string)GetValue(btnRDContentProperty); }
        set { SetValue(btnRDContentProperty, value); }
    }
    public static readonly DependencyProperty btnRDContentProperty =
          DependencyProperty.Register(nameof(btnRDContent), typeof(string), typeof(btnRoundedControl),
             new PropertyMetadata(string.Empty));

    public string btnRDTextWrap {
        get { return (string)GetValue(btnRDTextWrapProperty); }
        set { SetValue(btnRDTextWrapProperty, value); }
    }
    public static readonly DependencyProperty btnRDTextWrapProperty =
          DependencyProperty.Register(nameof(btnRDTextWrap), typeof(string), typeof(btnRoundedControl),
             new PropertyMetadata(string.Empty));

    public string btnRDTextAlign {
        get { return (string)GetValue(btnRDTextAlignProperty); }
        set { SetValue(btnRDTextAlignProperty, value); }
    }
    public static readonly DependencyProperty btnRDTextAlignProperty =
          DependencyProperty.Register(nameof(btnRDTextAlign), typeof(string), typeof(btnRoundedControl),
             new PropertyMetadata(string.Empty));

    public CornerRadius btnRDCornerRadius {
        get { return (CornerRadius)GetValue(btnRDCornerRadiusProperty); }
        set { SetValue(btnRDCornerRadiusProperty, value); }
    }
    public static readonly DependencyProperty btnRDCornerRadiusProperty =
          DependencyProperty.Register(nameof(btnRDCornerRadius), typeof(CornerRadius), typeof(btnRoundedControl),
             new PropertyMetadata(_defaultCornerRadius));

    public Thickness TextPadding {
        get { return (Thickness)GetValue(TextPaddingProperty); }
        set { SetValue(TextPaddingProperty, value); }
    }
    public static readonly DependencyProperty TextPaddingProperty =
        DependencyProperty.Register(nameof(TextPadding), typeof(Thickness), typeof(btnRoundedControl),
            new PropertyMetadata(new Thickness(0, 0, 0, 0)));



    /// <summary>
    /// ////////// Event Handler 
    /// </summary>

    public event RoutedEventHandler btnClick {
        add { AddHandler(btnClickEvent, value); }
        remove { RemoveHandler(btnClickEvent, value); }
    }
    public static readonly RoutedEvent btnClickEvent =
        EventManager.RegisterRoutedEvent(nameof(btnClick), RoutingStrategy.Bubble, typeof(RoutedEventHandler),
            typeof(btnRoundedControl));

    private void OnBtnClick(object sender, RoutedEventArgs e) {
        RaiseEvent(new RoutedEventArgs(btnClickEvent));
    }


    public Visibility btnIconOverlayIsEnabled
    {
        get { return (Visibility)GetValue(btnIconOverlayIsEnabledProperty); }
        set { SetValue(btnIconOverlayIsEnabledProperty, value); }
    }
    public static readonly DependencyProperty btnIconOverlayIsEnabledProperty =
        DependencyProperty.Register(nameof(btnIconOverlayIsEnabled), typeof(Visibility), 
            typeof(btnRoundedControl), new PropertyMetadata(Visibility.Collapsed));





    public btnRoundedControl() {
        InitializeComponent();
    }
}
