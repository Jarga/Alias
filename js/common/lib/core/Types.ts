import webdriver = require("selenium-webdriver");
import Interfaces = require("./Interfaces");
import ph = require("../selenium/Helpers/PromiseHelpers");
import fh = require("../selenium/Helpers/FindHelpers");


export abstract class SeleniumWebObject {
    abstract GetSearchContext(): webdriver.IWebElementFinders;
    abstract GetWeDriver(): webdriver.WebDriver;

    SubElements: { [index: string]: { [index: string]: string; }; };

    GetElementProperties(targetElement: string): { [key: string]: string; } {
        if (this.SubElements[targetElement]) {
            return this.SubElements[targetElement];
        }
        throw new Error(`Element name ${targetElement} is not a registered element name.`);
    }

    FindSubElement(targetSubElement: string): webdriver.promise.Promise<Interfaces.ITestableWebElement>;
    FindSubElement(targetSubElement: string, timeout: number): webdriver.promise.Promise<Interfaces.ITestableWebElement>;
    FindSubElement(subElementProperties: { [index: string]: string; }): webdriver.promise.Promise<Interfaces.ITestableWebElement>;
    FindSubElement(subElementProperties: { [index: string]: string; }, timeout: number): webdriver.promise.Promise<Interfaces.ITestableWebElement>;
    FindSubElement(targetSubElement: any, timeout?: number): webdriver.promise.Promise<Interfaces.ITestableWebElement> {
        //If argument is a string then the target was a pre-defined sub element
        if (typeof targetSubElement === "string") {
            return this.FindSubElementByProperties(this.GetElementProperties(targetSubElement), timeout);
        }

        var elementProperties: { [index: string]: string; };
        if (typeof targetSubElement === typeof elementProperties) {
            return this.FindSubElementByProperties(targetSubElement, timeout);
        }

        throw new Error(`Invalid target of type ${typeof targetSubElement} found for findSubElement, must be string or Dictionary: { [index: string]: string; }`);
    }

    FindSubElementByProperties(subElementProperties: { [index: string]: string; }, timeout?: number): webdriver.promise.Promise<Interfaces.ITestableWebElement> {
        if (!!subElementProperties["parentelement"]) {
            var parentName = subElementProperties["parentelement"];
            var elementProperties: { [index: string]: string; } = {};

            for (var prop in subElementProperties) {
                if (subElementProperties.hasOwnProperty(prop)) {
                    elementProperties[prop] = subElementProperties[prop];
                }
            }
            delete elementProperties["parentelement"];

            //Compiler complains about this not being a webdriver.promise.Promise<SeleniumWebElement> when it actually is
            return fh.FindHelpers.FindSubElement(this.GetSearchContext(), this.GetElementProperties(parentName))
                .then((parent) => parent.FindSubElement(elementProperties, timeout));
        }

        var condition = () => fh.FindHelpers.FindSubElement(this.GetSearchContext(), subElementProperties).then((elementFound) => elementFound, () => null);

        return this.GetWeDriver().wait(ph.PromiseHelpers.AsCondition(condition), timeout);
    }

    FindSubElements(targetSubElement: string): webdriver.promise.Promise<Interfaces.ITestableWebElement[]>;
    FindSubElements(targetSubElement: string, timeout: number): webdriver.promise.Promise<Interfaces.ITestableWebElement[]>;
    FindSubElements(subElementProperties: { [index: string]: string; }): webdriver.promise.Promise<Interfaces.ITestableWebElement[]>;
    FindSubElements(subElementProperties: { [index: string]: string; }, timeout: number): webdriver.promise.Promise<Interfaces.ITestableWebElement[]>;
    FindSubElements(targetSubElement: any, timeout?: number): webdriver.promise.Promise<Interfaces.ITestableWebElement[]> {
        //If argument is a string then the target was a pre-defined sub element
        if (typeof targetSubElement === "string") {
            return this.FindSubElementsByProperties(this.GetElementProperties(targetSubElement), timeout);
        }

        var elementProperties: { [index: string]: string; };
        if (typeof targetSubElement === typeof elementProperties) {
            return this.FindSubElementsByProperties(targetSubElement, timeout);
        }

        throw new Error(`Invalid target of type ${typeof targetSubElement} found for findSubElements, must be string or Dictionary: { [index: string]: string; }`);
    }

