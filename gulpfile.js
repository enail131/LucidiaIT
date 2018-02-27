'use strict';
var fs = require('fs');
var gulp = require('gulp');
var autoprefixer = require('gulp-autoprefixer');
var cleanCSS = require('gulp-clean-css');
var concat = require('gulp-concat');
var del = require('del');
var dist = "site";
var jslint = require('gulp-jslint');
var mocha = require('gulp-mocha');
var path = require('path');
var pump = require('pump');
var pug = require('gulp-pug');
var rename = require('gulp-rename');
var runSequence = require('run-sequence');
var sass = require('gulp-sass');
var uglify = require('gulp-uglify');

gulp.task('lint', function () {
    return gulp.src([
        'Common/js/main.js',
        'Common/js/utilities/*.js',
        'Components/**/*.js',
        'Common/js/final.js',
        'Common/js/run.js'
    ])
        .pipe(jslint({
            browser: true,
            multivar: true,
            plusplus: true,
            white: true,
            fudge: true,
            this: true,
            'for': true,
            predef: ['$', 'CMS', 'window', 'google']
        }))
        .pipe(jslint.reporter('stylish'));
});

gulp.task('js', function () {
    return gulp.src([
        'Common/js/main.js',
        'Common/js/utilities/*.js',
        'Components/**/*.js',
        'Common/js/final.js',
        'Common/js/run.js'
    ])
        .pipe(concat('site.js'))
        .pipe(gulp.dest('./wwwroot/js'));
});

gulp.task('minify', ['js'], function (cb) {
    pump([
        gulp.src('./wwwroot/js/site.js'),
        uglify(),
        rename('site.min.js'),
        gulp.dest('./wwwroot/js')
    ])
});

gulp.task('sass', function () {
    return gulp.src([
        'Common/scss/variables.scss',
        'Common/scss/main.scss',
        'Common/scss/*.scss',
        'Components/**/*.scss',
        'Common/scss/final.scss'
    ])
        .pipe(concat('site.css'))
        .pipe(sass({ includePaths: ['/', 'Common/scss'] }).on('error', sass.logError))
        .pipe(autoprefixer())
        .pipe(gulp.dest('./wwwroot/css'));
});

gulp.task('minify-css', ['sass'], function (cb) {
    pump([
        gulp.src('./wwwroot/css/site.css'),
        cleanCSS(),
        rename('site.min.css'),
        gulp.dest('./wwwroot/css')
    ],
        cb
    );
});

gulp.task('build', ['sass', 'minify-css', 'js', 'minify']);

gulp.task("watch", function () {
    gulp.watch(["Common/scss/*.scss", "Components/**/*.scss"], function () {
        gulp.start("sass");
        gulp.start("minify-css");
    });

    gulp.watch(["Components/**/*.js", "Common/js/*.js"], function () {
        gulp.start("js");
        gulp.start("minify");
    });
});

gulp.task('default', ['build', 'watch']);