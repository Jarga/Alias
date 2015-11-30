import webdriver = require("selenium-webdriver");
import Interfaces = require("./Interfaces");
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
export declare class SeleniumWebPage extends SeleniumWebObject implements Interfaces.ITestableWebPage {
    constructor(driver: webdriver.WebDriver);
    Driver: webdriver.WebDriver;
    WindowHandle: string;
    DefaultActionTimeout: number;
    GetSearchContext(): webdriver.IWebElementFinders;
    GetWeDriver(): webdriver.WebDriver;
    EnsureFocus(): webdriver.promise.Promise<void>;
    SetActiveWindow(windowUrlContains: string, timeout: number): webdriver.promise.Promise<void>;
    Open(url: string): webdriver.promise.Promise<void>;
    Close(): webdriver.promise.Promise<void>;
    Maximize(): webdriver.promise.Promise<void>;
    GetCurrentUrl(): webdriver.promise.Promise<string>;
    GetScreenshot(): webdriver.promise.Promise<string>;
    GetRootElement(): webdriver.promise.Promise<Interfaces.ITestableWebElement>;
    Quit(): webdriver.promise.Promise<void>;
    Clear(): webdriver.promise.Promise<void>;
    Type(text: string): webdriver.promise.Promise<void>;
    Type(text: string, element: string): webdriver.promise.Promise<void>;
    Click(): webdriver.promise.Promise<void>;
    Click(element: string): webdriver.promise.Promise<void>;
    Select(item: string, isValue: boolean): webdriver.promise.Promise<void>;
    Select(item: string, isValue: boolean, element: string): webdriver.promise.Promise<void>;
    Exists(targetSubElement: string, timeout: number): webdriver.promise.Promise<boolean>;
    WaitForAppear(timeout: number): webdriver.promise.Promise<boolean>;
    WaitForAppear(timeout: number, targetSubElement: string): webdriver.promise.Promise<boolean>;
    WaitForDisappear(timeout: number): webdriver.promise.Promise<boolean>;
    WaitForDisappear(timeout: number, targetSubElement: string): webdriver.promise.Promise<boolean>;
    IsDisplayed(): webdriver.promise.Promise<boolean>;
    IsSelected(): webdriver.promise.Promise<boolean>;
    SetChecked(value: boolean): webdriver.promise.Promise<void>;
    SetChecked(value: boolean, element: string): webdriver.promise.Promise<void>;
    WaitForAttributeState(attributeName: string, condition: (value: string) => boolean, timeout: number): webdriver.promise.Promise<boolean>;
    WaitForAttributeState(attributeName: string, condition: (value: string) => boolean, timeout: number, targetSubElement: string): webdriver.promise.Promise<boolean>;
    GetCssValue(propertyName: string): webdriver.promise.Promise<string>;
    GetAttribute(attributeName: string): webdriver.promise.Promise<string>;
    GetTagName(): webdriver.promise.Promise<string>;
    InnerHtml(): webdriver.promise.Promise<string>;
    GetText(): webdriver.promise.Promise<string>;
    Parent(levels: number): webdriver.promise.Promise<Interfaces.ITestableWebElement>;
}
export declare class SeleniumWebElement extends SeleniumWebObject implements Interfaces.ITestableWebElement {
    constructor(_baseObject: webdriver.WebElement, DefaultActionTimeout?: number);
    _baseObject: webdriver.WebElement;
    DefaultActionTimeout: number;
    GetSearchContext(): webdriver.IWebElementFinders;
    GetWeDriver(): webdriver.WebDriver;
    Clear(): webdriver.promise.Promise<void>;
    Type(text: string): webdriver.promise.Promise<void>;
    Type(text: string, element: string): webdriver.promise.Promise<void>;
    Click(): webdriver.promise.Promise<void>;
    Click(element: string): webdriver.promise.Promise<void>;
    Select(item: string, isValue: boolean): webdriver.promise.Promise<void>;
    Select(item: string, isValue: boolean, element: string): webdriver.promise.Promise<void>;
    Exists(targetSubElement: string, timeout: number): webdriver.promise.Promise<boolean>;
    WaitForAppear(timeout: number): webdriver.promise.Promise<boolean>;
    WaitForAppear(timeout: number, targetSubElement: string): webdriver.promise.Promise<boolean>;
    WaitForDisappear(timeout: number): webdriver.promise.Promise<boolean>;
    WaitForDisappear(timeout: number, targetSubElement: string): webdriver.promise.Promise<boolean>;
    IsDisplayed(): webdriver.promise.Promise<boolean>;
    IsSelected(): webdriver.promise.Promise<boolean>;
    SetChecked(value: boolean): webdriver.promise.Promise<void>;
    SetChecked(value: boolean, element: string): webdriver.promise.Promise<void>;
    WaitForAttributeState(attributeName: string, condition: (value: string) => boolean, timeout: number): webdriver.promise.Promise<boolean>;
    WaitForAttributeState(attributeName: string, condition: (value: string) => boolean, timeout: number, targetSubElement: string): webdriver.promise.Promise<boolean>;
    GetCssValue(propertyName: string): webdriver.promise.Promise<string>;
    GetAttribute(attributeName: string): webdriver.promise.Promise<string>;
    GetTagName(): webdriver.promise.Promise<string>;
    InnerHtml(): webdriver.promise.Promise<string>;
    GetText(): webdriver.promise.Promise<string>;
    Parent(levels: number): webdriver.promise.Promise<Interfaces.ITestableWebElement>;
}
export declare class WebPage extends SeleniumWebPage {
    constructor(testConfiguration: Interfaces.ITestConfiguration);
    TestConfiguration: Interfaces.ITestConfiguration;
    New<T extends Interfaces.ITestableWebPage>(type: {
        new (testConfiguration: Interfaces.ITestConfiguration): T;
    }): T;
    EnsureElementLoaded(verificationElement: string, successText: string, failedText: string, takeScreenshotOnSuccess?: boolean): void;
}
export declare class WebElement extends SeleniumWebElement {
}
