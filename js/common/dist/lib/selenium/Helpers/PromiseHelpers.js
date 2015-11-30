var PromiseHelpers = (function () {
    function PromiseHelpers() {
    }
    PromiseHelpers.AsCondition = function (promiseFunc, message) {
        var promiseResult;
        promiseFunc()
            .then(function (result) {
            promiseResult = result;
        });
        return new webdriver.until.Condition(message || "promise returns successfully", function (driver) {
            if (!promiseResult) {
                driver.sleep(200); //prevent processor overloading of continuously evaluating the condition
            }
            return promiseResult;
        });
    };
    return PromiseHelpers;
})();
exports.PromiseHelpers = PromiseHelpers;
