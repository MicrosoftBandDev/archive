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

namespace BandSdkSample.Model
{
    public partial class StreamingModel
    {
        #region No Gyro
        private bool accel16Ms;
        public bool Accel16Ms
        {
            get { return this.accel16Ms; }
            set { Set("Accel16Ms", ref accel16Ms, value); }
        }

        private bool accel32Ms;
        public bool Accel32Ms
        {
            get { return this.accel32Ms; }
            set { Set("Accel32Ms", ref accel32Ms, value); }
        }

        private bool accel128Ms;
        public bool Accel128Ms
        {
            get { return this.accel128Ms; }
            set { Set("Accel128Ms", ref accel128Ms, value); }
        }

        private double accel_Ax;
        public double Accel_Ax
        {
            get { return accel_Ax; }
            set { Set("Accel_Ax", ref accel_Ax, value); }
        }

        private double accel_Ay;
        public double Accel_Ay
        {
            get { return accel_Ay; }
            set { Set("Accel_Ay", ref accel_Ay, value); }
        }

        private double accel_Az;
        public double Accel_Az
        {
            get { return accel_Az; }
            set { Set("Accel_Az", ref accel_Az, value); }
        }

        private DateTimeOffset accelTimestamp;
        public DateTimeOffset AccelTimestamp
        {
            get { return accelTimestamp; }
            set { Set("AccelTimestamp", ref accelTimestamp, value); }
        }
        #endregion

        #region With Gyro
        private bool motion16Ms;
        public bool Motion16Ms
        {
            get { return this.motion16Ms; }
            set { Set("Motion16Ms", ref motion16Ms, value); }
        }

        private bool motion32Ms;
        public bool Motion32Ms
        {
            get { return this.motion32Ms; }
            set { Set("Motion32Ms", ref motion32Ms, value); }
        }

        private bool motion128Ms;
        public bool Motion128Ms
        {
            get { return this.motion128Ms; }
            set { Set("Motion128Ms", ref motion128Ms, value); }
        }

        private double motion_Ax;
        public double Motion_Ax
        {
            get { return motion_Ax; }
            set { Set("Motion_Ax", ref motion_Ax, value); }
        }

        private double motion_Ay;
        public double Motion_Ay
        {
            get { return motion_Ay; }
            set { Set("Motion_Ay", ref motion_Ay, value); }
        }

        private double motion_Az;
        public double Motion_Az
        {
            get { return motion_Az; }
            set { Set("Motion_Az", ref motion_Az, value); }
        }

        private double motion_Gx;
        public double Motion_Gx
        {
            get { return motion_Gx; }
            set { Set("Motion_Gx", ref motion_Gx, value); }
        }

        private double motion_Gy;
        public double Motion_Gy
        {
            get { return motion_Gy; }
            set { Set("Motion_Gy", ref motion_Gy, value); }
        }

        private double motion_Gz;
        public double Motion_Gz
        {
            get { return motion_Gz; }
            set { Set("Motion_Gz", ref motion_Gz, value); }
        }

        private DateTimeOffset motionTimestamp;
        public DateTimeOffset MotionTimestamp
        {
            get { return motionTimestamp; }
            set { Set("MotionTimestamp", ref motionTimestamp, value); }
        }
        #endregion
    }
}
