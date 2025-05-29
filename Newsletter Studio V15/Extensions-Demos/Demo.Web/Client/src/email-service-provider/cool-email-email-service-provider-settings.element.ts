import { UmbTextStyles } from '@umbraco-cms/backoffice/style';
import { nsBindToValidation } from '@newsletterstudio/umbraco/forms';
import { css, html, customElement, state } from '@newsletterstudio/umbraco/lit';
import { NsEmailServiceProviderUiBase } from '@newsletterstudio/umbraco/extensibility';
import {  WorkspaceManageValueFrontendModel } from '@newsletterstudio/umbraco/backend';
import {NS_ADMINISTRATION_WORKSPACE_CONTEXT, NsAdministrationWorkspaceContext} from '@newsletterstudio/umbraco/administration';
import { Observable } from '@umbraco-cms/backoffice/external/rxjs';

@customElement('cool-email-email-service-provider-settings')
export class CoolEmailServiceProviderSettingsElement extends NsEmailServiceProviderUiBase<CoolEmailServiceProviderSettings> {
  
  #workspaceContext? : NsAdministrationWorkspaceContext;
  
  @state()
  workspaceKey? : string;

  _baseUrl? : string;

  /**
   * A debounced observable of the workspace edit model so that we
   * can avoid server-fetch until user stops typing
   * */
  debouncedBaseUrlChange? : Observable<WorkspaceManageValueFrontendModel>;

  constructor() {
    super();
    
    this.consumeContext(NS_ADMINISTRATION_WORKSPACE_CONTEXT,(instance?) => {
      this.#workspaceContext = instance;

      this.observe(this.#workspaceContext?.workspaceKey,(workspaceKey) => {
        this.workspaceKey = workspaceKey;
      });

    });

  }
  
  renderSettings(settings : CoolEmailServiceProviderSettings) {

    return html`
      <ns-property
        label="API Key"
        description="Enter the API key for Cool Company" required>
        <uui-form-layout-item>
          <uui-input type="text"
                      .value=${settings.cc_apiKey ?? ''}
                      name="cc_apiKey"
                      @change=${(e:Event)=>this.updateValueFromEvent('cc_apiKey',e)}
                      label="API Key"}
                      ${nsBindToValidation(this,'$.cc_apiKey',settings.cc_apiKey)}
                      required></uui-input>
        </uui-form-layout-item>
      </ns-property>
          
    `

  }

  static styles = [UmbTextStyles, css`
    uui-input {width:100%;}
  `]
}

export default CoolEmailServiceProviderSettingsElement;

declare global {
  interface HTMLElementTagNameMap {
    'cool-email-email-service-provider-settings': CoolEmailServiceProviderSettingsElement;
  }
}

interface CoolEmailServiceProviderSettings {
  cc_apiKey : string;
}