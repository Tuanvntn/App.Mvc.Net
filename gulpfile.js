// Import các gói cần thiết
const { series, watch, src, dest } = require('gulp');
const sass = require('gulp-sass')(require('sass'));
const cssmin = require('gulp-cssmin');
const rename = require('gulp-rename');

// Định nghĩa task biên dịch và nén Sass
function compileAndMinifySass() {
    return src('assets/scss/site.scss')
        .pipe(sass().on('error', sass.logError))
        // .pipe(cssmin())
        .pipe(rename({
            // suffix: ".min"
        }))
        .pipe(dest('wwwroot/css/'));
}

// Định nghĩa task theo dõi file
function watchFiles() {
    // Sửa đường dẫn để theo dõi tất cả các file .scss trong thư mục assets/scss
    watch('assets/scss/**/*.scss', compileAndMinifySass);
}

// Export các task để có thể chạy bằng lệnh gulp
exports.default = compileAndMinifySass;
exports.watch = watchFiles;