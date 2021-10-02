using Microsoft.Band;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BandCalculator.Model
{
    class BandModel
    {
        static IBandInfo _selectedBand;

        public static IBandInfo SelectedBand
        {
            get { return BandModel._selectedBand; }
            set { BandModel._selectedBand = value; }
        }

        private static IBandClient _bandClient;
        public static IBandClient BandClient
        {
            get { return _bandClient; }
            set
            {
                _bandClient = value;
            }
        }


        public static bool IsConnected
        {
            get
            {
                return BandClient != null;
            }

        }

        public static async Task FindDevicesAsync()
        {
            var bands = await BandClientManager.Instance.GetBandsAsync();
            if (bands != null && bands.Length > 0)
            {
                SelectedBand = bands[0]; // take the first band

            }
        }

        public static async Task InitAsync()
        {
            if (IsConnected)
                return;

            await FindDevicesAsync();
            if (SelectedBand != null)
            {
                BandClient = await BandClientManager.Instance.ConnectAsync(SelectedBand);
            }
        }
    }
}
