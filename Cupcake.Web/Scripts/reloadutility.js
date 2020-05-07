/// <summary>
/// 实现js版本 string.foramt功能 
/// var template1 = "我是{0}，今年{1}了"; var result1 = template1.format("loogn", 22);
/// template2 = "我是{name}，今年{age}了";var result2 = template2.format({ name: "loogn", age: 22 });
/// </summary>
/// <returns>true,false</returns> 
String.prototype.Format = function (args) {
    var result = this;
    if (arguments.length > 0) {
        if (arguments.length == 1 && typeof (args) == "object") {
            for (var key in args) {
                if (args[key] != undefined) {
                    var reg = new RegExp("({" + key + "})", "g");
                    result = result.replace(reg, args[key]);
                }
            }
        }
        else {
            for (var i = 0; i < arguments.length; i++) {
                if (arguments[i] != undefined) {
                    var reg = new RegExp("({)" + i + "(})", "g");
                    result = result.replace(reg, arguments[i]);
                }
            }
        }
    }
    return result;
}


/// <summary>
/// 获取请求Request 
/// var request = GetRequest(); var type = request["type"];
/// </summary>
/// <returns></returns> 
function GetRequest() {
    var url = location.search;
    var _Request = {};
    if (url.indexOf("?") != -1) {
        var str = url.substr(1);
        strs = str.split("&");
        for (var i = 0; i < strs.length; i++) {
            _Request[strs[i].split("=")[0]] = strs[i].split("=")[1];
        }
    }
    return _Request;
}

String.prototype.Trim = function (c) {
    if (c == null || c == "") {
        var str = this.replace(/^s*/, '');
        var rg = /s/;
        var i = str.length;
        while (rg.test(str.charAt(--i)));
        return str.slice(0, i + 1);
    }
    else {
        var rg = new RegExp("^" + c + "*");
        var str = this.replace(rg, '');
        rg = new RegExp(c);
        var i = str.length;
        while (rg.test(str.charAt(--i)));
        return str.slice(0, i + 1);
    }
}

//去除字符串头部空格或指定字符  
String.prototype.TrimStart = function (c) {
    if (c == null || c == "") {
        var str = this.replace(/^s*/, '');
        return str;
    }
    else {
        var rg = new RegExp("^" + c + "*");
        var str = this.replace(rg, '');
        return str;
    }
}

//去除字符串尾部空格或指定字符  
String.prototype.TrimEnd = function (c) {
    if (c == null || c == "") {
        var str = this;
        var rg = /s/;
        var i = str.length;
        while (rg.test(str.charAt(--i)));
        return str.slice(0, i + 1);
    }
    else {
        var str = this;
        var rg = new RegExp(c);
        var i = str.length;
        while (rg.test(str.charAt(--i)));
        return str.slice(0, i + 1);
    }
}


//Json到Url参数相互转换
function JsonToggleUrlParam()
{

    this.ConvertJsonToUrlParams = function (json) {
       
        if (isJson(json)) {
            var strParams = JSON.stringify(json);
            return encodeURI(strParams);
        }
        else {
            return "";
        }
    }

    this.ConvertUrlParamsToJson = function (urlParams) {
        var params = urlParams;
        params = decodeURI(params);
        var json = eval("(" + params + ")");
        return json;
    }

    var isJson = function (obj) {
        var isjson = typeof (obj) == "object" && Object.prototype.toString.call(obj).toLowerCase() == "[object object]" && !obj.length;
        return isjson;
    }

    //类说明,动态创建，防止多次创建
    if (this.ShowUseage == undefined)
    {
        JsonToggleUrlParam.prototype.ShowUseage = function () {
            alert("帮助类:Json对象与编码字符串相互转换 ");
        }
    }
}

