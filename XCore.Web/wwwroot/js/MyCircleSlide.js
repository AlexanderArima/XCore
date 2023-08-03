$(function () {
    var _imageBase64;    // 大图
    var _id;

    // 鼠标左键是否按下
    var isMouseDown;

    // 鼠标按下x值
    var mouseDownStartX;

    // 鼠标移动距离
    var mouseMoveLength;

    init();

    document.onmousedown = function (event) {
        var obj = getElementPosition(document.getElementById('sliderBlock'))
        if (event.clientX > obj.left &&
            event.clientX < (obj.left + obj.width) &&
            event.clientY > obj.top &&
            event.clientY < (obj.top + obj.height)) {
            // 鼠标点击事件发生在滑动条的范围内
            this.isMouseDown = true
            this.mouseDownStartX = event.clientX
            console.log("鼠标点击事件发生在滑动条的范围内");
        }
    }

    document.onmousemove = function (event) {
        if (this.isMouseDown) {
            this.mouseMoveLength = event.clientX - this.mouseDownStartX;    // 计算滑块拖动的距离
            if (this.mouseMoveLength > 0 &&
                this.mouseMoveLength < 360) {
                // 滑块拖动的最小距离是5px，最大距离是大图的宽度 - 小图的宽度
                document.getElementById('sliderBlock').style.left = 5 + this.mouseMoveLength + 'px'
                $("#backImage").css("transform", "rotate(" + (this.mouseMoveLength) + "deg)");
            }
        }
    }

    document.onmouseup = function (event) {
        // 鼠标松开后停止拖动
        if (this.isMouseDown) {
            this.isMouseDown = false
            // console.log("小图移动了：" + this.mouseMoveLength);
            // check(x.replace("px", ""));
            check(this.mouseMoveLength);
        }
    }

    // dom在浏览器的位置
    function getElementPosition(element) {
        let top = element.offsetTop
        let left = element.offsetLeft
        let width = element.offsetWidth
        let height = element.offsetHeight
        var currentParent = element.offsetParent;
        while (currentParent !== null) {
            top += currentParent.offsetTop
            left += currentParent.offsetLeft
            currentParent = currentParent.offsetParent
        }

        return { top, left, width, height }
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

    // 设置当前图片
    function setCurrentImageBase64(imageBase64) {
        _imageBase64 = imageBase64
        document.getElementById('backImage').src = _imageBase64
    }

    function init() {
        $.get("Create", function (data) {
            // 获取两张图片和令牌
            var obj = JSON.parse(data);
            _id = obj.data.id;
            setCurrentImageBase64('data:image/webp;base64,' + obj.data.img);
        });
    }
});