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
using Microsoft.Band.Notifications;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace BandSdkSample.Model
{
    public class BasicsModel : PageModelBase
    {
        public BasicsModel Self
        {
            get { return this; }
        }

        private IBandInfo selectedDevice;
        public IBandInfo SelectedDevice
        {
            get { return this.selectedDevice; }
            set { Set("SelectedDevice", ref selectedDevice, value, true); }
        }

        private ObservableCollection<IBandInfo> devices;
        public ObservableCollection<IBandInfo> Devices
        {
            get { return devices; }
            set { Set("Devices", ref devices, value); }
        }

        private bool connecting;
        public bool Connecting
        {
            get { return connecting; }
            set { Set("Connecting", ref connecting, value, true); }
        }

        private VibrationType[] vibrationList;
        public VibrationType[] VibrationList
        {
            get { return vibrationList ?? (vibrationList = Enum.GetValues(typeof(VibrationType)).OfType<VibrationType>().ToArray()); }
        }

        private VibrationType selectedVibration;
        public VibrationType SelectedVibration
        {
            get { return selectedVibration; }
            set { Set("SelectedVibration", ref selectedVibration, value); }
        }
    }
}
