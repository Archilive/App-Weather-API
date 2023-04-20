using System;
using System.IO;
using Newtonsoft.Json;
using Gtk;

namespace CSharp_Meteo.Option
{
  public class Options
  {
    public OptionsSchema values { get; private set; } = new OptionsSchema();

    public Options()
    {
      if (!File.Exists("./options.json"))
      {
        Jsonify(values);
      }
      else
      {
        var jsonOpts = File.ReadAllText("./options.json");

        values = JsonConvert.DeserializeObject<OptionsSchema>(jsonOpts);
      }
    }

    public void saveValues(OptionsSchema newValues)
    {
      values = newValues;

      Jsonify(newValues);
    }

    private void Jsonify(OptionsSchema schema)
    {
      var jsonify = JsonConvert.SerializeObject(values);

      using (StreamWriter sw = File.CreateText("./options.json"))
      {
        sw.Write(jsonify);
      }
    }
  }
}