    FindSubElementsByProperties(subElementProperties: { [index: string]: string; }, timeout?: number): webdriver.promise.Promise<Interfaces.ITestableWebElement[]> {
        if (!!subElementProperties["parentelement"]) {
            var elementProperties: { [index: string]: string; } = {};

            for (var prop in subElementProperties) {
                if (subElementProperties.hasOwnProperty(prop)) {
                    elementProperties[prop] = subElementProperties[prop];
                }
            }
            delete elementProperties["parentelement"];
            return this.FindSubElementsByProperties(elementProperties, timeout);
        }

        var condition = () => fh.FindHelpers.FindSubElements(this.GetSearchContext(), subElementProperties).then((elementsFound) => {
            if (elementsFound.length > 0) {
                return elementsFound;
            }
            return [];
        });

        return this.GetWeDriver().wait(ph.PromiseHelpers.AsCondition(condition), timeout);
    }

    RegisterSubElement(name: string, elementProperties: { [index: string]: string; }): void {
        this.SubElements[name] = {};

        //lowercase all element properties
        for (var prop in elementProperties) {
            if (elementProperties.hasOwnProperty(prop)) {
                this.SubElements[name][prop.toLowerCase()] = elementProperties[prop];
            }
        }
    }
}




export class SeleniumWebPage extends SeleniumWebObject implements Interfaces.ITestableWebPage {

    constructor(driver: webdriver.WebDriver) {
        super();
        this.Driver = driver;

        //we do not want the driver to do work until we record what window handle we are working with
        this.Driver.wait(this.Driver.getWindowHandle().then((handle) => this.WindowHandle = handle));
    }

    Driver: webdriver.WebDriver;

    WindowHandle: string;
    DefaultActionTimeout: number;

    GetSearchContext(): webdriver.IWebElementFinders {
        return this.Driver;
    }

    GetWeDriver(): webdriver.WebDriver {
        return this.Driver;
    }

    EnsureFocus(): webdriver.promise.Promise<void> {
        return this.Driver.switchTo().window(this.WindowHandle);
    }

    SetActiveWindow(windowUrlContains: string, timeout: number): webdriver.promise.Promise<void> {
        var setActiveWindow = () => this.Driver.getAllWindowHandles().then((handles) => {
            //Look through all active window handles for a window that matches the given
            var promises: webdriver.promise.Promise<boolean>[] = [];
            for (var i = 0; i < handles.length; i++) {
                promises.push(this.Driver.switchTo().window(handles[i])
                    .then(() => this.Driver.getCurrentUrl())
                    .then((url) => {
                        if (url.indexOf(windowUrlContains) > -1) {
                            return true;
                        }
                        return false;
                    }));
            }

            //return results of all the attempted window switches
            return webdriver.promise.all(promises);
        }).then((results) => { //If we failed to switch to any new window switch back to the original window and fail this condition loop
            if (results.filter((r) => r).length === 0) {
                this.Driver.switchTo().window(this.WindowHandle);
                return false;
            }
            return true;
        });
        return this.Driver.wait(ph.PromiseHelpers.AsCondition(setActiveWindow), timeout).then((result) => {
            if (!result) {
                throw new Error("Failed to switch to target window.");
            }
        });
    }

    Open(url: string): webdriver.promise.Promise<void> {
        return this.EnsureFocus()
            .then(() => this.Driver.get(url));
    }

    Close(): webdriver.promise.Promise<void> {
        return this.EnsureFocus()
            .then(() => this.Driver.close());
    }

    Maximize(): webdriver.promise.Promise<void> {
        return this.EnsureFocus()
            .then(() => this.Driver.manage().window().maximize());
    }

    GetCurrentUrl(): webdriver.promise.Promise<string> {
        return this.EnsureFocus()
            .then(() => this.Driver.getCurrentUrl());
    }

    GetScreenshot(): webdriver.promise.Promise<string> {
        return this.EnsureFocus()
            .then(() => this.Driver.takeScreenshot());
    }

    GetRootElement(): webdriver.promise.Promise<Interfaces.ITestableWebElement> {
        return this.EnsureFocus()
            .then(() => this.Driver.findElement(webdriver.By.tagName("html"))
                .then((element) => new SeleniumWebElement(element))
            );
    }

    Quit(): webdriver.promise.Promise<void> {
        return this.Driver.quit();
    }

