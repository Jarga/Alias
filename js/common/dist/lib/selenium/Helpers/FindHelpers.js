var swe = require("../SeleniumWebElement");
var FindHelpers = (function () {
    function FindHelpers() {
    }
    FindHelpers.BuildBy = function (key, value) {
        var isContains = false;
        if (value.indexOf("contains=") === 0) {
            value = value.substring(9);
            isContains = true;
        }
        switch (key) {
            case "class":
            case "classname":
                return webdriver.By.className(value);
            case "cssselector":
            case "css":
                return webdriver.By.css(value);
            case "id":
                return isContains
                    ? webdriver.By.css("[id*=" + value + "]")
                    : webdriver.By.id(value);
            case "linktext":
                return isContains
                    ? webdriver.By.partialLinkText(value)
                    : webdriver.By.linkText(value);
            case "name":
                return webdriver.By.name(value);
            case "partiallinktext":
                return webdriver.By.partialLinkText(value);
            case "tagname":
                //return By.TagName(value); //TODO: Figure out why this by clause sucks (e.g. Does not actually work)!
                return webdriver.By.xpath(".//" + value);
            case "xpath":
                return webdriver.By.xpath(value);
            case "text":
            case "innertext":
                return isContains
                    ? webdriver.By.xpath(".//*[contains(normalize-space(.), \"" + value + "\")]")
                    : webdriver.By.xpath(".//*[normalize-space(.)=\"" + value + "\"]");
            case "type":
                return webdriver.By.xpath(".//*[@type='" + value + "']");
            case "href":
                return isContains
                    ? webdriver.By.css("[href*='" + value + "']")
                    : webdriver.By.css("[href='" + value + "']");
            default:
                throw "Invalid By Clause property " + key + " for Selenium Element!";
        }
    };
    FindHelpers.BuildCompositeXPathBy = function (keyValueDictionary) {
        var keys = [];
        for (var dictKey in keyValueDictionary) {
            if (keyValueDictionary.hasOwnProperty(dictKey)) {
                keys.push(dictKey);
            }
        }
        if (keys.length === 0) {
            throw "Invalid Element Properties passed to build By Clause!";
        }
        var tagKeys = keys.filter(function (k) { return k === "tag" || k === "tagname"; });
        var xpathExpression = ".//" + (tagKeys.length > 0 ? keyValueDictionary[tagKeys[0]] : "*");
        if (tagKeys.length > 0) {
            keys.splice(keys.indexOf(tagKeys[0]), 1);
        }
        for (var key in keys) {
            if (keys.hasOwnProperty(key)) {
                var value = keyValueDictionary[key];
                var isContains = false;
                if (value.indexOf("contains=") === 0) {
                    value = value.substring(9);
                    isContains = true;
                }
                switch (key) {
                    case "class":
                    case "classname":
                        xpathExpression += FindHelpers.GetAttributeXPath("class", value, isContains);
                        continue;
                    case "id":
                        xpathExpression += FindHelpers.GetAttributeXPath("id", value, isContains);
                        continue;
                    case "value":
                        xpathExpression += FindHelpers.GetAttributeXPath("value", value, isContains);
                        continue;
                    case "name":
                        xpathExpression += FindHelpers.GetAttributeXPath("name", value, isContains);
                        continue;
                    case "text":
                    case "innertext":
                        xpathExpression += (isContains
                            ? "[contains(normalize-space(.), \"" + value + "\")]"
                            : "[normalize-space(.)=\"" + value + "\"]");
                        continue;
                    case "type":
                        xpathExpression += FindHelpers.GetAttributeXPath("type", value, isContains);
                        continue;
                    case "href":
                        xpathExpression += (isContains
                            ? "[contains(@href, \"" + value + "\")]"
                            : "[@href=\"" + value + "\"]");
                        continue;
                    default:
                        throw "Invalid Composite By Clause property " + key + " for Selenium Element!";
                }
            }
        }
        return webdriver.By.xpath(xpathExpression);
    };
    FindHelpers.GetAttributeXPath = function (attribute, value, isContains) {
        return isContains ? "[contains(@" + attribute + ", '" + value + "')]" : "[@" + attribute + "='" + value + "']";
    };
    FindHelpers.FindSubElement = function (baseObject, elementProperties) {
        return baseObject.findElement(this.BuldUniversalByClause(elementProperties))
            .then(function (value) { return new swe.SeleniumWebElement(value); });
    };
    FindHelpers.FindSubElements = function (baseObject, elementProperties) {
        return (baseObject.findElements(this.BuldUniversalByClause(elementProperties))
            .then(function (results) { return (results.length < 1 ? [] : results.map(function (e) { return new swe.SeleniumWebElement(e); })); }));
    };
    FindHelpers.BuldUniversalByClause = function (elementProperties) {
        var keys = [];
        for (var key in elementProperties) {
            if (elementProperties.hasOwnProperty(key)) {
                keys.push(key);
            }
        }
        if (keys.length === 0) {
            throw "Element was not registered or has no properties assigned!";
        }
        if (keys.length === 1) {
            return FindHelpers.BuildBy(keys[0], elementProperties[keys[0]]);
        }
        else {
            return FindHelpers.BuildCompositeXPathBy(elementProperties);
        }
    };
    return FindHelpers;
})();
exports.FindHelpers = FindHelpers;
