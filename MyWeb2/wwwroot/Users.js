// Получение всех пользователей
function GetUsers() {
    $.ajax({
        url: '/api/users',
        type: 'GET',
        contentType: "application/json",
        success: function (users) {
            var rows = "";
            $.each(users, function (index, user) {
                // добавляем полученные элементы в таблицу
                rows += row(user);
            })
            $("table tbody").append(rows);
        }
    });
}
// Получение одного пользователя
function GetUser(id) {
    $.ajax({
        url: '/api/users/' + id,
        type: 'GET',
        contentType: "application/json",
        success: function (user) {
            var form = document.forms["userForm"];
            form.elements["id"].value = user.userId;
            form.elements["name"].value = user.userName;
            form.elements["password"].value = user.password;
        }
    });
}
// Добавление пользователя
function CreateUser(name, password) {
    $.ajax({
        url: "api/users",
        contentType: "application/json",
        method: "POST",
        data: JSON.stringify({
            userName: name,
            password: password
        }),
        success: function (user) {
            reset();
            $("table tbody").append(row(user));
        }
    })
}
// Изменение пользователя
function EditUser(userId, userName, password) {
    $.ajax({
        url: "api/users/" + userId,
        contentType: "application/json",
        method: "PUT",
        data: JSON.stringify({
            userid: userId,
            username: userName,
            password: password
        }),
        success: function (user) {
            reset();
            $("tr[data-rowid='" + user.userId + "']").replaceWith(row(user));
        }
    })
}

// сброс формы
function reset() {
    var form = document.forms["userForm"];
    form.reset();
    form.elements["id"].value = 0;
}

// Удаление пользователя
function DeleteUser(id) {
    $.ajax({
        url: "api/users/" + id,
        contentType: "application/json",
        method: "DELETE",
        success: function (user) {
            $("tr[data-rowid='" + user.userId + "']").remove();
        }
    })
}
// создание строки для таблицы
var row = function (user) {
    return "<tr data-rowid='" + user.userId + "'><td>" + user.userId + "</td>" +
        "<td>" + user.userName + "</td> <td>" + user.password + "</td>" +
        "<td><a class='editLink' data-id='" + user.userId + "'>Изменить</a> | " +
        "<a class='removeLink' data-id='" + user.userId + "'>Удалить</a></td></tr>";
}
// сброс значений формы
$("#reset").click(function (e) {

    e.preventDefault();
    reset();
})

// отправка формы
$("form").submit(function (e) {
    e.preventDefault();
    var id = this.elements["id"].value;
    var name = this.elements["name"].value;
    var password = this.elements["password"].value;
    if (id == 0)
        CreateUser(name, password);
    else
        EditUser(id, name, password);
});

// нажимаем на ссылку Изменить
$("body").on("click", ".editLink", function () {
    var id = $(this).data("id");
    GetUser(id);
})
// нажимаем на ссылку Удалить
$("body").on("click", ".removeLink", function () {
    var id = $(this).data("id");
    DeleteUser(id);
})

// загрузка пользователей
GetUsers();