
$(document).ready(function () {
    loadDataRoom();
});
//отображение данных
function loadDataRoom() {
    $.ajax({
        url: "/Admin/ListRoom",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                //html += '<td>' + item.Id + '</td>';
                html += '<td>' + item.Name + '</td>';
                html += '<td>' + item.RoomTypeAsString + '</td>';
                html += '<td>' + item.SizeRoom + '</td>';

                html += '<td><a href="#" onclick="return getbyRoomID(' + item.Id + ')">Редактировать</a> | <a href="#" onclick="Delele(' + item.Id + ')">Удалить</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
//добавить комнату
function AddRoom() {
    var res = validateRoom();
    if (res == false) {
        return false;
    }
    var RoomObj = {
        Id: $('#IdRoom').val(),
        Name: $('#Name').val(),
        RoomTypeAsString: $('#RoomTypeAsString').val(),
        SizeRoom: $('#SizeRoom').val()

    };
    $.ajax({
        url: "/Admin/AddRoom",
        data: JSON.stringify(RoomObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            
            $('#ModalRoom').modal('hide');
            loadDataRoom();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
//редактирование
function UpdateRoom() {
    var res = validateRoom();
    if (res == false) {
        return false;
    }
    var RoomObj = {
        Id: $('#IdRoom').val(),
        Name: $('#Name').val(),
        RoomTypeAsString:$('#RoomTypeAsString').val(),
        SizeRoom: $('#SizeRoom').val()

    };
    $.ajax({
        url: "/Admin/UpdateRoom",
        data: JSON.stringify(RoomObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            
            $('#ModalRoom').modal('hide');
            $('#IdRoom').val("");
            $('#Name').val("");
            $('#RoomTypeAsString').val("");
            $('#SizeRoom').val("");
            loadDataRoom();

        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
//Function for getting the Data Based upon Room ID
function getbyRoomID(RoomID) {
    $('#Name').css('border-color', 'lightgrey');
    $('#RoomTypeAsString').css('border-color', 'lightgrey');
    $('#SizeRoom').css('border-color', 'lightgrey');
    $.ajax({
        url: "/Admin/GetbyIDRoom/" + RoomID,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#IdRoom').val(result.Id);
            $('#Name').val(result.Name);
            $('#RoomTypeAsString').val(result.RoomTypeAsString)
            $('#SizeRoom').val(result.SizeRoom);
            $('#ModalRoom').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}


//function for deleting room record
function Delele(ID) {
    var ans = confirm("Запись будет удалена.Продолжить?");
    if (ans) {
        $.ajax({
            url: "/Admin/DeleteRoom/" + ID,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                loadDataRoom();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}

//Function for clearing the textboxes
function clearTextBoxRoom() {
    $('#IdRoom').val("");
    $('#Name').val("");
    $('#RoomTypeAsString').val("");
    $('#SizeRoom').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#Name').css('border-color', 'lightgrey');

    $('#SizeRoom').css('border-color', 'lightgrey');
}
//Valdidation using jquery
function validateRoom() {
    var isValid = true;
  
    if ($('#Name').val().trim() == "") {
        $('#Name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Name').css('border-color', 'lightgrey');
    }
    if ($('#RoomTypeAsString').val().trim() == "") {
        $('#RoomTypeAsString').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#RoomTypeAsString').css('border-color', 'lightgrey'); /*#RoomTypeAsString*/
    }
    if ($('#SizeRoom').val().trim == "") {
        $('#SizeRoom').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#SizeRoom').css('border-color', 'lightgrey');
    }
    var res = $('#SizeRoom').val();
    
    if (res <= 0) {
        $('#SizeRoom').css('border-color', 'Red');
        isValid = false;
    } else {
        $('#SizeRoom').css('border-color', 'lightgrey');
    }

    return isValid;

}