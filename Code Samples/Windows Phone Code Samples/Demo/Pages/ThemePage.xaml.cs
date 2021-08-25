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

using System;
using Windows.ApplicationModel.Activation;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace BandSdkSample.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ThemePage : Page
    {
        private FileOpenPicker meTileImagePicker = null;

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void FindImage_Click(object sender, RoutedEventArgs e)
        {
            if (meTileImagePicker == null)
            {
                meTileImagePicker = new FileOpenPicker
                {
                    SuggestedStartLocation = PickerLocationId.PicturesLibrary,
                    ViewMode = PickerViewMode.Thumbnail
                };

                meTileImagePicker.FileTypeFilter.Add(".jpg");
                meTileImagePicker.FileTypeFilter.Add(".jpeg");
                meTileImagePicker.FileTypeFilter.Add(".png");
            }

            App.Current.Activated += AppActivated;

            model.FindingMeTileImage = true;

            meTileImagePicker.PickSingleFileAndContinue();
        }

        async void AppActivated(IActivatedEventArgs e)
        {
            App.Current.Activated -= AppActivated;

            if (!(e is FileOpenPickerContinuationEventArgs))
            {
                model.FindingMeTileImage = false;

                return;
            }

            using (new DisposableAction(() => model.FindingMeTileImage = false))
            {
                FileOpenPickerContinuationEventArgs args = e as FileOpenPickerContinuationEventArgs;

                if (args.Files.Count != 1)
                {
                    return;
                }

                bool sizeError = false;

                try
                {
                    await GetMeTileImageFromFile(args.Files[0]);
                }
                catch (MeTileImageSizeException)
                {
                    sizeError = true;
                }

                if (sizeError)
                {
                    await new MessageDialog("Me Tile Image must be 310x102 pixels.").ShowAsync();
                }
            }
        }
    }
}
    