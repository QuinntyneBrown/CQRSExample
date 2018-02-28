using System.Collections.Generic;

namespace CQRSExample.Core.Entities
{
    public class Dashboard: BaseModel
    {
        public int DashboardId { get; set; }           
		public string Name { get; set; }
        public ICollection<DashboardTile> DashboardTiles { get; set; } = new HashSet<DashboardTile>();
    }
}
