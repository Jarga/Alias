var SeleniumWebPage = (function () {
    function SeleniumWebPage(Driver) {
        this.Driver = Driver;
    }
    SeleniumWebPage.prototype.AsNew = function () { throw new Error("Not implemented"); };
    SeleniumWebPage.prototype.EnsureFocus = function () { };
    SeleniumWebPage.prototype.SetActiveWindow = function (windowUrlContains, timeout) { };
    SeleniumWebPage.prototype.Open = function (url) { };
    SeleniumWebPage.prototype.Close = function () { };
    SeleniumWebPage.prototype.Maximize = function () { };
    SeleniumWebPage.prototype.GetCurrentUrl = function () { throw new Error("Not implemented"); };
    SeleniumWebPage.prototype.GetScreenshot = function () { throw new Error("Not implemented"); };
    SeleniumWebPage.prototype.Clear = function () { throw new Error("Not implemented"); };
    SeleniumWebPage.prototype.Type = function (text, element) { throw new Error("Not implemented"); };
    SeleniumWebPage.prototype.Click = function (element) { throw new Error("Not implemented"); };
    SeleniumWebPage.prototype.Select = function (item, isValue, element) { throw new Error("Not implemented"); };
    SeleniumWebPage.prototype.Exists = function (targetSubElement, timeout) { throw new Error("Not implemented"); };
    SeleniumWebPage.prototype.WaitForAppear = function (timeout, targetSubElement) { throw new Error("Not implemented"); };
    SeleniumWebPage.prototype.WaitForDisappear = function (timeout, targetSubElement) { throw new Error("Not implemented"); };
    SeleniumWebPage.prototype.IsDisplayed = function () { throw new Error("Not implemented"); };
    SeleniumWebPage.prototype.IsSelected = function () { throw new Error("Not implemented"); };
    SeleniumWebPage.prototype.SetChecked = function (value, element) { throw new Error("Not implemented"); };
    SeleniumWebPage.prototype.WaitForAttributeState = function (attributeName, condition, timeout, targetSubElement) { throw new Error("Not implemented"); };
    SeleniumWebPage.prototype.GetAttribute = function (attributeName) { throw new Error("Not implemented"); };
    SeleniumWebPage.prototype.GetTagName = function () { throw new Error("Not implemented"); };
    SeleniumWebPage.prototype.InnerHtml = function () { throw new Error("Not implemented"); };
    SeleniumWebPage.prototype.GetText = function () { throw new Error("Not implemented"); };
    SeleniumWebPage.prototype.Parent = function (levels) { throw new Error("Not implemented"); };
    SeleniumWebPage.prototype.GetElementProperties = function (targetElement) { throw new Error("Not implemented"); };
    SeleniumWebPage.prototype.FindSubElement = function (targetSubElement, timeout) { throw new Error("Not implemented"); };
    SeleniumWebPage.prototype.FindSubElements = function (targetSubElement, timeout) { throw new Error("Not implemented"); };
    SeleniumWebPage.prototype.RegisterSubElement = function (name, elementProperties) { };
    return SeleniumWebPage;
})();
