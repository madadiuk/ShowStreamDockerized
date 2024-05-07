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
CREATE TABLE [dbo].[tblUsers] (
    [UserID]   INT           IDENTITY (1, 1) NOT NULL,
    [Username] VARCHAR (255) NULL,
    [Email]    VARCHAR (255) NULL,
    [Password] VARCHAR (255) NULL,
    [Role]     VARCHAR (50)  NULL,
    PRIMARY KEY CLUSTERED ([UserID] ASC),
    UNIQUE NONCLUSTERED ([Username] ASC)
);

##
CREATE TABLE [dbo].[tblUserProfiles] (
    [ProfileID]   INT           IDENTITY (1, 1) NOT NULL,
    [UserID]      INT           NULL,
    [FirstName]   VARCHAR (255) NULL,
    [LastName]    VARCHAR (255) NULL,
    [DateOfBirth] DATE          NULL,
    PRIMARY KEY CLUSTERED ([ProfileID] ASC),
    FOREIGN KEY ([UserID]) REFERENCES [dbo].[tblUsers] ([UserID])
);

##
CREATE TABLE [dbo].[tblTransactions] (
    [TransactionID]   INT             IDENTITY (1, 1) NOT NULL,
    [UserID]          INT             NULL,
    [Amount]          DECIMAL (19, 4) NULL,
    [TransactionDate] DATETIME        NULL,
    [PaymentMethod]   VARCHAR (50)    NULL,
    [Status]          VARCHAR (50)    NULL,
    PRIMARY KEY CLUSTERED ([TransactionID] ASC),
    FOREIGN KEY ([UserID]) REFERENCES [dbo].[tblUsers] ([UserID])
);
##
CREATE TABLE [dbo].[tblSeries] (
    [SeriesID]     INT           IDENTITY (1, 1) NOT NULL,
    [GenreID]      INT           NULL,
    [Title]        VARCHAR (255) NULL,
    [Season]       INT           NULL,
    [EpisodeCount] INT           NULL,
    [StartYear]    DATE          NULL,
    [EndYear]      DATE          NULL,
    [Country]      VARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([SeriesID] ASC),
    FOREIGN KEY ([GenreID]) REFERENCES [dbo].[tblGenres] ([GenreID])
);
##
CREATE TABLE [dbo].[tblMovies] (
    [MovieID]     INT           IDENTITY (1, 1) NOT NULL,
    [Title]       VARCHAR (255) NULL,
    [Description] VARCHAR (MAX) NULL,
    [GenreID]     INT           NULL,
    [Director]    VARCHAR (255) NULL,
    [ReleaseDate] DATE          NULL,
    [Duration]    INT           NULL,
    PRIMARY KEY CLUSTERED ([MovieID] ASC),
    FOREIGN KEY ([GenreID]) REFERENCES [dbo].[tblGenres] ([GenreID])
);
##
CREATE TABLE [dbo].[tblGenres] (
    [GenreID]     INT           IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (255) NULL,
    [Description] VARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([GenreID] ASC)
);
##
CREATE TABLE [dbo].[tblDownloads] (
    [DownloadID]   INT          IDENTITY (1, 1) NOT NULL,
    [UserID]       INT          NULL,
    [MovieID]      INT          NULL,
    [SeriesID]     INT          NULL,
    [DownloadDate] DATETIME     NULL,
    [ContentType]  VARCHAR (50) NULL,
    [Status]       VARCHAR (50) NULL,
    [FileSize]     BIGINT       NULL,
    PRIMARY KEY CLUSTERED ([DownloadID] ASC),
    FOREIGN KEY ([UserID]) REFERENCES [dbo].[tblUsers] ([UserID]),
    FOREIGN KEY ([MovieID]) REFERENCES [dbo].[tblMovies] ([MovieID]),
    FOREIGN KEY ([SeriesID]) REFERENCES [dbo].[tblSeries] ([SeriesID])
);

## stored procedures

# spAddDownload
CREATE PROCEDURE spAddDownload
    @UserID INT,
    @MovieID INT,
    @SeriesID INT,
    @DownloadDate DATE,
    @ContentType VARCHAR(50),
    @Status VARCHAR(50),
    @FileSize BIGINT
AS
BEGIN
    INSERT INTO tblDownloads (UserID, MovieID, SeriesID, DownloadDate, ContentType, Status, FileSize)
    VALUES (@UserID, @MovieID, @SeriesID, @DownloadDate, @ContentType, @Status, @FileSize)
END

# spAddGenre
CREATE PROCEDURE spAddGenre
    @Name VARCHAR(255),
    @Description VARCHAR(MAX)
AS
BEGIN
    INSERT INTO tblGenres (Name, Description)
    VALUES (@Name, @Description)
END

# spAddMovie
CREATE PROCEDURE spAddMovie
    @Title VARCHAR(255),
    @Description VARCHAR(MAX),
    @GenreID INT,
    @Director VARCHAR(255),
    @ReleaseDate DATE,
    @Duration INT
