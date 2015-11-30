var gulp = require('gulp');
var ts = require('gulp-typescript');
var shell = require('gulp-shell');
var runseq = require('run-sequence');
var tslint = require('gulp-tslint');

var tsProject = ts.createProject('tsconfig.json');
var projRootFolders = ['src/**/*.ts'];

gulp.task('default', ['lint', 'build']);

// ** Watching ** //

gulp.task('watch', function () {
    gulp.watch(projRootFolders, ['compile:typescript']);
});

// ** Compilation ** //

gulp.task('build', ['compile:typescript']);
gulp.task('compile:typescript', function () {
    var tsResult = tsProject.src()
                    .pipe(ts(tsProject));

    return tsResult.js.pipe(gulp.dest('.'));

});

// ** Linting ** //

gulp.task('lint', ['lint:default']);
gulp.task('lint:default', function () {
    return gulp.src(projRootFolders)
        .pipe(tslint())
        .pipe(tslint.report('prose', {
            emitError: false
        }));
});
