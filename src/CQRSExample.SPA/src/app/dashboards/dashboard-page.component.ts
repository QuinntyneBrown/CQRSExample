import { Component, ElementRef } from "@angular/core";
import { Router, ActivatedRoute } from "@angular/router";
import { FormControl } from "@angular/forms";
import { Store } from "@ngrx/store";
import { Observable } from "rxjs/Observable";
import { Subject } from "rxjs/Subject";
import { DashboardsState } from "./dashboards.state";
import { DashboardRemoved } from "./dashboard.actions";
import { Dashboard } from "./dashboard.model";
import { DashboardsService } from "./dashboards.service";

@Component({
    templateUrl: "./dashboard-page.component.html",
    styleUrls: ["./dashboard-page.component.css"],
    selector: "cs-dashboard-page"
})
export class DashboardPageComponent { 
    constructor(
        private _elementRef: ElementRef,
        private _store: Store<DashboardsState>,
        private _dashboardService: DashboardsService
    ) {

        this.dashboards$ = this._store.select('dashboards');
    }

    private _ngUnsubscribe: Subject<void> = new Subject();
    
    ngOnDestroy() {
        this._ngUnsubscribe.next();
    }

    public remove(options: { dashboard: Dashboard }) {
        this._dashboardService.remove({ dashboard: options.dashboard })
            .takeUntil(this._ngUnsubscribe)
            .do(() => this._store.dispatch(new DashboardRemoved(options.dashboard)))
            .subscribe();
    }

    dashboards$;
}