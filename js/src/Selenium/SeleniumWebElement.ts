/// <reference path="../../typings/selenium-webdriver.d.ts" />
/// <reference path="../core/interfaces.ts" />

class SeleniumWebElement implements Interfaces.ITestableWebElement  {

    constructor(_baseObject: webdriver.WebElement, DefaultActionTimeout?: number) {
        this._baseObject = _baseObject;
        this.DefaultActionTimeout = DefaultActionTimeout || 60;
        this.SubElements = {};
    }

    _baseObject: webdriver.WebElement;
    DefaultActionTimeout: number;
    SubElements: { [index: string]: { [index: string]: string; }; };

    Clear(): webdriver.promise.Promise<void> {
        this.EnsureElementFocus();
        return this._baseObject.clear();
    }

    Type(text: string): webdriver.promise.Promise<void>;
    Type(text: string, element: Interfaces.ITestableWebElement): webdriver.promise.Promise<void>;
    Type(text: string, element?: Interfaces.ITestableWebElement): webdriver.promise.Promise<void> {
        if (element == null) {
            return this._baseObject.sendKeys(text);
        } else {
            return element.Type(text);
        }
    }

    Click(): webdriver.promise.Promise<void>;
    Click(element: Interfaces.ITestableWebElement): webdriver.promise.Promise<void>;
    Click(element?: Interfaces.ITestableWebElement): webdriver.promise.Promise<void> {
        if (element == null) {
            return this._baseObject.click();
        } else {
            return element.Click();
        }
    }

