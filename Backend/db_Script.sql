-- Creation of the 'Projects' table
CREATE TABLE IF NOT EXISTS Projects (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    Description TEXT,
    DateCreation TEXT NOT NULL
    );

-- Creation of the 'Lists' table
CREATE TABLE IF NOT EXISTS Lists (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    IdProject INTEGER,
    FOREIGN KEY (IdProject) REFERENCES Projects (Id)
);


-- Creation of the 'Cards' (Tasks) table
CREATE TABLE IF NOT EXISTS Cards (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Title TEXT NOT NULL,
    Description TEXT,
    DateCreation TEXT NOT NULL,
    IdList INTEGER,
    FOREIGN KEY (IdList) REFERENCES Lists (Id)
);

-- Creation of the 'Comments' table
CREATE TABLE IF NOT EXISTS Comments (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Content TEXT NOT NULL,
    DateCreation TEXT NOT NULL,
    IdCard INTEGER,
    UserName TEXT NOT NULL,
    FOREIGN KEY (IdCard) REFERENCES Cards(Id)
);

CREATE TABLE IF NOT EXISTS Users(
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    UserName TEXT NOT NULL,
    Password TEXT
);

CREATE TABLE IF NOT EXISTS UserProjects (
    UserId INTEGER,
    ProjectId INTEGER,
    PRIMARY KEY (UserId, ProjectId),
    FOREIGN KEY (UserId) REFERENCES Users (Id),
    FOREIGN KEY (ProjectId) REFERENCES Projects (Id)
);

-- Ajout des relations entre les utilisateurs et les projets
INSERT INTO UserProjects (UserId, ProjectId)
VALUES (1,1),
       (1,3),
       (2,1),
       (2,2),
       (3,1),
       (3,3);


-- Inserting the provided sample data
-- Projects
INSERT INTO Projects (Name, Description, DateCreation) VALUES ('Project 1', 'Description 1', datetime('now'));
INSERT INTO Projects (Name, Description, DateCreation) VALUES ('Project 2', 'Description 2', datetime('now'));
INSERT INTO Projects (Name, Description, DateCreation) VALUES ('Project 3', 'Description 3', datetime('now'));
INSERT INTO Projects (Name, Description, DateCreation) VALUES ('Project 4', 'Description 4', datetime('now'));

-- Lists
INSERT INTO Lists (Name, IdProject) VALUES ('List 1', 1);
INSERT INTO Lists (Name, IdProject) VALUES ('List 2', 1);
INSERT INTO Lists (Name, IdProject) VALUES ('List 3', 1);
INSERT INTO Lists (Name, IdProject) VALUES ('List 4', 1);
INSERT INTO Lists (Name, IdProject) VALUES ('List 1', 2);
INSERT INTO Lists (Name, IdProject) VALUES ('List 2', 2);
INSERT INTO Lists (Name, IdProject) VALUES ('List 1', 3);

-- Cards
INSERT INTO Cards (Title, Description, DateCreation, IdList) VALUES ('Card 1', 'Description Card 1', datetime('now'), 1);
INSERT INTO Cards (Title, Description, DateCreation, IdList) VALUES ('Card 2', 'Description Card 2', datetime('now'), 1);
INSERT INTO Cards (Title, Description, DateCreation, IdList) VALUES ('Card 3', 'Description Card 3', datetime('now'), 1);
INSERT INTO Cards (Title, Description, DateCreation, IdList) VALUES ('Card 1', 'Description Card 1', datetime('now'), 2);
INSERT INTO Cards (Title, Description, DateCreation, IdList) VALUES ('Card 2', 'Description Card 2', datetime('now'), 2);
INSERT INTO Cards (Title, Description, DateCreation, IdList) VALUES ('Card 1', 'Description Card 1', datetime('now'), 3);
INSERT INTO Cards (Title, Description, DateCreation, IdList) VALUES ('Card 1', 'Description Card 1', datetime('now'), 4);
INSERT INTO Cards (Title, Description, DateCreation, IdList) VALUES ('Card 2', 'Description Card 2', datetime('now'), 4);
INSERT INTO Cards (Title, Description, DateCreation, IdList) VALUES ('Card 1', 'Description Card 1', datetime('now'), 5);
INSERT INTO Cards (Title, Description, DateCreation, IdList) VALUES ('Card 1', 'Description Card 1', datetime('now'), 6);
INSERT INTO Cards (Title, Description, DateCreation, IdList) VALUES ('Card 1', 'Description Card 1', datetime('now'), 7);

-- Comments
INSERT INTO Comments (Content, DateCreation, IdCard, UserName) VALUES ('Content Comment 1', datetime('now'), 1, 'User');
INSERT INTO Comments (Content, DateCreation, IdCard, UserName) VALUES ('Content Comment 1', datetime('now'), 3, 'User');
INSERT INTO Comments (Content, DateCreation, IdCard, UserName) VALUES ('Content Comment 2', datetime('now'), 3, 'User');
INSERT INTO Comments (Content, DateCreation, IdCard, UserName) VALUES ('Content Comment 3', datetime('now'), 3, 'User');
INSERT INTO Comments (Content, DateCreation, IdCard, UserName) VALUES ('Content Comment 1', datetime('now'), 4, 'User');
INSERT INTO Comments (Content, DateCreation, IdCard, UserName) VALUES ('Content Comment 2', datetime('now'), 4, 'User');
INSERT INTO Comments (Content, DateCreation, IdCard, UserName) VALUES ('Content Comment 1', datetime('now'), 5, 'User');

-- Users
INSERT INTO Users (UserName, Password) VALUES ('Ahmed' , ''     );
INSERT INTO Users (UserName, Password) VALUES ('Idir'  , ''     );
INSERT INTO Users (UserName, Password) VALUES ('Nadine', ''     );
INSERT INTO Users (UserName, Password) VALUES ('Albert', 'Camus');
INSERT INTO Users (UserName, Password) VALUES ('Victor', 'Hugo' );
