namespace ResultManager.Model.Configuration
{
    public class HcpConfiguration : IHcpConfiguration
    {
        public int CourseAdjustedPar { get; set; } = 48;
        public double SlopeFactor { get; set; } = 0.8;
        public int HcpDecimals { get; set; } = 2;
        public int TotalRounds { get; set; } = 18;
    }
}
