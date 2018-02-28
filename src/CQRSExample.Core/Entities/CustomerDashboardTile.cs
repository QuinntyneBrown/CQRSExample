using CQRSExample.Core.Interfaces;

namespace CQRSExample.Core.Entities
{
    public class CustomerDashboardTile: DashboardTile, IDashboardTile<DashboardTileSettings>
    {
        public new CustomerDashboardTileSettings Settings { get; set; }
    }
}
