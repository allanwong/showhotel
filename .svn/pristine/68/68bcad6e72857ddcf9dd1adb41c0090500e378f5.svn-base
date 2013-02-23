function loadContent(options) {
    if (_.isEmpty(options) || _.isEmpty(options.url)) {
        throw "必须指定要ajax读取的url";
    }

    var fullUrl = options.url;
    if (options.data && !_.isEmpty(options.data)) {
        var query = $.param(options.data),
        ch = '?';

        if (_.str.endsWith(ch)) {
            ch = '&';
        }

        fullUrl = fullUrl + ch + query;
    }

    $("#content").block({message: " "});
    $("#content").load(fullUrl, function (responseText, textStatus, XMLHttpRequest) {
        $("#content").attr("data-url", options.url).unblock();
        // === Tooltips === //
        $('.tip').tooltip();
        $('.tip-left').tooltip({ placement: 'left' });
        $('.tip-right').tooltip({ placement: 'right' });
        $('.tip-top').tooltip({ placement: 'top' });
        $('.tip-bottom').tooltip({ placement: 'bottom' });
        if ($.isFunction(options.onLoad)) {
            options.onLoad.apply(this, [responseText, textStatus, options]);
        }
    });
}

function loadContentByVName(vname, options) {
    var url = $(_.template("#sidebar a[data-vname='<%= vname %>']", { vname: vname })).attr("data-url");
    options = $.extend(options,
        {
            url: url
        });
    loadContent(options);
}

$(function () {
    // === Sidebar navigation === //
    $('#sidebar a').click(function () {
        var url = $(this).attr("data-url");
        var parent = $(this).closest(".submenu");
        $('#sidebar li').removeClass("active");

        if (!$(this).parent().hasClass("submenu")) {
            if (parent.length) {
                parent.addClass("active");
            }
            else {
                $(this).closest("li").addClass("active");
            }
        }

        if (!_.isEmpty(url)) {
            loadContent(
                {
                    url: url
                });
        }
    });

    $('.submenu > a').click(function (e) {
        e.preventDefault();
        var submenu = $(this).siblings('ul');
        var li = $(this).parents('li');
        var submenus = $('#sidebar li.submenu ul');
        var submenus_parents = $('#sidebar li.submenu');

        if (li.hasClass('open')) {
            if (($(window).width() > 768) || ($(window).width() < 479)) {
                submenu.slideUp();
            } else {
                submenu.fadeOut(250);
            }
            li.removeClass('open');
        } else {
            if (($(window).width() > 768) || ($(window).width() < 479)) {
                submenus.slideUp();
                submenu.slideDown();
            } else {
                submenus.fadeOut(250);
                submenu.fadeIn(250);
            }
            submenus_parents.removeClass('open');
            li.addClass('open');
        }
    });

    var ul = $('#sidebar > ul');

    $('#sidebar > a').click(function (e) {
        e.preventDefault();
        var sidebar = $('#sidebar');
        if (sidebar.hasClass('open')) {
            sidebar.removeClass('open');
            ul.slideUp(250);
        } else {
            sidebar.addClass('open');
            ul.slideDown(250);
        }
    });

    // === Resize window related === //
    $(window).resize(function () {
        if ($(window).width() > 479) {
            ul.css({ 'display': 'block' });
            $('#content-header .btn-group').css({ width: 'auto' });
        }
        if ($(window).width() < 479) {
            ul.css({ 'display': 'none' });
            fix_position();
        }
        if ($(window).width() > 768) {
            $('#user-nav > ul').css({ width: 'auto', margin: '0' });
            $('#content-header .btn-group').css({ width: 'auto' });
        }
    });

    if ($(window).width() < 468) {
        ul.css({ 'display': 'none' });
        fix_position();
    }
    if ($(window).width() > 479) {
        $('#content-header .btn-group').css({ width: 'auto' });
        ul.css({ 'display': 'block' });
    }

    // === Tooltips === //
    $('.tip').tooltip();
    $('.tip-left').tooltip({ placement: 'left' });
    $('.tip-right').tooltip({ placement: 'right' });
    $('.tip-top').tooltip({ placement: 'top' });
    $('.tip-bottom').tooltip({ placement: 'bottom' });

    // === Fixes the position of buttons group in content header and top user navigation === //
    function fix_position() {
        var uwidth = $('#user-nav > ul').width();
        $('#user-nav > ul').css({ width: uwidth, 'margin-left': '-' + uwidth / 2 + 'px' });

        var cwidth = $('#content-header .btn-group').width();
        $('#content-header .btn-group').css({ width: cwidth, 'margin-left': '-' + uwidth / 2 + 'px' });
    }

    // === Style switcher === //
    $('#style-switcher i').click(function () {
        if ($(this).hasClass('open')) {
            $(this).parent().animate({ marginRight: '-=190' });
            $(this).removeClass('open');
        } else {
            $(this).parent().animate({ marginRight: '+=190' });
            $(this).addClass('open');
        }
        $(this).toggleClass('icon-arrow-left');
        $(this).toggleClass('icon-arrow-right');
    });

    $("#btn-logout").click(function () {
        var href = $(this).attr("data-href");
        $.dialog.confirm("您确定要退出系统吗？", function () {
            window.location = href;
        }, function () {
            
        });
    });

    //delegates
    $("#content").on("click", "#refreshContent", function () {
        var url = $("#content").attr("data-url");
        loadContent(
            {
                url: url
            });
    });
});