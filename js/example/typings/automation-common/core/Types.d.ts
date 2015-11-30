import Interfaces = require("./Interfaces");
import swp = require("../selenium/SeleniumWebPage");
import swe = require("../selenium/SeleniumWebElement");
export declare class WebPage extends swp.SeleniumWebPage {
    constructor(testConfiguration: Interfaces.ITestConfiguration);
    TestConfiguration: Interfaces.ITestConfiguration;
    New<T extends Interfaces.ITestableWebPage>(type: {
        new (testConfiguration: Interfaces.ITestConfiguration): T;
    }): T;
    EnsureElementLoaded(verificationElement: string, successText: string, failedText: string, takeScreenshotOnSuccess?: boolean): void;
}
export declare class WebElement extends swe.SeleniumWebElement {
}
