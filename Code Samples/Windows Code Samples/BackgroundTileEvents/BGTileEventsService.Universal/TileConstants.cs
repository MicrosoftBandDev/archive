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

namespace BGTileEventsService.Universal
{
    /// <summary>
    /// Constants related to the tile we install on the Band and its visual components.
    /// </summary>
    public static class TileConstants
    {
        // WARNING! This tile guid is only an example. Please do not copy it to your test application;
        // always create a unique guid for each application.
        // If one application installs its tile, a second application using the same guid will fail to install
        // its tile due to a guid conflict. In the event of such a failure, the text of the exception will not
        // report that the tile with the same guid already exists on the band.
        // There might be other unexpected behavior.
        private static Guid tileGuid = new Guid("59761A7C-5630-4844-9E66-ED2CDD570F05");
        private static Guid page1Guid = new Guid("00000000-0000-0000-0000-000000000001");

        public static Guid TileGuid { get { return tileGuid; } }
        public static Guid Page1Guid { get { return page1Guid; } }

        public static short Button1ElementId { get { return 1; } }
        public static short TextElementId { get { return 2; } }

        public static string ButtonLabel { get { return "Push Me"; } }
    }
}
