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

/**
 * 本插件主要功能, 用于社交媒体插件, 上传图片, 上传视频, #话题
 * **/

function orxySocialEditor(element) {
    var rootElement = element;

    //container
    var container = createElement('div', null, { "style": "border:1px solid #2878ff; ", "class": "card card-outline-primary" });
    setElementClass(container, 'container');

    //header
    var header = createElement('div', null, { "class": "card-header " });
    setElementAttribute(header, { "style": "width:100%;min-height:50px;" })

    //body
    var body = createElement('div');
    setElementAttribute(body, { "style": "width:100%;min-height:200px;border:1px solid #2878ff;", "class": "card-body" })
    setElementAttribute(body, { "contenteditable": true })

    //footer
    var footer = createElement('div', null, { "class": "card-footer text-muted" });
    setElementAttribute(footer, { "style": "width:100%;min-height:50px;" })

    var btnEmoji = createElement('button', 'Emoji', {
        'style': "width:105px;height:45px;float:left;margin:2px 5px;color:white;",
        "class": "btn btn-primary btn-md btn-rounded btn-emoji"
    })
    btnEmoji.dataset["emojiPlaceholder"] = "smiley:"
    var btnMedia = createElement('button', 'Media', { 'style': "width:105px;height:45px;float:left;margin:2px 5px;color:white;", "class": "btn btn-primary btn-md btn-rounded btn-media" })
    var btnNeedPay = createElement('button', 'Pay?', { 'style': "width:105px;height:45px;float:left;margin:2px 5px;color:white;", "class": "btn btn-primary btn-md btn-rounded btn-needpay" })
    footer.appendChild(btnEmoji)
    footer.appendChild(btnMedia)
    footer.appendChild(btnNeedPay)

    var btnSender = createElement('button', 'Send', { 'style': "width:145px;height:45px;float:right;margin:2px 5px;color:white;", "class": "btn btn-primary btn-md btn-rounded btn-send" })
    footer.appendChild(btnSender)


    setElementAttribute(rootElement, { "style": "width:100%;min-height:400px;" })
    rootElement.appendChild(container)
    container.appendChild(header)
    container.appendChild(body)
    container.appendChild(footer)

}
function setElementStyle(element, style) {
    setElementAttribute(element, { "style": style })
}
function setElementClass(element, _class) {
    element.classList.add(_class)
}


function setElementAttribute(element, attr) {
    var isarr = attr instanceof Array
    var attArr = []
    if (!isarr) {
        attArr.push(attr);
    } else {
        attArr = attr
    }

    for (var index in attArr) {
        var item = attArr[index];
        var keys = Object.keys(item);
        for (var ki in keys) {
            var key = keys[ki];
            element && element.setAttribute(key, item[key])
        }
    }
}

function createElement(tagName, innerHtml, attr) {
    var element = document.createElement(tagName)
    if (innerHtml) {

        element.innerText = innerHtml;
    }
    if (attr) {
        setElementAttribute(element, attr)
    }
    return element;
}