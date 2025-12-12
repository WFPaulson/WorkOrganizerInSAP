namespace ContractWork.CustomControls; 

public partial class cmbRoundedControl : UserControl {

    public SolidColorBrush cmbForeground {
        get { return (SolidColorBrush)GetValue(cmbForegroundProperty); }
        set { SetValue(cmbForegroundProperty, value); }
    }
    public static readonly DependencyProperty cmbForegroundProperty =
        DependencyProperty.Register(nameof(cmbForeground), typeof(SolidColorBrush), typeof(cmbRoundedControl),
            new PropertyMetadata((SolidColorBrush)new BrushConverter().ConvertFrom(Colors.Black.ToString())));

    public SolidColorBrush cmbBorder {
        get { return (SolidColorBrush)GetValue(cmbBorderProperty); }
        set { SetValue(cmbBorderProperty, value); }
    }
    public static readonly DependencyProperty cmbBorderProperty =
        DependencyProperty.Register(nameof(cmbBorder), typeof(SolidColorBrush), typeof(cmbRoundedControl),
            new PropertyMetadata((SolidColorBrush)new BrushConverter().ConvertFrom(Colors.LightGray.ToString())));

    public SolidColorBrush cmbtxtBorder {
        get { return (SolidColorBrush)GetValue(cmbtxtBorderProperty); }
        set { SetValue(cmbtxtBorderProperty, value); }
    }
    public static readonly DependencyProperty cmbtxtBorderProperty =
        DependencyProperty.Register(nameof(cmbtxtBorder), typeof(SolidColorBrush), typeof(cmbRoundedControl),
            new PropertyMetadata((SolidColorBrush)new BrushConverter().ConvertFrom(Colors.WhiteSmoke.ToString())));

    public SolidColorBrush cmbTextBackground {
        get { return (SolidColorBrush)GetValue(cmbTextBackgroundProperty); }
        set { SetValue(cmbTextBackgroundProperty, value); }
    }
    public static readonly DependencyProperty cmbTextBackgroundProperty =
        DependencyProperty.Register(nameof(cmbTextBackground), typeof(SolidColorBrush), typeof(cmbRoundedControl),
            new PropertyMetadata((SolidColorBrush)new BrushConverter().ConvertFrom(Colors.OrangeRed.ToString())));

    // you can also use hex "#ffaacc", just put hex straight between parenthesis

    public SolidColorBrush cmbFillerBackground {
        get { return (SolidColorBrush)GetValue(cmbFillerBackgroundProperty); }
        set { SetValue(cmbFillerBackgroundProperty, value); }
    }
    public static readonly DependencyProperty cmbFillerBackgroundProperty =
        DependencyProperty.Register(nameof(cmbFillerBackground), typeof(SolidColorBrush), typeof(cmbRoundedControl),
            new PropertyMetadata((SolidColorBrush)new BrushConverter().ConvertFrom(Colors.Purple.ToString())));



    public SolidColorBrush cmbItemBackground {
        get { return (SolidColorBrush)GetValue(cmbItemBackgroundProperty); }
        set { SetValue(cmbItemBackgroundProperty, value); }
    }
    public static readonly DependencyProperty cmbItemBackgroundProperty =
        DependencyProperty.Register(nameof(cmbItemBackground), typeof(SolidColorBrush), typeof(cmbRoundedControl),
            new PropertyMetadata((SolidColorBrush)new BrushConverter().ConvertFrom(Colors.LightGray.ToString())));



    public SolidColorBrush cmbArrow {
        get { return (SolidColorBrush)GetValue(cmbArrowProperty); }
        set { SetValue(cmbArrowProperty, value); }
    }
    public static readonly DependencyProperty cmbArrowProperty =
        DependencyProperty.Register(nameof(cmbArrow), typeof(SolidColorBrush), typeof(cmbRoundedControl),
            new PropertyMetadata((SolidColorBrush)new BrushConverter().ConvertFrom(Colors.DarkGray.ToString())));

    //(SolidColorBrush)new BrushConverter().ConvertFrom(Colors.DarkGray.ToString()))
    //new SolidColorBrush(Colors.Black)))

    public ObservableCollection<string> cmbList {
        get { return (ObservableCollection<string>)GetValue(cmbListProperty); }
        set { SetValue(cmbListProperty, value); }
    }
    public static readonly DependencyProperty cmbListProperty =
        DependencyProperty.Register(nameof(cmbList), typeof(ObservableCollection<string>), typeof(cmbRoundedControl),
            new PropertyMetadata(null));

    public string cmbSelectedItem {
        get { return (string)GetValue(cmbSelectedItemProperty); }
        set { SetValue(cmbSelectedItemProperty, value); }
    }
    public static readonly DependencyProperty cmbSelectedItemProperty =
        DependencyProperty.Register(nameof(cmbSelectedItem), typeof(string), typeof(cmbRoundedControl),
            new PropertyMetadata(null));



    public int cmbHeight {
        get { return (int)GetValue(cmbHeightProperty); }
        set { SetValue(cmbHeightProperty, value); }
    }
    public static readonly DependencyProperty cmbHeightProperty =
        DependencyProperty.Register(nameof(cmbHeight), typeof(int), typeof(cmbRoundedControl),
            new PropertyMetadata(25));

    public int cmbWidth {
        get { return (int)GetValue(cmbWidthProperty); }
        set { SetValue(cmbWidthProperty, value); }
    }
    public static readonly DependencyProperty cmbWidthProperty =
        DependencyProperty.Register(nameof(cmbWidth), typeof(int), typeof(cmbRoundedControl),
            new PropertyMetadata(120));

    public string cmbHorizAlign {
        get { return (string)GetValue(cmbHorizAlignProperty); }
        set { SetValue(cmbHorizAlignProperty, value); }
    }
    public static readonly DependencyProperty cmbHorizAlignProperty =
        DependencyProperty.Register(nameof(cmbHorizAlign), typeof(string), typeof(cmbRoundedControl),
            new PropertyMetadata("Left"));



    public Thickness cmbMargin {
        get { return (Thickness)GetValue(cmbMarginProperty); }
        set { SetValue(cmbMarginProperty, value); }
    }
    public static readonly DependencyProperty cmbMarginProperty =
        DependencyProperty.Register(nameof(cmbMargin), typeof(Thickness), typeof(cmbRoundedControl),
            new PropertyMetadata(new Thickness(20, 0, 0, 0)));


    public Thickness cmbtxtPadding {
        get { return (Thickness)GetValue(cmbtxtPaddingProperty); }
        set { SetValue(cmbtxtPaddingProperty, value); }
    }
    public static readonly DependencyProperty cmbtxtPaddingProperty =
        DependencyProperty.Register(nameof(cmbtxtPadding), typeof(Thickness), typeof(cmbRoundedControl),
            new PropertyMetadata(new Thickness(10, 3, 23, 3)));

    public string PlaceHolderText {
        get { return (string)GetValue(PlaceHolderTextProperty); }
        set { SetValue(PlaceHolderTextProperty, value); }
    }
    public static readonly DependencyProperty PlaceHolderTextProperty =
        DependencyProperty.Register(nameof(PlaceHolderText), typeof(string), typeof(cmbRoundedControl),
            new PropertyMetadata("Search..."));


    public cmbRoundedControl() {
        InitializeComponent();
    }
}