AS
BEGIN
    INSERT INTO tblMovies (Title, Description, GenreID, Director, ReleaseDate, Duration)
    VALUES (@Title, @Description, @GenreID, @Director, @ReleaseDate, @Duration)
END

# spAddSeries
CREATE PROCEDURE spAddSeries
    @GenreID INT,
    @Title VARCHAR(255),
    @Season INT,
    @EpisodeCount INT,
    @StartYear DATE,
    @EndYear DATE,
    @Country VARCHAR(255)
AS
BEGIN
    INSERT INTO tblSeries (GenreID, Title, Season, EpisodeCount, StartYear, EndYear, Country)
    VALUES (@GenreID, @Title, @Season, @EpisodeCount, @StartYear, @EndYear, @Country)
END

# spAddTransaction
CREATE PROCEDURE spAddTransaction
    @UserID INT,
    @Amount DECIMAL(19,4),
    @TransactionDate DATE,
    @PaymentMethod VARCHAR(50),
    @Status VARCHAR(50)
AS
BEGIN
    INSERT INTO tblTransactions (UserID, Amount, TransactionDate, PaymentMethod, Status)
    VALUES (@UserID, @Amount, @TransactionDate, @PaymentMethod, @Status)
END

# spAddUser
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

# spAddUserProfile
CREATE PROCEDURE spAddUserProfile
    @UserID INT,
    @FirstName VARCHAR(255),
    @LastName VARCHAR(255),
    @DateOfBirth DATE
AS
BEGIN
    INSERT INTO tblUserProfiles (UserID, FirstName, LastName, DateOfBirth)
    VALUES (@UserID, @FirstName, @LastName, @DateOfBirth)
END
# spDeleteDownload
CREATE PROCEDURE spDeleteDownload
    @DownloadID INT
AS
BEGIN
    DELETE FROM tblDownloads
    WHERE DownloadID = @DownloadID
END
# spDeleteGenre
CREATE PROCEDURE spDeleteGenre
    @GenreID INT
AS
BEGIN
    DELETE FROM tblGenres
    WHERE GenreID = @GenreID
END
# spDeleteMovie
CREATE PROCEDURE spDeleteMovie
    @MovieID INT
AS
BEGIN
    DELETE FROM tblMovies
    WHERE MovieID = @MovieID
END
# spDeleteSeries
CREATE PROCEDURE spDeleteSeries
    @SeriesID INT
AS
BEGIN
    DELETE FROM tblSeries
    WHERE SeriesID = @SeriesID
END
# spDeleteTransaction
CREATE PROCEDURE spDeleteTransaction
    @TransactionID INT
AS
BEGIN
    DELETE FROM tblTransactions
    WHERE TransactionID = @TransactionID
END
# spDeleteUser
CREATE PROCEDURE spDeleteUser
    @UserID INT
AS
BEGIN
    DELETE FROM tblUsers
    WHERE UserID = @UserID
END
#  spDeleteUserProfile
CREATE PROCEDURE spDeleteUserProfile
    @ProfileID INT
AS
BEGIN
    DELETE FROM tblUserProfiles
    WHERE ProfileID = @ProfileID
END
#  spGetAllDownloads
CREATE PROCEDURE spGetAllDownloads
AS
BEGIN
    SELECT DownloadID, UserID, MovieID, SeriesID, DownloadDate, ContentType, Status, FileSize
    FROM tblDownloads
END
# spGetAllGenres
CREATE PROCEDURE spGetAllGenres
AS
BEGIN
    SELECT GenreID, Name, Description
    FROM tblGenres
END
# spGetAllMovies
CREATE PROCEDURE spGetAllMovies
AS
BEGIN
    SELECT MovieID, Title, Description, GenreID, Director, ReleaseDate, Duration
    FROM tblMovies
END
# spGetAllSeries
CREATE PROCEDURE spGetAllSeries
AS
BEGIN
    SELECT SeriesID, GenreID, Title, Season, EpisodeCount, StartYear, EndYear, Country
    FROM tblSeries
END
# spGetAllTransactions
CREATE PROCEDURE spGetAllTransactions
AS
BEGIN
    SELECT TransactionID, UserID, Amount, TransactionDate, PaymentMethod, Status
    FROM tblTransactions
END
# spGetAllUserProfiles
CREATE PROCEDURE spGetAllUserProfiles
AS
BEGIN
    SELECT ProfileID, UserID, FirstName, LastName, DateOfBirth
    FROM tblUserProfiles
END
# spGetAllUsers
CREATE PROCEDURE spGetAllUsers
AS
BEGIN
    SELECT UserID, Username, Email, Role
    FROM tblUsers
END
# spGetDownload
CREATE PROCEDURE spGetDownload
    @DownloadID INT
AS
BEGIN
    SELECT DownloadID, UserID, MovieID, SeriesID, DownloadDate, ContentType, Status, FileSize
    FROM tblDownloads
    WHERE DownloadID = @DownloadID
END
# spGetGenre
CREATE PROCEDURE spGetGenre
    @GenreID INT
AS
BEGIN
    SELECT GenreID, Name, Description
    FROM tblGenres
    WHERE GenreID = @GenreID
