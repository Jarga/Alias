/**
 * Instance is an App controller. Automatically creates
 * model. Creates view if none given.
 */
var Controller = (function () {
    function Controller(greeting, view) {
        this.model = new Model(greeting);
        this.view = (view || new View());
    }
    Controller.prototype.greet = function () {
        this.view.display(this.model.getGreeting());
    };
    return Controller;
})();
exports.Controller = Controller;
/**
 * Private class. Instance represents a greeting to the world.
 */
var Model = (function () {
    function Model(greeting) {
        this.greeting = greeting;
    }
    Model.prototype.getGreeting = function () {
        return this.greeting + ", world!";
    };
    return Model;
})();
/**
 * Instance is a message logger; outputs messages to console.
 */
var View = (function () {
    function View() {
    }
    View.prototype.display = function (msg) {
        console.log(msg);
    };
    return View;
})();
exports.View = View;
/*
 * Factory function. Returns a default first app.
 */
function defaultGreeter(view) {
    return new Controller("Hello", view);
}
exports.defaultGreeter = defaultGreeter;
