import { UmbTextStyles } from '@umbraco-cms/backoffice/style';
import { css, html, customElement, state } from '@newsletterstudio/umbraco/lit';
import { NsEmailServiceProviderUiBase } from '@newsletterstudio/umbraco/extensibility';
import {NS_ADMINISTRATION_WORKSPACE_CONTEXT, NsAdministrationWorkspaceContext} from '@newsletterstudio/umbraco/administration';
import { umbBindToValidation } from '@umbraco-cms/backoffice/validation';

@customElement('cool-email-email-service-provider-settings')
export class CoolEmailServiceProviderSettingsElement extends NsEmailServiceProviderUiBase<CoolEmailServiceProviderSettings> {
  
  #workspaceContext? : NsAdministrationWorkspaceContext;
  
  @state()
  workspaceKey? : string;
  
  constructor() {
    super();
    
    this.consumeContext(NS_ADMINISTRATION_WORKSPACE_CONTEXT,(instance?) => {
      this.#workspaceContext = instance;

      this.observe(this.#workspaceContext?.workspaceKey,(workspaceKey) => {
        this.workspaceKey = workspaceKey;
      });

    });
  }
  
  /**
   * Notice the name renderSettings(), the parameter provided will be an object will all settings
   * @param settings 
   * @returns 
   */
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
                      ${umbBindToValidation(this,'$.cc_apiKey',settings.cc_apiKey)}
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