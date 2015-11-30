export class PromiseHelpers {
    static AsCondition<T>(promiseFunc: () => webdriver.promise.Promise<T>, message?: string): webdriver.until.Condition<T> {
        var promiseResult: T;

        promiseFunc()
            .then((result) => {
                promiseResult = result;
            });

        return new webdriver.until.Condition(message || "promise returns successfully", (driver) => {
            if (!promiseResult) {
                driver.sleep(200); //prevent processor overloading of continuously evaluating the condition
            }
            return promiseResult;
        });
    }
}