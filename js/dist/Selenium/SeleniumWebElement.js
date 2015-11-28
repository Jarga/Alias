/// <reference path="../../typings/selenium-webdriver.d.ts" />
/// <reference path="../core/interfaces.ts" />
var SeleniumWebElement = (function () {
    function SeleniumWebElement(_baseObject, DefaultActionTimeout) {
        this._baseObject = _baseObject;
        this.DefaultActionTimeout = DefaultActionTimeout || 60;
        this.SubElements = {};
    }
    SeleniumWebElement.prototype.Clear = function () {
        this.EnsureElementFocus();
        return this._baseObject.clear();
    };
    SeleniumWebElement.prototype.Type = function (text, element) {
        if (element == null) {
            return this._baseObject.sendKeys(text);
        }
        else {
            return element.Type(text);
        }
    };
    SeleniumWebElement.prototype.Click = function (element) {
        if (element == null) {
            return this._baseObject.click();
        }
        else {
            return element.Click();
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
            return element.Click()
                .then(function () { return element.FindSubElement(optionProperties); })
                .then(function (subElement) { return subElement.Click(); });
        }
    };
    SeleniumWebElement.prototype.Exists = function (targetSubElement, timeout) {
        var condition = this.FindSubElements(targetSubElement).then(function (elementsFound) {
            if (elementsFound.length > 0) {
                return true;
            }
            return false;
        });
        return this._baseObject.getDriver().wait(condition, timeout);
    };
    SeleniumWebElement.prototype.WaitForAppear = function (timeout, targetSubElement) {
        var condition;
        if (targetSubElement == null) {
            condition = this.IsDisplayed();
        }
        else {
            condition = this.FindSubElements(targetSubElement).then(function (elementsFound) {
                if (elementsFound.length > 0) {
                    //Create promise that finds all the elements on the page and returns if they are displayed or not
                    return webdriver.promise.all(elementsFound.map(function (element) { return element.IsDisplayed(); }));
                }
                return new webdriver.promise.Promise(function (accept) { return accept([false]); });
            }).then(function (results) {
                return results.filter(function (item) { return item === false; }).length === 0;
            });
        }
        return this._baseObject.getDriver().wait(condition, timeout);
    };
    SeleniumWebElement.prototype.WaitForDisappear = function (timeout, targetSubElement) {
        var condition;
        if (targetSubElement == null) {
            condition = this.IsDisplayed().then(function (result) { return !result; });
        }
        else {
            var condition = this.FindSubElements(targetSubElement).then(function (elementsFound) {
                if (elementsFound.length > 0) {
                    //Create promise that finds all the elements on the page and returns if they are displayed or not
                    return webdriver.promise.all(elementsFound.map(function (element) { return element.IsDisplayed(); }));
                }
                return new webdriver.promise.Promise(function (accept) { return accept([false]); });
            }).then(function (results) {
                return results.filter(function (item) { return item !== false; }).length === 0;
            });
        }
        return this._baseObject.getDriver().wait(condition, timeout);
    };
    SeleniumWebElement.prototype.IsDisplayed = function () {
        this.EnsureElementFocus();
        return this._baseObject.isDisplayed();
    };
    SeleniumWebElement.prototype.IsSelected = function () {
        this.EnsureElementFocus();
        return this._baseObject.isSelected();
    };
    SeleniumWebElement.prototype.SetChecked = function (value, element) {
        var _this = this;
        if (element != null) {
            return element.SetChecked(value);
        }
        this.EnsureElementFocus();
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
        var waitCondition;
        if (targetSubElement == null) {
            waitCondition = this.GetAttribute(attributeName).then(function (value) { return condition(value); });
        }
        else {
            waitCondition = this.FindSubElements(targetSubElement).then(function (elementsFound) {
                if (elementsFound.length > 0) {
                    return webdriver.promise.all(elementsFound.map(function (element) { return element.GetAttribute(attributeName); }));
                }
                return new webdriver.promise.Promise(function (accept) { return accept([null]); });
            }).then(function (results) {
                return results.every(function (item) { return item != null && condition(item); });
            });
        }
        return this._baseObject.getDriver().wait(waitCondition, timeout);
    };
    SeleniumWebElement.prototype.GetAttribute = function (attributeName) {
        this.EnsureElementFocus();
        return this._baseObject.getAttribute(attributeName);
    };
    SeleniumWebElement.prototype.GetTagName = function () {
        this.EnsureElementFocus();
        return this._baseObject.getTagName();
    };
    SeleniumWebElement.prototype.InnerHtml = function () {
        this.EnsureElementFocus();
        return this._baseObject.getInnerHtml();
    };
    SeleniumWebElement.prototype.GetText = function () {
        this.EnsureElementFocus();
        return this._baseObject.getText();
    };
    SeleniumWebElement.prototype.Parent = function (levels) {
        this.EnsureElementFocus();
        var xpath = "..";
        for (var i = 1; i < levels; i++) {
            xpath += "/..";
        }
        return this._baseObject.findElement(webdriver.By.xpath(xpath)).then(function (element) { return new SeleniumWebElement(element); });
    };
    //
    //Search Context Methods
    //
    SeleniumWebElement.prototype.EnsureElementFocus = function () {
        return;
    };
    SeleniumWebElement.prototype.GetElementProperties = function (targetElement) {
        if (this.SubElements[targetElement]) {
            return this.SubElements[targetElement];
        }
        throw new Error("Element name " + targetElement + " is not a registered element name.");
    };
    SeleniumWebElement.prototype.FindSubElement = function (targetSubElement, timeout) {
        this.EnsureElementFocus();
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
    SeleniumWebElement.prototype.FindSubElementByProperties = function (subElementProperties, timeout) {
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
            return FindExtensions.FindSubElement(this._baseObject, this.GetElementProperties(parentName))
                .then(function (parent) { return parent.FindSubElement(elementProperties, timeout); });
        }
        var condition = FindExtensions.FindSubElement(this._baseObject, subElementProperties).then(function (elementFound) { return elementFound; }, function () { return null; });
        return this._baseObject.getDriver().wait(condition, timeout);
    };
    SeleniumWebElement.prototype.FindSubElements = function (targetSubElement, timeout) {
        this.EnsureElementFocus();
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
    SeleniumWebElement.prototype.FindSubElementsByProperties = function (subElementProperties, timeout) {
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
        var condition = FindExtensions.FindSubElements(this._baseObject, subElementProperties).then(function (elementsFound) {
            if (elementsFound.length > 0) {
                return elementsFound;
            }
            return [];
        });
        return this._baseObject.getDriver().wait(condition, timeout);
    };
    SeleniumWebElement.prototype.RegisterSubElement = function (name, elementProperties) {
        this.SubElements[name] = {};
        //lowercase all element properties
        for (var prop in elementProperties) {
            if (elementProperties.hasOwnProperty(prop)) {
                this.SubElements[name][prop.toLowerCase()] = elementProperties[prop];
            }
        }
    };
    return SeleniumWebElement;
})();
