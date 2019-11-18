namespace HomeApi.Libraries.Models.Lighting
{
    public class Light
    {
        public byte Brightness { get; set; }

        public double[] ColourCoordinates { get; set; }

        public string ColourMode { get; set; }

        public int? ColourTemperature { get; set; }

        public int? Hue { get; set; }

        public string Id { get; set; }

        public bool IsOn { get; set; }

        public bool? IsReachable { get; set; }

        public string Name { get; set; }

        public int? Saturation { get; set; }

        public int? TransitionMilliseconds { get; set; }
    }
}