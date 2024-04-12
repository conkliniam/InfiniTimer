using InfiniTimer.Common;
using InfiniTimer.Enums;
using InfiniTimer.Models;
using Plugin.Maui.Audio;
using System.Collections.ObjectModel;

namespace InfiniTimer.ViewModels
{
    public class SoundPickerViewModel
    {
        public SoundPickerViewModel(Action<TimerSound, string> HandleSoundSelection)
        {
            PopulateTimerSounds();
            OnChangeSound = HandleSoundSelection;
        }

        public ObservableCollection<SoundPickerModel> TimerSoundOptions { get; private set; }

        public Action<TimerSound, string> OnChangeSound { get; set; }

        public async void PlayPauseSound(SoundPickerModel soundPickerModel)
        {
            if (null == soundPickerModel || string.IsNullOrEmpty(soundPickerModel.FileName))
            {
                return;
            }

            if (null == soundPickerModel.AudioPlayer)
            {
                soundPickerModel.AudioPlayer = AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync(soundPickerModel.FileName));
            }

            if (soundPickerModel.AudioPlayer.IsPlaying)
            {
                soundPickerModel.AudioPlayer.Pause();
            }
            else
            {
                soundPickerModel.AudioPlayer.Play();
            }
            soundPickerModel.IsPlaying = soundPickerModel.AudioPlayer.IsPlaying;
            soundPickerModel.PlayStarted = true;
        }

        public void StopSound(SoundPickerModel soundPickerModel)
        {
            if (null != soundPickerModel && null != soundPickerModel.AudioPlayer)
            {
                if (!soundPickerModel.AudioPlayer.IsPlaying)
                {
                    soundPickerModel.AudioPlayer.Play();
                }

                soundPickerModel.AudioPlayer.Stop();
                soundPickerModel.IsPlaying = soundPickerModel.AudioPlayer.IsPlaying;
                soundPickerModel.PlayStarted = false;
            }
        }

        private void PopulateTimerSounds()
        {
            TimerSoundOptions = new ObservableCollection<SoundPickerModel>();

            foreach (KeyValuePair<TimerSound, SoundInfo> soundInfo in SoundHelper.SoundInfo)
            {
                TimerSoundOptions.Add(new SoundPickerModel
                {
                    Sound = soundInfo.Key,
                    DisplayName = soundInfo.Value.DisplayName,
                    FileName = soundInfo.Value.FileName + SoundHelper.MP3,
                });
            }
        }
    }
}
