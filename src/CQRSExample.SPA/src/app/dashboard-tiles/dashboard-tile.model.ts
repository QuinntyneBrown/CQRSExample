export interface IDashboardTile {
    dashboardTileId: number;
    code: string;
    top: number;
    width: number;
    left: number;
    height: number;
}

export class DashboardTile implements IDashboardTile {
    public dashboardTileId: number;
    public code: string;
    public top: number;
    public width: number;
    public left: number;
    public height: number;
}
