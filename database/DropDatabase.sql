USE [Master]
GO
/****** Drop the whole database ******/
IF EXISTS (SELECT name FROM sys.databases WHERE name = N'Submitter')
DROP DATABASE [Submitter]
GO
