namespace LightningAlert.Models
{
    public class Lightning
    {
        public int flashType { get; set; }
        public long strikeTime { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public int peakAmps { get; set; }
        public string reserved { get; set; }
        public int icHeight { get; set; }
        public long receivedTime { get; set; }
        public int numberOfSensors { get; set; }
        public int multiplicity { get; set; }
    }
}
