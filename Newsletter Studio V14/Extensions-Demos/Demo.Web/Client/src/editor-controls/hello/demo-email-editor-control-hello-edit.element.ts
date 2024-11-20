import { NsEmailEditorControlEditUiBase } from "@newsletterstudio/umbraco/extensibility";
import { NsEmailEditorControlHelloData } from "./demo-email-editor-control-hello.models";
import { css, customElement, html } from "@umbraco-cms/backoffice/external/lit";

@customElement('demo-email-editor-hello-edit')
export class DemoEmailEditorHalloEditElement extends NsEmailEditorControlEditUiBase<NsEmailEditorControlHelloData> 
{
    #updateText(e : InputEvent){
        this.updateModel({
            text : (e.target as HTMLTextAreaElement).value 
        });
    }

    //TODO: Update event parameter to use NsPaddingChangedEvent

    render(){
        return html`
            <div>
                <ns-property label="Enter text">
                    <textarea .value=${this.model?.text ?? ''} @keyup=${this.#updateText}></textarea>                
                </ns-property>                
                <ns-property label="ns_margin">
                    <ns-padding-editor
                        id="padding"
                        .value=${this.model!.padding}
                        @change=${(e : any)=>this.updateModel({padding:e.detail})}></ns-padding-editor>
                </ns-property>
            </div>
        `;
    }

    static styles = css`
        textarea {
            width:100%;
            min-height:250px;
        }
    `
}

export default DemoEmailEditorHalloEditElement;

declare global {
    interface HTMLElementTagNameMap {
        'demo-email-editor-hello-edit': DemoEmailEditorHalloEditElement;
    }
}