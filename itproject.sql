-- phpMyAdmin SQL Dump
-- version 4.9.2
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1:3306
-- Generation Time: Dec 18, 2019 at 04:07 AM
-- Server version: 10.4.10-MariaDB
-- PHP Version: 7.3.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `itproject`
--

-- --------------------------------------------------------

--
-- Table structure for table `daiktas`
--

DROP TABLE IF EXISTS `daiktas`;
CREATE TABLE IF NOT EXISTS `daiktas` (
  `pavadinimas` varchar(255) COLLATE utf8_bin NOT NULL,
  `aprasymas` varchar(255) COLLATE utf8_bin NOT NULL,
  `kodas` int(11) NOT NULL,
  `kaina` double NOT NULL,
  `parduodamas` tinyint(1) NOT NULL,
  `kiekis` int(11) NOT NULL,
  `bukle` varchar(255) COLLATE utf8_bin NOT NULL,
  `pagaminimo_data` date NOT NULL,
  `fk_Sandelisid` int(11) NOT NULL,
  PRIMARY KEY (`kodas`),
  KEY `sandeliuojamas` (`fk_Sandelisid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

--
-- Dumping data for table `daiktas`
--

INSERT INTO `daiktas` (`pavadinimas`, `aprasymas`, `kodas`, `kaina`, `parduodamas`, `kiekis`, `bukle`, `pagaminimo_data`, `fk_Sandelisid`) VALUES
('Nvidia Geforce 970', 'Vaizdo plokštė', 150, 199.99, 1, 1100, 'Naujas', '2018-10-00', 1),
('Samsung F12345', 'Monitorius', 151, 500.51, 0, 1010, 'Naujas', '2015-12-00', 1),
('ąčęėĄČĖĖĘ', 'asdasd', 522, 500, 1, 540, 'Naujas', '2019-12-03', 2),
('AMD Radeon 500', 'Vaizdo plokšte', 560, 299.99, 1, 500, 'Naujas', '2015-01-00', 2);

-- --------------------------------------------------------

--
-- Table structure for table `daikto_kiekis_nuoma`
--

DROP TABLE IF EXISTS `daikto_kiekis_nuoma`;
CREATE TABLE IF NOT EXISTS `daikto_kiekis_nuoma` (
  `kiekis` int(11) NOT NULL,
  `id_Daikto_kiekis_nuoma` int(11) NOT NULL AUTO_INCREMENT,
  `fk_Nuomos_sutartisid` int(11) NOT NULL,
  `fk_Daiktaskodas` int(11) NOT NULL,
  PRIMARY KEY (`id_Daikto_kiekis_nuoma`),
  KEY `yra1` (`fk_Nuomos_sutartisid`),
  KEY `fkc_daikto_kiekis_nuoma_daiktas` (`fk_Daiktaskodas`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `daikto_kiekis_pardavimas`
--

DROP TABLE IF EXISTS `daikto_kiekis_pardavimas`;
CREATE TABLE IF NOT EXISTS `daikto_kiekis_pardavimas` (
  `kiekis` int(11) NOT NULL,
  `id_Daikto_kiekis_pardavimas` int(11) NOT NULL AUTO_INCREMENT,
  `fk_Pardavimo_sutartisid` int(11) NOT NULL,
  `fk_Daiktaskodas` int(11) NOT NULL,
  PRIMARY KEY (`id_Daikto_kiekis_pardavimas`),
  KEY `yra2` (`fk_Pardavimo_sutartisid`),
  KEY `fkc_daikto_kiekis_pardavimas_daiktas` (`fk_Daiktaskodas`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `daikto_kiekis_pardavimas`
--

INSERT INTO `daikto_kiekis_pardavimas` (`kiekis`, `id_Daikto_kiekis_pardavimas`, `fk_Pardavimo_sutartisid`, `fk_Daiktaskodas`) VALUES
(4, 1, 1, 560),
(5, 2, 1, 151),
(1, 3, 2, 150);

-- --------------------------------------------------------

--
-- Table structure for table `is_vartotojas`
--

DROP TABLE IF EXISTS `is_vartotojas`;
CREATE TABLE IF NOT EXISTS `is_vartotojas` (
  `vardas` varchar(255) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `pavarde` varchar(255) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `gimimo_data` date NOT NULL,
  `el_pastas` varchar(255) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `adresas` varchar(255) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `slapyvardis` varchar(255) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `slaptazodis` varchar(255) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `dirba_nuo` date NOT NULL DEFAULT current_timestamp(),
  `darbo_valandos` varchar(255) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `alga` double NOT NULL,
  `parduotuves_adresas` varchar(255) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `fk_Sandelisid` int(11) DEFAULT NULL,
  `typeSelector` char(255) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  PRIMARY KEY (`id`),
  KEY `dirba` (`fk_Sandelisid`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `is_vartotojas`
--

INSERT INTO `is_vartotojas` (`vardas`, `pavarde`, `gimimo_data`, `el_pastas`, `adresas`, `slapyvardis`, `slaptazodis`, `dirba_nuo`, `darbo_valandos`, `alga`, `parduotuves_adresas`, `id`, `fk_Sandelisid`, `typeSelector`) VALUES
('Valdas', 'Šorys', '1998-10-06', 'valdas@test.com', 'Kaunas, Gričiupio g. 5696', 'valdas123', 'nezinau', '2019-12-01', '9:00-18:00', 958, 'Kaunas', 1, 1, 'Taisytojas');

-- --------------------------------------------------------

--
-- Table structure for table `klientas`
--

DROP TABLE IF EXISTS `klientas`;
CREATE TABLE IF NOT EXISTS `klientas` (
  `vardas` varchar(255) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `pavarde` varchar(255) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `el_pastas` varchar(255) CHARACTER SET utf8 COLLATE utf8_bin DEFAULT NULL,
  `telefono_nr` varchar(255) CHARACTER SET utf8 COLLATE utf8_bin DEFAULT NULL,
  `adresas` varchar(255) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `pasto_kodas` varchar(255) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `id_Klientas` int(11) NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`id_Klientas`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `klientas`
--

INSERT INTO `klientas` (`vardas`, `pavarde`, `el_pastas`, `telefono_nr`, `adresas`, `pasto_kodas`, `id_Klientas`) VALUES
('Tadas', 'Marusk', 'tadas@gmail.com', '4848444', 'Kazkur 84-12', '26461', 1),
('Nojus', 'Rimeisis', 'nojus@gmail.com', '483326', 'Eda 48', '84566', 2);

-- --------------------------------------------------------

--
-- Table structure for table `nuomos_sutartis`
--

DROP TABLE IF EXISTS `nuomos_sutartis`;
CREATE TABLE IF NOT EXISTS `nuomos_sutartis` (
  `sudarymo_data` date NOT NULL,
  `grazinimo_data` date DEFAULT NULL,
  `kaina` double NOT NULL,
  `busena` varchar(255) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL DEFAULT 'ivykdyta',
  `pastabos` varchar(255) CHARACTER SET utf8 COLLATE utf8_bin DEFAULT NULL,
  `id_Nuomos_sutartis` int(11) NOT NULL AUTO_INCREMENT,
  `fk_Klientasid` int(11) NOT NULL,
  `fk_ISVartotojas` int(11) NOT NULL,
  PRIMARY KEY (`id_Nuomos_sutartis`),
  KEY `uzsako1` (`fk_Klientasid`),
  KEY `sudaro1` (`fk_ISVartotojas`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `nuomos_sutartis`
--

INSERT INTO `nuomos_sutartis` (`sudarymo_data`, `grazinimo_data`, `kaina`, `busena`, `pastabos`, `id_Nuomos_sutartis`, `fk_Klientasid`, `fk_ISVartotojas`) VALUES
('2019-12-10', '2019-12-25', 420220, 'neivykdyta', 'Testas1', 1, 1, 1),
('2019-12-19', '2019-12-26', 420220, 'ivykdyta', 'Testas2', 2, 1, 1),
('2019-12-06', NULL, 420220, 'ivykdyta', NULL, 3, 1, 1),
('2009-06-15', NULL, 420220, 'neivykdyta', NULL, 4, 1, 1),
('2019-12-18', NULL, 1683, 'neivykdyta', 'Subraižytas', 5, 2, 1),
('2019-12-18', NULL, 1689, 'neivykdyta', NULL, 9, 1, 1),
('2019-12-18', NULL, 153, 'neivykdyta', NULL, 10, 2, 1);

-- --------------------------------------------------------

--
-- Table structure for table `pardavimo_sutartis`
--

DROP TABLE IF EXISTS `pardavimo_sutartis`;
CREATE TABLE IF NOT EXISTS `pardavimo_sutartis` (
  `sudarymo_data` date NOT NULL,
  `kaina` double NOT NULL,
  `busena` varchar(255) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL DEFAULT 'ivykdyta',
  `id_Pardavimo_sutartis` int(11) NOT NULL AUTO_INCREMENT,
  `fk_Klientasid` int(11) NOT NULL,
  `fk_ISvartotojas` int(11) NOT NULL,
  PRIMARY KEY (`id_Pardavimo_sutartis`),
  KEY `uzsako2` (`fk_Klientasid`),
  KEY `sudaro2` (`fk_ISvartotojas`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `pardavimo_sutartis`
--

INSERT INTO `pardavimo_sutartis` (`sudarymo_data`, `kaina`, `busena`, `id_Pardavimo_sutartis`, `fk_Klientasid`, `fk_ISvartotojas`) VALUES
('2019-12-09', 500, 'ivykdyta', 1, 2, 1),
('2019-12-01', 200, 'istrinta', 2, 1, 1);

-- --------------------------------------------------------

--
-- Table structure for table `sandelis`
--

DROP TABLE IF EXISTS `sandelis`;
CREATE TABLE IF NOT EXISTS `sandelis` (
  `adresas` varchar(255) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `pasto_kodas` varchar(255) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `plotas` double NOT NULL,
  `telefono_nr` varchar(255) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `el_pastas` varchar(255) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `id_Sandelis` int(11) NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`id_Sandelis`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `sandelis`
--

INSERT INTO `sandelis` (`adresas`, `pasto_kodas`, `plotas`, `telefono_nr`, `el_pastas`, `id_Sandelis`) VALUES
('asdasd', 'asdasd', 10, 'asdasd', 'asdasd', 1),
('Kaunas', 'LT-12345', 500, '+37061234567', 'sandelis1@gmail.com', 2);

-- --------------------------------------------------------

--
-- Table structure for table `taisomasirenginys`
--

DROP TABLE IF EXISTS `taisomasirenginys`;
CREATE TABLE IF NOT EXISTS `taisomasirenginys` (
  `pavadinimas` varchar(255) CHARACTER SET utf8 COLLATE utf8_bin DEFAULT NULL,
  `kodas` int(11) DEFAULT NULL,
  `komentaras` varchar(255) CHARACTER SET utf8 COLLATE utf8_bin DEFAULT NULL,
  `id_TaisomasIrenginys` int(11) NOT NULL AUTO_INCREMENT,
  `fk_Taisymasid` int(11) NOT NULL,
  PRIMARY KEY (`id_TaisomasIrenginys`),
  KEY `taiso` (`fk_Taisymasid`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `taisymas`
--

DROP TABLE IF EXISTS `taisymas`;
CREATE TABLE IF NOT EXISTS `taisymas` (
  `palikimo_data` date NOT NULL,
  `kaina` int(11) NOT NULL,
  `gedimas` int(11) NOT NULL,
  `busena` char(17) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `id_Taisymas` int(11) NOT NULL AUTO_INCREMENT,
  `fk_Klientasid` int(11) NOT NULL,
  `fk_ISVartotojas` int(11) NOT NULL,
  PRIMARY KEY (`id_Taisymas`),
  KEY `uzsako3` (`fk_Klientasid`),
  KEY `sukuria` (`fk_ISVartotojas`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `uzsakymas`
--

DROP TABLE IF EXISTS `uzsakymas`;
CREATE TABLE IF NOT EXISTS `uzsakymas` (
  `sukurimo_data` date NOT NULL,
  `altiktas` tinyint(1) NOT NULL DEFAULT 0,
  `altikimo_data` date DEFAULT NULL,
  `kiekis` int(11) NOT NULL,
  `id_Uzsakymas` int(11) NOT NULL AUTO_INCREMENT,
  `fk_ISvartotojas` int(11) NOT NULL,
  `fk_daiktokodas` int(11) NOT NULL,
  PRIMARY KEY (`id_Uzsakymas`),
  KEY `sudaro3` (`fk_ISvartotojas`),
  KEY `fk_daiktokodas` (`fk_daiktokodas`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `uzsakymas`
--

INSERT INTO `uzsakymas` (`sukurimo_data`, `altiktas`, `altikimo_data`, `kiekis`, `id_Uzsakymas`, `fk_ISvartotojas`, `fk_daiktokodas`) VALUES
('2019-12-17', 1, '2019-12-17', 10, 5, 1, 522),
('2019-12-17', 1, '2019-12-17', 500, 6, 1, 151),
('2019-12-17', 1, '2019-12-17', 10, 7, 1, 151),
('2019-12-17', 1, '2019-12-17', 500, 8, 1, 150),
('2019-12-17', 1, '2019-12-17', 500, 9, 1, 150),
('2019-12-17', 0, NULL, 500, 10, 1, 560);

-- --------------------------------------------------------

--
-- Table structure for table `veiklos_istorija`
--

DROP TABLE IF EXISTS `veiklos_istorija`;
CREATE TABLE IF NOT EXISTS `veiklos_istorija` (
  `data` date NOT NULL,
  `veiksmas` varchar(255) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `id_Veiklos_istorija` int(11) NOT NULL AUTO_INCREMENT,
  `fk_ISvartotojas` int(11) NOT NULL,
  PRIMARY KEY (`id_Veiklos_istorija`),
  KEY `fk_ISvartotojas` (`fk_ISvartotojas`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `daiktas`
--
ALTER TABLE `daiktas`
  ADD CONSTRAINT `sandeliuojamas` FOREIGN KEY (`fk_Sandelisid`) REFERENCES `sandelis` (`id_Sandelis`);

--
-- Constraints for table `daikto_kiekis_nuoma`
--
ALTER TABLE `daikto_kiekis_nuoma`
  ADD CONSTRAINT `fkc_daikto_kiekis_nuoma_daiktas` FOREIGN KEY (`fk_Daiktaskodas`) REFERENCES `daiktas` (`kodas`),
  ADD CONSTRAINT `yra1` FOREIGN KEY (`fk_Nuomos_sutartisid`) REFERENCES `nuomos_sutartis` (`id_Nuomos_sutartis`);

--
-- Constraints for table `daikto_kiekis_pardavimas`
--
ALTER TABLE `daikto_kiekis_pardavimas`
  ADD CONSTRAINT `fkc_daikto_kiekis_pardavimas_daiktas` FOREIGN KEY (`fk_Daiktaskodas`) REFERENCES `daiktas` (`kodas`),
  ADD CONSTRAINT `yra2` FOREIGN KEY (`fk_Pardavimo_sutartisid`) REFERENCES `pardavimo_sutartis` (`id_Pardavimo_sutartis`);

--
-- Constraints for table `is_vartotojas`
--
ALTER TABLE `is_vartotojas`
  ADD CONSTRAINT `dirba` FOREIGN KEY (`fk_Sandelisid`) REFERENCES `sandelis` (`id_Sandelis`);

--
-- Constraints for table `nuomos_sutartis`
--
ALTER TABLE `nuomos_sutartis`
  ADD CONSTRAINT `sudaro1` FOREIGN KEY (`fk_ISVartotojas`) REFERENCES `is_vartotojas` (`id`),
  ADD CONSTRAINT `uzsako1` FOREIGN KEY (`fk_Klientasid`) REFERENCES `klientas` (`id_Klientas`);

--
-- Constraints for table `pardavimo_sutartis`
--
ALTER TABLE `pardavimo_sutartis`
  ADD CONSTRAINT `sudaro2` FOREIGN KEY (`fk_ISvartotojas`) REFERENCES `is_vartotojas` (`id`),
  ADD CONSTRAINT `uzsako2` FOREIGN KEY (`fk_Klientasid`) REFERENCES `klientas` (`id_Klientas`);

--
-- Constraints for table `taisomasirenginys`
--
ALTER TABLE `taisomasirenginys`
  ADD CONSTRAINT `taiso` FOREIGN KEY (`fk_Taisymasid`) REFERENCES `taisymas` (`id_Taisymas`);

--
-- Constraints for table `taisymas`
--
ALTER TABLE `taisymas`
  ADD CONSTRAINT `sukuria` FOREIGN KEY (`fk_ISVartotojas`) REFERENCES `is_vartotojas` (`id`),
  ADD CONSTRAINT `uzsako3` FOREIGN KEY (`fk_Klientasid`) REFERENCES `klientas` (`id_Klientas`);

--
-- Constraints for table `uzsakymas`
--
ALTER TABLE `uzsakymas`
  ADD CONSTRAINT `sudaro3` FOREIGN KEY (`fk_ISvartotojas`) REFERENCES `is_vartotojas` (`id`),
  ADD CONSTRAINT `uzsakytas` FOREIGN KEY (`fk_daiktokodas`) REFERENCES `daiktas` (`kodas`);

--
-- Constraints for table `veiklos_istorija`
--
ALTER TABLE `veiklos_istorija`
  ADD CONSTRAINT `veiklos_istorija_ibfk_1` FOREIGN KEY (`fk_ISvartotojas`) REFERENCES `is_vartotojas` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
