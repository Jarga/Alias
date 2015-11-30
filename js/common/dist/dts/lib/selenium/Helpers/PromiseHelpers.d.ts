export declare class PromiseHelpers {
    static AsCondition<T>(promiseFunc: () => webdriver.promise.Promise<T>, message?: string): webdriver.until.Condition<T>;
}
