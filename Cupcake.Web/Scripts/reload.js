

function getThemeColorFromCss(n) { var t = $("<span><\/span>").hide().appendTo("body"), i; return t.addClass(n), i = t.css("color"), t.remove(), i } function InitiateSideMenu() {
    $(".sidebar-toggler").on("click", function () {
        
        //return $("#sidebar").toggleClass("hide");
        //点击sidebar-toggler是的样式问题
        if ($("#sidebar").hasClass("hide")) {
            $("#sidebar").removeClass("hide");
            $(".breadcrumb").css("margin-left", "22px");
            return;
        } else {
            $("#sidebar").addClass("hide");
            $(".breadcrumb").css("margin-left", "50px");
        }
        $(".sidebar-toggler").toggleClass("active"), !1
       
    });
    if ($(".page-sidebar").hasClass("hide")) {
        alert("4545656")
        //$(".breadcrumb").css("margin-left", "50px")
    }
    var n = $("#sidebar").hasClass("menu-compact");
    $("#sidebar-collapse").on("click", function () {
    $("#sidebar").is(":visible") || $("#sidebar").toggleClass("hide"); $("#sidebar").toggleClass("menu-compact");
    $(".sidebar-collapse").toggleClass("active");

    n = $("#sidebar").hasClass("menu-compact");
    n && $(".open > .submenu").removeClass("open");
    if ($("#sidebar").hasClass("menu-compact") == true) {
        $("#sidebar").addClass("menu-compact");
        $(".page-content").css({ "margin-left": "43px", "background": "#eee" });
       
        /*左边侧边栏闭合 ie8情况*/
        if (navigator.appName == "Microsoft Internet Explorer" && navigator.appVersion.split(";")[1].replace(/[ ]/g, "") == "MSIE8.0") {
            $(".sidebar-collapse").hover(function () {
                $(this).children(".collapse-icon").css("color","#f40")
            }, function () {
                $(this).children(".collapse-icon").css("color", "#f40")
            });
            
            $(".page-body").css({ "background-color": "#d3d7da", "z-index": "1", "left": "4px" });

            $(".submenu").css({ "border": "solid 1px #eee", "z-index": "1000","display":"none" });
            $(".sidebar-menu li ul li").css("border-top", "solid 1px #eee");
            $(".menu-dropdown").css({ "border": "solid 1px #f3f3f3", "width": "41.99px" });
            //$(".open").css({ "border-left": "solid 1px #f3f3f3", "width": "41.99px" });
            //$(".open").hover(function () {
            //    $(this).css("width", "42.5px");
            //}, function () {
            //    $(this).css("width", "41.99px");
            //});
                   
            //$(".sidebar-menu li").hover(function () {
              
            //    $(this).children(".submenu").css("display", "block");
            //    $(this).find("a .menu-text").css("display", "block");

            //}, function () {
            //    $(this).children(".submenu").css("display", "none");
            //    $(this).children("a").children(".menu-text").css("display", "none");
            //});
        }
    }
    else {    
        $("#sidebar").removeClass("menu-compact");
        $(".page-content").css("margin-left", "224px");
        $(".sidebar_ie8").css("width", "219px");
        $(".page-body").css("left", "0px");

        /*左边侧边栏展开 ie8情况*/
        if (navigator.appName == "Microsoft Internet Explorer" && navigator.appVersion.split(";")[1].replace(/[ ]/g, "") == "MSIE8.0") {
            //$(".submenu li").hover(function () {
            //    $(this).children(".left_sidebar_border").css("display","block")
            //}, function () {
            //    $(this).sliblings().children(".left_sidebar_border").css("display", "none")
            //});
            $(".sidebar-menu li ul li").css("border-top", "none");
            $(".open").css("width", "218.88px")
            $(".menu-dropdown").css({ "border": "solid 1px #f3f3f3", "width": "219px" });
            $(".open").hover(function () {
                $(this).css("width", "218.88px");
            }, function () {
                $(this).css("width", "218.88px");
            });
            
           
        }
        
    }
});
    /*判断ie8的情况===============================*/
    var browser = navigator.appName
    var b_version = navigator.appVersion
    var version = b_version.split(";");
    //var trim_Version = version[1].replace(/[ ]/g, "");
function iEchoose(){
   
    if (navigator.appName == "Microsoft Internet Explorer" && navigator.appVersion.split(";")[1].replace(/[ ]/g, "") == "MSIE8.0") {
         alert("我是标准IE8浏览器")
        $(".fullscreen").hide();
        $(".sidebar-toggler").hide();
        $(".searchhelper").css("background-color", "#e9e9e9");
        //$(".page-header").css("min-height", "37px");
        //$(".loading-container").css("display", "none");
         $(".searchinput").css("border", "solid 1px #eee");
        $(".sidebar_ie8").css("display", "block");
        $(".page-body").css({ "background": "#d3d7da", "margin-top": "-10px" });
        $(".sidebar-menu li").css("position","relative")
        //$(".page-content").css("background", "#eee")
        $(".sidebar-menu li").hover(function () {
            $(this).children(".ie8_hover_border").css("display", "block");
        }, function () {
            $(this).children(".ie8_hover_border").css("display", "none");
        });
       
    }
   
   
    
}

window.onload = function () {
    iEchoose();
}
   

//将折叠的代码展开
$(".sidebar-menu").on("click",
function (t) {
    var i = $(t.target).closest("a"),
    u,
    r,
    f;
    if (i && i.length != 0) {
        if (!i.hasClass("menu-dropdown")) return n && i.get(0).parentNode.parentNode == this && (u = i.find(".menu-text").get(0), t.target != u && !$.contains(u, t.target)) ? !1 : void 0;
        if (r = i.next().get(0), !$(r).is(":visible")) {
            if (f = $(r.parentNode).closest("ul"), n && f.hasClass("sidebar-menu")) return;
            f.find("> .open > .submenu").each(function () {
                this == r || $(this.parentNode).hasClass("active") || $(this).slideUp(200).parent().removeClass("open")
            })
        }
        return n && $(r.parentNode.parentNode).hasClass("sidebar-menu") ? !1 : ($(r).slideToggle(200).parent().toggleClass("open"), !1)
    }
})
}
function InitiateWidgets() {
    $('.widget-buttons *[data-toggle="maximize"]').on("click",
    function (n) {
        n.preventDefault();
        var t = $(this).parents(".widget").eq(0),
        i = $(this).find("i").eq(0),
        r = "fa-compress",
        u = "fa-expand";
        t.hasClass("maximized") ? (i && i.addClass(u).removeClass(r), t.removeClass("maximized"), t.find(".widget-body").css("height", "auto")) : (i && i.addClass(r).removeClass(u), t.addClass("maximized"), maximize(t))
    });
    $('.widget-buttons *[data-toggle="collapse"]').on("click",
    function (n) {
        n.preventDefault();
        var t = $(this).parents(".widget").eq(0),
        r = t.find(".widget-body"),
        i = $(this).find("i"),
        u = "fa-plus",
        f = "fa-minus",
        e = 300;
        t.hasClass("collapsed") ? (i && i.addClass(f).removeClass(u), t.removeClass("collapsed"), r.slideUp(0,
        function () {
            r.slideDown(e)
        })) : (i && i.addClass(u).removeClass(f), r.slideUp(200,
        function () {
            t.addClass("collapsed")
        }))
    });
    $('.widget-buttons *[data-toggle="dispose"]').on("click",
    function (n) {
        n.preventDefault();
        var i = $(this),
        t = i.parents(".widget").eq(0);
        t.hide(300,
        function () {
            t.remove()
        })
    })
}
function maximize(n) {
    if (n) {
        var t = $(window).height(),
        i = n.find(".widget-header").height();
        n.find(".widget-body").height(t - i)
    }
}
function scrollTo(n, t) {473981680
    var i = n && n.size() > 0 ? n.offset().top : 0;
    jQuery("html,body").animate({
        scrollTop: i + (t ? t : 0)
    },
    "slow")
}
function Notify(n, t, i, r, u, f) {
    toastr.options.positionClass = "toast-" + t;
    toastr.options.extendedTimeOut = 0;
    toastr.options.timeOut = i;
    toastr.options.closeButton = f;
    toastr.options.iconClass = u + " toast-" + r;
    toastr.custom(n)
}
function InitiateSettings() {
    readCookie("navbar-fixed-top") != null && readCookie("navbar-fixed-top") == "true" && ($("#checkbox_fixednavbar").prop("checked", !0), $(".navbar").addClass("navbar-fixed-top"));
    readCookie("sidebar-fixed") != null && readCookie("sidebar-fixed") == "true" && ($("#checkbox_fixedsidebar").prop("checked", !0), $(".page-sidebar").addClass("sidebar-fixed"));
    readCookie("breadcrumbs-fixed") != null && readCookie("breadcrumbs-fixed") == "true" && ($("#checkbox_fixedbreadcrumbs").prop("checked", !0), $(".page-breadcrumbs").addClass("breadcrumbs-fixed"));
    readCookie("page-header-fixed") != null && readCookie("page-header-fixed") == "true" && ($("#checkbox_fixedheader").prop("checked", !0), $(".page-header").addClass("page-header-fixed"));
    $("#checkbox_fixednavbar").change(function () {
        $(".navbar").toggleClass("navbar-fixed-top");
        $("#checkbox_fixedsidebar").is(":checked") && ($("#checkbox_fixedsidebar").prop("checked", !1), $(".page-sidebar").toggleClass("sidebar-fixed"));
        $("#checkbox_fixedbreadcrumbs").is(":checked") && !$(this).is(":checked") && ($("#checkbox_fixedbreadcrumbs").prop("checked", !1), $(".page-breadcrumbs").toggleClass("breadcrumbs-fixed"));
        $("#checkbox_fixedheader").is(":checked") && !$(this).is(":checked") && ($("#checkbox_fixedheader").prop("checked", !1), $(".page-header").toggleClass("page-header-fixed"));
        setCookiesForFixedSettings()
    });
    $("#checkbox_fixedsidebar").change(function () {
        $(".page-sidebar").toggleClass("sidebar-fixed");
        $("#checkbox_fixednavbar").is(":checked") || ($("#checkbox_fixednavbar").prop("checked", !0), $(".navbar").toggleClass("navbar-fixed-top"));
        $("#checkbox_fixedbreadcrumbs").is(":checked") && !$(this).is(":checked") && ($("#checkbox_fixedbreadcrumbs").prop("checked", !1), $(".page-breadcrumbs").toggleClass("breadcrumbs-fixed"));
        $("#checkbox_fixedheader").is(":checked") && !$(this).is(":checked") && ($("#checkbox_fixedheader").prop("checked", !1), $(".page-header").toggleClass("page-header-fixed"));
        setCookiesForFixedSettings()
    });
    $("#checkbox_fixedbreadcrumbs").change(function () {
        $(".page-breadcrumbs").toggleClass("breadcrumbs-fixed");
        $("#checkbox_fixedsidebar").is(":checked") || ($("#checkbox_fixedsidebar").prop("checked", !0), $(".page-sidebar").toggleClass("sidebar-fixed"));
        $("#checkbox_fixednavbar").is(":checked") || ($("#checkbox_fixednavbar").prop("checked", !0), $(".navbar").toggleClass("navbar-fixed-top"));
        $("#checkbox_fixedheader").is(":checked") && !$(this).is(":checked") && ($("#checkbox_fixedheader").prop("checked", !1), $(".page-header").toggleClass("page-header-fixed"));
        setCookiesForFixedSettings()
    });
    $("#checkbox_fixedheader").change(function () {
        $(".page-header").toggleClass("page-header-fixed");
        $("#checkbox_fixedbreadcrumbs").is(":checked") || ($("#checkbox_fixedbreadcrumbs").prop("checked", !0), $(".page-breadcrumbs").toggleClass("breadcrumbs-fixed"));
        $("#checkbox_fixedsidebar").is(":checked") || ($("#checkbox_fixedsidebar").prop("checked", !0), $(".page-sidebar").toggleClass("sidebar-fixed"));
        $("#checkbox_fixednavbar").is(":checked") || ($("#checkbox_fixednavbar").prop("checked", !0), $(".navbar").toggleClass("navbar-fixed-top"));
        setCookiesForFixedSettings()
    })
}
function setCookiesForFixedSettings() {
    createCookie("navbar-fixed-top", $("#checkbox_fixednavbar").is(":checked"), 100);
    createCookie("sidebar-fixed", $("#checkbox_fixedsidebar").is(":checked"), 100);
    createCookie("breadcrumbs-fixed", $("#checkbox_fixedbreadcrumbs").is(":checked"), 100);
    createCookie("page-header-fixed", $("#checkbox_fixedheader").is(":checked"), 100)
}
function getcolor(n) {
    switch (n) {
        case "themeprimary":
            return themeprimary;
        case "themesecondary":
            return themesecondary;
        case "themethirdcolor":
            return themethirdcolor;
        case "themefourthcolor":
            return themefourthcolor;
        case "themefifthcolor":
            return themefifthcolor;
        default:
            return n
    }
}
function switchClasses(n, t) {
    var u = document.getElementsByClassName(n),
    r;
    for (i = u.length - 1; i >= 0; i--) hasClass(u[i], "dropdown-menu") || (addClass(u[i], n + "-temp"), removeClass(u[i], n));
    for (r = document.getElementsByClassName(t), i = r.length - 1; i >= 0; i--) hasClass(r[i], "dropdown-menu") || (addClass(r[i], n), removeClass(r[i], t));
    for (tempClasses = document.getElementsByClassName(n + "-temp"), i = tempClasses.length - 1; i >= 0; i--) hasClass(tempClasses[i], "dropdown-menu") || (addClass(tempClasses[i], t), removeClass(tempClasses[i], n + "-temp"))
}
function addClass(n, t) {
    var i = n.className;
    i && (i += " ");
    n.className = i + t
}
function removeClass(n, t) {
    var i = " " + n.className + " ";
    n.className = i.replace(" " + t, "").replace(/^\s+/g, "").replace(/\s+$/g, "")
}
function hasClass(n, t) {
    var i = " " + n.className + " ",
    r = " " + t + " ";
    return i.indexOf(r) != -1
}
var themeprimary = getThemeColorFromCss("themeprimary"),
themesecondary = getThemeColorFromCss("themesecondary"),
themethirdcolor = getThemeColorFromCss("themethirdcolor"),
themefourthcolor = getThemeColorFromCss("themefourthcolor"),
themefifthcolor = getThemeColorFromCss("themefifthcolor"),
rtlchanger,
popovers,
hoverpopovers;
$("#skin-changer li a").click(function () {
    createCookie("current-skin", $(this).attr("rel"), 10);
    window.location.reload()
});
rtlchanger = document.getElementById("rtl-changer");
location.pathname != "/index-rtl-fa.html" && location.pathname != "/index-rtl-ar.html" && (readCookie("rtl-support") ? (switchClasses("pull-right", "pull-left"), switchClasses("databox-right", "databox-left"), switchClasses("item-right", "item-left"), $(".navbar-brand small img").attr("src", "assets/img/logo-rtl.png"), rtlchanger != null && (document.getElementById("rtl-changer").checked = !0)) : rtlchanger != null && (rtlchanger.checked = !1), rtlchanger != null && (rtlchanger.onchange = function () {
    this.checked ? createCookie("rtl-support", "true", 10) : eraseCookie("rtl-support");
    setTimeout(function () {
        window.location.reload()
    },
    600)
}));
$(window).load(function () {
    setTimeout(function () {
        $(".loading-container").addClass("loading-inactive")
    },
    0)
});
$("#btn-setting").on("click",
function () {
    $(".navbar-account").toggleClass("setting-open")
});
$("#fullscreen-toggler").on("click",
function () {
    var n = document.documentElement;
    $("body").hasClass("full-screen") ? ($("body").removeClass("full-screen"), $("#fullscreen-toggler").removeClass("active"), document.exitFullscreen ? document.exitFullscreen() : document.mozCancelFullScreen ? document.mozCancelFullScreen() : document.webkitExitFullscreen && document.webkitExitFullscreen()) : ($("body").addClass("full-screen"), $("#fullscreen-toggler").addClass("active"), n.requestFullscreen ? n.requestFullscreen() : n.mozRequestFullScreen ? n.mozRequestFullScreen() : n.webkitRequestFullscreen ? n.webkitRequestFullscreen() : n.msRequestFullscreen && n.msRequestFullscreen())
});
popovers = $("[data-toggle=popover]");
$.each(popovers,
function () {
    $(this).popover({
        html: !0,
        template: '<div class="popover ' + $(this).data("class") + '"><div class="arrow"><\/div><h3 class="popover-title ' + $(this).data("titleclass") + '">Popover right<\/h3><div class="popover-content"><\/div><\/div>'
    })
});
hoverpopovers = $("[data-toggle=popover-hover]");
$.each(hoverpopovers,
function () {
    $(this).popover({
        html: !0,
        template: '<div class="popover ' + $(this).data("class") + '"><div class="arrow"><\/div><h3 class="popover-title ' + $(this).data("titleclass") + '">Popover right<\/h3><div class="popover-content"><\/div><\/div>',
        trigger: "hover"
    })
});
$("[data-toggle=tooltip]").tooltip({
    html: !0
});
InitiateSideMenu();
InitiateSettings();
InitiateWidgets();
/*
//# sourceMappingURL=reload.js.map
*/

