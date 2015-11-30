import Interfaces = require("../core/Interfaces");
import swo = require("./SeleniumWebObject");
import swe = require("./SeleniumWebElement");
import ph = require("./Helpers/PromiseHelpers");

export class SeleniumWebPage extends swo.SeleniumWebObject implements Interfaces.ITestableWebPage {

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
                .then((element) => new swe.SeleniumWebElement(element))
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

