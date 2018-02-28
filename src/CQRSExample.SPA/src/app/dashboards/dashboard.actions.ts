import { Action } from "@ngrx/store";
import { Dashboard } from "./dashboard.model";

export const DASHBOARD_ADDED = '[Dashboards] DASHBOARD_ADDED';

export const DASHBOARD_REMOVED = '[Dashboards] DASHBOARD_REMOVED';

export const DASHBOARDS_LOADED = '[Dashboards] DASHBOARDS_LOADED';


export class DashboardsLoaded implements Action {
    readonly type = DASHBOARDS_LOADED;

    constructor(public payload: Array<Dashboard>) { }
}

export class DashboardAdded implements Action {
    readonly type = DASHBOARD_ADDED;    

    constructor(public payload: Dashboard ) { }
}

export class DashboardRemoved implements Action {
    readonly type = DASHBOARD_REMOVED;    

    constructor(public payload: Dashboard) { }
}

export type All = DashboardsLoaded | DashboardAdded | DashboardRemoved;

