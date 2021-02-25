USE master;
GO

IF EXISTS(select * from sys.databases where name='AssignmentProject2')
	DROP DATABASE AssignmentProject2

CREATE DATABASE AssignmentProject2;
GO

USE AssignmentProject2;
GO

CREATE TABLE Employees (
	EmployeeID INT IDENTITY
		CONSTRAINT EmployeeID_PK PRIMARY KEY(EmployeeID),
	username VARCHAR(45),
	password VARCHAR(45),
	firstname VARCHAR(45),
	lastname VARCHAR(45),
	payment INT,
	--Assignment INT 
		--CONSTRAINT Assingment_FK FOREIGN KEY(Assignment) REFERENCES Assignments(AssignmentID)
)

CREATE TABLE Tasks (
	TaskID INT IDENTITY 
		CONSTRAINT TaskID_PK PRIMARY KEY(TaskID),
	TaskName VARCHAR(30) NOT NULL
		CONSTRAINT TaskNev_U UNIQUE(TaskName),
	TaskDescription VARCHAR(100),
	ExpirationDate DATE
);

CREATE TABLE Assignments (
	AssignmentID INT IDENTITY
		CONSTRAINT AssignmentID_PK PRIMARY KEY(AssignmentID),
	AssignmentName VARCHAR(30),
	StartDate DATE,
	FinishDate DATE,
	TaskId INT
		CONSTRAINT Task_FK FOREIGN KEY(Task) REFERENCES Tasks(TaskID)
);

CREATE TABLE Works (
	WorkID INT IDENTITY,
	EmployeeID INT NOT NULL CONSTRAINT FK_empoyeeID FOREIGN KEY (EmployeeID) REFERENCES Employees(EmployeeID),
	AssignmentID INT NOT NULL CONSTRAINT FK_assignmentID FOREIGN KEY (AssignmentID) REFERENCES Assignments(AssignmentID),
	StartDate DATETIME NOT NULL DEFAULT GETDATE(),
	CONSTRAINT Work_PK PRIMARY KEY (WorkID, EmployeeID, AssignmentID)
)