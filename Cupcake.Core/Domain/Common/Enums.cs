using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cupcake.Core.Domain
{
    [Description("用户类型")]
    public enum UserType
    {
        [Description("超级管理员")]
        Supper = 0,
        [Description("管理员")]
        Admin = 1,
        [Description("普通用户")]
        Normal = 2,
        [Description("统一认证用户")]
        UARC = 3
    }

    [Description("发布状态")]
    public enum PublishStatus
    {
        [Description("已发布")]
        Published = 0,
        [Description("未发布")]
        NotPublished = 1
    }

    [Description("对象状态")]
    public enum ObjectStatus
    {
        [Description("启用")]
        Enable = 0,
        [Description("禁用")]
        Disable = 1
    }

    [Description("微信公众号类型")]
    public enum MPType
    {
        [Description("订阅号")]
        Subscribe = 0,
        [Description("服务号")]
        Service = 1
    }
    [Description("统一认证组织类型")]
    public enum OrganizationType
    {
        [Description("节点")]
        Node = 0,
        [Description("区")]
        District = 1,
        [Description("分组")]
        Group = 2,
        [Description("部门")]
        Department = 3,
        [Description("科室")]
        Section = 4,
        [Description("统一认证组织下面的系统组织")]
        Special
    }

    [Description("定时任务频率")]
    public enum JobFrequency
    {
        [Description("立即")]
        Immediately = 0,
        [Description("每时")]
        EveryHour = 1,
        [Description("每天")]
        EveryDay = 2,
        [Description("每周")]
        EveryWeek = 3,
        [Description("每月")]
        EveryMonth = 4
    }

    [Description("日志类型")]
    public enum LogType 
    {
        [Description("调试")]
        Debug = 0,
        [Description("信息")]
        Info = 1,
        [Description("警告")]
        Warn = 2,
        [Description("错误")]
        Error = 3,
        [Description("致命")]
        Fatal = 4
    }

    public enum Result
    {
        [Description("成功")]
        Success = 0,
        [Description("信息")]
        Info = 1,
        [Description("错误")]
        Error = 2,
        [Description("警告")]
        Warning = 3
    }

    public enum ButtonCss
    {
        Default,
        Primary,
        Success,
        Info,
        Warning,
        Danger,
        Link
    }

    public enum MediaClass
    {
        图片 = 0,
        文档 = 1,
        自定义 = 2
    }
    public enum ColumnType
    {
        [Description("NVarchar")]
        NVarchar = 0,
        [Description("Int")]
        Int = 1,
        [Description("Bit")]
        Bit = 2,
        [Description("Date")]
        Date = 3,
        [Description("NChar")]
        NChar = 4,
        [Description("Decimal")]
        Decimal = 5,
        [Description("Float")]
        Float = 6,
        [Description("NText")]
        NText = 7
    }

    public enum InputType
    {
        文本框 = 5,
        文本域 = 10,
        富文本编辑器 = 15,
        下拉列表 = 20,
        单选按钮 = 25,
        多选按钮 = 30,
        扩展控件 = 35,
        数据关联 = 40
    }
    public enum ExtendedInputType
    {
        选择用户 = 5,
        日期选择器 = 10,
        日期时间选择器 = 15,
        选择字体图标 = 17,
        选择图片 = 20,
        上传图片 = 25,
        选择表单 = 30,
    }
    public enum AssociatedObject
    {
        当前用户 = 0,
        当前用户组织 = 1,
    }
}
