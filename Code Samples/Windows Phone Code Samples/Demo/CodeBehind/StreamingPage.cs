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
using Microsoft.Band.Sensors;
using System;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace BandSdkSample.Pages
{
    public partial class StreamingPage
    {
        private StreamingModel model;
        private Task updateUI = null;

        public StreamingPage()
        {
            this.InitializeComponent();

            this.model = new StreamingModel();
            this.DataContext = this.model;

            model.Main.PropertyChanged += Main_PropertyChanged;
        }

        private async void AccelToggle_Toggled(object sender, RoutedEventArgs e)
        {
            using (new DisposableAction(() => model.StreamingBusy = true, () => model.StreamingBusy = false))
            {
                if (AccelStreaming.Visibility == Visibility.Collapsed)
                {
                    if (Accel16ms.IsChecked.GetValueOrDefault())
                    {
                        model.Main.BandClient.SensorManager.Accelerometer.ReportingInterval = TimeSpan.FromMilliseconds(16.0);
                    }
                    else if (Accel32ms.IsChecked.GetValueOrDefault())
                    {
                        model.Main.BandClient.SensorManager.Accelerometer.ReportingInterval = TimeSpan.FromMilliseconds(32.0);
                    }
                    else if (Accel128ms.IsChecked.GetValueOrDefault())
                    {
                        model.Main.BandClient.SensorManager.Accelerometer.ReportingInterval = TimeSpan.FromMilliseconds(128.0);
                    }

                    Accel16ms.IsEnabled = false;
                    Accel32ms.IsEnabled = false;
                    Accel128ms.IsEnabled = false;

                    try
                    {
                        //subscribe to Accelerometer
                        model.Main.BandClient.SensorManager.Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
                        await model.Main.BandClient.SensorManager.Accelerometer.StartReadingsAsync();
                    }
                    catch
                    {
                        Accel16ms.IsEnabled = true;
                        Accel32ms.IsEnabled = true;
                        Accel128ms.IsEnabled = true;

                        throw;
                    }

                    AccelStreaming.Visibility = Visibility.Visible;
                }
                else
                {
                    //unsubscribe to Accelerometer
                    model.Main.BandClient.SensorManager.Accelerometer.ReadingChanged -= Accelerometer_ReadingChanged;
                    await model.Main.BandClient.SensorManager.Accelerometer.StopReadingsAsync();

                    Accel16ms.IsEnabled = true;
                    Accel32ms.IsEnabled = true;
                    Accel128ms.IsEnabled = true;

                    AccelStreaming.Visibility = Visibility.Collapsed;
                }
            }
        }

        void Accelerometer_ReadingChanged(object sender, BandSensorReadingEventArgs<IBandAccelerometerReading> e)
        {
            if (updateUI == null || updateUI.IsCompleted)
            {
                updateUI = App.Current.InvokeOnUIThread(
                    () =>
                    {
                        model.Accel_Ax = e.SensorReading.AccelerationX;
                        model.Accel_Ay = e.SensorReading.AccelerationY;
                        model.Accel_Az = e.SensorReading.AccelerationZ;
                        model.AccelTimestamp = e.SensorReading.Timestamp;
                    }
                ); 
            }
        }

        private async void AccelGyro_Toggled(object sender, RoutedEventArgs e)
        {
            using (new DisposableAction(() => model.StreamingBusy = true, () => model.StreamingBusy = false))
            {
                if (AccelGyroStreaming.Visibility == Visibility.Collapsed)
                {
                    if (Motion16ms.IsChecked.GetValueOrDefault())
                    {
                        model.Main.BandClient.SensorManager.Gyroscope.ReportingInterval = TimeSpan.FromMilliseconds(16.0);
                    }
                    else if (Motion32ms.IsChecked.GetValueOrDefault())
                    {
                        model.Main.BandClient.SensorManager.Gyroscope.ReportingInterval = TimeSpan.FromMilliseconds(32.0);
                    }
                    else if (Motion128ms.IsChecked.GetValueOrDefault())
                    {
                        model.Main.BandClient.SensorManager.Gyroscope.ReportingInterval = TimeSpan.FromMilliseconds(128.0);
                    }

                    Motion16ms.IsEnabled = false;
                    Motion32ms.IsEnabled = false;
                    Motion128ms.IsEnabled = false;

                    try
                    {
                        //subscribe to Accelerometer/Gyroscope
                        model.Main.BandClient.SensorManager.Gyroscope.ReadingChanged += Gyroscope_ReadingChanged;
                        await model.Main.BandClient.SensorManager.Gyroscope.StartReadingsAsync();
                    }
                    catch
                    {
                        Motion16ms.IsEnabled = true;
                        Motion32ms.IsEnabled = true;
                        Motion128ms.IsEnabled = true;

                        throw;
                    }

                    AccelGyroStreaming.Visibility = Visibility.Visible;
                }
                else
                {
                    //unsubscribe to Accelerometer/Gyroscope
                    model.Main.BandClient.SensorManager.Gyroscope.ReadingChanged -= Gyroscope_ReadingChanged;
                    await model.Main.BandClient.SensorManager.Gyroscope.StopReadingsAsync();

                    Motion16ms.IsEnabled = true;
                    Motion32ms.IsEnabled = true;
                    Motion128ms.IsEnabled = true;

                    AccelGyroStreaming.Visibility = Visibility.Collapsed;
                }
            }
        }

        void Gyroscope_ReadingChanged(object sender, BandSensorReadingEventArgs<IBandGyroscopeReading> e)
        {
            if (updateUI == null || updateUI.IsCompleted)
            {
                updateUI = App.Current.InvokeOnUIThread(
                    () => 
                    {
                        model.Motion_Ax = e.SensorReading.AccelerationX;
                        model.Motion_Ay = e.SensorReading.AccelerationY;
                        model.Motion_Az = e.SensorReading.AccelerationZ;
                        model.Motion_Gx = e.SensorReading.AngularVelocityX;
                        model.Motion_Gy = e.SensorReading.AngularVelocityY;
                        model.Motion_Gz = e.SensorReading.AngularVelocityZ;
                        model.MotionTimestamp = e.SensorReading.Timestamp;
                    }
                );
            }
        }

        private async void DistanceStreaming_Toggled(object sender, RoutedEventArgs e)
        {
            using (new DisposableAction(() => model.StreamingBusy = true, () => model.StreamingBusy = false))
            {
                if (DistanceStreaming.Visibility == Visibility.Collapsed)
                {
                    //subscribe to Distance
                    model.Main.BandClient.SensorManager.Distance.ReadingChanged += Distance_ReadingChanged;
                    await model.Main.BandClient.SensorManager.Distance.StartReadingsAsync();

                    DistanceStreaming.Visibility = Visibility.Visible;
                }
                else
                {
                    //unsubscribe to Distance
                    model.Main.BandClient.SensorManager.Distance.ReadingChanged -= Distance_ReadingChanged;
                    await model.Main.BandClient.SensorManager.Distance.StopReadingsAsync();

                    DistanceStreaming.Visibility = Visibility.Collapsed;
                }
            }
        }

        void Distance_ReadingChanged(object sender, BandSensorReadingEventArgs<IBandDistanceReading> e)
        {
            App.Current.InvokeOnUIThread(
                () =>
                {
                    model.Distance = e.SensorReading.TotalDistance;
                    model.Speed = e.SensorReading.Speed;
                    model.Pace = e.SensorReading.Pace;
                    model.Motion = e.SensorReading.CurrentMotion;
                    model.DistanceTimestamp = e.SensorReading.Timestamp;
                }
            );
        }

        private async void UVStreaming_Toggled(object sender, RoutedEventArgs e)
        {
            using (new DisposableAction(() => model.StreamingBusy = true, () => model.StreamingBusy = false))
            {
                if (UVStreaming.Visibility == Visibility.Collapsed)
                {
                    //subscribe to UV
                    model.Main.BandClient.SensorManager.Ultraviolet.ReadingChanged += Ultraviolet_ReadingChanged;
                    await model.Main.BandClient.SensorManager.Ultraviolet.StartReadingsAsync(new CancellationToken());

                    UVStreaming.Visibility = Visibility.Visible;
                }
                else
                {
                    //unsubscribe to UV
                    model.Main.BandClient.SensorManager.Ultraviolet.ReadingChanged -= Ultraviolet_ReadingChanged;
                    await model.Main.BandClient.SensorManager.Ultraviolet.StopReadingsAsync(new CancellationToken());

                    UVStreaming.Visibility = Visibility.Collapsed;
                }
            }
        }

        void Ultraviolet_ReadingChanged(object sender, BandSensorReadingEventArgs<IBandUltravioletLightReading> e)
        {
            App.Current.InvokeOnUIThread(
                () =>
                {
                    model.UVIndexX10 = e.SensorReading.ExposureLevel;
                    model.UvTimestamp = e.SensorReading.Timestamp;
                }
            );
        }

        private async void SkinTempStreaming_Toggled(object sender, RoutedEventArgs e)
        {
            using (new DisposableAction(() => model.StreamingBusy = true, () => model.StreamingBusy = false))
            {
                if (SkinTempStreaming.Visibility == Visibility.Collapsed)
                {
                    //subscribe to skin temperature
                    model.Main.BandClient.SensorManager.SkinTemperature.ReadingChanged += SkinTemperature_ReadingChanged;
                    await model.Main.BandClient.SensorManager.SkinTemperature.StartReadingsAsync(new CancellationToken());

                    SkinTempStreaming.Visibility = Visibility.Visible;
                }
                else
                {
                    //unsubscribe to skin temperature
                    model.Main.BandClient.SensorManager.SkinTemperature.ReadingChanged -= SkinTemperature_ReadingChanged;
                    await model.Main.BandClient.SensorManager.SkinTemperature.StopReadingsAsync(new CancellationToken());

                    SkinTempStreaming.Visibility = Visibility.Collapsed;
                }
            }
        }

        void SkinTemperature_ReadingChanged(object sender, BandSensorReadingEventArgs<IBandSkinTemperatureReading> e)
        {
            App.Current.InvokeOnUIThread(
                () =>
                {
                    model.SkinTemp = e.SensorReading.Temperature;
                    model.SkinTempTimestamp = e.SensorReading.Timestamp;
                }
            );
        }

        private async void PedometerStreaming_Toggled(object sender, RoutedEventArgs e)
        {
            using (new DisposableAction(() => model.StreamingBusy = true, () => model.StreamingBusy = false))
            {
                if (PedometerStreaming.Visibility == Visibility.Collapsed)
                {
                    //subscribe to Pedometer
                    model.Main.BandClient.SensorManager.Pedometer.ReadingChanged += Pedometer_ReadingChanged;
                    await model.Main.BandClient.SensorManager.Pedometer.StartReadingsAsync(new CancellationToken());

                    PedometerStreaming.Visibility = Visibility.Visible;
                }
                else
                {
                    //unsubscribe to HR
                    model.Main.BandClient.SensorManager.Pedometer.ReadingChanged -= Pedometer_ReadingChanged;
                    await model.Main.BandClient.SensorManager.Pedometer.StopReadingsAsync(new CancellationToken());

                    PedometerStreaming.Visibility = Visibility.Collapsed;
                }
            }
        }

        void Pedometer_ReadingChanged(object sender, BandSensorReadingEventArgs<IBandPedometerReading> e)
        {
            App.Current.InvokeOnUIThread(
            () =>
                {
                    model.Steps = e.SensorReading.TotalSteps;
                    model.PedometerTimestamp = e.SensorReading.Timestamp;
                }
            );
        }

        private async void HRStreaming_Toggled(object sender, RoutedEventArgs e)
        {
            using (new DisposableAction(() => model.StreamingBusy = true, () => model.StreamingBusy = false))
            {
                if (HRStreaming.Visibility == Visibility.Collapsed)
                {
                    //subscribe to HR
                    model.Main.BandClient.SensorManager.HeartRate.ReadingChanged += HeartRate_ReadingChanged;
                    await model.Main.BandClient.SensorManager.HeartRate.StartReadingsAsync(new CancellationToken());

                    HRStreaming.Visibility = Visibility.Visible;
                }
                else
                {
                    //unsubscribe to HR
                    model.Main.BandClient.SensorManager.HeartRate.ReadingChanged -= HeartRate_ReadingChanged;
                    await model.Main.BandClient.SensorManager.HeartRate.StopReadingsAsync(new CancellationToken());

                    HRStreaming.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void HeartRate_ReadingChanged(object sender, BandSensorReadingEventArgs<IBandHeartRateReading> e)
        {
            App.Current.InvokeOnUIThread(
                () =>
                {
                    model.HeartRate = e.SensorReading.HeartRate;
                    model.HRQuality = e.SensorReading.Quality;
                    model.HrTimestamp = e.SensorReading.Timestamp;
                }
            );
        }

        private async void DeviceContactStreaming_Toggled(object sender, RoutedEventArgs e)
        {
            using (new DisposableAction(() => model.StreamingBusy = true, () => model.StreamingBusy = false))
            {
                if (DeviceContactStreaming.Visibility == Visibility.Collapsed)
                {
                    //subscribe to sleep classification
                    model.Main.BandClient.SensorManager.Contact.ReadingChanged += Contact_ReadingChanged;
                    await model.Main.BandClient.SensorManager.Contact.StartReadingsAsync();

                    DeviceContactStreaming.Visibility = Visibility.Visible;
                }
                else
                {
                    //unsubscribe to sleep classification
                    model.Main.BandClient.SensorManager.Contact.ReadingChanged -= Contact_ReadingChanged;
                    await model.Main.BandClient.SensorManager.Contact.StopReadingsAsync();

                    DeviceContactStreaming.Visibility = Visibility.Collapsed;
                }
            }
        }

        void Contact_ReadingChanged(object sender, BandSensorReadingEventArgs<IBandContactReading> e)
        {
            App.Current.InvokeOnUIThread(
                () =>
                {
                    model.WornState = e.SensorReading.State;
                    model.WornStateTimestamp = e.SensorReading.Timestamp;
                }
            );
        }

        private async void GetCurrentState_Clicked(object sender, RoutedEventArgs e)
        {
            using (new DisposableAction(() => model.StreamingBusy = true, () => model.StreamingBusy = false))
            {
                IBandContactReading reading;

                reading = await model.Main.BandClient.SensorManager.Contact.GetCurrentStateAsync();

                CurrentWornState.Text = reading.State.ToString();
            }
        }

        private void Main_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "BandClient" :
                    if (model.Main.BandClient == null)
                    {
                        AccelStreaming.Visibility = Visibility.Collapsed;
                        AccelGyroStreaming.Visibility = Visibility.Collapsed;
                        DistanceStreaming.Visibility = Visibility.Collapsed;
                        HRStreaming.Visibility = Visibility.Collapsed;
                        PedometerStreaming.Visibility = Visibility.Collapsed;
                        SkinTempStreaming.Visibility = Visibility.Collapsed;
                        UVStreaming.Visibility = Visibility.Collapsed;
                        DeviceContactStreaming.Visibility = Visibility.Collapsed;
                        CurrentWornState.Visibility = Visibility.Collapsed;
                    }

                    break;
    
            }
        }
    }
}
