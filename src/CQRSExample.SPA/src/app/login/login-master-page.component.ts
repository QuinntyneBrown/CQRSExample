import { Component, ChangeDetectionStrategy, Input, Inject } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { RedirectService } from "../shared/services/redirect.service";
import { Storage } from "../shared/services/storage.service";
import { constants } from "../shared/constants";
import { Subject } from "rxjs/Subject";
import { Client } from "../shared/services/client";

@Component({
    templateUrl: "./login-master-page.component.html",
    styleUrls: ["./login-master-page.component.css"],
    selector: "ce-login-master-page"
})
export class LoginMasterPageComponent {
    constructor(
        private _client: HttpClient,
        private _loginRedirectService: RedirectService,
        private _storage: Storage,
        @Inject(constants.BASE_URL) private _baseUrl: string
    ) { }

    public ngOnInit() {
        const loginCredentials = this._storage.get({ name: constants.LOGIN_CREDENTIALS_KEY });

        if (loginCredentials && loginCredentials.rememberMe) {
            this.code = loginCredentials.code;
            this.password = loginCredentials.password;
            this.rememberMe = loginCredentials.rememberMe;
        }
    }

    public tryToLogin($event: { value: { username: string, password: string, rememberMe: boolean } }) {

        this._storage.put({ name: constants.LOGIN_CREDENTIALS_KEY, value: $event.value.rememberMe ? $event.value : null });

        const headers = new HttpHeaders().set('Content-Type', 'application/json'); 

        this._client.post(`${this._baseUrl}/api/users/signin`, $event.value, { headers })
            .takeUntil(this._ngUnsubscribe)
            .do(response => {
                this._storage.put({ name: constants.ACCESS_TOKEN_KEY, value: response["accessToken"] });
                this._storage.put({ name: constants.AUTHENTICATED_USER_ID_KEY, value: response["userId"] });
            })            
            .do(() => this._loginRedirectService.redirectPreLogin())
            .subscribe();
    }

    public code: string = "";

    public password: string = "";

    public rememberMe: boolean = false;

    private _ngUnsubscribe: Subject<void> = new Subject();

    public ngOnDestroy() { this._ngUnsubscribe.next(); }
}