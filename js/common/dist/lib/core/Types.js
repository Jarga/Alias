var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var swp = require("../selenium/SeleniumWebPage");
var swe = require("../selenium/SeleniumWebElement");
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
})(swp.SeleniumWebPage);
exports.WebPage = WebPage;
var WebElement = (function (_super) {
    __extends(WebElement, _super);
    function WebElement() {
        _super.apply(this, arguments);
    }
    return WebElement;
})(swe.SeleniumWebElement);
exports.WebElement = WebElement;
