module Interfaces {
	export interface ITestableWebPage extends ITestableWebElement
    {
        AsNew(): ITestableWebPage;
        EnsureFocus(): void;
        SetActiveWindow(windowUrlContains: string, timeout: number): void;
        Open(url: string): void;
        Close(): void;
        Maximize(): void;
        GetCurrentUrl(): string;
        GetScreenshot(): string;
    }
	
	export interface ITestableWebElement
    {
        DefaultActionTimeout: number;
        SubElements: { [key: string]: { [key: string]: string } }

        Clear(): webdriver.promise.Promise<void>;
        Type(text: string): webdriver.promise.Promise<void>;
        Type(text: string, element: ITestableWebElement): webdriver.promise.Promise<void>;
        Click(): webdriver.promise.Promise<void>;
        Click(element: ITestableWebElement): webdriver.promise.Promise<void>;
        Select(item: string, isValue: boolean): webdriver.promise.Promise<void>;
        Select(item: string, isValue: boolean, element: ITestableWebElement): webdriver.promise.Promise<void>;

        Exists(targetSubElement: string, timeout: number): webdriver.promise.Promise<boolean>;
        WaitForAppear(timeout: number): webdriver.promise.Promise<boolean>;
        WaitForAppear(timeout: number, targetSubElement: string): webdriver.promise.Promise<boolean>;
        WaitForDisappear(timeout: number): webdriver.promise.Promise<boolean>;
        WaitForDisappear(timeout: number, targetSubElement: string): webdriver.promise.Promise<boolean>;
        IsDisplayed(): webdriver.promise.Promise<boolean>;
        IsSelected(): webdriver.promise.Promise<boolean>;
        SetChecked(value: boolean): webdriver.promise.Promise<void>;
        SetChecked(value: boolean, element: ITestableWebElement): webdriver.promise.Promise<void>;
        WaitForAttributeState(attributeName: string, condition: (value: string) => boolean, timeout: number): webdriver.promise.Promise<boolean>;
        WaitForAttributeState(attributeName: string, condition: (value: string) => boolean, timeout: number, targetSubElement: string): webdriver.promise.Promise<boolean>;
	
        GetAttribute(attributeName: string): webdriver.promise.Promise<string>;
        GetTagName(): webdriver.promise.Promise<string>;
        InnerHtml(): webdriver.promise.Promise<string>;
        GetText(): webdriver.promise.Promise<string>;
        Parent(levels: number): webdriver.promise.Promise<SeleniumWebElement>;

        GetElementProperties(targetElement: string): { [key: string]: string };
        FindSubElement(targetSubElement: string): webdriver.promise.Promise<SeleniumWebElement>;
        FindSubElement(targetSubElement: string, timeout: number): webdriver.promise.Promise<SeleniumWebElement>;
        FindSubElement(subElementProperties: { [key: string]: string }): webdriver.promise.Promise<SeleniumWebElement>;
        FindSubElement(subElementProperties: { [key: string]: string }, timeout: number): webdriver.promise.Promise<SeleniumWebElement>;
        FindSubElements(targetSubElement: string): webdriver.promise.Promise<SeleniumWebElement[]>;
        FindSubElements(targetSubElement: string, timeout: number): webdriver.promise.Promise<SeleniumWebElement[]>;
        FindSubElements(subElementProperties: { [key: string]: string }): webdriver.promise.Promise<SeleniumWebElement[]>;
        FindSubElements(subElementProperties: { [key: string]: string }, timeout: number): webdriver.promise.Promise<SeleniumWebElement[]>;

        RegisterSubElement(name: string, elementProperties: { [key: string]: string }): void;
	}
}