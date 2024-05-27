CREATE TABLE [dbo].[tblUsers] (
    [UserID]   INT           IDENTITY (1, 1) NOT NULL,
    [Username] VARCHAR (255) NOT NULL,
    [Email]    VARCHAR (255) NOT NULL,
    [Password] VARCHAR (255) NOT NULL,
    [Role]     VARCHAR (50)  NOT NULL,
    PRIMARY KEY CLUSTERED ([UserID] ASC),
    UNIQUE NONCLUSTERED ([Username] ASC),
    UNIQUE NONCLUSTERED ([Email] ASC)  -- Added unique constraint on Email to prevent duplicate emails
);


