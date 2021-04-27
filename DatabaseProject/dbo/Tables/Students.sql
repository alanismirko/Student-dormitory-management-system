CREATE TABLE [dbo].[Students] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [Email]            NVARCHAR (256) NOT NULL,
    [Password]         NVARCHAR (256) NOT NULL,
    [FirstName]        NVARCHAR (50)  NOT NULL,
    [LastName]         NVARCHAR (50)  NOT NULL,
    [PhoneNumber]      NVARCHAR (15)  NOT NULL,
    [DateOfBirth]      DATE           NOT NULL,
    [Nationality]      NVARCHAR (50)  NOT NULL,
    [EducationEndDate] DATE           NOT NULL,
    [AddressId]        INT            ,
    [ModifiedDate] DATETIME2 NOT NULL DEFAULT CURRENT_TIMESTAMP, 
    PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_Students_Addresses] FOREIGN KEY ([AddressId]) REFERENCES [Addresses]([Id])
    
);

GO
CREATE TRIGGER [dbo].[Trigger_Students_UpdateModifiedDate]
ON dbo.Students
AFTER UPDATE
AS
UPDATE dbo.Students
SET ModifiedDate = CURRENT_TIMESTAMP
WHERE Id IN (SELECT DISTINCT Id FROM inserted);
