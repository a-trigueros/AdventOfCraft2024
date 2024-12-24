namespace ControlSystem.Core
{
    public enum SleighEngineStatus
    {
        Off,
        On
    }
    
    public static class SleighEngineStatusExtensions
    {
        public static bool IsStarted(this SleighEngineStatus status) => status == SleighEngineStatus.On;
    }
}