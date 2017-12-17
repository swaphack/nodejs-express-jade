// 配置文件

// 默认端口
var ini = {};
module.exports = ini;

// 资源路径
ini.res = {
    // 根目录
    root : "/",
    // 通用代码
    common : "/common/",
    // 页面
    pubic : "/public/",
    // 文件
    files : "/files/",
    // 界面
    jade : "/views/",
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
    // 数据
    data : {
        host : 'localhost',
        port : 3306,
        user : 'root',
        password : '123456',
        database : 'sample_data'
    },
    // 用户
    user : {
        host : 'localhost',
        port : 3306,
        user : 'root',
        password : '123456',
        database : 'sample_user'
    },
    // 日志
    log : {
        host : 'localhost',
        port : 3306,
        user : 'root',
        password : '123456',
        database : 'sample_log'
    },
};

ini.xmlConfig = {
  dir : '/xml/',
};