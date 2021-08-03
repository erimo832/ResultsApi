namespace ResultManager.Model.Configuration
{
    public interface IHcpConfiguration
    {
        int CourseAdjustedPar { get; set; }
        double SlopeFactor { get; set; }
        int HcpDecimals { get; set; }
        int TotalRounds { get; set; }
    }
}
