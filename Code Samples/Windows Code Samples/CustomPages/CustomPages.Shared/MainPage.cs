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
using Microsoft.Band.Tiles;
using Microsoft.Band.Tiles.Pages;
using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;

namespace CustomPages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    partial class MainPage
    {
        private App viewModel;

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            this.viewModel.StatusMessage = "Running ...";

            try
            {
                // Get the list of Microsoft Bands paired to the phone/tablet/PC.
                IBandInfo[] pairedBands = await BandClientManager.Instance.GetBandsAsync();
                if (pairedBands.Length < 1)
                {
                    this.viewModel.StatusMessage = "This sample app requires a Microsoft Band paired to your device. Also make sure that you have the latest firmware installed on your Band, as provided by the latest Microsoft Health app.";
                    return;
                }

                // Connect to Microsoft Band.
                using (IBandClient bandClient = await BandClientManager.Instance.ConnectAsync(pairedBands[0]))
                {
                    // We'll create a Tile that looks like this:
                    // +--------------------+
                    // | MY CARD            | 
                    // | |||||||||||||||||  | 
                    // | 123456789          |
                    // +--------------------+
                    
                    // First, we'll prepare the layout for the Tile page described above.
                    TextBlock myCardTextBlock = new TextBlock()
                    {
                        Color = Colors.Blue.ToBandColor(),
                        ElementId = 1, // the Id of the TextBlock element; we'll use it later to set its text to "MY CARD"
                        Rect = new PageRect(0, 0, 200, 30)
                    };
                    Barcode barcode = new Barcode(BarcodeType.Code39)
                    {
                        ElementId = 2, // the Id of the Barcode element; we'll use it later to set its barcode value to be rendered
                        Rect = new PageRect(0, 0, 250, 50)
                    };
                    TextBlock digitsTextBlock = new TextBlock()
                    {
                        ElementId = 3, // the Id of the TextBlock element; we'll use it later to set its text to "123456789"
                        Rect = new PageRect(0, 0, 200, 30)
                    };
                    FlowPanel panel = new FlowPanel(myCardTextBlock, barcode, digitsTextBlock)
                    {
                        Orientation = FlowPanelOrientation.Vertical,
                        Rect = new PageRect(0, 0, 250, 110)
                    };

                    // Now we'll create the Tile.
                    // WARNING! This tile guid is only an example. Please do not copy it to your test application;
                    // always create a unique guid for each application.
                    // If one application installs its tile, a second application using the same guid will fail to install
                    // its tile due to a guid conflict. In the event of such a failure, the text of the exception will not
                    // report that the tile with the same guid already exists on the band.
                    // There might be other unexpected behavior.
                    Guid myTileId = new Guid("B747BD8A-D98C-4ED3-9378-8C2CF98EF0F0");
                    BandTile myTile = new BandTile(myTileId)
                    {
                        Name = "My Tile",
                        TileIcon = await LoadIcon("ms-appx:///Assets/SampleTileIconLarge.png"),
                        SmallIcon = await LoadIcon("ms-appx:///Assets/SampleTileIconSmall.png")
                    };
                    myTile.PageLayouts.Add(new PageLayout(panel));

                    // Remove the Tile from the Band, if present. An application won't need to do this everytime it runs. 
                    // But in case you modify this sample code and run it again, let's make sure to start fresh.
                    await bandClient.TileManager.RemoveTileAsync(myTile.TileId);

                    // Create the Tile on the Band.
                    await bandClient.TileManager.AddTileAsync(myTile);

                    // And create the page with the specified texts and values.
                    PageData page = new PageData(
                        Guid.NewGuid(), // the Id for the page
                        0, // the index of the layout to be used; we have only one layout in this sample app, but up to 5 layouts can be registered for a Tile
                        new TextBlockData(myCardTextBlock.ElementId.Value, "MY CARD"),
                        new BarcodeData(barcode.BarcodeType, barcode.ElementId.Value, "123456789"),
                        new TextBlockData(digitsTextBlock.ElementId.Value, "123456789"));

                    await bandClient.TileManager.SetPagesAsync(myTile.TileId, page);

                    this.viewModel.StatusMessage = "Done. Check the Tile on your Band (it's the last Tile).";
                }
            }
            catch (Exception ex)
            {
                this.viewModel.StatusMessage = ex.ToString();
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
    }
}
