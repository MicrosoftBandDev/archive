﻿/*
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
using Windows.UI.Xaml.Media.Imaging;

namespace BandSdkSample.Model
{
    public class ThemeModel : PageModelBase
    {
        public ThemeModel Self
        {
            get { return this; }
        }

        private bool personalizationManagerBusy;
        public bool PersonalizationManagerBusy
        {
            get { return personalizationManagerBusy; }
            set { Set("PersonalizationManagerBusy", ref personalizationManagerBusy, value, true); }
        }

        private ObservableBandTheme theme;
        public ObservableBandTheme Theme
        {
            get { return theme; }
            set { Set("Theme", ref theme, value, true); }
        }

        private WriteableBitmap meTileImage;
        public WriteableBitmap MeTileImage
        {
            get { return meTileImage; }
            set { Set("MeTileImage", ref meTileImage, value, true); }
        }

        private bool findingMeTileImage;
        public bool FindingMeTileImage
        {
            get { return findingMeTileImage; }
            set { Set("FindingMeTileImage", ref findingMeTileImage, value); }
        }
    }
}
