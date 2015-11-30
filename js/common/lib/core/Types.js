var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var webdriver = require("selenium-webdriver");
var ph = require("../selenium/Helpers/PromiseHelpers");
var fh = require("../selenium/Helpers/FindHelpers");
var SeleniumWebObject = (function () {
    function SeleniumWebObject() {
    }
    SeleniumWebObject.prototype.GetElementProperties = function (targetElement) {
        if (this.SubElements[targetElement]) {
            return this.SubElements[targetElement];
        }
        throw new Error("Element name " + targetElement + " is not a registered element name.");
    };
    SeleniumWebObject.prototype.FindSubElement = function (targetSubElement, timeout) {
        //If argument is a string then the target was a pre-defined sub element
        if (typeof targetSubElement === "string") {
            return this.FindSubElementByProperties(this.GetElementProperties(targetSubElement), timeout);
        }
        var elementProperties;
        if (typeof targetSubElement === typeof elementProperties) {
            return this.FindSubElementByProperties(targetSubElement, timeout);
        }
        throw new Error("Invalid target of type " + typeof targetSubElement + " found for findSubElement, must be string or Dictionary: { [index: string]: string; }");
    };
    SeleniumWebObject.prototype.FindSubElementByProperties = function (subElementProperties, timeout) {
        var _this = this;
        if (!!subElementProperties["parentelement"]) {
            var parentName = subElementProperties["parentelement"];
            var elementProperties = {};
            for (var prop in subElementProperties) {
                if (subElementProperties.hasOwnProperty(prop)) {
                    elementProperties[prop] = subElementProperties[prop];
                }
            }
            delete elementProperties["parentelement"];
            //Compiler complains about this not being a webdriver.promise.Promise<SeleniumWebElement> when it actually is
            return fh.FindHelpers.FindSubElement(this.GetSearchContext(), this.GetElementProperties(parentName))
                .then(function (parent) { return parent.FindSubElement(elementProperties, timeout); });
        }
        var condition = function () { return fh.FindHelpers.FindSubElement(_this.GetSearchContext(), subElementProperties).then(function (elementFound) { return elementFound; }, function () { return null; }); };
        return this.GetWeDriver().wait(ph.PromiseHelpers.AsCondition(condition), timeout);
    };
    SeleniumWebObject.prototype.FindSubElements = function (targetSubElement, timeout) {
        //If argument is a string then the target was a pre-defined sub element
        if (typeof targetSubElement === "string") {
            return this.FindSubElementsByProperties(this.GetElementProperties(targetSubElement), timeout);
        }
        var elementProperties;
        if (typeof targetSubElement === typeof elementProperties) {
            return this.FindSubElementsByProperties(targetSubElement, timeout);
        }
        throw new Error("Invalid target of type " + typeof targetSubElement + " found for findSubElements, must be string or Dictionary: { [index: string]: string; }");
    };
    SeleniumWebObject.prototype.FindSubElementsByProperties = function (subElementProperties, timeout) {
        var _this = this;
        if (!!subElementProperties["parentelement"]) {
            var elementProperties = {};
            for (var prop in subElementProperties) {
                if (subElementProperties.hasOwnProperty(prop)) {
                    elementProperties[prop] = subElementProperties[prop];
                }
            }
            delete elementProperties["parentelement"];
            return this.FindSubElementsByProperties(elementProperties, timeout);
        }
        var condition = function () { return fh.FindHelpers.FindSubElements(_this.GetSearchContext(), subElementProperties).then(function (elementsFound) {
            if (elementsFound.length > 0) {
                return elementsFound;
            }
            return [];
        }); };
        return this.GetWeDriver().wait(ph.PromiseHelpers.AsCondition(condition), timeout);
    };
    SeleniumWebObject.prototype.RegisterSubElement = function (name, elementProperties) {
        this.SubElements[name] = {};
        //lowercase all element properties
        for (var prop in elementProperties) {
            if (elementProperties.hasOwnProperty(prop)) {
                this.SubElements[name][prop.toLowerCase()] = elementProperties[prop];
            }
        }
    };
    return SeleniumWebObject;
})();
exports.SeleniumWebObject = SeleniumWebObject;
var SeleniumWebPage = (function (_super) {
    __extends(SeleniumWebPage, _super);
    function SeleniumWebPage(driver) {
        var _this = this;
        _super.call(this);
        this.Driver = driver;
        //we do not want the driver to do work until we record what window handle we are working with
        this.Driver.wait(this.Driver.getWindowHandle().then(function (handle) { return _this.WindowHandle = handle; }));
    }
    SeleniumWebPage.prototype.GetSearchContext = function () {
        return this.Driver;
    };
    SeleniumWebPage.prototype.GetWeDriver = function () {
        return this.Driver;
    };
    SeleniumWebPage.prototype.EnsureFocus = function () {
        return this.Driver.switchTo().window(this.WindowHandle);
    };
    SeleniumWebPage.prototype.SetActiveWindow = function (windowUrlContains, timeout) {
        var _this = this;
        var setActiveWindow = function () { return _this.Driver.getAllWindowHandles().then(function (handles) {
            //Look through all active window handles for a window that matches the given
            var promises = [];
            for (var i = 0; i < handles.length; i++) {
                promises.push(_this.Driver.switchTo().window(handles[i])
                    .then(function () { return _this.Driver.getCurrentUrl(); })
                    .then(function (url) {
                    if (url.indexOf(windowUrlContains) > -1) {
                        return true;
                    }
                    return false;
                }));
            }
            //return results of all the attempted window switches
            return webdriver.promise.all(promises);
        }).then(function (results) {
            if (results.filter(function (r) { return r; }).length === 0) {
                _this.Driver.switchTo().window(_this.WindowHandle);
                return false;
            }
            return true;
        }); };
        return this.Driver.wait(ph.PromiseHelpers.AsCondition(setActiveWindow), timeout).then(function (result) {
            if (!result) {
                throw new Error("Failed to switch to target window.");
            }
        });
    };
    SeleniumWebPage.prototype.Open = function (url) {
        var _this = this;
        return this.EnsureFocus()
            .then(function () { return _this.Driver.get(url); });
    };
    SeleniumWebPage.prototype.Close = function () {
        var _this = this;
        return this.EnsureFocus()
            .then(function () { return _this.Driver.close(); });
    };
    SeleniumWebPage.prototype.Maximize = function () {
        var _this = this;
        return this.EnsureFocus()
            .then(function () { return _this.Driver.manage().window().maximize(); });
    };
    SeleniumWebPage.prototype.GetCurrentUrl = function () {
        var _this = this;
        return this.EnsureFocus()
            .then(function () { return _this.Driver.getCurrentUrl(); });
    };
    SeleniumWebPage.prototype.GetScreenshot = function () {
        var _this = this;
        return this.EnsureFocus()
            .then(function () { return _this.Driver.takeScreenshot(); });
    };
    SeleniumWebPage.prototype.GetRootElement = function () {
        var _this = this;
        return this.EnsureFocus()
            .then(function () { return _this.Driver.findElement(webdriver.By.tagName("html"))
            .then(function (element) { return new SeleniumWebElement(element); }); });
    };
    SeleniumWebPage.prototype.Quit = function () {
        return this.Driver.quit();
    };
    SeleniumWebPage.prototype.Clear = function () {
        throw new Error("You can't clear the browser.");
    };
    SeleniumWebPage.prototype.Type = function (text, element) {
        return this.GetRootElement().then(function (root) { return root.Type(text, element); });
    };
    SeleniumWebPage.prototype.Click = function (element) {
        return this.GetRootElement().then(function (root) { return root.Click(element); });
    };
    SeleniumWebPage.prototype.Select = function (item, isValue, element) {
        return this.GetRootElement().then(function (root) { return root.Select(item, isValue, element); });
    };
    SeleniumWebPage.prototype.Exists = function (targetSubElement, timeout) {
        return this.GetRootElement().then(function (root) { return root.Exists(targetSubElement, timeout); });
    };
    SeleniumWebPage.prototype.WaitForAppear = function (timeout, targetSubElement) {
        return this.GetRootElement().then(function (root) { return root.WaitForAppear(timeout, targetSubElement); });
    };
    SeleniumWebPage.prototype.WaitForDisappear = function (timeout, targetSubElement) {
        return this.GetRootElement().then(function (root) { return root.WaitForDisappear(timeout, targetSubElement); });
    };
    SeleniumWebPage.prototype.IsDisplayed = function () {
        return this.GetRootElement().then(function (root) { return root.IsDisplayed(); });
    };
    SeleniumWebPage.prototype.IsSelected = function () {
        return this.GetRootElement().then(function (root) { return root.IsSelected(); });
    };
    SeleniumWebPage.prototype.SetChecked = function (value, element) {
        return this.GetRootElement().then(function (root) { return root.SetChecked(value, element); });
    };
    SeleniumWebPage.prototype.WaitForAttributeState = function (attributeName, condition, timeout, targetSubElement) {
        return this.GetRootElement().then(function (root) { return root.WaitForAttributeState(attributeName, condition, timeout, targetSubElement); });
    };
    SeleniumWebPage.prototype.GetCssValue = function (propertyName) {
        return this.GetRootElement().then(function (root) { return root.GetCssValue(propertyName); });
    };
    SeleniumWebPage.prototype.GetAttribute = function (attributeName) {
        return this.GetRootElement().then(function (root) { return root.GetAttribute(attributeName); });
    };
    SeleniumWebPage.prototype.GetTagName = function () {
        return this.GetRootElement().then(function (root) { return root.GetTagName(); });
    };
    SeleniumWebPage.prototype.InnerHtml = function () {
        return this.GetRootElement().then(function (root) { return root.InnerHtml(); });
    };
    SeleniumWebPage.prototype.GetText = function () {
        return this.GetRootElement().then(function (root) { return root.GetText(); });
    };
    SeleniumWebPage.prototype.Parent = function (levels) {
        throw new Error("No parent to the root element.");
    };
    return SeleniumWebPage;
})(SeleniumWebObject);
exports.SeleniumWebPage = SeleniumWebPage;
var SeleniumWebElement = (function (_super) {
    __extends(SeleniumWebElement, _super);
    function SeleniumWebElement(_baseObject, DefaultActionTimeout) {
        _super.call(this);
        this._baseObject = _baseObject;
        this.DefaultActionTimeout = DefaultActionTimeout || 60;
        this.SubElements = {};
    }
    SeleniumWebElement.prototype.GetSearchContext = function () {
        return this._baseObject;
    };
    SeleniumWebElement.prototype.GetWeDriver = function () {
        return this._baseObject.getDriver();
    };
    SeleniumWebElement.prototype.Clear = function () {
        return this._baseObject.clear();
    };
    SeleniumWebElement.prototype.Type = function (text, element) {
        if (element == null) {
            return this._baseObject.sendKeys(text);
        }
        else {
            return this.FindSubElement(element).then(function (element) { return element.Type(text); });
        }
    };
    SeleniumWebElement.prototype.Click = function (element) {
        if (element == null) {
            return this._baseObject.click();
        }
        else {
            return this.FindSubElement(element).then(function (element) { return element.Click(); });
        }
    };
    SeleniumWebElement.prototype.Select = function (item, isValue, element) {
        var _this = this;
        var optionProperties = { "tag": "option" };
        if (isValue) {
            optionProperties["value"] = item;
        }
        else {
            optionProperties["text"] = item;
        }
        if (element == null) {
            return this.Click()
                .then(function () { return _this.FindSubElement(optionProperties); })
                .then(function (element) { return element.Click(); });
        }
        else {
            return this.FindSubElement(element).then(function (element) { return element.Select(item, isValue); });
        }
    };
    SeleniumWebElement.prototype.Exists = function (targetSubElement, timeout) {
        var _this = this;
        var exists = function () { return _this.FindSubElements(targetSubElement).then(function (elementsFound) {
            if (elementsFound.length > 0) {
                return true;
            }
            return false;
        }); };
        return this._baseObject.getDriver().wait(ph.PromiseHelpers.AsCondition(exists), timeout);
    };
    SeleniumWebElement.prototype.WaitForAppear = function (timeout, targetSubElement) {
        var _this = this;
        var condition;
        if (targetSubElement == null) {
            condition = function () { return _this.IsDisplayed(); };
        }
        else {
            condition = function () { return _this.FindSubElements(targetSubElement).then(function (elementsFound) {
                if (elementsFound.length > 0) {
                    //Create promise that finds all the elements on the page and returns if they are displayed or not
                    return webdriver.promise.all(elementsFound.map(function (element) { return element.IsDisplayed(); }));
                }
                return new webdriver.promise.Promise(function (accept) { return accept([false]); });
            }).then(function (results) {
                return results.filter(function (item) { return item === false; }).length === 0;
            }); };
        }
        return this._baseObject.getDriver().wait(ph.PromiseHelpers.AsCondition(condition), timeout);
    };
    SeleniumWebElement.prototype.WaitForDisappear = function (timeout, targetSubElement) {
        var _this = this;
        var condition;
        if (targetSubElement == null) {
            condition = function () { return _this.IsDisplayed().then(function (result) { return !result; }); };
        }
        else {
            condition = function () { return _this.FindSubElements(targetSubElement).then(function (elementsFound) {
                if (elementsFound.length > 0) {
                    //Create promise that finds all the elements on the page and returns if they are displayed or not
                    return webdriver.promise.all(elementsFound.map(function (element) { return element.IsDisplayed(); }));
                }
                return new webdriver.promise.Promise(function (accept) { return accept([false]); });
            }).then(function (results) {
                return results.filter(function (item) { return item !== false; }).length === 0;
            }); };
        }
        return this._baseObject.getDriver().wait(ph.PromiseHelpers.AsCondition(condition), timeout);
    };
    SeleniumWebElement.prototype.IsDisplayed = function () {
        return this._baseObject.isDisplayed();
    };
    SeleniumWebElement.prototype.IsSelected = function () {
        return this._baseObject.isSelected();
    };
    SeleniumWebElement.prototype.SetChecked = function (value, element) {
        var _this = this;
        if (element != null) {
            return this.FindSubElement(element).then(function (element) { return element.SetChecked(value); });
        }
        return this.IsSelected()
            .then(function (selected) {
            if (!selected) {
                _this.Click();
            }
            return _this.IsSelected();
        }).then(function (selected) {
            if (!selected) {
                _this._baseObject.sendKeys(webdriver.Key.ENTER);
            }
            return _this.IsSelected();
        }).then(function (selected) {
            if (!selected) {
                throw new Error("Failed to set element to checked!");
            }
        });
    };
    SeleniumWebElement.prototype.WaitForAttributeState = function (attributeName, condition, timeout, targetSubElement) {
        var _this = this;
        var waitCondition;
        if (targetSubElement == null) {
            waitCondition = function () { return _this.GetAttribute(attributeName).then(function (value) { return condition(value); }); };
        }
        else {
            waitCondition = function () { return _this.FindSubElements(targetSubElement).then(function (elementsFound) {
                if (elementsFound.length > 0) {
                    return webdriver.promise.all(elementsFound.map(function (element) { return element.GetAttribute(attributeName); }));
                }
                return new webdriver.promise.Promise(function (accept) { return accept([null]); });
            }).then(function (results) {
                return results.every(function (item) { return item != null && condition(item); });
            }); };
        }
        return this._baseObject.getDriver().wait(ph.PromiseHelpers.AsCondition(waitCondition), timeout);
    };
    SeleniumWebElement.prototype.GetCssValue = function (propertyName) {
        return this._baseObject.getCssValue(propertyName);
    };
    SeleniumWebElement.prototype.GetAttribute = function (attributeName) {
        return this._baseObject.getAttribute(attributeName);
    };
    SeleniumWebElement.prototype.GetTagName = function () {
        return this._baseObject.getTagName();
    };
    SeleniumWebElement.prototype.InnerHtml = function () {
        return this._baseObject.getInnerHtml();
    };
    SeleniumWebElement.prototype.GetText = function () {
        return this._baseObject.getText();
    };
    SeleniumWebElement.prototype.Parent = function (levels) {
        var xpath = "..";
        for (var i = 1; i < levels; i++) {
            xpath += "/..";
        }
        return this._baseObject.findElement(webdriver.By.xpath(xpath)).then(function (element) { return new SeleniumWebElement(element); });
    };
    return SeleniumWebElement;
})(SeleniumWebObject);
exports.SeleniumWebElement = SeleniumWebElement;
var WebPage = (function (_super) {
    __extends(WebPage, _super);
    function WebPage(testConfiguration) {
        _super.call(this, testConfiguration.Driver);
        this.TestConfiguration = testConfiguration;
    }
    WebPage.prototype.New = function (type) {
        return new type(this.TestConfiguration);
    };
    WebPage.prototype.EnsureElementLoaded = function (verificationElement, successText, failedText, takeScreenshotOnSuccess) {
        var _this = this;
        this.FindSubElement(verificationElement, 120)
            .then(function () {
            if (successText) {
                if (takeScreenshotOnSuccess) {
                    _this.GetScreenshot().then(function (screenshot) {
                        _this.TestConfiguration.Output.WriteLine(successText, screenshot);
                    });
                }
                else {
                    _this.TestConfiguration.Output.WriteLine(successText);
                }
            }
        }, function (error) {
            if (failedText) {
                _this.GetScreenshot().then(function (screenshot) {
                    _this.TestConfiguration.Output.WriteLine(failedText, screenshot);
                }).then(function () {
                    throw error;
                });
            }
            else {
                throw error;
            }
        });
    };
    return WebPage;
})(SeleniumWebPage);
exports.WebPage = WebPage;
var WebElement = (function (_super) {
    __extends(WebElement, _super);
    function WebElement() {
        _super.apply(this, arguments);
    }
    return WebElement;
})(SeleniumWebElement);
exports.WebElement = WebElement;
