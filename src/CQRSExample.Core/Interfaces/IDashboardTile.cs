using CQRSExample.Core.Entities;

namespace CQRSExample.Core.Interfaces
{
    public interface IDashboardTile<T> where T: DashboardTileSettings
    {
        T Settings { get; set; }
    }
}
