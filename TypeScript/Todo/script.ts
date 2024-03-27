declare const $:any;

class TodoModel{
    id: number = setNewId();
    work: string = "";
    isCompleted: boolean = false;
    createdAt: Date = new Date();
}

const todos: TodoModel[] = []

$("#save").on("click", save);

function save(){
    const el = $("#work");

    const todo = new TodoModel();
    todo.work = el.val();

    todos.push(todo);

    el.val("");
    el.focus();

    setTRElements();
}

function setTRElements(){
    const el = $("#tbodyEl");
    let html = "";
    for(let index in todos){
        const todo = todos[index];

        const date = todo.createdAt.getDate() + "." + (+(todo.createdAt.getMonth()) + 1)+ "." + todo.createdAt.getFullYear() + " " + todo.createdAt.getHours() + ":" + todo.createdAt.getMinutes();

        html += `
        <tr>
        <td>${todo.id}</td>
        <td>${todo.work}</td>
        <td>${date}</td>
        <td>${todo.isCompleted}</td>
        <td>
            <button>Delete</button>
        </td>
        </tr>`
    }

    el.html(html);
}

function setNewId(){
    return todos.length + 1;
}