-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Apr 20, 2026 at 11:47 AM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `acadenodb`
--

-- --------------------------------------------------------

--
-- Table structure for table `academictasktypes`
--

CREATE TABLE `academictasktypes` (
  `TypeID` varchar(10) NOT NULL,
  `GradeID` varchar(10) NOT NULL,
  `Name` varchar(100) NOT NULL,
  `Weight` double NOT NULL,
  `TargetScore` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `academictasktypes`
--

INSERT INTO `academictasktypes` (`TypeID`, `GradeID`, `Name`, `Weight`, `TargetScore`) VALUES
('TYP-1', 'GRD-1', 'Laboratory Activity', 40, 100);

-- --------------------------------------------------------

--
-- Table structure for table `academicyears`
--

CREATE TABLE `academicyears` (
  `YearID` varchar(10) NOT NULL,
  `UserID` varchar(10) NOT NULL,
  `Name` varchar(20) NOT NULL,
  `isCurrent` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `academicyears`
--

INSERT INTO `academicyears` (`YearID`, `UserID`, `Name`, `isCurrent`) VALUES
('YR-01', 'U-001', '2025-2026', 0);

-- --------------------------------------------------------

--
-- Table structure for table `courses`
--

CREATE TABLE `courses` (
  `CourseID` varchar(10) NOT NULL,
  `TermID` varchar(10) NOT NULL,
  `CourseCode` varchar(10) NOT NULL,
  `Name` varchar(100) NOT NULL,
  `Units` int(11) NOT NULL,
  `TargetGrade` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `courses`
--

INSERT INTO `courses` (`CourseID`, `TermID`, `CourseCode`, `Name`, `Units`, `TargetGrade`) VALUES
('CRS-1', 'TRM-1', 'IT-211', 'Database Management System', 3, 1);

-- --------------------------------------------------------

--
-- Table structure for table `grades`
--

CREATE TABLE `grades` (
  `GradeID` varchar(10) NOT NULL,
  `CourseID` varchar(10) NOT NULL,
  `RawGrade` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `grades`
--

INSERT INTO `grades` (`GradeID`, `CourseID`, `RawGrade`) VALUES
('GRD-1', 'CRS-1', 1);

-- --------------------------------------------------------

--
-- Table structure for table `scheduleentries`
--

CREATE TABLE `scheduleentries` (
  `EntryID` varchar(10) NOT NULL,
  `CourseID` varchar(10) NOT NULL,
  `Day` int(11) NOT NULL,
  `StartTime` datetime NOT NULL,
  `EndTime` datetime NOT NULL,
  `Session` varchar(20) NOT NULL,
  `Room` varchar(50) NOT NULL,
  `Building` int(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `tasks`
--

CREATE TABLE `tasks` (
  `TaskID` varchar(10) NOT NULL,
  `UserID` varchar(10) NOT NULL,
  `Name` varchar(100) NOT NULL,
  `Difficulty` int(11) NOT NULL,
  `StartDate` datetime NOT NULL,
  `DueDate` datetime NOT NULL,
  `Status` int(11) NOT NULL,
  `RiskLevel` int(11) NOT NULL,
  `isCompleted` tinyint(1) NOT NULL,
  `isAcademic` double NOT NULL,
  `TypeID` varchar(10) NOT NULL,
  `CourseID` varchar(10) NOT NULL,
  `Score` double NOT NULL,
  `MaxScore` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tasks`
--

INSERT INTO `tasks` (`TaskID`, `UserID`, `Name`, `Difficulty`, `StartDate`, `DueDate`, `Status`, `RiskLevel`, `isCompleted`, `isAcademic`, `TypeID`, `CourseID`, `Score`, `MaxScore`) VALUES
('TSK-1', 'U-001', 'LAB ACT 1', 4, '2026-04-20 07:00:00', '2026-04-20 10:00:00', 4, 1, 1, 1, 'TYP-1', 'CRS-1', 47, 50);

-- --------------------------------------------------------

--
-- Table structure for table `terms`
--

CREATE TABLE `terms` (
  `TermID` varchar(10) NOT NULL,
  `YearID` varchar(10) NOT NULL,
  `Name` varchar(20) NOT NULL,
  `isCurrent` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `terms`
--

INSERT INTO `terms` (`TermID`, `YearID`, `Name`, `isCurrent`) VALUES
('TRM-1', 'YR-01', '1ST SEMESTER', 0);

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `UserID` varchar(10) NOT NULL,
  `Name` varchar(50) NOT NULL,
  `University` varchar(100) NOT NULL,
  `Program` varchar(100) NOT NULL,
  `PreferredScale` int(1) NOT NULL,
  `TargetGeneralAverage` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`UserID`, `Name`, `University`, `Program`, `PreferredScale`, `TargetGeneralAverage`) VALUES
('U-001', 'ADMIN', 'Batangas State University - The National Engineering University, Alangilan Campus', 'BS Computer Science', 2, 1.5);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `academictasktypes`
--
ALTER TABLE `academictasktypes`
  ADD PRIMARY KEY (`TypeID`),
  ADD KEY `fk_acadtasktype_grade` (`GradeID`);

--
-- Indexes for table `academicyears`
--
ALTER TABLE `academicyears`
  ADD PRIMARY KEY (`YearID`),
  ADD KEY `fk_year_user` (`UserID`);

--
-- Indexes for table `courses`
--
ALTER TABLE `courses`
  ADD PRIMARY KEY (`CourseID`),
  ADD KEY `fk_course_term` (`TermID`);

--
-- Indexes for table `grades`
--
ALTER TABLE `grades`
  ADD PRIMARY KEY (`GradeID`),
  ADD KEY `fk_grade_course` (`CourseID`);

--
-- Indexes for table `scheduleentries`
--
ALTER TABLE `scheduleentries`
  ADD PRIMARY KEY (`EntryID`),
  ADD KEY `fk_entry_course` (`CourseID`);

--
-- Indexes for table `tasks`
--
ALTER TABLE `tasks`
  ADD PRIMARY KEY (`TaskID`),
  ADD KEY `fk_task_course` (`CourseID`),
  ADD KEY `fk_task_type` (`TypeID`),
  ADD KEY `fk_task_user` (`UserID`);

--
-- Indexes for table `terms`
--
ALTER TABLE `terms`
  ADD PRIMARY KEY (`TermID`),
  ADD KEY `fk_term_year` (`YearID`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`UserID`);

--
-- Constraints for dumped tables
--

--
-- Constraints for table `academictasktypes`
--
ALTER TABLE `academictasktypes`
  ADD CONSTRAINT `fk_acadtasktype_grade` FOREIGN KEY (`GradeID`) REFERENCES `grades` (`GradeID`);

--
-- Constraints for table `academicyears`
--
ALTER TABLE `academicyears`
  ADD CONSTRAINT `fk_year_user` FOREIGN KEY (`UserID`) REFERENCES `users` (`UserID`);

--
-- Constraints for table `courses`
--
ALTER TABLE `courses`
  ADD CONSTRAINT `fk_course_term` FOREIGN KEY (`TermID`) REFERENCES `terms` (`TermID`) ON UPDATE CASCADE;

--
-- Constraints for table `grades`
--
ALTER TABLE `grades`
  ADD CONSTRAINT `fk_grade_course` FOREIGN KEY (`CourseID`) REFERENCES `courses` (`CourseID`);

--
-- Constraints for table `scheduleentries`
--
ALTER TABLE `scheduleentries`
  ADD CONSTRAINT `fk_entry_course` FOREIGN KEY (`CourseID`) REFERENCES `scheduleentries` (`EntryID`);

--
-- Constraints for table `tasks`
--
ALTER TABLE `tasks`
  ADD CONSTRAINT `fk_task_course` FOREIGN KEY (`CourseID`) REFERENCES `courses` (`CourseID`),
  ADD CONSTRAINT `fk_task_type` FOREIGN KEY (`TypeID`) REFERENCES `academictasktypes` (`TypeID`),
  ADD CONSTRAINT `fk_task_user` FOREIGN KEY (`UserID`) REFERENCES `users` (`UserID`);

--
-- Constraints for table `terms`
--
ALTER TABLE `terms`
  ADD CONSTRAINT `fk_term_year` FOREIGN KEY (`YearID`) REFERENCES `academicyears` (`YearID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
