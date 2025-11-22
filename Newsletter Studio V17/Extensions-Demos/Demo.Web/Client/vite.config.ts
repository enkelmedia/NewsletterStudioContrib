import { defineConfig } from "vite";

export default defineConfig(({ command }) => {

  const buildMode = process.env.build_mode?.trim();

  console.log('vite.config.ts | env.buildMode',buildMode);

  return {
    build: {
        lib: {
            entry: "src/entry.ts",
            formats: ["es"],
            name : "NsDemo"
        },
        outDir: "../wwwroot/App_Plugins/NsDemo/dist",
        sourcemap: buildMode == 'development' ? true : false,
        rollupOptions: {
            external: [/^@umbraco/,/^@newsletterstudio/,]
        },
    }
  }
});
