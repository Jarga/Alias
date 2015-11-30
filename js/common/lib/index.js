var Enums = require("./core/Enums");
var Types = require("./core/Types");
var Configuration = require("./core/Configuration");
var Common;
(function (Common) {
    Common.EnvironmentType = Enums.EnvironmentType;
    Common.WebPage = Types.WebPage;
    Common.WebElement = Types.WebElement;
    Common.DefaultConfiguration = Configuration.DefaultConfiguration;
    Common.FileConfiguration = Configuration.FileConfiguration;
})(Common || (Common = {}));
module.exports = Common;
