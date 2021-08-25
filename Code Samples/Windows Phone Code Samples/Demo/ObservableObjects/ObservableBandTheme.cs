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
using Windows.UI;

namespace BandSdkSample.ObservableObjects
{
    public class ObservableBandTheme : ObservableObject
    {
        public ObservableBandTheme(BandTheme source)
        {
            this.source = source;
        }

        private BandTheme source;
        public BandTheme Source
        {
            get { return source; }
            set { Set("Source", ref source, value); }
        }

        private Color? _base;
        public Color Base
        {
            get { return _base.HasValue ? _base.Value : (_base = source.Base.ToColor()).Value; }
            set
            {
                if (!_base.HasValue || _base.Value != value)
                {
                    source.Base = value.ToBandColor();
                    Set("Base", ref _base, value);
                }
            }
        }

        private Color? highlight;
        public Color Highlight
        {
            get { return highlight.HasValue ? highlight.Value : (highlight = source.Highlight.ToColor()).Value; }
            set
            {
                if (!highlight.HasValue || highlight.Value != value)
                {
                    source.Highlight = value.ToBandColor();
                    Set("Highlight", ref highlight, value);
                }
            }
        }

        private Color? lowlight;
        public Color Lowlight
        {
            get { return lowlight.HasValue ? lowlight.Value : (lowlight = source.Lowlight.ToColor()).Value; }
            set
            {
                if (!lowlight.HasValue || lowlight.Value != value)
                {
                    source.Lowlight = value.ToBandColor();
                    Set("Lowlight", ref lowlight, value);
                }
            }
        }

        private Color? secondaryText;
        public Color SecondaryText
        {
            get { return secondaryText.HasValue ? secondaryText.Value : (secondaryText = source.SecondaryText.ToColor()).Value; }
            set
            {
                if (!secondaryText.HasValue || secondaryText.Value != value)
                {
                    source.SecondaryText = value.ToBandColor();
                    Set("SecondaryText", ref secondaryText, value);
                }
            }
        }

        private Color? highContrast;
        public Color HighContrast
        {
            get { return highContrast.HasValue ? highContrast.Value : (highContrast = source.HighContrast.ToColor()).Value; }
            set
            {
                if (!highContrast.HasValue || highContrast.Value != value)
                {
                    source.HighContrast = value.ToBandColor();
                    Set("HighContrast", ref highContrast, value);
                }
            }
        }

        private Color? muted;
        public Color Muted
        {
            get { return muted.HasValue ? muted.Value : (muted = source.Muted.ToColor()).Value; }
            set
            {
                if (!muted.HasValue || muted.Value != value)
                {
                    source.Muted = value.ToBandColor();
                    Set("Muted", ref muted, value);
                }
            }
        }
    }
}
