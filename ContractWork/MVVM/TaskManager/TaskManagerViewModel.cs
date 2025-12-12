using System.Diagnostics.Eventing.Reader;

namespace ContractWork.MVVM.TaskManager;

public partial class TaskManagerViewModel : ObservableObject {

#region Observable Properties
    [ObservableProperty]
    private DataTable _dtTask;

    [ObservableProperty]
    private NewTaskContent _taskContent;

    [ObservableProperty]
    private ObservableCollection<NewTaskContent> _taskList;

    [ObservableProperty]
    private NewTaskContent _selectedTask;

    [ObservableProperty]
    private string _selectedNote;




    [ObservableProperty]
    private string _tasksViewingby = "Viewing Current";
    [ObservableProperty]
    private bool? _isCheckedTasksArchived = false;
    #endregion

    private int click = 0;

    public NavigationService _navigationService { get; set; }
    public TaskManagerViewModel(NavigationService navigationService) {
        _navigationService = navigationService;
        TaskList = new();
        _taskContent = new();
        RunSetup();
    }

    public void RunSetup() {
        AccessService db = new();
        string sqlLoadTasks =
            "SELECT * " +
            "FROM [tblTask] ";

        TasksViewingby = "Viewing All";

        if (IsCheckedTasksArchived == false) {
            TasksViewingby = "Viewing Current";
            sqlLoadTasks += "WHERE [IsCompleted] = 0";
        }
        else if (IsCheckedTasksArchived == true) {
            TasksViewingby = "Viewing Archived";
            sqlLoadTasks += "WHERE [IsCompleted] = -1";
        }

        DtTask = db.FetchDBRecordRequest(sqlLoadTasks);

        TaskList.Clear();
        int posOfNewLine;
        foreach (DataRow drow in DtTask.Rows) {
            
            //if (string.IsNullOrEmpty(drow["CustomerName"].ToString())) {
            //    posOfNewLine = drow["Notes"].ToString().IndexOf("\r\n");
            //    drow["CustomerName"] = drow["Notes"].ToString().Substring(15, posOfNewLine - 15);
            //}
            TaskList.Add(new NewTaskContent(drow));
        }
    }

    [RelayCommand]
    private void NewTask() {
        _taskContent = new();
        NewTaskPopup CustomerNewTask = new(_taskContent);
        if (CustomerNewTask.ShowDialog() == true) {
            _taskContent = CustomerNewTask.taskContent;
            NewTask_db(_taskContent);
        }
    }

    private void NewTask_db(NewTaskContent taskContent) {
        AccessService db = new();

        //DateOnly? datestartedonly = DateOnly.FromDateTime((DateTime)taskContent.dateStarted);
        // DateOnly? datedueonly = DateOnly.FromDateTime((DateTime)taskContent.dateDue);

        //if (taskContent.customerID == 0 && string.IsNullOrEmpty(taskContent.customerName)) {
        //    taskContent.note = $"Customer Name: {taskContent.customerName} \n";
        //}       
        
        string sqlNewTask =
            "INSERT INTO tblTask ([CustomerID], [CustomerName], [Description], [Notes], [DateCreated], [DateDue], [Importance]) " +
            $"VALUES ({taskContent.customerID}, '{taskContent.customerName}', '{taskContent.description}', '{taskContent.note}', #{taskContent.dateStarted}#, " +
            $"#{taskContent.dateDue}#, {taskContent.importance})";

        //see if there is no taskContent.customerName put the first line of taskContent.note


        //SqlParameter notesParameter = new SqlParameter("@dateStart", datestartedonly);
        //notesParameter = new SqlParameter("@dateDue", datedueonly);

        db.AddToAccount(SQLInsert: sqlNewTask, fileLocation: DashboardViewModel.fileLocatinDict["AccessFilePath"]);     //, parameters: notesParameter);

        if (string.IsNullOrEmpty(taskContent.customerName)) {
            //string custName = taskContent.note.Substring(15, 26);
            taskContent.customerName = taskContent.note.Substring(15, 26);
        }

        DtTask.Rows.Add(taskContent.taskID, taskContent.customerID, taskContent.customerName, taskContent.description, taskContent.note, taskContent.dateStarted, taskContent.dateDue, taskContent.dateCompleted, taskContent.importance, taskContent.IsCompleted);
        RunSetup();
    }

    [RelayCommand]
    private void EditTask() {
        if (SelectedTask != null) {
            NewTaskPopup CustomerNewTask = new(SelectedTask);
            if (CustomerNewTask.ShowDialog() == true) {
                _taskContent = CustomerNewTask.taskContent;
                //NewTask_db(_taskContent);
                UpdateTask(_taskContent);
            }
        }
        else NewTask();
        

    }

    private void UpdateTask(NewTaskContent taskContent) {
        AccessService db = new();
        string sqlUpdateTask =
            "UPDATE [tblTask] " +
            $"SET [CustomerID] = {taskContent.customerID}, [CustomerName] = '{taskContent.customerName}', [Description] = '{taskContent.description}', " +
            $"[DateCreated] = #{taskContent.dateStarted}#, [DateDue] = #{taskContent.dateDue}#, [Importance] = {taskContent.importance} " +
            $"WHERE [TaskID] = {taskContent.taskID}";

        db.AddToAccount(SQLInsert: sqlUpdateTask);
        RunSetup();
    }

