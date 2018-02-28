namespace CQRSExample.Core.Entities
{
    public class CustomerDashboardTileSettings: DashboardTileSettings
    {
        public new CustomerDashboardTile DashboardTile { get; set; }
        public int PageSize { get; set; }
    }
}