    Clear(): webdriver.promise.Promise<void> {
        throw new Error("You can't clear the browser.");
    }

    Type(text: string): webdriver.promise.Promise<void>;
    Type(text: string, element: string): webdriver.promise.Promise<void>;
    Type(text: string, element?: string): webdriver.promise.Promise<void> {
        return this.GetRootElement().then((root) => root.Type(text, element));
    }

    Click(): webdriver.promise.Promise<void>;
    Click(element: string): webdriver.promise.Promise<void>;
    Click(element?: string): webdriver.promise.Promise<void> {
        return this.GetRootElement().then((root) => root.Click(element));
    }

    Select(item: string, isValue: boolean): webdriver.promise.Promise<void>;
    Select(item: string, isValue: boolean, element: string): webdriver.promise.Promise<void>;
    Select(item: string, isValue: boolean, element?: string): webdriver.promise.Promise<void> {
        return this.GetRootElement().then((root) => root.Select(item, isValue, element));
    }

    Exists(targetSubElement: string, timeout: number): webdriver.promise.Promise<boolean> {
        return this.GetRootElement().then((root) => root.Exists(targetSubElement, timeout));
    }

    WaitForAppear(timeout: number): webdriver.promise.Promise<boolean>;
    WaitForAppear(timeout: number, targetSubElement: string): webdriver.promise.Promise<boolean>;
    WaitForAppear(timeout: number, targetSubElement?: string): webdriver.promise.Promise<boolean> {
        return this.GetRootElement().then((root) => root.WaitForAppear(timeout, targetSubElement));
    }

    WaitForDisappear(timeout: number): webdriver.promise.Promise<boolean>;
    WaitForDisappear(timeout: number, targetSubElement: string): webdriver.promise.Promise<boolean>;
    WaitForDisappear(timeout: number, targetSubElement?: string): webdriver.promise.Promise<boolean> {
        return this.GetRootElement().then((root) => root.WaitForDisappear(timeout, targetSubElement));
    }

    IsDisplayed(): webdriver.promise.Promise<boolean> {
        return this.GetRootElement().then((root) => root.IsDisplayed());
    }

    IsSelected(): webdriver.promise.Promise<boolean> {
        return this.GetRootElement().then((root) => root.IsSelected());
    }

    SetChecked(value: boolean): webdriver.promise.Promise<void>;
    SetChecked(value: boolean, element: string): webdriver.promise.Promise<void>;
    SetChecked(value: boolean, element?: string): webdriver.promise.Promise<void> {
        return this.GetRootElement().then((root) => root.SetChecked(value, element));
    }

    WaitForAttributeState(attributeName: string, condition: (value: string) => boolean, timeout: number): webdriver.promise.Promise<boolean>;
    WaitForAttributeState(attributeName: string, condition: (value: string) => boolean, timeout: number, targetSubElement: string): webdriver.promise.Promise<boolean>;
    WaitForAttributeState(attributeName: string, condition: (value: string) => boolean, timeout: number, targetSubElement?: string): webdriver.promise.Promise<boolean> {
        return this.GetRootElement().then((root) => root.WaitForAttributeState(attributeName, condition, timeout, targetSubElement));
    }

    GetCssValue(propertyName: string): webdriver.promise.Promise<string> {
        return this.GetRootElement().then((root) => root.GetCssValue(propertyName));
    }

    GetAttribute(attributeName: string): webdriver.promise.Promise<string> {
        return this.GetRootElement().then((root) => root.GetAttribute(attributeName));
    }

    GetTagName(): webdriver.promise.Promise<string> {
        return this.GetRootElement().then((root) => root.GetTagName());
    }

    InnerHtml(): webdriver.promise.Promise<string> {
        return this.GetRootElement().then((root) => root.InnerHtml());
    }

    GetText(): webdriver.promise.Promise<string> {
        return this.GetRootElement().then((root) => root.GetText());
    }

    Parent(levels: number): webdriver.promise.Promise<Interfaces.ITestableWebElement> {
        throw new Error("No parent to the root element.");
    }
}

export class SeleniumWebElement extends SeleniumWebObject implements Interfaces.ITestableWebElement {

    constructor(_baseObject: webdriver.WebElement, DefaultActionTimeout?: number) {
        super();
        this._baseObject = _baseObject;
        this.DefaultActionTimeout = DefaultActionTimeout || 60;
        this.SubElements = {};
    }

