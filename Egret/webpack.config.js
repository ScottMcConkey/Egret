"use strict";
const path = require('path');
const bundleFileName = 'bundle';

module.exports = {
    mode: "development",
    entry: ['./wwwroot/src/index.js', './wwwroot/src/sass/index.scss'],
    output: {
        filename: bundleFileName + '.js',
        path: path.resolve(__dirname, 'wwwroot/dist')
    },
    module: {
        rules: [
            {
                test: /\.(scss)$/,
                use: [
                    {
                        // Adds CSS to the DOM by injecting a `<style>` tag
                        loader: 'style-loader'
                    },
                    {
                        // Interprets `@import` and `url()` like `import/require()` and will resolve them
                        loader: 'css-loader'
                    },
                    {
                        // Loads a SASS/SCSS file and compiles it to CSS
                        loader: 'sass-loader'
                    }
                ]
            }
        ]
    }
};