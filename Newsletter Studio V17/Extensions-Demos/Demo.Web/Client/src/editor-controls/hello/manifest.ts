const editUi : UmbExtensionManifest = {
    type: 'nsEmailControlEditUi',
    alias : 'nsEmailControlEditUiHello',
    name : 'NS Email Control Edit Ui Hello',
    element : ()=> import('./demo-email-editor-control-hello-edit.element.js'),
    meta : {
      controlTypeAlias : 'hello'
    }
  }
  
  const displayUi : UmbExtensionManifest = {
    type: 'nsEmailControlDisplayUi',
    alias : 'nsEmailControlDisplayUiHello',
    name : 'NS Email Control Display Ui Hello',
    element : ()=> import('./demo-email-editor-control-hello-display.element.js'),
    meta : {
      controlTypeAlias : 'hello'
    }
  }

  export const manifests = [
    editUi,
    displayUi
  ]
  