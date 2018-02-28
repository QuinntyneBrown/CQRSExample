import { Component } from "@angular/core";
import { Subject } from "rxjs/Subject";

@Component({
    templateUrl: "./update-tile-page.component.html",
    styleUrls: ["./update-tile-page.component.css"],
    selector: "cs-update-tile-page"
})
export class UpdateTilePageComponent { 

    private _ngUnsubscribe: Subject<void> = new Subject<void>();

    ngOnDestroy() {
         this._ngUnsubscribe.next();	
    }
}
