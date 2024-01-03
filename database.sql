CREATE TABLE IF NOT EXISTS Projects (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    Description TEXT ,
    Creation_Date DATE 
);

CREATE TABLE IF NOT EXISTS Lists(
    Id_List INTEGER PRIMARY KEY AUTOINCREMENT,
    Name_List TEXT,
    Id_Project INTEGER,
    FOREIGN KEY (Id_Project) REFERENCES Projects(Id)
);


INSERT INTO Projects (Id, Name, Description, Creation_Date)
VALUES  (1, 'Project 1','Description 1', CURRENT_DATE),
        (2, 'Project 2','Description 2', CURRENT_DATE),
        (3, 'Project 3','Description 3', CURRENT_DATE),
        (4, 'Project 4','Description 4', CURRENT_DATE);




INSERT INTO Lists (Id_Project,Name_List)
VALUES  (1,'liste 1'),
        (1,'liste 2'),
        (1,'liste 3'),
        (1,'liste 4'),
        (2,'liste 1'),
        (2,'liste 2'),
        (3,'liste 1');

/*
CREATE TABLE Projects_Lists(
    Id_Project INT NOT NULL,
    Id_List INT NOT NULL,
    FOREIGN KEY (Id_Project) REFERENCES Projects(Id)
    FOREIGN KEY (Id_List)    REFERENCES Lists(Id_List)
    PRIMARY KEY (Id_Project, Id_List)
);

INSERT INTO Projects_Lists (Id_Project, Id_List)
VALUES  (1, 1),
        (1, 2),
        (1, 3),
        (2, 4),
        (2, 5),
        (3, 6),
        (4, 7);

*/
CREATE TABLE IF NOT EXISTS Cards (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Title TEXT NOT NULL,
    Description TEXT,
    Creation_Date DATE,
    Id_List INT NOT NULL,
    FOREIGN KEY (Id_List) REFERENCES Lists(Id_List)
);

INSERT INTO Cards(Id_List, Title, Description, Creation_Date)
Values  (1, 'Card 1', 'Description Card 1', CURRENT_DATE),
        (1, 'Card 2', 'Description Card 2', CURRENT_DATE),
        (1, 'Card 3', 'Description Card 3', CURRENT_DATE),
        (2, 'Card 1', 'Description Card 1', CURRENT_DATE),
        (2, 'Card 2', 'Description Card 2', CURRENT_DATE),
        (3, 'Card 1', 'Description Card 1', CURRENT_DATE),
        (4, 'Card 1', 'Description Card 1', CURRENT_DATE),
        (4, 'Card 2', 'Description Card 2', CURRENT_DATE),
        (5, 'Card 1', 'Description Card 1', CURRENT_DATE),
        (6, 'Card 1', 'Description Card 1', CURRENT_DATE),
        (7, 'Card 1', 'Description Card 1', CURRENT_DATE);

/*
CREATE TABLE Lists_Cards(
    Id_Card INT NOT NULL,
    Id_List INT NOT NULL,
    FOREIGN KEY (Id_Card) REFERENCES Cards(Id)
    FOREIGN KEY (Id_List) REFERENCES Lists(Id_List)
    PRIMARY KEY (Id_Card, Id_List)
);

INSERT INTO Lists_Cards (Id_List, Id_Card)
VALUES  (1, 1),
        (1, 2),
        (1, 3),
        (2, 4),
        (2, 5),
        (3, 6),
        (4, 7),
        (4, 8),
        (5, 9),
        (6, 10),
        (7, 11);
*/
CREATE TABLE IF NOT EXISTS Comments (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Content TEXT,
    Creation_Date DATE,
    Id_Card INT NOT NULL,
    /* User */
    FOREIGN KEY (Id_Card) REFERENCES Cards(Id)
);

INSERT INTO Comments (Content, Creation_Date, Id_Card)
VALUES  ('Content Comment 1', CURRENT_DATE, 1),
        ('Content Comment 1', CURRENT_DATE, 3),
        ('Content Comment 2', CURRENT_DATE, 3),
        ('Content Comment 3', CURRENT_DATE, 3),
        ('Content Comment 1', CURRENT_DATE, 4),
        ('Content Comment 2', CURRENT_DATE, 4),
        ('Content Comment 1', CURRENT_DATE, 5);

/*CREATE TABLE Cards_Comments(
    Id_Card INT NOT NULL,
    Id_Comment INT NOT NULL,
    FOREIGN KEY (Id_Card) REFERENCES Cards(Id)
    FOREIGN KEY (Id_Comment) REFERENCES Comments(Id)
    PRIMARY KEY (Id_Card, Id_Comment)
);

INSERT INTO Cards_Comments (Id_Card, Id_Comment)
VALUES  (1, 1),
        (3, 2),
        (3, 3),
        (3, 4),
        (4, 5),
        (4, 6),
        (5, 7);

*/