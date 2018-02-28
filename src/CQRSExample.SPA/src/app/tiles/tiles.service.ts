import { Injectable } from "@angular/core";
import { Tile } from "./tile.model";
import { Observable } from "rxjs/Observable";
import { Storage } from "../shared/services/storage.service";

@Injectable()
export class TilesService {
    constructor() {

    }

    public tilelist(): Observable<Array<Tile>> {
        throw new Error("Not Implemented!");
    }    
}
