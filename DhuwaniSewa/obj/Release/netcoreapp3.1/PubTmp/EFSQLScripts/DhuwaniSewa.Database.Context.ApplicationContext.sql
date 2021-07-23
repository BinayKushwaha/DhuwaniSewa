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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210511185246_initDbCreate')
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210511185246_initDbCreate')
BEGIN
    CREATE TABLE [Users] (
        [Id] nvarchar(450) NOT NULL,
        [IsDeleted] bit NOT NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210511185246_initDbCreate')
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210511185246_initDbCreate')
BEGIN
    CREATE TABLE [Roles] (
        [Id] nvarchar(450) NOT NULL,
        [IsDeleted] bit NOT NULL,
        [Description] nvarchar(max) NULL,
        CONSTRAINT [PK_Roles] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Roles_AspNetRoles_Id] FOREIGN KEY ([Id]) REFERENCES [AspNetRoles] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210511185246_initDbCreate')
BEGIN
    CREATE TABLE [AppUsers] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NULL,
        CONSTRAINT [PK_AppUsers] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspUser_ApplicationUser] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210511185246_initDbCreate')
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210511185246_initDbCreate')
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210511185246_initDbCreate')
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210511185246_initDbCreate')
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210511185246_initDbCreate')
BEGIN
    CREATE TABLE [PersonalDetail] (
        [Id] int NOT NULL IDENTITY,
        [FirstName] nvarchar(200) NOT NULL,
        [MiddleName] nvarchar(200) NULL,
        [LastName] nvarchar(250) NOT NULL,
        [AppUserId] int NOT NULL,
        CONSTRAINT [PK_PersonalDetail] PRIMARY KEY ([Id]),
        CONSTRAINT [Fk_ApplicationUser_PersonalDetail] FOREIGN KEY ([AppUserId]) REFERENCES [AppUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210511185246_initDbCreate')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [IX_AppUsers_UserId] ON [AppUsers] ([UserId]) WHERE [UserId] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210511185246_initDbCreate')
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210511185246_initDbCreate')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210511185246_initDbCreate')
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210511185246_initDbCreate')
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210511185246_initDbCreate')
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210511185246_initDbCreate')
BEGIN
    CREATE UNIQUE INDEX [IX_PersonalDetail_AppUserId] ON [PersonalDetail] ([AppUserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210511185246_initDbCreate')
BEGIN
    CREATE INDEX [EmailIndex] ON [Users] ([NormalizedEmail]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210511185246_initDbCreate')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [Users] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210511185246_initDbCreate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210511185246_initDbCreate', N'5.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210513180015_dbUpdateWithNewAndExisting')
BEGIN
    ALTER TABLE [AppUsers] DROP CONSTRAINT [FK_AspUser_ApplicationUser];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210513180015_dbUpdateWithNewAndExisting')
BEGIN
    ALTER TABLE [PersonalDetail] DROP CONSTRAINT [Fk_ApplicationUser_PersonalDetail];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210513180015_dbUpdateWithNewAndExisting')
BEGIN
    DROP INDEX [IX_AppUsers_UserId] ON [AppUsers];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210513180015_dbUpdateWithNewAndExisting')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'IsDeleted');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Users] DROP COLUMN [IsDeleted];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210513180015_dbUpdateWithNewAndExisting')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Roles]') AND [c].[name] = N'IsDeleted');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Roles] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Roles] DROP COLUMN [IsDeleted];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210513180015_dbUpdateWithNewAndExisting')
