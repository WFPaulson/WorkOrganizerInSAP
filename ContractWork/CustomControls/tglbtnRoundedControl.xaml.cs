namespace ContractWork.CustomControls;

public partial class tglbtnRoundedControl : UserControl {

    public Brush tglbtnBackground {
        get { return (Brush)GetValue(tglbtnBackgroundProperty); }
        set { SetValue(tglbtnBackgroundProperty, value); }
    }
    public static readonly DependencyProperty tglbtnBackgroundProperty =
        DependencyProperty.Register(nameof(tglbtnBackground), typeof(Brush), typeof(tglbtnRoundedControl), 
            new PropertyMetadata(new BrushConverter().ConvertFrom("#5F9EA0")));



    public Brush tglbtnForeground {
        get { return (Brush)GetValue(tglbtnForegroundProperty); }
        set { SetValue(tglbtnForegroundProperty, value); }
    }
    public static readonly DependencyProperty tglbtnForegroundProperty =
        DependencyProperty.Register(nameof(tglbtnForeground), typeof(Brush), typeof(tglbtnRoundedControl), 
            new PropertyMetadata(new BrushConverter().ConvertFrom("#000000")));



    public int tglbtnCornerRadius {
        get { return (int)GetValue(tglbtnCornerRadiusProperty); }
        set { SetValue(tglbtnCornerRadiusProperty, value); }
    }
    public static readonly DependencyProperty tglbtnCornerRadiusProperty =
        DependencyProperty.Register(nameof(tglbtnCornerRadius), typeof(int), typeof(tglbtnRoundedControl), 
            new PropertyMetadata(10));

    public Brush tglbtnBorderBrush {
        get { return (Brush)GetValue(tglbtnBorderBrushProperty); }
        set { SetValue(tglbtnBorderBrushProperty, value); }
    }
    public static readonly DependencyProperty tglbtnBorderBrushProperty =
        DependencyProperty.Register(nameof(tglbtnBorderBrush), typeof(Brush), typeof(tglbtnRoundedControl),
            new PropertyMetadata(new BrushConverter().ConvertFrom("#000000")));

    public Thickness tglbtnBorderThickness {
        get { return (Thickness)GetValue(tglbtnBorderThicknessProperty); }
        set { SetValue(tglbtnBorderThicknessProperty, value); }
    }
    public static readonly DependencyProperty tglbtnBorderThicknessProperty =
        DependencyProperty.Register(nameof(tglbtnBorderThickness), typeof(Thickness), typeof(tglbtnRoundedControl), 
            new PropertyMetadata(new Thickness(1, 1, 1, 1)));

    public ICommand tglbtnCommand {
        get { return (ICommand)GetValue(tglbtnCommandProperty); }
        set { SetValue(tglbtnCommandProperty, value); }
    }
    public static readonly DependencyProperty tglbtnCommandProperty =
        DependencyProperty.Register(nameof(tglbtnCommand), typeof(ICommand), typeof(tglbtnRoundedControl),
            new PropertyMetadata(null));

    public int tglbtnWidth {
        get { return (int)GetValue(tglbtnWidthProperty); }
        set { SetValue(tglbtnWidthProperty, value); }
    }
    public static readonly DependencyProperty tglbtnWidthProperty =
        DependencyProperty.Register(nameof(tglbtnWidth), typeof(int), typeof(tglbtnRoundedControl),
            new PropertyMetadata(20));

    public int tglbtnHeight {
        get { return (int)GetValue(tglbtnHeightProperty); }
        set { SetValue(tglbtnHeightProperty, value); }
    }
    public static readonly DependencyProperty tglbtnHeightProperty =
        DependencyProperty.Register(nameof(tglbtnHeight), typeof(int), typeof(tglbtnRoundedControl),
            new PropertyMetadata(20));

    public Thickness tglbtnMargin {
        get { return (Thickness)GetValue(tglbtnMarginProperty); }
        set { SetValue(tglbtnMarginProperty, value); }
    }
    public static readonly DependencyProperty tglbtnMarginProperty =
        DependencyProperty.Register(nameof(tglbtnMargin), typeof(Thickness), typeof(tglbtnRoundedControl),
            new PropertyMetadata(new Thickness(0, 0, 0, 0)));



    public string TextContent {
        get { return (string)GetValue(TextContentProperty); }
        set { SetValue(TextContentProperty, value); }
    }
    public static readonly DependencyProperty TextContentProperty =
        DependencyProperty.Register(nameof(TextContent), typeof(string), typeof(tglbtnRoundedControl),
            new PropertyMetadata(string.Empty));

    //new FrameworkPropertyMetadata
    //{
    //    BindsTwoWayByDefault = true,
    //});
    //new PropertyMetadata(string.Empty));
    //new FrameworkPropertyMetadata(string.Empty,
    //FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
    //
    //public DependencyProperty SomeProperty =
    //DependencyProperty.Register("Some", typeof(bool), typeof(Window1),
    //    new FrameworkPropertyMetadata(default(bool),
    //        FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


    public int TextFontSize {
        get { return (int)GetValue(TextFontSizeProperty); }
        set { SetValue(TextFontSizeProperty, value); }
    }
    public static readonly DependencyProperty TextFontSizeProperty =
        DependencyProperty.Register(nameof(TextFontSize), typeof(int), typeof(tglbtnRoundedControl),
            new PropertyMetadata(14));

    public Thickness TextPadding {
        get { return (Thickness)GetValue(TextPaddingProperty); }
        set { SetValue(TextPaddingProperty, value); }
    }
    public static readonly DependencyProperty TextPaddingProperty =
        DependencyProperty.Register(nameof(TextPadding), typeof(Thickness), typeof(tglbtnRoundedControl),
            new PropertyMetadata(new Thickness(0, 0, 0, 0)));

