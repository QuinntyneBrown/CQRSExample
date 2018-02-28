import { pluckOut } from "../shared/utilities/pluck-out";

import { DASHBOARD_ADDED, DASHBOARD_REMOVED, DASHBOARDS_LOADED, All } from "./dashboard.actions";

export function dashboardsReducer(state: Array<any> = [], action: All) {
    
    switch (action.type) {
        case DASHBOARD_ADDED:
            return state.concat(action.payload);
        
        case DASHBOARD_REMOVED:                        
            return pluckOut({ key: "code", value: action.payload.name, items: state });

        case DASHBOARDS_LOADED:
            return action.payload;

        default: 
            return state;
    }
}