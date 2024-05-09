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



## stored procedures (59)

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

## tblSeries

# Add Series


CREATE PROCEDURE spAddSeries 
@Title VARCHAR(255), @GenreID INT, @StartYear DATE, @EndYear DATE, @Country VARCHAR(255)
AS
BEGIN
    INSERT INTO tblSeries (Title, GenreID, StartYear, EndYear, Country) 
    VALUES (@Title, @GenreID, @StartYear, @EndYear, @Country)
END

## Update Series


CREATE PROCEDURE spUpdateSeries 
@SeriesID INT, @Title VARCHAR(255), @GenreID INT, @StartYear DATE, @EndYear DATE, @Country VARCHAR(255)
AS
BEGIN
    UPDATE tblSeries 
    SET Title = @Title, GenreID = @GenreID, StartYear = @StartYear, EndYear = @EndYear, Country = @Country 
    WHERE SeriesID = @SeriesID
END

## Delete Series


CREATE PROCEDURE spDeleteSeries 
@SeriesID INT
AS
BEGIN
    DELETE FROM tblSeries WHERE SeriesID = @SeriesID
END

## Get All Series


CREATE PROCEDURE spGetAllSeries
AS
BEGIN
    SELECT SeriesID, Title, GenreID, StartYear, EndYear, Country FROM tblSeries
END

## tblEpisodes
# Add Episode


CREATE PROCEDURE spAddEpisode 
@SeriesID INT, @SeasonNumber INT, @EpisodeNumber INT, @Title VARCHAR(255), @Description VARCHAR(MAX), @ReleaseDate DATE
AS
BEGIN
    INSERT INTO tblEpisodes (SeriesID, SeasonNumber, EpisodeNumber, Title, Description, ReleaseDate) 
    VALUES (@SeriesID, @SeasonNumber, @EpisodeNumber, @Title, @Description, @ReleaseDate)
END

## Update Episode


CREATE PROCEDURE spUpdateEpisode 
@EpisodeID INT, @SeriesID INT, @SeasonNumber INT, @EpisodeNumber INT, @Title VARCHAR(255), @Description VARCHAR(MAX), @ReleaseDate DATE
AS
BEGIN
    UPDATE tblEpisodes 
    SET SeriesID = @SeriesID, SeasonNumber = @SeasonNumber, EpisodeNumber = @EpisodeNumber, Title = @Title, Description = @Description, ReleaseDate = @ReleaseDate 
    WHERE EpisodeID = @EpisodeID
END

## Delete Episode


CREATE PROCEDURE spDeleteEpisode 
@EpisodeID INT
AS
BEGIN
    DELETE FROM tblEpisodes WHERE EpisodeID = @EpisodeID
END

## Get All Episodes


CREATE PROCEDURE spGetAllEpisodes
AS
BEGIN
    SELECT EpisodeID, SeriesID, SeasonNumber, EpisodeNumber, Title, Description, ReleaseDate FROM tblEpisodes
END

## tblVideoFiles

# Add VideoFile


CREATE PROCEDURE spAddVideoFile 
@MovieID INT, @SeriesID INT, @EpisodeID INT, @VideoQuality VARCHAR(50), @FilePath VARCHAR(MAX), @FileSize BIGINT
AS
BEGIN
    INSERT INTO tblVideoFiles (MovieID, SeriesID, EpisodeID, VideoQuality, FilePath, FileSize) 
    VALUES (@MovieID, @SeriesID, @EpisodeID, @VideoQuality, @FilePath, @FileSize)
END
## Update VideoFile


CREATE PROCEDURE spUpdateVideoFile 
@VideoFileID INT, @MovieID INT, @SeriesID INT, @EpisodeID INT, @VideoQuality VARCHAR(50), @FilePath VARCHAR(MAX), @FileSize BIGINT
AS
BEGIN
    UPDATE tblVideoFiles 
    SET MovieID = @MovieID, SeriesID = @SeriesID, EpisodeID = @EpisodeID, VideoQuality = @VideoQuality, FilePath = @FilePath, FileSize = @FileSize 
    WHERE VideoFileID = @VideoFileID
END

## Delete VideoFile



CREATE PROCEDURE spDeleteVideoFile 
@VideoFileID INT
AS
BEGIN
    DELETE FROM tblVideoFiles WHERE VideoFileID = @VideoFileID
END

## Get All VideoFiles


