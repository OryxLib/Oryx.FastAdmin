/**
 * 知页科技旗下 , 剑羚技术实验室开发.
 * 
 * 主要提供软件技术服务 , 软件技术咨询.
 * 
 * 主要语言, .Net Core 跨平台开发, Xamarin 开发, 前端开发, Python开发
 * 
 * 研究方向, 跨平台应用开发, 大数据+ , 人工智能应用, 物联网应用, mirobit少儿编程
 * 
 * **/
function orxyWebClient(path) {
    var protocol = window.location.protocol == "http" ? "ws" : "wss";
    var host = window.location.host;
    var _this = this;

    orxyWebClient.prototype.instance = new WebSocket(protocol + "://" + host + path)
    orxyWebClient.prototype.instance.onopen = function (msg) {
        _this.onStart(msg);
    }
    orxyWebClient.prototype.instance.onmessage = function (msg) {
        data = msg.data.trim()
        dataObj = JSON.parse(data );
        _this.onRecieve && _this.onRecieve(dataObj);
    }
    orxyWebClient.prototype.instance.onclose = function (msg) {
        _this.onClose && _this.onClose(msg);
    }
    orxyWebClient.prototype.instance.onerror = function (msg) {
        _this.onError && _this.onError(msg);
    }
}

orxyWebClient.prototype = {
    onStart: function (handler) {
        var _this = this;
        setInterval(function () {
            _this.instance.send("heart beat!")
        }, 50 * 1e+3)
    },
    Send: function (dataObj) {
        this.instance.send(JSON.stringify(dataObj));
    }
}




