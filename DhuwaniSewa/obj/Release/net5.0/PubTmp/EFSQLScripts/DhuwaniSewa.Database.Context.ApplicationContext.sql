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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210724101452_CreateDhuwaniSewaScheema.2021.07.24')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210724101452_CreateDhuwaniSewaScheema.2021.07.24')
BEGIN
    CREATE TABLE [Category] (
        [Id] int NOT NULL IDENTITY,
        [Active] bit NOT NULL DEFAULT CAST(1 AS bit),
        [Enum] nvarchar(150) NULL,
        [DisplayName] nvarchar(250) NOT NULL,
        [Description] nvarchar(max) NULL,
        [CreatedDate] datetime2 NOT NULL,
        [CreatedBy] int NULL,
        [ModifiedDate] datetime2 NULL,
        [ModifiedBy] int NULL,
        CONSTRAINT [PK_Category] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210724101452_CreateDhuwaniSewaScheema.2021.07.24')
BEGIN
    CREATE TABLE [ContactDetail] (
        [Id] int NOT NULL IDENTITY,
        [ContactNumber] nvarchar(max) NULL,
        [Email] nvarchar(max) NULL,
        [MobileNumberConfirmed] bit NOT NULL DEFAULT CAST(0 AS bit),
        [EmailConfirmed] bit NOT NULL DEFAULT CAST(0 AS bit),
        CONSTRAINT [PK_ContactDetail] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210724101452_CreateDhuwaniSewaScheema.2021.07.24')
BEGIN
    CREATE TABLE [DocumentDetail] (
        [Id] int NOT NULL IDENTITY,
        [Type] nvarchar(50) NOT NULL,
        [RegistrationNumber] nvarchar(100) NOT NULL,
        [IssuedDistrict] nvarchar(100) NULL,
        CONSTRAINT [PK_DocumentDetail] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210724101452_CreateDhuwaniSewaScheema.2021.07.24')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210724101452_CreateDhuwaniSewaScheema.2021.07.24')
BEGIN
    CREATE TABLE [PersonalDetails] (
        [Id] int NOT NULL IDENTITY,
        [FirstName] nvarchar(200) NOT NULL,
        [MiddleName] nvarchar(max) NULL,
        [LastName] nvarchar(250) NOT NULL,
        CONSTRAINT [PK_PersonalDetails] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210724101452_CreateDhuwaniSewaScheema.2021.07.24')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210724101452_CreateDhuwaniSewaScheema.2021.07.24')
BEGIN
    CREATE TABLE [Users] (
        [Id] nvarchar(450) NOT NULL,
        [IsActive] bit NOT NULL DEFAULT CAST(1 AS bit),
        [UserName] nvarchar(350) NOT NULL,
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210724101452_CreateDhuwaniSewaScheema.2021.07.24')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210724101452_CreateDhuwaniSewaScheema.2021.07.24')
BEGIN
    CREATE TABLE [Roles] (
        [Id] nvarchar(450) NOT NULL,
        [IsActive] bit NOT NULL DEFAULT CAST(1 AS bit),
        [Description] nvarchar(max) NULL,
        CONSTRAINT [PK_Roles] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Roles_AspNetRoles_Id] FOREIGN KEY ([Id]) REFERENCES [AspNetRoles] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210724101452_CreateDhuwaniSewaScheema.2021.07.24')
BEGIN
    CREATE TABLE [Choice] (
        [Id] int NOT NULL IDENTITY,
        [Active] bit NOT NULL DEFAULT CAST(1 AS bit),
        [Enum] nvarchar(150) NULL,
        [DisplayName] nvarchar(250) NOT NULL,
        [CategoryId] int NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [CreatedBy] int NULL,
        [ModifiedDate] datetime2 NULL,
        [ModifiedBy] int NULL,
        CONSTRAINT [PK_Choice] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Category_Choice] FOREIGN KEY ([CategoryId]) REFERENCES [Category] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210724101452_CreateDhuwaniSewaScheema.2021.07.24')
BEGIN
    CREATE TABLE [ContactPerson] (
        [Id] int NOT NULL IDENTITY,
        [PersonId] int NOT NULL,
        [Active] bit NOT NULL DEFAULT CAST(1 AS bit),
        CONSTRAINT [PK_ContactPerson] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ContactPerson_Person] FOREIGN KEY ([PersonId]) REFERENCES [PersonalDetails] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210724101452_CreateDhuwaniSewaScheema.2021.07.24')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210724101452_CreateDhuwaniSewaScheema.2021.07.24')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210724101452_CreateDhuwaniSewaScheema.2021.07.24')
BEGIN
    CREATE TABLE [AppUsers] (
        [Id] int NOT NULL IDENTITY,
        [Active] bit NOT NULL DEFAULT CAST(1 AS bit),
        [UserId] nvarchar(450) NOT NULL,
        [IsEmployee] bit NOT NULL DEFAULT CAST(0 AS bit),
        [IsCompnay] bit NOT NULL DEFAULT CAST(0 AS bit),
        [IsServiceProvider] bit NOT NULL DEFAULT CAST(1 AS bit),
        [Otp] nvarchar(250) NULL,
        [OtpCreatedDate] datetime2 NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [CreatedBy] int NULL,
        [ModifiedDate] datetime2 NULL,
        [ModifiedBy] int NULL,
        CONSTRAINT [PK_AppUsers] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspUser_ApplicationUser] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210724101452_CreateDhuwaniSewaScheema.2021.07.24')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210724101452_CreateDhuwaniSewaScheema.2021.07.24')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210724101452_CreateDhuwaniSewaScheema.2021.07.24')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210724101452_CreateDhuwaniSewaScheema.2021.07.24')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210724101452_CreateDhuwaniSewaScheema.2021.07.24')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210724101452_CreateDhuwaniSewaScheema.2021.07.24')
