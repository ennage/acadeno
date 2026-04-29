-- 1. USERS
CREATE TABLE "users" (
  "UserID" varchar(5) PRIMARY KEY NOT NULL,
  "Name" varchar(50) NOT NULL,
  "University" varchar(100) NOT NULL,
  "Program" varchar(100) NOT NULL,
  "PreferredScale" integer NOT NULL
);

-- 2. ACADEMIC YEARS
CREATE TABLE "academicyears" (
  "YearID" varchar(5) PRIMARY KEY NOT NULL,
  "UserID" varchar(5) NOT NULL,
  "YearSpan" varchar(20) NOT NULL,
  "IsCurrent" integer NOT NULL, -- SQLite uses integer for booleans (0 or 1)
  "AYTargetGenAve" double DEFAULT NULL,
  "AYCalculatedGenAve" double DEFAULT NULL,
  FOREIGN KEY ("UserID") REFERENCES "users" ("UserID")
);

-- 3. TERMS
CREATE TABLE "terms" (
  "TermID" varchar(5) PRIMARY KEY NOT NULL,
  "YearID" varchar(5) NOT NULL,
  "Name" varchar(20) NOT NULL,
  "IsCurrent" integer NOT NULL,
  "TermTargetGenAve" double DEFAULT NULL,
  "TermCalculatedGenAve" double DEFAULT NULL,
  FOREIGN KEY ("YearID") REFERENCES "academicyears" ("YearID")
);

-- 4. COURSES
CREATE TABLE "courses" (
  "CourseID" varchar(5) PRIMARY KEY NOT NULL,
  "TermID" varchar(5) NOT NULL,
  "CourseCode" varchar(10) NOT NULL,
  "Name" varchar(100) NOT NULL,
  "Units" integer NOT NULL,
  FOREIGN KEY ("TermID") REFERENCES "terms" ("TermID") ON UPDATE CASCADE
);

-- 5. GRADES
CREATE TABLE "grades" (
  "GradeID" varchar(5) PRIMARY KEY NOT NULL,
  "CourseID" varchar(5) NOT NULL,
  "TargetCourseGrade" double DEFAULT NULL,
  "CourseGrade" double DEFAULT NULL,
  FOREIGN KEY ("CourseID") REFERENCES "courses" ("CourseID")
);

-- 6. ACADEMIC TASK TYPES
CREATE TABLE "academictasktypes" (
  "TypeID" varchar(5) PRIMARY KEY NOT NULL,
  "GradeID" varchar(5) DEFAULT NULL,
  "Name" varchar(100) NOT NULL,
  "Description" text DEFAULT NULL,
  "Weight" double NOT NULL,
  FOREIGN KEY ("GradeID") REFERENCES "grades" ("GradeID")
);

-- 7. SCHEDULE ENTRIES
CREATE TABLE "scheduleentries" (
  "EntryID" varchar(5) PRIMARY KEY NOT NULL,
  "CourseID" varchar(5) NOT NULL,
  "Label" varchar(50) NOT NULL,
  "Description" text DEFAULT NULL,
  "Day" integer NOT NULL,
  "StartTime" datetime NOT NULL,
  "EndTime" datetime NOT NULL,
  "Session" varchar(20) NOT NULL,
  "Room" varchar(50) NOT NULL,
  "Building" varchar(50) NOT NULL,
  FOREIGN KEY ("CourseID") REFERENCES "courses" ("CourseID")
);

-- 8. TASKS
CREATE TABLE "tasks" (
  "TaskID" varchar(5) PRIMARY KEY NOT NULL,
  "UserID" varchar(5) NOT NULL,
  "TypeID" varchar(5) NOT NULL,
  "CourseID" varchar(5) DEFAULT NULL,
  "Name" varchar(100) NOT NULL,
  "Description" text DEFAULT NULL,
  "Difficulty" integer NOT NULL,
  "StartDate" datetime NOT NULL,
  "DueDate" datetime NOT NULL,
  "Status" integer NOT NULL,
  "RiskLevel" integer NOT NULL,
  "TargetScore" double DEFAULT NULL,
  "Score" double DEFAULT NULL,
  "MaxScore" double DEFAULT NULL,
  FOREIGN KEY ("UserID") REFERENCES "users" ("UserID"),
  FOREIGN KEY ("TypeID") REFERENCES "academictasktypes" ("TypeID"),
  FOREIGN KEY ("CourseID") REFERENCES "courses" ("CourseID")
);

-- SEED DATA
INSERT INTO "academictasktypes" ("TypeID", "GradeID", "Name", "Description", "Weight") 
VALUES ('TYP00', NULL, 'Unallocated', 'Default category for items not yet assigned a syllabus type.', 0.0);

