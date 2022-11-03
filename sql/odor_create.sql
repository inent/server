-- --------------------------------------------------------
-- 호스트:                          127.0.0.1
-- 서버 버전:                        10.3.31-MariaDB-0ubuntu0.20.04.1 - Ubuntu 20.04
-- 서버 OS:                        debian-linux-gnu
-- HeidiSQL 버전:                  11.2.0.6213
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- odor 데이터베이스 구조 내보내기
CREATE DATABASE IF NOT EXISTS `odor` /*!40100 DEFAULT CHARACTER SET utf8mb4 */;
USE `odor`;

-- 테이블 odor.alertlists 구조 내보내기
CREATE TABLE IF NOT EXISTS `alertlists` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `alertid` int(11) NOT NULL,
  `status` varchar(50) NOT NULL DEFAULT '',
  `type` varchar(50) DEFAULT NULL,
  `name` varchar(50) NOT NULL DEFAULT '',
  `kind` varchar(50) DEFAULT NULL,
  `content` longtext DEFAULT NULL,
  `value` varchar(50) DEFAULT NULL,
  `times` datetime DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=66 DEFAULT CHARSET=utf8mb4;

-- 내보낼 데이터가 선택되어 있지 않습니다.

-- 테이블 odor.alerts 구조 내보내기
CREATE TABLE IF NOT EXISTS `alerts` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `part` varchar(50) DEFAULT NULL,
  `name` varchar(50) DEFAULT NULL,
  `type` varchar(50) DEFAULT NULL,
  `warn` varchar(50) DEFAULT NULL,
  `err` varchar(50) DEFAULT NULL,
  `setup` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=22 DEFAULT CHARSET=utf8mb4;

-- 내보낼 데이터가 선택되어 있지 않습니다.

-- 테이블 odor.alertusers 구조 내보내기
CREATE TABLE IF NOT EXISTS `alertusers` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `alertid` int(11) NOT NULL,
  `userid` varchar(50) NOT NULL,
  `type` varchar(50) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8mb4;

-- 내보낼 데이터가 선택되어 있지 않습니다.

-- 테이블 odor.attribs 구조 내보내기
CREATE TABLE IF NOT EXISTS `attribs` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `productid` varchar(50) DEFAULT NULL,
  `type` longtext DEFAULT NULL,
  `alias` longtext DEFAULT NULL,
  `name` longtext DEFAULT NULL,
  `onoff` longtext DEFAULT NULL,
  `label` longtext DEFAULT NULL,
  `spec` longtext DEFAULT NULL,
  `chemiunit` longtext DEFAULT NULL,
  `threshold` longtext DEFAULT NULL,
  `min` longtext DEFAULT NULL,
  `max` longtext DEFAULT NULL,
  `elecunit` longtext DEFAULT NULL,
  `note` longtext DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=46 DEFAULT CHARSET=utf8mb4;

-- 내보낼 데이터가 선택되어 있지 않습니다.

