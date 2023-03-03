CREATE DATABASE [Scribble.Authors]

GO

USE [Scribble.Authors]

CREATE TABLE [dbo].[Authors]
(
    [Id]         [UNIQUEIDENTIFIER] NOT NULL PRIMARY KEY DEFAULT NEWID(),
    [IdentityId] [UNIQUEIDENTIFIER] NOT NULL,
    [FirstName]  [NVARCHAR](500)    NOT NULL,
    [LastName]   [NVARCHAR](500)    NULL,
);