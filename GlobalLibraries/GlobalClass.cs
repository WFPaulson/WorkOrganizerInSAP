using System.Data;
using System.Windows;

namespace GlobalLibraries;
public class GlobalColumnWidths : ObservableObject {

    public Dictionary<string, int> ColumnWidths { get; set; }

    public GlobalColumnWidths() {
        ColumnWidths = new Dictionary<string, int>();
    }

    public void GetMaxColumnWidths(DataTable dataTable) {

        List<int> DataGridMinColumnWidths = new List<int>();
        List<int> NewColumnWidth = new List<int>();
        List<int> ColumnWidthCompare;

        int length = 0, currentlength = 0, oldlength = 0, newlength = 0;

        #region get header widths first

        foreach (DataColumn dc in dataTable.Columns) {
            if (dc.ColumnName.ToLower().Contains("date")) { length = 99; }
            else { length = dc.ColumnName.Length; }

            ColumnWidths.Add(dc.ColumnName, length);

            DataGridMinColumnWidths.Add(length);
            NewColumnWidth.Add(length);
        }
        #endregion

        #region read each row get column length but skip header row 1
        string item = string.Empty;
        foreach (DataRow dr in dataTable.Rows.Cast<DataRow>().Skip(1)) {
            ColumnWidthCompare = new List<int>();
            foreach (DataColumn dc in dataTable.Columns) {
                item = dr[dc].ToString();
                length = item.Length;
                ColumnWidthCompare.Add(length);
            }

            for (int i = 0; i < ColumnWidthCompare.Count; i++) {
                currentlength = ColumnWidthCompare[i];
                oldlength = NewColumnWidth[i];
                newlength = currentlength > oldlength ? currentlength : oldlength;
                NewColumnWidth[i] = newlength;
            }
        }
        #endregion

        #region compare widths to get widest
        int diff = 0, count = 0;

        for (int i = 0; i < NewColumnWidth.Count; i++) {
            diff = DataGridMinColumnWidths[i] - NewColumnWidth[i];
            if (diff == 0) { NewColumnWidth[i] = 0; }
        }

        var keys = new List<string>(ColumnWidths.Keys);

        foreach (string key in keys) {
            ColumnWidths[key] = NewColumnWidth[count];
            count++;
        }

        #endregion

    }
}

    public class GlobalVisibility : ObservableObject {

        private Visibility _ServicePlanNumberVisibility = Visibility.Visible;
        public Visibility ServicePlanNumberVisibility {
            get => _ServicePlanNumberVisibility;
            set {
                if (_ServicePlanNumberVisibility != value) {
                    _ServicePlanNumberVisibility = value;
                    OnPropertyChanged(nameof(ServicePlanNumberVisibility));
                }
            }
        }

        private Visibility _SerialNumberVisibility = Visibility.Visible;
        public Visibility SerialNumberVisibility {
            get => _SerialNumberVisibility;
            set {
                if (_SerialNumberVisibility != value) {
                    _SerialNumberVisibility = value;
                    OnPropertyChanged(nameof(SerialNumberVisibility));
                }
            }
        }

        private Visibility _AccountNameVisibility = Visibility.Visible;
        public Visibility AccountNameVisibility {
            get => _AccountNameVisibility;
            set {
                if (_AccountNameVisibility != value) {
                    _AccountNameVisibility = value;
                    OnPropertyChanged(nameof(AccountNameVisibility));
                }
            }
        }

        private Visibility _StartDateVisibility = Visibility.Visible;
        public Visibility StartDateVisibility {
            get => _StartDateVisibility;
            set {
                if (_StartDateVisibility != value) {
                    _StartDateVisibility = value;
                    OnPropertyChanged(nameof(StartDateVisibility));
                }
            }
        }

        private Visibility _POExpireDateVisibility = Visibility.Visible;
        public Visibility POExpireDateVisibility {
            get => _POExpireDateVisibility;
            set {
                if (_POExpireDateVisibility != value) {
                    _POExpireDateVisibility = value;
                    OnPropertyChanged(nameof(POExpireDateVisibility));
                }
            }
        }

        private Visibility _ExpirationDateVisibility = Visibility.Visible;
        public Visibility ExpirationDateVisibility {
            get => _ExpirationDateVisibility;
            set {
                if (_ExpirationDateVisibility != value) {
                    _ExpirationDateVisibility = value;
                    OnPropertyChanged(nameof(ExpirationDateVisibility));
                }
            }
        }

        private Visibility _NewPORequiredVisibility = Visibility.Visible;
        public Visibility NewPORequiredVisibility {
            get => _NewPORequiredVisibility;
            set {
                if (_NewPORequiredVisibility != value) {
                    _NewPORequiredVisibility = value;
                    OnPropertyChanged(nameof(NewPORequiredVisibility));
                }
            }
        }

        private Visibility _NewQuoteRequiredVisibility = Visibility.Visible;
        public Visibility NewQuoteRequiredVisibility {
            get => _NewQuoteRequiredVisibility;
            set {
                if (_NewQuoteRequiredVisibility != value) {
                    _NewQuoteRequiredVisibility = value;
                    OnPropertyChanged(nameof(NewQuoteRequiredVisibility));
                }
            }
        }

        private Visibility _TypeVisibility = Visibility.Visible;
        public Visibility TypeVisibility {
            get => _TypeVisibility;
            set {
                if (_TypeVisibility != value) {
                    _TypeVisibility = value;
                    OnPropertyChanged(nameof(TypeVisibility));
                }
            }
        }

        private Visibility _MainAccountVisibility = Visibility.Visible;
        public Visibility MainAccountVisibility {
            get => _MainAccountVisibility;
            set {
                if (_MainAccountVisibility != value) {
                    _MainAccountVisibility = value;
                    OnPropertyChanged(nameof(MainAccountVisibility));
                }
            }
        }

        private Visibility _PlanStatusVisibility = Visibility.Visible;
        public Visibility PlanStatusVisibility {
            get => _PlanStatusVisibility;
            set {
                if (_PlanStatusVisibility != value) {
                    _PlanStatusVisibility = value;
                    OnPropertyChanged(nameof(PlanStatusVisibility));
                }
            }
        }

        private Visibility _ContractDescriptionVisibility = Visibility.Visible;
        public Visibility ContractDescriptionVisibility {
            get => _ContractDescriptionVisibility;
            set {
                if (_ContractDescriptionVisibility != value) {
                    _ContractDescriptionVisibility = value;
                    OnPropertyChanged(nameof(ContractDescriptionVisibility));
                }
            }
        }

        private Visibility _ModelDescriptionVisibility = Visibility.Visible;
        public Visibility ModelDescriptionVisibility {
            get => _ModelDescriptionVisibility;
            set {
                if (_ModelDescriptionVisibility != value) {
                    _ModelDescriptionVisibility = value;
                    OnPropertyChanged(nameof(ModelDescriptionVisibility));
                }
            }
        }

        private Visibility _ArchiveVisibility = Visibility.Visible;
        public Visibility ArchiveVisibility {
            get => _ArchiveVisibility;
            set {
                if (_ArchiveVisibility != value) {
                    _ArchiveVisibility = value;
                    OnPropertyChanged(nameof(ArchiveVisibility));
                }
            }
        }

        private Visibility _ServiceRepVisibility = Visibility.Visible;
        public Visibility ServiceRepVisibility {
            get => _ServiceRepVisibility;
            set {
                if (_ServiceRepVisibility != value) {
                    _ServiceRepVisibility = value;
                    OnPropertyChanged(nameof(ServiceRepVisibility));
                }
            }
        }

        private Visibility _SalesRepVisibility = Visibility.Visible;
        public Visibility SalesRepVisibility {
            get => _SalesRepVisibility;
            set {
                if (_SalesRepVisibility != value) {
                    _SalesRepVisibility = value;
                    OnPropertyChanged(nameof(SalesRepVisibility));
                }
            }
        }

        private Visibility _LocationAddressVisibility = Visibility.Visible;
        public Visibility LocationAddressVisibility {
            get => _LocationAddressVisibility;
            set {
                if (_LocationAddressVisibility != value) {
                    _LocationAddressVisibility = value;
                    OnPropertyChanged(nameof(LocationAddressVisibility));
                }
            }
        }

        private Visibility _CountyVisibility = Visibility.Visible;
        public Visibility CountyVisibility {
            get => _CountyVisibility;
            set {
                if (_CountyVisibility != value) {
                    _CountyVisibility = value;
                    OnPropertyChanged(nameof(CountyVisibility));
                }
            }
        }
        
    }

    //public class GlobalServicePlanID : ObservableService {
    //    public Dictionary<string, int> ServicePlanIDValue { get; set; }

    //    string ServicePlanNumber;
    //    int ServicePlanId;

    //    public GlobalServicePlanID() {
    //        ServicePlanIDValue = new Dictionary<string, int>();
    //    }

    //    public void GetServicePlanNumberList(string _serviceplanNumber) {


    //    }
    //}

    //public class GlobalCustomerAccountID : ObservableService {
    //    public Dictionary<string, int> CustomerAccountIDValue { get; set; }
        
    //    string AccountName;
    //    int CustomerAccountID;

    //    public GlobalCustomerAccountID() {
    //        CustomerAccountIDValue = new Dictionary<string, int>();
    //    }

    //    public void GetCustomerAccountIDList() {


    //    }
    //}


