namespace ContractWork.CustomControls; 

public partial class btnMaintenanceControl : UserControl {


    /*          **************************************************************          */
    //Main Brdr properties

    public Brush MaintBackground {
        get { return (Brush)GetValue(MaintBackgroundProperty); }
        set { SetValue(MaintBackgroundProperty, value); }
    }
    public static readonly DependencyProperty MaintBackgroundProperty =
        DependencyProperty.Register(nameof(MaintBackground), typeof(Brush), typeof(btnMaintenanceControl),
            new PropertyMetadata(new BrushConverter().ConvertFrom("#B6C7DC")));

    

    public CornerRadius MaintCornerRadius {
        get { return (CornerRadius)GetValue(MaintCornerRadiusProperty); }
        set { SetValue(MaintCornerRadiusProperty, value); }
    }
    public static readonly DependencyProperty MaintCornerRadiusProperty =
          DependencyProperty.Register(nameof(MaintCornerRadius), typeof(CornerRadius), typeof(btnMaintenanceControl),
             new PropertyMetadata(new CornerRadius(20, 20, 20, 20)));


    /*          **************************************************************          */
    //btn Brdr properties

    public Brush btnBrdrBackground {
        get { return (Brush)GetValue(btnBrdrBackgroundProperty); }
        set { SetValue(btnBrdrBackgroundProperty, value); }
    }
    public static readonly DependencyProperty btnBrdrBackgroundProperty =
        DependencyProperty.Register(nameof(btnBrdrBackground), typeof(Brush), typeof(btnMaintenanceControl), 
            new PropertyMetadata(new BrushConverter().ConvertFrom("#B6C7DC")));

    public Thickness btnBrdrMargin {
        get { return (Thickness)GetValue(btnBrdrMarginProperty); }
        set { SetValue(btnBrdrMarginProperty, value); }
    }
    public static readonly DependencyProperty btnBrdrMarginProperty =
          DependencyProperty.Register(nameof(btnBrdrMargin), typeof(Thickness), typeof(btnMaintenanceControl),
              new PropertyMetadata(new Thickness(0, 0, 0, 0)));

    public string btnBrdrHeight {
        get { return (string)GetValue(btnBrdrHeightProperty); }
        set { SetValue(btnBrdrHeightProperty, value); }
    }
    public static readonly DependencyProperty btnBrdrHeightProperty =
        DependencyProperty.Register(nameof(btnBrdrHeight), typeof(string), typeof(btnMaintenanceControl),
            new PropertyMetadata("auto"));

    public string btnBrdrWidth {
        get { return (string)GetValue(btnBrdrWidthProperty); }
        set { SetValue(btnBrdrWidthProperty, value); }
    }
    public static readonly DependencyProperty btnBrdrWidthProperty =
        DependencyProperty.Register(nameof(btnBrdrWidth), typeof(string), typeof(btnMaintenanceControl),
            new PropertyMetadata("auto"));

    public Brush btnBrdrBrush {
        get { return (Brush)GetValue(btnBrdrBrushProperty); }
        set { SetValue(btnBrdrBrushProperty, value); }
    }
    public static readonly DependencyProperty btnBrdrBrushProperty =
        DependencyProperty.Register(nameof(btnBrdrBrush), typeof(Brush), typeof(btnMaintenanceControl),
            new PropertyMetadata(new BrushConverter().ConvertFrom("#FFF")));

    public Thickness btnBrdrThickness {
        get { return (Thickness)GetValue(btnBrdrThicknessProperty); }
        set { SetValue(btnBrdrThicknessProperty, value); }
    }
    public static readonly DependencyProperty btnBrdrThicknessProperty =
          DependencyProperty.Register(nameof(btnBrdrThickness), typeof(Thickness), typeof(btnMaintenanceControl),
              new PropertyMetadata(new Thickness(1, 1, 0, 0)));

    public Brush btn2ndBrdrBrush {
        get { return (Brush)GetValue(btn2ndBrdrBrushProperty); }
        set { SetValue(btn2ndBrdrBrushProperty, value); }
    }
    public static readonly DependencyProperty btn2ndBrdrBrushProperty =
        DependencyProperty.Register(nameof(btn2ndBrdrBrush), typeof(Brush), typeof(btnMaintenanceControl), 
            new PropertyMetadata(new BrushConverter().ConvertFrom("#000")));



