﻿"use strict";
const path = require('path');
const MiniCssExtractPlugin = require("mini-css-extract-plugin");

module.exports = {
    mode: "development",
    entry: './wwwroot/src/index.js',
    output: {
        filename: 'bundle.js',
        publicPath: '/dist',
        path: path.resolve(__dirname, 'wwwroot/dist')
    },
    module: {
        rules: [

            {
                test: /\.s?css$/,
                use: [
                    MiniCssExtractPlugin.loader,
                    "css-loader",
                    "sass-loader"
                ]
            },
            {
                test: /\.(woff(2)?|ttf|eot|svg)(\?v=\d+\.\d+\.\d+)?$/,
                use: [{
                    loader: 'url-loader',
                    options: {
                        name: '[name].[ext]',
                        limit: 50,
                        outputPath: '/fonts'
                    }
                }]
            }
        ]
    },
    plugins: [
        new MiniCssExtractPlugin({
            filename: "css/styles.css",
            chunkFilename: "[id].css"
        })
    ]
};