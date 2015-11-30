import Enums = require("./Enums");
export interface ITestableWebPage extends ITestableWebElement {
    EnsureFocus(): webdriver.promise.Promise<void>;
    SetActiveWindow(windowUrlContains: string, timeout: number): webdriver.promise.Promise<void>;
    Open(url: string): webdriver.promise.Promise<void>;
    Close(): webdriver.promise.Promise<void>;
    Maximize(): webdriver.promise.Promise<void>;
    GetCurrentUrl(): webdriver.promise.Promise<string>;
    GetScreenshot(): webdriver.promise.Promise<string>;
    GetRootElement(): webdriver.promise.Promise<ITestableWebElement>;
    Quit(): webdriver.promise.Promise<void>;
}
export interface ITestableWebElement {
    DefaultActionTimeout: number;
    SubElements: {
        [key: string]: {
            [key: string]: string;
        };
    };
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
    Parent(levels: number): webdriver.promise.Promise<ITestableWebElement>;
    GetElementProperties(targetElement: string): {
        [key: string]: string;
    };
    FindSubElement(targetSubElement: string): webdriver.promise.Promise<ITestableWebElement>;
    FindSubElement(targetSubElement: string, timeout: number): webdriver.promise.Promise<ITestableWebElement>;
    FindSubElement(subElementProperties: {
        [key: string]: string;
    }): webdriver.promise.Promise<ITestableWebElement>;
    FindSubElement(subElementProperties: {
        [key: string]: string;
    }, timeout: number): webdriver.promise.Promise<ITestableWebElement>;
    FindSubElements(targetSubElement: string): webdriver.promise.Promise<ITestableWebElement[]>;
    FindSubElements(targetSubElement: string, timeout: number): webdriver.promise.Promise<ITestableWebElement[]>;
    FindSubElements(subElementProperties: {
        [key: string]: string;
    }): webdriver.promise.Promise<ITestableWebElement[]>;
    FindSubElements(subElementProperties: {
        [key: string]: string;
    }, timeout: number): webdriver.promise.Promise<ITestableWebElement[]>;
    RegisterSubElement(name: string, elementProperties: {
        [key: string]: string;
    }): void;
}
export interface ITestOutput {
    WriteLine(message: string, base64Image?: string): void;
}
export interface ITestConfiguration {
    Driver: webdriver.WebDriver;
    ActionTimeout: number;
    EnvironmentType: Enums.EnvironmentType;
    Output: ITestOutput;
    Build(driver?: webdriver.WebDriver, actionTimeout?: number, environmentType?: Enums.EnvironmentType, output?: ITestOutput): ITestConfiguration;
}
