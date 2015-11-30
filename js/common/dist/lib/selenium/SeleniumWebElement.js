var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var ph = require("./Helpers/PromiseHelpers");
var swo = require("./SeleniumWebObject");
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
})(swo.SeleniumWebObject);
exports.SeleniumWebElement = SeleniumWebElement;
