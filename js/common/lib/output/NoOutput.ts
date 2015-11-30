import Interfaces = require("../core/Interfaces");

export class NoOutput implements Interfaces.ITestOutput {
    WriteLine(message: string, base64Image?: string): void {
        return;
    }
}

