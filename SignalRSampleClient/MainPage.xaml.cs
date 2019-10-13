using Microsoft.AspNet.SignalR.Client;
using Windows.UI.Xaml.Controls;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace SignalRSampleClient
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private HubConnection _connection;
        delegate void SomeDelegate(string a);


        public MainPage()
        {
            this.InitializeComponent();
            HubConnection connection = new HubConnection("http://localhost:8080/device");
            connection.Closed += Connection_Closed;

            connection.StateChanged += (state) =>
            {
                if (state.NewState == ConnectionState.Connected)
                {
                    _connection = connection;
                }
            };
            var proxy = connection.CreateHubProxy("Sample");

            proxy.On<string, string>("HelloWorld", (user, message) =>
            {
                System.Diagnostics.Debug.WriteLine("=====あああああああああああ=====");
                System.Diagnostics.Debug.WriteLine(user);
                System.Diagnostics.Debug.WriteLine(message);
            });

            connection.Start().Wait();
            proxy.Invoke("CallSample").Wait();
            proxy.Invoke("CallSampleTwo", "引数のデータだよ").Wait();
            proxy.Invoke("CallSampleThree").Wait();
        }

        private void Connection_Closed()
        {
            System.Diagnostics.Debug.WriteLine("=====Connection Closed=====");
        }
    }
}