    _baseObject: webdriver.WebElement;

    DefaultActionTimeout: number;

    GetSearchContext(): webdriver.IWebElementFinders {
        return this._baseObject;
    }

    GetWeDriver(): webdriver.WebDriver {
        return this._baseObject.getDriver();
    }

    Clear(): webdriver.promise.Promise<void> {
        return this._baseObject.clear();
    }

    Type(text: string): webdriver.promise.Promise<void>;
    Type(text: string, element: string): webdriver.promise.Promise<void>;
    Type(text: string, element?: string): webdriver.promise.Promise<void> {
        if (element == null) {
            return this._baseObject.sendKeys(text);
        } else {
            return this.FindSubElement(element).then((element) => element.Type(text));
        }
    }

    Click(): webdriver.promise.Promise<void>;
    Click(element: string): webdriver.promise.Promise<void>;
    Click(element?: string): webdriver.promise.Promise<void> {
        if (element == null) {
            return this._baseObject.click();
        } else {
            return this.FindSubElement(element).then((element) => element.Click());
        }
    }

    Select(item: string, isValue: boolean): webdriver.promise.Promise<void>;
    Select(item: string, isValue: boolean, element: string): webdriver.promise.Promise<void>;
    Select(item: string, isValue: boolean, element?: string): webdriver.promise.Promise<void> {

        var optionProperties: { [key: string]: string } = { "tag": "option" };

        if (isValue) {
            optionProperties["value"] = item;
        } else {
            optionProperties["text"] = item;
        }

        if (element == null) {
            return this.Click()
                .then(() => this.FindSubElement(optionProperties))
                .then((element) => element.Click());
        } else {
            return this.FindSubElement(element).then((element) => element.Select(item, isValue));
        }
    }

    Exists(targetSubElement: string, timeout: number): webdriver.promise.Promise<boolean> {
        var exists = () => this.FindSubElements(targetSubElement).then((elementsFound) => {
            if (elementsFound.length > 0) {
                return true;
            }
            return false;
        });

        return this._baseObject.getDriver().wait(ph.PromiseHelpers.AsCondition(exists), timeout);
    }

    WaitForAppear(timeout: number): webdriver.promise.Promise<boolean>;
    WaitForAppear(timeout: number, targetSubElement: string): webdriver.promise.Promise<boolean>;
    WaitForAppear(timeout: number, targetSubElement?: string): webdriver.promise.Promise<boolean> {
        var condition: () => webdriver.promise.Promise<boolean>;

        if (targetSubElement == null) {
            condition = () => this.IsDisplayed();
        } else {
            condition = () => this.FindSubElements(targetSubElement).then((elementsFound) => {
                if (elementsFound.length > 0) {
                    //Create promise that finds all the elements on the page and returns if they are displayed or not
                    return webdriver.promise.all(elementsFound.map((element) => element.IsDisplayed()));
                }
                return new webdriver.promise.Promise((accept) => accept([false]));
            }).then((results) => { //Verify all elements are displayed
                return results.filter((item) => item === false).length === 0;
            });
        }

        return this._baseObject.getDriver().wait(ph.PromiseHelpers.AsCondition(condition), timeout);
    }

    WaitForDisappear(timeout: number): webdriver.promise.Promise<boolean>;
    WaitForDisappear(timeout: number, targetSubElement: string): webdriver.promise.Promise<boolean>;
    WaitForDisappear(timeout: number, targetSubElement?: string): webdriver.promise.Promise<boolean> {
        var condition: () => webdriver.promise.Promise<boolean>;

        if (targetSubElement == null) {
            condition = () => this.IsDisplayed().then((result) => !result);
        } else {
            condition = () => this.FindSubElements(targetSubElement).then((elementsFound) => {
                if (elementsFound.length > 0) {
                    //Create promise that finds all the elements on the page and returns if they are displayed or not
                    return webdriver.promise.all(elementsFound.map((element) => element.IsDisplayed()));
                }
                return new webdriver.promise.Promise((accept) => accept([false]));
            }).then((results) => { //Verify all elements are not displayed
                return results.filter((item) => item !== false).length === 0;
            });
        }

        return this._baseObject.getDriver().wait(ph.PromiseHelpers.AsCondition(condition), timeout);
    }

    IsDisplayed(): webdriver.promise.Promise<boolean> {
        return this._baseObject.isDisplayed();
    }

