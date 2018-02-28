using CQRSExample.Core.Entities;

namespace CQRSExample.Features.Tiles
{
    public class TileApiModel
    {        
        public int TileId { get; set; }
        public string Name { get; set; }

        public static TileApiModel FromTile(Tile tile)
        {
            var model = new TileApiModel();
            model.TileId = tile.TileId;
            model.Name = tile.Name;
            return model;
        }
    }
}
