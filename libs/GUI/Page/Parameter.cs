using Gtk;
using CSharp_Meteo.API;
using CSharp_Meteo.Option;
using UI = Gtk.Builder.ObjectAttribute;
using System;

namespace CSharp_Meteo.GUI
{
  public class Parameter : MainApplication
  {
    [UI] private Button confirm = null;
    [UI] private Entry town_input = null;
    [UI] private ComboBoxText dropdown_lang = null;
    [UI] private ComboBoxText temperature_units_available = null;

    public Parameter(Builder builder) : base(builder)
    {
      confirm.Clicked += Window_ConfirmParamsEvent;

      town_input.Text = options.values.cityName;
      dropdown_lang.SetActiveId(options.values.lang);
      temperature_units_available.SetActiveId(options.values.tempUnit);
    }
    private async void Window_ConfirmParamsEvent(object sender, EventArgs a)
    {
      var text = town_input.Text;
      var lang = dropdown_lang.ActiveId;
      var unit = temperature_units_available.ActiveId;

      var resCurrent = await search_API.CallAutocomplete(text, 1);

      if (resCurrent == null)
      {
        town_input.Text = "API Error, retry Later";

        return;
      }

      var schema = new OptionsSchema();
      schema.cityName = resCurrent[0].name;
      schema.cityLatitude = resCurrent[0].coordinates.Latitude;
      schema.cityLongitude = resCurrent[0].coordinates.Longitude;
      schema.lang = lang;
      schema.tempUnit = unit;

      options.saveValues(schema);

      town_input.Text = resCurrent[0].name;
    }
  }
}