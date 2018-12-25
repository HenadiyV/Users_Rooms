$(document).ready(function () {
    loadDataUser();
});
//список пользователей
function loadDataUser() {
    $.ajax({
        url: "/Home/UserList",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                //html += '<td>' + item.Id +'</td>';
                html += '<td>' + item.FirstName + '</td>';
                html += '<td>' + item.Email + '</td>';
                html += '<td><a href="#" data-toggle="modal" data-target="#myModalRoom" onclick="loadRoomNotUsers(' + item.Id + ')">Добавить комнату</a> <a href="#" data-toggle="modal" data-target="#myModalRoom" onclick="loadRooms_User(' + item.Id + ')">Удалить комнату</a></td>';
                html += '</tr>';
            });
            $('.tbodyUser').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
var RoomId;
var UserId;
//список комнат
function loadRooms_User(ID) {
    RoomId = ID;
    $.ajax({
        url: "/Home/Rooms_User/" + ID,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                //html += '<td>' + item.Id + '</td>';
                html += '<td>' + item.Name + '</td>';
                html += '<td><a href="#" onclick="DeleleRoom(' + item.Id + ')">Удалить</a> </td>';
                html += '</tr>';
            });
            $('.tbodyRoom').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
// список пользователей в не комнаты
function loadRoomNotUsers(ID) {
    UserId = ID;
    $.ajax({
        url: "/Home/RoomNotUsers/" + ID,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                //html += '<td>' + item.Id + '</td>';|<a href="#" onclick="DeleleRoom(' + item.Id + ')">Delete</a>
                html += '<td>' + item.Name + '</td>';
                html += '<td><a href="#" onclick="UserAddRoom(' + item.Id + ')">Добавить</a> </td>';
                html += '</tr>';
            });
            $('.tbodyRoom').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
//добавление пользователю комнаты
function UserAddRoom(ID) {
    RoomId = ID;
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
//function for deleting room record
function DeleleRoom(ID) {
    var ans = confirm("Запись будет удалена.Продолжить?");
    RoomId = ID;
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
