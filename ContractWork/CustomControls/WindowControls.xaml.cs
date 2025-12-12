namespace ContractWork.CustomControls;

public partial class WindowControls : UserControl
{

    public Brush brdrBckgrnd {
        get { return (Brush)GetValue(brdrBckgrndProperty); }
        set { SetValue(brdrBckgrndProperty, value); }
    }
    public static readonly DependencyProperty brdrBckgrndProperty =
        DependencyProperty.Register(nameof(brdrBckgrnd), typeof(Brush), typeof(WindowControls),
            new PropertyMetadata(new BrushConverter().ConvertFrom("#000000")));

    public int brdrCornerRadius {
        get { return (int)GetValue(brdrCornerRadiusProperty); }
        set { SetValue(brdrCornerRadiusProperty, value); }
    }
    public static readonly DependencyProperty brdrCornerRadiusProperty =
        DependencyProperty.Register(nameof(brdrCornerRadius), typeof(int), typeof(WindowControls),
            new PropertyMetadata(10));

    /// <summary>
    /// ////////// Button properties
    /// </summary>


    public Brush btnBckgrnd {
        get { return (Brush)GetValue(btnBckgrndProperty); }
        set { SetValue(btnBckgrndProperty, value); }
    }
    public static readonly DependencyProperty btnBckgrndProperty =
        DependencyProperty.Register(nameof(btnBckgrnd), typeof(Brush), typeof(WindowControls),
            new PropertyMetadata(new BrushConverter().ConvertFrom("#000000")));

    public Thickness btnMargin {
        get { return (Thickness)GetValue(btnMarginProperty); }
        set { SetValue(btnMarginProperty, value); }
    }
    public static readonly DependencyProperty btnMarginProperty =
        DependencyProperty.Register(nameof(btnMargin), typeof(Thickness), typeof(WindowControls),
            new PropertyMetadata(new Thickness(0, 0, 0, 0)));

    public ICommand btnCommand {
        get { return (ICommand)GetValue(btnCommandProperty); }
        set { SetValue(btnCommandProperty, value); }
    }
    public static readonly DependencyProperty btnCommandProperty =
        DependencyProperty.Register(nameof(btnCommand), typeof(ICommand), typeof(WindowControls),
            new PropertyMetadata(null));

    public object btnCmdParameter {
        get { return (object)GetValue(btnCmdParameterProperty); }
        set { SetValue(btnCmdParameterProperty, value); }
    }
    public static readonly DependencyProperty btnCmdParameterProperty =
        DependencyProperty.Register(nameof(btnCmdParameter), typeof(object), typeof(WindowControls),
            new PropertyMetadata(null));



    public int btnWidth {
        get { return (int)GetValue(btnWidthProperty); }
        set { SetValue(btnWidthProperty, value); }
    }
    public static readonly DependencyProperty btnWidthProperty =
        DependencyProperty.Register(nameof(btnWidth), typeof(int), typeof(WindowControls),
            new PropertyMetadata(20));




    public int btnHeight {
        get { return (int)GetValue(btnHeightProperty); }
        set { SetValue(btnHeightProperty, value); }
    }
    public static readonly DependencyProperty btnHeightProperty =
        DependencyProperty.Register(nameof(btnHeight), typeof(int), typeof(WindowControls),
            new PropertyMetadata(20));



    public string btnHorizAlign {
        get { return (string)GetValue(btnHorizAlignProperty); }
        set { SetValue(btnHorizAlignProperty, value); }
    }
    public static readonly DependencyProperty btnHorizAlignProperty =
        DependencyProperty.Register(nameof(btnHorizAlign), typeof(string), typeof(WindowControls),
            new PropertyMetadata("Left"));

    public string btnVertAlign {
        get { return (string)GetValue(btnVertAlignProperty); }
        set { SetValue(btnVertAlignProperty, value); }
    }
    public static readonly DependencyProperty btnVertAlignProperty =
        DependencyProperty.Register(nameof(btnVertAlign), typeof(string), typeof(WindowControls),
            new PropertyMetadata("Center"));

    public string btnContentVertAlign {
        get { return (string)GetValue(btnContentVertAlignProperty); }
        set { SetValue(btnContentVertAlignProperty, value); }
    }
    public static readonly DependencyProperty btnContentVertAlignProperty =
        DependencyProperty.Register(nameof(btnContentVertAlign), typeof(string), typeof(WindowControls),
            new PropertyMetadata("Center"));

