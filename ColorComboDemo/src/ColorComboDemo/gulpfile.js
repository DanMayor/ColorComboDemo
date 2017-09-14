var gulp = require("gulp");
var path = require("path");
var sass = require("gulp-sass");
var ts = require("gulp-typescript");
var sourcemaps = require("gulp-sourcemaps");
var tslint = require("gulp-tslint");
var del = require('del');
var Builder = require('systemjs-builder');

var scriptsPath = "Scripts/app/*.ts";
var destPath = "wwwroot/app";

gulp.task('clean', function () {
    return del([
        destPath + "app"
    ]);
});

gulp.task("typescript", function () {
    var tsProject = ts.createProject("tsconfig.json", {
        declarationFiles: true,
        noExternalResolve: true,
        isolatedModules: true
    });

    var tsResult =
            gulp.src(scriptsPath)
            .pipe(sourcemaps.init())
        	.pipe(ts(tsProject));

    tsResult.js
        .pipe(sourcemaps.write())
        .pipe(gulp.dest(destPath));
});

gulp.task("tslint", function () {
    gulp.src(scriptsPath)
        .pipe(tslint({
            formatter: "prose"
        }))
        .pipe(tslint.report({
            emitError: false
        }));
});

gulp.task("static files", function () {
    gulp.src("Scripts/app/systemjs.config.js")
        .pipe(gulp.dest(destPath));
});

gulp.task('bundle', function () {
    var builder = new Builder('/', 'wwwroot/app/systemjs.config.js');
    return Promise.all([
        builder.buildStatic('wwwroot/app/home/boot.js', 'wwwroot/app/home/bundle.js', { minify: false, sourceMaps: false }),
        builder.buildStatic('wwwroot/app/passwordReset/boot.js', 'wwwroot/app/passwordReset/bundle.js', { minify: false, sourceMaps: false }),
        builder.buildStatic('wwwroot/app/fassaden/boot.js', 'wwwroot/app/fassaden/bundle.js', { minify: false, sourceMaps: false }),
        builder.buildStatic('wwwroot/app/lamellen/boot.js', 'wwwroot/app/lamellen/bundle.js', { minify: false, sourceMaps: false })
    ])
        .then(function () {
            console.log("bundles complete");
        })
        .catch(function (err) {
            console.log("bundles build error");
            console.log(err);
        })
});

gulp.task("build", ['clean'], function () {
    gulp.start("static files");
    gulp.start("typescript");
});

gulp.task("watch", ['clean', 'build'], function () {
    gulp.watch(scriptsPath, ["typescript", "tslint"]);
});

gulp.task("default", function () {
});
