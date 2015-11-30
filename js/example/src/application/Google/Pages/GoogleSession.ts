import Common = require("../../../../../common/lib/index");
import Google = require("./GoogleBasePage");


export class GoogleSession extends Common.WebPage {
    constructor(testConfiguration: Common.ITestConfiguration) {
        super(testConfiguration);
    }

    Start(): Google.GoogleBasePage {
        this.Open(this.GetEnvironmentUrl());
        this.Maximize();

        return this.New(Google.GoogleBasePage);
    }

    GetEnvironmentUrl(): string {
        switch (this.TestConfiguration.EnvironmentType) {
            default:
                return "https://www.google.com/";
        }
    }
}
