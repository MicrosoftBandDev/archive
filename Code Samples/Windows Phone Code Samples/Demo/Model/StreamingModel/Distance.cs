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

using Microsoft.Band.Sensors;
using System;

namespace BandSdkSample.Model
{
    public partial class StreamingModel
    {
        private long distance;
        public long Distance
        {
            get { return distance; }
            set { Set("Distance", ref distance, value); }
        }

        private double speed;
        public double Speed
        {
            get { return speed; }
            set { Set("Speed", ref speed, value); }
        }

        private double pace;
        public double Pace
        {
            get { return pace; }
            set { Set("Pace", ref pace, value); }
        }

        private MotionType motion;
        public MotionType Motion
        {
            get { return motion; }
            set { Set("Motion", ref motion, value); }
        }

        private DateTimeOffset distanceTimestamp;
        public DateTimeOffset DistanceTimestamp
        {
            get { return distanceTimestamp; }
            set { Set("DistanceTimestamp", ref distanceTimestamp, value); }
        }
    }
}
