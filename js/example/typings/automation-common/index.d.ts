import Enums = require("./core/Enums");
import Interfaces = require("./core/Interfaces");
import Types = require("./core/Types");
import dc = require("./initialization/Configurations/DefaultConfiguration");
import fc = require("./initialization/Configurations/FileConfiguration");
declare module Common {
    type EnvironmentType = Enums.EnvironmentType;
    var EnvironmentType: typeof Enums.EnvironmentType;
    type ITestableWebElement = Interfaces.ITestableWebElement;
    type ITestableWebPage = Interfaces.ITestableWebPage;
    type ITestOutput = Interfaces.ITestOutput;
    type ITestConfiguration = Interfaces.ITestConfiguration;
    type WebPage = Types.WebPage;
    var WebPage: typeof Types.WebPage;
    type WebElement = Types.WebElement;
    var WebElement: typeof Types.WebElement;
    type DefaultConfiguration = dc.DefaultConfiguration;
    var DefaultConfiguration: typeof dc.DefaultConfiguration;
    type FileConfiguration = fc.FileConfiguration;
    var FileConfiguration: typeof fc.FileConfiguration;
}
export = Common;
