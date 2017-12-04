var lg = require("./common/index");

// 初始化数据库
module.exports.initMysql = function (dbConfig) {
    if (!dbConfig) {
        return;
    }
    lg.mysql.init(dbConfig);
};

// 初始化xml配置
module.exports.initXml = function (xmlConfig) {
    if (!xmlConfig) {
        return;
    }

    lg.xml.init();
    lg.xml.load(__dirname + xmlConfig.dir);
};

module.exports.mysql = lg.mysql;
module.exports.xml = lg.xml;