    IsSelected(): webdriver.promise.Promise<boolean> {
        return this._baseObject.isSelected();
    }

    SetChecked(value: boolean): webdriver.promise.Promise<void>;
    SetChecked(value: boolean, element: string): webdriver.promise.Promise<void>;
    SetChecked(value: boolean, element?: string): webdriver.promise.Promise<void> {
        if (element != null) {
            return this.FindSubElement(element).then((element) => element.SetChecked(value));
        }

        return this.IsSelected()
            .then((selected) => { //Try clicking first
                if (!selected) {
                    this.Click();
                }
                return this.IsSelected();
            }).then((selected) => { //Then try using space
                if (!selected) {
                    this._baseObject.sendKeys(webdriver.Key.ENTER);
                }
                return this.IsSelected();
            }).then((selected) => { //If that fails throw
                if (!selected) {
                    throw new Error("Failed to set element to checked!");
                }
            });
    }

    WaitForAttributeState(attributeName: string, condition: (value: string) => boolean, timeout: number): webdriver.promise.Promise<boolean>;
    WaitForAttributeState(attributeName: string, condition: (value: string) => boolean, timeout: number, targetSubElement: string): webdriver.promise.Promise<boolean>;
    WaitForAttributeState(attributeName: string, condition: (value: string) => boolean, timeout: number, targetSubElement?: string): webdriver.promise.Promise<boolean> {
        var waitCondition: () => webdriver.promise.Promise<boolean>;

        if (targetSubElement == null) {
            waitCondition = () => this.GetAttribute(attributeName).then((value) => condition(value));
        } else {
            waitCondition = () => this.FindSubElements(targetSubElement).then((elementsFound) => {
                if (elementsFound.length > 0) {
                    return webdriver.promise.all(elementsFound.map((element) => element.GetAttribute(attributeName)));
                }
                return new webdriver.promise.Promise((accept) => accept([null]));
            }).then((results) => {
                return results.every((item) => item != null && condition(item));
            });
        }

        return this._baseObject.getDriver().wait(ph.PromiseHelpers.AsCondition(waitCondition), timeout);
    }


    GetCssValue(propertyName: string): webdriver.promise.Promise<string> {
        return this._baseObject.getCssValue(propertyName);
    }

    GetAttribute(attributeName: string): webdriver.promise.Promise<string> {
        return this._baseObject.getAttribute(attributeName);
    }

    GetTagName(): webdriver.promise.Promise<string> {
        return this._baseObject.getTagName();
    }

    InnerHtml(): webdriver.promise.Promise<string> {
        return this._baseObject.getInnerHtml();
    }

    GetText(): webdriver.promise.Promise<string> {
        return this._baseObject.getText();
    }

    Parent(levels: number): webdriver.promise.Promise<Interfaces.ITestableWebElement> {
        var xpath = "..";

        for (var i = 1; i < levels; i++) {
            xpath += "/..";
        }

        return this._baseObject.findElement(webdriver.By.xpath(xpath)).then((element) => new SeleniumWebElement(element));
    }

}

export class WebPage extends SeleniumWebPage {
    constructor(testConfiguration: Interfaces.ITestConfiguration) {
        super(testConfiguration.Driver);
        this.TestConfiguration = testConfiguration;
    }

    TestConfiguration: Interfaces.ITestConfiguration;

    New<T extends Interfaces.ITestableWebPage>(type: { new (testConfiguration: Interfaces.ITestConfiguration): T }): T {
        return new type(this.TestConfiguration);
    }

    EnsureElementLoaded(verificationElement: string, successText: string, failedText: string, takeScreenshotOnSuccess?: boolean) {
        this.FindSubElement(verificationElement, 120)
            .then(() => {
                if (successText) {
                    if (takeScreenshotOnSuccess) {
                        this.GetScreenshot().then((screenshot) => {
                            this.TestConfiguration.Output.WriteLine(successText, screenshot);
                        });
                    } else {
                        this.TestConfiguration.Output.WriteLine(successText);
                    }
                }
            }, (error) => {

                if (failedText) {
                    this.GetScreenshot().then((screenshot) => {
                        this.TestConfiguration.Output.WriteLine(failedText, screenshot);
                    }).then(() => {
                        throw error;
                    });
                } else {
                    throw error;
                }
            });
    }
}

export class WebElement extends SeleniumWebElement {

}

