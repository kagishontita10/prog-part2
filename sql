CREATE DATABASE races_prog;

USE races_prog;


CREATE TABLE Users(
    userID INT IDENTITY(1,1) PRIMARY KEY, 
    fullName VARCHAR(100) NOT NULL,  
    password VARCHAR(255) NOT NULL,
    role VARCHAR(50) CHECK(role IN('organiser','participant')) NOT NULL, 
    email VARCHAR(100) UNIQUE NOT NULL
);

CREATE TABLE Events(
    eventID INT IDENTITY(1,1) PRIMARY KEY,  
    userID INT NOT NULL FOREIGN KEY REFERENCES Users(userID), 
    EventName VARCHAR(100) NOT NULL,  
    description VARCHAR(200),
    EventDate DATE NOT NULL, 
    location VARCHAR(100) NOT NULL,
    eventType VARCHAR(50) CHECK(eventType IN('run','walk','cycle')) NOT NULL
); 

CREATE TABLE weather(
    weatherID INT IDENTITY(1,1) PRIMARY KEY, 
    eventID INT FOREIGN KEY REFERENCES Events(eventID),  
    temperature DECIMAL(5,2) NULL, 
    rainProbability DECIMAL(5,2)
); 

CREATE TABLE categories(
    categoryID INT IDENTITY(1,1) PRIMARY KEY, 
    eventID INT NOT NULL FOREIGN KEY REFERENCES Events(eventID),  
    categoryType VARCHAR(50), 
    age INT NOT NULL
); 

CREATE TABLE eventRoute(
    routeID INT IDENTITY(1,1) PRIMARY KEY,  
    eventID INT NOT NULL FOREIGN KEY REFERENCES Events(eventID), 
    routeName VARCHAR(100) NOT NULL, 
    distance DECIMAL(6,2) NOT NULL
); 

CREATE TABLE Enrolments(
    enrolmentID INT IDENTITY(1,1) PRIMARY KEY,  
    userID INT NOT NULL FOREIGN KEY REFERENCES Users(userID), 
    eventID INT NOT NULL FOREIGN KEY REFERENCES Events(eventID), 
    categoryID INT NOT NULL FOREIGN KEY REFERENCES categories(categoryID), 
    enrolmemtDate DATE NOT NULL, 
    status VARCHAR(20)
); 

CREATE TABLE Results(
    resultID INT IDENTITY(1,1) PRIMARY KEY,  
    enrolmentID INT NOT NULL FOREIGN KEY REFERENCES Enrolments(enrolmentID), 
    finishTime TIME NOT NULL,
    position INT, 
    recordedeAT DATETIME DEFAULT GETDATE()
); 

-- Now inserts work without needing IDs
INSERT INTO Users(fullName, password, role, email) 
VALUES('Thabang Nkosi','Thabang123', 'organiser', 'Thabang@gmail.com'), 
      ('Comi Nzola','Nzola@25','organiser','Nzola@gmail.com'), 
      ('sama nkosi','sama456','participant','sama@gmail.com'), 
      ('kamo Liza','kamo134','participant','kamoL@gmail.com'); 

INSERT INTO Events(userID, EventName, description, EventDate, location, eventType) 
VALUES(1,'Tembisa fun run','community running event','2026-10-10','Tembisa','run'), 
      (2,'Soweto Marathon','Annual marathon event','2026-11-15','soweto','walk'), 
      (3,'pretoria cycle race','road cycle event','2026-12-05','pretoria','cycle'); 

INSERT INTO weather(eventID,temperature,rainProbability) 
VALUES(1,24.50,10.00), (2,21.00,20.00), (3,26.00,15.00); 

INSERT INTO categories(eventID,categoryType,age) 
VALUES(1,'5km',18), (1,'10km',18), (2,'half marathon',18), 
      (2,'full marathon',18), (3,'20km cycling',18), (3,'40km cycling',18); 

INSERT INTO eventRoute(eventID, routeName, distance) 
VALUES(1,'tembisa community route',5.00), 
      (2,'soweto marathon route', 42.20), 
      (3,'pretoria cycling route',40.00); 

INSERT INTO Enrolments(userID,eventID,categoryID,enrolmemtDate, status) 
VALUES(3,1,1,'2026-09-01','Active'), 
      (4,1,2,'2026-09-02','Active'), 
      (3,2,3,'2026-09-03','Active'), 
      (4,3,5,'2026-09-04','Active'); 

INSERT INTO Results(enrolmentID, finishTime,position) 
VALUES(1,'00:32:15',5), (2,'00:58:20',12), (3,'01:45:30',8); 
