using InfiniTimer.ViewModels;

namespace InfiniTimer
{
    public partial class MainPage : ContentPage
    {
        public MainPage(StagedTimersViewModel stagedTimersViewModel)
        {
            InitializeComponent();
            BindingContext = stagedTimersViewModel;
        }

        private void OnAddTimerClicked(object sender, EventArgs e)
        {
        }
    }
}