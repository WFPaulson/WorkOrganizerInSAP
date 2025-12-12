namespace ContractWork.CustomControls;

public partial class TextBoxControls : UserControl
{
    private static CornerRadius _defaultCornerRadius = new CornerRadius(20);

    public CornerRadius txtbxCornerRadius {
        get { return (CornerRadius)GetValue(txtbxCornerRadiusProperty); }
        set { SetValue(txtbxCornerRadiusProperty, value); }
    }
    public static readonly DependencyProperty txtbxCornerRadiusProperty =
          DependencyProperty.Register(nameof(txtbxCornerRadius), typeof(CornerRadius), typeof(TextBoxControls),
             new PropertyMetadata(new CornerRadius(5)));

    public Thickness txtbxMargin {
        get { return (Thickness)GetValue(txtbxMarginProperty); }
        set { SetValue(txtbxMarginProperty, value); }
    }
    public static readonly DependencyProperty txtbxMarginProperty =
          DependencyProperty.Register(nameof(txtbxMargin), typeof(Thickness), typeof(TextBoxControls),
              new PropertyMetadata(new Thickness(0, 0, 0, 0)));

    public Brush txtbxBackground {
        get { return (Brush)GetValue(txtbxBackgroundProperty); }
        set { SetValue(txtbxBackgroundProperty, value); }
    }
    public static readonly DependencyProperty txtbxBackgroundProperty =
          DependencyProperty.Register(nameof(txtbxBackground), typeof(Brush), typeof(TextBoxControls),
             new PropertyMetadata(Brushes.White));
    //DAA520    B6C7DC  new BrushConverter().ConvertFrom("#FFF"))

    public Brush txtbxForeground {
        get { return (Brush)GetValue(txtbxForegroundProperty); }
        set { SetValue(txtbxForegroundProperty, value); }
    }
    public static readonly DependencyProperty txtbxForegroundProperty =
          DependencyProperty.Register(nameof(txtbxForeground), typeof(Brush), typeof(TextBoxControls),
             new PropertyMetadata(Brushes.Black));

    public Brush txtbxBorderBrush {
        get { return (Brush)GetValue(txtbxBorderBrushProperty); }
        set { SetValue(txtbxBorderBrushProperty, value); }
    }
    public static readonly DependencyProperty txtbxBorderBrushProperty =
          DependencyProperty.Register(nameof(txtbxBorderBrush), typeof(Brush), typeof(TextBoxControls),
             new PropertyMetadata(Brushes.Black));

    public Thickness txtbxBorderThickness {
        get { return (Thickness)GetValue(txtbxBorderThicknessProperty); }
        set { SetValue(txtbxBorderThicknessProperty, value); }
    }
    public static readonly DependencyProperty txtbxBorderThicknessProperty =
        DependencyProperty.Register(nameof(txtbxBorderThickness), typeof(Thickness), typeof(TextBoxControls),
            new PropertyMetadata(new Thickness(1)));


    public int txtbxWidth {
        get { return (int)GetValue(txtbxWidthProperty); }
        set { SetValue(txtbxWidthProperty, value); }
    }
    public static readonly DependencyProperty txtbxWidthProperty =
          DependencyProperty.Register(nameof(txtbxWidth), typeof(int), typeof(TextBoxControls),
             new PropertyMetadata(140));

    public int txtbxHeight {
        get { return (int)GetValue(txtbxHeightProperty); }
        set { SetValue(txtbxHeightProperty, value); }
    }
    public static readonly DependencyProperty txtbxHeightProperty =
          DependencyProperty.Register(nameof(txtbxHeight), typeof(int), typeof(TextBoxControls),
             new PropertyMetadata(40));

    public int txtbxFontSize {
        get { return (int)GetValue(txtbxFontSizeProperty); }
        set { SetValue(txtbxFontSizeProperty, value); }
    }
    public static readonly DependencyProperty txtbxFontSizeProperty =
          DependencyProperty.Register(nameof(txtbxFontSize), typeof(int), typeof(TextBoxControls),
             new PropertyMetadata(14));

    public string txtbxFontWeight {
        get { return (string)GetValue(txtbxFontWeightProperty); }
        set { SetValue(txtbxFontWeightProperty, value); }
    }
    public static readonly DependencyProperty txtbxFontWeightProperty =
          DependencyProperty.Register(nameof(txtbxFontWeight), typeof(string), typeof(TextBoxControls),
             new PropertyMetadata("Normal"));

    public Thickness txtbxPadding {
        get { return (Thickness)GetValue(txtbxPaddingProperty); }
        set { SetValue(txtbxPaddingProperty, value); }
    }
    public static readonly DependencyProperty txtbxPaddingProperty =
          DependencyProperty.Register(nameof(txtbxPadding), typeof(Thickness), typeof(TextBoxControls),
             new PropertyMetadata(new Thickness(0)));

    public string txtbxHorizContentAlign {
        get { return (string)GetValue(txtbxHorizContentAlignProperty); }
        set { SetValue(txtbxHorizContentAlignProperty, value); }
    }
    public static readonly DependencyProperty txtbxHorizContentAlignProperty =
          DependencyProperty.Register(nameof(txtbxHorizContentAlign), typeof(string), typeof(TextBoxControls),
             new PropertyMetadata("Left"));

    public string txtbxVertContentAlign {
        get { return (string)GetValue(txtbxVertContentAlignProperty); }
        set { SetValue(txtbxVertContentAlignProperty, value); }
    }
    public static readonly DependencyProperty txtbxVertContentAlignProperty =
          DependencyProperty.Register(nameof(txtbxVertContentAlign), typeof(string), typeof(TextBoxControls),
             new PropertyMetadata("Center"));

    public string txtbxHorizAlign {
        get { return (string)GetValue(txtbxHorizAlignProperty); }
        set { SetValue(txtbxHorizAlignProperty, value); }
    }
    public static readonly DependencyProperty txtbxHorizAlignProperty =
        DependencyProperty.Register(nameof(txtbxHorizAlign), typeof(string), typeof(TextBoxControls),
            new PropertyMetadata("Center"));



    public string txtbxVerticAlign {
        get { return (string)GetValue(txtbxVerticAlignProperty); }
        set { SetValue(txtbxVerticAlignProperty, value); }
    }
    public static readonly DependencyProperty txtbxVerticAlignProperty =
        DependencyProperty.Register(nameof(txtbxVerticAlign), typeof(string), typeof(TextBoxControls),
            new PropertyMetadata("Center"));



    public string txtbxText {
        get { return (string)GetValue(txtbxTextProperty); }
        set { SetValue(txtbxTextProperty, value); }
    }
    public static readonly DependencyProperty txtbxTextProperty =
          DependencyProperty.Register(nameof(txtbxText), typeof(string), typeof(TextBoxControls),
             new PropertyMetadata(string.Empty));

    public ICommand txtbxCommand {
        get { return (ICommand)GetValue(txtbxCommandProperty); }
        set { SetValue(txtbxCommandProperty, value); }
    }
    public static readonly DependencyProperty txtbxCommandProperty =
          DependencyProperty.Register(nameof(txtbxCommand), typeof(ICommand), typeof(TextBoxControls),
             new PropertyMetadata(null));
    public TextBoxControls()
    {
        InitializeComponent();
    }
}
