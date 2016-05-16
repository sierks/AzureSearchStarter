USE [codecamp]
GO

DROP USER [codecampguest]
GO

CREATE USER [codecampguest]
FOR LOGIN [codecampguest]
WITH DEFAULT_SCHEMA = [dbo]
GO

ALTER ROLE db_owner ADD MEMBER [codecampguest]
GO
