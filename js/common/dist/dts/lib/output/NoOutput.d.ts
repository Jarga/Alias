import Interfaces = require("../core/Interfaces");
export declare class NoOutput implements Interfaces.ITestOutput {
    WriteLine(message: string, base64Image?: string): void;
}
