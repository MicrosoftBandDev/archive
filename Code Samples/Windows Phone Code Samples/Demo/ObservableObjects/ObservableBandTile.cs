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
using System;
using Windows.UI.Xaml.Media.Imaging;

namespace BandSdkSample.ObservableObjects
{
    public class ObservableBandTile : ObservableObject
    {
        public ObservableBandTile(BandTile source)
        {
            this.source = source;
        }

        private BandTile source;
        public BandTile Source
        {
            get { return source; }
            set { Set("Source", ref source, value); }
        }

        public Guid TileId
        {
            get { return source.TileId; }
        }

        public string Name
        {
            get { return source.Name; }
        }

        private WriteableBitmap tileIcon;
        public WriteableBitmap TileIcon
        {
            get { return tileIcon ?? (tileIcon = source.TileIcon.ToWriteableBitmap()); }
        }
    }
}