    public Thickness btn2ndBrdrThickness {
        get { return (Thickness)GetValue(btn2ndBrdrThicknessProperty); }
        set { SetValue(btn2ndBrdrThicknessProperty, value); }
    }
    public static readonly DependencyProperty btn2ndBrdrThicknessProperty =
        DependencyProperty.Register(nameof(btn2ndBrdrThickness), typeof(Thickness), typeof(btnMaintenanceControl), 
            new PropertyMetadata(new Thickness(0, 0, 1, 1)));






    public CornerRadius btnBrdrCornerRadius {
        get { return (CornerRadius)GetValue(btnBrdrCornerRadiusProperty); }
        set { SetValue(btnBrdrCornerRadiusProperty, value); }
    }
    public static readonly DependencyProperty btnBrdrCornerRadiusProperty =
          DependencyProperty.Register(nameof(btnBrdrCornerRadius), typeof(CornerRadius), typeof(btnMaintenanceControl),
             new PropertyMetadata(new CornerRadius(20, 20, 20, 20)));

    /*          **************************************************************          */
    //btn properties

    public string btnHeight {
        get { return (string)GetValue(btnHeightProperty); }
        set { SetValue(btnHeightProperty, value); }
    }
    public static readonly DependencyProperty btnHeightProperty =
        DependencyProperty.Register(nameof(btnHeight), typeof(string), typeof(btnMaintenanceControl),
            new PropertyMetadata("Auto"));

    public string btnWidth {
        get { return (string)GetValue(btnWidthProperty); }
        set { SetValue(btnWidthProperty, value); }
    }
    public static readonly DependencyProperty btnWidthProperty =
        DependencyProperty.Register(nameof(btnWidth), typeof(string), typeof(btnMaintenanceControl),
            new PropertyMetadata("Auto"));

    public ICommand btnClickCommand {
        get { return (ICommand)GetValue(btnClickCommandProperty); }
        set { SetValue(btnClickCommandProperty, value); }
    }
    public static readonly DependencyProperty btnClickCommandProperty =
          DependencyProperty.Register(nameof(btnClickCommand), typeof(ICommand), typeof(btnMaintenanceControl),
             new PropertyMetadata(null));

    public object btnCmdParameter {
        get { return (object)GetValue(btnCmdParameterProperty); }
        set { SetValue(btnCmdParameterProperty, value); }
    }
    public static readonly DependencyProperty btnCmdParameterProperty =
        DependencyProperty.Register(nameof(btnCmdParameter), typeof(object), typeof(btnMaintenanceControl),
            new PropertyMetadata(null));


    /*          **************************************************************          */
    //btn content properties

    public Brush btnForeground {
        get { return (Brush)GetValue(btnForegroundProperty); }
        set { SetValue(btnForegroundProperty, value); }
    }
    public static readonly DependencyProperty btnForegroundProperty =
        DependencyProperty.Register(nameof(btnForeground), typeof(Brush), typeof(btnMaintenanceControl),
            new PropertyMetadata(new BrushConverter().ConvertFrom("#000")));

    public string btnContentFontSize {
        get { return (string)GetValue(btnContentFontSizeProperty); }
        set { SetValue(btnContentFontSizeProperty, value); }
    }
    public static readonly DependencyProperty btnContentFontSizeProperty =
        DependencyProperty.Register(nameof(btnContentFontSize), typeof(string), typeof(btnMaintenanceControl),
            new PropertyMetadata("14"));

    public string btnContentText {
        get { return (string)GetValue(btnContentTextProperty); }
        set { SetValue(btnContentTextProperty, value); }
    }
    public static readonly DependencyProperty btnContentTextProperty =
        DependencyProperty.Register(nameof(btnContentText), typeof(string), typeof(btnMaintenanceControl),
            new PropertyMetadata(string.Empty));

