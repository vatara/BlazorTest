using Microsoft.AspNetCore.Blazor.Browser.Interop;
using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Threading;

namespace BlazorTest.Client.Display.Elements
{
    public class DateTime : BlazorComponent
    {
        [Parameter]
        string Format { get; set; } = "HH:mm MM/dd/yyyy";

        protected string currentDateTime;

        public static TimeSpan GetTimezoneOffset()
        {
            return new TimeSpan(0, RegisteredFunction.Invoke<int>("getTimezoneOffset"), 0);
        }

        void SetDateTime()
        {
            currentDateTime = System.DateTime.Now.Subtract(GetTimezoneOffset()).ToString(Format);
        }

        protected override void OnInit()
        {
            SetDateTime();

            var timer = new Timer(new TimerCallback(_ =>
            {
                SetDateTime();

                // Note that the following line is necessary because otherwise
                // Blazor would not recognize the state change and not refresh the UI
                StateHasChanged();
            }), null, 500, 500);
        }
    }
}