    Select(item: string, isValue: boolean): webdriver.promise.Promise<void>;
    Select(item: string, isValue: boolean, element: Interfaces.ITestableWebElement): webdriver.promise.Promise<void>;
    Select(item: string, isValue: boolean, element?: Interfaces.ITestableWebElement): webdriver.promise.Promise<void> {
        

        var optionProperties: { [key: string]: string } = { "tag": "option" }
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
            return element.Click()
                    .then(() => element.FindSubElement(optionProperties))
                    .then((subElement) => subElement.Click());
        }
    }

    Exists(targetSubElement: string, timeout: number): webdriver.promise.Promise<boolean> {
        var condition = this.FindSubElements(targetSubElement).then((elementsFound) => {
            if (elementsFound.length > 0) {
                return true;
            }
            return false;
        });

        return this._baseObject.getDriver().wait(condition, timeout);
    }

    WaitForAppear(timeout: number): webdriver.promise.Promise<boolean>;
    WaitForAppear(timeout: number, targetSubElement: string): webdriver.promise.Promise<boolean>;
    WaitForAppear(timeout: number, targetSubElement?: string): webdriver.promise.Promise<boolean> {
        var condition: webdriver.promise.Promise<boolean>;

        if (targetSubElement == null) {
            condition = this.IsDisplayed();
        } else {
            condition = this.FindSubElements(targetSubElement).then((elementsFound) => {
                if (elementsFound.length > 0) {
                    //Create promise that finds all the elements on the page and returns if they are displayed or not
                    return webdriver.promise.all(elementsFound.map((element) => element.IsDisplayed()));
                }
                return new webdriver.promise.Promise((accept) => accept([false]));
            }).then((results) => { //Verify all elements are displayed
                return results.filter((item) => item === false).length === 0;
            });
        }

        return this._baseObject.getDriver().wait(condition, timeout);
    }

    WaitForDisappear(timeout: number): webdriver.promise.Promise<boolean>;
    WaitForDisappear(timeout: number, targetSubElement: string): webdriver.promise.Promise<boolean>;
    WaitForDisappear(timeout: number, targetSubElement?: string): webdriver.promise.Promise<boolean> {
        var condition: webdriver.promise.Promise<boolean>;

        if (targetSubElement == null) {
            condition = this.IsDisplayed().then((result) => !result);
        } else {
            var condition = this.FindSubElements(targetSubElement).then((elementsFound) => {
                if (elementsFound.length > 0) {
                    //Create promise that finds all the elements on the page and returns if they are displayed or not
                    return webdriver.promise.all(elementsFound.map((element) => element.IsDisplayed()));
                }
                return new webdriver.promise.Promise((accept) => accept([false]));
            }).then((results) => { //Verify all elements are not displayed
                return results.filter((item) => item !== false).length === 0;
            });
        }

        return this._baseObject.getDriver().wait(condition, timeout);
    }

    IsDisplayed(): webdriver.promise.Promise<boolean> {
        this.EnsureElementFocus();
        return this._baseObject.isDisplayed();
    }

    IsSelected(): webdriver.promise.Promise<boolean> {
        this.EnsureElementFocus();
        return this._baseObject.isSelected();
    }

    SetChecked(value: boolean): webdriver.promise.Promise<void>;
    SetChecked(value: boolean, element: Interfaces.ITestableWebElement): webdriver.promise.Promise<void>;
    SetChecked(value: boolean, element?: Interfaces.ITestableWebElement): webdriver.promise.Promise<void> {
        if (element != null) {
            return element.SetChecked(value);
        }

        this.EnsureElementFocus();
        
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
        var waitCondition: webdriver.promise.Promise<boolean>;

        if (targetSubElement == null) {
            waitCondition = this.GetAttribute(attributeName).then((value) => condition(value));
        } else {
            waitCondition = this.FindSubElements(targetSubElement).then((elementsFound) => {
                if (elementsFound.length > 0) {
                    return webdriver.promise.all(elementsFound.map((element) => element.GetAttribute(attributeName)));
                }
                return new webdriver.promise.Promise((accept) => accept([null]));
            }).then((results) => {
                return results.every((item) => item != null && condition(item));
            });
        }

        return this._baseObject.getDriver().wait(waitCondition, timeout);}

    GetAttribute(attributeName: string): webdriver.promise.Promise<string> {
        this.EnsureElementFocus();
        return this._baseObject.getAttribute(attributeName);
    }

    GetTagName(): webdriver.promise.Promise<string> {
        this.EnsureElementFocus();
        return this._baseObject.getTagName();
    }

    InnerHtml(): webdriver.promise.Promise<string> {
        this.EnsureElementFocus();
        return this._baseObject.getInnerHtml();
    }

    GetText(): webdriver.promise.Promise<string> {
        this.EnsureElementFocus();
        return this._baseObject.getText();
    }

    Parent(levels: number): webdriver.promise.Promise<SeleniumWebElement> {

        this.EnsureElementFocus();
        var xpath = "..";

        for (var i = 1; i < levels; i++)
        {
            xpath += "/..";
        }

        return this._baseObject.findElement(webdriver.By.xpath(xpath)).then((element) => new SeleniumWebElement(element));
    }

    //
    //Search Context Methods
    //

    EnsureElementFocus() {
        return;
    }

    GetElementProperties(targetElement: string): { [key: string]: string; } {
        if (this.SubElements[targetElement]) {
            return this.SubElements[targetElement];
        }
        throw new Error(`Element name ${targetElement} is not a registered element name.`);
    }

    FindSubElement(targetSubElement: string): webdriver.promise.Promise<SeleniumWebElement>;
    FindSubElement(targetSubElement: string, timeout: number): webdriver.promise.Promise<SeleniumWebElement>;
    FindSubElement(subElementProperties: { [index: string]: string; }): webdriver.promise.Promise<SeleniumWebElement>;
    FindSubElement(subElementProperties: { [index: string]: string; }, timeout: number): webdriver.promise.Promise<SeleniumWebElement>;
    FindSubElement(targetSubElement: any, timeout?: number): webdriver.promise.Promise<SeleniumWebElement> {
        this.EnsureElementFocus();

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

    FindSubElementByProperties(subElementProperties: { [index: string]: string; }, timeout?: number): webdriver.promise.Promise<SeleniumWebElement> {
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
            return <any>FindExtensions.FindSubElement(this._baseObject, this.GetElementProperties(parentName))
                .then((parent) => parent.FindSubElement(elementProperties, timeout));
        }

        var condition = FindExtensions.FindSubElement(this._baseObject, subElementProperties).then((elementFound) => elementFound, () => null);

        return this._baseObject.getDriver().wait(condition, timeout);
    }

    FindSubElements(targetSubElement: string): webdriver.promise.Promise<SeleniumWebElement[]>;
    FindSubElements(targetSubElement: string, timeout: number): webdriver.promise.Promise<SeleniumWebElement[]>;
    FindSubElements(subElementProperties: { [index: string]: string; }): webdriver.promise.Promise<SeleniumWebElement[]>;
    FindSubElements(subElementProperties: { [index: string]: string; }, timeout: number): webdriver.promise.Promise<SeleniumWebElement[]>;
    FindSubElements(targetSubElement: any, timeout?: number): webdriver.promise.Promise<SeleniumWebElement[]> {
        this.EnsureElementFocus();

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

    FindSubElementsByProperties(subElementProperties: { [index: string]: string; }, timeout?: number): webdriver.promise.Promise<SeleniumWebElement[]> {
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

        var condition = FindExtensions.FindSubElements(this._baseObject, subElementProperties).then((elementsFound) => {
            if (elementsFound.length > 0) {
                return elementsFound;
            }
            return [];
        });

        return this._baseObject.getDriver().wait(condition, timeout);
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
