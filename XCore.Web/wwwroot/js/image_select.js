
// 图片列表
var _imageBase64 = new Array();

// 文字描述的图片
var _imageText = new String();

// 令牌
var _id;

// 设置当前图片
function setCurrentImageBase64(imageText, imageBase64, img_index) {
    // 显示描述文字
    _imageText = 'data:image/webp;base64,' + imageText;
    document.getElementById('backTextImage').src = _imageText;

    // 显示图片
    for (var i = 0; i < imageBase64.length; i++) {
        _imageBase64[i] = 'data:image/webp;base64,' + imageBase64[i];
        document.getElementById('backImage' + (i + 1)).src = _imageBase64[i];
        document.getElementById('backImage' + (i + 1)).attributes["val1"] = img_index[i];
    }
}

function check(x) {
    $.get("Check?code=" + x + "&id=" + _id, function (data) {
        var obj = JSON.parse(data);
        if (obj.code == "0") {
            alert("验证成功");
        }
        else {
            alert("验证失败");
        }
    });
}

window.onload = function () {
    $.get("Create", function (data) {
        // 获取两张图片和令牌
        var obj = JSON.parse(data);
        _id = obj.data.id;
        // console.log(obj.data.img_index.length);
        setCurrentImageBase64(obj.data.img_text, obj.data.img, obj.data.img_index);
    });

    $(".img_check").click(function (event) {
        var id = event.currentTarget.id;
        var index = id.replace("backImage", "");
        // console.log(event.currentTarget.attributes["val1"]);
        if (event.currentTarget.attributes["checked"].value == "0") {
            event.currentTarget.attributes["checked"].value = "1";
            $("#imgIcon" + index).css("height", "25");
            $("#imgIcon" + index).css("width", "25");
        }
        else {
            event.currentTarget.attributes["checked"].value = "0";
            $("#imgIcon" + index).css("height", "1");
            $("#imgIcon" + index).css("width", "1");
        }
    });

    $("#button_submit").click(function () {
        var result = "";
        var v1 = $("#backImage1");
        var v2 = $("#backImage2");
        var v3 = $("#backImage3");
        var v4 = $("#backImage4");
        if (v1[0].attributes["checked"].value == "1") {
            var code = v1[0].attributes["val1"];
            result = result + code + ","
        }

        if (v2[0].attributes["checked"].value == "1") {
            var code = v2[0].attributes["val1"];
            result = result + code + ","
        }

        if (v3[0].attributes["checked"].value == "1") {
            var code = v3[0].attributes["val1"];
            result = result + code + ","
        }

        if (v4[0].attributes["checked"].value == "1") {
            var code = v4[0].attributes["val1"];
            result = result + code + ","
        }

        if (result.length <= 0) {
            alert("请选择图片");
            return;
        }
        else {
            result = result.substring(0, result.length - 1);
        }

        check(result);
    })
}