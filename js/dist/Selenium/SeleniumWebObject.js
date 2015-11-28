/// <reference path="../../typings/selenium-webdriver.d.ts" />
/// <reference path="../core/interfaces.ts" />
var SeleniumWebObject = (function () {
    function SeleniumWebObject(SearchContext, DefaultActionTimeout) {
        this.DefaultActionTimeout = DefaultActionTimeout || 60;
        this.SearchContext = SearchContext;
    }
    SeleniumWebObject.prototype.GetElementProperties = function (targetElement) { throw new Error("Not implemented"); };
    SeleniumWebObject.prototype.FindSubElement = function (targetSubElement, timeout) { throw new Error("Not implemented"); };
    SeleniumWebObject.prototype.FindSubElements = function (targetSubElement, timeout) { throw new Error("Not implemented"); };
    SeleniumWebObject.prototype.RegisterSubElement = function (name, elementProperties) { };
    return SeleniumWebObject;
})();
