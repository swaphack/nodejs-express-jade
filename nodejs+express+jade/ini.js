// 配置文件

// 默认端口
var ini = {};
module.exports = ini;

// 服务器
ini.server = {
    host : 'localhost',
    port : 8080,
};

ini.url = {
    home_jade : "index",
    home_url : "/index",
}

// 数据库
ini.database = {
    host : 'localhost',
    port : 3306,
    user : 'root',
    password : '12345',
    database : '',
}