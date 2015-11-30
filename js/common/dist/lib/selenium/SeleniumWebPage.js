var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var swo = require("./SeleniumWebObject");
var swe = require("./SeleniumWebElement");
var ph = require("./Helpers/PromiseHelpers");
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
            .then(function (element) { return new swe.SeleniumWebElement(element); }); });
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
})(swo.SeleniumWebObject);
exports.SeleniumWebPage = SeleniumWebPage;
