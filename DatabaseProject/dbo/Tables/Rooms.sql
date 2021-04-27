CREATE TABLE [dbo].[Rooms]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [RoomNumber] INT NOT NULL, 
    [Floor] INT NOT NULL, 
    [Capacity] INT NOT NULL, 
    [Area] FLOAT NOT NULL, 
    [Price] FLOAT NOT NULL,
    [Description]         NVARCHAR (256) NOT NULL,
    [IsAvailable] BIT NOT NULL
)
