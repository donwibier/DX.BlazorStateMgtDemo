using DevExpress.Blazor;
using DX.BlazorStateMgtDemo.Data;

namespace DX.BlazorStateMgtDemo.ViewModels
{
    public interface IGridViewModel
    {
        ButtonRenderStyle ButtonStyle { get; }
        string ButtonText { get; }
        bool ShowFahrenheit { get; }
        bool ShowCelcius { get; }

        event Action? StateChanged;
        WeatherForecast[] Forecast { get; }
        bool HasData { get; }

        void ToggleTemperatureScale();

        Task Initialize();

    }
    public class GridViewModel : IGridViewModel
    {
        readonly WeatherForecastService weatherService;
        public GridViewModel(WeatherForecastService weatherService)
        {
            this.weatherService = weatherService;
        }                
        
        public bool HasData { get => Forecast != null; }
        public WeatherForecast[] Forecast { get; private set; } = null;
        public async Task Initialize()
        {
            if (Forecast == null)
            {
                Forecast = await weatherService.GetForecastAsync(DateTime.Now);
                TriggerStateChange();
            }
        }

        public string ButtonText { get => ShowCelcius ? "Show in Fahrenheit" : "Show in Celcius"; }
        public ButtonRenderStyle ButtonStyle { get => ShowCelcius ? ButtonRenderStyle.Secondary: ButtonRenderStyle.Light; }
        public bool ShowFahrenheit {  get => !ShowCelcius; }
        public bool ShowCelcius { get; private set; }
        public void ToggleTemperatureScale()
        {
            ShowCelcius = !ShowCelcius;
            TriggerStateChange();
        }

        public event Action? StateChanged;

        protected void TriggerStateChange()
        {
            StateChanged?.Invoke();
        }
    }
}
