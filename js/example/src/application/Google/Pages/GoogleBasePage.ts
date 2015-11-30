import Common = require("../../../../../common/lib/index");
import Google = require("./SearchResults/WebSearchResults");

export class GoogleBasePage extends Common.WebPage {
    constructor(testConfiguration: Common.ITestConfiguration) {
        super(testConfiguration);

        this.RegisterSubElement("Query Box", { name: "q" });
        this.RegisterSubElement("Submit Query", { css: "[name=btnK], [name=btnG]" });

        this.EnsureElementLoaded("Query Box", null, "Google page failed to load");
    }

    Search(value: string): Google.WebSearchResults {
        this.Type(value, "Query Box");
        this.Click("Submit Query");

        return this.New(Google.WebSearchResults);
    }
}
