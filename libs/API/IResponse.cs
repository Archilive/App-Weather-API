using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;


namespace CSharp_Meteo.API
{
  public abstract class Response
  {
    public DateTime RespondedAt {get;} = DateTime.Now;
  }
}