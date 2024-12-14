import { NsEmailEditorControlDisplayUiBase } from "@newsletterstudio/umbraco/extensibility";
import { NsEmailEditorControlHelloData } from "./demo-email-editor-control-hello.models";
import { customElement, html, styleMap } from "@umbraco-cms/backoffice/external/lit";

@customElement('demo-email-editor-hello-display')
export class DemoEmailEditorHalloDisplayElement extends NsEmailEditorControlDisplayUiBase<NsEmailEditorControlHelloData> 
{
    render(){

        // Fetching the wrapperStyles (e.g. padding) form the base class
        const wrapperStyles =
        {
            ...this.getWrapperStyleMap(this.model)
        }

        return html`
        <div style=${styleMap(wrapperStyles)}>
            <p>Text: ${this.model?.text ?? ''}</p>
        </div>
        
        `;
    }
}

export default DemoEmailEditorHalloDisplayElement;

declare global {
    interface HTMLElementTagNameMap {
        'demo-email-editor-hello-display': DemoEmailEditorHalloDisplayElement;
    }
}
