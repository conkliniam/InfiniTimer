using InfiniTimer.ViewModels;

namespace InfiniTimer
{
    public partial class MainPage : ContentPage
    {
        private Timer _logoTimer;

        public MainPage(StagedTimersViewModel stagedTimersViewModel)
        {
            InitializeComponent();
            BindingContext = stagedTimersViewModel;
            InitLogoTimer();
        }

        private void InitLogoTimer()
        {
            _logoTimer = new Timer(RotateLogo, null, 1000, 3000);
        }

        private async void RotateLogo(object state)
        {
            await infiniTimerLogo.RelRotateTo(360, 2000);
            infiniTimerLogo.Rotation = 0;
        }

        private void OnAddTimerClicked(object sender, EventArgs e)
        {
            
        }
    }
}