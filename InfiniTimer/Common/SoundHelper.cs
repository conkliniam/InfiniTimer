using InfiniTimer.Enums;

namespace InfiniTimer.Common
{
    public static class SoundHelper
    {
        public static readonly Dictionary<TimerSound, SoundInfo> SoundInfo = new()
        {
            {
                TimerSound.None,
                new SoundInfo{ FileName = "", DisplayName = "None" }
            },
            {
                TimerSound.EightBit,
                new SoundInfo{ FileName = "8bit.mp3", DisplayName = "8 Bit" }
            },
            {
                TimerSound.CheerfulMood,
                new SoundInfo{ FileName = "cheerful_mood.mp3", DisplayName = "Cheerful Mood" }
            },
            {
                TimerSound.Choir,
                new SoundInfo{ FileName = "choir.mp3", DisplayName = "Choir" }
            },
            {
                TimerSound.HearMeRingtone,
                new SoundInfo{ FileName = "hear_me_ringtone.mp3", DisplayName = "Hear Me Ringtone" }
            },
            {
                TimerSound.Honk,
                new SoundInfo{ FileName = "honk.mp3", DisplayName = "Honk" }
            },
            {
                TimerSound.Marimba1,
                new SoundInfo{ FileName = "marimba_1.mp3", DisplayName = "Marimba 1" }
            },
            {
                TimerSound.Marimba2,
                new SoundInfo{ FileName = "marimba_2.mp3", DisplayName = "Marimba 2" }
            },
            {
                TimerSound.Marimba3,
                new SoundInfo{ FileName = "marimba_3.mp3", DisplayName = "Marimba 3" }
            },
            {
                TimerSound.Nokia,
                new SoundInfo{ FileName = "nokia.mp3", DisplayName = "Nokia" }
            },
            {
                TimerSound.Ominous,
                new SoundInfo{ FileName = "ominous.mp3", DisplayName = "Ominous" }
            },
            {
                TimerSound.RetroMozart,
                new SoundInfo{ FileName = "retro_mozart.mp3", DisplayName = "Retro Mozart" }
            },
            {
                TimerSound.Ringing1,
                new SoundInfo{ FileName = "ringing_1.mp3", DisplayName = "Ringing 1" }
            },
            {
                TimerSound.Ringing2,
                new SoundInfo{ FileName = "ringing_2.mp3", DisplayName = "Ringing 2" }
            },
            {
                TimerSound.Ringing3,
                new SoundInfo{ FileName = "ringing_3.mp3", DisplayName = "Ringing 3" }
            },
            {
                TimerSound.Ringtone1,
                new SoundInfo{ FileName = "ringtone_1.mp3", DisplayName = "Ringtone 1" }
            },
            {
                TimerSound.Ringtone2,
                new SoundInfo{ FileName = "ringtone_2.mp3", DisplayName = "Ringtone 2" }
            },
            {
                TimerSound.Ringtone3,
                new SoundInfo{ FileName = "ringtone_3.mp3", DisplayName = "Ringtone 3" }
            },
            {
                TimerSound.Ringtone4,
                new SoundInfo{ FileName = "ringtone_4.mp3", DisplayName = "Ringtone 4" }
            },
            {
                TimerSound.Ringtone5,
                new SoundInfo{ FileName = "ringtone_5.mp3", DisplayName = "Ringtone 5" }
            },
            {
                TimerSound.Ringtone6,
                new SoundInfo{ FileName = "ringtone_6.mp3", DisplayName = "Ringtone 6" }
            },
            {
                TimerSound.Ringtone7,
                new SoundInfo{ FileName = "ringtone_7.mp3", DisplayName = "Ringtone 7" }
            },
            {
                TimerSound.Ringtone8,
                new SoundInfo{ FileName = "ringtone_8.mp3", DisplayName = "Ringtone 8" }
            },
        };
    }

    public struct SoundInfo
    {
        public string FileName;
        public string DisplayName;
    }
}