END

#spGetMovie

CREATE PROCEDURE spGetMovie
    @MovieID INT
AS
BEGIN
    SELECT MovieID, Title, Description, GenreID, Director, ReleaseDate, Duration
    FROM tblMovies
    WHERE MovieID = @MovieID
END
# spGetSeries
CREATE PROCEDURE spGetSeries
    @SeriesID INT
AS
BEGIN
    SELECT SeriesID, GenreID, Title, Season, EpisodeCount, StartYear, EndYear, Country
    FROM tblSeries
    WHERE SeriesID = @SeriesID
END

# spGetTransaction
CREATE PROCEDURE spGetTransaction
    @TransactionID INT
AS
BEGIN
    SELECT TransactionID, UserID, Amount, TransactionDate, PaymentMethod, Status
    FROM tblTransactions
    WHERE TransactionID = @TransactionID
END
# spGetUser
CREATE PROCEDURE spGetUser
    @UserID INT
AS
BEGIN
    SELECT UserID, Username, Email, Role
    FROM tblUsers
    WHERE UserID = @UserID
END

# spGetUserProfile
CREATE PROCEDURE spGetUserProfile
    @ProfileID INT
AS
BEGIN
    SELECT ProfileID, UserID, FirstName, LastName, DateOfBirth
    FROM tblUserProfiles
    WHERE ProfileID = @ProfileID
END
# spUpdateDownload
CREATE PROCEDURE spUpdateDownload
    @DownloadID INT,
    @UserID INT,
    @MovieID INT,
    @SeriesID INT,
    @DownloadDate DATE,
    @ContentType VARCHAR(50),
    @Status VARCHAR(50),
    @FileSize BIGINT
AS
BEGIN
    UPDATE tblDownloads
    SET UserID = @UserID, MovieID = @MovieID, SeriesID = @SeriesID, 
        DownloadDate = @DownloadDate, ContentType = @ContentType, 
        Status = @Status, FileSize = @FileSize
    WHERE DownloadID = @DownloadID
END

# spUpdateGenre
CREATE PROCEDURE spUpdateGenre
    @GenreID INT,
    @Name VARCHAR(255),
    @Description VARCHAR(MAX)
AS
BEGIN
    UPDATE tblGenres
    SET Name = @Name, Description = @Description
    WHERE GenreID = @GenreID
END
#  spUpdateMovie
CREATE PROCEDURE spUpdateMovie
    @MovieID INT,
    @Title VARCHAR(255),
    @Description VARCHAR(MAX),
    @GenreID INT,
    @Director VARCHAR(255),
    @ReleaseDate DATE,
    @Duration INT
AS
BEGIN
    UPDATE tblMovies
    SET Title = @Title, Description = @Description, GenreID = @GenreID, 
        Director = @Director, ReleaseDate = @ReleaseDate, Duration = @Duration
    WHERE MovieID = @MovieID
END

# spUpdateSeries
CREATE PROCEDURE spUpdateSeries
    @SeriesID INT,
    @GenreID INT,
    @Title VARCHAR(255),
    @Season INT,
    @EpisodeCount INT,
    @StartYear DATE,
    @EndYear DATE,
    @Country VARCHAR(255)
AS
BEGIN
    UPDATE tblSeries
    SET GenreID = @GenreID, Title = @Title, Season = @Season, EpisodeCount = @EpisodeCount, 
        StartYear = @StartYear, EndYear = @EndYear, Country = @Country
    WHERE SeriesID = @SeriesID
END
# spUpdateTransaction
CREATE PROCEDURE spUpdateTransaction
    @TransactionID INT,
    @UserID INT,
    @Amount DECIMAL(19,4),
    @TransactionDate DATE,
    @PaymentMethod VARCHAR(50),
    @Status VARCHAR(50)
AS
BEGIN
    UPDATE tblTransactions
    SET UserID = @UserID, Amount = @Amount, TransactionDate = @TransactionDate, 
        PaymentMethod = @PaymentMethod, Status = @Status
    WHERE TransactionID = @TransactionID
END

# spUpdateUser
CREATE PROCEDURE spUpdateUser
    @UserID INT,
    @Username VARCHAR(255),
    @Email VARCHAR(255),
    @Password VARCHAR(255),
    @Role VARCHAR(50)
AS
BEGIN
    UPDATE tblUsers
    SET Username = @Username, Email = @Email, Password = @Password, Role = @Role
    WHERE UserID = @UserID
END
# spUpdateUserProfile
CREATE PROCEDURE spUpdateUserProfile
    @ProfileID INT,
    @FirstName VARCHAR(255),
    @LastName VARCHAR(255),
    @DateOfBirth DATE
AS
BEGIN
    UPDATE tblUserProfiles
    SET FirstName = @FirstName, LastName = @LastName, DateOfBirth = @DateOfBirth
    WHERE ProfileID = @ProfileID
END


# Database ERD Diagram:
![tblUsers (9)](https://github.com/madadiuk/ShowStream/assets/24778272/1b9e590f-df72-4acd-b2cb-bd2225a112a2)

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