CREATE PROCEDURE spGetAllVideoFiles
AS
BEGIN
    SELECT VideoFileID, MovieID, SeriesID, EpisodeID, VideoQuality, FilePath, FileSize FROM tblVideoFiles
END

## tblTransactions

# Add Transaction


CREATE PROCEDURE spAddTransaction 
@UserID INT, @Amount DECIMAL(19,4), @TransactionDate DATE, @PaymentMethod VARCHAR(50), @Status VARCHAR(50)
AS
BEGIN
    INSERT INTO tblTransactions (UserID, Amount, TransactionDate, PaymentMethod, Status) 
    VALUES (@UserID, @Amount, @TransactionDate, @PaymentMethod, @Status)
END

## Update Transaction


CREATE PROCEDURE spUpdateTransaction 
@TransactionID INT, @UserID INT, @Amount DECIMAL(19,4), @TransactionDate DATE, @PaymentMethod VARCHAR(50), @Status VARCHAR(50)
AS
BEGIN
    UPDATE tblTransactions 
    SET UserID = @UserID, Amount = @Amount, TransactionDate = @TransactionDate, PaymentMethod = @PaymentMethod, Status = @Status 
    WHERE TransactionID = @TransactionID
END

## Delete Transaction


CREATE PROCEDURE spDeleteTransaction 
@TransactionID INT
AS
BEGIN
    DELETE FROM tblTransactions WHERE TransactionID = @TransactionID
END

## Get All Transactions


CREATE PROCEDURE spGetAllTransactions
AS
BEGIN
    SELECT TransactionID, UserID, Amount, TransactionDate, PaymentMethod, Status FROM tblTransactions
END

# tblDownloads

## Add Download


CREATE PROCEDURE spAddDownload 
@UserID INT, @MovieID INT, @SeriesID INT, @DownloadDate DATE, @ContentType VARCHAR(50), @DownloadQuality VARCHAR(50), @Status VARCHAR(50), @FileSize BIGINT
AS
BEGIN
    INSERT INTO tblDownloads (UserID, MovieID, SeriesID, DownloadDate, ContentType, DownloadQuality, Status, FileSize) 
    VALUES (@UserID, @MovieID, @SeriesID, @DownloadDate, @ContentType, @DownloadQuality, @Status, @FileSize)
END

## Update Download


CREATE PROCEDURE spUpdateDownload 
@DownloadID INT, @UserID INT, @MovieID INT, @SeriesID INT, @DownloadDate DATE, @ContentType VARCHAR(50), @DownloadQuality VARCHAR(50), @Status VARCHAR(50), @FileSize BIGINT
AS
BEGIN
    UPDATE tblDownloads 
    SET UserID = @UserID, MovieID = @MovieID, SeriesID = @SeriesID, DownloadDate = @DownloadDate, ContentType = @ContentType, DownloadQuality = @DownloadQuality, Status = @Status, FileSize = @FileSize 
    WHERE DownloadID = @DownloadID
END

## Delete Download


CREATE PROCEDURE spDeleteDownload 
@DownloadID INT
AS
BEGIN
    DELETE FROM tblDownloads WHERE DownloadID = @DownloadID
END

## Get All Downloads


CREATE PROCEDURE spGetAllDownloads
AS
BEGIN
    SELECT DownloadID, UserID, MovieID, SeriesID, DownloadDate, ContentType, DownloadQuality, Status, FileSize FROM tblDownloads
END

## filter movies by genre:

CREATE PROCEDURE spFilterMoviesByGenre 
@GenreID INT
AS
BEGIN
    SELECT MovieID, Title, Description, Director, ReleaseDate, Duration 
    FROM tblMovies 
    WHERE GenreID = @GenreID
END

## tblUserProfiles

# Add UserProfile

CREATE PROCEDURE spAddUserProfile 
@UserID INT, @FirstName VARCHAR(255), @LastName VARCHAR(255), @DateOfBirth DATE
AS
BEGIN
    INSERT INTO tblUserProfiles (UserID, FirstName, LastName, DateOfBirth) 
    VALUES (@UserID, @FirstName, @LastName, @DateOfBirth)
END

## Update UserProfile


CREATE PROCEDURE spUpdateUserProfile 
@ProfileID INT, @FirstName VARCHAR(255), @LastName VARCHAR(255), @DateOfBirth DATE
AS
BEGIN
    UPDATE tblUserProfiles 
    SET FirstName = @FirstName, LastName = @LastName, DateOfBirth = @DateOfBirth 
    WHERE ProfileID = @ProfileID
