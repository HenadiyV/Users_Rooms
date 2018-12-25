//Load Data in Table when documents is ready

$(document).ready(function () {
    loadDataUser();
});
//Load Data function
function loadDataUser() {

    $.ajax({
        url: "/Admin/ListUser",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                /* html += '<td>' + item.Id + '</td>';*/
                html += '<td>' + item.FirstName + '</td>';
                html += '<td>' + item.LastName + '</td>';
                html += '<td>' + item.Email + '</td>';
                html += '<td>' + item.Password + '</td>';
                html += '<td>' + item.Role.Name + '</td>';
                html += '<td><a href="#" onclick="return getbyID(' + item.Id + ')">Редактировать</a> | <a href="#" onclick="Delele(' + item.Id + ')">Удалить</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);

        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
//Add Data Function
function AddUser() {

    var res = validateUser();
    if (res == false) {
        return false;
    }
    var UserObj = {
        Id: $('#IdUser').val(),
        FirstName: $('#FirstName').val(),
        LastName: $('#LastName').val(),
        Email: $('#Email').val(),
        RoleId: $('#RoleId').val(),
        Password: $('#Password').val()

    };
    $.ajax({
        url: "/Admin/AddUser",
        data: JSON.stringify(UserObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $('#myModal').modal('hide');
            loadDataUser();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
//function for updating user record
function UpdateUser() {

    var res = validateUser();
    if (res == false) {
        return false;
    }
    var UserObj = {
        Id: $('#IdUser').val(),
        FirstName: $('#FirstName').val(),
        LastName: $('#LastName').val(),
        Email: $('#Email').val(),
        RoleId: $('#RoleId').val(),
        Password: $('#Password').val()
    };
    $.ajax({
        url: "/Admin/UpdateUser",
        data: JSON.stringify(UserObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadDataUser();
            $('#myModal').modal('hide');
            $('#Id').val("");
            $('#FirstName').val("");
            $('#LastName').val("");
            $('#Email').val("");
            $('#RoleId').val("");
            $('#Password').val("");

        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}


//Function for getting the Data Based upon user ID
function getbyID(UserID) {
    $('#FirstName').css('border-color', 'lightgrey');
    $('#LaststName').css('border-color', 'lightgrey');
    $('#Email').css('border-color', 'lightgrey');
    $('#RoleId').css('border-color', 'lightgrey');
    $('#Password').css('border-color', 'lightgrey');
    $.ajax({
        url: "/Admin/GetbyIDUser/" + UserID,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#IdUser').val(result.Id);
            $('#FirstName').val(result.FirstName);
            $('#LastName').val(result.LastName);
            $('#Email').val(result.Email);
            $('#RoleId').val(result.Rol);
            $('#Password').val(result.Password);
            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}


//function for deleting user record
function Delele(ID) {
    var ans = confirm("Запись будет удалена.Продолжить?");
    if (ans) {
        $.ajax({
            url: "/Admin/DeleteUser/" + ID,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                loadDataUser();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}

//Function for clearing the textboxes
function clearTextBox() {
    $('#IdUser').val("");
    $('#FirstName').val("");
    $('#LastName').val("");
    $('#Email').val("");
    $('#RoleId').val("");
    $('#Password').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#FirstName').css('border-color', 'lightgrey');
    $('#LastName').css('border-color', 'lightgrey');
    $('#Email').css('border-color', 'lightgrey');
    $('#Rol').css('border-color', 'lightgrey');
    $('#Password').css('border-color', 'lightgrey');

}
//Valdidation using jquery
function validateUser() {
    var isValid = true;
    if ($('#FirstName').val().trim() == "") {
        $('#FirstName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#FirstName').css('border-color', 'lightgrey');
    }

    if ($('#LastName').val().trim() == "") {
        $('#LastName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#LastName').css('border-color', 'lightgrey');
    }
    if ($('#Email').val().trim() == "") {

        $('#Email').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Email').css('border-color', 'lightgrey');
    }
    if ($('#Email').val().indexOf('@') == -1) {
        $('#Email').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Email').css('border-color', 'lightgrey');
    }
    if ($('#RoleId').val().trim() == "") {
        $('#RoleId').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#RoleId').css('border-color', 'lightgrey');
    }
    if ($('#Password').val().trim() == "") {
        $('#Password').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Password').css('border-color', 'lightgrey');
    }
    return isValid;
}
