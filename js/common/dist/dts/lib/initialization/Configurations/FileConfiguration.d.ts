import dc = require("./DefaultConfiguration");
export interface IConfig {
    browser: string;
    server: string;
    port: string;
    output: string;
    environment: string;
    actiontimeout: number;
}
export declare class FileConfiguration extends dc.DefaultConfiguration {
    constructor();
}
