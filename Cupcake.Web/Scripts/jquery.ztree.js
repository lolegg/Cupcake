//树插件
(function ($) {
    var funcsetting = { controller: '' };
    var gtreesetting;
    var selNode;

    //从后台读取配置的树权限
    function LoadTreeFuncion(callback) {
        $.ajax({
            type: 'get',
            url: '/' + funcsetting.controller + '/GetTreeSetting?1=1' + "&" + $.BaseSimpleTree.orginaleSearch,
            data: '',
            success: function (data) {

                _InitSetting(data);

                if (callback instanceof Function) {
                    callback();
                }

            },
            error: function (ex) {
                //alert(ex.responseText);
            }
        });
    }

    function _InitSetting(treesetting) {
        gtreesetting = $.BaseSimpleTree.gtreesetting = treesetting;
        //var orginaleSearch = $.BaseSimpleTree.orginaleSearch = location.search.replace("?", ""); //保存原始请求参数
        var orginaleSearch = $.BaseSimpleTree.orginaleSearch = $("#hidUrlParams").val();
        if (orginaleSearch) { orginaleSearch = "&" + orginaleSearch; }

        var addHoverDomFunc = treesetting.ShowAdd == false ? false : addHoverDom;
        var beforeExpandFunc = zTreeOnBeforeExpand;//treesetting.IsAsync == false ? false : zTreeOnBeforeExpand;
        var zTreeOnCheckFunc = treesetting.ShowCheck == false ? false : zTreeOnCheck; //如果显示checkbox则绑定事件
        var showCheck = treesetting.ShowCheck;
        if (treesetting.ShowAddRoot == true) {
            $("#btnAddRoot").show();
            $("#btnAddRoot").bind("click", zTreeAddRoot);
        }

        //全部展开只在非异步模式下才有效
        if (treesetting.IsAsync == false && treesetting.ShowExpandAll == true) {
            $("#btnExpandAll").show();
            $("#btnExpandAll").bind("click", zTreeExpandAll);
        }

        //全部折叠只在非异步模式下才有效
        if (treesetting.IsAsync == false && treesetting.ShowCloseAll == true) {
            $("#btnCloseAll").show();
            $("#btnCloseAll").bind("click", zTreeCloseAll);
        }
        //搜索只在非异步模式下才有效
        if (treesetting.IsAsync == false && treesetting.ShowSearch == true) {
            //$("#btnShowSearch").show();
            //$("#btnShowSearch").bind("click", zTreeShowSearch);
            $("#btnSearch").bind("click", zTreeSearch);
        }

        $("#TreeName").text(treesetting.TreeName);

        setting = {
            check: {
                enable: showCheck
            },
            edit: {
                enable: false, //treesetting.ShowEdit,
                showRemoveBtn: false,//treesetting.ShowDelete
            },
            view: {
                addHoverDom: addHoverDomFunc,
                removeHoverDom: removeHoverDom,
                dblClickExpand: false,
                showLine: true,
                nameIsHTML: true,
                fontCss: getFontCss,
                selectedMulti: false,
                showTreeData: treesetting.ShowTreeData
            },
            data: {
                simpleData: {
                    enable: true,
                    idKey: "id",
                    pIdKey: "parentid",
                    rootPId: "0"
                },
                key: {
                    checked: "check"
                }
            },
            callback: {
                beforeClick: zTreebeforeClick,
                beforeRemove: zTreeOnRemove,
                beforeExpand: beforeExpandFunc,
                onRename: zTreeOnRename,
                beforeRename: zTreeBeforeRename,
                onCheck: zTreeOnCheckFunc,
            }
        };
    }

    //默认加载数据
    function LoadMenu() {
        $.ajax({
            type: 'get',
            url: '/' + funcsetting.controller + '/LoadTree?1=1' + "&" + $.BaseSimpleTree.orginaleSearch,
            data: '',
            success: function (data) {
                var json = data;
                $.fn.zTree.init($("#tree"), setting, json);

                var zTree = $.fn.zTree.getZTreeObj("tree");//获取ztree对象

                //加载第一个结点数据
                var nodes = zTree.getNodes();
                if (nodes.length > 0) {
                    var node = nodes[0];
                    zTree.selectNode(node);
                    zTree.setting.callback.beforeClick(zTree.setting.treeId, node);
                }
            },
            error: function (ex) {
                //alert(ex.responseText);
            }
        });

    }

    //展开加载下级数据
    function zTreeOnBeforeExpand(treeId, treeNode) {

        if (gtreesetting.IsAsync == true) {
            var t = $.fn.zTree.getZTreeObj("tree");
            var node = t.getNodesByParam("parentid", treeNode.id, null);
            if (node.length <= 0) {
                $.ajax({
                    type: 'get',
                    url: '/' + funcsetting.controller + '/LoadTree?pid=' + treeNode.id + "&" + $.BaseSimpleTree.orginaleSearch,
                    data: '',
                    success: function (data) {
                        var json = data; // eval("(" + data + ")");

                        t.addNodes(treeNode, json);
                    },
                    error: function (ex) {
                        //alert(ex.responseText);
                    }
                });
            }
        }
    }

    var g_async = false;
    //修改结点
    function zTreeOnRename(event, treeId, treeNode, isCancel) {
        //导致添加时不检测是否重名
        //if (g_async == true) {
        //    g_async = false;
        //    return;
        //}
        g_async = true;
        if (treeNode.name == "") {
            return;
        }
        var zTree = $.fn.zTree.getZTreeObj("tree");//获取ztree对象
        var url = '/' + funcsetting.controller + '/EditTree?id=' + treeNode.id + "&name=" + treeNode.name;
        if (treeNode.id == "") {
            url = '/' + funcsetting.controller + '/InsertTree?pid=' + treeNode.parentid + "&name=" + treeNode.name;
        }
        url += "&" + $.BaseSimpleTree.orginaleSearch;
        $.ajax({
            type: 'get',
            url: url,
            data: '',
            success: function (responseTxt) {
                
                if (typeof (responseTxt) == "string") {
                    if (responseTxt.indexOf('<input type="hidden" value="error" />') != -1) {
                        Notify('出错了', 'bottom-right', '4000', 'error', 'fa-times', true);
                        zTree.editName(treeNode);
                    }
                }
                else {
                    if (responseTxt.Result == 0) {
                        if (treeNode.id == "") {
                            treeNode.id = responseTxt.Id;
                        }
                        zTree.setting.callback.beforeClick(zTree.setting.treeId, treeNode);
                    }
                    else {
                        zTree.removeNode(treeNode);
                        //zTree.editName(treeNode);
                    }
                    showNotify(responseTxt);
                }

                //if (data.Status === 1) {
                //    if (data.Msg) {
                //        alert(data.Msg);
                //    }
                //    zTree.editName(treeNode);
                //}
                //else {
                //    if (treeNode.id == "") {
                //        treeNode.id = data.Id;
                //    }
                //    if (data.Msg) {
                //        alert(data.Msg);
                //    }
                //    zTree.setting.callback.beforeClick(zTree.setting.treeId, treeNode);
                //}
            },
            error: function (ex) {
                //alert(ex.responseText);
            }
        });
    }

    //修改名称之前
    function zTreeBeforeRename(treeId, treeNode, newName, isCancel) {
        var oldname = treeNode.name;
        if (newName.length == 0) {
            alert("名称不能为空");
            var zTree = $.fn.zTree.getZTreeObj("tree");
            if (treeNode.id == "") {
                zTree.removeNode(treeNode);
            }
            return false;
        }
        return true;
    }

    //点击菜单之前
    function zTreebeforeClick(treeId, treeNode) {

        var zTree = $.fn.zTree.getZTreeObj("tree");

        selNode = $.BaseSimpleTree.selNode = treeNode;



        //是否显示右边action数据
        if (zTree.setting.view.showTreeData == true) {
            if ($.BaseSimpleTree.gtreesetting.ShowParentTreeData == false && treeNode.isParent) {
                zTree.expandNode(treeNode);
            }
            else {
                $("#menetitle").text("当前菜单:" + treeNode.name);
                $("#hidParams").val("");
                LoadData();
            }
        }
        else {
            //影藏右边，同时更新左边class
            $("#treeViewRight").hide();
            $("#treeViewLeft").removeClass("col-md-4");
            $("#treeViewLeft").addClass("col-md-12");
        }

        return true;
    }

    //自定义hover事件，增加add按钮
    function addHoverDom(treeId, treeNode) {
        var zTree = $.fn.zTree.getZTreeObj("tree");

        var sObj = $("#" + treeNode.tId + "_span");


        if (treeNode.showAdd == true) {
            if (!(treeNode.editNameFlag || $("#addBtn_" + treeNode.tId).length > 0)) {
                var addStr = "<span class='button add' id='addBtn_" + treeNode.tId + "'></span>";
                sObj.after(addStr);
                var btn = $("#addBtn_" + treeNode.tId);
                if (btn) btn.bind("click", function () {
                    var zTree = $.fn.zTree.getZTreeObj("tree");

                    //延迟到实现时进行验证
                    if (funcsetting.beforeAdd) {
                        if (!funcsetting.beforeAdd.call(this, treeNode, zTree)) {
                            return;
                        }
                    }

                    //添加子借点的时候先展开当前跟结点
                    zTree.setting.callback.beforeExpand(zTree.setting.treeId, treeNode);
                    setTimeout(function () {
                        var newNode = { id: "", pId: treeNode.id, name: "新节点" };
                        newNode = zTree.addNodes(treeNode, newNode);
                        zTree.editName(newNode[0]);

                    }, 200);
                    return false;
                });
            }
        }


        if (treeNode.showRemove == true) {
            if (!(treeNode.editNameFlag || $("#removeBtn_" + treeNode.tId).length > 0)) {
                var deleteStr = "<span class='button remove' id='removeBtn_" + treeNode.tId + "'></span>";
                sObj.after(deleteStr);

                var btnRemove = $("#removeBtn_" + treeNode.tId);
                if (btnRemove) btnRemove.bind("click", function () {
                    zTree.setting.callback.beforeRemove(treeNode.tId, treeNode);

                    return false;
                });

            }
        }




        if (treeNode.showEdit == true) {
            if (!(treeNode.editNameFlag || $("#editBtn_" + treeNode.tId).length > 0)) {
                var editStr = "<span class='button edit' id='editBtn_" + treeNode.tId + "'></span>";
                sObj.after(editStr);

                var btnEdit = $("#editBtn_" + treeNode.tId);
                if (btnEdit) btnEdit.bind("click", function () {
                    zTree.editName(treeNode);
                    return false;
                });
            }
        }
    };

    function zTreeAddRoot() {
        var zTree = $.fn.zTree.getZTreeObj("tree");


        //延迟到实现时进行验证
        if (funcsetting.beforeAdd) {
            if (!funcsetting.beforeAdd.call(this, null, zTree)) {
                return;
            }
        }

        var treeNode = zTree.addNodes(null, { id: "", pId: "", isParent: false, name: "新节点" });
        if (treeNode) {
            zTree.editName(treeNode[0]);
        }
    }

    //自定义removehover事件，移除add按钮，和绑定的事件
    function removeHoverDom(treeId, treeNode) {
        $("#addBtn_" + treeNode.tId).unbind().remove();
        $("#removeBtn_" + treeNode.tId).unbind().remove();
        $("#editBtn_" + treeNode.tId).unbind().remove();
    };

    function zTreeOnRemove(treeId, treeNode) {

        var t = $.fn.zTree.getZTreeObj("tree");
        var node = t.getNodesByParam("parentid", treeNode.id, null);
        if (node.length > 0) {
            alert("父节点不能删");
            return false;
        }
        else {

            if (confirm("确认删除?") == true) {
                $.ajax({
                    type: 'get',
                    url: '/' + funcsetting.controller + '/DeleteTree?id=' + treeNode.id + (treeNode.nodeParam ? "&" + treeNode.nodeParam : "") + "&" + $.BaseSimpleTree.orginaleSearch,
                    data: '',
                    success: function (data) {
                        if (data.Status === 1) {
                            if (data.Msg) {
                                alert(data.Msg);
                            }
                        }
                        else {
                            t.removeNode(treeNode);
                        }
                    },
                    error: function (ex) {

                    }
                });
            }
        }
    }

    //checkbox事件
    function zTreeOnCheck(event, treeId, treeNode) {
        var treeObj = $.fn.zTree.getZTreeObj("tree");
        var nodes = treeObj.getCheckedNodes(true);
        var arry = new Array();


        $.each(nodes, function (index, data) {
            arry.push(data.id);
        });


        $.ajax({
            type: 'get',
            url: '/' + funcsetting.controller + '/CheckTree?checkednodes=' + arry.join(",") + "&" + $.BaseSimpleTree.orginaleSearch,
            data: '',
            success: function (responseTxt) {
                if (typeof (responseTxt) == "string") {
                    if (responseTxt.indexOf('<input type="hidden" value="error" />') != -1) {
                        Notify('出错了', 'bottom-right', '4000', 'error', 'fa-times', true);
                    }
                }
                else {
                    showNotify(responseTxt);
                }
            },
            error: function (ex) {

            }
        });

    }

    //展开全部借点
    function zTreeExpandAll() {
        var zTree = $.fn.zTree.getZTreeObj("tree");
        zTree.expandAll(true);
    }

    //全部折叠
    function zTreeCloseAll() {
        var zTree = $.fn.zTree.getZTreeObj("tree");
        zTree.expandAll(false);
    }

    //显示查找
    function zTreeShowSearch() {
        $("#divSearch").toggle();
    }

    //查找节点
    var nodelist = [];
    function zTreeSearch() {
        var key = $("#inputKey").val();
        if (key == "") return;

        var zTree = $.fn.zTree.getZTreeObj("tree");

        var nodes = zTree.getNodesByParamFuzzy("name", key, null);

        for (var i = 0, j = nodelist.length; i < j; i++) {
            nodelist[i].highlight = false
            zTree.updateNode(nodelist[i]);
        }

        nodelist = nodes;

        for (var i = 0, j = nodes.length; i < j; i++) {
            nodes[i].highlight = true

            var pNode = nodes[i].getParentNode();
            zTree.expandNode(pNode, true, true);
            zTree.updateNode(nodes[i]);


        }
        $("#inputKey").val("");
        //$("#divSearch").toggle();
    }

    //查找结果样式处理
    function getFontCss(treeId, treeNode) {

        if (treeNode.highlight) {
            return (!!treeNode.highlight) ? { color: "rgb(35, 189, 165)", "font-weight": "bold" } : { color: "#333", "font-weight": "normal" };
        }

        if (treeNode.font) {
            return eval("(" + treeNode.font + ")");
        }

    }

    //加载树右边数据
    function LoadData(_pageindex, serchparas) {
        var nodeid = "";
        var sparams = "";

        //if (serchparas) {
        //    $("#hidParams").val(serchparas);
        //}
        sparams = $("#hidParams").val();

        if (selNode) {
            nodeid = selNode.id;
        }
        if (nodeid == undefined) {
            return;
        }

        var pageindex = 0;
        if (_pageindex)
            pageindex = _pageindex;

        var params = "nodeid=" + nodeid + (selNode.nodeParam ? "&" + selNode.nodeParam : "") + "&pageindex=" + pageindex + sparams;
        $.ajax({
            url: '/' + funcsetting.controller + "/NodeView?" + $.BaseSimpleTree.orginaleSearch,
            contentType: 'application/html;charset=utf-8',
            type: 'GET',
            data: params,
            dataType: 'html',
            success: function (result) {
                //返回部分视图的html内容
                $("#treeViewRight").html(result);
            },
            error: function (xhr, status) {
                alert(xhr.responseText);
            }
        });
    }

    //提供外部代码更新节点
    function UpdateTree(treeNode) {
        var zTree = $.fn.zTree.getZTreeObj("tree");
        zTree.updateNode(treeNode);
    }

    //刷新外部代码结点
    function RefreshNode(treeNode) {
        var zTree = $.fn.zTree.getZTreeObj("tree");
        //zTree.setting.callback.beforeExpand(zTree.setting.treeId, treeNode);
        $.ajax({
            type: 'get',
            url: '/' + funcsetting.controller + '/LoadTree?pid=' + treeNode.id + (selNode.nodeParam ? "&" + selNode.nodeParam : "") + "&" + $.BaseSimpleTree.orginaleSearch,
            data: '',
            success: function (data) {
                if (data && data.length > 0) {
                    var json = data;
                    zTree.removeChildNodes(treeNode);
                    zTree.addNodes(treeNode, json);
                }
            },
            error: function (ex) {
                //alert(ex.responseText);
            }
        });

    }

    //分页*@
    function LoadPaged(pageing) {        
        var pager = $("#page");
        if (pageing.Total <= 3) {
            $("#page").html("<div class='pagination'><span>&nbsp;</span></div>");
        }
        else {
            pager.pagination(pageing.Total, {
                num_edge_entries: 2,
                num_display_entries: 4,
                prev_text: '上一页',
                next_text: '下一页',
                num_display_entries: 5,
                num_edge_entries: 1,
                link_to: 'javascript:void(0)',
                current_page: pageing.PageIndex,
                callback: Pagenation,
                items_per_page: pageing.PageSize
            });
        }
    }

    function Pagenation(page_id, jq) {
        LoadData(page_id);
    }

    //扩展一些方法,写在这里为减少依赖*@
    function StringHelper(str) {
        var params = str;
        this.Trim = function (c) {

            if (c == null || c == "") {
                var str = params.replace(/^s*/, '');
                var rg = /s/;
                var i = str.length;
                while (rg.test(str.charAt(--i)));
                return str.slice(0, i + 1);
            }
            else {
                var rg = new RegExp("^" + c + "*");
                var str = params.replace(rg, '');
                rg = new RegExp(c);
                var i = str.length;
                while (rg.test(str.charAt(--i)));
                return str.slice(0, i + 1);
            }

        }

        this.TrimStart = function (c) {
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

    }

    $.BaseSimpleTree = function (method, options) {

        if (methods[method]) {
            methods[method].apply(options);
        }
        else {

            funcsetting = $.extend(funcsetting, arguments[0]);
            LoadTreeFuncion(LoadMenu);
        }

    }

    var methods = {
        LoadPaged: function () {
            var page = this;
            LoadPaged(page);
        },
        LoadData: function () {
            LoadData("", this);
        },
        UpdateTree: function () {
            //提供外部代码更新节点ujjj;ldfdfdfdfdfdf
            UpdateTree(this);
        },
        Refresh: function () {
            //提供外部代码刷新接电
            RefreshNode(this);
        }
    };

    $.BaseSimpleTree.selNode = new Array();
    $.BaseSimpleTree.gtreesetting = new Array();
    $.BaseSimpleTree.orginaleSearch = "";
})(window.jQuery)