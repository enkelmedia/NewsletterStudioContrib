
const nsTiptapConfiguration : UmbExtensionManifest = {
  type : 'nsTiptapConfiguration',
  name: "Demo Newsletter Studio Tiptap Configuration",
  alias: "Demo.NewsletterStudio.Tiptap.Configuration",
  api : ()=> import('./tiptap-configuration.js')
}

export const manifests = [
    nsTiptapConfiguration
]
