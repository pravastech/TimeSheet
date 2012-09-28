SiteUtil = {
    genRandTxt: function () {
        var guid = "";
        for (var i = 1; i <= 16; i++) {
            var n = Math.floor(Math.random() * 36.0).toString(36);
            guid += n;
        }
        return guid
    }
}