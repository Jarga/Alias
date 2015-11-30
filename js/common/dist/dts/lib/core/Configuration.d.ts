import Enums = require(".//Enums");
import Interfaces = require("./Interfaces");
export declare class DefaultConfiguration implements Interfaces.ITestConfiguration {
    constructor(driver?: webdriver.WebDriver, actionTimeout?: number, environmentType?: Enums.EnvironmentType, output?: Interfaces.ITestOutput);
    Driver: webdriver.WebDriver;
    ActionTimeout: number;
    EnvironmentType: Enums.EnvironmentType;
    Output: Interfaces.ITestOutput;
    Build(driver?: webdriver.WebDriver, actionTimeout?: number, environmentType?: Enums.EnvironmentType, output?: Interfaces.ITestOutput): DefaultConfiguration;
}
export interface IConfig {
    browser: string;
    server: string;
    port: string;
    output: string;
    environment: string;
    actiontimeout: number;
}
export declare class FileConfiguration extends DefaultConfiguration {
    constructor();
}