END

## Delete UserProfile


CREATE PROCEDURE spDeleteUserProfile 
@ProfileID INT
AS
BEGIN
    DELETE FROM tblUserProfiles WHERE ProfileID = @ProfileID
END

## Get UserProfile


CREATE PROCEDURE spGetUserProfile 
@ProfileID INT
AS
BEGIN
    SELECT ProfileID, UserID, FirstName, LastName, DateOfBirth 
    FROM tblUserProfiles 
    WHERE ProfileID = @ProfileID
END

## Get All UserProfiles


CREATE PROCEDURE spGetAllUserProfiles
AS
BEGIN
    SELECT ProfileID, UserID, FirstName, LastName, DateOfBirth 
    FROM tblUserProfiles
END

## Filter Users by Role

# This procedure filters users based on their roles, useful for role-based views or administrative functions.


CREATE PROCEDURE spFilterUsersByRole
@Role VARCHAR(50)
AS
BEGIN
    SELECT UserID, Username, Email, Role
    FROM tblUsers
    WHERE Role = @Role
END

## Filter Movies by Director

# Retrieve all movies directed by a specific director, helpful for director-centric queries.


CREATE PROCEDURE spFilterMoviesByDirector
@Director VARCHAR(255)
AS
BEGIN
    SELECT MovieID, Title, Description, GenreID, Director, ReleaseDate, Duration
    FROM tblMovies
    WHERE Director = @Director
END
## Filter Series by Country

# This procedure allows filtering series based on the country of origin.


CREATE PROCEDURE spFilterSeriesByCountry
@Country VARCHAR(255)
AS
BEGIN
    SELECT SeriesID, Title, GenreID, StartYear, EndYear, Country
    FROM tblSeries
    WHERE Country = @Country
END

## Filter Episodes by Release Year

# Retrieve all episodes released in a specific year, which is useful for timeline-based queries.


CREATE PROCEDURE spFilterEpisodesByReleaseYear
@ReleaseYear INT
AS
BEGIN
    SELECT EpisodeID, SeriesID, SeasonNumber, EpisodeNumber, Title, Description, ReleaseDate
    FROM tblEpisodes
    WHERE YEAR(ReleaseDate) = @ReleaseYear
END

## Filter Downloads by Date Range

# This procedure helps to retrieve downloads within a specific date range.



CREATE PROCEDURE spFilterDownloadsByDate
@StartDate DATE, @EndDate DATE
AS
BEGIN
    SELECT DownloadID, UserID, MovieID, SeriesID, DownloadDate, ContentType, DownloadQuality, Status, FileSize
    FROM tblDownloads
    WHERE DownloadDate BETWEEN @StartDate AND @EndDate
END

## Filter Transactions by Amount Range

# Retrieve transactions within a specific amount range.


CREATE PROCEDURE spFilterTransactionsByAmount
@MinAmount DECIMAL(19,4), @MaxAmount DECIMAL(19,4)
AS
BEGIN
    SELECT TransactionID, UserID, Amount, TransactionDate, PaymentMethod, Status
    FROM tblTransactions
    WHERE Amount BETWEEN @MinAmount AND @MaxAmount
END

## Filter Transactions by User

# Retrieve all transactions made by a specific user, which is useful for user account management and financial auditing.


CREATE PROCEDURE spFilterTransactionsByUser
@UserID INT
AS
BEGIN
    SELECT TransactionID, Amount, TransactionDate, PaymentMethod, Status
    FROM tblTransactions
    WHERE UserID = @UserID
END
## Filter Downloads by User

# Retrieve all downloads initiated by a specific user. This is especially useful for tracking user activity and preferences.


CREATE PROCEDURE spFilterDownloadsByUser
@UserID INT
AS
BEGIN
    SELECT DownloadID, MovieID, SeriesID, DownloadDate, ContentType, DownloadQuality, Status, FileSize
    FROM tblDownloads
    WHERE UserID = @UserID
END

## Filter Episodes by Series

# Retrieve all episodes from a specific series. This helps users or applications quickly gather all content related to a particular series.


