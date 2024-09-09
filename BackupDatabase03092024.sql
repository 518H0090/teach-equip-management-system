USE [master]
GO
/****** Object:  Database [TeachEquipManagement]    Script Date: 03/09/2024 8:45:46 SA ******/
CREATE DATABASE [TeachEquipManagement]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TeachEquipManagement', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\TeachEquipManagement.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TeachEquipManagement_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\TeachEquipManagement_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [TeachEquipManagement] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TeachEquipManagement].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TeachEquipManagement] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TeachEquipManagement] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TeachEquipManagement] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TeachEquipManagement] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TeachEquipManagement] SET ARITHABORT OFF 
GO
ALTER DATABASE [TeachEquipManagement] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TeachEquipManagement] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TeachEquipManagement] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TeachEquipManagement] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TeachEquipManagement] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TeachEquipManagement] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TeachEquipManagement] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TeachEquipManagement] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TeachEquipManagement] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TeachEquipManagement] SET  ENABLE_BROKER 
GO
ALTER DATABASE [TeachEquipManagement] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TeachEquipManagement] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TeachEquipManagement] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TeachEquipManagement] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TeachEquipManagement] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TeachEquipManagement] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [TeachEquipManagement] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TeachEquipManagement] SET RECOVERY FULL 
GO
ALTER DATABASE [TeachEquipManagement] SET  MULTI_USER 
GO
ALTER DATABASE [TeachEquipManagement] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TeachEquipManagement] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TeachEquipManagement] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TeachEquipManagement] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TeachEquipManagement] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TeachEquipManagement] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'TeachEquipManagement', N'ON'
GO
ALTER DATABASE [TeachEquipManagement] SET QUERY_STORE = ON
GO
ALTER DATABASE [TeachEquipManagement] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [TeachEquipManagement]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 03/09/2024 8:45:46 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AccountDetails]    Script Date: 03/09/2024 8:45:46 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountDetails](
	[AccountId] [uniqueidentifier] NOT NULL,
	[FullName] [nvarchar](255) NOT NULL,
	[Address] [nvarchar](255) NOT NULL,
	[Phone] [nvarchar](255) NOT NULL,
	[Avatar] [nvarchar](255) NULL,
	[SpoFileId] [nvarchar](max) NULL,
 CONSTRAINT [PK_AccountDetails] PRIMARY KEY CLUSTERED 
(
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 03/09/2024 8:45:46 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[Id] [uniqueidentifier] NOT NULL,
	[Username] [nvarchar](255) NOT NULL,
	[PasswordHash] [varbinary](max) NOT NULL,
	[PasswordSalt] [varbinary](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[RefreshToken] [nvarchar](max) NULL,
	[RefreshTokenExpiryTime] [datetime2](7) NULL,
	[RoleId] [int] NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ApprovalRequests]    Script Date: 03/09/2024 8:45:46 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApprovalRequests](
	[AccountId] [uniqueidentifier] NOT NULL,
	[InventoryId] [uniqueidentifier] NOT NULL,
	[Quantity] [int] NOT NULL,
	[RequestType] [nvarchar](max) NOT NULL,
	[RequestDate] [datetime2](7) NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
	[ApproveDate] [datetime2](7) NULL,
	[IsApproved] [bit] NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_ApprovalRequests] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 03/09/2024 8:45:46 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Inventories]    Script Date: 03/09/2024 8:45:46 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Inventories](
	[Id] [uniqueidentifier] NOT NULL,
	[TotalQuantity] [int] NOT NULL,
	[AmountBorrow] [int] NOT NULL,
	[ToolId] [int] NOT NULL,
 CONSTRAINT [PK_Inventories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InventoryHistories]    Script Date: 03/09/2024 8:45:46 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InventoryHistories](
	[AccountId] [uniqueidentifier] NOT NULL,
	[InventoryId] [uniqueidentifier] NOT NULL,
	[Quantity] [int] NOT NULL,
	[ActionDate] [datetime2](7) NOT NULL,
	[ActionType] [nvarchar](max) NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_InventoryHistories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invoices]    Script Date: 03/09/2024 8:45:46 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoices](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Price] [float] NOT NULL,
	[InvoiceDate] [datetime2](7) NOT NULL,
	[ToolId] [int] NOT NULL,
 CONSTRAINT [PK_Invoices] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 03/09/2024 8:45:46 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](max) NOT NULL,
	[RoleDescription] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Suppliers]    Script Date: 03/09/2024 8:45:46 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Suppliers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SupplierName] [nvarchar](max) NOT NULL,
	[ContactName] [nvarchar](max) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[Phone] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Suppliers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ToolCategories]    Script Date: 03/09/2024 8:45:46 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ToolCategories](
	[ToolId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
 CONSTRAINT [PK_ToolCategories] PRIMARY KEY CLUSTERED 
(
	[ToolId] ASC,
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tools]    Script Date: 03/09/2024 8:45:46 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tools](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ToolName] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[SupplierId] [int] NOT NULL,
	[Avatar] [nvarchar](max) NULL,
	[SpoFileId] [nvarchar](max) NULL,
	[Unit] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Tools] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240723135524_StructureSprint3', N'8.0.6')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240729080024_UpdateDatabaseForInventoryHistoryAndApprovalRequest', N'8.0.6')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240729094047_removemanagerapprove', N'8.0.6')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240730024740_ChangeToAccountIdForInventoryHistory', N'8.0.6')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240807021950_SeedDataForRole', N'8.0.6')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240811052022_changeNameUseDetailToAccountDetails', N'8.0.6')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240815083213_AddNewFieldForTool', N'8.0.6')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240817020719_RemoveUnitFromCategory', N'8.0.6')
