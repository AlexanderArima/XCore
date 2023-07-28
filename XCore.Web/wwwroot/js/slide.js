class SlidingVerification {
    // 鼠标左键是否按下
    isMouseDown

    // 鼠标按下x值
    mouseDownStartX

    // 鼠标移动距离
    mouseMoveLength

    // 准确的x位置
    correctX

    // 图片
    imageBase64

    // 缺失图片的X轴的值
    _notchAreaX

    constructor(imageBase64, notchAreaX) {
        this.setCurrentImageBase64(imageBase64)
        this.getImageData()
        this.bindMouseEvent()
        this._notchAreaX = notchAreaX;
    }

    // 设置当前图片
    setCurrentImageBase64(imageBase64) {
        this.imageBase64 = imageBase64
        document.getElementById('backImage').src = imageBase64
    }

    // 绑定鼠标事件
    bindMouseEvent() {
        document.onmousemove = (event) => {
            if (this.isMouseDown) {
                this.mouseMoveLength = event.clientX - this.mouseDownStartX
                if (this.mouseMoveLength + 5 > 0 && this.mouseMoveLength + 5 < document.getElementById('backImage').offsetWidth - 5 - document.getElementById('smallImage').offsetWidth) {
                    document.getElementById('smallImage').style.left = 5 + this.mouseMoveLength + 'px'
                    document.getElementById('sliderBlock').style.left = 5 + this.mouseMoveLength + 'px'
                }
            }
        }

        //鼠标按下事件
        document.onmousedown = (event) => {
            let { top, left, width, height } = this.getElementPosition(document.getElementById('sliderBlock'))
            if (event.clientX > left && event.clientX < (left + width) && event.clientY > top && event.clientY < (top + height)) {
                this.isMouseDown = true
                this.mouseDownStartX = event.clientX
            }
        }

        //鼠标抬起事件
        document.onmouseup = () => {
            if (this.isMouseDown) {
                this.isMouseDown = false
                this.checkVerificatonIsCorrect()
            }
        }
    }

    // 检测拼图是否对接完成
    checkVerificatonIsCorrect() {
        if (this.correctX && this.correctX) {
            let backColor = ((Math.abs(this.correctX - (this.mouseMoveLength + 5)) < 5)) ? 'rgb(146,190,70)' : 'rgba(205,64,74)'
            document.getElementById('slider').style.background = backColor
            setTimeout(() => {
                document.getElementById('slider').style.background = 'aliceblue'
                document.getElementById('smallImage').style.left = 5 + 'px'
                document.getElementById('sliderBlock').style.left = 5 + 'px'
            }, 1000)
        }
    }

    // dom在浏览器的位置
    getElementPosition(element) {
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


    // 获取图片
    async getImageData() {
        var image = new Image();
        image.src = this.imageBase64
        await new Promise((resolve) => {
            image.onload = resolve
        })
        let width = image.width
        let height = image.height

        // console.log("getImageData：width = " + width + "，height = " + height);
        let canvas = document.createElement('canvas')
        canvas.setAttribute('width', `${width}px`)
        canvas.setAttribute('height', `${height}px`)
        var ctx = canvas.getContext("2d")
        ctx.drawImage(image, 0, 0, width, height)
        this.shrinkUrl = canvas.toDataURL('image/jpeg', 1)
        try {
            //保存像素
            let originalPiexls = ctx.getImageData(0, 0, width, height)
            this.drawImage(originalPiexls, width, height)
        } catch (error) {
            console.log(error)
        }
    }

    // 缺失的区域
    getNotchArea(width, height) {
        let notchAreaWidth = parseInt(width / 8.0)
        let notchAreaHeight = notchAreaWidth
        // let notchAreaX = parseInt(Math.random() * ((width - notchAreaWidth) - (notchAreaWidth * 2)) + notchAreaWidth * 2)
        this.correctX = (_notchAreaX / width) * document.getElementById('backImage').offsetWidth
        let notchAreaY = parseInt(Math.random() * ((height - notchAreaHeight) - 60) + 60)
        console.log("getNotchArea：_notchAreaX = " + _notchAreaX + "，notchAreaY = " + notchAreaY);
        document.getElementById('smallImage').style.top = (notchAreaY / height) * document.getElementById('backImage').offsetHeight + 'px'
        document.getElementById('smallImage').style.left = 5 + 'px'
        document.getElementById('smallImage').style.width = (notchAreaWidth / width) * document.getElementById('backImage').offsetWidth + 'px'
        return { _notchAreaX, notchAreaY, notchAreaWidth, notchAreaHeight }
    }

    // 绘制图片
    drawImage(originalPiexls, width, height) {
        if (!originalPiexls || !originalPiexls.data || originalPiexls.data.length == 0) {
            throw ('像素为空')
        }
        let { notchAreaX, notchAreaY, notchAreaWidth, notchAreaHeight } = this.getNotchArea(width, height)
        let notchAreaPixels = new Uint8ClampedArray(notchAreaWidth * notchAreaHeight * 4)
        let startY = notchAreaY
        let endY = notchAreaY + notchAreaHeight
        let startX = notchAreaX * 4
        let endX = (notchAreaX + notchAreaWidth) * 4
        let currentSmallIndex = 0
        for (let y = startY; y < endY; y++) {
            for (let x = startX; x < endX; x++) {
                let currentIndex = (y * width * 4) + x
                // 保存小图像素
                notchAreaPixels[currentSmallIndex] = originalPiexls.data[currentIndex]
                // 添加边框
                let borderWidth = 3
                if (Math.abs(y - startY) < borderWidth || Math.abs(y - endY) < borderWidth || Math.abs((x / 4.0) - (startX / 4.0)) < borderWidth || Math.abs((x / 4.0) - (endX / 4.0)) < borderWidth) {
                    notchAreaPixels[currentSmallIndex] = 255
                }
                currentSmallIndex += 1
                // 替换大图像素
                originalPiexls.data[currentIndex] = 245
            }
        }

        // 小图
        //let smallCanvas = document.createElement('canvas')
        //smallCanvas.setAttribute('width', `${notchAreaWidth}px`)
        //smallCanvas.setAttribute('height', `${notchAreaHeight}px`)
        //let smallCtx = smallCanvas.getContext("2d")
        //let smallImageData = new ImageData(notchAreaPixels, notchAreaWidth, notchAreaHeight)
        //smallCtx.putImageData(smallImageData, 0, 0, 0, 0, notchAreaWidth, notchAreaHeight)
        //let url = smallCanvas.toDataURL('image/jpeg', 1)
        //document.getElementById('smallImage').src = url

        // 大图
        let bigCanvas = document.createElement('canvas')
        bigCanvas.setAttribute('width', `${width}px`)
        bigCanvas.setAttribute('height', `${height}px`)
        let bigCtx = bigCanvas.getContext("2d")
        let bigImageData = new ImageData(originalPiexls.data, width, height)
        bigCtx.putImageData(bigImageData, 0, 0, 0, 0, width, height)
        let bigUrl = bigCanvas.toDataURL('image/jpeg', 1)
        document.getElementById('backImage').src = bigUrl
    }
}

