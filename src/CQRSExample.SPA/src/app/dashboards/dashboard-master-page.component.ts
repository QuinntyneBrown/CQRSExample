import { Component } from "@angular/core";
import { Subject } from "rxjs/Subject";
import { DashboardsService } from "./dashboards.service";
import { DashboardsLoaded } from "./dashboard.actions";
import { Store } from "@ngrx/store";
import { DashboardsState } from "./dashboards.state";
import { Dashboard } from ".";
import { Router, NavigationEnd, ActivatedRoute } from "@angular/router";

@Component({
    templateUrl: "./dashboard-master-page.component.html",
    styleUrls: ["./dashboard-master-page.component.css"],
    selector: "ce-dashboard-master-page"
})
export class DashboardMasterPageComponent { 
    constructor(
        private _activatedRoute: ActivatedRoute,
        private _dashboardsService: DashboardsService,
        private _store: Store<DashboardsState>,
        private _router: Router
    ) {
        this._router.events
            .takeUntil(this._ngUnsubscribe)
            .filter(x => x instanceof NavigationEnd)
            .switchMap(() => this._dashboardsService.getByCurrentUser())
            .subscribe(x => {

            });
    }

    public get dashboardId() { return this._activatedRoute.snapshot.params["id"]; }

    public onDashboardAdded() {
        
    }

    public onDashboardRemoved() {

    }

    ngOnInit() {
        this._dashboardsService
            .getByCurrentUser()
            .takeUntil(this._ngUnsubscribe)
            .map((response) => this._store.dispatch(new DashboardsLoaded(response.dashboards)))
            .subscribe();
    }

    private _ngUnsubscribe: Subject<void> = new Subject<void>();

    ngOnDestroy() {
         this._ngUnsubscribe.next();	
    }
}
