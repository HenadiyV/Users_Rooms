$(document).ready(function () {
    loadDataRoom();

});
//список комнат
function loadDataRoom() {
    $.ajax({
        url: "/Home/RoomList",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                //html += '<td>' + item.Id +'</td>';
                html += '<td>' + item.Name + '</td>';

                html += '<td> <a href="#" data-toggle="modal" data-target="#myModalRoom" onclick="loadUsersNotRoom(' + item.Id + ')">Добавить пользователя</a>|<a href="#" data-toggle="modal" data-target="#myModalRoom" onclick="loadUsers_Room(' + item.Id + ')">Удалить пользователя</a></td>';
                html += '</tr>';
            });
            $('.tbodyRoom').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

var RoomId;
//список пользователей
function loadUsers_Room(ID) {
    RoomId = ID;
    $.ajax({
        url: "/Home/Users_Room/" + ID,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {

                html += '<tr>';
                //html += '<td>' + item.Id + '</td>';
                html += '<td>' + item.FirstName + '</td>';
                html += '<td>' + item.Email + '</td>';
                html += '<td><a href="#" onclick="DeleleUser(' + item.Id + ')">Удалить</a> </td>';
                html += '</tr>';
            });
            $('.tbodyUser').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
// свободные комнаты
function loadUsersNotRoom(ID) {
    RoomId = ID;
    $.ajax({
        url: "/Home/UsersNotRoom/" + ID,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {

                html += '<tr>';
                //html += '<td>' + item.Id + '</td>';
                html += '<td>' + item.FirstName + '</td>';
                html += '<td>' + item.Email + '</td>';
                html += '<td><a href="#" onclick="RoomAddUser(' + item.Id + ')">Добавить</a></td>';
                html += '</tr>';
            });
            $('.tbodyUser').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
// добавление
var UserId;
function RoomAddUser(ID) {
    UserId = ID;
    var UserObj = {
        Rooms_Id: RoomId,
        Users_Id: UserId
    };
    $.ajax({
        url: "/Home/RoomAddUser",
        data: JSON.stringify(UserObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $('#myModalRoom').modal('hide');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
//удаление 
function DeleleUser(ID) {
    var ans = confirm("Запись будет удалена. Продолжить?");
    UserId = ID;
    var UserObj = {
        Rooms_Id: RoomId,
        Users_Id: UserId
    };
    if (ans) {
        $.ajax({
            url: "/Home/DeleteUserAndRoom/",
            type: "POST",
            data: JSON.stringify(UserObj),
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                $('#myModalRoom').modal('hide');
              
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}
