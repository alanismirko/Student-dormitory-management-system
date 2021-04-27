CREATE TABLE [dbo].[Bookings]
(
	[Id] INT IDENTITY PRIMARY KEY NOT NULL,
	[CreationDate] DATETIME2 NOT NULL DEFAULT getutcdate(), 
    [MoveInDate] DATE NOT NULL, 
    [MoveOutDate] DATE NOT NULL, 
    [Status] INT NOT NULL,
    [RoomId] INT, 
    CONSTRAINT [FK_Bookings_Rooms] FOREIGN KEY ([RoomId]) REFERENCES [Rooms]([Id])  

)
