var Enums = require("./core/Enums");
var Types = require("./core/Types");
var dc = require("./initialization/Configurations/DefaultConfiguration");
var fc = require("./initialization/Configurations/FileConfiguration");
var Common;
(function (Common) {
    Common.EnvironmentType = Enums.EnvironmentType;
    Common.WebPage = Types.WebPage;
    Common.WebElement = Types.WebElement;
    Common.DefaultConfiguration = dc.DefaultConfiguration;
    Common.FileConfiguration = fc.FileConfiguration;
})(Common || (Common = {}));
module.exports = Common;
