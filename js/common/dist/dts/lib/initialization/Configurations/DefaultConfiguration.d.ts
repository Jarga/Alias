import Enums = require("../../core/Enums");
import Interfaces = require("../../core/Interfaces");
export declare class DefaultConfiguration implements Interfaces.ITestConfiguration {
    constructor(driver?: webdriver.WebDriver, actionTimeout?: number, environmentType?: Enums.EnvironmentType, output?: Interfaces.ITestOutput);
    Driver: webdriver.WebDriver;
    ActionTimeout: number;
    EnvironmentType: Enums.EnvironmentType;
    Output: Interfaces.ITestOutput;
    Build(driver?: webdriver.WebDriver, actionTimeout?: number, environmentType?: Enums.EnvironmentType, output?: Interfaces.ITestOutput): DefaultConfiguration;
}
