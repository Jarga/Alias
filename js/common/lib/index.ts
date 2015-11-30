import Enums = require("./core/Enums");
import Interfaces = require("./core/Interfaces");
import Types = require("./core/Types");
import Configuration = require("./core/Configuration");

module Common {
    export type EnvironmentType = Enums.EnvironmentType;
    export var EnvironmentType = Enums.EnvironmentType;
    export type ITestableWebElement = Interfaces.ITestableWebElement;
    export type ITestableWebPage = Interfaces.ITestableWebPage;
    export type ITestOutput = Interfaces.ITestOutput;
    export type ITestConfiguration = Interfaces.ITestConfiguration;

    export type WebPage = Types.WebPage;
    export var WebPage = Types.WebPage;
    export type WebElement = Types.WebElement;
    export var WebElement = Types.WebElement;

    export type DefaultConfiguration = Configuration.DefaultConfiguration;
    export var DefaultConfiguration = Configuration.DefaultConfiguration;
    export type FileConfiguration = Configuration.FileConfiguration;
    export var FileConfiguration = Configuration.FileConfiguration;
}

export = Common