CREATE PROCEDURE spFilterEpisodesBySeries
@SeriesID INT
AS
BEGIN
    SELECT EpisodeID, SeasonNumber, EpisodeNumber, Title, Description, ReleaseDate
    FROM tblEpisodes
    WHERE SeriesID = @SeriesID
END

## Filter Video Files by Quality

# Retrieve all video files that match a specified quality. This can be useful for streaming services that need to adapt the video quality based on user preferences or network conditions.



CREATE PROCEDURE spFilterVideoFilesByQuality
@VideoQuality VARCHAR(50)
AS
BEGIN
    SELECT VideoFileID, MovieID, SeriesID, EpisodeID, FilePath, FileSize
    FROM tblVideoFiles
    WHERE VideoQuality = @VideoQuality
END

## Filter Movies by Release Year

# Retrieve all movies released in a specific year, allowing for time-based queries that might be useful for generating annual reports or summaries.


CREATE PROCEDURE spFilterMoviesByReleaseYear
@ReleaseYear INT
AS
BEGIN
    SELECT MovieID, Title, Description, GenreID, Director, ReleaseDate, Duration
    FROM tblMovies
    WHERE YEAR(ReleaseDate) = @ReleaseYear
END

## Filter User Profiles by Age Range

# This is a more complex query that might involve calculating ages based on DateOfBirth and filtering based on a specified age range.


CREATE PROCEDURE spFilterUserProfilesByAge
@MinAge INT, @MaxAge INT
AS
BEGIN
    DECLARE @CurrentDate DATE = GETDATE()
    SELECT ProfileID, UserID, FirstName, LastName, DateOfBirth
    FROM tblUserProfiles
    WHERE (DATEDIFF(YEAR, DateOfBirth, @CurrentDate) - CASE WHEN (MONTH(DateOfBirth) > MONTH(@CurrentDate)) OR (MONTH(DateOfBirth) = MONTH(@CurrentDate) AND DAY(DateOfBirth) > DAY(@CurrentDate)) THEN 1 ELSE 0 END) BETWEEN @MinAge AND @MaxAge
END

## Additional Useful Stored Procedures

## Filter Series by Genre

# This stored procedure filters series based on genre, useful for users searching for series within specific genres.


CREATE PROCEDURE spFilterSeriesByGenre
@GenreID INT
AS
BEGIN
    SELECT SeriesID, Title, StartYear, EndYear, Country
    FROM tblSeries
    WHERE GenreID = @GenreID
END

## Filter Movies by Parent Movie

# Useful for finding all sequels or related movies based on the original (parent) movie.


CREATE PROCEDURE spFilterMoviesByParent
@ParentMovieID INT
AS
BEGIN
    SELECT MovieID, Title, Description, GenreID, Director, ReleaseDate, Duration
    FROM tblMovies
    WHERE ParentMovieID = @ParentMovieID
END

## List Recent Transactions

# This stored procedure retrieves recent transactions, helpful for real-time monitoring or recent activity dashboards.


CREATE PROCEDURE spListRecentTransactions
@RecentDays INT
AS
BEGIN
    SELECT TransactionID, UserID, Amount, TransactionDate, PaymentMethod, Status
    FROM tblTransactions
    WHERE TransactionDate >= DATEADD(DAY, -@RecentDays, GETDATE())
END
## List Active Downloads

# Retrieve ongoing or recently completed downloads, which can be essential for monitoring current server load or user activity.


CREATE PROCEDURE spListActiveDownloads
@HoursAgo INT
AS
BEGIN
    SELECT DownloadID, UserID, MovieID, SeriesID, DownloadDate, ContentType, DownloadQuality, Status, FileSize
    FROM tblDownloads
    WHERE DownloadDate >= DATEADD(HOUR, -@HoursAgo, GETDATE()) AND Status IN ('InProgress', 'Completed')
END
## Get Movies with Specific Duration Range

# Retrieve movies that fall within a specific duration range, useful for users who have time constraints.


CREATE PROCEDURE spGetMoviesByDuration
@MinDuration INT, @MaxDuration INT
AS
BEGIN
    SELECT MovieID, Title, Description, GenreID, Director, ReleaseDate, Duration
    FROM tblMovies
    WHERE Duration BETWEEN @MinDuration AND @MaxDuration
END

## Find User by Username

# Quickly retrieve user details based on the username, frequently used in user login processes.


CREATE PROCEDURE spFindUserByUsername
@Username VARCHAR(255)
AS
BEGIN
    SELECT UserID, Username, Email, Role
    FROM tblUsers
    WHERE Username = @Username