-- 테이블 odor.devices 구조 내보내기
CREATE TABLE IF NOT EXISTS `devices` (
  `id` varchar(50) NOT NULL,
  `productid` varchar(50) DEFAULT NULL,
  `name` longtext DEFAULT NULL,
  `macaddr` longtext DEFAULT NULL,
  `addr` longtext DEFAULT NULL,
  `geocode` varchar(50) DEFAULT NULL,
  `lati` longtext DEFAULT NULL,
  `longi` longtext DEFAULT NULL,
  `firmware` longtext DEFAULT NULL,
  `serverip` varchar(50) DEFAULT NULL,
  `serverport` varchar(50) DEFAULT NULL,
  `memo` longtext DEFAULT NULL,
  `control` longtext DEFAULT NULL,
  `status` longtext DEFAULT NULL,
  `on_nh3` tinyint(1) DEFAULT 0,
  `on_h2s` tinyint(1) DEFAULT 0,
  `on_odor` tinyint(1) DEFAULT 0,
  `on_voc` tinyint(1) DEFAULT 0,
  `on_indol` tinyint(1) DEFAULT 0,
  `on_temp` tinyint(1) DEFAULT 0,
  `on_humi` tinyint(1) unsigned DEFAULT 0,
  `on_sen1` tinyint(1) DEFAULT 0,
  `on_sen2` tinyint(1) DEFAULT 0,
  `on_sen3` tinyint(1) DEFAULT 0,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- 내보낼 데이터가 선택되어 있지 않습니다.

-- 테이블 odor.geocodes 구조 내보내기
CREATE TABLE IF NOT EXISTS `geocodes` (
  `id` varchar(50) NOT NULL,
  `name` varchar(50) NOT NULL,
  `metro` varchar(50) DEFAULT NULL,
  `district` varchar(50) DEFAULT NULL,
  `lati` varchar(50) DEFAULT NULL,
  `longi` varchar(50) DEFAULT NULL,
  `measure` varchar(50) DEFAULT NULL,
  `exurl` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- 내보낼 데이터가 선택되어 있지 않습니다.

-- 테이블 odor.idevices 구조 내보내기
CREATE TABLE IF NOT EXISTS `idevices` (
  `id` varchar(50) NOT NULL,
  `measect` varchar(50) DEFAULT '0',
  `meacycle` varchar(50) DEFAULT '0',
  `flushsect` varchar(50) DEFAULT '0',
  `restsect` varchar(50) DEFAULT '0',
  `multiple` varchar(50) DEFAULT '0',
  `ratio` varchar(50) DEFAULT '0',
  `constant` varchar(50) DEFAULT '0',
  `resolution` varchar(50) DEFAULT '0',
  `deci` varchar(50) DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- 내보낼 데이터가 선택되어 있지 않습니다.

-- 테이블 odor.jdevices 구조 내보내기
CREATE TABLE IF NOT EXISTS `jdevices` (
  `id` varchar(50) NOT NULL,
  `rex_nh3` varchar(100) DEFAULT '( x )',
  `rex_h2s` varchar(100) DEFAULT '( x )',
  `rex_odor` varchar(100) DEFAULT '( x )',
  `rex_voc` varchar(100) DEFAULT '( x )',
  `rex_ou` varchar(100) DEFAULT '( x )',
  `min1` varchar(100) DEFAULT '0',
  `min2` varchar(100) DEFAULT '0',
  `min3` varchar(100) DEFAULT '0',
  `min4` varchar(100) DEFAULT '0',
  `min5` varchar(100) DEFAULT '0',
  `rsvtime` varchar(100) DEFAULT '0',
  `rsvproc` varchar(100) DEFAULT '0',
  `odorlev` varchar(100) DEFAULT '0',
  `autoproc` varchar(100) DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- 내보낼 데이터가 선택되어 있지 않습니다.

-- 테이블 odor.monitors 구조 내보내기
CREATE TABLE IF NOT EXISTS `monitors` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `deviceid` varchar(50) DEFAULT NULL,
  `odor` longtext DEFAULT NULL,
  `silution` longtext DEFAULT NULL,
  `solidity` longtext DEFAULT NULL,
  `h2s` longtext DEFAULT NULL,
  `nh3` longtext DEFAULT NULL,
  `voc` longtext DEFAULT NULL,
  `airt` longtext DEFAULT NULL,
  `spd` longtext DEFAULT NULL,
  `tmp` longtext DEFAULT NULL,
  `hum` longtext DEFAULT NULL,
  `status` longtext DEFAULT NULL,
  `alert` longtext DEFAULT NULL,
  `sensingDt` datetime DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=81982 DEFAULT CHARSET=utf8mb4;

-- 내보낼 데이터가 선택되어 있지 않습니다.

-- 테이블 odor.products 구조 내보내기
CREATE TABLE IF NOT EXISTS `products` (
  `id` varchar(50) NOT NULL,
  `name` varchar(50) DEFAULT NULL,
  `company` varchar(50) DEFAULT NULL,
  `regist` varchar(50) DEFAULT NULL,
  `release` varchar(50) DEFAULT NULL,
  `purpose` varchar(50) DEFAULT NULL,
  `note` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- 내보낼 데이터가 선택되어 있지 않습니다.

-- 테이블 odor.users 구조 내보내기
CREATE TABLE IF NOT EXISTS `users` (
  `userid` varchar(50) NOT NULL,
  `userpw` longtext NOT NULL,
  `username` longtext DEFAULT NULL,
  `depart` longtext DEFAULT NULL,
  `position` longtext DEFAULT NULL,
  `email` longtext NOT NULL,
  `phone` longtext DEFAULT NULL,
  `role` longtext DEFAULT NULL,
  `geocode` varchar(50) DEFAULT NULL,
  `token` longtext DEFAULT NULL,
  `pushid` longtext DEFAULT NULL,
  `onweb` tinyint(1) DEFAULT NULL,
  `onmail` tinyint(1) DEFAULT NULL,
  `onsms` tinyint(1) DEFAULT NULL,
  `onpush` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`userid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- 내보낼 데이터가 선택되어 있지 않습니다.

-- 테이블 odor.worklogs 구조 내보내기
CREATE TABLE IF NOT EXISTS `worklogs` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `userid` varchar(50) DEFAULT NULL,
  `part` longtext DEFAULT NULL,
  `level` longtext DEFAULT NULL,
  `content` longtext DEFAULT NULL,
  `times` datetime DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=57210 DEFAULT CHARSET=utf8mb4;

-- 내보낼 데이터가 선택되어 있지 않습니다.

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
