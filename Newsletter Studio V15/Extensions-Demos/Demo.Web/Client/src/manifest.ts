import type { UmbBackofficeExtensionRegistry } from "@umbraco-cms/backoffice/extension-registry";
import { manifests as editorControlManifests } from "./editor-controls/manifest";

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
    ...translationManifests
	]);
}
