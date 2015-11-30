var Enums = require("../../core/Enums");
var no = require("../../output/NoOutput");
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
