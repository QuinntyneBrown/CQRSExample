using CQRSExample.Core.Interfaces;

namespace CQRSExample.Core.Entities
{
    public class DashboardTile: BaseModel, IDashboardTile<DashboardTileSettings>
    {        
        public int DashboardTileId { get; set; }           
		public string Name { get; set; }
        public virtual DashboardTileSettings Settings { get; set; }
    }
}
