import {
    Component,
    Input,
    OnInit,
    EventEmitter,
    Output,
    AfterViewInit,
    AfterContentInit,
    Renderer,
    ElementRef,
} from "@angular/core";

import { Dashboard } from "./dashboard.model";
import { DashboardsService } from "./dashboards.service";
import { Subject } from "rxjs";
import { pluckOut } from "../shared/utilities/pluck-out";
import { Router } from "@angular/router";
import { Store } from "@ngrx/store";
import { DashboardsState } from "./dashboards.state";
import { Observable } from "rxjs/Observable";
import { DashboardAdded } from "./dashboard.actions";
import { FormGroup, FormControl, Validators } from "@angular/forms";

@Component({
    templateUrl: "./dashboard-header.component.html",
    styleUrls: [
        "../shared/components/forms.css",
        "./dashboard-header.component.css"
    ],
    selector: "ce-dashboard-header"
})
export class DashboardHeaderComponent {
    constructor(
        private _dashboardsService: DashboardsService,
        private _router: Router,
        private _store: Store<DashboardsState>
    ) {
        this.dashboards$ = this._store.select("dashboards");
    }
    
    public dashboards$: Observable<Array<Dashboard>>;

    private _ngUnsubscribe: Subject<void> = new Subject();

    public ngOnDestroy() { this._ngUnsubscribe.next(); }

    public form = new FormGroup({ name: new FormControl('', [Validators.required]) });

    public dashboardInEditMode: Partial<Dashboard> = null;

    public tryToAddDashboard() {
        this.dashboardInEditMode = {}
    }

    public tryToSaveDashboard() {
        this.dashboardInEditMode = { name: this.form.value.name };

        this._dashboardsService
            .save({ dashboard: this.dashboardInEditMode })
            .map((dashboardId: any) => Object.assign(this.dashboardInEditMode, { dashboardId: dashboardId }))
            .map(x => this._store.dispatch(new DashboardAdded(<Dashboard>this.dashboardInEditMode)))
            .do(() => {
                this.dashboardInEditMode = null;
                this.form.patchValue({ "name": null });
            })
            .takeUntil(this._ngUnsubscribe)
            .subscribe();
    }
}