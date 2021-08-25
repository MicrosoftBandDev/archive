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
using BandSdkSample.ObservableObjects;
using Microsoft.Band;
using Microsoft.Band.Personalization;
using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;

namespace BandSdkSample.Pages
{
    public partial class ThemePage
    {
        private ThemeModel model;

        public ThemePage()
        {
            this.InitializeComponent();

            this.model = new ThemeModel();
            this.DataContext = this.model;
        }

        private async void GetImage_Click(object sender, RoutedEventArgs e)
        {
            BandImage image= null;

            try
            {
                using (new DisposableAction(() => model.PersonalizationManagerBusy = true, () => model.PersonalizationManagerBusy = false))
                {
                    image = await model.Main.BandClient.PersonalizationManager.GetMeTileImageAsync();
                }
            }
            catch
            { }

            if (image != null)
            {
                model.MeTileImage = image.ToWriteableBitmap();
            }
            else
            {
                model.MeTileImage = null;
            }
        }

        private async void Sample_OnClick(object sender, RoutedEventArgs e)
        {
            using (new DisposableAction(() => model.FindingMeTileImage = true, () => model.FindingMeTileImage = false))
            {
                await GetMeTileImageFromFile(
                    await StorageFile.GetFileFromApplicationUriAsync(
                        new Uri("ms-appx:///Assets/SampleMeTileImage.jpg")
                    )
                );
            }
        }

        public async Task GetMeTileImageFromFile(StorageFile imageFile)
        {
            WriteableBitmap bitmap = new WriteableBitmap(310, 102);

            using (IRandomAccessStream fileStream = await imageFile.OpenAsync(FileAccessMode.Read))
            {
                await bitmap.SetSourceAsync(fileStream);
            }

            if (bitmap.PixelWidth != 310 || bitmap.PixelHeight != 102)
            {
                throw new MeTileImageSizeException();
            }

            model.MeTileImage = bitmap;
        }

        private async void SetImage_Click(object sender, RoutedEventArgs e)
        {
            BandImage image;

            image = model.MeTileImage.ToBandImage();

            using (new DisposableAction(() => model.PersonalizationManagerBusy = true, () => model.PersonalizationManagerBusy = false))
            {
                await model.Main.BandClient.PersonalizationManager.SetMeTileImageAsync(image);
            }
        }

        private async void ClearImage_OnClick(object sender, RoutedEventArgs e)
        {
            using (new DisposableAction(() => model.PersonalizationManagerBusy = true, () => model.PersonalizationManagerBusy = false))
            {
                await model.Main.BandClient.PersonalizationManager.SetMeTileImageAsync(null);
            }

            model.MeTileImage = null;
        }

        private async void GetTheme_Click(object sender, RoutedEventArgs e)
        {
            using (new DisposableAction(() => model.PersonalizationManagerBusy = true, () => model.PersonalizationManagerBusy = false))
            {
                model.Theme = new ObservableBandTheme(await model.Main.BandClient.PersonalizationManager.GetThemeAsync());
            }

            model.Main.BandThemeBaseColor = model.Theme.Base;
        }

        private async void SetTheme_Click(object sender, RoutedEventArgs e)
        {
            using (new DisposableAction(() => model.PersonalizationManagerBusy = true, () => model.PersonalizationManagerBusy = false))
            {
                await model.Main.BandClient.PersonalizationManager.SetThemeAsync(model.Theme.Source);
            }

            model.Main.BandThemeBaseColor = model.Theme.Base;
        }

        private async void ClearTheme_Click(object sender, RoutedEventArgs e)
        {
            using (new DisposableAction(() => model.PersonalizationManagerBusy = true, () => model.PersonalizationManagerBusy = false))
            {
                await model.Main.BandClient.PersonalizationManager.SetThemeAsync(null);
            }

            model.Theme = new ObservableBandTheme(await model.Main.BandClient.PersonalizationManager.GetThemeAsync());
        }
    }
}
