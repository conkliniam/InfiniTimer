using InfiniTimer.Common;
using InfiniTimer.Enums;
using Plugin.Maui.Audio;

namespace InfiniTimer.Models
{
    public class SoundPickerModel : CommonBase
    {
        #region Private Fields
        private bool _isPlaying;
        private bool _playStarted;
        private IAudioPlayer _audioPlayer;
        #endregion

        #region Public Properties
        public TimerSound Sound { get; set; }
        public string DisplayName { get; set; }
        public string FileName { get; set; }
        public IAudioPlayer AudioPlayer
        {
            get
            {
                return _audioPlayer;
            }
            set
            {
                if (_audioPlayer != value)
                {
                    var old = _audioPlayer;
                    _audioPlayer = value;

                    if (old != null)
                    {
                        old.PlaybackEnded -= AudioPlayer_PlaybackEnded;
                    }

                    if (_audioPlayer != null)
                    {
                        _audioPlayer.PlaybackEnded += AudioPlayer_PlaybackEnded;
                    }
                }
            }
        }

        public bool IsPlaying
        {
            get
            {
                return _isPlaying;
            }
            set
            {
                if (_isPlaying != value)
                {
                    _isPlaying = value;
                    RaisePropertyChanged(nameof(IsPlaying));
                }
            }
        }

        public bool PlayStarted
        {
            get
            {
                return _playStarted;
            }
            set
            {
                if (_playStarted != value)
                {
                    _playStarted = value;
                    RaisePropertyChanged(nameof(PlayStarted));
                }
            }
        }
        #endregion

        #region Private Methods
        private void AudioPlayer_PlaybackEnded(object sender, EventArgs e)
        {
            IsPlaying = false;
            PlayStarted = false;
        }
        #endregion
    }
}