    public string TextVertAlign {
        get { return (string)GetValue(TextVertAlignProperty); }
        set { SetValue(TextVertAlignProperty, value); }
    }
    public static readonly DependencyProperty TextVertAlignProperty =
        DependencyProperty.Register(nameof(TextVertAlign), typeof(string), typeof(btnMaintenanceControl),
            new PropertyMetadata("Center"));

    public HorizontalAlignment btnHorizAlign {
        get { return (HorizontalAlignment)GetValue(btnHorizAlignProperty); }
        set { SetValue(btnHorizAlignProperty, value); }
    }
    public static readonly DependencyProperty btnHorizAlignProperty =
        DependencyProperty.Register(nameof(btnHorizAlign), typeof(HorizontalAlignment), typeof(btnMaintenanceControl), 
            new PropertyMetadata(HorizontalAlignment.Center));

    public VerticalAlignment btnVertAlign {
        get { return (VerticalAlignment)GetValue(btnVertAlignProperty); }
        set { SetValue(btnVertAlignProperty, value); }
    }
    public static readonly DependencyProperty btnVertAlignProperty =
        DependencyProperty.Register(nameof(btnVertAlign), typeof(VerticalAlignment), typeof(btnMaintenanceControl), 
            new PropertyMetadata(VerticalAlignment.Center));



    public Thickness textPadding {
        get { return (Thickness)GetValue(textPaddingProperty); }
        set { SetValue(textPaddingProperty, value); }
    }
    public static readonly DependencyProperty textPaddingProperty =
        DependencyProperty.Register(nameof(textPadding), typeof(Thickness), typeof(btnMaintenanceControl), 
            new PropertyMetadata(new Thickness(0, 0, 0, 0)));



    /// <summary>
    /// Image dependency properties
    /// </summary>
    public string ImagePath {
        get { return (string)GetValue(ImagePathProperty); }
        set { SetValue(ImagePathProperty, value); }
    }
    public static readonly DependencyProperty ImagePathProperty =
        DependencyProperty.Register(nameof(ImagePath), typeof(string), typeof(btnMaintenanceControl), new PropertyMetadata(null));

    public ImageSource ImageSource {
        get { return (ImageSource)GetValue(ImageSourceProperty); }
        set { SetValue(ImageSourceProperty, value); }
    }
    public static readonly DependencyProperty ImageSourceProperty =
        DependencyProperty.Register(nameof(ImageSource), typeof(ImageSource), typeof(btnMaintenanceControl), new PropertyMetadata(null));

    public int ImageWidth {
        get { return (int)GetValue(ImageWidthProperty); }
        set { SetValue(ImageWidthProperty, value); }
    }
    public static readonly DependencyProperty ImageWidthProperty =
        DependencyProperty.Register(nameof(ImageWidth), typeof(int), typeof(btnMaintenanceControl), new PropertyMetadata(30));

    public int ImageHeight {
        get { return (int)GetValue(ImageHeightProperty); }
        set { SetValue(ImageHeightProperty, value); }
    }
    public static readonly DependencyProperty ImageHeightProperty =
        DependencyProperty.Register(nameof(ImageHeight), typeof(int), typeof(btnMaintenanceControl), new PropertyMetadata(30));




    public Thickness ImageMargin {
        get { return (Thickness)GetValue(ImageMarginProperty); }
        set { SetValue(ImageMarginProperty, value); }
    }
    public static readonly DependencyProperty ImageMarginProperty =
        DependencyProperty.Register(nameof(ImageMargin), typeof(Thickness), typeof(btnMaintenanceControl), 
            new PropertyMetadata(new Thickness(0,0,0,0)));





    public event RoutedEventHandler btnClick {
        add { AddHandler(btnClickEvent, value); }
        remove { RemoveHandler(btnClickEvent, value); }
    }
    public static readonly RoutedEvent btnClickEvent =
        EventManager.RegisterRoutedEvent(nameof(btnClick), RoutingStrategy.Bubble, typeof(RoutedEventHandler),
            typeof(btnMaintenanceControl));

    private void OnBtnClick(object sender, RoutedEventArgs e) {
        RaiseEvent(new RoutedEventArgs(btnClickEvent));
    }


    public btnMaintenanceControl() {
        InitializeComponent();
    }
}
