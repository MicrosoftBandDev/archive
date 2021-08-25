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
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;

namespace TileEvents
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    partial class MainPage
    {
        private App viewModel;
        private ButtonKind buttonKind;
        private bool handlingClick;
        
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (handlingClick)
            {
                return;
            }

            this.viewModel.StatusMessage = "Running ...";

            handlingClick = true;
            try
            {
                buttonKind = ButtonKindFromModel();

                // Get the list of Microsoft Bands paired to the phone.
                IBandInfo[] pairedBands = await BandClientManager.Instance.GetBandsAsync();
                if (pairedBands.Length < 1)
                {
                    this.viewModel.StatusMessage = "This sample app requires a Microsoft Band paired to your device. Also make sure that you have the latest firmware installed on your Band, as provided by the latest Microsoft Health app.";
                    return;
                }

                // Connect to Microsoft Band.
                using (IBandClient bandClient = await BandClientManager.Instance.ConnectAsync(pairedBands[0]))
                {
                    // Create a Tile with a TextButton on it.
                    // WARNING! This tile guid is only an example. Please do not copy it to your test application;
                    // always create a unique guid for each application.
                    // If one application installs its tile, a second application using the same guid will fail to install
                    // its tile due to a guid conflict. In the event of such a failure, the text of the exception will not
                    // report that the tile with the same guid already exists on the band.
                    // There might be other unexpected behavior.
                    Guid myTileId = new Guid("497B746E-4F5F-44D4-96E2-FC46D407B6E3");
                    BandTile myTile = new BandTile(myTileId)
                    {
                        Name = "My Tile",
                        TileIcon = await LoadIcon("ms-appx:///Assets/SampleTileIconLarge.png"),
                        SmallIcon = await LoadIcon("ms-appx:///Assets/SampleTileIconSmall.png")
                    };

                    await BuildLayout(myTile);

                    // Remove the Tile from the Band, if present. An application won't need to do this everytime it runs. 
                    // But in case you modify this sample code and run it again, let's make sure to start fresh.
                    await bandClient.TileManager.RemoveTileAsync(myTileId);
                    
                    // Create the Tile on the Band.
                    await bandClient.TileManager.AddTileAsync(myTile);
                    await bandClient.TileManager.SetPagesAsync(myTileId, new PageData(new Guid("5F5FD06E-BD37-4B71-B36C-3ED9D721F200"), 0, GetPageElementData()));

                    // Subscribe to Tile events.
                    int buttonPressedCount = 0;
                    TaskCompletionSource<bool> closePressed = new TaskCompletionSource<bool>();

                    bandClient.TileManager.TileButtonPressed += (s, args) => 
                    {
                        Dispatcher.RunAsync(
                            CoreDispatcherPriority.Normal,
                            () =>
                            {
                                buttonPressedCount++;
                                this.viewModel.StatusMessage = string.Format("TileButtonPressed = {0}", buttonPressedCount);
                            }
                        );
                    };
                    bandClient.TileManager.TileClosed += (s, args) => 
                    {
                        closePressed.TrySetResult(true);
                    };

                    await bandClient.TileManager.StartReadingsAsync();

                    // Receive events until the Tile is closed.
                    this.viewModel.StatusMessage = "Check the Tile on your Band (it's the last Tile). Waiting for events ...";

                    await closePressed.Task;
                    
                    // Stop listening for Tile events.
                    await bandClient.TileManager.StopReadingsAsync();

                    this.viewModel.StatusMessage = "Done.";
                }
            }
            catch (Exception ex)
            {
                this.viewModel.StatusMessage = ex.ToString();
            }
            finally
            {
                handlingClick = false;
            }
        }

        private async Task BuildLayout(BandTile myTile)
        {
            FilledPanel panel = new FilledPanel() { Rect = new PageRect(0, 0, 220, 150) };

            PageElement buttonElement = null;

            switch (buttonKind)
            {
                case ButtonKind.Text:
                    buttonElement = new TextButton();
                    break;

                case ButtonKind.Filled:
                    buttonElement = new FilledButton() { BackgroundColor = new BandColor(0, 128, 0) };
                    break;

                case ButtonKind.Icon:
                    buttonElement = new IconButton();
                    myTile.AdditionalIcons.Add(await LoadIcon("ms-appx:///Assets/Smile.png"));
                    myTile.AdditionalIcons.Add(await LoadIcon("ms-appx:///Assets/SmileUpsideDown.png"));
                    break;

                default:
                    throw new NotImplementedException();
            }

            buttonElement.ElementId = 1;
            buttonElement.Rect = new PageRect(10, 10, 200, 90);

            panel.Elements.Add(buttonElement);

            myTile.PageLayouts.Add(new PageLayout(panel));
        }
        
        private PageElementData GetPageElementData()
        {
            switch (buttonKind)
            {
                case ButtonKind.Text:
                    return new TextButtonData(1, "Click here");

                case ButtonKind.Filled:
                    return new FilledButtonData(1, new BandColor(128, 0, 0));

                case ButtonKind.Icon:
                    return new IconButtonData(
                        elementId: 1,
                        iconIndex: 2, // The first 2 indexes are taken by the tile icons.
                        pressedIconIndex: 3, // The first 2 indexes are taken by the tile icons.
                        iconColor: new BandColor(0, 128, 0),
                        pressedIconColor: new BandColor(128, 0, 0),
                        backgroundColor: new BandColor(0x35, 0x35, 0x35),
                        pressedBackgroundColor: new BandColor(0x20, 0x20, 0x20));

                default:
                    throw new NotImplementedException();
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

        ButtonKind ButtonKindFromModel()
        {
            if (this.viewModel.UseTextButton)
            {
                return ButtonKind.Text;
            }
            else if (this.viewModel.UseFilledButton)
            {
                return ButtonKind.Filled;
            }
            else if (this.viewModel.UseIconButton)
            {
                return ButtonKind.Icon;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private enum ButtonKind
        {
            Text,
            Filled,
            Icon
        }

    }
}
