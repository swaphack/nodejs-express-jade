/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 50717
Source Host           : localhost:3306
Source Database       : sample_user

Target Server Type    : MYSQL
Target Server Version : 50717
File Encoding         : 65001

Date: 2017-12-12 15:36:11
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for user
-- ----------------------------
DROP TABLE IF EXISTS `user`;
CREATE TABLE `user` (
  `id` bigint(11) unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(20) NOT NULL COMMENT '名称',
  `pwd` varchar(20) NOT NULL COMMENT '密码',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of user
-- ----------------------------
INSERT INTO `user` VALUES ('16', 'root', '123');

-- ----------------------------
-- Table structure for user_info
-- ----------------------------
DROP TABLE IF EXISTS `user_info`;
CREATE TABLE `user_info` (
  `id` bigint(11) unsigned NOT NULL COMMENT '主键',
  `vip` smallint(255) DEFAULT '0' COMMENT 'vip等级',
  `level` smallint(6) DEFAULT '1' COMMENT '等级',
  `exp` bigint(20) DEFAULT '0' COMMENT '经验',
  `gold` bigint(20) DEFAULT '0' COMMENT '金币',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of user_info
-- ----------------------------
INSERT INTO `user_info` VALUES ('16', '0', '1', '0', '0');

-- ----------------------------
-- Table structure for user_shopping_cart
-- ----------------------------
DROP TABLE IF EXISTS `user_shopping_cart`;
CREATE TABLE `user_shopping_cart` (
  `id` bigint(11) unsigned NOT NULL,
  `items` text,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of user_shopping_cart
-- ----------------------------
