// 配置文件

// 默认端口
var ini = {};
module.exports = ini;

// 资源路径
ini.res = {
    root : "/",
    pubic : "/public/",
    files : "/files/",
    views : "/views/"
};

// 服务器
ini.server = {
    host : 'localhost',
    port : 8080
};

// 定位符
ini.url = {
    // 主页
    home : {
        jade : "index",
        url : "/index"
    }
};

// 数据库
ini.database = {
    host : 'localhost',
    port : 3306,
    user : 'root',
    password : '123456',
    database : 'game'
};

ini.xmlConfig = {
  dir : '/xml/',
};