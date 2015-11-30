var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var Enums = require(".//Enums");
var no = require("../output/NoOutput");
var DefaultConfiguration = (function () {
    function DefaultConfiguration(driver, actionTimeout, environmentType, output) {
        this.Build(driver, actionTimeout, environmentType, output);
    }
    DefaultConfiguration.prototype.Build = function (driver, actionTimeout, environmentType, output) {
        this.Driver = driver || new webdriver.Builder().withCapabilities(webdriver.Capabilities.chrome()).build();
        this.ActionTimeout = actionTimeout || 60;
        this.EnvironmentType = environmentType || Enums.EnvironmentType.Dev;
        this.Output = output || new no.NoOutput();
        return this;
    };
    return DefaultConfiguration;
})();
exports.DefaultConfiguration = DefaultConfiguration;
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
})(DefaultConfiguration);
exports.FileConfiguration = FileConfiguration;
