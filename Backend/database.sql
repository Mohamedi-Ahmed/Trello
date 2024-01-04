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
    FOREIGN KEY (IdCard) REFERENCES Cards (Id)
);

-- Inserting some sample data
-- Projects
INSERT INTO Projects (Name, Description, DateCreation) VALUES ('Project Alpha', 'Description of Project Alpha', '2024-01-04');
INSERT INTO Projects (Name, Description, DateCreation) VALUES ('Project Beta', 'Description of Project Beta', '2024-01-05');

-- Lists
INSERT INTO Lists (Name, IdProject) VALUES ('To Do', (SELECT Id FROM Projects WHERE Name = 'Project Alpha'));
INSERT INTO Lists (Name, IdProject) VALUES ('Done', (SELECT Id FROM Projects WHERE Name = 'Project Alpha'));

-- Cards
INSERT INTO Cards (Title, Description, DateCreation, IdList) VALUES ('Task 1', 'Description of Task 1', '2024-01-04', (SELECT Id FROM Lists WHERE Name = 'To Do'));
INSERT INTO Cards (Title, Description, DateCreation, IdList) VALUES ('Task 2', 'Description of Task 2', '2024-01-04', (SELECT Id FROM Lists WHERE Name = 'Done'));

-- Comments
INSERT INTO Comments (Content, DateCreation, IdCard, UserName) VALUES ('This is a comment on Task 1', '2024-01-04', (SELECT Id FROM Cards WHERE Title = 'Task 1'), 'User1');
INSERT INTO Comments (Content, DateCreation, IdCard, UserName) VALUES ('This is another comment on Task 1', '2024-01-04', (SELECT Id FROM Cards WHERE Title = 'Task 1'), 'User2');
