/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 50717
Source Host           : localhost:3306
Source Database       : sample_data

Target Server Type    : MYSQL
Target Server Version : 50717
File Encoding         : 65001

Date: 2017-12-12 15:35:49
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for item
-- ----------------------------
DROP TABLE IF EXISTS `item`;
CREATE TABLE `item` (
  `id` int(11) unsigned NOT NULL COMMENT '物品编号',
  `name` varchar(50) DEFAULT NULL COMMENT '物品名称',
  `icon` varchar(255) DEFAULT NULL COMMENT '图片资源',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of item
-- ----------------------------
INSERT INTO `item` VALUES ('1', 'item1', 'sample/images/icon/item/dominion_1000.png');
INSERT INTO `item` VALUES ('2', 'item2', 'sample/images/icon/item/dominion_2000.png');
INSERT INTO `item` VALUES ('3', 'item3', 'sample/images/icon/item/dominion_3000.png');
INSERT INTO `item` VALUES ('4', 'item4', 'sample/images/icon/item/dominion_4000.png');
INSERT INTO `item` VALUES ('5', 'item5', 'sample/images/icon/item/dominion_1000.png');
INSERT INTO `item` VALUES ('6', 'item6', 'sample/images/icon/item/dominion_2000.png');

-- ----------------------------
-- Table structure for shop_item
-- ----------------------------
DROP TABLE IF EXISTS `shop_item`;
CREATE TABLE `shop_item` (
  `id` int(11) unsigned NOT NULL DEFAULT '0' COMMENT '商品编号',
  `item_id` int(11) DEFAULT NULL COMMENT '物品编号',
  `type` tinyint(4) DEFAULT '0' COMMENT '0-普通，1-热销，2-新品',
  `price` decimal(10,2) DEFAULT '0.00' COMMENT '价格',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of shop_item
-- ----------------------------
INSERT INTO `shop_item` VALUES ('1', '1', '1', '10.00');
INSERT INTO `shop_item` VALUES ('2', '2', '2', '20.00');
INSERT INTO `shop_item` VALUES ('3', '3', '0', '30.00');
INSERT INTO `shop_item` VALUES ('4', '4', '1', '5.00');
INSERT INTO `shop_item` VALUES ('5', '5', '2', '10.00');
