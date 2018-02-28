import { Dashboard } from "./dashboard.model";

export interface DashboardsState {
    dashboards: Array<Dashboard>;
    currentlyActiveDashboard: Dashboard
}