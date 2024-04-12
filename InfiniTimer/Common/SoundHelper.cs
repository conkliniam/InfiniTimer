using InfiniTimer.Enums;

namespace InfiniTimer.Common
{
    public static class SoundHelper
    {
        public const string MP3 = ".mp3";
        public const string AIFF = ".aiff";
        public const string EightBit = "8bit";
        public const string CheerfulMood = "cheerful_mood";
        public const string Choir = "choir";
        public const string HearMe = "hear_me_ringtone";
        public const string Honk = "honk";
        public const string Marimba1 = "marimba_1";
        public const string Marimba2 = "marimba_2";
        public const string Marimba3 = "marimba_3";
        public const string Nokia = "nokia";
        public const string Ominous = "ominous";
        public const string Mozart = "retro_mozart";
        public const string Ringing1 = "ringing_1";
        public const string Ringing2 = "ringing_2";
        public const string Ringing3 = "ringing_3";
        public const string Ringtone1 = "ringtone_1";
        public const string Ringtone2 = "ringtone_2";
        public const string Ringtone3 = "ringtone_3";
        public const string Ringtone4 = "ringtone_4";
        public const string Ringtone5 = "ringtone_5";
        public const string Ringtone6 = "ringtone_6";
        public const string Ringtone7 = "ringtone_7";
        public const string Ringtone8 = "ringtone_8";

        public static readonly Dictionary<TimerSound, SoundInfo> SoundInfo = new()
        {
            {
                TimerSound.None,
                new SoundInfo{ FileName = "", DisplayName = "None" }
            },
            {
                TimerSound.EightBit,
                new SoundInfo{ FileName = EightBit, DisplayName = "8 Bit" }
            },
            {
                TimerSound.CheerfulMood,
                new SoundInfo{ FileName = CheerfulMood, DisplayName = "Cheerful Mood" }
            },
            {
                TimerSound.Choir,
                new SoundInfo{ FileName = Choir, DisplayName = "Choir" }
            },
            {
                TimerSound.HearMeRingtone,
                new SoundInfo{ FileName = HearMe, DisplayName = "Hear Me Ringtone" }
            },
            {
                TimerSound.Honk,
                new SoundInfo{ FileName = Honk, DisplayName = "Honk" }
            },
            {
                TimerSound.Marimba1,
                new SoundInfo{ FileName = Marimba1, DisplayName = "Marimba 1" }
            },
            {
                TimerSound.Marimba2,
                new SoundInfo{ FileName = Marimba2, DisplayName = "Marimba 2" }
            },
            {
                TimerSound.Marimba3,
                new SoundInfo{ FileName = Marimba3, DisplayName = "Marimba 3" }
            },
            {
                TimerSound.Nokia,
                new SoundInfo{ FileName = Nokia, DisplayName = "Nokia" }
            },
            {
                TimerSound.Ominous,
                new SoundInfo{ FileName = Ominous, DisplayName = "Ominous" }
            },
            {
                TimerSound.RetroMozart,
                new SoundInfo{ FileName = Mozart, DisplayName = "Retro Mozart" }
            },
            {
                TimerSound.Ringing1,
                new SoundInfo{ FileName = Ringing1, DisplayName = "Ringing 1" }
            },
            {
                TimerSound.Ringing2,
                new SoundInfo{ FileName = Ringing2, DisplayName = "Ringing 2" }
            },
            {
                TimerSound.Ringing3,
                new SoundInfo{ FileName = Ringing3, DisplayName = "Ringing 3" }
            },
            {
                TimerSound.Ringtone1,
                new SoundInfo{ FileName = Ringtone1, DisplayName = "Ringtone 1" }
            },
            {
                TimerSound.Ringtone2,
                new SoundInfo{ FileName = Ringtone2, DisplayName = "Ringtone 2" }
            },
            {
                TimerSound.Ringtone3,
                new SoundInfo{ FileName = Ringtone3, DisplayName = "Ringtone 3" }
            },
            {
                TimerSound.Ringtone4,
                new SoundInfo{ FileName = Ringtone4, DisplayName = "Ringtone 4" }
            },
            {
                TimerSound.Ringtone5,
                new SoundInfo{ FileName = Ringtone5, DisplayName = "Ringtone 5" }
            },
            {
                TimerSound.Ringtone6,
                new SoundInfo{ FileName = Ringtone6, DisplayName = "Ringtone 6" }
            },
            {
                TimerSound.Ringtone7,
                new SoundInfo{ FileName = Ringtone7, DisplayName = "Ringtone 7" }
            },
            {
                TimerSound.Ringtone8,
                new SoundInfo{ FileName = Ringtone8, DisplayName = "Ringtone 8" }
            },
        };
    }

    public struct SoundInfo
    {
        public string FileName;
        public string DisplayName;
    }
}