    [RelayCommand]
    private void IncludeArchivedTasks() {
        RunSetup();
    }


    [RelayCommand]
    private void Check(object parameter) {
        var values = (object[])parameter;
        int selectedTaskID = (int)values[0];
        bool completedOrNot = (bool)values[1];

        AccessService db = new();
        string sqlCheckedIsComplete =
            "UPDATE [tblTask] " +
            $"SET [IsCompleted] = {completedOrNot} ";

        if (completedOrNot == true) {
            sqlCheckedIsComplete += ", [DateCompleted] = NOW() ";
        }
            
        sqlCheckedIsComplete += $"WHERE [TaskID] = {selectedTaskID}";

        db.AddToAccount(SQLInsert: sqlCheckedIsComplete);
        RunSetup();
    }

    [RelayCommand] 
    private void TaskPicked() {
        if (SelectedTask != null){
            string _name = SelectedTask.customerName;
            if (!string.IsNullOrEmpty(_name)) {
                // if (!string.IsNullOrEmpty(SelectedTask.customerName) && SelectedTask != null) {
                AccessService db = new();

                int num = SelectedTask.taskID;
                string sqlGetTaskNotes =
                    "SELECT [Notes] " +
                    "FROM [tblTask] " +
                    $"WHERE [TaskID] = {num}";

                DataTable dt = db.FetchDBRecordRequest(sqlGetTaskNotes);
                SelectedNote = dt.Rows[0]["Notes"].ToString();
                click = 0;
            }
        }
        else if (click == 2) {
            NewTask();
            click = 0;
        }
        else click++;
    }
}


public partial class NewTaskContent : ObservableObject {
    public int taskID { get; set; } = 0;
    public int customerID { get; set; } = 0;
    public string customerName { get; set; } = string.Empty;
    public string description { get; set; } = string.Empty;
    public string note { get; set; } = string.Empty;
    public DateTime? dateStarted { get; set; } = null;
    public DateTime? dateDue { get; set; } = null;
    public DateTime? dateCompleted { get; set; } = null;
    public int importance { get; set; } = 0;

    [ObservableProperty]
    private bool? _isCompleted = false;

    public string rating { get; set; }

    string[] notesParams = { "tblTask", "Notes", "TaskID" };


    public NewTaskContent() {

    }

    public NewTaskContent(DataRow dataRow) {
        ScrubDataFromDataRow(dataRow);
    }

    private void ScrubDataFromDataRow(DataRow dtRow) {

        if (dtRow.Table.Columns.Contains("TaskID")) {
            taskID = (int)dtRow["TaskID"];
        }
        if (dtRow.Table.Columns.Contains("CustomerID")) {
            customerID = (int)dtRow["CustomerID"];
        }
        if (dtRow.Table.Columns.Contains("CustomerName")) {
            customerName = dtRow["CustomerName"].ToString();
        }
        if (dtRow.Table.Columns.Contains("Description")) {
            description = dtRow["Description"].ToString();
        }
        if (dtRow.Table.Columns.Contains("Notes")) {
            note = dtRow["Notes"].ToString();
        }
        if (dtRow.Table.Columns.Contains("DateCreated")) {
            if (dtRow["DateCreated"] != DBNull.Value) {
                dateStarted = (DateTime)dtRow["DateCreated"];
            }
        }
        if (dtRow.Table.Columns.Contains("DateDue")) {
            if (dtRow["DateDue"] != DBNull.Value) {
                dateDue = (DateTime)dtRow["DateDue"];
            }
        }
        if (dtRow.Table.Columns.Contains("DateCompleted")) {
            if (dtRow["DateCompleted"] != DBNull.Value) {
                dateCompleted = (DateTime)dtRow["DateCompleted"];
            }
        }
        if (dtRow.Table.Columns.Contains("Importance")) {
            importance = (int)dtRow["Importance"];
            GetRating(importance);
        }
        if (dtRow.Table.Columns.Contains("IsCompleted")) {
            IsCompleted = Convert.ToBoolean(dtRow["IsCompleted"]);
        }
        
    }

    private void GetRating(int importance) {
        switch (importance) {
            case 1:
                rating = "Routine";
                break;
            case 2:
                rating = "Priority";
                break;
            case 3:
                rating = "Urgent";
                break;
        }
    }

    [RelayCommand]
    private void Check(int selectedTask) {
        //AccessService db = new();
        //string sqlCheckedIsComplete =
        //    "UPDATE [tblTask] " +
        //    "SET [IsCompleted] = True " +
        //    $"WHERE [TaskID] = {selectedTask}";

        //db.AddToAccount(SQLInsert: sqlCheckedIsComplete);
        
        
        MessageBox.Show("checked in class");
    }

    [RelayCommand]
    private void LostFocusCompareValue(string notesToWrite) {
        AccessService db = new();
        string sqlUpdateNotes =
            "UPDATE [tblTask] " +
            $"SET [Notes] = '{notesToWrite}' " +
            $"WHERE [TaskID] = {taskID}";

        //AccessService.UpdateCustomerAccountDetails<NewTaskContent>(note, this, notesParams);
        db.AddToAccount(SQLInsert: sqlUpdateNotes);
    }

    [RelayCommand]
    private void GotFocusInitialValue() {

    }
}

