import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Dashboard } from "./dashboard.model";
import { Observable } from "rxjs/Observable";
import { constants } from "../shared/constants";
import { EventHub } from "../shared/services/event-hub";
import { Subject } from "rxjs";

@Injectable()
export class DashboardsService {
    constructor(
        private _eventHub: EventHub,
        private _httpClient: HttpClient,
        @Inject(constants.BASE_URL) private _baseUrl:string
    ) { }

    public save(options: { dashboard: Partial<Dashboard> }) {
        let correlationId;        

        return this._httpClient
            .post(`${this._baseUrl}/api/dashboards/save`, options)
            .map((response: any) => correlationId = response.correlationId)
            .switchMap(() => this._eventHub.events)
            .map(serverEvent => JSON.parse(serverEvent))
            .filter(serverEvent => serverEvent.correlationId == correlationId)
            .map(x => x.entity.dashboardId);
    }

    public get(): Observable<{ dashboards: Array<Dashboard> }> {
        return this._httpClient
            .get<{ dashboards: Array<Dashboard> }>(`${this._baseUrl}/api/dashboards/get`);
    }
    
    public getByCurrentUser(): Observable<{ dashboards: Array<Dashboard> }> {
        return this._httpClient
            .get<{ dashboards: Array<Dashboard> }>(`${this._baseUrl}/api/dashboards/user/current`);
    }

    public getDefault(): Observable<{ dashboard: Dashboard }> {
        return this._httpClient
            .get<{ dashboard: Dashboard }>(`${this._baseUrl}/api/dashboards/getDefault`);
    }

    public getById(options: { dashboardId: number }): Observable<{ dashboard:Dashboard}> {
        return this._httpClient
            .get<{dashboard: Dashboard}>(`${this._baseUrl}/api/dashboards/getById?id=${options.dashboardId}`);
    }

    public remove(options: { dashboard: Partial<Dashboard>}) {
        return this._httpClient
            .delete(`${this._baseUrl}/api/dashboards/delete/${options.dashboard.dashboardId}`);
    }
}
