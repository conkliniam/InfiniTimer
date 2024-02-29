using CommunityToolkit.Mvvm.Messaging;
using InfiniTimer.Enums;
using InfiniTimer.Models;
using InfiniTimer.Models.Timers;
using InfiniTimer.Services;
using InfiniTimer.Services.Messages;
using InfiniTimer.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace InfiniTimer.ViewModels
{
    public class EditTimerViewModel
    {
        #region Private Fields
        private readonly StackLayout _timerLayout;
        private readonly ISavedTimerService _savedTimerService;
        private readonly IStagedTimerService _stagedTimerService;
        private EditTimerModel _editTimerModel;
        #endregion

        #region Constructors
        public EditTimerViewModel(StackLayout timerLayout,
                                  TimerModel timerModel,
                                  ISavedTimerService savedTimerService,
                                  IStagedTimerService stagedTimerService,
                                  string title,
                                  INavigation navigation)
        {
            _timerLayout = timerLayout;
            _savedTimerService = savedTimerService;
            _stagedTimerService = stagedTimerService;

            if (timerModel == null)
            {
                timerModel = new SimpleTimerModel();
                _savedTimerService.AddSessionTimer(timerModel);
            }

            EditTimerModel = new EditTimerModel(timerModel);
            TimerTypes = new ObservableCollection<string>(Enum.GetNames(typeof(TimerType)).ToList());
            Title = title;
            FillTimerLayout();
            BackCommand = new Command(async () =>
            {
                SendDoneEditingMessage();
                await navigation.PopAsync();
            });
        }
        #endregion

        #region Public Properties
        public string Title { get; set; }

        public ObservableCollection<String> TimerTypes { get; private set; }

        public EditTimerModel EditTimerModel
        {
            get
            {
                return _editTimerModel;
            }
            set
            {
                if (_editTimerModel != value)
                {
                    var old = _editTimerModel;
                    _editTimerModel = value;

                    if (old != null)
                    {
                        old.PropertyChanged -= EditTimerModel_PropertyChanged;
                    }

                    if (_editTimerModel != null)
                    {
                        _editTimerModel.PropertyChanged += EditTimerModel_PropertyChanged;
                    }
                }
            }
        }

        public EditSingleTimerView SingleTimerView { get; set; }
        public EditAdvancedTimerView AdvancedTimerView { get; set; }
        #endregion

        #region Public Methods
        public void HandleCancel()
        {
            _savedTimerService.ResetTimer(EditTimerModel.TimerModel.Id);
        }

        public bool HandleSave()
        {
            var success = _savedTimerService.SaveTimer(EditTimerModel.TimerModel.Id);
            WeakReferenceMessenger.Default.Send(new TimerDoneEditingMessage(EditTimerModel.TimerModel));
            return success;
        }

        public void HandleStage()
        {
            _stagedTimerService.StageTimer(EditTimerModel.TimerModel);
            WeakReferenceMessenger.Default.Send(new TimerDoneEditingMessage(EditTimerModel.TimerModel));
        }

        public void SendDoneEditingMessage()
        {
            EditTimerModel.TimerModel.IsDirty = true;
            WeakReferenceMessenger.Default.Send(new TimerDoneEditingMessage(EditTimerModel.TimerModel));
        }

        public ICommand BackCommand { get; set; }
        #endregion

        #region Private Methods
        private void EditTimerModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(EditTimerModel.TimerType))
            {
                _timerLayout.Children.Clear();

                var timerName = EditTimerModel.TimerModel.Name;
                var id = EditTimerModel.TimerModel.Id;

                if (EditTimerModel.TimerType == Enum.GetName(typeof(TimerType), TimerType.Simple))
                {
                    EditTimerModel.TimerModel = new SimpleTimerModel()
                    {
                        Id = id,
                        Name = timerName
                    };
                }
                else if (EditTimerModel.TimerType == Enum.GetName(typeof(TimerType), TimerType.Advanced))
                {
                    EditTimerModel.TimerModel = new AdvancedTimerModel()
                    {
                        Id = id,
                        Name = timerName
                    };
                }

                EditTimerModel.TimerModel.IgnoreChanges = false;

                _savedTimerService.AddSessionTimer(EditTimerModel.TimerModel);

                FillTimerLayout();
            }
            else
            {
                Console.WriteLine("PropertyChanged: " + e.PropertyName);
            }
        }

        private void FillTimerLayout()
        {
            if (EditTimerModel.TimerModel is SimpleTimerModel simpleTimerModel)
            {
                simpleTimerModel.Timer ??= new SingleTimerSection(1);
                _timerLayout.Children.Add(new EditSingleTimerView(simpleTimerModel.Timer));
            }
            else if (EditTimerModel.TimerModel is AdvancedTimerModel advancedTimerModel)
            {
                _timerLayout.Children.Add(new EditAdvancedTimerView(advancedTimerModel));
            }
        }
        #endregion
    }
}
