import Enums = require(".//Enums");
import Interfaces = require("./Interfaces");
import no = require("../output/NoOutput");

export class DefaultConfiguration implements Interfaces.ITestConfiguration {

    constructor(driver?: webdriver.WebDriver, actionTimeout?: number, environmentType?: Enums.EnvironmentType, output?: Interfaces.ITestOutput) {
        this.Build(driver, actionTimeout, environmentType, output);
    }

    Driver: webdriver.WebDriver;
    ActionTimeout: number;
    EnvironmentType: Enums.EnvironmentType;
    Output: Interfaces.ITestOutput;

    Build(driver?: webdriver.WebDriver, actionTimeout?: number, environmentType?: Enums.EnvironmentType, output?: Interfaces.ITestOutput) {
        this.Driver = driver || new webdriver.Builder().withCapabilities(webdriver.Capabilities.chrome()).build();
        this.ActionTimeout = actionTimeout || 60;
        this.EnvironmentType = environmentType || Enums.EnvironmentType.Dev;
        this.Output = output || new no.NoOutput();

        return this;
    }
}

export interface IConfig {
    browser: string;
    server: string;
    port: string;
    output: string;
    environment: string;
    actiontimeout: number;
}

export class FileConfiguration extends DefaultConfiguration {

    constructor() {
        var config: IConfig;

        var request = new XMLHttpRequest();
        request.onload = function () {
            config = JSON.parse(this.responseText);
        };
        request.open("get", "webdriver-config.json", true);
        request.send();
        super();
    }
}

