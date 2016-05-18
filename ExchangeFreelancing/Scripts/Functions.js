function toogleOrder(elem, toogleElem) {
    $(elem).parent("div").children(toogleElem).toggle(500);
}

$(document).ready(function () {
    $(".order_header").click(function () {
        toogleOrder(this, ".order-body");
    });
});
function ShowChat(path, sender) {
    
    var chat = $(sender).parents("div").children(".chat");

    var mes = {
        order: chat.children(".hid_field").val(),
    }
    if (chat.is(":visible")) {
        chat.hide(500);
        chat.children(".mesgs").empty();
    }
    else {
        chat.show(500);
        $.ajax({
            type: "post",
            url: path,
            data: mes,
            success: function (data) {
                chat.children(".mesgs").append(data);
            },
            error: function (data) {
                alert("fail");
                alert(data.status);
            }
        })
    }
}
function Subm(path, _user,sender) {
    var chat = $(sender).parent(".chat");
    var textarea = chat.children(".mes");
    var m = chat.children(".mesgs");
    if ($.trim(textarea.val()).length > 0) {
        var mes = {
            _order: chat.children(".hid_field").val(),
            user: _user,
            message: textarea.val()
        }
        $.ajax({
            type: "POST",
            url: path,
            data: mes,
            success: function (response) {
                m.append(response);
                textarea.val('');
            },
            error: function (data) {
                alert(data);
            }
        })
    }


}
function Refresh(sender) {    
    var chat = $(sender).parent(".chat");
    var mes = {
        order: chat.children(".hid_field").val(),
    }
    chat.children(".mesgs").empty();
    $.ajax({
        type: "post",
        url: "/Message/GetMessages/",
        data: mes,
        success: function (data) {
            chat.children(".mesgs").append(data);
        },
        error: function (data) {
            alert("fail");
            alert(data.status);
        }
    })
}
