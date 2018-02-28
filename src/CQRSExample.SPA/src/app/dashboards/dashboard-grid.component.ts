import { Component, ElementRef, Input, ChangeDetectionStrategy, Output, EventEmitter } from "@angular/core";
import { Subject } from "rxjs/Subject";
import { Dashboard } from ".";
import { Observable } from "rxjs/Observable";

@Component({
    templateUrl: "./dashboard-grid.component.html",
    styleUrls: ["./dashboard-grid.component.css"],
    selector: "cs-dashboard-grid",
})
export class DashboardGridComponent { 
    constructor(private _elementRef: ElementRef) { }

    @Input()
    public dashboards: Array<Dashboard>;

    @Output()
    public tryToRemoveDashboard: EventEmitter<any> = new EventEmitter();    
}
