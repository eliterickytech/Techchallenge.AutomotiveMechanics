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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231023233823_Inicio')
BEGIN
    CREATE TABLE [Manufacturer] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(50) NOT NULL,
        [CreatedDate] datetime NOT NULL DEFAULT ((getdate())),
        [Enabled] bit NOT NULL DEFAULT ((1)),
        [LastModifiedDate] datetime2 NULL,
        CONSTRAINT [PK_Manufacturer] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231023233823_Inicio')
BEGIN
    CREATE TABLE [User] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(60) NOT NULL,
        [Email] nvarchar(80) NOT NULL,
        [PasswordHash] varbinary(max) NULL,
        [PasswordSalt] varbinary(max) NULL,
        [CreatedDate] datetime NOT NULL DEFAULT ((getdate())),
        [Enabled] bit NOT NULL DEFAULT ((1)),
        [LastModifiedDate] datetime2 NULL,
        CONSTRAINT [PK_User] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231023233823_Inicio')
BEGIN
    CREATE TABLE [Model] (
        [Id] int NOT NULL IDENTITY,
        [ManufacturerId] int NOT NULL,
        [Name] nvarchar(50) NOT NULL,
        [CreatedDate] datetime NOT NULL DEFAULT ((getdate())),
        [Enabled] bit NOT NULL DEFAULT ((1)),
        [LastModifiedDate] datetime2 NULL,
        CONSTRAINT [PK_Model] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Model_Manufacturer] FOREIGN KEY ([ManufacturerId]) REFERENCES [Manufacturer] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231023233823_Inicio')
BEGIN
    CREATE TABLE [Car] (
        [Id] int NOT NULL IDENTITY,
        [ModelId] int NOT NULL,
        [YearManufactured] int NOT NULL,
        [Plate] nvarchar(10) NOT NULL,
        [CreatedDate] datetime2 NOT NULL DEFAULT ((getdate())),
        [Enabled] bit NOT NULL DEFAULT ((1)),
        [LastModifiedDate] datetime2 NULL,
        CONSTRAINT [PK_Car] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Car_Model] FOREIGN KEY ([ModelId]) REFERENCES [Model] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231023233823_Inicio')
BEGIN
    CREATE TABLE [Service] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(100) NOT NULL,
        [CarId] int NOT NULL,
        [CreatedDate] datetime NOT NULL DEFAULT ((getdate())),
        [Enabled] bit NOT NULL DEFAULT ((1)),
        [LastModifiedDate] datetime2 NULL,
        CONSTRAINT [PK_Service] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Service_Car_CarId] FOREIGN KEY ([CarId]) REFERENCES [Car] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231023233823_Inicio')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'LastModifiedDate', N'Name') AND [object_id] = OBJECT_ID(N'[Manufacturer]'))
        SET IDENTITY_INSERT [Manufacturer] ON;
    EXEC(N'INSERT INTO [Manufacturer] ([Id], [LastModifiedDate], [Name])
    VALUES (1, NULL, N''BMW''),
    (2, NULL, N''VW''),
    (3, NULL, N''Hyundai'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'LastModifiedDate', N'Name') AND [object_id] = OBJECT_ID(N'[Manufacturer]'))
        SET IDENTITY_INSERT [Manufacturer] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231023233823_Inicio')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'LastModifiedDate', N'ManufacturerId', N'Name') AND [object_id] = OBJECT_ID(N'[Model]'))
        SET IDENTITY_INSERT [Model] ON;
    EXEC(N'INSERT INTO [Model] ([Id], [LastModifiedDate], [ManufacturerId], [Name])
    VALUES (1, NULL, 1, N''X5''),
    (2, NULL, 1, N''X6''),
    (3, NULL, 1, N''X1''),
    (4, NULL, 1, N''X2''),
    (5, NULL, 1, N''X3''),
    (6, NULL, 1, N''320I''),
    (7, NULL, 1, N''330I''),
    (8, NULL, 1, N''M3''),
    (9, NULL, 2, N''Golf''),
    (10, NULL, 2, N''Polo''),
    (11, NULL, 2, N''Passat''),
    (12, NULL, 2, N''Tiguan''),
    (13, NULL, 2, N''Touareg''),
    (14, NULL, 2, N''Arteon''),
    (15, NULL, 2, N''T-Roc''),
    (16, NULL, 2, N''T-Cross''),
    (17, NULL, 2, N''Up''),
    (18, NULL, 2, N''Amarok''),
    (19, NULL, 2, N''Caddy''),
    (20, NULL, 2, N''Transporter''),
    (21, NULL, 3, N''i30''),
    (22, NULL, 3, N''Elantra''),
    (23, NULL, 3, N''Kona''),
    (24, NULL, 3, N''Tucson''),
    (25, NULL, 3, N''Santa Fe''),
    (26, NULL, 3, N''Ioniq''),
    (27, NULL, 3, N''Veloster'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'LastModifiedDate', N'ManufacturerId', N'Name') AND [object_id] = OBJECT_ID(N'[Model]'))
        SET IDENTITY_INSERT [Model] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231023233823_Inicio')
BEGIN
    CREATE INDEX [IX_Car_ModelId] ON [Car] ([ModelId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231023233823_Inicio')
BEGIN
    CREATE INDEX [IX_Model_ManufacturerId] ON [Model] ([ManufacturerId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231023233823_Inicio')
BEGIN
    CREATE INDEX [IX_Service_CarId] ON [Service] ([CarId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231023233823_Inicio')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231023233823_Inicio', N'7.0.11');
END;
GO

COMMIT;
GO

