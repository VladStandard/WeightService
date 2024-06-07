import typescript from '@rollup/plugin-typescript';
import multi from '@rollup/plugin-multi-entry';
import terser from '@rollup/plugin-terser';

export default {
  input: ['./wwwroot/libs/**/*.ts', '!./wwwroot/libs/**/*.d.ts'],
  output: {
    format: 'es',
    dir: 'wwwroot'
  },
  plugins: [typescript({ exclude: '**/*.d.ts' }), multi({ entryFileName: 'index.js' }), terser()]
};