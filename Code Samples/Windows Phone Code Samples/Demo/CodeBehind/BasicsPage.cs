/*
    Copyright (c) Microsoft Corporation All rights reserved.  
 
    MIT License: 
 
    Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
    documentation files (the  "Software"), to deal in the Software without restriction, including without limitation
    the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software,
    and to permit persons to whom the Software is furnished to do so, subject to the following conditions: 
 
    The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software. 
 
    THE SOFTWARE IS PROVIDED ""AS IS"", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
    TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
    THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
    TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

using BandSdkSample.Model;
using Microsoft.Band;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;

namespace BandSdkSample.Pages
{
    public partial class BasicsPage
    {
        private BasicsModel model;

        public BasicsPage()
        {
            this.InitializeComponent();

            this.model = new BasicsModel();
            this.DataContext = this.model;

            App.Current.Resuming += App_Resuming;

            var t = this.FindDevicesAsync();
        }

        private async void ConnectDisconnect_Click(object sender, RoutedEventArgs e)
        {
            if (model.Main.BandClient == null)
            {
                using (new DisposableAction(() => model.Connecting = true, () => model.Connecting = false))
                {
                    try
                    {
                        // This method will throw an exception upon failure for a veriety of reasons,
                        // such as Band out of range or turned off.
                        model.Main.BandClient = await BandClientManager.Instance.ConnectAsync(model.SelectedDevice);
                    }
                    catch (Exception ex)
                    {
                        var t = new MessageDialog(ex.Message, "Failed to Connect").ShowAsync();
                    }
                }
            }
            else
            {
                model.Main.DisconnectDevice();
            }
        }

        private async void GetFWVersion_Click(object sender, RoutedEventArgs e)
        {
            FwVersion.Text = await model.Main.BandClient.GetFirmwareVersionAsync();
        }

        private async void GetHWVersion_Click(object sender, RoutedEventArgs e)
        {
            HwVersion.Text = await model.Main.BandClient.GetHardwareVersionAsync();
        }

        private async void Vibrate_Click(object sender, RoutedEventArgs e)
        {
            await model.Main.BandClient.NotificationManager.VibrateAsync(model.SelectedVibration);
        }

        private async Task FindDevicesAsync()
        {
            IBandInfo selected = model.SelectedDevice;                        

            IBandInfo[] bands = await BandClientManager.Instance.GetBandsAsync();

            model.Devices = new ObservableCollection<IBandInfo>(bands);

            if (selected != null)
            {
                model.SelectedDevice =model.Devices.SingleOrDefault((i) => { return (i.Name == selected.Name); } );
            }
            else if (model.Devices.Count > 0)
            {
                model.SelectedDevice = model.Devices[0];
            }
        }

        async void App_Resuming(object sender, object e)
        {
            await this.FindDevicesAsync();
        }
    }
}
