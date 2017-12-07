(function () {
    const fs = require("fs");
    const xml2js = require("xml2js");


    var xml = {
        _db: {},
        init: function () {
        },

        readDir: function (dir, callback) {
            if (!callback) {
                return;
            }
            fs.readdir(dir, function (err, files) {
                if (err) {
                    console.log(err);
                    callback(null);
                    return;
                }
                callback(files);
            });
        },

        readFile: function (fileName, callback) {
            if (!callback) {
                return;
            }

            fs.readFile(fileName, "utf-8", function (err, data) {
                if (err) {
                    console.log(err);
                    callback(null);
                    return;
                }
                callback(data);
            });
        },

        xml2js: function (data, callback) {
            if (!callback) {
                return;
            }

            xml2js.parseString(data, {explicitArray: true}, function (err, json) {
                if (err) {
                    console.log(err);
                    callback(null);
                    return;
                }
                callback(json);
            });
        },

        // 扫描目录，加载配置
        load: function (dir) {
            if (!dir) {
                return;
            }

            console.log("load xml config : " + dir);
            (function (self) {
                xml.readDir(dir, function (files) {
                    if (!files) {
                        return;
                    }
                    for (var i in files) {
                        var fileName = files[i];
                        (function (file, func) {
                            xml.readFile(dir + file, function (data) {
                                if (!data) {
                                    return;
                                }
                                xml.xml2js(data, function (json) {
                                    if (!json) {
                                        return;
                                    }
                                    func(file, json);
                                });
                            });
                        }(fileName, function (fileName, json) {
                            var index = fileName.lastIndexOf(".");
                            var name = null;
                            if (index !== -1) {
                                name = fileName.substr(0, index);
                            } else {
                                name = fileName;
                            }
                            self._db[name] = json;
                        }));
                    }
                });
            }(this));
        },

        close: function () {
            this._db = {};
        },
    };

    if (typeof module !== 'undefined' && module.exports) {
        module.exports = xml;
    } else if (typeof define === 'function' && define.amd) {
        // AMD. Register as an anonymous module.
        define([], function () {
            return xml
        });
    }
}());