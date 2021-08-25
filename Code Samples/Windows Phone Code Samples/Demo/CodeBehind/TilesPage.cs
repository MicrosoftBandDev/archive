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
using Microsoft.Band.Notifications;
using Microsoft.Band.Tiles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;

namespace BandSdkSample.Pages
{
    public partial class TilesPage
    {
        private TilesModel model;

        public TilesPage()
        {
            this.InitializeComponent();

            this.model = new TilesModel();
            this.DataContext = this.model;

            model.Main.PropertyChanged += Main_PropertyChanged;
        }

        private async void Main_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "BandClient" :
                    if (model.Main.BandClient != null)
                    {
                        using (new DisposableAction(() => model.TileManagerBusy = true, () => model.TileManagerBusy = false))
                        {
                            model.AvailableSpace = await model.Main.BandClient.TileManager.GetRemainingTileCapacityAsync();
                        }
                    }
                    else
                    {
                        model.AvailableSpace = null;
                        model.Tiles = null;
                    }

                    break;
    
            }
        }

        private async void GetTiles_Click(object sender, RoutedEventArgs e)
        {
            using (new DisposableAction(() => model.TileManagerBusy = true, () => model.TileManagerBusy = false))
            {
                await GetTilesHelper();
                model.AvailableSpace = await model.Main.BandClient.TileManager.GetRemainingTileCapacityAsync();
            }
        }

        private async void AddTile_Click(object sender, RoutedEventArgs e)
        {
            BandTile newTile;

            newTile = new BandTile(Guid.NewGuid())
            {
                Name = model.NewTileName,
                IsBadgingEnabled = NewTileBadging.IsChecked.GetValueOrDefault(),
                TileIcon = (await GetImageFromFile("ms-appx:///Assets/SampleTileIconLarge.png")).ToBandIcon(),
                SmallIcon = (await GetImageFromFile("ms-appx:///Assets/SampleTileIconSmall.png")).ToBandIcon()
            };

            using (new DisposableAction(() => model.TileManagerBusy = true, () => model.TileManagerBusy = false))
            {
                await model.Main.BandClient.TileManager.AddTileAsync(newTile);
                model.AvailableSpace = await model.Main.BandClient.TileManager.GetRemainingTileCapacityAsync();
                await GetTilesHelper();
            }
        }

        private async void RemoveTile_Click(object sender, RoutedEventArgs e)
        {
            using (new DisposableAction(() => model.TileManagerBusy = true, () => model.TileManagerBusy = false))
            {
                await model.Main.BandClient.TileManager.RemoveTileAsync(model.SelectedTile.Source);
                model.Tiles.Remove(model.SelectedTile);
                model.AvailableSpace = await model.Main.BandClient.TileManager.GetRemainingTileCapacityAsync();
            }
        }

        private async void SendMessage_Click(object sender, RoutedEventArgs e)
        {
            using (new DisposableAction(() => model.TileManagerBusy = true, () => model.TileManagerBusy = false))
            {
                await model.Main.BandClient.NotificationManager.SendMessageAsync(
                    model.SelectedTile.TileId,
                    model.MessageTitle, model.MessageBody,
                    DateTimeOffset.UtcNow,
                    WithDialog.IsChecked.GetValueOrDefault() ? MessageFlags.ShowDialog : MessageFlags.None
                );
            }
        }

        private async void ShowDialog_Click(object sender, RoutedEventArgs e)
        {
            using (new DisposableAction(() => model.TileManagerBusy = true, () => model.TileManagerBusy = false))
            {
                await model.Main.BandClient.NotificationManager.ShowDialogAsync(model.SelectedTile.TileId, model.MessageTitle, model.MessageBody);
            }
        }

        public async Task GetTilesHelper()
        {
            IEnumerable<BandTile> tiles;
            ObservableBandTile[] tileProxies;
            int i = 0;
            Guid selected = Guid.Empty;

            if (model.SelectedTile != null)
            {
                selected = model.SelectedTile.TileId;
            } 
 
            tiles = (await model.Main.BandClient.TileManager.GetTilesAsync());

            tileProxies = new ObservableBandTile[tiles.Count()];

            foreach (BandTile tile in tiles)
            {
                tileProxies[i++] = new ObservableBandTile(tile);
            }

            model.Tiles = new ObservableCollection<ObservableBandTile>(tileProxies);

            if (selected != Guid.Empty)
            {
                model.SelectedTile = model.Tiles.SingleOrDefault((tile) => { return tile.TileId == selected; });
            }
        }

        private async Task<WriteableBitmap> GetImageFromFile(string Uri)
        {
            StorageFile imageFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri(Uri));

            using (IRandomAccessStream fileStream = await imageFile.OpenAsync(FileAccessMode.Read))
            {
                WriteableBitmap bitmap = new WriteableBitmap(1, 1);

                await bitmap.SetSourceAsync(fileStream);

                return bitmap;
            }
        }
    }
}
