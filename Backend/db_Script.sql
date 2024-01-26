BEGIN;

-- Suppression des tables si elles existent
DROP TABLE IF EXISTS Comments;
DROP TABLE IF EXISTS Cards;
DROP TABLE IF EXISTS Lists;
DROP TABLE IF EXISTS UserProjects;
DROP TABLE IF EXISTS Projects;
DROP TABLE IF EXISTS Users;

-- Création de la table 'Projects'
CREATE TABLE IF NOT EXISTS Projects (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name NVARCHAR(255) NOT NULL,
    Description TEXT,
    DateCreation DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- Création de la table 'Users'
CREATE TABLE IF NOT EXISTS Users (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    FirstName NVARCHAR(255),
    LastName NVARCHAR(255),
    UserName NVARCHAR(255) NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    Password NVARCHAR(255) NOT NULL
);

-- Création de la table 'UserProjects' (relation many-to-many avec roles)
CREATE TABLE IF NOT EXISTS UserProjects (
    UserId INTEGER,
    ProjectId INTEGER,
    UserRole NVARCHAR(255) NOT NULL DEFAULT 'Membre',
    PRIMARY KEY (UserId, ProjectId),
    FOREIGN KEY (UserId) REFERENCES Users(Id),
    FOREIGN KEY (ProjectId) REFERENCES Projects(Id)
);

-- Création de la table 'Lists' avec suppression en cascade
CREATE TABLE IF NOT EXISTS Lists (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name NVARCHAR(255) NOT NULL,
    IdProject INTEGER,
    FOREIGN KEY (IdProject) REFERENCES Projects(Id) ON DELETE CASCADE
);

-- Création de la table 'Cards' (Tâches) avec suppression en cascade
CREATE TABLE IF NOT EXISTS Cards (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Title NVARCHAR(255) NOT NULL,
    Description TEXT,
    DateCreation DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    IdList INTEGER,
    CreatorId INTEGER,
    FOREIGN KEY (IdList) REFERENCES Lists(Id) ON DELETE CASCADE,
    FOREIGN KEY (CreatorId) REFERENCES Users(Id)
);

-- Création de la table 'Comments' avec suppression en cascade et référence à l'utilisateur
CREATE TABLE IF NOT EXISTS Comments (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Content TEXT NOT NULL,
    DateCreation DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    IdCard INTEGER,
    UserId INTEGER,
    FOREIGN KEY (IdCard) REFERENCES Cards(Id) ON DELETE CASCADE,
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);

-- Insertion de données d'exemple dans la table 'Projects'
INSERT INTO Projects (Name, Description) VALUES 
('Project 1', 'Description 1'), 
('Project 2', 'Description 2'), 
('Project 3', 'Description 3'), 
('Project 4', 'Description 4'),
('Project 5', 'Description 5'), 
('Project 6', 'Description 6'), 
('Project 7', 'Description 7'), 
('Project 8', 'Description 8'),
('Project 9', 'Description 9'), 
('Project 10', 'Description 10'), 
('Project 11', 'Description 11'), 
('Project 12', 'Description 12'),
('Project 13', 'Description 13'), 
('Project 15', 'Description 15'),
('Project 16', 'Description 16');

-- Insertion de données d'exemple dans la table 'Users'
INSERT INTO Users (FirstName, LastName, UserName, Email, Password) VALUES 
('Ahmed' , 'Mohamedi' , 'minitork', 'mohamedi_ahmed@yahoo.fr', 'password123'),
('Idir'  , 'Ait ouali', 'idir'    , 'idir@example.com'       , 'password123'),
('Nadine', 'Gasmi'    , 'nadine'  , 'nadine@example.com'     , 'password123'),
('admin' , ''         , 'admin'   ,'admin@example.com'       , 'admin'),
('Albert', 'Camus'    , 'ACamus'  , 'acamus@example.com'     , 'password123'),
('Victor', 'Hugo'     , 'Vhugo'   , 'vhugo@example.com'      , 'password123');

-- Insertion de données d'exemple dans la table 'UserProjects' for project creators
INSERT INTO UserProjects (UserId, ProjectId, UserRole) VALUES 
(1, 1, 'Créateur'),  
(2, 2, 'Créateur'),  
(3, 3, 'Créateur'), 
(4, 4, 'Créateur'), 
(5, 5, 'Créateur'),  
(6, 6, 'Créateur'), 

-- Insertion de données d'exemple dans la table 'UserProjects' for project members
INSERT INTO UserProjects (UserId, ProjectId, UserRole) VALUES 
(1, 1, 'Membre'), (2, 2, 'Membre'), 
(3, 3, 'Membre'), (4, 4, 'Membre'), 
(5, 5, 'Membre'), (6, 6, 'Membre'), 
(1, 7, 'Membre'), (2, 8, 'Membre'), 
(3, 9, 'Membre'), (4, 10, 'Membre'), 
(5, 11, 'Membre'), (6, 12, 'Membre'),
(1, 13, 'Membre'), (2, 14, 'Membre'), 
(3, 15, 'Membre'), (4, 15, 'Membre'),
(1, 2  , 'Membre'), (1, 3, 'Membre'),
(1, 4  , 'Membre'), (1, 5, 'Membre'),
(1, 6  , 'Membre'), (1, 7, 'Membre'),
(1, 8  , 'Membre'), (1, 9, 'Membre'),
(1, 10 , 'Membre');

-- Insertion de données d'exemple dans la table 'Lists'
INSERT INTO Lists (Name, IdProject) VALUES 
('List 1', 1), 
('List 2', 1), 
('List 3', 1), 
('List 4', 1), 
('List 1', 2), 
('List 2', 2), 
('List 1', 3);

-- Insertion de données d'exemple dans la table 'Cards'
INSERT INTO Cards (Title, Description, IdList, CreatorId) VALUES 
('Card 1', 'Description Card 1', 1, 1), 
('Card 3', 'Description Card 3', 1, 3),
('Card 1', 'Description Card 1', 2, 1), 
('Card 2', 'Description Card 2', 2, 2), 
('Card 3', 'Description Card 3', 2, 3),
('Card 1', 'Description Card 1', 3, 1), 
('Card 2', 'Description Card 2', 3, 2), 
('Card 3', 'Description Card 3', 3, 3),
('Card 4', 'Description Card 4', 3, 4),
('Card 5', 'Description Card 5', 1, 5), 
('Card 6', 'Description Card 6', 2, 6),
('Card 7', 'Description Card 7', 3, 1), 
('Card 8', 'Description Card 8', 1, 1), 
('Card 9', 'Description Card 9', 2, 2), 
('Card 10', 'Description Card 10', 2, 3), 
('Card 11', 'Description Card 11', 3, 1),
('Card 12', 'Description Card 12', 3, 2); 

-- Insertion de données d'exemple dans la table 'Comments'
INSERT INTO Comments (Content, IdCard, UserId) VALUES 
('Content Comment 1', 1, 1), 
('Content Comment 2', 2, 2), 
('Content Comment 3', 3, 3), 
('Content Comment 4', 4, 1), 
('Content Comment 5', 5, 2), 
('Content Comment 6', 6, 3), 
('Content Comment 7', 7, 4), 
('Content Comment 8', 8, 5), 
('Content Comment 9', 9, 6), 
('Content Comment 10', 10, 1);

COMMIT;
