var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var Common = require("../../../../../common/lib/index");
var Google = require("./GoogleBasePage");
var GoogleSession = (function (_super) {
    __extends(GoogleSession, _super);
    function GoogleSession(testConfiguration) {
        _super.call(this, testConfiguration);
    }
    GoogleSession.prototype.Start = function () {
        this.Open(this.GetEnvironmentUrl());
        this.Maximize();
        return this.New(Google.GoogleBasePage);
    };
    GoogleSession.prototype.GetEnvironmentUrl = function () {
        switch (this.TestConfiguration.EnvironmentType) {
            default:
                return "https://www.google.com/";
        }
    };
    return GoogleSession;
})(Common.WebPage);
exports.GoogleSession = GoogleSession;
