import { ManifestEmailServiceProviderSettingsUi } from "@newsletterstudio/umbraco/extensibility";

const smtpCoolCompanyUi : ManifestEmailServiceProviderSettingsUi = {
  type: "nsEmailServiceProviderSettingsUi",
  name: "Cool Company Email Service Provider Settings",
  alias: "Cc.EmailServiceProviderSettings",
  element: () => import('./cool-email-email-service-provider-settings.element.js'),
  meta: {
    // this alias should match alias in IEmailServiceProvider-implementation
    alias : 'coolEmail' 
  }
};

export const manifests = [
    smtpCoolCompanyUi
]
