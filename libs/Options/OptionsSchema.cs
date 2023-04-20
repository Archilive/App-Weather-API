namespace CSharp_Meteo.Option
{
  public class OptionsSchema {
    public string lang { get; set; } = "fr";
    public string cityName { get; set; } = "Bordeaux";
    public double cityLongitude { get; set; } = -0.5805;
    public double cityLatitude { get; set; } = 44.8404;
    public string tempUnit {get; set;} = "C";
  }
}