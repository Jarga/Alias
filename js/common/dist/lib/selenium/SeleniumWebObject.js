var fh = require("./Helpers/FindHelpers");
var ph = require("./Helpers/PromiseHelpers");
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