GO
INSERT [dbo].[AccountDetails] ([AccountId], [FullName], [Address], [Phone], [Avatar], [SpoFileId]) VALUES (N'3b28098a-23de-485d-8610-1788be91b1b8', N'', N'', N'', N'', N'')
INSERT [dbo].[AccountDetails] ([AccountId], [FullName], [Address], [Phone], [Avatar], [SpoFileId]) VALUES (N'86618d09-3ede-43ab-949e-860b71607331', N'', N'', N'', N'', N'')
INSERT [dbo].[AccountDetails] ([AccountId], [FullName], [Address], [Phone], [Avatar], [SpoFileId]) VALUES (N'aeed8865-74a9-4f52-9916-a26271d8571c', N'', N'', N'', N'', N'')
INSERT [dbo].[AccountDetails] ([AccountId], [FullName], [Address], [Phone], [Avatar], [SpoFileId]) VALUES (N'fa776b8a-f2d1-44e8-ba81-b63b616ab8c9', N'', N'', N'', N'', N'')
INSERT [dbo].[AccountDetails] ([AccountId], [FullName], [Address], [Phone], [Avatar], [SpoFileId]) VALUES (N'1ba68635-67d8-4dbb-8937-cba49842217b', N'Huỳnh Trần Trung Hiếu', N'Bình Tân', N'0384992205', N'https://trunghieu1204.sharepoint.com/sites/FamilyTree/Avatars/avartar-anime-91.jpg', N'01TFPUVZ6M5W7LOAE4HRBZ35XLDJTCDBSE')
INSERT [dbo].[AccountDetails] ([AccountId], [FullName], [Address], [Phone], [Avatar], [SpoFileId]) VALUES (N'14cd4bff-8938-44d6-a6a6-ea579ebe37ad', N'', N'', N'', N'', N'')
INSERT [dbo].[AccountDetails] ([AccountId], [FullName], [Address], [Phone], [Avatar], [SpoFileId]) VALUES (N'54fa708c-b193-4c4f-a541-f8d8f0057a8b', N'', N'', N'', N'', N'')
INSERT [dbo].[AccountDetails] ([AccountId], [FullName], [Address], [Phone], [Avatar], [SpoFileId]) VALUES (N'48a7b07b-855a-4b75-92e4-ff931957182b', N'', N'', N'', N'', N'')
GO
INSERT [dbo].[Accounts] ([Id], [Username], [PasswordHash], [PasswordSalt], [Email], [RefreshToken], [RefreshTokenExpiryTime], [RoleId]) VALUES (N'3b28098a-23de-485d-8610-1788be91b1b8', N'hoiaita5', 0xDF32559B49DA77362909806DEFBD57D9AD54C500A3615E98083A3FA00102992188EDDBD28423210B2289BD79EAE5F73E957C66D5DF760570128E12EC826C5575, 0xE46730CEE430E70950B7796E4F3866195E7BC7F5F5CC25043409EB37DBFEB543C2452676857FB903D549E1157939DD62A19013A636CCD3D2972715DB9BAC04AA9A7C7D9E2F705F488E51A90CA1CA3A4735DEDAD5DE4F99048DE3D983F02998DDAD1C401300079B2989BE9A8AD8C29BC13959C8031226F4CA949222A4B3FFF246, N'BeckyGraham@gmail.com', N'', NULL, 2)
INSERT [dbo].[Accounts] ([Id], [Username], [PasswordHash], [PasswordSalt], [Email], [RefreshToken], [RefreshTokenExpiryTime], [RoleId]) VALUES (N'86618d09-3ede-43ab-949e-860b71607331', N'ReidAustin', 0xB8E7178704D7BCABB427B0B7599A4E5C6E4D88438208D36792327CCE4A9F8E01CE52C25BFA7BE6AC86CD1EC8DFA466B6B4F03745B582C6FE5E73426EC97493CE, 0xE58BA79F34459F9E5C491623297EF6AB5EE81A5BA2F9C84C255703A192EAA7FD7F9694ADEC582BCC39434884FB6D5594E0B0798BF3A45306FD7C6EE6B906D02EEE745FF23BE995DF4525E6C27360A9B7F92E40C49E8D47D42B0886B758649A087C5B8C55085E2466B363B91A08A4C50BC3B0BBA4AC62B21779653CD0728B65CC, N'AustinR1290@gmail.com', N'', NULL, 1)
INSERT [dbo].[Accounts] ([Id], [Username], [PasswordHash], [PasswordSalt], [Email], [RefreshToken], [RefreshTokenExpiryTime], [RoleId]) VALUES (N'aeed8865-74a9-4f52-9916-a26271d8571c', N'hieuro5122', 0x6C5872AE6F20B4DC5A359FE90310FC03E613BCE398E2978CEAD2527F0367E32D9780D05B7633CBE29842D9527F8BF8F37CD8CC21730A61AC91025AE2FA23DD78, 0xA38A9480EE5145ACF94BBAE68F75AB754B76DDC944D297ECCD62839C416243DF9CFE661D8F57EB93D36B0554FCF30403BD802A708C07E54B5A884A52E77F1E2479967B3EB3BD2972DAFC2747355067CDB25FEAF73537BD39D759CB868226BEC7C3A8C3FDE7C46E9AE3F217A1BB9FA4DE72492A61C053DAA5022FA4039719BB6C, N'hieuro5122@gmail.com', N'', NULL, 3)
INSERT [dbo].[Accounts] ([Id], [Username], [PasswordHash], [PasswordSalt], [Email], [RefreshToken], [RefreshTokenExpiryTime], [RoleId]) VALUES (N'fa776b8a-f2d1-44e8-ba81-b63b616ab8c9', N'Shawna Buck', 0x5112ABA55F2B108511767FA78670ED701C6985C2967FAEE1DBF8C6BA84D0333181F8F3E1B2669E7909D570D1883347F4CC8F8212F330C78ED5611E9859268F3E, 0xE9DA60E2B66C49E793AF247064BB9A116B36EEA6E4D28130AB628A574E5D251E93E33EAAC0B5F1D68A19709D1D7E40FE05EA0BFFB9D344F5520B21AEF1FA88736DA6EF3CB644CCF0201048E272E46E5C7DED303A736F8032CE30B4196EA712E500FFBA5E0C1552B6C539FE1FF0DBC1B547F664991AE4982DBE00F6675FF99987, N'Shabuck@gmail.com', N'', NULL, 2)
INSERT [dbo].[Accounts] ([Id], [Username], [PasswordHash], [PasswordSalt], [Email], [RefreshToken], [RefreshTokenExpiryTime], [RoleId]) VALUES (N'1ba68635-67d8-4dbb-8937-cba49842217b', N'trunghieuit1204', 0xD3B2A98421F430796BA1273712DDEE1A1C90F4CE9535DB4971DFCCF603872A59295FBFCACC45B48682AC8D28CE07B234442B271D20A7894D6804AFD99461B7DB, 0x11B5794F940A551B9A82886216249F13884D60382346D2F2AB69AAC4BDCA009E829CE5C20C0F83CD95F95AAF7AFC471FD9FDCE21AADA24DB3964E2A57A37C90AE2031B90D6FEAEF19B9E39520B3459ACC6FD5CD9464317D6F8F1192DCB3677D26AE244A50159B6E8A50D8C26D4E197B25E9BB7B8CBDC62AB865607BBDC85F5E6, N'trunghieuit1204@gmail.com', N'ikoOaHeSsMKE8faDPSzfbdb0r5sZcDtjLx6ASG1jklY=', CAST(N'2024-09-10T08:40:42.1852281' AS DateTime2), 1)
INSERT [dbo].[Accounts] ([Id], [Username], [PasswordHash], [PasswordSalt], [Email], [RefreshToken], [RefreshTokenExpiryTime], [RoleId]) VALUES (N'14cd4bff-8938-44d6-a6a6-ea579ebe37ad', N'kylahen123', 0x9B852189B58E34A44DDEF7E51A1EF3A6E47D559A88700625B58EA1337ED1665435426BC6D779E5556572BA358F526FA45D009C267715B64F76FE9AC0167F395F, 0x7B0C0504F54AD639D0018BF51D044F8AF6EC9853F7FE2A1669B45DB79A6AFB42C740DE23BABDB847731A307262CCF0562CC6CC5592BEAB3C89D5B11B4BF2275C0531C42D58E354B741492AB9CEE161B25321C5C8F2DB63A5C3CD14BB2D397CEBCEDF8CC6DB7E03A4018578379B2BA40DCDC4E1180AECA41BC496C4B99F82DBD2, N'ArthurHolloway@gmail.com', N'', NULL, 2)
INSERT [dbo].[Accounts] ([Id], [Username], [PasswordHash], [PasswordSalt], [Email], [RefreshToken], [RefreshTokenExpiryTime], [RoleId]) VALUES (N'54fa708c-b193-4c4f-a541-f8d8f0057a8b', N'chaymetwa', 0x1F14C3F428F4FF459F9BB65BAA004A7F52DC7EEE208BDA714EE37647768822DA8BCDC368E26146EDEBA53B3ACDF96E107FDCE0BABFA7E5F44205D3C772C94FEB, 0x607C880B6A4C230273A100D33D5D868EF27AF88C77A996C7580E451EECCED99F3E2D1C74B3747E6FA80FCF7D8D2A588929D4506ED047F310D93BB89A0BCAEA6106133A81DAB09FE2753BE53CD2833194EDB118B9EA969D9D77CFDB920F3BE757A7A0867024D90FD1941109AA115839A3B538E38CB5FEF64156668108BF2D44BD, N'chaymetwa@gmail.com', N'aR3KNn1umqM+tq9yWdSGPBn6kbf+SAUlKIvqgs5oH1I=', CAST(N'2024-08-19T21:38:24.6461609' AS DateTime2), 3)
INSERT [dbo].[Accounts] ([Id], [Username], [PasswordHash], [PasswordSalt], [Email], [RefreshToken], [RefreshTokenExpiryTime], [RoleId]) VALUES (N'48a7b07b-855a-4b75-92e4-ff931957182b', N'bichtuyen', 0xD629AAA059E3A18C964FED5BAB046EEF8F2B84DC70727D2C0F2F17B7A76670CAC58DFF7BAE679794B4A111752B1213E88D3C74073DA65EF20463A137FA377C05, 0x7A8AF00BEECB7A3B39B99ADA6EA245D070B719156A1E0EEFF081886F7AFC767E7AA7AA1DD5EB73BA33F0F7C6F04E7EC3BDA4D0CE7AE2A2197BB3BB4AB49706481B43C807287A2AF9883881EFFA2449E5759C0444A6994B13B980791C3DE92DC5EC9ADCE1E18BAE318423BBE5410E0FA4419B4C1BD4FE7D9D18EFD9170355735F, N'bichtuyen@gmail.com', N'', NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[ApprovalRequests] ON 

INSERT [dbo].[ApprovalRequests] ([AccountId], [InventoryId], [Quantity], [RequestType], [RequestDate], [Status], [ApproveDate], [IsApproved], [Id]) VALUES (N'1ba68635-67d8-4dbb-8937-cba49842217b', N'1a4a8018-4880-46d2-922e-3088984772c0', 4, N'Borrow', CAST(N'2024-08-12T21:16:12.2920087' AS DateTime2), N'Accept', CAST(N'2024-08-14T15:27:07.4681524' AS DateTime2), 1, 1)
INSERT [dbo].[ApprovalRequests] ([AccountId], [InventoryId], [Quantity], [RequestType], [RequestDate], [Status], [ApproveDate], [IsApproved], [Id]) VALUES (N'54fa708c-b193-4c4f-a541-f8d8f0057a8b', N'd01d8672-3d84-4d23-85e1-052162185821', 7, N'Borrow', CAST(N'2024-08-12T21:31:04.6188555' AS DateTime2), N'Accept', CAST(N'2024-08-14T15:27:01.9401938' AS DateTime2), 1, 2)
INSERT [dbo].[ApprovalRequests] ([AccountId], [InventoryId], [Quantity], [RequestType], [RequestDate], [Status], [ApproveDate], [IsApproved], [Id]) VALUES (N'1ba68635-67d8-4dbb-8937-cba49842217b', N'0706a594-6559-46e4-a40c-1523bfa7c2b7', 10, N'Buy', CAST(N'2024-08-14T15:26:03.9745414' AS DateTime2), N'Accept', CAST(N'2024-08-14T15:27:19.6263062' AS DateTime2), 1, 5)
INSERT [dbo].[ApprovalRequests] ([AccountId], [InventoryId], [Quantity], [RequestType], [RequestDate], [Status], [ApproveDate], [IsApproved], [Id]) VALUES (N'1ba68635-67d8-4dbb-8937-cba49842217b', N'0f1b06fc-e736-448f-b2c8-4f7c87650084', 7, N'Buy', CAST(N'2024-08-14T15:26:48.7778220' AS DateTime2), N'Accept', CAST(N'2024-08-23T16:53:06.9977408' AS DateTime2), 1, 6)
INSERT [dbo].[ApprovalRequests] ([AccountId], [InventoryId], [Quantity], [RequestType], [RequestDate], [Status], [ApproveDate], [IsApproved], [Id]) VALUES (N'1ba68635-67d8-4dbb-8937-cba49842217b', N'bb574986-d75d-4815-9a62-466de1fee29c', 3, N'Borrow', CAST(N'2024-08-14T15:29:24.2579549' AS DateTime2), N'Pending', NULL, 0, 7)
INSERT [dbo].[ApprovalRequests] ([AccountId], [InventoryId], [Quantity], [RequestType], [RequestDate], [Status], [ApproveDate], [IsApproved], [Id]) VALUES (N'1ba68635-67d8-4dbb-8937-cba49842217b', N'1a4a8018-4880-46d2-922e-3088984772c0', 2, N'Return', CAST(N'2024-08-14T15:31:46.0860260' AS DateTime2), N'Accept', CAST(N'2024-08-14T15:31:50.7075522' AS DateTime2), 1, 8)
SET IDENTITY_INSERT [dbo].[ApprovalRequests] OFF
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([Id], [Type]) VALUES (1, N'rubbish packs')
INSERT [dbo].[Categories] ([Id], [Type]) VALUES (2, N'Storage Sacks')
INSERT [dbo].[Categories] ([Id], [Type]) VALUES (3, N'Households')
INSERT [dbo].[Categories] ([Id], [Type]) VALUES (4, N'Maintenance Sprays ')
INSERT [dbo].[Categories] ([Id], [Type]) VALUES (5, N'Tapes')
INSERT [dbo].[Categories] ([Id], [Type]) VALUES (6, N'Marker Pens')
INSERT [dbo].[Categories] ([Id], [Type]) VALUES (7, N'Hand Cleaners')
INSERT [dbo].[Categories] ([Id], [Type]) VALUES (8, N'Electronics')
INSERT [dbo].[Categories] ([Id], [Type]) VALUES (9, N'School suppliers')
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
INSERT [dbo].[Inventories] ([Id], [TotalQuantity], [AmountBorrow], [ToolId]) VALUES (N'd01d8672-3d84-4d23-85e1-052162185821', 23, 7, 4)
INSERT [dbo].[Inventories] ([Id], [TotalQuantity], [AmountBorrow], [ToolId]) VALUES (N'0706a594-6559-46e4-a40c-1523bfa7c2b7', 70, 0, 5)
INSERT [dbo].[Inventories] ([Id], [TotalQuantity], [AmountBorrow], [ToolId]) VALUES (N'1a4a8018-4880-46d2-922e-3088984772c0', 29, 2, 3)
INSERT [dbo].[Inventories] ([Id], [TotalQuantity], [AmountBorrow], [ToolId]) VALUES (N'bb574986-d75d-4815-9a62-466de1fee29c', 32, 0, 2)
INSERT [dbo].[Inventories] ([Id], [TotalQuantity], [AmountBorrow], [ToolId]) VALUES (N'0f1b06fc-e736-448f-b2c8-4f7c87650084', 107, 0, 6)
INSERT [dbo].[Inventories] ([Id], [TotalQuantity], [AmountBorrow], [ToolId]) VALUES (N'6f90f78e-81f4-4597-9232-f385d39ca598', 17, 0, 1)
GO
SET IDENTITY_INSERT [dbo].[InventoryHistories] ON 

INSERT [dbo].[InventoryHistories] ([AccountId], [InventoryId], [Quantity], [ActionDate], [ActionType], [Id]) VALUES (N'54fa708c-b193-4c4f-a541-f8d8f0057a8b', N'd01d8672-3d84-4d23-85e1-052162185821', 7, CAST(N'2024-08-14T15:27:01.9405660' AS DateTime2), N'Borrow', 1)
INSERT [dbo].[InventoryHistories] ([AccountId], [InventoryId], [Quantity], [ActionDate], [ActionType], [Id]) VALUES (N'1ba68635-67d8-4dbb-8937-cba49842217b', N'1a4a8018-4880-46d2-922e-3088984772c0', 4, CAST(N'2024-08-14T15:27:07.4681540' AS DateTime2), N'Borrow', 2)
INSERT [dbo].[InventoryHistories] ([AccountId], [InventoryId], [Quantity], [ActionDate], [ActionType], [Id]) VALUES (N'1ba68635-67d8-4dbb-8937-cba49842217b', N'bb574986-d75d-4815-9a62-466de1fee29c', 7, CAST(N'2024-08-14T15:27:16.8442727' AS DateTime2), N'Buy', 3)
INSERT [dbo].[InventoryHistories] ([AccountId], [InventoryId], [Quantity], [ActionDate], [ActionType], [Id]) VALUES (N'1ba68635-67d8-4dbb-8937-cba49842217b', N'0706a594-6559-46e4-a40c-1523bfa7c2b7', 10, CAST(N'2024-08-14T15:27:19.6263072' AS DateTime2), N'Buy', 4)
INSERT [dbo].[InventoryHistories] ([AccountId], [InventoryId], [Quantity], [ActionDate], [ActionType], [Id]) VALUES (N'1ba68635-67d8-4dbb-8937-cba49842217b', N'1a4a8018-4880-46d2-922e-3088984772c0', 2, CAST(N'2024-08-14T15:31:50.7075538' AS DateTime2), N'Return', 5)
INSERT [dbo].[InventoryHistories] ([AccountId], [InventoryId], [Quantity], [ActionDate], [ActionType], [Id]) VALUES (N'1ba68635-67d8-4dbb-8937-cba49842217b', N'0f1b06fc-e736-448f-b2c8-4f7c87650084', 7, CAST(N'2024-08-23T16:53:06.9979531' AS DateTime2), N'Buy', 6)
SET IDENTITY_INSERT [dbo].[InventoryHistories] OFF
GO
SET IDENTITY_INSERT [dbo].[Invoices] ON 

INSERT [dbo].[Invoices] ([Id], [Price], [InvoiceDate], [ToolId]) VALUES (1, 34000000, CAST(N'2024-08-12T21:17:14.4820975' AS DateTime2), 1)
INSERT [dbo].[Invoices] ([Id], [Price], [InvoiceDate], [ToolId]) VALUES (2, 25000000, CAST(N'2024-08-12T21:17:24.2431479' AS DateTime2), 2)
INSERT [dbo].[Invoices] ([Id], [Price], [InvoiceDate], [ToolId]) VALUES (3, 2599999, CAST(N'2024-08-12T21:17:37.9767021' AS DateTime2), 4)
INSERT [dbo].[Invoices] ([Id], [Price], [InvoiceDate], [ToolId]) VALUES (4, 389400, CAST(N'2024-08-12T21:17:51.4306825' AS DateTime2), 5)
INSERT [dbo].[Invoices] ([Id], [Price], [InvoiceDate], [ToolId]) VALUES (5, 40000, CAST(N'2024-08-12T21:18:01.7628729' AS DateTime2), 6)
INSERT [dbo].[Invoices] ([Id], [Price], [InvoiceDate], [ToolId]) VALUES (6, 400000, CAST(N'2024-08-12T21:19:20.6439625' AS DateTime2), 2)
INSERT [dbo].[Invoices] ([Id], [Price], [InvoiceDate], [ToolId]) VALUES (7, 45000, CAST(N'2024-09-03T08:43:16.8684303' AS DateTime2), 3)
SET IDENTITY_INSERT [dbo].[Invoices] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([Id], [RoleName], [RoleDescription]) VALUES (1, N'admin', N'This role can manage all system')
INSERT [dbo].[Roles] ([Id], [RoleName], [RoleDescription]) VALUES (2, N'manager', N'This role can manage inventory, approve request')
INSERT [dbo].[Roles] ([Id], [RoleName], [RoleDescription]) VALUES (3, N'user', N'This role just can view inventory, create request')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[Suppliers] ON 

INSERT [dbo].[Suppliers] ([Id], [SupplierName], [ContactName], [Address], [Phone]) VALUES (1, N'Công ty Cổ Phần Tập Đoàn Hòa Phát', N'Trần Đình Long', N'171 Trường Chinh, P. An Khê, Q. Thanh Khê, Đà Nẵng', N'02363721232')
INSERT [dbo].[Suppliers] ([Id], [SupplierName], [ContactName], [Address], [Phone]) VALUES (2, N'Công ty Cổ Phần Tập Đoàn Hoa Sen', N'Lê Phước Vũ', N'Số 9, Đại lộ Thống Nhất, Khu công nghiệp Sóng Thần II, Phường Dĩ An, Thành phố Dĩ An, Tỉnh Bình Dương, Việt Nam.', N'18001515')
INSERT [dbo].[Suppliers] ([Id], [SupplierName], [ContactName], [Address], [Phone]) VALUES (3, N'Công ty Cổ Phần Tôn Đông Á', N'Tôn Giả Hoàng', N'Số 5, đường số 5, Khu Công Nghiệp Sóng Thần 1, phường Dĩ An, thành phố Dĩ An, tỉnh Bình Dương', N'02743790420')
INSERT [dbo].[Suppliers] ([Id], [SupplierName], [ContactName], [Address], [Phone]) VALUES (4, N'Công ty Cổ Phần Vicostone', N'Hồ Xuân Năng', N'Khu công nghệ cao Hòa Lạc, Thạch Hòa, Thạch Thất, Hà Nội', N'18006766')
INSERT [dbo].[Suppliers] ([Id], [SupplierName], [ContactName], [Address], [Phone]) VALUES (5, N'Tổng công ty Viglacera - CTCP', N'Nguyễn Văn Tuấn', N'Tòa nhà Viglacera, Số 1 Đại lộ Thăng Long, Hà Nội', N'35536660')
INSERT [dbo].[Suppliers] ([Id], [SupplierName], [ContactName], [Address], [Phone]) VALUES (6, N'Công ty Cổ Phần Eurowindow', N'Tống Gia Phát', N'Tòa nhà văn phòng Eurowindow Office Building, Số 02 Tôn Thất Tùng, Đống Đa, Hà Nội', N'37474777')
INSERT [dbo].[Suppliers] ([Id], [SupplierName], [ContactName], [Address], [Phone]) VALUES (7, N'Công ty Cổ Phần Công Nghiệp Vĩnh Tường', N'Vĩnh Thế Tương', N'Lô C23a, Khu Công Nghiệp Hiệp Phước, Xã Hiệp Phước, Huyện Nhà Bè, Thành phố Hồ Chí Minh, Việt Nam', N'0837818554')
SET IDENTITY_INSERT [dbo].[Suppliers] OFF
GO
INSERT [dbo].[ToolCategories] ([ToolId], [CategoryId]) VALUES (3, 3)
INSERT [dbo].[ToolCategories] ([ToolId], [CategoryId]) VALUES (4, 3)
INSERT [dbo].[ToolCategories] ([ToolId], [CategoryId]) VALUES (1, 6)
INSERT [dbo].[ToolCategories] ([ToolId], [CategoryId]) VALUES (5, 7)
INSERT [dbo].[ToolCategories] ([ToolId], [CategoryId]) VALUES (1, 8)
INSERT [dbo].[ToolCategories] ([ToolId], [CategoryId]) VALUES (2, 8)
INSERT [dbo].[ToolCategories] ([ToolId], [CategoryId]) VALUES (4, 8)
INSERT [dbo].[ToolCategories] ([ToolId], [CategoryId]) VALUES (6, 9)
GO
SET IDENTITY_INSERT [dbo].[Tools] ON 

INSERT [dbo].[Tools] ([Id], [ToolName], [Description], [SupplierId], [Avatar], [SpoFileId], [Unit]) VALUES (1, N'Bục giảng thông minh', N'Trang bị màn hình cảm ứng độ phân giải cao
 Trang bị micro, camera vật thể hoặc laptop của giáo viên
Tích hợp hệ thống điều khiển trung tâm cho tất cả các thiết bị
Điều chỉnh độ cao phù hợp với giáo viên', 3, N'https://trunghieu1204.sharepoint.com/sites/FamilyTree/Avatars/images.jpg', N'01TFPUVZ5CHAUJ34KTJRGK3EFK6G6MZINN', N'pieces')
INSERT [dbo].[Tools] ([Id], [ToolName], [Description], [SupplierId], [Avatar], [SpoFileId], [Unit]) VALUES (2, N'Laptop', N'Laptop hay còn gọi là máy tính xách tay là một chiếc máy tính cá nhân giúp dễ dàng mang đi và làm việc ở những địa điểm và địa hình khác nhau', 6, N'https://trunghieu1204.sharepoint.com/sites/FamilyTree/Avatars/images%20(1).jpg', N'01TFPUVZ2VYA5VAWZ6ZFFKHJXUDPSRT6W4', N'pieces')
INSERT [dbo].[Tools] ([Id], [ToolName], [Description], [SupplierId], [Avatar], [SpoFileId], [Unit]) VALUES (3, N'Chổi', N'Chổi là dụng cụ có công dụng loại sạch bụi bẩn trong không gian sống. Sản phẩm có thiết kế cán dài với đầu chổi dáng tam giác, hỗ trợ quét sạch rác ở mọi ngóc ngách trong nhà.', 3, N'https://trunghieu1204.sharepoint.com/sites/FamilyTree/Avatars/image2.jpg', N'01TFPUVZ52TAGSSXHR6ZGJTN2VPLLGPBCI', N'pieces')
INSERT [dbo].[Tools] ([Id], [ToolName], [Description], [SupplierId], [Avatar], [SpoFileId], [Unit]) VALUES (4, N'Máy hút bụi', N'Máy hút bụi là sản phẩm giúp hút sạch bụi bẩn trong nhà một cách nhanh chóng, phù hợp với những gia đình có diện tích rộng rãi. Sản phẩm có thiết kế nhỏ gọn và công suất lớn, giúp cho không gian sống được làm sạch hiệu quả.', 3, N'https://trunghieu1204.sharepoint.com/sites/FamilyTree/Avatars/images%20(2).jpg', N'01TFPUVZZOG3NTAVPA2RF2X2W6YA4IWZBF', N'set')
INSERT [dbo].[Tools] ([Id], [ToolName], [Description], [SupplierId], [Avatar], [SpoFileId], [Unit]) VALUES (5, N'Chai xịt nước', N'Với các chai xịt, bạn sẽ dễ dàng kiểm soát được lượng nước, lượng chất tẩy rửa phun lên bề mặt của vật dụng, phun đúng vị trí cần làm sạch.', 1, N'https://trunghieu1204.sharepoint.com/sites/FamilyTree/Avatars/Binh-xit-nuoc-Wahl-3.jpg', N'01TFPUVZ4K452S6IZG2NEJD4UHMIYAX65K', N'bottle')
INSERT [dbo].[Tools] ([Id], [ToolName], [Description], [SupplierId], [Avatar], [SpoFileId], [Unit]) VALUES (6, N'Máy gọt bút chì ', N'Tự động gọt/Sử dụng dễ dàng tiện lợi.
Hộp đựng trong suốt, dễ dàng vệ sinh
Khi mở hộp máy, thiết bị sẽ tự động dừng hoạt động, an toàn sử dụng.
Lưỡi dao làm từ hợp kim thép, gọt mịn, nhanh chóng.
Tiết kiệm thời gian, chỉ mất 4 giây gọt bút chì cũ, với bút chì mới chỉ cần 6 giây
Thiết kế sản phẩm chắc chắn, bền với chất liệu nhựa ABS cao cấp, có đế chống trượt.
Thiết kế với hệ thống bảo vệ nhiệt, tránh máy quá nóng', 4, N'https://trunghieu1204.sharepoint.com/sites/FamilyTree/Avatars/m_1596788079_may-got-but-chi-2.jpg', N'01TFPUVZ5HHOKTV4KUG5HLQICF7VY3NFRS', N'set')
SET IDENTITY_INSERT [dbo].[Tools] OFF
GO
/****** Object:  Index [IX_Accounts_Id]    Script Date: 03/09/2024 8:45:47 SA ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Accounts_Id] ON [dbo].[Accounts]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Accounts_RoleId]    Script Date: 03/09/2024 8:45:47 SA ******/
CREATE NONCLUSTERED INDEX [IX_Accounts_RoleId] ON [dbo].[Accounts]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ApprovalRequests_AccountId]    Script Date: 03/09/2024 8:45:47 SA ******/
CREATE NONCLUSTERED INDEX [IX_ApprovalRequests_AccountId] ON [dbo].[ApprovalRequests]
(
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ApprovalRequests_InventoryId]    Script Date: 03/09/2024 8:45:47 SA ******/
CREATE NONCLUSTERED INDEX [IX_ApprovalRequests_InventoryId] ON [dbo].[ApprovalRequests]
(
	[InventoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Inventories_ToolId]    Script Date: 03/09/2024 8:45:47 SA ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Inventories_ToolId] ON [dbo].[Inventories]
(
	[ToolId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_InventoryHistories_AccountId]    Script Date: 03/09/2024 8:45:47 SA ******/
CREATE NONCLUSTERED INDEX [IX_InventoryHistories_AccountId] ON [dbo].[InventoryHistories]
(
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_InventoryHistories_InventoryId]    Script Date: 03/09/2024 8:45:47 SA ******/
CREATE NONCLUSTERED INDEX [IX_InventoryHistories_InventoryId] ON [dbo].[InventoryHistories]
(
	[InventoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Invoices_ToolId]    Script Date: 03/09/2024 8:45:47 SA ******/
CREATE NONCLUSTERED INDEX [IX_Invoices_ToolId] ON [dbo].[Invoices]
(
	[ToolId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ToolCategories_CategoryId]    Script Date: 03/09/2024 8:45:47 SA ******/
CREATE NONCLUSTERED INDEX [IX_ToolCategories_CategoryId] ON [dbo].[ToolCategories]
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Tools_SupplierId]    Script Date: 03/09/2024 8:45:47 SA ******/
CREATE NONCLUSTERED INDEX [IX_Tools_SupplierId] ON [dbo].[Tools]
(
	[SupplierId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AccountDetails] ADD  DEFAULT (N'') FOR [FullName]
GO
ALTER TABLE [dbo].[AccountDetails] ADD  DEFAULT (N'') FOR [Address]
GO
ALTER TABLE [dbo].[AccountDetails] ADD  DEFAULT (N'') FOR [Phone]
GO
ALTER TABLE [dbo].[AccountDetails] ADD  DEFAULT (N'') FOR [Avatar]
GO
ALTER TABLE [dbo].[ApprovalRequests] ADD  DEFAULT ((0)) FOR [Quantity]
GO
ALTER TABLE [dbo].[ApprovalRequests] ADD  DEFAULT ('2024-08-17T09:07:16.8290519+07:00') FOR [RequestDate]
GO
ALTER TABLE [dbo].[ApprovalRequests] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsApproved]
GO
ALTER TABLE [dbo].[Inventories] ADD  DEFAULT ((0)) FOR [TotalQuantity]
GO
ALTER TABLE [dbo].[Inventories] ADD  DEFAULT ((0)) FOR [AmountBorrow]
GO
ALTER TABLE [dbo].[InventoryHistories] ADD  DEFAULT ((0)) FOR [Quantity]
GO
ALTER TABLE [dbo].[InventoryHistories] ADD  DEFAULT ('2024-08-17T09:07:16.8303035+07:00') FOR [ActionDate]
GO
ALTER TABLE [dbo].[Invoices] ADD  DEFAULT ((0.0000000000000000e+000)) FOR [Price]
GO
ALTER TABLE [dbo].[Invoices] ADD  DEFAULT ('2024-08-17T09:07:16.8282931+07:00') FOR [InvoiceDate]
GO
ALTER TABLE [dbo].[Tools] ADD  DEFAULT (N'') FOR [Unit]
GO
ALTER TABLE [dbo].[AccountDetails]  WITH CHECK ADD  CONSTRAINT [FK_AccountDetails_Accounts_AccountId] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Accounts] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AccountDetails] CHECK CONSTRAINT [FK_AccountDetails_Accounts_AccountId]
GO
ALTER TABLE [dbo].[Accounts]  WITH CHECK ADD  CONSTRAINT [FK_Accounts_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Accounts] CHECK CONSTRAINT [FK_Accounts_Roles_RoleId]
GO
ALTER TABLE [dbo].[ApprovalRequests]  WITH CHECK ADD  CONSTRAINT [FK_ApprovalRequests_Accounts_AccountId] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Accounts] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ApprovalRequests] CHECK CONSTRAINT [FK_ApprovalRequests_Accounts_AccountId]
GO
ALTER TABLE [dbo].[ApprovalRequests]  WITH CHECK ADD  CONSTRAINT [FK_ApprovalRequests_Inventories_InventoryId] FOREIGN KEY([InventoryId])
REFERENCES [dbo].[Inventories] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ApprovalRequests] CHECK CONSTRAINT [FK_ApprovalRequests_Inventories_InventoryId]
GO
ALTER TABLE [dbo].[Inventories]  WITH CHECK ADD  CONSTRAINT [FK_Inventories_Tools_ToolId] FOREIGN KEY([ToolId])
REFERENCES [dbo].[Tools] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Inventories] CHECK CONSTRAINT [FK_Inventories_Tools_ToolId]
GO
ALTER TABLE [dbo].[InventoryHistories]  WITH CHECK ADD  CONSTRAINT [FK_InventoryHistories_Accounts_AccountId] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Accounts] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[InventoryHistories] CHECK CONSTRAINT [FK_InventoryHistories_Accounts_AccountId]
GO
ALTER TABLE [dbo].[InventoryHistories]  WITH CHECK ADD  CONSTRAINT [FK_InventoryHistories_Inventories_InventoryId] FOREIGN KEY([InventoryId])
REFERENCES [dbo].[Inventories] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[InventoryHistories] CHECK CONSTRAINT [FK_InventoryHistories_Inventories_InventoryId]
GO
ALTER TABLE [dbo].[Invoices]  WITH CHECK ADD  CONSTRAINT [FK_Invoices_Tools_ToolId] FOREIGN KEY([ToolId])
REFERENCES [dbo].[Tools] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Invoices] CHECK CONSTRAINT [FK_Invoices_Tools_ToolId]
GO
ALTER TABLE [dbo].[ToolCategories]  WITH CHECK ADD  CONSTRAINT [FK_ToolCategories_Categories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ToolCategories] CHECK CONSTRAINT [FK_ToolCategories_Categories_CategoryId]
GO
ALTER TABLE [dbo].[ToolCategories]  WITH CHECK ADD  CONSTRAINT [FK_ToolCategories_Tools_ToolId] FOREIGN KEY([ToolId])
REFERENCES [dbo].[Tools] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ToolCategories] CHECK CONSTRAINT [FK_ToolCategories_Tools_ToolId]
GO
ALTER TABLE [dbo].[Tools]  WITH CHECK ADD  CONSTRAINT [FK_Tools_Suppliers_SupplierId] FOREIGN KEY([SupplierId])
REFERENCES [dbo].[Suppliers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Tools] CHECK CONSTRAINT [FK_Tools_Suppliers_SupplierId]
GO
USE [master]
GO
ALTER DATABASE [TeachEquipManagement] SET  READ_WRITE 
GO