END

## Advanced and Specialized Stored Procedures

## Aggregate Statistics for Movies by Genre

# This procedure computes aggregate statistics like average duration and count of movies per genre, which can be useful for analytics dashboards or reporting.


CREATE PROCEDURE spAggregateMoviesByGenre
AS
BEGIN
    SELECT g.Name AS Genre, COUNT(m.MovieID) AS TotalMovies, AVG(m.Duration) AS AverageDuration
    FROM tblMovies m
    JOIN tblGenres g ON m.GenreID = g.GenreID
    GROUP BY g.Name
END

## List Series With Most Episodes

# Retrieve the series with the most episodes, which can help highlight popular or extensive series in your application.


CREATE PROCEDURE spListSeriesWithMostEpisodes
AS
BEGIN
    SELECT TOP 5 s.SeriesID, s.Title, COUNT(e.EpisodeID) AS EpisodeCount
    FROM tblSeries s
    JOIN tblEpisodes e ON s.SeriesID = e.SeriesID
    GROUP BY s.SeriesID, s.Title
    ORDER BY EpisodeCount DESC
END

## Retrieve Transactions for Audit

# A procedure to retrieve transactions within a specific date range for auditing purposes, including detailed filtering capabilities.


CREATE PROCEDURE spRetrieveTransactionsForAudit
@StartDate DATE, @EndDate DATE
AS
BEGIN
    SELECT TransactionID, UserID, Amount, TransactionDate, PaymentMethod, Status
    FROM tblTransactions
    WHERE TransactionDate BETWEEN @StartDate AND @EndDate
    ORDER BY TransactionDate DESC
END

## Dynamic Search Across Multiple Tables

# A procedure that allows for dynamic searching across multiple key fields in the Users, Movies, and Series tables, useful for global search functionalities.


CREATE PROCEDURE spDynamicSearch
@SearchTerm VARCHAR(255)
AS
BEGIN
    SELECT 'User' AS Type, UserID AS ID, Username AS Name
    FROM tblUsers
    WHERE Username LIKE '%' + @SearchTerm + '%'
    UNION ALL
    SELECT 'Movie', MovieID, Title
    FROM tblMovies
    WHERE Title LIKE '%' + @SearchTerm + '%'
    UNION ALL
    SELECT 'Series', SeriesID, Title
    FROM tblSeries
    WHERE Title LIKE '%' + @SearchTerm + '%'
END

## Comprehensive User Details Report

# Generate a comprehensive report for a user that includes profile details, downloads, and transactions.


CREATE PROCEDURE spGenerateUserReport
@UserID INT
AS
BEGIN
    SELECT u.UserID, u.Username, u.Email, up.FirstName, up.LastName, up.DateOfBirth
    FROM tblUsers u
    JOIN tblUserProfiles up ON u.UserID = up.UserID
    WHERE u.UserID = @UserID;
    SELECT d.DownloadID, d.DownloadDate, d.ContentType, d.DownloadQuality, d.Status, d.FileSize
    FROM tblDownloads d
    WHERE d.UserID = @UserID;
    SELECT t.TransactionID, t.Amount, t.TransactionDate, t.PaymentMethod, t.Status
    FROM tblTransactions t
    WHERE t.UserID = @UserID;
END



