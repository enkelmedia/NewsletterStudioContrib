import type { UmbBackofficeExtensionRegistry } from "@umbraco-cms/backoffice/extension-registry";
import { manifests as editorControlManifests } from "./editor-controls/manifest.js";
import { manifests as emailServiceProviders} from './email-service-provider/manifest.js';
import {manifests as tiptapManifests} from './tiptap-configuration/manifest.js';

const translationManifests : Array<UmbExtensionManifest> = [
	{
		type: "localization",
		alias: "UmbNs.Localize.EnUS",
		name: "English (United States)",
		meta: {
			"culture": "en-us"
		},
		js : ()=> import('./localization/en-us.js')
	}
]

export function registerManifest(registry : UmbBackofficeExtensionRegistry) {

    registry.registerMany([
		...editorControlManifests,
    	...translationManifests,
		...emailServiceProviders,
		...tiptapManifests
	]);
}
