var mysql = require("./common/mysqlDB");

module.exports.init = function (dbConfig) {
    if (!dbConfig) {
        return;
    }
    mysql.init(dbConfig);
};

module.exports.mysql = mysql;