//禁用对话框
function disableDialog(src, completeCallback) {
    dialog("确定禁用该记录吗?", "注意", src, completeCallback);
}
//删除对话框
function deleteDialog(src, completeCallback) {
    dialog("确定删除该记录吗?", "警告", src, completeCallback);
}
//弹出对话框
function dialog(msg, title, src, completeCallback) {
    bootbox.dialog({
        message: msg,
        title: title,
        buttons: {
            confirm: {
                label: "确 定",
                className: "btn-danger",
                callback: function () {
                    $.post(src, function (responseTxt, statusTxt, xhr) {
                        if (typeof (responseTxt) == "string") {
                            if (responseTxt.indexOf('<input type="hidden" value="error" />') != -1) {
                                Notify('出错了', 'bottom-right', '4000', 'error', 'fa-times', true);
                            }
                        }
                        else {
                            if (responseTxt.Result == 0) {
                                if (completeCallback) {
                                    completeCallback();
                                }
                                else {
                                    loadListView();
                                }
                            }
                            showNotify(responseTxt);
                        }
                    });
                }
            },
            cancel: {
                label: "取 消",
                className: "btn-default"
            }
        }
    });
}

//获取请求的url参数
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
String.prototype.trimEnd = function (c) {
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

String.prototype.replaceAll = function (s1, s2) {
    return this.replace(new RegExp(s1, "gm"), s2);
}

function submitForm(pageIndex, selector) {
    var sl = ".page-body";
    if (selector) {
        if (typeof (selector) == "string") {
            sl = selector;
        }
    }
    var form = $(sl).find("form:first");
    var data;
    if (form.length > 0) {
        var controls = form.find("input,select,textarea");
        if (controls.length > 0) {
            data = controls.serializeArray();
        }
    }
    if (pageIndex) {
        var pi = {};
        pi["name"] = "pageIndex";
        pi["value"] = pageIndex.toString();
        data.push(pi);
    }
    var action = $(sl).find("form:first").attr("action");
    $(sl).load(action, data);
}
//加载列表视图
function loadListView(pageIndex) {
    submitForm(pageIndex);
}

function loadModalView1(pageIndex) {
    submitForm(pageIndex, "#cupcakeModal1 .modal-body");
}
function loadModalView2(pageIndex) {
    submitForm(pageIndex, "#cupcakeModal2 .modal-body");
}

//加载树视图
function loadTreeView(pageIndex, selector) {
    submitForm(pageIndex, "#treeViewRight");
    //删除节点后无法刷新tree
    //var zTree = $.fn.zTree.getZTreeObj("tree");//获取ztree对象
    //var nodes = $.BaseSimpleTree.selNode;
    //加载第一个结点数据
    //var nodes = zTree.getNodes();
    //zTree.updateNode(nodes);
    //if (nodes.length > 0) {
    //    var node = nodes[0];
    //    zTree.selectNode(node);
    //    zTree.setting.callback.beforeClick(zTree.setting.treeId, node);
    //}

    //var sl = "#treeViewRight";
    //if (selector) {
    //    if (typeof (selector) == "string") {
    //        sl = selector;
    //    }
    //}
    //var data = $(sl).find("form:first").find("input,select,textarea").serializeArray();
    //if (pageIndex) {
    //    var pi = {};
    //    pi["name"] = "pageIndex";
    //    pi["value"] = pageIndex.toString();
    //    data.push(pi);
    //}
    //var action = $(sl).find("form:first").attr("action");
    //$(sl).load(action, data);
}

//初始化列表分页设置
function initPager(selector, pageInfo, callback) {
    var pager = $(selector);
    if (pageInfo.Total <= pageInfo.PageSize) {
        pager.html("<div class='pagination'></div>");
    }
    else {
        pager.pagination(pageInfo.Total, {
            prev_text: '上一页',
            next_text: '下一页',
            num_display_entries: 5,
            num_edge_entries: 2,
            link_to: 'javascript:void(0)',
            current_page: pageInfo.PageIndex,
            callback: callback,
            items_per_page: pageInfo.PageSize
        });
    }
}

//提交模态框
function loadModal(selector, successCallback, customAction) {
    $(selector).find('.modal-footer').children().each(function (i) {
        if (i != 0) {
            $(this).attr("disabled", "disabled");
        }
    });

    var para = $(selector).find('.modal-body').find("input,select,textarea").serializeArray();
    if (!customAction) {
        customAction = $(selector).find('.modal-body').find("form:first").attr("action");
    }
    $(selector).find('.modal-body').load(customAction, para, function (responseTxt, statusTxt, xhr) {
        if (responseTxt[0] == "{") {
            var ajaxResult = eval("(" + responseTxt + ")");
            if (ajaxResult.Result == 0) {
                $(selector).parent().modal("hide");
                $(selector).find('button').removeAttr("disabled");
                if (successCallback) {
                    successCallback();
                }
                else {
                    loadListView();
                }
            }
            showNotify(ajaxResult);
        }
        else {
            if (responseTxt.indexOf('<input type="hidden" value="error" />') == -1) {
                $(selector).find('button').removeAttr("disabled");
            }
        }
    });
}

function loadModal1(successCallback, customAction) {
    loadModal("#cupcakeModal1", successCallback, customAction);
}
function loadModal2(successCallback, customAction) {
    loadModal("#cupcakeModal2", successCallback, customAction);
}

function showNotify(ajaxResult) {
    switch (ajaxResult.Result) {
        case 0:
            Notify(ajaxResult.Message == "" ? '成功' : ajaxResult.Message, 'bottom-right', '4000', 'success', 'fa-check', true);
            break;
        case 1:
            Notify(ajaxResult.Message == "" ? '信息' : ajaxResult.Message, 'bottom-right', '4000', 'info', 'fa-info', true);
            break;
        case 2:
            Notify(ajaxResult.Message == "" ? '失败' : ajaxResult.Message, 'bottom-right', '4000', 'error', 'fa-times', true);
            break;
        case 3:
            Notify(ajaxResult.Message == "" ? '警告' : ajaxResult.Message, 'bottom-right', '4000', 'warning', 'fa-warning', true);
            break;
    }
}

//弹出自定义模态框
function showModal(selector, title, src, data, buttonInfos, size) {
    initModalButtons(selector, buttonInfos);
    if (size) {
        switch (size) {
            case "lg":
                $(selector).addClass("modal-lg");
                break;
            case "sm":
                $(selector).addClass("modal-sm");
                break;
        }
    }
    $(selector).find('.modal-title').text(title);
    $(selector).find('.modal-footer').children().eq(0).removeAttr("disabled");

    $(selector).find('.modal-body').load(src, data, function (responseTxt, statusTxt, xhr) {
        if (responseTxt.indexOf("<input type=\"hidden\" value=\"error\" />") != -1) {
            $(selector).find('.modal-footer').children().each(function (i) {
                if (i != 0) {
                    $(this).attr("disabled", "disabled");
                }
            });
        }
    });

    $(selector).parent().modal("show");
}

function showModal1(title, src, data, buttonInfos, size) {
    showModal("#cupcakeModal1", title, src, data, buttonInfos, size);
}
function showModal2(title, src, data, buttonInfos, size) {
    showModal("#cupcakeModal2", title, src, data, buttonInfos, size);
}

//弹出查看模态框(只有关闭按钮)
function showViewModal1(title, src, data, size) {
    showModal('#cupcakeModal1', title, src, data, null, size);    
}
function showViewModal2(title, src, data, size) {
    showModal('#cupcakeModal2', title, src, data, null, size);
}

function initEditButtons(editCallback) {
    var buttonInfos;
    if (editCallback) {
        buttonInfos = {
            button1: '<button type="button" class="btn btn-default shiny" onclick="' + editCallback + '"><i class="fa fa-save"></i> 保存并关闭</button>'
        }
    }
    else {
        buttonInfos = {
            button1: '<button type="button" class="btn btn-default shiny" onclick="loadModal1();"><i class="fa fa-save"></i> 保存并关闭</button>'
        }
    }
    return buttonInfos;
}
//弹出编辑模态框(关闭按钮 + 保存并关闭)
function showEditModal1(title, src, data, editCallback, size) {
    var buttonInfos = initEditButtons(editCallback);
    showModal('#cupcakeModal1', title, src, data, buttonInfos, size);
}
function showEditModal2(title, src, data, editCallback, size) {
    var buttonInfos = initEditButtons(editCallback);
    showModal('#cupcakeModal2', title, src, data, buttonInfos, size);
}

//初始化模态框按钮
function initModalButtons(selector, buttonsInfo) {
    $(selector).find('.modal-footer').html('<button type="button" class="btn btn-default shiny" data-dismiss="modal"><i class="fa fa-power-off"></i> 关 闭</button>');
    if (buttonsInfo) {
        $.each(buttonsInfo, function (button, html) {
            $(selector).find('.modal-footer').append(html.replaceAll("@", "'"));
        });
    }
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

//初始化选日期组件
function initFormDate() {
    if ($('#BeginDate').length > 0) {
        var beginDate = $('#BeginDate').datepicker().on('changeDate', function (ev) {
            beginDate.hide();
        }).data('datepicker');
    }

    if ($('#EndDate').length > 0) {
        var endDate = $('#EndDate').datepicker().on('changeDate', function (ev) {
            endDate.hide();
        }).data('datepicker');
    }
}

function checkboxAll(selector, es) {
    if (es.is(':checked') == true) {
        $(selector).find("input[type='checkbox']").prop("checked", true);
    }
    else {
        $(selector).find("input[type='checkbox']").prop("checked", false);
    }
}


//Json到Url参数相互转换
function JsonToggleUrlParam() {

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
    if (this.ShowUseage == undefined) {
        JsonToggleUrlParam.prototype.ShowUseage = function () {
            alert("帮助类:Json对象与编码字符串相互转换 ");
        }
    }
}

$.each($('#cupcakeModal1,#cupcakeModal2'), function () {
    $(this).parent().on('hidden.bs.modal', function (e) {
        $(this).find('.modal-body').html("");
        $(this).children().eq(0).removeClass("modal-lg");
        $(this).children().eq(0).removeClass("modal-sm");
    });
});