-- Creation of the 'Projects' table
CREATE TABLE IF NOT EXISTS Projects (
    Id INTEGER PRIMARY KEY,
    Name TEXT NOT NULL,
    Description TEXT,
    DateCreation TEXT NOT NULL
);

-- Creation of the 'Lists' table
CREATE TABLE IF NOT EXISTS Lists (
    Id INTEGER PRIMARY KEY,
    Name TEXT NOT NULL,
    IdProject INTEGER,
    FOREIGN KEY (IdProject) REFERENCES Projects (Id)
);

-- Creation of the 'Cards' (Tasks) table
CREATE TABLE IF NOT EXISTS Cards (
    Id INTEGER PRIMARY KEY,
    Title TEXT NOT NULL,
    Description TEXT,
    DateCreation TEXT NOT NULL,
    IdList INTEGER,
    FOREIGN KEY (IdList) REFERENCES Lists (Id)
);

-- Creation of the 'Comments' table
CREATE TABLE IF NOT EXISTS Comments (
    Id INTEGER PRIMARY KEY,
    Content TEXT NOT NULL,
    DateCreation TEXT NOT NULL,
    IdCard INTEGER,
    UserName TEXT NOT NULL,
    FOREIGN KEY (IdCard) REFERENCES Cards (Id)
);

-- Inserting the provided sample data
-- Projects
INSERT INTO Projects (Id, Name, Description, DateCreation) VALUES (1, 'Project 1', 'Description 1', CURRENT_DATE);
INSERT INTO Projects (Id, Name, Description, DateCreation) VALUES (2, 'Project 2', 'Description 2', CURRENT_DATE);
INSERT INTO Projects (Id, Name, Description, DateCreation) VALUES (3, 'Project 3', 'Description 3', CURRENT_DATE);
INSERT INTO Projects (Id, Name, Description, DateCreation) VALUES (4, 'Project 4', 'Description 4', CURRENT_DATE);

-- Lists
INSERT INTO Lists (Id, Name, IdProject) VALUES (1, 'List 1', 1);
INSERT INTO Lists (Id, Name, IdProject) VALUES (2, 'List 2', 1);
INSERT INTO Lists (Id, Name, IdProject) VALUES (3, 'List 3', 1);
INSERT INTO Lists (Id, Name, IdProject) VALUES (4, 'List 4', 1);
INSERT INTO Lists (Id, Name, IdProject) VALUES (5, 'List 1', 2);
INSERT INTO Lists (Id, Name, IdProject) VALUES (6, 'List 2', 2);
INSERT INTO Lists (Id, Name, IdProject) VALUES (7, 'List 1', 3);

-- Cards
INSERT INTO Cards (Id, Title, Description, DateCreation, IdList) VALUES (1, 'Card 1', 'Description Card 1', CURRENT_DATE, 1);
INSERT INTO Cards (Id, Title, Description, DateCreation, IdList) VALUES (2, 'Card 2', 'Description Card 2', CURRENT_DATE, 1);
INSERT INTO Cards (Id, Title, Description, DateCreation, IdList) VALUES (3, 'Card 3', 'Description Card 3', CURRENT_DATE, 1);
INSERT INTO Cards (Id, Title, Description, DateCreation, IdList) VALUES (4, 'Card 1', 'Description Card 1', CURRENT_DATE, 2);
INSERT INTO Cards (Id, Title, Description, DateCreation, IdList) VALUES (5, 'Card 2', 'Description Card 2', CURRENT_DATE, 2);
INSERT INTO Cards (Id, Title, Description, DateCreation, IdList) VALUES (6, 'Card 1', 'Description Card 1', CURRENT_DATE, 3);
INSERT INTO Cards (Id, Title, Description, DateCreation, IdList) VALUES (7, 'Card 1', 'Description Card 1', CURRENT_DATE, 4);
INSERT INTO Cards (Id, Title, Description, DateCreation, IdList) VALUES (8, 'Card 2', 'Description Card 2', CURRENT_DATE, 4);
INSERT INTO Cards (Id, Title, Description, DateCreation, IdList) VALUES (9, 'Card 1', 'Description Card 1', CURRENT_DATE, 5);
INSERT INTO Cards (Id, Title, Description, DateCreation, IdList) VALUES (10, 'Card 1', 'Description Card 1', CURRENT_DATE, 6);
INSERT INTO Cards (Id, Title, Description, DateCreation, IdList) VALUES (11, 'Card 1', 'Description Card 1', CURRENT_DATE, 7);

-- Comments
INSERT INTO Comments (Id, Content, DateCreation, IdCard, UserName) VALUES (1, 'Content Comment 1', CURRENT_DATE, 1, 'User');
INSERT INTO Comments (Id, Content, DateCreation, IdCard, UserName) VALUES (2, 'Content Comment 1', CURRENT_DATE, 3, 'User');
INSERT INTO Comments (Id, Content, DateCreation, IdCard, UserName) VALUES (3, 'Content Comment 2', CURRENT_DATE, 3, 'User');
INSERT INTO Comments (Id, Content, DateCreation, IdCard, UserName) VALUES (4, 'Content Comment 3', CURRENT_DATE, 3, 'User');
INSERT INTO Comments (Id, Content, DateCreation, IdCard, UserName) VALUES (5, 'Content Comment 1', CURRENT_DATE, 4, 'User');
INSERT INTO Comments (Id, Content, DateCreation, IdCard, UserName) VALUES (6, 'Content Comment 2', CURRENT_DATE, 4, 'User');
INSERT INTO Comments (Id, Content, DateCreation, IdCard, UserName) VALUES (7, 'Content Comment 1', CURRENT_DATE, 5, 'User');
