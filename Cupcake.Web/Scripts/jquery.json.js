jQuery.extend({
   evalJSON: function(json) {
     return eval("(" + json + ")");
   }
});
jQuery.extend({
    toJSON:function(object){
        var type = typeof object;
        if ('object' == type) {
            if (Array == object.constructor) 
                type = 'array';
            else if (RegExp == object.constructor)
                type = 'regexp';
            else
                type = 'object';
        }
        switch (type) {
            case 'undefined':
            case 'unknown':
                return;
            case 'function':
            case 'boolean':
            case 'regexp':
                return object.toString();
            case 'number':
                return isFinite(object) ? object.toString() : 'null';
            case 'string':
                // qianwt 2012-12-26 加上 反斜杠, 单引号, 双引号, 回车, 换行, 制表符 替换成UNICODE编码
                // replace(/\x5C/g, "\\u005C").replace(/\x27/g, "\\u0027").replace(/\x22/g, "\\u0022").replace(/\x0D/g, "\\u000D").replace(/\x0A/g, "\\u000A").replace(/\x09/g, "\\u0009")
                return '"' + object.replace(/(|")/g, "$1").replace(/n|r|t/g, function(){
                            var a = arguments[0];
                            return (a == 'n') ? 'n': (a == 'r') ? 'r': (a == 't') ? 't': ""
                        }).replace(/\x5C/g, "\\u005C").replace(/\x27/g, "\\u0027").replace(/\x22/g, "\\u0022").replace(/\x0D/g, "\\u000D").replace(/\x0A/g, "\\u000A").replace(/\x09/g, "\\u0009") + '"';
            case 'object':
                if (object === null) 
                    return 'null';
                var results = [];
                for (var property in object) {
                    var value = jQuery.toJSON(object[property]);
                    if (value !== undefined) results.push(jQuery.toJSON(property) + ':' + value);
                }
                return '{' + results.join(',') + '}';
            case 'array':
                var results = [];
                for (var i = 0; i < object.length; i++) {
                    var value = jQuery.toJSON(object[i]);
                    if (value !== undefined) results.push(value);
                }
                return '[' + results.join(',') + ']';
        }
    }
});