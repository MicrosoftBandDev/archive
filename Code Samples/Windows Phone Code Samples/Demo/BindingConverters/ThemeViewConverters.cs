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
using System;
using System.Globalization;
using Windows.UI;
using Windows.UI.Xaml.Data;

namespace BandSdkSample.BindingConverters
{
    public class GetClearImageThemeCommandEnabledConverter : OneWayValueConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, string language)
        {
            return Convert((ThemeModel)value);
        }

        public bool Convert(ThemeModel vm)
        {
            return (vm.Main.BandClient != null && !vm.PersonalizationManagerBusy);
        }
    }

    public class SetImageCommandEnabledConverter : OneWayValueConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, string language)
        {
            return Convert((ThemeModel)value);
        }

        public bool Convert(ThemeModel vm)
        {
            return (vm.Main.BandClient != null && vm.MeTileImage != null && !vm.PersonalizationManagerBusy);
        }
    }

    public class SetThemeCommandEnabledConverter : OneWayValueConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, string language)
        {
            return Convert((ThemeModel)value);
        }

        public bool Convert(ThemeModel vm)
        {
            return (vm.Main.BandClient != null && vm.Theme != null && !vm.PersonalizationManagerBusy);
        }
    }

    public class ColorToHexNumericTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return Convert((Color)value);
        }

        public string Convert(Color color)
        {
            int rgb;

            rgb =
               (color.R << 16) |
               (color.G << 08) |
               (color.B << 00);

            return rgb.ToString("X6");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return ConvertBack((string)value);
        }

        public Color ConvertBack(string hex)
        {
            UInt32 rgb;

            if (UInt32.TryParse(hex, NumberStyles.HexNumber, null, out rgb) && rgb <= 0xFFFFFF)
            {
                return Color.FromArgb(
                    0xFF,
                    (byte)((rgb & 0x00FF0000) >> 16),
                    (byte)((rgb & 0x0000FF00) >> 08),
                    (byte)((rgb & 0x000000FF) >> 00)
                );
            }
            else
            {
                return Color.FromArgb(0xFF, 0, 0, 0);
            }
        }
    }
}
