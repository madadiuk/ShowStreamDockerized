# ShowStream
Explore a vast library of movies and TV series with ShowStream. Enjoy HD streaming, personalized profiles, offline viewing, and smart recommendations. Dive into endless entertainment!
![showStream Logo](https://github.com/madadiuk/ShowStream/assets/24778272/a44756b8-331d-4a28-a643-c02e6ef6b327)

# Step 1: Install Docker
Ensure Docker is installed on your system. You can download and install Docker Desktop from the Docker official website.
https://docs.docker.com/engine/install/

# Step 2: Pull the SQL Server Docker Image

docker pull mcr.microsoft.com/mssql/server:2019-latest

# install docker:
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=42714271Ma" -p 1433:1433 --name sqlserver -v sqlvolume:/var/opt/mssql -d mcr.microsoft.com/mssql/server:2019-latest

# docker to start the existing container (if stopped):

docker start sqlserver

# To stop the container:

docker stop sqlserver

# To restart the container:

docker restart sqlserver

# Microsoft SQL Server , Server name:
localhost,1433

# Username:
sa

# Password:
42714271Ma

# enter the database name:

master 


## Database details to connect at DMU University machines.
# Server name:
v00egd00002l.lec-admin.dmu.ac.uk
# Username:
p2731259
# Password:
42714271427142714271


## datebase code

# tblUsers - Users must be defined before their profiles or transactions.

CREATE TABLE [dbo].[tblUsers] (
    [UserID]   INT           IDENTITY (1, 1) NOT NULL,
    [Username] VARCHAR (255) NOT NULL,
    [Email]    VARCHAR (255) NOT NULL,
    [Password] VARCHAR (255) NOT NULL,
    [Role]     VARCHAR (50)  NULL,
    PRIMARY KEY CLUSTERED ([UserID] ASC),
    UNIQUE NONCLUSTERED ([Username] ASC),
    UNIQUE NONCLUSTERED ([Email] ASC)  -- Ensures that the email address is unique across all users
);



## tblUserProfiles - Depends on tblUsers.

#

CREATE TABLE [dbo].[tblUserProfiles] (
    [ProfileID]   INT           IDENTITY (1, 1) NOT NULL,
    [UserID]      INT           NOT NULL,
    [FirstName]   VARCHAR (255) NULL,
    [LastName]    VARCHAR (255) NULL,
    [DateOfBirth] DATE          NULL,
    PRIMARY KEY CLUSTERED ([ProfileID] ASC),
    FOREIGN KEY ([UserID]) REFERENCES [dbo].[tblUsers] ([UserID])
);



## tblGenres - Independent of other tables, can be created early.

#

CREATE TABLE [dbo].[tblGenres] (
    [GenreID]     INT           IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (255) NOT NULL,
    [Description] VARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([GenreID] ASC)
);



## tblMovies - Depends on tblGenres.

#

CREATE TABLE [dbo].[tblMovies] (
    [MovieID]     INT           IDENTITY (1, 1) NOT NULL,
    [Title]       VARCHAR (255) NULL,
    [Description] VARCHAR (MAX) NULL,
    [GenreID]     INT           NULL,
    [Director]    VARCHAR (255) NULL,
    [ReleaseDate] DATE          NULL,
    [Duration]    INT           NULL,
    [ParentMovieID] INT         NULL,
    PRIMARY KEY CLUSTERED ([MovieID] ASC),
    FOREIGN KEY ([GenreID]) REFERENCES [dbo].[tblGenres] ([GenreID]),
    FOREIGN KEY ([ParentMovieID]) REFERENCES [dbo].[tblMovies] ([MovieID])
);


## tblSeries - Depends on tblGenres.

#

CREATE TABLE [dbo].[tblSeries] (
    [SeriesID]    INT           IDENTITY (1, 1) NOT NULL,
    [Title]       VARCHAR (255) NOT NULL,
    [GenreID]     INT           NULL,
    [StartYear]   DATE          NULL,
    [EndYear]     DATE          NULL,
    [Country]     VARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([SeriesID] ASC),
    FOREIGN KEY ([GenreID]) REFERENCES [dbo].[tblGenres] ([GenreID])
);


## tblEpisodes - Depends on tblSeries.

#

CREATE TABLE [dbo].[tblEpisodes] (
    [EpisodeID]     INT           IDENTITY (1, 1) NOT NULL,
    [SeriesID]      INT           NOT NULL,
    [SeasonNumber]  INT           NOT NULL,
    [EpisodeNumber] INT           NOT NULL,
    [Title]         VARCHAR (255) NULL,
    [Description]   VARCHAR (MAX) NULL,
    [ReleaseDate]   DATE          NULL,
    PRIMARY KEY CLUSTERED ([EpisodeID] ASC),
    FOREIGN KEY ([SeriesID]) REFERENCES [dbo].[tblSeries] ([SeriesID])
);


## tblVideoFiles - Depends on tblMovies, tblSeries, and tblEpisodes.

#

CREATE TABLE [dbo].[tblVideoFiles] (
    [VideoFileID]  INT           IDENTITY (1, 1) NOT NULL,
    [MovieID]      INT           NULL,
    [SeriesID]     INT           NULL,
    [EpisodeID]    INT           NULL,
    [VideoQuality] VARCHAR (50)  NOT NULL,
    [FilePath]     VARCHAR (MAX) NOT NULL,
    [FileSize]     BIGINT        NULL,
    PRIMARY KEY CLUSTERED ([VideoFileID] ASC),
    FOREIGN KEY ([MovieID]) REFERENCES [dbo].[tblMovies] ([MovieID]),
    FOREIGN KEY ([SeriesID]) REFERENCES [dbo].[tblSeries] ([SeriesID]),
    FOREIGN KEY ([EpisodeID]) REFERENCES [dbo].[tblEpisodes] ([EpisodeID])
);

## tblTransactions - Depends on tblUsers.

#

CREATE TABLE [dbo].[tblTransactions] (
    [TransactionID]        INT             IDENTITY (1, 1) NOT NULL,
    [UserID]               INT             NOT NULL,
    [Amount]               DECIMAL (19, 4) NOT NULL,
    [TransactionDate]      DATETIME        NOT NULL,
    [PaymentMethod]        VARCHAR (50)    NOT NULL,
    [PaymentMethodDetails] VARCHAR (255)   NULL,
    [Status]               VARCHAR (50)    NOT NULL,
    PRIMARY KEY CLUSTERED ([TransactionID] ASC),
    FOREIGN KEY ([UserID]) REFERENCES [dbo].[tblUsers] ([UserID])
);

## tblDownloads - Depends on tblUsers, tblMovies, and tblSeries.

#

CREATE TABLE [dbo].[tblDownloads] (
    [DownloadID]      INT          IDENTITY (1, 1) NOT NULL,
    [UserID]          INT          NOT NULL,
    [MovieID]         INT          NULL,
    [SeriesID]        INT          NULL,
    [DownloadDate]    DATETIME     NOT NULL,
    [ContentType]     VARCHAR (50) NOT NULL,
    [DownloadQuality] VARCHAR (50) NOT NULL,
    [Status]          VARCHAR (50) NOT NULL,
    [FileSize]        BIGINT       NULL,
    PRIMARY KEY CLUSTERED ([DownloadID] ASC),
    FOREIGN KEY ([UserID]) REFERENCES [dbo].[tblUsers] ([UserID]),
    FOREIGN KEY ([MovieID]) REFERENCES [dbo].[tblMovies] ([MovieID]),
    FOREIGN KEY ([SeriesID]) REFERENCES [dbo].[tblSeries] ([SeriesID])
);



## stored procedures

## tblUsers

# Add User


CREATE PROCEDURE spAddUser 
@Username VARCHAR(255), 
@Email VARCHAR(255), 
@Password VARCHAR(255), 
@Role VARCHAR(50)
AS
BEGIN
    INSERT INTO tblUsers (Username, Email, Password, Role) 
    VALUES (@Username, @Email, @Password, @Role)
END

## Delete User


CREATE PROCEDURE spDeleteUser 
@UserID INT
AS
BEGIN
    DELETE FROM tblUsers WHERE UserID = @UserID
END

## Update User


CREATE PROCEDURE spUpdateUser 
@UserID INT, @Username VARCHAR(255), @Email VARCHAR(255), @Password VARCHAR(255), @Role VARCHAR(50)
AS
BEGIN
    UPDATE tblUsers 
    SET Username = @Username, Email = @Email, Password = @Password, Role = @Role 
    WHERE UserID = @UserID
END

## Get All Users


CREATE PROCEDURE spGetAllUsers
AS
BEGIN
    SELECT UserID, Username, Email, Role FROM tblUsers
END

## tblGenres

# Add Genre


CREATE PROCEDURE spAddGenre 
@Name VARCHAR(255), @Description VARCHAR(MAX)
AS
BEGIN
    INSERT INTO tblGenres (Name, Description) VALUES (@Name, @Description)
END

## Update Genre


CREATE PROCEDURE spUpdateGenre 
@GenreID INT, @Name VARCHAR(255), @Description VARCHAR(MAX)
AS
BEGIN
    UPDATE tblGenres 
    SET Name = @Name, Description = @Description 
    WHERE GenreID = @GenreID
END

## Delete Genre


CREATE PROCEDURE spDeleteGenre 
@GenreID INT
AS
BEGIN
    DELETE FROM tblGenres WHERE GenreID = @GenreID
END

## tblMovies

# Add Movie


CREATE PROCEDURE spAddMovie 
@Title VARCHAR(255), @Description VARCHAR(MAX), @GenreID INT, @Director VARCHAR(255), @ReleaseDate DATE, @Duration INT
AS
BEGIN
    INSERT INTO tblMovies (Title, Description, GenreID, Director, ReleaseDate, Duration) 
    VALUES (@Title, @Description, @GenreID, @Director, @ReleaseDate, @Duration)
END

## Update Movie


CREATE PROCEDURE spUpdateMovie 
@MovieID INT, @Title VARCHAR(255), @Description VARCHAR(MAX), @GenreID INT, @Director VARCHAR(255), @ReleaseDate DATE, @Duration INT
AS
BEGIN
    UPDATE tblMovies 
    SET Title = @Title, Description = @Description, GenreID = @GenreID, Director = @Director, ReleaseDate = @ReleaseDate, Duration = @Duration 
    WHERE MovieID = @MovieID
END

## Delete Movie


CREATE PROCEDURE spDeleteMovie 
@MovieID INT
AS
BEGIN
    DELETE FROM tblMovies WHERE MovieID = @MovieID
END

##





# Database ERD Diagram:
![tblUsers (1)](https://github.com/madadiuk/ShowStreamDockerized/assets/24778272/510e2f4d-e447-47cb-8f48-b0750f5381be)


## tblUserProfiles
This table holds additional personal information about users. Each record links to a tblUsers record and includes the user's first name, last name, and date of birth.

ProfileID: A unique identifier for each user profile.
UserID: The ID of the user this profile belongs to.
FirstName: The user's first name.
LastName: The user's last name.
DateOfBirth: The user's date of birth.

## tblUsers
This table stores the essential information of all users, including their credentials and roles within the platform.

UserID: A unique identifier for each user.
Username: The chosen username for the user, which must be unique.
Email: The user's email address, which must be unique.
Password: The user's password, stored in a hashed format for security.
Role: The user's role, which could be a regular user or an admin, controlling access to different parts of the system.

## tblTransactions
This table records transactions made by users, which could involve purchasing subscriptions or content.

TransactionID: A unique identifier for each transaction.
UserID: The ID of the user who made the transaction.
Amount: The financial amount of the transaction.
TransactionDate: The date and time when the transaction occurred.
PaymentMethod: The method used for the transaction (e.g., credit card, PayPal).
Status: The current status of the transaction (e.g., completed, pending).

## tblDownloads
This table tracks the downloads initiated by users, detailing what content was downloaded, when, and the size of the files.

DownloadID: A unique identifier for each download event.
UserID: The ID of the user who initiated the download.
MovieID/SeriesID: The ID of the movie or series that was downloaded.
DownloadDate: The date and time the download was initiated.
ContentType: The type of content downloaded, either a movie or series.
Status: The status of the download (e.g., in progress, completed).
FileSize: The size of the downloaded file in bytes.

## tblSeries
This table contains information about TV series available on the platform.

SeriesID: A unique identifier for each series.
GenreID: The genre the series belongs to.
Title: The title of the series.
Season: The season number of the series.
EpisodeCount: The number of episodes in the series.
StartYear/EndYear: The year the series started and ended (if it has concluded).
Country: The country of origin of the series.

## tblGenres
This table defines the different genres that can be associated with movies and series.

GenreID: A unique identifier for each genre.
Name: The name of the genre (e.g., action, drama, comedy).
Description: A more detailed description of what the genre entails.

## tblMovies
This table stores details about the individual movies available for streaming and downloading.

MovieID: A unique identifier for each movie.
GenreID: The genre the movie belongs to.
Title: The title of the movie.
Description: A brief synopsis of the movie.
Director: The director of the movie.
ReleaseDate: The release date of the movie.
Duration: The runtime of the movie, typically in minutes.
This overview should provide a clear understanding of the purpose of each table and how they relate to one another within the ShowStream platform. If further details or features need to be explained, such as specific administrative functionalities or user actions, those can be added to the descriptions accordingly.


## Here's an explanation of each table in your ERD that you can share with your colleagues:

# tblUserProfiles
This table holds additional personal information about users. Each record links to a tblUsers record and includes the user's first name, last name, and date of birth.

ProfileID: A unique identifier for each user profile.
UserID: The ID of the user this profile belongs to.
FirstName: The user's first name.
LastName: The user's last name.
DateOfBirth: The user's date of birth.
# tblUsers
This table stores the essential information of all users, including their credentials and roles within the platform.

UserID: A unique identifier for each user.
Username: The chosen username for the user, which must be unique.
Email: The user's email address, which must be unique.
Password: The user's password, stored in a hashed format for security.
Role: The user's role, which could be a regular user or an admin, controlling access to different parts of the system.
# tblTransactions
This table records transactions made by users, which could involve purchasing subscriptions or content.

TransactionID: A unique identifier for each transaction.
UserID: The ID of the user who made the transaction.
Amount: The financial amount of the transaction.
TransactionDate: The date and time when the transaction occurred.
PaymentMethod: The method used for the transaction (e.g., credit card, PayPal).
Status: The current status of the transaction (e.g., completed, pending).
# tblDownloads
This table tracks the downloads initiated by users, detailing what content was downloaded, when, and the size of the files.

DownloadID: A unique identifier for each download event.
UserID: The ID of the user who initiated the download.
MovieID/SeriesID: The ID of the movie or series that was downloaded.
DownloadDate: The date and time the download was initiated.
ContentType: The type of content downloaded, either a movie or series.
Status: The status of the download (e.g., in progress, completed).
FileSize: The size of the downloaded file in bytes.
# tblSeries
This table contains information about TV series available on the platform.

SeriesID: A unique identifier for each series.
GenreID: The genre the series belongs to.
Title: The title of the series.
Season: The season number of the series.
EpisodeCount: The number of episodes in the series.
StartYear/EndYear: The year the series started and ended (if it has concluded).
Country: The country of origin of the series.
# tblGenres
This table defines the different genres that can be associated with movies and series.

GenreID: A unique identifier for each genre.
Name: The name of the genre (e.g., action, drama, comedy).
Description: A more detailed description of what the genre entails.
# tblMovies
This table stores details about the individual movies available for streaming and downloading.

MovieID: A unique identifier for each movie.
GenreID: The genre the movie belongs to.
Title: The title of the movie.
Description: A brief synopsis of the movie.
Director: The director of the movie.
ReleaseDate: The release date of the movie.
Duration: The runtime of the movie, typically in minutes.
This overview should provide a clear understanding of the purpose of each table and how they relate to one another within the ShowStream platform. If further details or features need to be explained, such as specific administrative functionalities or user actions, those can be added to the descriptions accordingly.

## a summary of its correctness:

# tblUsers:
 You’ve appropriately differentiated users by role, which allows for the implementation of permission levels within your application logic. The UserID is correctly set as an auto-incremented primary key, and the Username and Email fields are correctly set to be unique.
# tblUserProfiles:
 The one-to-one relationship with tblUsers is correct, with UserID as a foreign key.
# tblMovies:
 The MovieID is the primary key, and GenreID is a foreign key that references tblGenres, which is standard practice.
# tblSeries:
 The setup is analogous to tblMovies, with its own primary key and a foreign key to tblGenres.
# tblGenres:
 It’s standalone and serves as a reference for genres that are used by tblMovies and tblSeries.
# tblDownloads:
 The relationships with tblUsers, tblMovies, and tblSeries are correct, showing that a user can download many movies or series, and a movie or series can be downloaded by many users.
# tblTransactions:
 The relationship with tblUsers is accurately depicted to show that a user can have many transactions.
# Verb Labels:
 The verb labels such as "initiates", "is downloaded", and "conducts" are appropriate and provide a clear understanding of the nature of the relationships.
