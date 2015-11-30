var NoOutput = (function () {
    function NoOutput() {
    }
    NoOutput.prototype.WriteLine = function (message, base64Image) {
        return;
    };
    return NoOutput;
})();
exports.NoOutput = NoOutput;
