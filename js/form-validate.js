//以下为修改jQuery Validation插件兼容Bootstrap的方法，没有直接写在插件中是为了便于插件升级
$.validator.setDefaults({
    highlight: function (element) {
        $(element).closest('.form-group').removeClass('has-success').addClass('has-error');
    },
    success: function (element) {
        element.closest('.form-group').removeClass('has-error').addClass('has-success');
    },
    errorElement: "span",
    errorPlacement: function (error, element) {
        if (element.is(":radio") || element.is(":checkbox")) {
            error.appendTo(element.parent().parent().parent());
        } else {
            error.appendTo(element.parent());
        }
    },
    errorClass: "help-block m-b-none text-info",
    validClass: "help-block m-b-none text-info"
});

$().ready(function () {

    // 登录验证
    var icon = "<i class='fa fa-times-circle'></i> ";
    $("#signupForm").validate({
        rules: {
            uname: "required",
            pass: "required",
            uname: {
                required: true,
                minlength: 4,
                maxlength: 16
            },
            pass: {
                required: true,
                minlength: 8,
                maxlength: 20
            }
        },
        messages: {
            uname: icon + "请输入您的用户名",
            uname: {
                required: icon + "请输入您的用户名",
                minlength: icon + "用户名必须4个字符以上",
                maxlength: icon + "用户名必须16个字符以下"
            },
            pass: icon + "请输入您的密码",
            pass: {
                required: icon + "请输入您的密码",
                minlength: icon + "密码必须8个字符以上",
                maxlength: icon + "密码必须20个字符以下"
            }
        },
        submitHandler: function () {
            var t_uname = $('input[name=uname]').val();
            var t_pass = $('input[name=pass').val();
            ajaxSubmitForm(t_uname, t_pass);
        }
    });

    //会议添加验证
    $('#addMeetingForm').validate({
        rules: {
            m_title: {
                required: true,
                minlength: 3,
                maxlength: 50
            },
            m_date: {
                required: true
            }
        },
        messages: {
            m_title: {
                required: '会议标题必须要填写',
                minlength: '标题太短',
                maxlength: '标题不能超过50字符，精简一些'
            },
            m_date: {
                required: '会议开始的日期时间要填写'
            }
        }
    });

    //议程添加验证
    $('#addItemForm').validate({
        rules: {
            item_title: {
                required: true,
                minlength: 3,
                maxlength: 50
            },
            item_time: {
                required: true,
                range: [1, 999]
            }
        },
        messages: {
            item_title: {
                required: '会议标题必须要填写',
                minlength: '标题太短',
                maxlength: '标题不能超过50字符，精简一些'
            },
            item_time: {
                required: '议程所需时间必须填写',
                range: '请输入一个介于 {0} 和 {1} 之间的值'
            }
        }
    });
    //用户添加验证
    $('#addUserForm').validate({
        rules: {
            u_username: {
                required: true,
                minlength: 4,
                maxlength: 20,
                remote: {
                    url: "UserFormUsernameValidate.ashx",
                    type: "post",
                    dataType: "json",
                    data: {
                        username: function () {
                            return $("#u_username").val().trim();
                        }
                    }
                }
            },
            u_password: {
                required: true,
                minlength: 8,
                maxlength: 80
            },
            u_re_password: {
                required: true,
                equalTo: "#u_password",
                minlength: 8,
                maxlength: 80
            }
        },
        messages: {
            u_username: {
                required: '用户名必须要填写',
                minlength: '用户名太短',
                maxlength: '用户名太长，精简一些',
                remote:'用户名已存在!'
            },
            u_password: {
                required: '密码必须要填写',
                minlength: '密码太短',
                maxlength: '密码太长，精简一些'
            },
            u_re_password: {
                required: '确认密码要填写',
                minlength: '密码太短',
                maxlength: '密码太长，精简一些',
                equalTo: '两次密码填写不一致'
            }
        }
    });
    //用户密码修改
    $('#EditUserForm').validate({
        rules: {
            u_username: {
                required: true,
                minlength: 4,
                maxlength: 20
            },
            u_password: {
                required: true,
                minlength: 8,
                maxlength: 80
            },
            u_re_password: {
                required: true,
                equalTo: "#u_password",
                minlength: 8,
                maxlength: 80
            },
            u_old_password:{
                required:true,
                remote: {
                    url: "UserFormValidate.ashx",
                    type: "post",
                    dataType: "json",
                    data: {
                        username: function () {
                            return $("#u_username").val();
                        },
                        password: function () {
                            return $("#u_old_password").val();
                        }
                    }
                }
            }
        },
        messages: {
            u_username: {
                required: '用户名必须要填写',
                minlength: '用户名太短',
                maxlength: '用户名太长，精简一些'
            },
            u_password: {
                required: '密码必须要填写',
                minlength: '密码太短',
                maxlength: '密码太长，精简一些'
            },
            u_re_password: {
                required: '确认密码要填写',
                minlength: '密码太短',
                maxlength: '密码太长，精简一些',
                equalTo: '两次密码填写不一致'
            },
            u_old_password: {
                required: '原始密码不能为空',
                remote:'原始密码不正确'
            }
        }
    });
});

function ajaxSubmitForm(uname, pass) {
    var loginLoding = $('#loginLoding');
    loginLoding.removeClass("sr-only");
    $.ajax({
        url: "../server/Login.ashx",
        data: { 'loginName': uname, 'loginPwd': pass },
        type: "post",
        cache: false,
        dataType: "json",
        timeout: 10000,
        success: function (data, textStatus) {
            if (data.isLogin) {
                loginLoding.addClass("sr-only");
                addCookie("user",uname,1,"/");
                swal({ title: "恭喜，登录成功！", text: "3秒钟后自动进入~", type: "success", confirmButtonText: "进入" }, function () {
                    loginLoding.addClass("sr-only");
                    window.location.href = "index.html";
                });
                setTimeout(function () {
                    loginLoding.addClass("sr-only");
                    window.location.href = "index.html";
                }, 3000);

            } else {
                loginLoding.addClass("sr-only");
                swal({ title: "登录失败!" + data.msg, text: "2秒钟后自动关闭~", timer: 2000, showConfirmButton: false });
            }
        },
        complete: function (XMLHttpRequest, status) {
            if (status == 'timeout') {
                loginLoding.addClass("sr-only");
                swal({ title: "亲，连接服务器超时!", text: "5秒钟后自动关闭~", timer: 5000, showConfirmButton: false });
            }
        }
    });
}
