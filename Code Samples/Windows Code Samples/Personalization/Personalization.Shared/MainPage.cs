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
using Microsoft.Band.Personalization;
using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Personalization
{
    public sealed partial class MainPage : Page
    {
        private App viewModel;

        private async void SetGetMeTileImage_Click(object sender, RoutedEventArgs args)
        {
            IBandInfo[] pairedBands;
            IBandClient client = null;

            this.viewModel.StatusMessage = "Running Me Tile image demo ...";

            // Enumerate Bands that are paired to this device.
            pairedBands = await BandClientManager.Instance.GetBandsAsync();

            if (pairedBands.Length == 0)
            {
                this.viewModel.StatusMessage = "This sample app requires a Microsoft Band paired to your device. Also make sure that you have the latest firmware installed on your Band, as provided by the latest Microsoft Health app.";
                return;
            }

            try
            {
                // Connect to the first Band in the enumeration.
                client = await BandClientManager.Instance.ConnectAsync(pairedBands[0]);
            }
            catch (Exception e)
            {
                this.viewModel.StatusMessage = "Failed to connect to a Band.\r\n" + e.Message;
                return;
            }

            using (client)
            {
                // Get the custom image from storage, and convert it to a BandImage.
                WriteableBitmap meTileBitmap = await LoadImage("ms-appx:///Assets/SampleMeTileImage.jpg");
                BandImage meTileImage = meTileBitmap.ToBandImage();

                // Set the MeTile image on the band.
                await client.PersonalizationManager.SetMeTileImageAsync(meTileImage);

                // Read the MeTile image back from the band.
                meTileImage = await client.PersonalizationManager.GetMeTileImageAsync();

                // Convert the image back to a WritableBitmap.
                meTileBitmap = meTileImage.ToWriteableBitmap();

                // Display the image on screen.
                this.MeTileImage.Source = meTileBitmap;

                // To clear the MeTile image on the Band, set it to null.
                // await client.PersonalizationManager.SetMeTileImageAsync(null);
            }

            this.viewModel.StatusMessage = "Done.";
        }

        private async void SetGetTheme_Click(object sender, RoutedEventArgs args)
        {
            IBandInfo[] pairedBands;
            IBandClient client = null;

            this.viewModel.StatusMessage = "Running Theme demo ...";

            // Enumerate Bands that are paired to this device.
            pairedBands = await BandClientManager.Instance.GetBandsAsync();

            if (pairedBands.Length == 0)
            {
                this.viewModel.StatusMessage = "This sample app requires a Microsoft Band paired to your device. Also make sure that you have the latest firmware installed on your Band, as provided by the latest Microsoft Health app.";
                return;
            }

            try
            {
                // Connect to the first Band in the enumeration.
                client = await BandClientManager.Instance.ConnectAsync(pairedBands[0]);
            }
            catch (Exception e)
            {
                this.viewModel.StatusMessage = "Failed to connect to a Band.\r\n" + e.Message;
                return;
            }

            using (client)
            {
                // Set up a custom theme.
                BandTheme theme = new BandTheme
                {
                    Base          = Colors.BurlyWood.ToBandColor(),
                    Highlight     = Colors.BlanchedAlmond.ToBandColor(),
                    Lowlight      = Colors.Azure.ToBandColor(),
                    SecondaryText = Colors.DarkGreen.ToBandColor(),
                    HighContrast  = Colors.LightGreen.ToBandColor(),
                    Muted         = Colors.Purple.ToBandColor()
                };

                // Set the theme on the band.
                await client.PersonalizationManager.SetThemeAsync(theme);

                // Get the theme from the band.
                theme = await client.PersonalizationManager.GetThemeAsync();

                // Display the theme on screen.
                ThemeColor_Base.Background          = new SolidColorBrush(theme.Base.ToColor());
                ThemeColor_Highlight.Background     = new SolidColorBrush(theme.Highlight.ToColor());
                ThemeColor_Lowlight.Background      = new SolidColorBrush(theme.Lowlight.ToColor());
                ThemeColor_SecondaryText.Background = new SolidColorBrush(theme.SecondaryText.ToColor());
                ThemeColor_HighContrast.Background  = new SolidColorBrush(theme.HighContrast.ToColor());
                ThemeColor_Muted.Background         = new SolidColorBrush(theme.Muted.ToColor());
            }

            this.viewModel.StatusMessage = "Done.";
        }

        private async Task<WriteableBitmap> LoadImage(string uri)
        {
            StorageFile imageFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri(uri));

            using (IRandomAccessStream fileStream = await imageFile.OpenAsync(FileAccessMode.Read))
            {
                WriteableBitmap bitmap = new WriteableBitmap(1, 1);

                await bitmap.SetSourceAsync(fileStream);

                return bitmap;
            }
        }
    }
}
