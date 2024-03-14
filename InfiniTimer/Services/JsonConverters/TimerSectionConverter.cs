using InfiniTimer.Enums;
using InfiniTimer.Models.Timers;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace InfiniTimer.Services.JsonConverters
{
    public class TimerSectionConverter : JsonConverter<TimerSection>
    {
        enum TypeDiscriminator
        {
            SingleTimer = 1,
            AlternatingTimers = 2,
            SequentialTimers = 3,
        }

        public override bool CanConvert(Type typeToConvert) =>
            typeof(TimerSection).IsAssignableFrom(typeToConvert);

        public override TimerSection Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            reader.Read();
            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException();
            }

            string propertyName = reader.GetString();
            if (propertyName != "TypeDiscriminator")
            {
                throw new JsonException();
            }

            reader.Read();
            if (reader.TokenType != JsonTokenType.Number)
            {
                throw new JsonException();
            }

            TypeDiscriminator typeDiscriminator = (TypeDiscriminator)reader.GetInt32();
            TimerSection timerSection = typeDiscriminator switch
            {
                TypeDiscriminator.SingleTimer => new SingleTimerSection(),
                TypeDiscriminator.AlternatingTimers => new AlternatingTimerSection(),
                TypeDiscriminator.SequentialTimers => new SequentialTimerSection(),
                _ => throw new JsonException()
            };

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return timerSection;
                }

                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    propertyName = reader.GetString();
                    reader.Read();
                    switch (propertyName)
                    {
                        case "Depth":
                            int depth = reader.GetInt32();
                            timerSection.Depth = depth;
                            break;
                        case "DisplayText":
                            string displayText = reader.GetString();
                            ((SingleTimerSection)timerSection).DisplayText = displayText;
                            break;
                        case "Seconds":
                            int seconds = reader.GetInt32();
                            ((SingleTimerSection)timerSection).Seconds = seconds;
                            break;
                        case "Color":
                            TimerColor color = (TimerColor)reader.GetInt32();
                            ((SingleTimerSection)timerSection).Color = color;
                            break;
                        case "Sound":
                            TimerSound sound = (TimerSound)reader.GetInt32();
                            ((SingleTimerSection)timerSection).Sound = sound;
                            break;
                        case "Vibrate":
                            bool vibrate = reader.GetBoolean();
                            ((SingleTimerSection)timerSection).Vibrate = vibrate;
                            break;
                        case "MainTimerSection":
                            TimerSection mainTimerSection = (TimerSection)JsonSerializer.Deserialize(ref reader, typeof(TimerSection), options);
                            ((AlternatingTimerSection)timerSection).MainTimerSection = mainTimerSection;
                            break;
                        case "AlternateTimerSection":
                            TimerSection alternateTimerSection = (TimerSection)JsonSerializer.Deserialize(ref reader, typeof(TimerSection), options);
                            ((AlternatingTimerSection)timerSection).AlternateTimerSection = alternateTimerSection;
                            break;
                        case "Cycles":
                            int cycles = reader.GetInt32();
                            ((AlternatingTimerSection)timerSection).Cycles = cycles;
                            break;
                        case "TimerSections":
                            if (reader.TokenType != JsonTokenType.StartArray)
                            {
                                throw new JsonException();
                            }
                            reader.Read();

                            ObservableCollection<TimerSection> timerSections = new();

                            while (reader.TokenType != JsonTokenType.EndArray)
                            {
                                timerSections.Add(JsonSerializer.Deserialize<TimerSection>(ref reader, options)!);

                                reader.Read();
                            }

                            ((SequentialTimerSection)timerSection).TimerSections = timerSections;
                            break;
                    }
                }
            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, TimerSection timerSection, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            if (timerSection is SingleTimerSection singleTimerSection)
            {
                writer.WriteNumber("TypeDiscriminator", (int)TypeDiscriminator.SingleTimer);
                writer.WriteString("DisplayText", singleTimerSection.DisplayText);
                writer.WriteNumber("Seconds", singleTimerSection.Seconds);
                writer.WriteNumber("Color", (int)singleTimerSection.Color);
                writer.WriteBoolean("Vibrate", singleTimerSection.Vibrate);
                writer.WriteNumber("Sound", (int)singleTimerSection.Sound);
            }
            else if (timerSection is AlternatingTimerSection alternatingTimerSection)
            {
                writer.WriteNumber("TypeDiscriminator", (int)TypeDiscriminator.AlternatingTimers);
                writer.WritePropertyName("MainTimerSection");
                JsonSerializer.Serialize(writer, alternatingTimerSection.MainTimerSection, typeof(TimerSection), options);
                writer.WritePropertyName("AlternateTimerSection");
                JsonSerializer.Serialize(writer, alternatingTimerSection.AlternateTimerSection, typeof(TimerSection), options);
                writer.WriteNumber("Cycles", alternatingTimerSection.Cycles);
            }
            else if (timerSection is SequentialTimerSection sequentialTimerSection)
            {
                writer.WriteNumber("TypeDiscriminator", (int)TypeDiscriminator.SequentialTimers);
                writer.WritePropertyName("TimerSections");
                writer.WriteStartArray();

                foreach (TimerSection section in sequentialTimerSection.TimerSections)
                {
                    JsonSerializer.Serialize(writer, section, typeof(TimerSection), options);
                }

                writer.WriteEndArray();
            }

            writer.WriteNumber("Depth", timerSection.Depth);

            writer.WriteEndObject();
        }
    }
}
