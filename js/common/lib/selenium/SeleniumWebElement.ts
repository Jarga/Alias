import Interfaces = require("../core/Interfaces");
import ph = require("./Helpers/PromiseHelpers");
import swo = require("./SeleniumWebObject");

export class SeleniumWebElement extends swo.SeleniumWebObject implements Interfaces.ITestableWebElement {

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
