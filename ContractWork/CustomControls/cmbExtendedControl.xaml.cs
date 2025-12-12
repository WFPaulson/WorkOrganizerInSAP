using System.Collections;

namespace ContractWork.CustomControls; 

public partial class cmbExtendedControl : UserControl {

    /// <summary>
    /// Combobox settings
    /// </summary>
    public int cmbWidth {
        get { return (int)GetValue(cmbWidthProperty); }
        set { SetValue(cmbWidthProperty, value); }
    }
    public static readonly DependencyProperty cmbWidthProperty =
        DependencyProperty.Register(nameof(cmbWidth), typeof(int), typeof(cmbExtendedControl), 
            new PropertyMetadata(350));

    public int cmbHeight {
        get { return (int)GetValue(cmbHeightProperty); }
        set { SetValue(cmbHeightProperty, value); }
    }
    public static readonly DependencyProperty cmbHeightProperty =
        DependencyProperty.Register(nameof(cmbHeight), typeof(int), typeof(cmbExtendedControl), 
            new PropertyMetadata(50));



    public string PlaceHolderText {
        get { return (string)GetValue(PlaceHolderTextProperty); }
        set { SetValue(PlaceHolderTextProperty, value); }
    }
    public static readonly DependencyProperty PlaceHolderTextProperty =
        DependencyProperty.Register(nameof(PlaceHolderText), typeof(string), typeof(cmbExtendedControl), 
            new PropertyMetadata("Search..."));




    public string cmbText {
        get { return (string)GetValue(cmbTextProperty); }
        set { SetValue(cmbTextProperty, value); }
    }
    public static readonly DependencyProperty cmbTextProperty =
        DependencyProperty.Register(nameof(cmbText), typeof(string), typeof(cmbExtendedControl), 
            new PropertyMetadata(string.Empty));

    public string cmbHorizAlign {
        get { return (string)GetValue(cmbHorizAlignProperty); }
        set { SetValue(cmbHorizAlignProperty, value); }
    }
    public static readonly DependencyProperty cmbHorizAlignProperty =
        DependencyProperty.Register(nameof(cmbHorizAlign), typeof(string), typeof(cmbExtendedControl),
            new PropertyMetadata("Left"));

    public Thickness cmbMargin {
        get { return (Thickness)GetValue(cmbMarginProperty); }
        set { SetValue(cmbMarginProperty, value); }
    }
    public static readonly DependencyProperty cmbMarginProperty =
        DependencyProperty.Register(nameof(cmbMargin), typeof(Thickness), typeof(cmbExtendedControl),
            new PropertyMetadata(new Thickness(20, 0, 0, 0)));

    public Thickness cmbTxtbxPadding {
        get { return (Thickness)GetValue(cmbTxtbxPaddingProperty); }
        set { SetValue(cmbTxtbxPaddingProperty, value); }
    }
    public static readonly DependencyProperty cmbTxtbxPaddingProperty =
        DependencyProperty.Register(nameof(cmbTxtbxPadding), typeof(Thickness), typeof(cmbExtendedControl),
            new PropertyMetadata(new Thickness(-20, 0, 0, 0)));

    public int cmbTxtbxWidth {
        get { return (int)GetValue(cmbTxtbxWidthProperty); }
        set { SetValue(cmbTxtbxWidthProperty, value); }
    }
    public static readonly DependencyProperty cmbTxtbxWidthProperty =
        DependencyProperty.Register(nameof(cmbTxtbxWidth), typeof(int), typeof(cmbExtendedControl), 
            new PropertyMetadata(200));

    //cmbTxtbxPadding


    /// <summary>
    /// Combobox Item settings
    /// </summary>

    public SolidColorBrush cmbItemBackground {
        get { return (SolidColorBrush)GetValue(cmbItemBackgroundProperty); }
        set { SetValue(cmbItemBackgroundProperty, value); }
    }
    public static readonly DependencyProperty cmbItemBackgroundProperty =
        DependencyProperty.Register(nameof(cmbItemBackground), typeof(SolidColorBrush), typeof(cmbExtendedControl), 
            new PropertyMetadata((SolidColorBrush)new BrushConverter().ConvertFrom(Colors.LightGray.ToString())));

    public SolidColorBrush cmbTextBackground {
        get { return (SolidColorBrush)GetValue(cmbTextBackgroundProperty); }
        set { SetValue(cmbTextBackgroundProperty, value); }
    }
    public static readonly DependencyProperty cmbTextBackgroundProperty =
        DependencyProperty.Register(nameof(cmbTextBackground), typeof(SolidColorBrush), typeof(cmbExtendedControl),
            new PropertyMetadata((SolidColorBrush)new BrushConverter().ConvertFrom(Colors.OrangeRed.ToString())));



    public SolidColorBrush btnBckgrnd {
        get { return (SolidColorBrush)GetValue(btnBckgrndProperty); }
        set { SetValue(btnBckgrndProperty, value); }
    }
    public static readonly DependencyProperty btnBckgrndProperty =
        DependencyProperty.Register(nameof(btnBckgrnd), typeof(SolidColorBrush), typeof(cmbExtendedControl), 
            new PropertyMetadata((SolidColorBrush)new BrushConverter().ConvertFrom(Colors.Green.ToString())));






    /// <summary>
    /// Combobox List settings
    /// </summary>
    /// 



    public IEnumerable cmbList {
        get { return (IEnumerable)GetValue(cmbListProperty); }
        set { SetValue(cmbListProperty, value); }
    }
    public static readonly DependencyProperty cmbListProperty =
        DependencyProperty.Register(nameof(cmbList), typeof(IEnumerable), typeof(cmbExtendedControl),
            new FrameworkPropertyMetadata { BindsTwoWayByDefault = false, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });



    public string cmbSelectedItem {
        get { return (string)GetValue(cmbSelectedItemProperty); }
        set { SetValue(cmbSelectedItemProperty, value); }
    }
    public static readonly DependencyProperty cmbSelectedItemProperty =
        DependencyProperty.Register(nameof(cmbSelectedItem), typeof(string), typeof(cmbExtendedControl),
            new PropertyMetadata(string.Empty));


    /// <summary>
    /// ////////// Event Handler, click, commands 
    /// </summary>

    public ICommand btnCommand {
        get { return (ICommand)GetValue(btnCommandProperty); }
        set { SetValue(btnCommandProperty, value); }
    }
    public static readonly DependencyProperty btnCommandProperty =
        DependencyProperty.Register(nameof(btnCommand), typeof(ICommand), typeof(cmbExtendedControl),
            new PropertyMetadata(null));

    public object btnCmdParameter {
        get { return (object)GetValue(btnCmdParameterProperty); }
        set { SetValue(btnCmdParameterProperty, value); }
    }
    public static readonly DependencyProperty btnCmdParameterProperty =
        DependencyProperty.Register(nameof(btnCmdParameter), typeof(object), typeof(cmbExtendedControl),
            new PropertyMetadata(null));

    public event RoutedEventHandler btnClick {
        add { AddHandler(btnClickEvent, value); }
        remove { RemoveHandler(btnClickEvent, value); }
    }
    public static readonly RoutedEvent btnClickEvent =
        EventManager.RegisterRoutedEvent(nameof(btnClick), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(cmbExtendedControl));

    private void OnBtnClick(object sender, RoutedEventArgs e) {
        RaiseEvent(new RoutedEventArgs(btnClickEvent));
    }


    public cmbExtendedControl() {
        InitializeComponent();
    }
}
