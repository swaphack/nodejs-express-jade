/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 50717
Source Host           : localhost:3306
Source Database       : game

Target Server Type    : MYSQL
Target Server Version : 50717
File Encoding         : 65001

Date: 2017-11-23 21:13:06
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for db_shop_item
-- ----------------------------
DROP TABLE IF EXISTS `db_shop_item`;
CREATE TABLE `db_shop_item` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(50) DEFAULT NULL COMMENT '物品名称',
  `price` float(10,2) DEFAULT '0.00' COMMENT '价格',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of db_shop_item
-- ----------------------------
INSERT INTO `db_shop_item` VALUES ('1', 'item1', '1.00');
INSERT INTO `db_shop_item` VALUES ('2', 'item2', '2.00');
INSERT INTO `db_shop_item` VALUES ('3', 'item3', '3.00');
INSERT INTO `db_shop_item` VALUES ('4', 'item4', '4.00');
INSERT INTO `db_shop_item` VALUES ('5', 'item5', '5.00');
INSERT INTO `db_shop_item` VALUES ('6', 'item6', '6.00');

-- ----------------------------
-- Table structure for user
-- ----------------------------
DROP TABLE IF EXISTS `user`;
CREATE TABLE `user` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(20) NOT NULL COMMENT '名称',
  `pwd` varchar(20) NOT NULL COMMENT '密码',
  `gold` float(11,2) unsigned zerofill NOT NULL DEFAULT '00000000.00' COMMENT '金币',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of user
-- ----------------------------
INSERT INTO `user` VALUES ('1', 'root', '123', '00000000.00');
INSERT INTO `user` VALUES ('2', 'test1', '123', '00000000.00');

-- ----------------------------
-- Table structure for user_shop
-- ----------------------------
DROP TABLE IF EXISTS `user_shop`;
CREATE TABLE `user_shop` (
  `id` int(11) NOT NULL,
  `items` text,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of user_shop
-- ----------------------------
INSERT INTO `user_shop` VALUES ('1', '');
