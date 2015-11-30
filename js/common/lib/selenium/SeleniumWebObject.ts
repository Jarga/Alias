import Interfaces = require("../core/Interfaces");
import fh = require("./Helpers/FindHelpers");
import ph = require("./Helpers/PromiseHelpers");

export abstract class SeleniumWebObject {
    abstract GetSearchContext(): webdriver.IWebElementFinders;
    abstract GetWeDriver(): webdriver.WebDriver;

    SubElements: { [index: string]: { [index: string]: string; }; };

    GetElementProperties(targetElement: string): { [key: string]: string; } {
        if (this.SubElements[targetElement]) {
            return this.SubElements[targetElement];
        }
        throw new Error(`Element name ${targetElement} is not a registered element name.`);
    }

    FindSubElement(targetSubElement: string): webdriver.promise.Promise<Interfaces.ITestableWebElement>;
    FindSubElement(targetSubElement: string, timeout: number): webdriver.promise.Promise<Interfaces.ITestableWebElement>;
    FindSubElement(subElementProperties: { [index: string]: string; }): webdriver.promise.Promise<Interfaces.ITestableWebElement>;
    FindSubElement(subElementProperties: { [index: string]: string; }, timeout: number): webdriver.promise.Promise<Interfaces.ITestableWebElement>;
    FindSubElement(targetSubElement: any, timeout?: number): webdriver.promise.Promise<Interfaces.ITestableWebElement> {
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

    FindSubElementByProperties(subElementProperties: { [index: string]: string; }, timeout?: number): webdriver.promise.Promise<Interfaces.ITestableWebElement> {
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
            return fh.FindHelpers.FindSubElement(this.GetSearchContext(), this.GetElementProperties(parentName))
                .then((parent) => parent.FindSubElement(elementProperties, timeout));
        }

        var condition = () => fh.FindHelpers.FindSubElement(this.GetSearchContext(), subElementProperties).then((elementFound) => elementFound, () => null);

        return this.GetWeDriver().wait(ph.PromiseHelpers.AsCondition(condition), timeout);
    }

    FindSubElements(targetSubElement: string): webdriver.promise.Promise<Interfaces.ITestableWebElement[]>;
    FindSubElements(targetSubElement: string, timeout: number): webdriver.promise.Promise<Interfaces.ITestableWebElement[]>;
    FindSubElements(subElementProperties: { [index: string]: string; }): webdriver.promise.Promise<Interfaces.ITestableWebElement[]>;
    FindSubElements(subElementProperties: { [index: string]: string; }, timeout: number): webdriver.promise.Promise<Interfaces.ITestableWebElement[]>;
    FindSubElements(targetSubElement: any, timeout?: number): webdriver.promise.Promise<Interfaces.ITestableWebElement[]> {
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

    FindSubElementsByProperties(subElementProperties: { [index: string]: string; }, timeout?: number): webdriver.promise.Promise<Interfaces.ITestableWebElement[]> {
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

        var condition = () => fh.FindHelpers.FindSubElements(this.GetSearchContext(), subElementProperties).then((elementsFound) => {
            if (elementsFound.length > 0) {
                return elementsFound;
            }
            return [];
        });

        return this.GetWeDriver().wait(ph.PromiseHelpers.AsCondition(condition), timeout);
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

