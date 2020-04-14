//<script src="//cdn.bootcss.com/plupload/2.1.8/plupload.full.min.js"></script>
//    <script src="//lib.eqh5.com/qiniu-js/1.0.17.1/qiniu.min.js"></script>

layui.define(['jquery', 'layer'], function (exports) {
    var  $ = layui.jQuery,layer=layui.layer;
    var qiniu=function (config) {
        //初始化设置参数
        this.config = {
            elem: '', //选择器id
            url:'/file/token', //服务端获取七牛签名url
            pElem:'', //父级id
            max_file_size:'10m',// 最大的文件限制
            domain:"xx自定义域名/",//七牛对应 bucket 域名
        };
        this.config = $.extend(this.config,config);
    };
    qiniu.prototype.render=function () {
        var o=this.config;
        var cropper_all = function() { //插入图片
            $(function () {
                Qiniu.uploader({
                    runtimes: 'html5,flash,html4',    //上传模式,依次退化
                    browse_button: o.elem,       //上传选择的点选按钮，**必需**
                    uptoken_url: o.url,            //Ajax请求upToken的Url，**强烈建议设置**（服务端提供）   默认get 请求   返回: {'uptoken':'xxxx'}
                    //uptoken : token, //若未指定uptoken_url,则必须指定 uptoken ,uptoken由其他程序生成
                    unique_names: true, // 默认 false，key为文件名。若开启该选项，SDK为自动生成上传成功后的key（文件名）。
                    save_key: false,   // 默认 false。若在服务端生成uptoken的上传策略中指定了 `sava_key`，则开启，SDK会忽略对key的处理
                    domain: o.domain,   //bucket 域名，下载资源时用到，**必需**
                    get_new_uptoken: false,  //设置上传文件的时候是否每次都重新获取新的token
                    container: o.pElem,           //上传区域DOM ID，默认是browser_button的父元素，
                    max_file_size: o.max_file_size,           //最大文件体积限制
                    flash_swf_url: 'js/plupload/Moxie.swf',  //引入flash,相对路径
                    max_retries: 3,                   //上传失败最大重试次数
                    dragdrop: true,                   //开启可拖曳上传
                    drop_element: 'container',        //拖曳上传区域元素的ID，拖曳文件或文件夹后可触发上传
                    chunk_size: '4mb',                //分块上传时，每片的体积
                    auto_start: true,                 //选择文件后自动上传，若关闭需要自己绑定事件触发上传
                    init: {
                        'FilesAdded': function(up, files) {
                            plupload.each(files, function(file) {
                                // 文件添加进队列后,处理相关的事情
                            });
                        },
                        'BeforeUpload': function(up, file){
                            // 每个文件上传前,处理相关的事情
                            loadIndex = layer.load(2, {time: 10*1000});
                        },
                        'UploadProgress': function(up, file) {
                            // 每个文件上传时,处理相关的事情
                        },
                        'FileUploaded': function(up, file, info) {
                            // 每个文件上传成功后,处理相关的事情
                            // 其中 info 是文件上传成功后，服务端返回的json，形式如
                            // {
                            //    "hash": "Fh8xVqod2MQ1mocfI4S4KpRL6D98",
                            //    "key": "gogopher.jpg"
                            //  }
                            let domain = up.getOption('domain');
                            let res = $.parseJSON(info);
                            let sourceLink = domain + res.key;
                            typeof o.success === 'function' && o.success(sourceLink);
                            layer.close(loadIndex);
                        },
                        'Error': function(up, err, errTip){
                            //上传出错时,处理相关的事情
                            typeof o.error === 'function' && o.error(err);
                            layer.msg('error!请重新上传',function () {
                                layer.close(loadIndex)
                            });
                        },
                        'UploadComplete': function(){
                            //队列文件处理完毕后,处理相关的事情

                        },
                        'Key': function(up, file) {
                            // 若想在前端对每个文件的key进行个性化处理，可以配置该函数
                            // 该配置必须要在 unique_names: false , save_key: false 时才生效
                            //var key = "";
                            // do something with key here
                            //return key;
                        }
                    }
                })
            })
        };
        cropper_all();
    };

    //输出模块
    exports('qiniu', function (config) {
        var _this = new qiniu(config);
        _this.render();
        return _this;
    });
});