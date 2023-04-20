namespace CSharp_Meteo.API
{
  public class SpottDefaultResponse : Response
  {
    public string message { get; set; }
  }
  public class SpottAutocompleteFieldCoordinates
  {

    public double Latitude { get; set; }
    public double Longitude { get; set; }
  }
  public class SpottAutocompleteFieldCountry
  {
    public string id { get; set; }
    public string name { get; set; }
    public string localizedName { get; set; }
  }
  public class SpottAutocompleteResponse : Response
  {
    public string name { get; set; }
    public SpottAutocompleteFieldCoordinates coordinates { get; set; }
    public SpottAutocompleteFieldCountry country { get; set; }
  }
}