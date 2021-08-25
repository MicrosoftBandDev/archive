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
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Band;
using Microsoft.Band.Tiles;
using Microsoft.Band.Tiles.Pages;
using BGTileEventsService.Universal;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BGTileEventsApp.Universal
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.DataContext = App.Current;
        }

        /// <summary>
        /// Called when the "Install Tile" button is pressed in the UI.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void InstallTileButton_Click(object sender, RoutedEventArgs e)
        {
            App.Current.StatusMessage = "Installing...\n";
            try
            {
                // Get the list of Microsoft Bands paired to the phone.
                IBandInfo[] pairedBands = await BandClientManager.Instance.GetBandsAsync();
                if (pairedBands.Length < 1)
                {
                    App.Current.StatusMessage = "This sample app requires a Microsoft Band paired to your device. Also make sure that you have the latest firmware installed on your Band, as provided by the latest Microsoft Health app.";
                    return;
                }

                // Connect to Microsoft Band.
                using (IBandClient bandClient = await BandClientManager.Instance.ConnectAsync(pairedBands[0]))
                {
                    // Create a Tile with a TextButton and WrappedTextBlock on it.
                    BandTile myTile = new BandTile(TileConstants.TileGuid)
                    {
                        Name = "My Tile",
                        TileIcon = await LoadIcon("ms-appx:///Assets/SampleTileIconLarge.png"),
                        SmallIcon = await LoadIcon("ms-appx:///Assets/SampleTileIconSmall.png")
                    };
                    TextButton button = new TextButton() { ElementId = TileConstants.Button1ElementId, Rect = new PageRect(10, 5, 200, 30) };
                    WrappedTextBlock textblock = new WrappedTextBlock() { ElementId = TileConstants.TextElementId, Rect = new PageRect(10, 40, 200, 88) };
                    PageElement[] elements = new PageElement[] { button, textblock };
                    FilledPanel panel = new FilledPanel(elements) { Rect = new PageRect(0, 0, 220, 128) };
                    myTile.PageLayouts.Add(new PageLayout(panel));

                    // Remove the Tile from the Band, if present. An application won't need to do this everytime it runs. 
                    // But in case you modify this sample code and run it again, let's make sure to start fresh.
                    await bandClient.TileManager.RemoveTileAsync(TileConstants.TileGuid);

                    // Create the Tile on the Band.
                    await bandClient.TileManager.AddTileAsync(myTile);
                    PageElementData[] pagedata = new PageElementData[] 
                    {
                        new TextButtonData(TileConstants.Button1ElementId, TileConstants.ButtonLabel),
                        new WrappedTextBlockData(TileConstants.TextElementId, "...")
                    };
                    await bandClient.TileManager.SetPagesAsync(TileConstants.TileGuid, new PageData(TileConstants.Page1Guid, 0, pagedata));

                    // Subscribe to background tile events
                    await bandClient.SubscribeToBackgroundTileEventsAsync(TileConstants.TileGuid);

                    App.Current.StatusMessage = "Installed Tile";
                }
            }
            catch (Exception ex)
            {
                App.Current.StatusMessage = ex.ToString();
            }
        }

        private async Task<BandIcon> LoadIcon(string uri)
        {
            StorageFile imageFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri(uri));

            using (IRandomAccessStream fileStream = await imageFile.OpenAsync(FileAccessMode.Read))
            {
                WriteableBitmap bitmap = new WriteableBitmap(1, 1);
                await bitmap.SetSourceAsync(fileStream);
                return bitmap.ToBandIcon();
            }
        }

        /// <summary>
        /// Called when the "Remove Tile" button is pressed in the UI.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void RemoveTileButton_Click(object sender, RoutedEventArgs e)
        {
            App.Current.StatusMessage = "Removing...\n";
            try
            {
                // Get the list of Microsoft Bands paired to the phone.
                IBandInfo[] pairedBands = await BandClientManager.Instance.GetBandsAsync();
                if (pairedBands.Length < 1)
                {
                    App.Current.StatusMessage = "This sample app requires a Microsoft Band paired to your device. Also make sure that you have the latest firmware installed on your Band, as provided by the latest Microsoft Health app.";
                    return;
                }

                // Connect to Microsoft Band.
                using (IBandClient bandClient = await BandClientManager.Instance.ConnectAsync(pairedBands[0]))
                {
                    // Unsubscribe from background tile events
                    await bandClient.UnsubscribeFromBackgroundTileEventsAsync(TileConstants.TileGuid);

                    // Remove the Tile from the Band, if present
                    await bandClient.TileManager.RemoveTileAsync(TileConstants.TileGuid);

                    App.Current.StatusMessage = "Removed Tile";
                }
            }
            catch (Exception ex)
            {
                App.Current.StatusMessage = ex.ToString();
            }
        }
    }
}