# Database ERD Diagram:
![ShowStream ERD diagram (1)](https://github.com/madadiuk/ShowStreamDockerized/assets/24778272/65ffd793-bef9-43b2-aad5-fcb1c8b4ecda)


Updated ERD Description
tblUsers

Purpose: This table stores essential information about all users, including credentials and roles.
Details:
UserID: Unique identifier for each user, primary key.
Username: User's chosen username, must be unique.
Email: User's email address, must be unique.
Password: User's password, stored securely.
Role: User's role (e.g., admin, regular user).
tblUserProfiles

Purpose: Stores additional personal information about users.
Relationship: Each profile is linked directly to a user in tblUsers.
Details:
ProfileID: Unique identifier for the user profile, primary key.
FirstName, LastName, DateOfBirth: Personal details of the user.
tblTransactions

Purpose: Records financial transactions made by users.
Relationship: Linked to tblUsers, indicating which user made each transaction.
Details:
TransactionID: Unique identifier for each transaction.
Amount, TransactionDate: Financial details and date of transaction.
PaymentMethod, Status: Method used (e.g., PayPal, credit card) and current status of the transaction.
tblDownloads

Purpose: Tracks downloads initiated by users.
Relationship: Linked to tblUsers, tblMovies, and tblSeries, showing what content the user downloaded.
Details:
DownloadID: Unique identifier for each download.
DownloadDate, ContentType, DownloadQuality, Status, FileSize: Details about the download such as type, quality, status, and size.
tblGenres

Purpose: Defines different genres for categorization of movies and series.
Details:
GenreID: Unique identifier for each genre.
Name, Description: Name and description of each genre.
tblMovies

Purpose: Stores details about movies available for streaming and downloading.
Relationship: Linked to tblGenres for genre classification and optionally to other movies as sequels/prequels.
Details:
MovieID: Unique identifier for each movie.
Title, Description, Director, ReleaseDate, Duration: Various details about the movie.
ParentMovieID: Links to another movie ID if this movie is a sequel or related.
tblSeries

Purpose: Contains information about TV series.
Relationship: Linked to tblGenres for genre classification.
Details:
SeriesID: Unique identifier for each series.
Title, StartYear, EndYear, Country: Basic information about the series.
tblEpisodes

Purpose: Details individual episodes within a series.
Relationship: Linked to tblSeries, indicating which series each episode belongs to.
Details:
EpisodeID: Unique identifier for each episode.
SeasonNumber, EpisodeNumber, Title, Description, ReleaseDate: Specific details about each episode.
tblVideoFiles

Purpose: Stores video files associated with movies, series, or specific episodes.
Relationship: Can be linked to tblMovies, tblSeries, or tblEpisodes, depending on the video file's content type.
Details:
VideoFileID: Unique identifier for each video file.
VideoQuality, FilePath, FileSize: Quality, storage path, and size of the video file.


## Updated Individual Specifications

Allocated Component: User Management System

# Developer: Abir Shoukat (Student A)
Overview:
Responsible for managing user accounts, profiles, authentication, and roles. This component ensures users can register, log in, and manage their personal information securely.
Table Design:
tblUsers: Stores user credentials and roles.
tblUserProfiles: Holds detailed user information like names and birthdates.
New Responsibilities: Ensure that the unique constraints on Username and Email are enforced and that the security measures for storing passwords are up to date.
Allocated Component: 


## Content Management System

# Developer: Md Abdus Salam Rafee (Student B)
Overview:
Handles all aspects of content within ShowStream, including movie and series metadata, genres, content categorization, and detailed episode management. Enables administrators to add, update, or remove content from the platform.
Table Design:
tblMovies: Includes movie details and references to genres, also manages movie sequels or prequels through ParentMovieID.
tblSeries: Stores series information and genre classification.
tblGenres: A reference table for movie and series genres.
tblEpisodes: Manages detailed episode information within series, linking episodes to their respective series.
New Responsibilities: Implement functionality for managing episodes within series and ensure that the relationships between movies, series, and genres are maintained properly.
Allocated Component: 


## Download and Transaction Management

# Developer: Mo Madadi (Student C)
Overview:
The Download and Transaction Management component is tasked with tracking and managing user interactions concerning content acquisition. It handles the logistics of content downloads, including tracking download status and file sizes, as well as processing user transactions for any purchases or subscriptions within the platform.
Table Design:
tblDownloads: Manages the downloading of movies and series, tracking the user, the content, download quality, and status.
tblTransactions: Tracks financial transactions such as purchases or subscriptions made by users.
tblVideoFiles: Manages the actual video files associated with movies, series, or specific episodes, including their quality, file path, and size.
New Responsibilities: Implement and manage the new tblVideoFiles table, ensuring that video files are properly linked to their corresponding movies, series, or episodes and handle the complexities of multiple formats and qualities for downloads.
Additional Notes
Integration Tasks: Ensure integration between components, particularly where tables overlap in responsibilities. For instance, content added or modified in the Content Management System must be correctly referenced in the Download and Transaction Management system.
Security and Data Integrity: Each developer must ensure that data integrity is maintained in their components, especially where foreign keys and relationships are involved.
Collaborative Testing: Coordinate testing across components to ensure that the system functions cohesively as a whole. This includes testing end-to-end workflows from user registration (handled by Student A) through content browsing and management (handled by Student B) to content downloading and transaction processing (handled by Student C).