BEGIN
    ALTER TABLE [Users] ADD [IsActive] bit NOT NULL DEFAULT CAST(1 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210513180015_dbUpdateWithNewAndExisting')
BEGIN
    ALTER TABLE [Roles] ADD [IsActive] bit NOT NULL DEFAULT CAST(1 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210513180015_dbUpdateWithNewAndExisting')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AppUsers]') AND [c].[name] = N'UserId');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [AppUsers] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [AppUsers] ALTER COLUMN [UserId] nvarchar(450) NOT NULL;
    ALTER TABLE [AppUsers] ADD DEFAULT N'' FOR [UserId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210513180015_dbUpdateWithNewAndExisting')
BEGIN
    ALTER TABLE [AppUsers] ADD [IsCompnay] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210513180015_dbUpdateWithNewAndExisting')
BEGIN
    ALTER TABLE [AppUsers] ADD [IsEmployee] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210513180015_dbUpdateWithNewAndExisting')
BEGIN
    CREATE TABLE [CompanyDetail] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(250) NOT NULL,
        [AppUserId] int NOT NULL,
        CONSTRAINT [PK_CompanyDetail] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Companydetail_To_AppUsers] FOREIGN KEY ([AppUserId]) REFERENCES [AppUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210513180015_dbUpdateWithNewAndExisting')
BEGIN
    CREATE TABLE [Customer] (
        [Id] int NOT NULL IDENTITY,
        [AppUserId] int NOT NULL,
        [IsActive] bit NOT NULL DEFAULT CAST(1 AS bit),
        CONSTRAINT [PK_Customer] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Customer_To_AppUser] FOREIGN KEY ([AppUserId]) REFERENCES [AppUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210513180015_dbUpdateWithNewAndExisting')
BEGIN
    CREATE TABLE [Employee] (
        [Id] int NOT NULL IDENTITY,
        [Desigination] nvarchar(max) NOT NULL,
        [AppUserId] int NOT NULL,
        [IsActive] bit NOT NULL DEFAULT CAST(1 AS bit),
        CONSTRAINT [PK_Employee] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Employee_To_AppUser] FOREIGN KEY ([AppUserId]) REFERENCES [AppUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210513180015_dbUpdateWithNewAndExisting')
BEGIN
    CREATE UNIQUE INDEX [IX_AppUsers_UserId] ON [AppUsers] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210513180015_dbUpdateWithNewAndExisting')
BEGIN
    CREATE UNIQUE INDEX [IX_CompanyDetail_AppUserId] ON [CompanyDetail] ([AppUserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210513180015_dbUpdateWithNewAndExisting')
BEGIN
    CREATE UNIQUE INDEX [IX_Customer_AppUserId] ON [Customer] ([AppUserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210513180015_dbUpdateWithNewAndExisting')
BEGIN
    CREATE UNIQUE INDEX [IX_Employee_AppUserId] ON [Employee] ([AppUserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210513180015_dbUpdateWithNewAndExisting')
BEGIN
    ALTER TABLE [AppUsers] ADD CONSTRAINT [FK_AspUser_ApplicationUser] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210513180015_dbUpdateWithNewAndExisting')
BEGIN
    ALTER TABLE [PersonalDetail] ADD CONSTRAINT [FK_PersonalDetail_To_AppUsers] FOREIGN KEY ([AppUserId]) REFERENCES [AppUsers] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210513180015_dbUpdateWithNewAndExisting')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210513180015_dbUpdateWithNewAndExisting', N'5.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210517174243_cascade delete on user and app user relation modeified to restrict')
BEGIN
    ALTER TABLE [AppUsers] DROP CONSTRAINT [FK_AspUser_ApplicationUser];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210517174243_cascade delete on user and app user relation modeified to restrict')
BEGIN
    ALTER TABLE [PersonalDetail] DROP CONSTRAINT [PK_PersonalDetail];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210517174243_cascade delete on user and app user relation modeified to restrict')
BEGIN
    ALTER TABLE [Employee] DROP CONSTRAINT [PK_Employee];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210517174243_cascade delete on user and app user relation modeified to restrict')
BEGIN
    ALTER TABLE [Customer] DROP CONSTRAINT [PK_Customer];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210517174243_cascade delete on user and app user relation modeified to restrict')
BEGIN
    ALTER TABLE [CompanyDetail] DROP CONSTRAINT [PK_CompanyDetail];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210517174243_cascade delete on user and app user relation modeified to restrict')
BEGIN
    EXEC sp_rename N'[PersonalDetail]', N'PersonalDetails';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210517174243_cascade delete on user and app user relation modeified to restrict')
BEGIN
    EXEC sp_rename N'[Employee]', N'Employees';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210517174243_cascade delete on user and app user relation modeified to restrict')
BEGIN
    EXEC sp_rename N'[Customer]', N'Customers';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210517174243_cascade delete on user and app user relation modeified to restrict')
BEGIN
    EXEC sp_rename N'[CompanyDetail]', N'CompanyDetails';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210517174243_cascade delete on user and app user relation modeified to restrict')
BEGIN
    EXEC sp_rename N'[PersonalDetails].[IX_PersonalDetail_AppUserId]', N'IX_PersonalDetails_AppUserId', N'INDEX';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210517174243_cascade delete on user and app user relation modeified to restrict')
BEGIN
    EXEC sp_rename N'[Employees].[IX_Employee_AppUserId]', N'IX_Employees_AppUserId', N'INDEX';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210517174243_cascade delete on user and app user relation modeified to restrict')
BEGIN
    EXEC sp_rename N'[Customers].[IX_Customer_AppUserId]', N'IX_Customers_AppUserId', N'INDEX';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210517174243_cascade delete on user and app user relation modeified to restrict')
BEGIN
    EXEC sp_rename N'[CompanyDetails].[IX_CompanyDetail_AppUserId]', N'IX_CompanyDetails_AppUserId', N'INDEX';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210517174243_cascade delete on user and app user relation modeified to restrict')
BEGIN
    ALTER TABLE [PersonalDetails] ADD CONSTRAINT [PK_PersonalDetails] PRIMARY KEY ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210517174243_cascade delete on user and app user relation modeified to restrict')
BEGIN
    ALTER TABLE [Employees] ADD CONSTRAINT [PK_Employees] PRIMARY KEY ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210517174243_cascade delete on user and app user relation modeified to restrict')
BEGIN
    ALTER TABLE [Customers] ADD CONSTRAINT [PK_Customers] PRIMARY KEY ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210517174243_cascade delete on user and app user relation modeified to restrict')
BEGIN
    ALTER TABLE [CompanyDetails] ADD CONSTRAINT [PK_CompanyDetails] PRIMARY KEY ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210517174243_cascade delete on user and app user relation modeified to restrict')
BEGIN
    ALTER TABLE [AppUsers] ADD CONSTRAINT [FK_AspUser_ApplicationUser] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210517174243_cascade delete on user and app user relation modeified to restrict')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210517174243_cascade delete on user and app user relation modeified to restrict', N'5.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210522165341_refresh token entity is added')
BEGIN
    CREATE TABLE [RefreshToken] (
        [Token] nvarchar(450) NOT NULL,
        [JwtId] nvarchar(max) NOT NULL,
        [UserId] nvarchar(450) NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        [ExpiryDate] datetime2 NOT NULL,
        CONSTRAINT [PK_RefreshToken] PRIMARY KEY ([Token]),
        CONSTRAINT [FK_RefreshToken_User] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210522165341_refresh token entity is added')
BEGIN
    CREATE UNIQUE INDEX [IX_RefreshToken_UserId] ON [RefreshToken] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210522165341_refresh token entity is added')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210522165341_refresh token entity is added', N'5.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708074102_Added entity like serviceProvider,ServiceSeeker and Others')
BEGIN
    EXEC sp_rename N'[Employees].[AppUserId]', N'UserId', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708074102_Added entity like serviceProvider,ServiceSeeker and Others')
BEGIN
    EXEC sp_rename N'[Employees].[IX_Employees_AppUserId]', N'IX_Employees_UserId', N'INDEX';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708074102_Added entity like serviceProvider,ServiceSeeker and Others')
BEGIN
    EXEC sp_rename N'[Customers].[AppUserId]', N'UserId', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708074102_Added entity like serviceProvider,ServiceSeeker and Others')
BEGIN
    EXEC sp_rename N'[Customers].[IX_Customers_AppUserId]', N'IX_Customers_UserId', N'INDEX';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708074102_Added entity like serviceProvider,ServiceSeeker and Others')
BEGIN
    ALTER TABLE [Employees] ADD [CreatedBy] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708074102_Added entity like serviceProvider,ServiceSeeker and Others')
BEGIN
    ALTER TABLE [Employees] ADD [DhuwaniSewaId] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708074102_Added entity like serviceProvider,ServiceSeeker and Others')
BEGIN
    ALTER TABLE [Employees] ADD [ModifiedBy] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708074102_Added entity like serviceProvider,ServiceSeeker and Others')
BEGIN
    ALTER TABLE [Employees] ADD [ModifiedDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708074102_Added entity like serviceProvider,ServiceSeeker and Others')
BEGIN
    ALTER TABLE [AppUsers] ADD [Active] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708074102_Added entity like serviceProvider,ServiceSeeker and Others')
BEGIN
    ALTER TABLE [AppUsers] ADD [CreatedBy] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708074102_Added entity like serviceProvider,ServiceSeeker and Others')
BEGIN
    ALTER TABLE [AppUsers] ADD [IsServiceProvider] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708074102_Added entity like serviceProvider,ServiceSeeker and Others')
BEGIN
    ALTER TABLE [AppUsers] ADD [ModifiedBy] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708074102_Added entity like serviceProvider,ServiceSeeker and Others')
BEGIN
    ALTER TABLE [AppUsers] ADD [ModifiedDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708074102_Added entity like serviceProvider,ServiceSeeker and Others')
BEGIN
    CREATE TABLE [Category] (
        [Id] int NOT NULL IDENTITY,
        [Active] bit NOT NULL DEFAULT CAST(1 AS bit),
        [Enum] nvarchar(150) NULL,
        [DisplayName] nvarchar(250) NOT NULL,
        [Description] nvarchar(max) NULL,
        [CreatedBy] int NOT NULL,
        [ModifiedDate] datetime2 NOT NULL,
        [ModifiedBy] int NOT NULL,
        CONSTRAINT [PK_Category] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708074102_Added entity like serviceProvider,ServiceSeeker and Others')
BEGIN
    CREATE TABLE [ContactDetail] (
        [Id] int NOT NULL IDENTITY,
        [ContactNumber] nvarchar(max) NULL,
        [Email] nvarchar(max) NULL,
        CONSTRAINT [PK_ContactDetail] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708074102_Added entity like serviceProvider,ServiceSeeker and Others')
BEGIN
    CREATE TABLE [DocumentDetail] (
        [Id] int NOT NULL IDENTITY,
        [Type] nvarchar(50) NOT NULL,
        [Number] nvarchar(100) NOT NULL,
        [IssuedDistrict] nvarchar(100) NULL,
        CONSTRAINT [PK_DocumentDetail] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708074102_Added entity like serviceProvider,ServiceSeeker and Others')
BEGIN
    CREATE TABLE [SerialNumbers] (
        [Id] int NOT NULL IDENTITY,
        [ServiceProvider] int NOT NULL,
        [ServiceSeeker] int NOT NULL,
        [Employee] int NOT NULL,
        CONSTRAINT [PK_SerialNumbers] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708074102_Added entity like serviceProvider,ServiceSeeker and Others')
BEGIN
    CREATE TABLE [ServiceProvider] (
        [Id] int NOT NULL IDENTITY,
        [Active] bit NOT NULL DEFAULT CAST(1 AS bit),
        [DetailsCorrectAgreed] bit NOT NULL,
        [UserId] int NOT NULL,
        [DhuwaniSewaId] nvarchar(250) NOT NULL,
        [CreatedBy] int NOT NULL,
        [ModifiedDate] datetime2 NOT NULL,
        [ModifiedBy] int NOT NULL,
        CONSTRAINT [PK_ServiceProvider] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ServiceProvider_AppUser] FOREIGN KEY ([UserId]) REFERENCES [AppUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708074102_Added entity like serviceProvider,ServiceSeeker and Others')
BEGIN
    CREATE TABLE [ServiceSeeker] (
        [Id] int NOT NULL IDENTITY,
        [Active] bit NOT NULL DEFAULT CAST(1 AS bit),
        [DetailsCorrectAgreed] bit NOT NULL,
        [UserId] int NOT NULL,
        [DhuwaniSewaId] nvarchar(250) NOT NULL,
        [CreatedBy] int NOT NULL,
        [ModifiedDate] datetime2 NOT NULL,
        [ModifiedBy] int NOT NULL,
        CONSTRAINT [PK_ServiceSeeker] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ServiceSeeker_AppUser] FOREIGN KEY ([UserId]) REFERENCES [AppUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708074102_Added entity like serviceProvider,ServiceSeeker and Others')
BEGIN
    CREATE TABLE [Choice] (
        [Id] int NOT NULL IDENTITY,
        [Active] bit NOT NULL DEFAULT CAST(1 AS bit),
        [Enum] nvarchar(150) NULL,
        [DisplayName] nvarchar(250) NOT NULL,
        [CategoryId] int NOT NULL,
        [CreatedBy] int NOT NULL,
        [ModifiedDate] datetime2 NOT NULL,
        [ModifiedBy] int NOT NULL,
        CONSTRAINT [PK_Choice] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Category_Choice] FOREIGN KEY ([CategoryId]) REFERENCES [Category] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708074102_Added entity like serviceProvider,ServiceSeeker and Others')
BEGIN
    CREATE TABLE [PersonalDetailContactDetail] (
        [PersonalDetailId] int NOT NULL,
        [ContactDetailId] int NOT NULL,
        CONSTRAINT [PK_PersonalDetailContactDetail] PRIMARY KEY ([PersonalDetailId], [ContactDetailId]),
        CONSTRAINT [FK_ConactDetail_PersonalDetailContactDetail] FOREIGN KEY ([ContactDetailId]) REFERENCES [ContactDetail] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_PersonalDetail_PersonalDetailContactDetail] FOREIGN KEY ([PersonalDetailId]) REFERENCES [PersonalDetails] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708074102_Added entity like serviceProvider,ServiceSeeker and Others')
BEGIN
    CREATE TABLE [PersonalDetailDocumentDetail] (
        [PersonDetailId] int NOT NULL,
        [DocumentDetailId] int NOT NULL,
        CONSTRAINT [PK_PersonalDetailDocumentDetail] PRIMARY KEY ([PersonDetailId], [DocumentDetailId]),
        CONSTRAINT [FK_DocumentDetail_PersonalDetailDocumentDetail] FOREIGN KEY ([DocumentDetailId]) REFERENCES [DocumentDetail] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_PersonalDetail_DocumenDetail] FOREIGN KEY ([PersonDetailId]) REFERENCES [PersonalDetails] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708074102_Added entity like serviceProvider,ServiceSeeker and Others')
BEGIN
    CREATE TABLE [VehicleDetail] (
        [Id] int NOT NULL IDENTITY,
        [TypeId] int NOT NULL,
        [BrandId] int NOT NULL,
        [RegistrationNumber] nvarchar(100) NOT NULL,
        CONSTRAINT [PK_VehicleDetail] PRIMARY KEY ([Id]),
        CONSTRAINT [Fk_VehicleDetail_Brand_Choice] FOREIGN KEY ([BrandId]) REFERENCES [Choice] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_VehicleDetail_Type_Choice] FOREIGN KEY ([TypeId]) REFERENCES [Choice] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708074102_Added entity like serviceProvider,ServiceSeeker and Others')
BEGIN
    CREATE TABLE [ServiceProviderVehicleDetail] (
        [ServiceProviderId] int NOT NULL,
        [VehicleDetailId] int NOT NULL,
        [NumberOfVehicle] int NOT NULL,
        CONSTRAINT [PK_ServiceProviderVehicleDetail] PRIMARY KEY ([ServiceProviderId], [VehicleDetailId]),
        CONSTRAINT [FK_SericeProvider_ServiceProviderVehicleDetail] FOREIGN KEY ([ServiceProviderId]) REFERENCES [ServiceProvider] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ServiceProviderVehicleDetail_VehicleDetail] FOREIGN KEY ([VehicleDetailId]) REFERENCES [VehicleDetail] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708074102_Added entity like serviceProvider,ServiceSeeker and Others')
BEGIN
    CREATE INDEX [IX_Choice_CategoryId] ON [Choice] ([CategoryId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708074102_Added entity like serviceProvider,ServiceSeeker and Others')
BEGIN
    CREATE INDEX [IX_PersonalDetailContactDetail_ContactDetailId] ON [PersonalDetailContactDetail] ([ContactDetailId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708074102_Added entity like serviceProvider,ServiceSeeker and Others')
BEGIN
    CREATE INDEX [IX_PersonalDetailDocumentDetail_DocumentDetailId] ON [PersonalDetailDocumentDetail] ([DocumentDetailId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708074102_Added entity like serviceProvider,ServiceSeeker and Others')
BEGIN
    CREATE UNIQUE INDEX [IX_ServiceProvider_UserId] ON [ServiceProvider] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708074102_Added entity like serviceProvider,ServiceSeeker and Others')
BEGIN
    CREATE UNIQUE INDEX [IX_ServiceProviderVehicleDetail_VehicleDetailId] ON [ServiceProviderVehicleDetail] ([VehicleDetailId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708074102_Added entity like serviceProvider,ServiceSeeker and Others')
BEGIN
    CREATE UNIQUE INDEX [IX_ServiceSeeker_UserId] ON [ServiceSeeker] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708074102_Added entity like serviceProvider,ServiceSeeker and Others')
BEGIN
    CREATE INDEX [IX_VehicleDetail_BrandId] ON [VehicleDetail] ([BrandId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708074102_Added entity like serviceProvider,ServiceSeeker and Others')
BEGIN
    CREATE INDEX [IX_VehicleDetail_TypeId] ON [VehicleDetail] ([TypeId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708074102_Added entity like serviceProvider,ServiceSeeker and Others')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210708074102_Added entity like serviceProvider,ServiceSeeker and Others', N'5.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708083932_RecordHistory tbl updated')
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ServiceSeeker]') AND [c].[name] = N'ModifiedDate');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [ServiceSeeker] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [ServiceSeeker] ALTER COLUMN [ModifiedDate] datetime2 NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708083932_RecordHistory tbl updated')
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ServiceSeeker]') AND [c].[name] = N'ModifiedBy');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [ServiceSeeker] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [ServiceSeeker] ALTER COLUMN [ModifiedBy] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708083932_RecordHistory tbl updated')
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ServiceSeeker]') AND [c].[name] = N'CreatedBy');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [ServiceSeeker] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [ServiceSeeker] ALTER COLUMN [CreatedBy] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708083932_RecordHistory tbl updated')
BEGIN
    ALTER TABLE [ServiceSeeker] ADD [CreatedDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708083932_RecordHistory tbl updated')
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ServiceProvider]') AND [c].[name] = N'ModifiedDate');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [ServiceProvider] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [ServiceProvider] ALTER COLUMN [ModifiedDate] datetime2 NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708083932_RecordHistory tbl updated')
BEGIN
    DECLARE @var7 sysname;
    SELECT @var7 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ServiceProvider]') AND [c].[name] = N'ModifiedBy');
    IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [ServiceProvider] DROP CONSTRAINT [' + @var7 + '];');
    ALTER TABLE [ServiceProvider] ALTER COLUMN [ModifiedBy] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708083932_RecordHistory tbl updated')
BEGIN
    DECLARE @var8 sysname;
    SELECT @var8 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ServiceProvider]') AND [c].[name] = N'CreatedBy');
    IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [ServiceProvider] DROP CONSTRAINT [' + @var8 + '];');
    ALTER TABLE [ServiceProvider] ALTER COLUMN [CreatedBy] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708083932_RecordHistory tbl updated')
BEGIN
    ALTER TABLE [ServiceProvider] ADD [CreatedDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708083932_RecordHistory tbl updated')
BEGIN
    DECLARE @var9 sysname;
    SELECT @var9 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Employees]') AND [c].[name] = N'ModifiedDate');
    IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Employees] DROP CONSTRAINT [' + @var9 + '];');
    ALTER TABLE [Employees] ALTER COLUMN [ModifiedDate] datetime2 NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708083932_RecordHistory tbl updated')
BEGIN
    DECLARE @var10 sysname;
    SELECT @var10 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Employees]') AND [c].[name] = N'ModifiedBy');
    IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [Employees] DROP CONSTRAINT [' + @var10 + '];');
    ALTER TABLE [Employees] ALTER COLUMN [ModifiedBy] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708083932_RecordHistory tbl updated')
BEGIN
    DECLARE @var11 sysname;
    SELECT @var11 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Employees]') AND [c].[name] = N'CreatedBy');
    IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [Employees] DROP CONSTRAINT [' + @var11 + '];');
    ALTER TABLE [Employees] ALTER COLUMN [CreatedBy] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708083932_RecordHistory tbl updated')
BEGIN
    ALTER TABLE [Employees] ADD [CreatedDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708083932_RecordHistory tbl updated')
BEGIN
    DECLARE @var12 sysname;
    SELECT @var12 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Choice]') AND [c].[name] = N'ModifiedDate');
    IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [Choice] DROP CONSTRAINT [' + @var12 + '];');
    ALTER TABLE [Choice] ALTER COLUMN [ModifiedDate] datetime2 NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708083932_RecordHistory tbl updated')
BEGIN
    DECLARE @var13 sysname;
    SELECT @var13 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Choice]') AND [c].[name] = N'ModifiedBy');
    IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [Choice] DROP CONSTRAINT [' + @var13 + '];');
    ALTER TABLE [Choice] ALTER COLUMN [ModifiedBy] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708083932_RecordHistory tbl updated')
BEGIN
    DECLARE @var14 sysname;
    SELECT @var14 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Choice]') AND [c].[name] = N'CreatedBy');
    IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [Choice] DROP CONSTRAINT [' + @var14 + '];');
    ALTER TABLE [Choice] ALTER COLUMN [CreatedBy] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708083932_RecordHistory tbl updated')
BEGIN
    ALTER TABLE [Choice] ADD [CreatedDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708083932_RecordHistory tbl updated')
BEGIN
    DECLARE @var15 sysname;
    SELECT @var15 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Category]') AND [c].[name] = N'ModifiedDate');
    IF @var15 IS NOT NULL EXEC(N'ALTER TABLE [Category] DROP CONSTRAINT [' + @var15 + '];');
    ALTER TABLE [Category] ALTER COLUMN [ModifiedDate] datetime2 NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708083932_RecordHistory tbl updated')
BEGIN
    DECLARE @var16 sysname;
    SELECT @var16 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Category]') AND [c].[name] = N'ModifiedBy');
    IF @var16 IS NOT NULL EXEC(N'ALTER TABLE [Category] DROP CONSTRAINT [' + @var16 + '];');
    ALTER TABLE [Category] ALTER COLUMN [ModifiedBy] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708083932_RecordHistory tbl updated')
BEGIN
    DECLARE @var17 sysname;
    SELECT @var17 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Category]') AND [c].[name] = N'CreatedBy');
    IF @var17 IS NOT NULL EXEC(N'ALTER TABLE [Category] DROP CONSTRAINT [' + @var17 + '];');
    ALTER TABLE [Category] ALTER COLUMN [CreatedBy] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708083932_RecordHistory tbl updated')
BEGIN
    ALTER TABLE [Category] ADD [CreatedDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708083932_RecordHistory tbl updated')
BEGIN
    DECLARE @var18 sysname;
    SELECT @var18 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AppUsers]') AND [c].[name] = N'ModifiedDate');
    IF @var18 IS NOT NULL EXEC(N'ALTER TABLE [AppUsers] DROP CONSTRAINT [' + @var18 + '];');
    ALTER TABLE [AppUsers] ALTER COLUMN [ModifiedDate] datetime2 NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708083932_RecordHistory tbl updated')
BEGIN
    DECLARE @var19 sysname;
    SELECT @var19 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AppUsers]') AND [c].[name] = N'ModifiedBy');
    IF @var19 IS NOT NULL EXEC(N'ALTER TABLE [AppUsers] DROP CONSTRAINT [' + @var19 + '];');
    ALTER TABLE [AppUsers] ALTER COLUMN [ModifiedBy] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708083932_RecordHistory tbl updated')
BEGIN
    DECLARE @var20 sysname;
    SELECT @var20 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AppUsers]') AND [c].[name] = N'CreatedBy');
    IF @var20 IS NOT NULL EXEC(N'ALTER TABLE [AppUsers] DROP CONSTRAINT [' + @var20 + '];');
    ALTER TABLE [AppUsers] ALTER COLUMN [CreatedBy] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708083932_RecordHistory tbl updated')
BEGIN
    ALTER TABLE [AppUsers] ADD [CreatedDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708083932_RecordHistory tbl updated')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210708083932_RecordHistory tbl updated', N'5.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708090342_AppUser Tbl updated')
BEGIN
    DECLARE @var21 sysname;
    SELECT @var21 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AppUsers]') AND [c].[name] = N'IsServiceProvider');
    IF @var21 IS NOT NULL EXEC(N'ALTER TABLE [AppUsers] DROP CONSTRAINT [' + @var21 + '];');
    ALTER TABLE [AppUsers] ADD DEFAULT CAST(1 AS bit) FOR [IsServiceProvider];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708090342_AppUser Tbl updated')
BEGIN
    DECLARE @var22 sysname;
    SELECT @var22 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AppUsers]') AND [c].[name] = N'Active');
    IF @var22 IS NOT NULL EXEC(N'ALTER TABLE [AppUsers] DROP CONSTRAINT [' + @var22 + '];');
    ALTER TABLE [AppUsers] ADD DEFAULT CAST(1 AS bit) FOR [Active];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708090342_AppUser Tbl updated')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210708090342_AppUser Tbl updated', N'5.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210709043258_update document number to registration number')
BEGIN
    EXEC sp_rename N'[DocumentDetail].[Number]', N'RegistrationNumber', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210709043258_update document number to registration number')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210709043258_update document number to registration number', N'5.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210709193736_Add Fiscal Year')
BEGIN
    CREATE TABLE [FiscalYear] (
        [FiscalYearId] int NOT NULL IDENTITY,
        [Name] nvarchar(250) NOT NULL,
        [StartDate] datetime2 NOT NULL,
        [EndDate] datetime2 NOT NULL,
        CONSTRAINT [PK_FiscalYear] PRIMARY KEY ([FiscalYearId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210709193736_Add Fiscal Year')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210709193736_Add Fiscal Year', N'5.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210711120927_Modify vehicle detail structure')
BEGIN
    DECLARE @var23 sysname;
    SELECT @var23 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ServiceProviderVehicleDetail]') AND [c].[name] = N'NumberOfVehicle');
    IF @var23 IS NOT NULL EXEC(N'ALTER TABLE [ServiceProviderVehicleDetail] DROP CONSTRAINT [' + @var23 + '];');
    ALTER TABLE [ServiceProviderVehicleDetail] DROP COLUMN [NumberOfVehicle];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210711120927_Modify vehicle detail structure')
BEGIN
    ALTER TABLE [VehicleDetail] ADD [MaxWeight] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210711120927_Modify vehicle detail structure')
BEGIN
    ALTER TABLE [VehicleDetail] ADD [Model] nvarchar(250) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210711120927_Modify vehicle detail structure')
BEGIN
    ALTER TABLE [VehicleDetail] ADD [WeightUnit] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210711120927_Modify vehicle detail structure')
BEGIN
    ALTER TABLE [VehicleDetail] ADD [WheelType] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210711120927_Modify vehicle detail structure')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210711120927_Modify vehicle detail structure', N'5.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210711164553_update table structure related with appUser')
BEGIN
    DROP INDEX [IX_ServiceSeeker_UserId] ON [ServiceSeeker];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210711164553_update table structure related with appUser')
BEGIN
    DROP INDEX [IX_ServiceProvider_UserId] ON [ServiceProvider];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210711164553_update table structure related with appUser')
BEGIN
    DROP INDEX [IX_PersonalDetails_AppUserId] ON [PersonalDetails];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210711164553_update table structure related with appUser')
BEGIN
    DROP INDEX [IX_Employees_UserId] ON [Employees];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210711164553_update table structure related with appUser')
BEGIN
    DROP INDEX [IX_CompanyDetails_AppUserId] ON [CompanyDetails];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210711164553_update table structure related with appUser')
BEGIN
    CREATE INDEX [IX_ServiceSeeker_UserId] ON [ServiceSeeker] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210711164553_update table structure related with appUser')
BEGIN
    CREATE INDEX [IX_ServiceProvider_UserId] ON [ServiceProvider] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210711164553_update table structure related with appUser')
BEGIN
    CREATE INDEX [IX_PersonalDetails_AppUserId] ON [PersonalDetails] ([AppUserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210711164553_update table structure related with appUser')
BEGIN
    CREATE INDEX [IX_Employees_UserId] ON [Employees] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210711164553_update table structure related with appUser')
BEGIN
    CREATE INDEX [IX_CompanyDetails_AppUserId] ON [CompanyDetails] ([AppUserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210711164553_update table structure related with appUser')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210711164553_update table structure related with appUser', N'5.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210712161932_check any remaining recent changes')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210712161932_check any remaining recent changes', N'5.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210713084127_ set UserName as required property of Application user')
BEGIN
    DECLARE @var24 sysname;
    SELECT @var24 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'UserName');
    IF @var24 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var24 + '];');
    ALTER TABLE [Users] ALTER COLUMN [UserName] nvarchar(350) NOT NULL;
    ALTER TABLE [Users] ADD DEFAULT N'' FOR [UserName];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210713084127_ set UserName as required property of Application user')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210713084127_ set UserName as required property of Application user', N'5.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210715073820_Add Contact Person')
BEGIN
    CREATE TABLE [ContactPerson] (
        [Id] int NOT NULL IDENTITY,
        [PersionId] int NOT NULL,
        [MobileNumberConfirmed] bit NOT NULL DEFAULT CAST(0 AS bit),
        [EmailConfirmed] bit NOT NULL DEFAULT CAST(0 AS bit),
        CONSTRAINT [PK_ContactPerson] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ContactPerson_Person] FOREIGN KEY ([PersionId]) REFERENCES [PersonalDetails] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210715073820_Add Contact Person')
BEGIN
    CREATE TABLE [ServiceProviderContactPerson] (
        [ServiceProviderId] int NOT NULL,
        [ContactPersonId] int NOT NULL,
        CONSTRAINT [PK_ServiceProviderContactPerson] PRIMARY KEY ([ContactPersonId], [ServiceProviderId]),
        CONSTRAINT [FK_SerciceProvider_ServiceProviderContactPerson] FOREIGN KEY ([ServiceProviderId]) REFERENCES [ServiceProvider] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_ServiceProviderContactPerson_ContactPerson] FOREIGN KEY ([ContactPersonId]) REFERENCES [ContactPerson] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210715073820_Add Contact Person')
BEGIN
    CREATE UNIQUE INDEX [IX_ContactPerson_PersionId] ON [ContactPerson] ([PersionId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210715073820_Add Contact Person')
BEGIN
    CREATE UNIQUE INDEX [IX_ServiceProviderContactPerson_ContactPersonId] ON [ServiceProviderContactPerson] ([ContactPersonId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210715073820_Add Contact Person')
BEGIN
    CREATE INDEX [IX_ServiceProviderContactPerson_ServiceProviderId] ON [ServiceProviderContactPerson] ([ServiceProviderId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210715073820_Add Contact Person')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210715073820_Add Contact Person', N'5.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210715085047_UpdateConactDetailProperty')
BEGIN
    DECLARE @var25 sysname;
    SELECT @var25 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ContactPerson]') AND [c].[name] = N'EmailConfirmed');
    IF @var25 IS NOT NULL EXEC(N'ALTER TABLE [ContactPerson] DROP CONSTRAINT [' + @var25 + '];');
    ALTER TABLE [ContactPerson] DROP COLUMN [EmailConfirmed];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210715085047_UpdateConactDetailProperty')
BEGIN
    DECLARE @var26 sysname;
    SELECT @var26 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ContactPerson]') AND [c].[name] = N'MobileNumberConfirmed');
    IF @var26 IS NOT NULL EXEC(N'ALTER TABLE [ContactPerson] DROP CONSTRAINT [' + @var26 + '];');
    ALTER TABLE [ContactPerson] DROP COLUMN [MobileNumberConfirmed];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210715085047_UpdateConactDetailProperty')
BEGIN
    EXEC sp_rename N'[ContactPerson].[PersionId]', N'PersonId', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210715085047_UpdateConactDetailProperty')
BEGIN
    EXEC sp_rename N'[ContactPerson].[IX_ContactPerson_PersionId]', N'IX_ContactPerson_PersonId', N'INDEX';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210715085047_UpdateConactDetailProperty')
BEGIN
    ALTER TABLE [ContactDetail] ADD [EmailConfirmed] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210715085047_UpdateConactDetailProperty')
BEGIN
    ALTER TABLE [ContactDetail] ADD [MobileNumberConfirmed] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210715085047_UpdateConactDetailProperty')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210715085047_UpdateConactDetailProperty', N'5.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210715090516_Update ContactPerson')
BEGIN
    ALTER TABLE [ContactPerson] ADD [Active] bit NOT NULL DEFAULT CAST(1 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210715090516_Update ContactPerson')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210715090516_Update ContactPerson', N'5.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210715115258_MakePersonIndependentEntity')
BEGIN
    ALTER TABLE [PersonalDetails] DROP CONSTRAINT [FK_PersonalDetail_To_AppUsers];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210715115258_MakePersonIndependentEntity')
BEGIN
    DROP INDEX [IX_PersonalDetails_AppUserId] ON [PersonalDetails];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210715115258_MakePersonIndependentEntity')
BEGIN
    DECLARE @var27 sysname;
    SELECT @var27 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PersonalDetails]') AND [c].[name] = N'AppUserId');
    IF @var27 IS NOT NULL EXEC(N'ALTER TABLE [PersonalDetails] DROP CONSTRAINT [' + @var27 + '];');
    ALTER TABLE [PersonalDetails] DROP COLUMN [AppUserId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210715115258_MakePersonIndependentEntity')
BEGIN
    DECLARE @var28 sysname;
    SELECT @var28 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PersonalDetails]') AND [c].[name] = N'MiddleName');
    IF @var28 IS NOT NULL EXEC(N'ALTER TABLE [PersonalDetails] DROP CONSTRAINT [' + @var28 + '];');
    ALTER TABLE [PersonalDetails] ALTER COLUMN [MiddleName] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210715115258_MakePersonIndependentEntity')
BEGIN
    CREATE TABLE [UserPersonDetail] (
        [UserId] int NOT NULL,
        [PersonId] int NOT NULL,
        CONSTRAINT [PK_UserPersonDetail] PRIMARY KEY ([UserId], [PersonId]),
        CONSTRAINT [FK_User_UserPersonDetail] FOREIGN KEY ([UserId]) REFERENCES [AppUsers] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_UserPersonDetail_Persondetail] FOREIGN KEY ([PersonId]) REFERENCES [PersonalDetails] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210715115258_MakePersonIndependentEntity')
BEGIN
    CREATE UNIQUE INDEX [IX_UserPersonDetail_PersonId] ON [UserPersonDetail] ([PersonId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210715115258_MakePersonIndependentEntity')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210715115258_MakePersonIndependentEntity', N'5.0.5');
END;
GO

COMMIT;
GO

