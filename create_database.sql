CREATE DATABASE OnlineQuizSystem;
GO

USE OnlineQuizSystem;
GO

CREATE TABLE Role (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(50) UNIQUE NOT NULL
);

CREATE TABLE [User] (
    Id INT PRIMARY KEY IDENTITY,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) UNIQUE NOT NULL,
    PwdHash NVARCHAR(256) NOT NULL,
    PwdSalt NVARCHAR(256) NOT NULL,
    RoleId INT NOT NULL,
    FOREIGN KEY (RoleId) REFERENCES Role(Id)
);

CREATE TABLE Course (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX),
    ProfessorId INT NOT NULL,
    FOREIGN KEY (ProfessorId) REFERENCES [User](Id)
);

CREATE TABLE Quiz (
    Id INT PRIMARY KEY IDENTITY,
    ProfessorId INT NOT NULL,
    Title NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX),
    DateCreated DATETIME DEFAULT GETDATE(),
    TimeLimit INT, 
    IsPublished BIT DEFAULT 0,
    CourseId INT,
    FOREIGN KEY (ProfessorId) REFERENCES [User](Id),
    FOREIGN KEY (CourseId) REFERENCES Course(Id)
);

CREATE TABLE Question (
    Id INT PRIMARY KEY IDENTITY,
    QuizId INT NOT NULL,
    QuestionText NVARCHAR(MAX) NOT NULL,
    Points INT NOT NULL,
    FOREIGN KEY (QuizId) REFERENCES Quiz(Id)
);

CREATE TABLE Answer (
    Id INT PRIMARY KEY IDENTITY,
    QuestionId INT NOT NULL,
    AnswerText NVARCHAR(MAX) NOT NULL,
    IsCorrect BIT DEFAULT 0,
    FOREIGN KEY (QuestionId) REFERENCES Question(Id)
);



CREATE TABLE QuizAttempt (
    Id INT PRIMARY KEY IDENTITY,
    StudentId INT NOT NULL,
    QuizId INT NOT NULL,
    AttemptDate DATETIME DEFAULT GETDATE(),
    Score INT, 
    FOREIGN KEY (StudentId) REFERENCES [User](Id),
    FOREIGN KEY (QuizId) REFERENCES Quiz(Id)
);

CREATE TABLE AnswerSubmission (
    Id INT PRIMARY KEY IDENTITY,
    QuizAttemptId INT NOT NULL,
    QuestionId INT NOT NULL,
    SelectedAnswerId INT NULL,
    IsCorrect BIT DEFAULT 0,
    FOREIGN KEY (QuizAttemptId) REFERENCES QuizAttempt(Id),
    FOREIGN KEY (QuestionId) REFERENCES Question(Id),
    FOREIGN KEY (SelectedAnswerId) REFERENCES Answer(Id)
);
