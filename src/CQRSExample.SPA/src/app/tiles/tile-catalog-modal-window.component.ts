import { Storage } from "../shared/services/storage.service";
import { constants } from "../shared/constants";
import { createElement } from "../shared/utilities/create-element";
import { EventHub } from "../shared/services/event-hub";
import { TILES_SELECTED, TILES_SELECT_CANCEL } from "./tiles.actions";
import { ModalService } from "../shared/services/modal.service";
import { render, TemplateResult, html } from "lit-html";
import { unsafeHTML } from "lit-html/lib/unsafe-html";

const styles = unsafeHTML(`<style>${require("./tile-catalog-modal-window.component.css")}</style>`);

export class TileCatalogModalWindowComponent extends HTMLElement {
    constructor(
        private _eventHub: EventHub = EventHub.instance,
        private _storage: Storage = Storage.instance,
        private _modalService: ModalService = ModalService.instance
    ) {
        super();
        this.cancel = this.cancel.bind(this);
        this.select = this.select.bind(this);
    }

    static get observedAttributes () {
        return [
            "tiles"
        ];
    }

    public get template(): TemplateResult {
        return html`
            ${styles}
            <cs-tile-catalog tiles="${JSON.stringify(this.tiles)}"></cs-tile-catalog>
        `;
    }
    async connectedCallback() {
        
        if(!this.shadowRoot) this.attachShadow({ mode: 'open' });
        
        if (!this.hasAttribute('role'))
            this.setAttribute('role', 'tilecatalogmodalwindow');

        render(this.template, this.shadowRoot);

        this._setEventListeners();
    }
    
    private _setEventListeners() {
        document.body.addEventListener(TILES_SELECT_CANCEL, this.cancel);
        document.body.addEventListener(TILES_SELECTED, this.select);
    }

    cancel(customEvent: CustomEvent) {
        this._modalService.close();
    } 

    select(customEvent: CustomEvent) {        
        this._modalService.close();
    }

    disconnectedCallback() {
        document.body.removeEventListener(TILES_SELECT_CANCEL, this.cancel);
        document.body.removeEventListener(TILES_SELECTED, this.select);
    }

    attributeChangedCallback (name, oldValue, newValue) {
        switch (name) {
            case "tiles":
                this.tiles = JSON.parse(newValue);
                break;
        }
    }

    public tiles: Array<any> = [];

    public get tileCatalogElement(): HTMLElement {
        return this.shadowRoot.querySelector("cs-tile-catalog") as HTMLElement;
    }
}

customElements.define(`cs-tile-catalog-modal-window`,TileCatalogModalWindowComponent);
