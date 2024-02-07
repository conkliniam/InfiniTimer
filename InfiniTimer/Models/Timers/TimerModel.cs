namespace InfiniTimer.Models.Timers
{
    public abstract class TimerModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public bool IsDirty { get; set; }
    }
}
