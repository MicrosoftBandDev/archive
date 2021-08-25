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

using Microsoft.Band;
using System.Collections.ObjectModel;
using Windows.UI;

namespace BandSdkSample.Model
{
    public class MainModel : ModelBase
    {
        #region Static Properties
        private static MainModel instance;
        public static MainModel Instance
        {
            get { return instance ?? (instance = new MainModel()); }
        }
        #endregion

        #region Constructor
        public MainModel()
        {
            App.Current.Suspending += AppSuspending;
        }
        #endregion

        #region Band SDK
        public void DisconnectDevice()
        {
            if (this.bandClient != null)
            {
                this.bandClient.Dispose();
                this.BandClient = null;
            }
        }
 
        void AppSuspending(object sender, Windows.ApplicationModel.SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            DisconnectDevice();

            deferral.Complete();
        }
        #endregion

        #region Properties
        public MainModel Self
        {
            get { return this; }
        }

        private IBandClient bandClient;
        public IBandClient BandClient
        {
            get { return bandClient; }
            set
            {
                if (Set("BandClient", ref bandClient, value, true))
                {
                    if (BandClient == null)
                    {
                        this.BandThemeBaseColor = null;
                    }
                }
            }
        }

        private ObservableCollection<IBandInfo> foundDevices;
        public ObservableCollection<IBandInfo> FoundDevices
        {
            get { return foundDevices; }
            set { Set("FoundDevices", ref foundDevices, value); }
        }

        private string connectionStatus;
        public string ConnectionStatus
        {
            get { return connectionStatus; }
            set { Set("ConnectionStatus", ref connectionStatus, value); }
        }

        private string findDevicesText = "Refresh Devices";
        public string FindDevicesText
        {
            get { return findDevicesText; }
            set { Set("FindDevicesText", ref findDevicesText, value); }
        }

        private string deviceIdString;
        public string DeviceIdString
        {
            get { return deviceIdString; }
            set { Set("DeviceIdString", ref deviceIdString, value); }
        }

        private Color? bandThemeBaseColor;
        public Color? BandThemeBaseColor
        {
            get { return bandThemeBaseColor; }
            set { Set("BandThemeBaseColor", ref bandThemeBaseColor, value); }
        }
        #endregion
   }
}