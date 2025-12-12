using System.Windows.Threading;

namespace ContractWork.MVVM.Popups;

public partial class TimedMessageBoxPopup : Window {           //, INotifyPropertyChanged, ICommand {

    public string ReturnStatus { get; set; } = "Good";

    //string textParagraph = @"D:\Cloud Services\NextCloud\Physio Work
    //                   \\r\\n\VBA files and images\Access Customer DB
    //                   files\Best version at ths time\_Customers
    //                   copy _2021-12-22 (conflicted copy 2022-02
    //                   -01 144142).accdb";

    DispatcherTimer timer;
    private int increment;

    public TimedMessageBoxPopup(string messageEnd, int duration = 3) {
        InitializeComponent();
        increment = duration;
        PopupMessageBegin.Text = "This Message Box will exit in";
        lblSecs.Content = $"{duration} secs.";
    //FormatEndText(messageEnd);
        PopupMessageEnd.Text = messageEnd;
        


    }

    private void FormatEndText(string messageEnd) {
        string formattedString = string.Empty;
        string newlineString = string.Empty;
        int stringPos=39;
        int charPos = 0;


        for (int i = 0; i < messageEnd.Length;) {

            if (messageEnd.Length - i < stringPos) { stringPos = messageEnd.Length - i; }

            newlineString += messageEnd.Substring(i, stringPos);
            newlineString += GL.nl + GL.tb;

            //formattedString +=

            i += charPos;
            //i = formattedString.Length;
        }
        PopupMessageEnd.Text = newlineString;
    }

    private void Window_Loaded(object sender, RoutedEventArgs e) {
        timer = new DispatcherTimer();
        timer.Interval = TimeSpan.FromSeconds(1);
        timer.Tick += Timer_Tick;
        timer.Start();
    }

    private void Timer_Tick(object sender, EventArgs e) {
        increment--;
        increment.ToString();
        lblSecs.Content = $"{increment} secs.";
        if (increment <= 0) { timer.Stop(); this.Close(); }
        else if (ReturnStatus == "Change File Location") { timer.Stop(); this.Close(); }
    }

    private void Continue_Unchecked(object sender, RoutedEventArgs e) {
        if (this.timer == null) {
            this.timer = new DispatcherTimer();
            this.timer.Interval = TimeSpan.FromSeconds(1);
            this.timer.Tick += new EventHandler(Timer_Tick);
        }
        timer.Start();
    }

    private void Pause_Checked(object sender, RoutedEventArgs e) {
        timer.Stop();
    }

    private void Next_Click(object sender, RoutedEventArgs e) {
        increment = 0;
    }

    private void ChangeButton_Click(object sender, RoutedEventArgs e) {
        ReturnStatus = "Change File Location";
        //timer.Stop();
        var window = Window.GetWindow(this);
        window.DialogResult = true;
    }

    private void Exit_Click(object sender, RoutedEventArgs e) {
        ReturnStatus = "Exit";
        timer.Stop();
        var window = Window.GetWindow(this);
        window.DialogResult = true;
        Application.Current.Shutdown();
    }

        
}

