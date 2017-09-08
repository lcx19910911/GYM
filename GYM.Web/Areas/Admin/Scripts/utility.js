Date.prototype.format = function (format) {
    var o = {
        "M+": this.getMonth() + 1, //month 
        "d+": this.getDate(), //day 
        "h+": this.getHours(), //hour 
        "m+": this.getMinutes(), //minute 
        "s+": this.getSeconds(), //second 
        "q+": Math.floor((this.getMonth() + 3) / 3), //quarter 
        "S": this.getMilliseconds() //millisecond 
    }

    if (/(y+)/.test(format)) {
        format = format.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    }

    for (var k in o) {
        if (new RegExp("(" + k + ")").test(format)) {
            format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
        }
    }
    return format;
}

//  格式化长数字
function FLongNumber(s, n) {
    n = n > 0 && n <= 20 ? n : 0;
    s = parseFloat((s + "").replace(/[^\d\.-]/g, "")).toFixed(n) + "";
    var l = s.split(".")[0].split("").reverse(),
    t = "";
    for (i = 0; i < l.length; i++) {
        t += l[i] + ((i + 1) % n == 0 && (i + 1) != l.length ? "," : "");
    }
    return t.split("").reverse().join("");
}



//  是否为整数
function IsInteger(input) {
    var reg = /^\d+$/;
    return reg.test(input);
}

//  是否邮箱
function IsEmail(input) {
    var reg = /^([\w\d\.]+@([\w\d]+\.)+[\w\d]+)$/;
    return reg.test(input);
}

//  是否移动电话
function IsMobilePhone(input) {
    var reg = /^([\d]{11})$/;
    return reg.test(input);
}

//  是否电话号码
function IsTelephone(input) {
    var reg = /^(([\d]{3,4})?([-\s]?)[\d]{7,8})$/;
    return reg.test(input);
}
//  是否浮点数
function IsDecimal(input) {
    var reg = /^(\d+(\.\d+)?)$/;
    return reg.test(input);
}
//判断字符串是否仅为某个重复的字符
function IsRepeatChar(input, char) {
    arrayObj = input.split(char);
    var flag = new Boolean(1);
    for (i = 0; i < arrayObj.length; i++) {
        if (arrayObj[i].length > 0) {
            flag = false;
        }
    }
    return flag;
}
//校验组织机构代码是否符合校验规则
function CheckOrgCode(orgcode) {

    orgcode.Trim;

    var ws = [3, 7, 9, 10, 5, 8, 4, 2];
    var str = '0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ';
    var reg = /^([0-9A-Z]){8}-[0-9|X]$/;
    if (!reg.test(orgcode)) {
        return "组织机构代码不正确";
    }

    var sum = 0;
    for (var i = 0; i < 8; i++) {
        sum += str.indexOf(orgcode.charAt(i)) * ws[i];
    }
    var c9 = 11 - (sum % 11);
    if (c9 == 10) {
        c9 = 'X';
    } else if (c9 == 11) {
        c9 = '0';
    }

    if (c9.toString() != orgcode.charAt(9)) {
        return "企业代码不正确，请输入正确的组织机构代码";
    }
    else
        return "";
}
//获取Json的日期
function RenderDate(data) {
    var da = eval('new ' + data.replace('/', '', 'g').replace('/', '', 'g'));
    return da.getFullYear() + "-" + (da.getMonth() + 1) + "-" + da.getDate();
}
//获取Json的时间
function RenderTime(data) {
    var da = eval('new ' + data.replace('/', '', 'g').replace('/', '', 'g'));
    var days = da.getDate();
    var hours = da.getHours();
    var minutes = da.getMinutes();
    var seconds = da.getSeconds();
    return da.getFullYear() + "-" + (da.getMonth() + 1) + "-" + (days < 10 ? "0" + days : days) + " " + (hours < 10 ? "0" + hours : hours) + ":" + (minutes < 10 ? "0" + minutes : minutes) + ":" + (seconds < 10 ? "0" + seconds : seconds);
}
function CurentTime() {
    var now = new Date();
    var year = now.getFullYear();
    var month = now.getMonth() + 1;
    var day = now.getDate();
    var hh = now.getHours();
    var mm = now.getMinutes();
    var clock = year + "-";
    if (month < 10)
        clock += "0";
    clock += month + "-";
    if (day < 10)
        clock += "0";
    clock += day + " ";
    if (hh < 10)
        clock += "0";
    clock += hh + ":";
    if (mm < 10) clock += '0';
    clock += mm;
    return (clock);
}

function ShowLoading() {
    $("#loadingDiv").show();
}
function CloseLoading() {
    $("#loadingDiv").hide();
}