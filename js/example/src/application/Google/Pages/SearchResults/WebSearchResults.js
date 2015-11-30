var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var Google = require("../GoogleBasePage");
var WebSearchResults = (function (_super) {
    __extends(WebSearchResults, _super);
    function WebSearchResults(testConfig) {
        _super.call(this, testConfig);
        this.RegisterSubElement("Submit Query", { name: "btnG" });
        this.RegisterSubElement("Selected Web Tab", { TagName: "div", Text: "Web", "class": "contains=hdtb-msel" });
        this.EnsureElementLoaded("Selected Web Tab", "Google web search page loaded.", "Google web search page failed to load.");
    }
    return WebSearchResults;
})(Google.GoogleBasePage);
exports.WebSearchResults = WebSearchResults;
