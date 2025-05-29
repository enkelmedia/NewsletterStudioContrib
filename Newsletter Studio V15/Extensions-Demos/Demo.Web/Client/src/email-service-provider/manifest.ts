import { ManifestEmailServiceProviderSettingsUi } from "@newsletterstudio/umbraco/extensibility";

const smtpCoolCompanyUi : ManifestEmailServiceProviderSettingsUi = {
  type: "nsEmailServiceProviderSettingsUi",
  name: "Cool Company Email Service Provider Settings",
  alias: "Cc.EmailServiceProviderSettings",
  element: () => import('./cool-email-email-service-provider-settings.element.js'),
  meta: {
    alias : 'coolEmail' // this alias should match alias in IEmailServiceProvider-implementation
  }
};

export const manifests = [
    smtpCoolCompanyUi
]