    public string btnContentHorizAlign {
        get { return (string)GetValue(btnContentHorizAlignProperty); }
        set { SetValue(btnContentHorizAlignProperty, value); }
    }
    public static readonly DependencyProperty btnContentHorizAlignProperty =
        DependencyProperty.Register(nameof(btnContentHorizAlign), typeof(string), typeof(WindowControls),
            new PropertyMetadata("Center"));

    /// <summary>
    /// /////////////  Button Text 
    /// </summary>
    /// 



    public Brush TextForeground {
        get { return (Brush)GetValue(TextForegroundProperty); }
        set { SetValue(TextForegroundProperty, value); }
    }
    public static readonly DependencyProperty TextForegroundProperty =
        DependencyProperty.Register(nameof(TextForeground), typeof(Brush), typeof(WindowControls), 
            new PropertyMetadata(new BrushConverter().ConvertFrom("#000000")));



    public string TextContent {
        get { return (string)GetValue(TextContentProperty); }
        set { SetValue(TextContentProperty, value); }
    }
    public static readonly DependencyProperty TextContentProperty =
        DependencyProperty.Register(nameof(TextContent), typeof(string), typeof(WindowControls),
            new PropertyMetadata(string.Empty));

    public int TextFontSize {
        get { return (int)GetValue(TextFontSizeProperty); }
        set { SetValue(TextFontSizeProperty, value); }
    }
    public static readonly DependencyProperty TextFontSizeProperty =
        DependencyProperty.Register(nameof(TextFontSize), typeof(int), typeof(WindowControls),
            new PropertyMetadata(14));

    public Thickness TextPadding {
        get { return (Thickness)GetValue(TextPaddingProperty); }
        set { SetValue(TextPaddingProperty, value); }
    }
    public static readonly DependencyProperty TextPaddingProperty =
        DependencyProperty.Register(nameof(TextPadding), typeof(Thickness), typeof(WindowControls),
            new PropertyMetadata(new Thickness(0, 0, 0, 0)));

    public string TextVertAlign {
        get { return (string)GetValue(TextVertAlignProperty); }
        set { SetValue(TextVertAlignProperty, value); }
    }
    public static readonly DependencyProperty TextVertAlignProperty =
        DependencyProperty.Register(nameof(TextVertAlign), typeof(string), typeof(WindowControls),
            new PropertyMetadata("Center"));

    /// <summary>
    /// ////////// Event Handler 
    /// </summary>

    public event RoutedEventHandler btnClick {
        add { AddHandler(btnClickEvent, value); }
        remove { RemoveHandler(btnClickEvent, value); }
    }
    public static readonly RoutedEvent btnClickEvent =
        EventManager.RegisterRoutedEvent(nameof(btnClick), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(WindowControls));

    private void OnBtnClick(object sender, RoutedEventArgs e) {
        RaiseEvent(new RoutedEventArgs(btnClickEvent));
    }

    /// <summary>
    /// ////////// Image properties
    /// </summary>

    public string ImagePath {
        get { return (string)GetValue(ImagePathProperty); }
        set { SetValue(ImagePathProperty, value); }
    }
    public static readonly DependencyProperty ImagePathProperty =
        DependencyProperty.Register(nameof(ImagePath), typeof(string), typeof(WindowControls), new PropertyMetadata(null));

    public ImageSource ImageSource {
        get { return (ImageSource)GetValue(ImageSourceProperty); }
        set { SetValue(ImageSourceProperty, value); }
    }
    public static readonly DependencyProperty ImageSourceProperty =
        DependencyProperty.Register(nameof(ImageSource), typeof(ImageSource), typeof(WindowControls), new PropertyMetadata(null));

    public int ImageWidth {
        get { return (int)GetValue(ImageWidthProperty); }
        set { SetValue(ImageWidthProperty, value); }
    }
    public static readonly DependencyProperty ImageWidthProperty =
        DependencyProperty.Register(nameof(ImageWidth), typeof(int), typeof(WindowControls), new PropertyMetadata(30));

    public int ImageHeight {
        get { return (int)GetValue(ImageHeightProperty); }
        set { SetValue(ImageHeightProperty, value); }
    }
    public static readonly DependencyProperty ImageHeightProperty =
        DependencyProperty.Register(nameof(ImageHeight), typeof(int), typeof(WindowControls), new PropertyMetadata(30));





    public WindowControls()
    {
        InitializeComponent();
    }
}
