import Enums = require("./core/Enums");
import Interfaces = require("./core/Interfaces");
import Types = require("./core/Types");
import Configuration = require("./core/Configuration");
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
    type DefaultConfiguration = Configuration.DefaultConfiguration;
    var DefaultConfiguration: typeof Configuration.DefaultConfiguration;
    type FileConfiguration = Configuration.FileConfiguration;
    var FileConfiguration: typeof Configuration.FileConfiguration;
}
export = Common;
