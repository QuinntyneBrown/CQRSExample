import { NgModule } from '@angular/core';
import { CommonModule } from "@angular/common";
import { HttpClientModule } from "@angular/common/http";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { RouterModule, Routes } from "@angular/router";
import { SharedModule } from "../shared/shared.module";

import { DashboardPageComponent } from "./dashboard-page.component";
import { DashboardsService } from "./dashboards.service";
import { DashboardMasterPageComponent } from "./dashboard-master-page.component";
import { DashboardHeaderComponent } from "./dashboard-header.component";

import { StoreModule } from "@ngrx/store";
import { DashboardGridComponent } from './dashboard-grid.component';

const providers = [
    DashboardsService
];

const declarations = [
    DashboardPageComponent,
    DashboardMasterPageComponent,
    DashboardHeaderComponent,
    DashboardGridComponent
];


@NgModule({
    imports: [
        CommonModule,
        HttpClientModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule,
        SharedModule,
        StoreModule
    ],
    declarations,
    providers,
    exports: declarations
})
export class DashboardModule { }