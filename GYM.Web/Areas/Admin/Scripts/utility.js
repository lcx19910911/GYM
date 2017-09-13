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

function ShowSex(item)
{
    return item.Sex==0?"未知":(item.Sex==1?"男":"女")
}

function ShowLoading() {
    $("#loadingDiv").show();
}
function CloseLoading() {
    $("#loadingDiv").hide();
}


//验证身份证
function isCardID(obj) {
    var sId = $(obj).val();
    if (sId != "") {
        if (sId.length == 10) {

            if (!/^\d{6}19\d{2}$/.test(sId) && !/^\d{6}20\d{2}$/.test(sId)) {

                //10位全数字 澳门
                if (/^[1|5|7][0-9]{2}/.test(sId.substr(0, 3))) {
                    if (!/^[1|5|7][0-9]{6}\([0-9Aa]\)/.test(sId)) {
                        $.Nuoya.alert("你输入的澳门身份证身份证长度或格式错误")
                        return false;
                    } else {
                        return true;
                    }
                }
                    //台湾和香港为第一位引英文。但是台湾后面全为数字 香港后面跟着6个数字
                    //台湾
                else if (/^[a-zA-Z][0-9]{7}$/.test(sId.substr(0, 8))) {
                    if (!/^[a-zA-Z][0-9]{9}$/.test(sId)) {
                        $.Nuoya.alert("你输入的台湾身份证身份证长度或格式错误")
                        return false;
                    }
                    else {
                        return true;
                    }
                }
                else if (!/^((\s?[A-Za-z])|([A-Za-z]{2}))\d{6}\(([0−9aA])|([0-9aA])\)$/.test(sId)) {
                    $.Nuoya.alert("你输入的香港身份证身份证长度或格式错误")
                    return false;
                }
                else {
                    return true;
                }

            }
        }
        else if (sId.length == 18) {
            var iSum = 0;
            var info = "";

            if (!/^\d{17}(\d|x)$/i.test(sId)) {
                $.Nuoya.alert("你输入的身份证长度或格式错误")
                return false;
            }
            sId = sId.replace(/x$/i, "a");
            if (parseInt(sId.substr(0, 2)) == null) {
                $.Nuoya.alert("你的身份证地区非法")
                return false;
            }
            var sBirthday = sId.substr(6, 4) + "-" + Number(sId.substr(10, 2)) + "-" + Number(sId.substr(12, 2));
            var d = new Date(sBirthday.replace(/-/g, "/"));
            if (sBirthday != (d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate())) {
                $.Nuoya.alert("身份证上的出生日期非法")
                return false;
            }
            for (var i = 17; i >= 0; i--) iSum += (Math.pow(2, i) % 11) * parseInt(sId.charAt(17 - i), 11);
            if (iSum % 11 != 1) {
                $.Nuoya.alert("你输入的身份证号非法");
                return false;
            }
            return true;
        }
        else {
            return false;
        }
    }
    return false;
}