IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [MeterReadings] (
    [Id] int NOT NULL IDENTITY,
    [AccountId] int NOT NULL,
    [MeterReadingDateTime] datetime2 NOT NULL,
    [MeterReadValue] nvarchar(5) NOT NULL,
    CONSTRAINT [PK_MeterReadings] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210906130836_CreateMeterReadingTable', N'5.0.9');
GO

COMMIT;
GO


