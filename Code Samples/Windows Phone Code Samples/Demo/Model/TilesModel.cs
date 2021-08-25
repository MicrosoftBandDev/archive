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

using BandSdkSample.ObservableObjects;
using System.Collections.ObjectModel;

namespace BandSdkSample.Model
{
    public class TilesModel : PageModelBase
    {
        public TilesModel Self
        {
            get { return this; }
        }

        private int? availableSpace;
        public int? AvailableSpace
        {
            get { return availableSpace; }
            set { Set("AvailableSpace", ref availableSpace, value, true); }
        }

        private string newTileName;
        public string NewTileName
        {
            get { return newTileName; }
            set { Set("NewTileName", ref newTileName, value, true); }
        }

        private ObservableCollection<ObservableBandTile> tiles;
        public ObservableCollection<ObservableBandTile> Tiles
        {
            get { return tiles; }
            set {Set("Tiles", ref tiles, value, true); }
        }

        private ObservableBandTile selectedTile;
        public ObservableBandTile SelectedTile
        {
            get { return selectedTile; }
            set { Set("SelectedTile", ref selectedTile, value, true); }
        }

        private string messageTitle = "Title";
        public string MessageTitle
        {
            get { return messageTitle; }
            set { Set("MessageTitle", ref messageTitle, value, true); }
        }

        private string messageBody = "Test message";
        public string MessageBody
        {
            get { return messageBody; }
            set { Set("MessageBody", ref messageBody, value, true); }
        }

        private bool tileManagerBusy;
        public bool TileManagerBusy
        {
            get { return tileManagerBusy; }
            set { Set("TileManagerBusy", ref tileManagerBusy, value, true); }
        }
    }
}
