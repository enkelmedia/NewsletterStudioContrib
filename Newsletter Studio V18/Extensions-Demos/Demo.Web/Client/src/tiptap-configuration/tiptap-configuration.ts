import { UmbPropertyEditorConfigProperty } from "@umbraco-cms/backoffice/property-editor";
import { NsTiptapConfigurationSource } from '@newsletterstudio/umbraco/extensibility';

export default class DemoNsToolbarSource implements NsTiptapConfigurationSource {

  /**
   * Provides an array of editor config properties for Tiptap when used inside Newsletter Studio.
   * @param defaults The default configuration for Newsletter Studio
   * @returns 
   */
  async getConfiguration(defaults: UmbPropertyEditorConfigProperty[])
  {
    // In here, we can return a fresh array of configs or modify the defaults.
    
    let configs = [...defaults];
    let toolbar = configs.find(x=>x.alias == 'toolbar')!;
    toolbar.value = [
      [
        [
          "Umb.Tiptap.Toolbar.SourceEditor",
        ],
        [
          "Ns.Tiptap.Toolbar.StyleMenu",
        ],
        [
          "Umb.Tiptap.Toolbar.TextAlignLeft",
          "Umb.Tiptap.Toolbar.TextAlignCenter",
          "Umb.Tiptap.Toolbar.TextAlignRight"
        ],
        [
          "Umb.Tiptap.Toolbar.BulletList",
          "Umb.Tiptap.Toolbar.OrderedList"
        ],
        [
          "Umb.Tiptap.Toolbar.Link",
          "Umb.Tiptap.Toolbar.Unlink"
        ]
      ]
    ]

    return configs;
  }

  destroy() {}

}

