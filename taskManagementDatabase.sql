CREATE DATABASE TaskManagementDb;


USE TaskManagementDb;


CREATE TABLE Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    UserName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE Tasks (
    TaskId INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(200) NOT NULL,
    Description NVARCHAR(500) NULL,
    Status NVARCHAR(20) NOT NULL DEFAULT 'Todo',
	CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    UserId INT NOT NULL,
    CONSTRAINT FK_Tasks_Users FOREIGN KEY (UserId)
        REFERENCES Users(UserId),
    CONSTRAINT CK_Tasks_Status CHECK (Status IN ('Todo', 'In Progress', 'Done'))
);

INSERT INTO Users (UserName, Email)
VALUES
('Abdul Baasith', 'abdul@example.com'),
('Student One', 'student1@example.com'),
('Student Two', 'student2@example.com');

INSERT INTO Tasks (Title, Description, Status, UserId)
VALUES
('Create database', 'Create Users and Tasks tables', 'Done', 1),
('Build user API', 'Create user endpoints', 'In Progress', 1),
('Create frontend', 'Build HTML pages', 'Todo', 2),
('Test API', 'Use Postman to test endpoints', 'Todo', 3);

SELECT * FROM Users;
SELECT * FROM Tasks;