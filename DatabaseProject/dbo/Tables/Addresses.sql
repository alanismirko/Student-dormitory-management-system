CREATE TABLE [dbo].[Addresses]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Street] NVARCHAR(50) NOT NULL, 
    [StreetNumber] NVARCHAR(50) NOT NULL, 
    [City] NVARCHAR(50) NOT NULL, 
    [PostlaCode] NVARCHAR(50) NOT NULL, 
    [Country] NVARCHAR(50) NOT NULL

)
