namespace support_tracker.ViewModels
{
    public enum Style { primary, success, danger }
    public class AlertModel
    {
        public string Style { get; set; }
        public string Message { get; set; }
    }
}