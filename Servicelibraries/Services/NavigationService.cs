using CommunityToolkit.Mvvm.ComponentModel;

namespace Servicelibraries;
public partial class NavigationService : ObservableObject {
    
    public event Action CurrentViewModelChanged;

    [ObservableProperty]
    private ObservableObject _currentViewModel;
    
    partial void OnCurrentViewModelChanged(ObservableObject value) {
        CurrentViewModelChanged?.Invoke();
    }


    private void OnCurrentViewModelChanged() {
        CurrentViewModelChanged?.Invoke();
    }

    public NavigationService() {

    }

    public NavigationService(ObservableObject currentViewModel) {
        CurrentViewModel = currentViewModel;
    }


}
