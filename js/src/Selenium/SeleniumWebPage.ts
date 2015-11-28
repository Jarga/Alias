class SeleniumWebPage implements Interfaces.ITestableWebPage {
    constructor(Driver: webdriver.WebDriver) {
        this.Driver = Driver;
    }

    WindowHandle: string;
    Driver: webdriver.WebDriver;

    AsNew(): Interfaces.ITestableWebPage { throw new Error("Not implemented"); }

    EnsureFocus(): void {}

    SetActiveWindow(windowUrlContains: string, timeout: number): void {}

    Open(url: string): void {}

    Close(): void {}

    Maximize(): void {}

    GetCurrentUrl(): string { throw new Error("Not implemented"); }

    GetScreenshot(): string { throw new Error("Not implemented"); }

    Clear(): webdriver.promise.Promise<void> { throw new Error("Not implemented"); }

    Type(text: string): webdriver.promise.Promise<void>;
    Type(text: string, element: Interfaces.ITestableWebElement): webdriver.promise.Promise<void>;
    Type(text: string, element?: Interfaces.ITestableWebElement): webdriver.promise.Promise<void> { throw new Error("Not implemented"); }

    Click(): webdriver.promise.Promise<void>;
    Click(element: Interfaces.ITestableWebElement): webdriver.promise.Promise<void>;
    Click(element?: Interfaces.ITestableWebElement): webdriver.promise.Promise<void> { throw new Error("Not implemented"); }

    Select(item: string, isValue: boolean): webdriver.promise.Promise<void>;
    Select(item: string, isValue: boolean, element: Interfaces.ITestableWebElement): webdriver.promise.Promise<void>;
    Select(item: string, isValue: boolean, element?: Interfaces.ITestableWebElement): webdriver.promise.Promise<void> { throw new Error("Not implemented"); }

    Exists(targetSubElement: string, timeout: number): webdriver.promise.Promise<boolean> { throw new Error("Not implemented"); }

    WaitForAppear(timeout: number): webdriver.promise.Promise<boolean>;
    WaitForAppear(timeout: number, targetSubElement: string): webdriver.promise.Promise<boolean>;
    WaitForAppear(timeout: number, targetSubElement?: string): webdriver.promise.Promise<boolean> { throw new Error("Not implemented"); }

    WaitForDisappear(timeout: number): webdriver.promise.Promise<boolean>;
    WaitForDisappear(timeout: number, targetSubElement: string): webdriver.promise.Promise<boolean>;
    WaitForDisappear(timeout: number, targetSubElement?: string): webdriver.promise.Promise<boolean> { throw new Error("Not implemented"); }

    IsDisplayed(): webdriver.promise.Promise<boolean> { throw new Error("Not implemented"); }

    IsSelected(): webdriver.promise.Promise<boolean> { throw new Error("Not implemented"); }

    SetChecked(value: boolean): webdriver.promise.Promise<void>;
    SetChecked(value: boolean, element: Interfaces.ITestableWebElement): webdriver.promise.Promise<void>;
    SetChecked(value: boolean, element?: Interfaces.ITestableWebElement): webdriver.promise.Promise<void> { throw new Error("Not implemented"); }

    WaitForAttributeState(attributeName: string, condition: (value: string) => boolean, timeout: number): webdriver.promise.Promise<boolean>;
    WaitForAttributeState(attributeName: string, condition: (value: string) => boolean, timeout: number, targetSubElement: string): webdriver.promise.Promise<boolean>;
    WaitForAttributeState(attributeName: string, condition: (value: string) => boolean, timeout: number, targetSubElement?: string): webdriver.promise.Promise<boolean> { throw new Error("Not implemented"); }

    GetAttribute(attributeName: string): webdriver.promise.Promise<string> { throw new Error("Not implemented"); }

    GetTagName(): webdriver.promise.Promise<string> { throw new Error("Not implemented"); }

    InnerHtml(): webdriver.promise.Promise<string> { throw new Error("Not implemented"); }

    GetText(): webdriver.promise.Promise<string> { throw new Error("Not implemented"); }

    Parent(levels: number): webdriver.promise.Promise<SeleniumWebElement> { throw new Error("Not implemented"); }

    GetElementProperties(targetElement: string): { [index: string]: string; } { throw new Error("Not implemented"); }

    FindSubElement(targetSubElement: string): webdriver.promise.Promise<SeleniumWebElement>;
    FindSubElement(targetSubElement: string, timeout: number): webdriver.promise.Promise<SeleniumWebElement>;
    FindSubElement(subElementProperties: { [index: string]: string; }): webdriver.promise.Promise<SeleniumWebElement>;
    FindSubElement(subElementProperties: { [index: string]: string; }, timeout: number): webdriver.promise.Promise<SeleniumWebElement>;
    FindSubElement(targetSubElement: string | { [index: string]: string; }, timeout?: number): webdriver.promise.Promise<SeleniumWebElement> { throw new Error("Not implemented"); }

    FindSubElements(targetSubElement: string): webdriver.promise.Promise<SeleniumWebElement[]>;
    FindSubElements(targetSubElement: string, timeout: number): webdriver.promise.Promise<SeleniumWebElement[]>;
    FindSubElements(subElementProperties: { [index: string]: string; }): webdriver.promise.Promise<SeleniumWebElement[]>;
    FindSubElements(subElementProperties: { [index: string]: string; }, timeout: number): webdriver.promise.Promise<SeleniumWebElement[]>;
    FindSubElements(targetSubElement: string | { [index: string]: string; }, timeout?: number): webdriver.promise.Promise<SeleniumWebElement[]> { throw new Error("Not implemented"); }

    RegisterSubElement(name: string, elementProperties: { [index: string]: string; }): void {}

    DefaultActionTimeout: number;
    SubElements: { [index: string]: { [index: string]: string; }; };
}
