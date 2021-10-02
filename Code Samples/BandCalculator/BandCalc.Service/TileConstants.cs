using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BandCalc.Service
{
    public static class TileConstants
    {
        private static Guid tileGuid = new Guid("d1f551ea-abd3-44b8-a265-26bf1def5ccc");
        private static Guid page0Guid = new Guid("00000000-0000-0000-0000-000000000001");
        private static Guid page1Guid = new Guid("00000000-0000-0000-0000-000000000002");
        private static Guid page2Guid = new Guid("00000000-0000-0000-0000-000000000003");
        private static Guid page3Guid = new Guid("00000000-0000-0000-0000-000000000004");
        private static Guid page4Guid = new Guid("00000000-0000-0000-0000-000000000005");
        private static Guid page5Guid = new Guid("00000000-0000-0000-0000-000000000006");

        public static Guid TileGuid { get { return tileGuid; } }
        public static Guid Page0Guid { get { return page0Guid; } }
        public static Guid Page1Guid { get { return page1Guid; } }
        public static Guid Page2Guid { get { return page2Guid; } }
        public static Guid Page3Guid { get { return page3Guid; } }
        public static Guid Page4Guid { get { return page4Guid; } }
        public static Guid Page5Guid { get { return page5Guid; } }
    }
}
