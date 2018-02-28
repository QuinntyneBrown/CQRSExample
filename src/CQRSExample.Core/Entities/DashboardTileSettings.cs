namespace CQRSExample.Core.Entities
{
    public class DashboardTileSettings
    {
        public int DashboardTileId { get; set; }
        public DashboardTile DashboardTile { get; set; }
        public int Top { get; set; }
        public int Left { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
