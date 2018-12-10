namespace ItIsPizzaDay.Client.Services.Statics
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Blazor;
    using Microsoft.JSInterop;

    public static class Interop
    {
        public static class Dom
        {
            public static Task ScrollToBottom(ElementRef element)
                => JSRuntime.Current.InvokeAsync<object>("interop.dom.scrollToBottom", element);

            public static Task<string> GetValue(ElementRef element)
                => JSRuntime.Current.InvokeAsync<string>("interop.dom.getValue", element);
        }
    }
}