window.udx = {
    onload: function () {
        console.info("udx onload start...")
        this.web.gotoWindowTop();
        console.info("udx onload end...")
    },
    web: {
        gotoWindowTop: function () {
            console.info("gotoWindowTop...");
            try {
                console.log(window.top);
                if (window.top && window.top != window.self) {
                    window.top.location.href = window.location.href;
                    }
            }
            catch (e) {
                console.log(e);
            }
        },
        getBodyHeight: function () {
            console.info("getBodyHeight...")
            try {
                var w = document.body.offsetHeight;
                if (w != 0) {
                    return w;
                }
                return 0;
            }
            catch (e) {
                console.log(e);
            }
        },
        
    },
    amis: {
        load: function (amisJSON) {

            let amis = amisRequire('amis/embed');
            //// 通过替换下面这个配置来生成不同页面
            //let amisJSON = {
            //    type: 'page',
            //    title: '表单页面',
            //    body: {
            //        type: 'form',
            //        mode: 'horizontal',
            //        api: '/saveForm',
            //        body: [
            //            {
            //                label: 'Name',
            //                type: 'input-text',
            //                name: 'name'
            //            },
            //            {
            //                label: 'Email',
            //                type: 'input-email',
            //                name: 'email'
            //            }
            //        ]
            //    }
            //};
            let amisScoped = amis.embed('#root', amisJSON);


        }
    },
    setCookie: function (name, value, days) {
        var expires;
        if (days) {
            var date = new Date();
            date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
            expires = "; expires=" + date.toGMTString();
        }
        else {
            expires = "";
        }
        document.cookie = name + "=" + value + expires + "; path=/";
    },
    getReferrer: function () {
        console.log(document);
        var ref = '';
        if (document.referrer.length > 0) ref = document.referrer;
        try {
            if (ref.length == 0 && opener.location.href.length > 0) ref = opener.location.href;
        }
        catch (e) { }
        console.log(ref);
        return ref;
    }
}