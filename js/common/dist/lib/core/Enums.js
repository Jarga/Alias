(function (EnvironmentType) {
    EnvironmentType[EnvironmentType["Dev"] = 0] = "Dev";
    EnvironmentType[EnvironmentType["QA"] = 1] = "QA";
    EnvironmentType[EnvironmentType["Stage"] = 2] = "Stage";
    EnvironmentType[EnvironmentType["DR"] = 4] = "DR";
    EnvironmentType[EnvironmentType["Production"] = 8] = "Production";
})(exports.EnvironmentType || (exports.EnvironmentType = {}));
var EnvironmentType = exports.EnvironmentType;
