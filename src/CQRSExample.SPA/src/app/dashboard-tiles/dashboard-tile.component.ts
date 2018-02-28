import { html, TemplateResult, render } from "lit-html";
import { repeat } from "lit-html/lib/repeat";
import { unsafeHTML } from "lit-html/lib/unsafe-html";
import { IDashboardTile } from "./dashboard-tile.model";

const styles = unsafeHTML(`<style>${require("./dashboard-tile.component.css")}<style>`);

export class DashboardTileComponent extends HTMLElement {
    constructor() {
        super();
    }

    static get observedAttributes () {
        return [
            "dashboard-tile"
        ];
    }

    public dashboardTile: IDashboardTile;

    connectedCallback() {     

        this.attachShadow({ mode: 'open' });
        
		render(this.template, this.shadowRoot);

        if (!this.hasAttribute('role'))
            this.setAttribute('role', 'dashboardtile');

        this._bind();
        this._setEventListeners();
    }

    public get styles() {
        return unsafeHTML(`
            <style>
                :host {
                    grid-column: var(--grid-column-start,${this.dashboardTile.left}) / var(--grid-column-stop,${this.dashboardTile.left + this.dashboardTile.width});
                    grid-row: var(--grid-row-start,${this.dashboardTile.top}) / var(--grid-row-stop,${this.dashboardTile.top + this.dashboardTile.height});
                    background-color: #fff;
                    box-shadow: 5px 3px 11px rgba(0,0,0,0.2);
                    overflow: hidden;
                    cursor: pointer;
                }
            </style>
        `);
    }

    public get template(): TemplateResult {
        return html`
            ${styles}
        `;
    }

    public async _bind() {

    }

    public _setEventListeners() {

    }

    disconnectedCallback() {

    }

    attributeChangedCallback (name, oldValue, newValue) {
        switch (name) {
            case "dashboard-tile":
                this.dashboardTile = JSON.parse(newValue);
                break;
        }
    }
}

customElements.define(`ce-dashboard-tile`,DashboardTileComponent);
