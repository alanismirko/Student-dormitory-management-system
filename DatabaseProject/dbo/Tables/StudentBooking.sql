CREATE TABLE [dbo].[StudentBooking] (
	[Id] INT PRIMARY KEY IDENTITY NOT NULL,
    [StudentId] INT NOT NULL,
    [BookingId] INT NOT NULL,
    CONSTRAINT [FK_StudentBooking_Students] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students] ([Id]),
    CONSTRAINT [FK_StudentBooking_Booking] FOREIGN KEY ([BookingId]) REFERENCES [dbo].[Bookings] ([Id])
);

