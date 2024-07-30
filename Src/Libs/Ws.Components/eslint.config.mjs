// @ts-check

import globals from 'globals'
import eslint from '@eslint/js'
import tseslint from 'typescript-eslint'
import pluginPromise from 'eslint-plugin-promise'
import simpleImportSort from "eslint-plugin-simple-import-sort";
import pluginSonarJs from "eslint-plugin-sonarjs"
import pluginUnicorn from "eslint-plugin-unicorn"

export default tseslint.config(
  eslint.configs.recommended,
  ...tseslint.configs.strictTypeChecked,
  pluginPromise.configs['flat/recommended'],
  pluginUnicorn.configs["flat/recommended"],
  pluginSonarJs.configs.recommended,
  {
    languageOptions: {
      parserOptions: {
        project: true,
        tsconfigRootDir: import.meta.dirname,
      },
      globals: globals.builtin,
    },
    plugins: {
      pluginSonarJs,
      "simple-import-sort": simpleImportSort
    },
    rules: {
      "simple-import-sort/imports": "error",
      "simple-import-sort/exports": "error"
    }
  },
);