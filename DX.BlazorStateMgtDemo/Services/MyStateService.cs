using Microsoft.AspNetCore.Components;

namespace DX.BlazorStateMgtDemo.Services
{
    public class MyStateService
    {
        public bool ShowCelcius { get; private set; }

        public bool SetShowCelcius(ComponentBase sender, bool showCelcius)
        {
            var oldState = ShowCelcius;
            if (showCelcius != ShowCelcius)
            {
                ShowCelcius = showCelcius;
                NotifyShowCelciusChanged(sender, showCelcius);
            }
            return oldState;
        }

        
        public event Action<ComponentBase, bool> ShowCelciusChanged;
        
        private void NotifyShowCelciusChanged(ComponentBase sender, bool ShowCelcius)
        {
             ShowCelciusChanged?.Invoke(sender, ShowCelcius);
        }
    }
}
