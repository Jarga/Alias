import Interfaces = require("../../core/Interfaces");
export declare class FindHelpers {
    static BuildBy(key: string, value: string): webdriver.Locator;
    static BuildCompositeXPathBy(keyValueDictionary: {
        [key: string]: string;
    }): webdriver.Locator;
    static GetAttributeXPath(attribute: string, value: string, isContains: boolean): string;
    static FindSubElement(baseObject: webdriver.IWebElementFinders, elementProperties: {
        [key: string]: string;
    }): webdriver.promise.Promise<Interfaces.ITestableWebElement>;
    static FindSubElements(baseObject: webdriver.IWebElementFinders, elementProperties: {
        [key: string]: string;
    }): webdriver.promise.Promise<Interfaces.ITestableWebElement[]>;
    static BuldUniversalByClause(elementProperties: {
        [key: string]: string;
    }): webdriver.Locator;
}
