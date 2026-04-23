CREATE TABLE IF NOT EXISTS "users" (
  "UserID" TEXT NOT NULL PRIMARY KEY,
  "Name" TEXT NOT NULL,
  "University" TEXT NOT NULL,
  "Program" TEXT NOT NULL,
  "PreferredScale" INTEGER NOT NULL,
  "TargetGeneralAverage" REAL NOT NULL
);

CREATE TABLE IF NOT EXISTS "academicyears" (
  "YearID" TEXT NOT NULL PRIMARY KEY,
  "UserID" TEXT NOT NULL,
  "YearSpan" TEXT NOT NULL,
  "isCurrent" INTEGER NOT NULL,
  FOREIGN KEY ("UserID") REFERENCES "users" ("UserID")
);

CREATE TABLE IF NOT EXISTS "terms" (
  "TermID" TEXT NOT NULL PRIMARY KEY,
  "YearID" TEXT NOT NULL,
  "Name" TEXT NOT NULL,
  "isCurrent" INTEGER NOT NULL,
  FOREIGN KEY ("YearID") REFERENCES "academicyears" ("YearID")
);

CREATE TABLE IF NOT EXISTS "courses" (
  "CourseID" TEXT NOT NULL PRIMARY KEY,
  "TermID" TEXT NOT NULL,
  "CourseCode" TEXT NOT NULL,
  "Name" TEXT NOT NULL,
  "Units" INTEGER NOT NULL,
  "TargetGrade" REAL NOT NULL,
  FOREIGN KEY ("TermID") REFERENCES "terms" ("TermID") ON UPDATE CASCADE
);

CREATE TABLE IF NOT EXISTS "grades" (
  "GradeID" TEXT NOT NULL PRIMARY KEY,
  "CourseID" TEXT NOT NULL,
  "RawGrade" REAL NOT NULL,
  FOREIGN KEY ("CourseID") REFERENCES "courses" ("CourseID")
);

CREATE TABLE IF NOT EXISTS "academictasktypes" (
  "TypeID" TEXT NOT NULL PRIMARY KEY,
  "GradeID" TEXT NOT NULL,
  "Name" TEXT NOT NULL,
  "Weight" REAL NOT NULL,
  "TargetScore" REAL NOT NULL,
  FOREIGN KEY ("GradeID") REFERENCES "grades" ("GradeID")
);

CREATE TABLE IF NOT EXISTS "tasks" (
  "TaskID" TEXT NOT NULL PRIMARY KEY,
  "UserID" TEXT NOT NULL,
  "TypeID" TEXT NOT NULL,
  "CourseID" TEXT,
  "Name" TEXT NOT NULL,
  "Description" TEXT,
  "Difficulty" INTEGER NOT NULL,
  "StartDate" TEXT NOT NULL,
  "DueDate" TEXT NOT NULL,
  "Status" INTEGER NOT NULL,
  "RiskLevel" INTEGER NOT NULL,
  "Score" REAL,
  "MaxScore" REAL,
  FOREIGN KEY ("UserID") REFERENCES "users" ("UserID"),
  FOREIGN KEY ("TypeID") REFERENCES "academictasktypes" ("TypeID"),
  FOREIGN KEY ("CourseID") REFERENCES "courses" ("CourseID")
);

CREATE TABLE IF NOT EXISTS "scheduleentries" (
  "EntryID" TEXT NOT NULL PRIMARY KEY,
  "CourseID" TEXT NOT NULL,
  "Label" TEXT NOT NULL,
  "Description" TEXT,
  "Day" INTEGER NOT NULL,
  "StartTime" TEXT NOT NULL,
  "EndTime" TEXT NOT NULL,
  "Session" TEXT NOT NULL,
  "Room" TEXT NOT NULL,
  "Building" TEXT NOT NULL,
  FOREIGN KEY ("CourseID") REFERENCES "courses" ("CourseID")
);