BEGIN
    CREATE TABLE [VehicleDetail] (
        [Id] int NOT NULL IDENTITY,
        [TypeId] int NOT NULL,
        [BrandId] int NOT NULL,
        [RegistrationNumber] nvarchar(100) NOT NULL,
        [Model] nvarchar(250) NULL,
        [MaxWeight] int NOT NULL,
        [WeightUnit] int NOT NULL,
        [WheelType] int NOT NULL,
        CONSTRAINT [PK_VehicleDetail] PRIMARY KEY ([Id]),
        CONSTRAINT [Fk_VehicleDetail_Brand_Choice] FOREIGN KEY ([BrandId]) REFERENCES [Choice] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_VehicleDetail_Type_Choice] FOREIGN KEY ([TypeId]) REFERENCES [Choice] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210724101452_CreateDhuwaniSewaScheema.2021.07.24')
BEGIN
    CREATE TABLE [CompanyDetails] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(250) NOT NULL,
        [AppUserId] int NOT NULL,
        CONSTRAINT [PK_CompanyDetails] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Companydetail_To_AppUsers] FOREIGN KEY ([AppUserId]) REFERENCES [AppUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210724101452_CreateDhuwaniSewaScheema.2021.07.24')
BEGIN
    CREATE TABLE [Customers] (
        [Id] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        [IsActive] bit NOT NULL DEFAULT CAST(1 AS bit),
        CONSTRAINT [PK_Customers] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Customer_To_AppUser] FOREIGN KEY ([UserId]) REFERENCES [AppUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210724101452_CreateDhuwaniSewaScheema.2021.07.24')
BEGIN
    CREATE TABLE [Employees] (
        [Id] int NOT NULL IDENTITY,
        [Desigination] nvarchar(max) NOT NULL,
        [UserId] int NOT NULL,
        [IsActive] bit NOT NULL DEFAULT CAST(1 AS bit),
        [DhuwaniSewaId] nvarchar(max) NULL,
        [CreatedDate] datetime2 NOT NULL,
        [CreatedBy] int NULL,
        [ModifiedDate] datetime2 NULL,
        [ModifiedBy] int NULL,
        CONSTRAINT [PK_Employees] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Employee_To_AppUser] FOREIGN KEY ([UserId]) REFERENCES [AppUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210724101452_CreateDhuwaniSewaScheema.2021.07.24')
BEGIN
    CREATE TABLE [ServiceProvider] (
        [Id] int NOT NULL IDENTITY,
        [Active] bit NOT NULL DEFAULT CAST(1 AS bit),
        [DetailsCorrectAgreed] bit NOT NULL,
        [UserId] int NOT NULL,
        [DhuwaniSewaId] nvarchar(250) NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [CreatedBy] int NULL,
        [ModifiedDate] datetime2 NULL,
        [ModifiedBy] int NULL,
        CONSTRAINT [PK_ServiceProvider] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ServiceProvider_AppUser] FOREIGN KEY ([UserId]) REFERENCES [AppUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210724101452_CreateDhuwaniSewaScheema.2021.07.24')
BEGIN
    CREATE TABLE [ServiceSeeker] (
        [Id] int NOT NULL IDENTITY,
        [Active] bit NOT NULL DEFAULT CAST(1 AS bit),
        [DetailsCorrectAgreed] bit NOT NULL,
        [UserId] int NOT NULL,
        [DhuwaniSewaId] nvarchar(250) NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [CreatedBy] int NULL,
        [ModifiedDate] datetime2 NULL,
        [ModifiedBy] int NULL,
        CONSTRAINT [PK_ServiceSeeker] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ServiceSeeker_AppUser] FOREIGN KEY ([UserId]) REFERENCES [AppUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210724101452_CreateDhuwaniSewaScheema.2021.07.24')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210724101452_CreateDhuwaniSewaScheema.2021.07.24')
BEGIN
    CREATE TABLE [ServiceProviderContactPerson] (
        [ServiceProviderId] int NOT NULL,
        [ContactPersonId] int NOT NULL,
        CONSTRAINT [PK_ServiceProviderContactPerson] PRIMARY KEY ([ContactPersonId], [ServiceProviderId]),
        CONSTRAINT [FK_SerciceProvider_ServiceProviderContactPerson] FOREIGN KEY ([ServiceProviderId]) REFERENCES [ServiceProvider] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ServiceProviderContactPerson_ContactPerson] FOREIGN KEY ([ContactPersonId]) REFERENCES [ContactPerson] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210724101452_CreateDhuwaniSewaScheema.2021.07.24')
BEGIN
    CREATE TABLE [ServiceProviderVehicleDetail] (
        [ServiceProviderId] int NOT NULL,
        [VehicleDetailId] int NOT NULL,
        CONSTRAINT [PK_ServiceProviderVehicleDetail] PRIMARY KEY ([ServiceProviderId], [VehicleDetailId]),
        CONSTRAINT [FK_SericeProvider_ServiceProviderVehicleDetail] FOREIGN KEY ([ServiceProviderId]) REFERENCES [ServiceProvider] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ServiceProviderVehicleDetail_VehicleDetail] FOREIGN KEY ([VehicleDetailId]) REFERENCES [VehicleDetail] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210724101452_CreateDhuwaniSewaScheema.2021.07.24')
BEGIN
    CREATE UNIQUE INDEX [IX_AppUsers_UserId] ON [AppUsers] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210724101452_CreateDhuwaniSewaScheema.2021.07.24')
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210724101452_CreateDhuwaniSewaScheema.2021.07.24')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210724101452_CreateDhuwaniSewaScheema.2021.07.24')
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210724101452_CreateDhuwaniSewaScheema.2021.07.24')
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210724101452_CreateDhuwaniSewaScheema.2021.07.24')
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210724101452_CreateDhuwaniSewaScheema.2021.07.24')
BEGIN
    CREATE INDEX [IX_Choice_CategoryId] ON [Choice] ([CategoryId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210724101452_CreateDhuwaniSewaScheema.2021.07.24')
BEGIN
    CREATE INDEX [IX_CompanyDetails_AppUserId] ON [CompanyDetails] ([AppUserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210724101452_CreateDhuwaniSewaScheema.2021.07.24')
BEGIN
    CREATE UNIQUE INDEX [IX_ContactPerson_PersonId] ON [ContactPerson] ([PersonId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210724101452_CreateDhuwaniSewaScheema.2021.07.24')
BEGIN
    CREATE UNIQUE INDEX [IX_Customers_UserId] ON [Customers] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210724101452_CreateDhuwaniSewaScheema.2021.07.24')
BEGIN
    CREATE INDEX [IX_Employees_UserId] ON [Employees] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210724101452_CreateDhuwaniSewaScheema.2021.07.24')
BEGIN
    CREATE INDEX [IX_PersonalDetailContactDetail_ContactDetailId] ON [PersonalDetailContactDetail] ([ContactDetailId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210724101452_CreateDhuwaniSewaScheema.2021.07.24')
BEGIN
    CREATE INDEX [IX_PersonalDetailDocumentDetail_DocumentDetailId] ON [PersonalDetailDocumentDetail] ([DocumentDetailId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210724101452_CreateDhuwaniSewaScheema.2021.07.24')
BEGIN
    CREATE UNIQUE INDEX [IX_RefreshToken_UserId] ON [RefreshToken] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210724101452_CreateDhuwaniSewaScheema.2021.07.24')
BEGIN
    CREATE INDEX [IX_ServiceProvider_UserId] ON [ServiceProvider] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210724101452_CreateDhuwaniSewaScheema.2021.07.24')
BEGIN
    CREATE UNIQUE INDEX [IX_ServiceProviderContactPerson_ContactPersonId] ON [ServiceProviderContactPerson] ([ContactPersonId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210724101452_CreateDhuwaniSewaScheema.2021.07.24')
BEGIN
    CREATE INDEX [IX_ServiceProviderContactPerson_ServiceProviderId] ON [ServiceProviderContactPerson] ([ServiceProviderId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210724101452_CreateDhuwaniSewaScheema.2021.07.24')
BEGIN
    CREATE UNIQUE INDEX [IX_ServiceProviderVehicleDetail_VehicleDetailId] ON [ServiceProviderVehicleDetail] ([VehicleDetailId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210724101452_CreateDhuwaniSewaScheema.2021.07.24')
BEGIN
    CREATE INDEX [IX_ServiceSeeker_UserId] ON [ServiceSeeker] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210724101452_CreateDhuwaniSewaScheema.2021.07.24')
BEGIN
    CREATE UNIQUE INDEX [IX_UserPersonDetail_PersonId] ON [UserPersonDetail] ([PersonId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210724101452_CreateDhuwaniSewaScheema.2021.07.24')
BEGIN
    CREATE INDEX [EmailIndex] ON [Users] ([NormalizedEmail]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210724101452_CreateDhuwaniSewaScheema.2021.07.24')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [Users] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210724101452_CreateDhuwaniSewaScheema.2021.07.24')
BEGIN
    CREATE INDEX [IX_VehicleDetail_BrandId] ON [VehicleDetail] ([BrandId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210724101452_CreateDhuwaniSewaScheema.2021.07.24')
BEGIN
    CREATE INDEX [IX_VehicleDetail_TypeId] ON [VehicleDetail] ([TypeId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210724101452_CreateDhuwaniSewaScheema.2021.07.24')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210724101452_CreateDhuwaniSewaScheema.2021.07.24', N'5.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210814104142_addFreshOtpPopertyInAppUserTbl')
BEGIN
    ALTER TABLE [AppUsers] ADD [IsFreshOtp] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210814104142_addFreshOtpPopertyInAppUserTbl')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210814104142_addFreshOtpPopertyInAppUserTbl', N'5.0.8');
END;
GO

COMMIT;
GO

