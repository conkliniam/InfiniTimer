﻿using CommunityToolkit.Mvvm.Messaging;
using InfiniTimer.Common;
using InfiniTimer.Common.Components;
using InfiniTimer.Common.Extensions;
using InfiniTimer.Models.Timers;
using InfiniTimer.Services;
using InfiniTimer.Services.Messages;

namespace InfiniTimer.ViewModels
{
    public class TimerViewModel : CommonBase
    {
        #region Private Fields
        private readonly ViewTimerContent _viewTimerContent;
        private readonly ISavedTimerService _savedTimerService;
        private readonly IStagedTimerService _stagedTimerService;
        private readonly INavigation _navigation;
        private TimerModel _timerModel;
        #endregion

        #region Constructors
        public TimerViewModel(TimerModel timerModel,
                              ViewTimerContent viewTimerContent,
                              ISavedTimerService savedTimerService,
                              IStagedTimerService stagedTimerService,
                              INavigation navigation)
        {
            _viewTimerContent = viewTimerContent;
            _savedTimerService = savedTimerService;
            _stagedTimerService = stagedTimerService;
            _navigation = navigation;
            TimerModel = timerModel;
            RegisterForMessages();
        }
        #endregion

        #region Public Properties
        public TimerModel TimerModel
        {
            get
            {
                return _timerModel;
            }
            set
            {
                if (_timerModel != value)
                {
                    _timerModel = value;

                    RaisePropertyChanged(nameof(TimerModel));
                    FillTimerContent();
                }
            }
        }
        #endregion

        #region Public Methods
        public void HandleStageTimer()
        {
            _stagedTimerService.StageTimer(TimerModel);
        }

        public void HandleUnstageTimer()
        {
            _stagedTimerService.UnstageTimer(TimerModel);
        }

        public void Copy()
        {
            var copy = TimerModel.Copy();
            copy.Name = $"{copy.Name} Copy";
            copy.Id = Guid.NewGuid();
            copy.IsDirty = true;
            _savedTimerService.AddSessionTimer(copy);
            TimerModel = copy;
        }

        public bool HandleSaveTimer()
        {
            return _savedTimerService.SaveTimer(TimerModel.Id);
        }

        public void HandleResetTimer()
        {
            _savedTimerService.ResetTimer(TimerModel.Id);
        }

        public bool HandleDeleteTimer()
        {
            return _savedTimerService.DeleteTimer(TimerModel.Id);
        }
        #endregion

        #region Private Methods
        private void FillTimerContent()
        {
            _viewTimerContent.TimerSection = TimerModel?.Timers;
        }

        private void RegisterForMessages()
        {
            WeakReferenceMessenger.Default.Register<TimerReplacedMessage>(this, (r, m) =>
            {
                OnTimerReplaced(m.Value);
            });

            WeakReferenceMessenger.Default.Register<TimerDoneEditingMessage>(this, (r, m) =>
            {
                OnTimerDoneEditing(m.Value);
            });

            WeakReferenceMessenger.Default.Register<TimerRemovedMessage>(this, async (r, m) =>
            {
                await OnTimerRemoved(m.Value);
            });
        }

        private async Task OnTimerRemoved(Guid timerId)
        {
            if (TimerModel.Id == timerId)
            {
                await _navigation.PopAsync();
            }
        }

        private void OnTimerDoneEditing(TimerModel value)
        {
            if (TimerModel.Id == value.Id)
            {
                RaisePropertyChanged(nameof(TimerModel));
                FillTimerContent();
            }
        }

        private void OnTimerReplaced(TimerModel value)
        {
            if (value.Id == TimerModel.Id)
            {
                TimerModel = value;
            }
        }

        public EditTimerPage GetEditPage()
        {
            return new EditTimerPage(TimerModel, _savedTimerService, _stagedTimerService);
        }
        #endregion
    }
}
