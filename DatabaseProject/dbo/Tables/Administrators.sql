CREATE TABLE [dbo].[Administrators] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [Email]          NVARCHAR (256) NOT NULL,
    [Password]         NVARCHAR (256) NOT NULL,
    [EmployeeNumber] NVARCHAR (50)  NOT NULL,
    [FirstName]      NVARCHAR (50)  NOT NULL,
    [LastName]       NVARCHAR (50)  NOT NULL,
    [PhoneNumber]    NVARCHAR (15)  NOT NULL,
    [ModifiedDate] DATETIME2 NOT NULL DEFAULT CURRENT_TIMESTAMP, 
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

GO
CREATE TRIGGER [dbo].[Trigger_Administrators_UpdateModifiedDate]
ON dbo.Administrators
AFTER UPDATE
AS
UPDATE dbo.Administrators
SET ModifiedDate = CURRENT_TIMESTAMP
WHERE Id IN (SELECT DISTINCT Id FROM inserted);