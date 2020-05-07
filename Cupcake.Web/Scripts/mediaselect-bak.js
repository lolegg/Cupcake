
(function (window) {
    
    
    var $ = window.jQuery;
    if ($ == undefined) { console.error("依赖jquery.js文件"); }

    //var layer = window.layer;
    //if (layer == undefined) { console.error("依赖layer.js文件"); }
    //操作类
    function MediaSelect() {

    }

    var nodes = new Array();
    var gSetting;
    var controller;

    function _InitSetting(setting) {

        $("#btnSelect").bind("click", Select);

        $("#btnCancel").bind("click", Cancle);

        $("#btnSearch").bind("click", Search);

        if (setting.DefaultIsSingleSelected == true) {
            //单选
            $("#chkIsMulti").attr("checked", false);
        }
        else {
            //多选
            $("#chkIsMulti").attr("checked", true);
        }


        if (setting.DefaultIsListView == true) {
            //列表
            $("#chkIsPic").attr("checked", false);
        }
        else {
            //图标
            $("#chkIsPic").attr("checked", true);

        }


        if (setting.ShowMutileSelect == true) {
            //显示多选单选切换按钮
            $("#spanchkIsMulti").show();
        }


        if (setting.ShowPicSelect == true) {
            //显示图文模式
            $("#spanchkIsPic").show();
        }


        $("#chkIsMulti").bind("click", ToggleListView);

        $("#chkIsPic").bind("click", TogglePartView);

    }

    //处理请求的controller,需要继承相应的基类
    function LoadFuncion(contro) {
        controller = contro;
        $.ajax({
            type: 'get',
            url: '/' + controller + '/GetSetting',
            data: '',
            success: function (setting) {

                gSetting = setting

                _InitSetting(setting);

                LoadCheckedNodes();

                LoadMediaType();

                if (setting.DefaultIsListView) {
                    LoadData();
                }
                else {
                    LoadPicData();
                }



            },
            error: function (ex) {
                //alert(ex.responseText);
            }
        });
    }


    //加载列表模板
    function LoadData(_pageindex, serchparas) {
        var pageindex = 0;
        var sparams = "";
        if (_pageindex)
            pageindex = _pageindex;

        if (serchparas) {
            $("#hidParams").val(serchparas);
        }

        sparams = $("#hidParams").val();

        var params = "&pageindex=" + pageindex + sparams;

        $.ajax({
            url: '/' + controller + '/ListView',
            contentType: 'application/html;charset=utf-8',
            type: 'GET',
            data: params,
            dataType: 'html',
            success: function (result) {
                //返回部分视图的html内容
                $("#divData").html(result);
                $("#divData").find("input[type='checkbox']").bind("click", SaveChecked);
                RestoreChecked();
            },
            error: function (xhr, status) {
                alert(xhr.responseText);
            }
        });
    }


    //加载图文模板
    function LoadPicData(_pageindex, serchparas) {
        var pageindex = 0;
        var sparams = "";
        if (_pageindex)
            pageindex = _pageindex;

        if (serchparas) {
            $("#hidParams").val(serchparas);
        }

        sparams = $("#hidParams").val();

        var params = "&pageindex=" + pageindex + sparams;

        $.ajax({
            url: '/' + controller + '/PicView',
            contentType: 'application/html;charset=utf-8',
            type: 'GET',
            data: params,
            dataType: 'html',
            success: function (result) {
                //返回部分视图的html内容
                $("#divData").html(result);

                RestoreChecked();

                PicClickBind();
            },
            error: function (xhr, status) {
                alert(xhr.responseText);
            }
        });
    }


    //获取保存过的选项
    function LoadCheckedNodes() {
        $.ajax({
            type: 'get',
            url: '/' + controller + '/ReturnCheckedNodes',
            data: '',
            success: function (data) {
                nodes = data;
            },
            error: function (ex) {
                //alert(ex.responseText);
            }
        });
    }

    //checkbox点击事件 勾选或取消
    function SaveChecked() {
        var jq = $(this);
        var isChecked = jq.is(':checked');
        var dataid = jq.attr("data-id");
        var datavalue = jq.attr("data-value");
        var datapath = jq.attr("data-path");
        var chkIsMulti = $("#chkIsMulti").is(":checked");
        if (chkIsMulti == false) {
            //单选的情况

            //先清空集合
            nodes = new Array();

            //清空所有checkbox选择状态
            $("#divData").find("input[type='checkbox']").each(function () {
                if (dataid != $(this).attr("data-id")) {
                    $(this).attr("checked", false);
                }
            });

            //添加到集合
            if (isChecked) {
                var item = new Object();
                item.name = datavalue;
                item.id = dataid;
                item.path = datapath;
                nodes.push(item);
            }

        }
        else {

            //多选的情况
            if (isChecked) {
                //添加到集合
                var item = new Object();
                item.name = datavalue;
                item.id = dataid;
                item.path = datapath;
                nodes.push(item);
            }
            else {
                //移除
                nodes = nodes.RemoveValue(dataid);
            }
        }
    }

    //恢复已保存数据勾选状态
    function RestoreChecked() {


        if (gSetting.DefaultIsListView) {
            var pagecks = $("#divData").find("input[type='checkbox']");
            for (var i = 0; i < pagecks.length; i++) {
                var item = $(pagecks[i]);
                var dataid = item.attr("data-id");
                if (nodes.HasValue(dataid) == true) {
                    item.attr("checked", "checked");
                }
            }
        }
        else {
            var pagecks = $("#divData").find("div[data-img='true']");
            for (var i = 0; i < pagecks.length; i++) {
                var item = $(pagecks[i]);
                var dataid = item.attr("data-id");
                if (nodes.HasValue(dataid) == true) {
                    item.find("div[class='backimg']").show();
                }
            }
        }


    }

    //图文切换
    function TogglePartView() {
        var jq = $(this);
        var isChecked = jq.is(':checked');
        nodes = new Array();
        if (isChecked) {
            LoadPicData();
        }
        else {

            LoadData();
        }
    }

    //单选多选切换
    function ToggleListView() {
        var jq = $(this);
        var isChecked = jq.is(':checked');

        if (isChecked) {
            //多选
        }
        else {
            nodes = new Array();
            //单选
            $("#divData").find("div[class='backimg']").hide();

            $("#divData").find("input[type='checkbox']:checked").each(function (data, index) {
                $(this).attr("checked", false);
            });;
        }
    }


    //图文选择
    function PicClickBind() {
        $("#divData").find("div[data-img='true']").bind("click", function () {

            var chkIsMulti = $("#chkIsMulti").is(":checked");
            var dataid = $(this).attr("data-id");
            var datavalue = $(this).attr("data-value");
            var datapath = $(this).attr("data-path");
            if (chkIsMulti == false) {
                //单选
                var isShow = $(this).find("div[class='backimg']").css("display");
                if (isShow == "none") {
                    nodes = new Array();
                    $("#divData").find("div[class='backimg']").hide();
                    $(this).find("div[class='backimg']").show();

                    var item = new Object();
                    item.id = dataid;
                    item.name = datavalue;
                    item.path = datapath;
                    nodes.push(item);

                }
                else {

                    $(this).find("div[class='backimg']").hide();
                    nodes = nodes.RemoveValue(dataid);
                }
            }
            else {
                //多选
                var isShow = $(this).find("div[class='backimg']").css("display");

                if (isShow == "none") {
                    $(this).find("div[class='backimg']").show();
                    var item = new Object();
                    item.id = dataid;
                    item.name = datavalue;
                    item.path = datapath;
                    nodes.push(item);
                }
                else {

                    $(this).find("div[class='backimg']").hide();
                    nodes = nodes.RemoveValue(dataid);
                }
            }

        });
    }

    //查询
    function Search() {
        _Search();
    }

    //选择数据到父页面
    function Select() {

        if (window.parent == "" || window.parent == undefined || window.parent == null) {
            return;
        }
        else {
            if (window.parent.ReturnMediaData == undefined) {
                alert("请在父页面实现 ReturnMediaData 方法才能传值");
                return;
            }

            if (window.parent.CloseLayer == undefined) {
                alert("请在父页面实现 CloseLayer 方法才能关闭次窗口");
                return;
            }
            window.parent.ReturnMediaData(nodes);
            window.parent.CloseLayer();
        }
    }

    //取消选择关闭窗口
    function Cancle() {
        if (window.parent == "" || window.parent == undefined || window.parent == null) {
            return;
        }
        else {
            if (window.parent.CloseLayer == undefined) {
                alert("请在父页面实现 CloseLayer 方法才能关闭次窗口");
                return;
            }
            window.parent.CloseLayer();
        }
    }


    //获取资源类型
    function LoadMediaType() {
        var formaterData = "<option value={id}>{name}</option>";
        $.ajax({
            type: 'get',
            url: '/' + controller + '/GetMediaType',
            data: '',
            success: function (data) {
                var arr = new Array();
                arr.push(formaterData.Format({ id: "", name: "全部" }));
                for (var i = 0, j = data.length; i < j; i++) {
                    arr.push(formaterData.Format({ id: data[i].Id, name: data[i].Name }));
                }
                $("#selMediaType").html(arr.join());

                SelBindClick();
            },
            error: function (ex) {
                //alert(ex.responseText);
            }
        });

    }

    //类型选择事件
    function SelBindClick() {
        $("#selMediaType").change(function () {
            _Search();
        });
    }

    //
    function _Search() {
        var mediatypeid = $("#selMediaType").children('option:selected').val();
        if (mediatypeid == undefined)
            mediatypeid = "";
        var name = $("#txtSearch").val();
        if (gSetting.DefaultIsListView) {
            LoadData(0, "&type=" + mediatypeid + "&name=" + name);
        }
        else {
            LoadPicData(0, "&type=" + mediatypeid + "&name=" + name);
        }
    }



    /********************************分start页/*********************************/
    //列表分页
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

    //图文分页
    function LoadPicPaged(pageing) {
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
                callback: PagenationPic,
                items_per_page: pageing.PageSize
            });
        }
    }
    function PagenationPic(page_id, jq) {
        LoadPicData(page_id);
    }
    /********************************分end页/*********************************/


    /***上传对start话框********/
    function UploadFile(setting) {

        var jsonHelper = new JsonToggleUrlParam();
        strParams = jsonHelper.ConvertJsonToUrlParams(setting);
        $("#reloadparas").val(strParams);
        showModal2('ddd', '/FileUpload/FileUpload?p=' + strParams);
        return;
        layer.open({
            type: 2,
            title: '图片选择',
            shadeClose: true,
            shade: 0.3,
            offset: '10px',
            area: ['600px', '90%'],
            content: '/FileUpload/FileUpload?p=' + strParams
        });
    }
    /*****上传对end话框******/

    Array.prototype.RemoveValue = function (id) {
        var list = this;
        if (list == null || list.length < 0) {
            return new Array();
        }

        for (var i = 0; i < list.length; i++) {
            if (list[i].id == id) {
                list.splice(i, 1);
            }
        }
        return list;
    }

    Array.prototype.HasValue = function (id) {
        var list = this;
        if (list == null || list.length < 0) {
            return false;
        }

        for (var i = 0; i < list.length; i++) {
            if (list[i].id == id) {
                return true;
            }
        }
        return false;
    }
    window.MediaSelect = new MediaSelect();

    //加载和绑定事件
    window.MediaSelect.InitPage = LoadFuncion;

    //加载图片风格
    window.MediaSelect.LoadPicData = LoadPicData;

    //加载列表风格
    window.MediaSelect.LoadData = LoadData;

    //列表分页
    window.MediaSelect.LoadPaged = LoadPaged;

    //图表分页
    window.MediaSelect.LoadPicPaged = LoadPicPaged;

    //上传对话框
    window.MediaSelect.UploadFile = UploadFile;

})(window);