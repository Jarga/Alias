var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var dc = require("./DefaultConfiguration");
var FileConfiguration = (function (_super) {
    __extends(FileConfiguration, _super);
    function FileConfiguration() {
        var config;
        var request = new XMLHttpRequest();
        request.onload = function () {
            config = JSON.parse(this.responseText);
        };
        request.open("get", "webdriver-config.json", true);
        request.send();
        _super.call(this);
    }
    return FileConfiguration;
})(dc.DefaultConfiguration);
exports.FileConfiguration = FileConfiguration;