    public string TextVertAlign {
        get { return (string)GetValue(TextVertAlignProperty); }
        set { SetValue(TextVertAlignProperty, value); }
    }
    public static readonly DependencyProperty TextVertAlignProperty =
        DependencyProperty.Register(nameof(TextVertAlign), typeof(string), typeof(tglbtnRoundedControl),
            new PropertyMetadata("Center"));




    public string ImagePath {
        get { return (string)GetValue(ImagePathProperty); }
        set { SetValue(ImagePathProperty, value); }
    }
    public static readonly DependencyProperty ImagePathProperty =
        DependencyProperty.Register(nameof(ImagePath), typeof(string), typeof(tglbtnRoundedControl), new PropertyMetadata(null));

    public ImageSource ImageSource {
        get { return (ImageSource)GetValue(ImageSourceProperty); }
        set { SetValue(ImageSourceProperty, value); }
    }
    public static readonly DependencyProperty ImageSourceProperty =
        DependencyProperty.Register(nameof(ImageSource), typeof(ImageSource), typeof(tglbtnRoundedControl), new PropertyMetadata(null));

    public int ImageWidth {
        get { return (int)GetValue(ImageWidthProperty); }
        set { SetValue(ImageWidthProperty, value); }
    }
    public static readonly DependencyProperty ImageWidthProperty =
        DependencyProperty.Register(nameof(ImageWidth), typeof(int), typeof(tglbtnRoundedControl), new PropertyMetadata(30));

    public int ImageHeight {
        get { return (int)GetValue(ImageHeightProperty); }
        set { SetValue(ImageHeightProperty, value); }
    }
    public static readonly DependencyProperty ImageHeightProperty =
        DependencyProperty.Register(nameof(ImageHeight), typeof(int), typeof(tglbtnRoundedControl), new PropertyMetadata(30));





    public bool tglbtnIsChecked {
        get { return (bool)GetValue(tglbtnIsCheckedProperty); }
        set { SetValue(tglbtnIsCheckedProperty, value); }
    }
    public static readonly DependencyProperty tglbtnIsCheckedProperty =
        DependencyProperty.Register(nameof(tglbtnIsChecked), typeof(bool), typeof(tglbtnRoundedControl), 
            new PropertyMetadata(false));






    public event RoutedEventHandler btnClick {
        add { AddHandler(btnClickEvent, value); }
        remove { RemoveHandler(btnClickEvent, value); }
    }
    public static readonly RoutedEvent btnClickEvent =
        EventManager.RegisterRoutedEvent(nameof(btnClick), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(tglbtnRoundedControl));
    private void OnBtnClick(object sender, RoutedEventArgs e) {
        RaiseEvent(new RoutedEventArgs(btnClickEvent));
    }


    public event RoutedEventHandler ClickChecked {
        add { AddHandler(ClickCheckedEvent, value); }
        remove { RemoveHandler(ClickCheckedEvent, value); }
    }
    public static readonly RoutedEvent ClickCheckedEvent =
        EventManager.RegisterRoutedEvent(nameof(ClickChecked), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(tglbtnRoundedControl));
    private void ToggleButton_Checked(object sender, RoutedEventArgs e) {
        RaiseEvent(new RoutedEventArgs(ClickCheckedEvent));
    }


    public event RoutedEventHandler ClickUnchecked {
        add { AddHandler(ClickUncheckedEvent, value); }
        remove { RemoveHandler(ClickUncheckedEvent, value); }
    }
    public static readonly RoutedEvent ClickUncheckedEvent =
        EventManager.RegisterRoutedEvent(nameof(ClickUnchecked), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(tglbtnRoundedControl));
    private void ToggleButton_Unchecked(object sender, RoutedEventArgs e) {
        RaiseEvent(new RoutedEventArgs(ClickUncheckedEvent));
    }



    public tglbtnRoundedControl() {
        InitializeComponent();
            
    }

    //public event RoutedEventHandler ClickB1;
    //public event RoutedEventHandler ClickB2;
    //private void button1_Click(object sender, RoutedEventArgs e) {

    //}
    //private void button2_Click(object sender, RoutedEventArgs e) {

    //}

    //public event RoutedEventHandler ClickChecked;
    //public event RoutedEventHandler ClickUnchecked;

    //public event RoutedEventHandler ClickChecked {
    //    add { AddHandler(ClickCheckedEvent, value); }
    //    remove { RemoveHandler(ClickCheckedEvent, value); }
    //}
    //public static readonly RoutedEvent ClickCheckedEvent =
    //    EventManager.RegisterRoutedEvent(nameof(ClickChecked), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(tglbtnRoundedControl));

    //private void ToggleButton_Checked(object sender, RoutedEventArgs e) {
    //    RaiseEvent(new RoutedEventArgs(ClickCheckedEvent));

    //    //if (ClickChecked != null) {
    //    //ClickChecked(this, e);
    //    //}
    //}

    //public event RoutedEventHandler ClickUnchecked {
    //    add { AddHandler(ClickUncheckedEvent, value); }
    //    remove { RemoveHandler(ClickUncheckedEvent, value); }
    //}
    //public static readonly RoutedEvent ClickUncheckedEvent =
    //    EventManager.RegisterRoutedEvent(nameof(ClickUnchecked), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(tglbtnRoundedControl));

    //private void ToggleButton_Unchecked(object sender, RoutedEventArgs e) {
    //    RaiseEvent(new RoutedEventArgs(ClickUncheckedEvent));
    //    //if (ClickUnchecked != null) {
    //    // ClickUnchecked(this, e);
    //    //}
    //}
}

