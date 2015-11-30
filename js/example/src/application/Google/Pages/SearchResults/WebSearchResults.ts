import Common = require("../../../../../../common/lib/index");
import Google = require("../GoogleBasePage");

export class WebSearchResults extends Google.GoogleBasePage {
    constructor(testConfig: Common.ITestConfiguration) {
        super(testConfig);
        this.RegisterSubElement("Submit Query", { name: "btnG" });
        this.RegisterSubElement("Selected Web Tab", { TagName: "div", Text: "Web", "class": "contains=hdtb-msel" });

        this.EnsureElementLoaded("Selected Web Tab", "Google web search page loaded.", "Google web search page failed to load.");
    }
}
