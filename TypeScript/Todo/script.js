var TodoModel = /** @class */ (function () {
    function TodoModel() {
        this.id = setNewId();
        this.work = "";
        this.isCompleted = false;
        this.createdAt = new Date();
    }
    return TodoModel;
}());
var todos = [];
$("#save").on("click", save);
function save() {
    var el = $("#work");
    var todo = new TodoModel();
    todo.work = el.val();
    todos.push(todo);
    el.val("");
    el.focus();
    setTRElements();
}
function setTRElements() {
    var el = $("#tbodyEl");
    var html = "";
    for (var index in todos) {
        var todo = todos[index];
        var date = todo.createdAt.getDate() + "." + (+(todo.createdAt.getMonth()) + 1) + "." + todo.createdAt.getFullYear() + " " + todo.createdAt.getHours() + ":" + todo.createdAt.getMinutes();
        html += "\n        <tr>\n        <td>".concat(todo.id, "</td>\n        <td>").concat(todo.work, "</td>\n        <td>").concat(date, "</td>\n        <td>").concat(todo.isCompleted, "</td>\n        <td>\n            <button>Delete</button>\n        </td>\n        </tr>");
    }
    el.html(html);
}
function setNewId() {
    return todos.length + 1;
}
