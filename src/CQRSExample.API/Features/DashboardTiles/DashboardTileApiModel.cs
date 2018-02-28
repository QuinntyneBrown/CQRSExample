using CQRSExample.Core.Entities;

namespace CQRSExample.Features.DashboardTiles
{
    public class DashboardTileApiModel
    {        
        public int DashboardTileId { get; set; }
        public string Name { get; set; }

        public static DashboardTileApiModel FromDashboardTile(DashboardTile dashboardTile)
        {
            var model = new DashboardTileApiModel();
            model.DashboardTileId = dashboardTile.DashboardTileId;
            model.Name = dashboardTile.Name;
            return model;
        }
    }
}
