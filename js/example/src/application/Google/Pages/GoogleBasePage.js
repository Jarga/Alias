var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var Common = require("../../../../../common/lib/index");
var Google = require("./SearchResults/WebSearchResults");
var GoogleBasePage = (function (_super) {
    __extends(GoogleBasePage, _super);
    function GoogleBasePage(testConfiguration) {
        _super.call(this, testConfiguration);
        this.RegisterSubElement("Query Box", { name: "q" });
        this.RegisterSubElement("Submit Query", { css: "[name=btnK], [name=btnG]" });
        this.EnsureElementLoaded("Query Box", null, "Google page failed to load");
    }
    GoogleBasePage.prototype.Search = function (value) {
        this.Type(value, "Query Box");
        this.Click("Submit Query");
        return this.New(Google.WebSearchResults);
    };
    return GoogleBasePage;
})(Common.WebPage);
exports.GoogleBasePage = GoogleBasePage;
