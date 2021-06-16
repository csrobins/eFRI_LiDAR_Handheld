
CREATE USER datacollector FROM LOGIN datacollector;
GO
sys.sp_addrolemember @rolename = N'db_datareader', @membername = N'datacollector';
GO
sys.sp_addrolemember @rolename = N'db_datawriter', @membername = N'datacollector';
GO
GRANT EXEC to datacollector;
GO