import Interfaces = require("../core/Interfaces");
export declare abstract class SeleniumWebObject {
    abstract GetSearchContext(): webdriver.IWebElementFinders;
    abstract GetWeDriver(): webdriver.WebDriver;
    SubElements: {
        [index: string]: {
            [index: string]: string;
        };
    };
    GetElementProperties(targetElement: string): {
        [key: string]: string;
    };
    FindSubElement(targetSubElement: string): webdriver.promise.Promise<Interfaces.ITestableWebElement>;
    FindSubElement(targetSubElement: string, timeout: number): webdriver.promise.Promise<Interfaces.ITestableWebElement>;
    FindSubElement(subElementProperties: {
        [index: string]: string;
    }): webdriver.promise.Promise<Interfaces.ITestableWebElement>;
    FindSubElement(subElementProperties: {
        [index: string]: string;
    }, timeout: number): webdriver.promise.Promise<Interfaces.ITestableWebElement>;
    FindSubElementByProperties(subElementProperties: {
        [index: string]: string;
    }, timeout?: number): webdriver.promise.Promise<Interfaces.ITestableWebElement>;
    FindSubElements(targetSubElement: string): webdriver.promise.Promise<Interfaces.ITestableWebElement[]>;
    FindSubElements(targetSubElement: string, timeout: number): webdriver.promise.Promise<Interfaces.ITestableWebElement[]>;
    FindSubElements(subElementProperties: {
        [index: string]: string;
    }): webdriver.promise.Promise<Interfaces.ITestableWebElement[]>;
    FindSubElements(subElementProperties: {
        [index: string]: string;
    }, timeout: number): webdriver.promise.Promise<Interfaces.ITestableWebElement[]>;
    FindSubElementsByProperties(subElementProperties: {
        [index: string]: string;
    }, timeout?: number): webdriver.promise.Promise<Interfaces.ITestableWebElement[]>;
    RegisterSubElement(name: string, elementProperties: {
        [index: string]: string;
    }): void;
}
