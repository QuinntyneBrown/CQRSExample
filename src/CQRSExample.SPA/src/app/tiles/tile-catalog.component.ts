import { Storage } from "../shared/services/storage.service";
import { Tile } from "./tile.model";
import { constants } from "../shared/constants";
import { TILES_SELECT_CANCEL, TILES_SELECTED } from "./tiles.actions";
declare var System: any;

const template = document.createElement("template");

const html = require("./tile-catalog.component.html");
const css = require("./tile-catalog.component.css");
const formsCss = require("../shared/components/forms.css");


export class TileCatalogComponent extends HTMLElement {
    constructor(private _storage:Storage = Storage.instance) {
        super();    
        this.add = this.add.bind(this);
        this.cancel = this.cancel.bind(this);
    }

    static get observedAttributes () {
        return ["tiles"];
    }

    async connectedCallback() {        
        template.innerHTML = `<style>${formsCss} ${css}</style>${html}`; 

        this.attachShadow({ mode: 'open' });
        this.shadowRoot.appendChild(document.importNode(template.content, true));  

        if (!this.hasAttribute('role'))
            this.setAttribute('role', 'tilecatalog');

        this._bind();
        this._setEventListeners();
    }

    private async _bind() {
        this.tileCatalogItemsHTMLElement.innerHTML = "";

        for (let i = 0; i < this.tiles.length; i++) {
            const item = document.createElement("cs-tile-catalog-item");
            item.setAttribute("tile", JSON.stringify(this.tiles[i]));
            this.tileCatalogItemsHTMLElement.appendChild(item);
        }
    }

    private _setEventListeners() {
        this.cancelButtonElement.addEventListener("click", this.cancel);
        this.addButtonElement.addEventListener("click", this.add);
        
    }

    disconnectedCallback() {
        this.cancelButtonElement.removeEventListener("click", this.cancel);
        this.addButtonElement.removeEventListener("click", this.add);
    }

    public cancel() {
        this.dispatchEvent(new CustomEvent(TILES_SELECT_CANCEL, {
            cancelable: true,
            bubbles: true,
            composed: true
        } as CustomEventInit));
    }

    public add() {        
        this.dispatchEvent(new CustomEvent(TILES_SELECTED, {
            cancelable: true,
            bubbles: true,
            composed: true,
            detail: { tiles: this.shadowRoot.querySelectorAll(".is-selected")}
        } as CustomEventInit));
    }

    attributeChangedCallback (name, oldValue, newValue) {
        switch (name) {
            case "tiles":
                this.tiles = JSON.parse(newValue);

                if (this.parentNode)
                    this._bind();

                break;
        }
    }

    public tiles: Array<Tile> = [];

    public get tileCatalogItemsHTMLElement() {
        return this.shadowRoot.querySelector(".tile-catalog-items");
    }

    public get cancelButtonElement(): HTMLElement { return this.shadowRoot.querySelector("button.cancel") as HTMLElement; }
    public get addButtonElement(): HTMLElement { return this.shadowRoot.querySelector("button.add") as HTMLElement; }
}

customElements.define(`cs-tile-catalog`,TileCatalogComponent);
