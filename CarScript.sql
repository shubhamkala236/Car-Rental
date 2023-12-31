USE [master]
GO
/****** Object:  Database [CarRentDb]    Script Date: 19-09-2023 12:47:03 ******/
CREATE DATABASE [CarRentDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CarRentDb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\CarRentDb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CarRentDb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\CarRentDb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [CarRentDb] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CarRentDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CarRentDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CarRentDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CarRentDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CarRentDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CarRentDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [CarRentDb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CarRentDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CarRentDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CarRentDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CarRentDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CarRentDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CarRentDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CarRentDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CarRentDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CarRentDb] SET  ENABLE_BROKER 
GO
ALTER DATABASE [CarRentDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CarRentDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CarRentDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CarRentDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CarRentDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CarRentDb] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [CarRentDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CarRentDb] SET RECOVERY FULL 
GO
ALTER DATABASE [CarRentDb] SET  MULTI_USER 
GO
ALTER DATABASE [CarRentDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CarRentDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CarRentDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CarRentDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CarRentDb] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'CarRentDb', N'ON'
GO
ALTER DATABASE [CarRentDb] SET QUERY_STORE = OFF
GO
USE [CarRentDb]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 19-09-2023 12:47:04 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cars]    Script Date: 19-09-2023 12:47:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cars](
	[CarId] [int] IDENTITY(1,1) NOT NULL,
	[Maker] [nvarchar](max) NOT NULL,
	[Model] [nvarchar](max) NOT NULL,
	[RentPrice] [int] NOT NULL,
	[AvailibilityStatus] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Cars] PRIMARY KEY CLUSTERED 
(
	[CarId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RentalAgreements]    Script Date: 19-09-2023 12:47:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RentalAgreements](
	[AgreementId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[CarId] [int] NOT NULL,
	[RentalDuration] [int] NOT NULL,
	[TotalCost] [int] NOT NULL,
	[IsAccepted] [bit] NOT NULL,
	[IsReturnRequested] [bit] NOT NULL,
	[IsReturned] [bit] NOT NULL,
	[FromDate] [datetime2](7) NOT NULL,
	[ToDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_RentalAgreements] PRIMARY KEY CLUSTERED 
(
	[AgreementId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 19-09-2023 12:47:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[IsAdmin] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230913122018_first', N'5.0.17')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230913132419_Added Regex Email', N'5.0.17')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230913191821_AcceptAgreement table added', N'5.0.17')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230913192105_AcceptedTable', N'5.0.17')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230913193936_Upated accepted user table with userId', N'5.0.17')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230913194541_Removed userId from AcceptedTable', N'5.0.17')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230915090553_Added Date in Agreement', N'5.0.17')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230915165712_Changed model car', N'5.0.17')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230917145117_deleted one table', N'5.0.17')
GO
SET IDENTITY_INSERT [dbo].[Cars] ON 

INSERT [dbo].[Cars] ([CarId], [Maker], [Model], [RentPrice], [AvailibilityStatus]) VALUES (1, N'Honda', N'Amaze', 3000, N'available')
INSERT [dbo].[Cars] ([CarId], [Maker], [Model], [RentPrice], [AvailibilityStatus]) VALUES (2, N'Maruti Suzuki', N'Swift', 2000, N'available')
INSERT [dbo].[Cars] ([CarId], [Maker], [Model], [RentPrice], [AvailibilityStatus]) VALUES (3, N'BMW', N'X7', 8000, N'available')
INSERT [dbo].[Cars] ([CarId], [Maker], [Model], [RentPrice], [AvailibilityStatus]) VALUES (4, N'Mercedes', N'Benz', 9000, N'available')
INSERT [dbo].[Cars] ([CarId], [Maker], [Model], [RentPrice], [AvailibilityStatus]) VALUES (5, N'Porche', N'Panamera', 22000, N'available')
INSERT [dbo].[Cars] ([CarId], [Maker], [Model], [RentPrice], [AvailibilityStatus]) VALUES (7, N'BMW', N'X001Series', 20000, N'unavailable')
INSERT [dbo].[Cars] ([CarId], [Maker], [Model], [RentPrice], [AvailibilityStatus]) VALUES (1001, N'Mahindra', N'Scorpio', 2000, N'unavailable')
SET IDENTITY_INSERT [dbo].[Cars] OFF
GO
SET IDENTITY_INSERT [dbo].[RentalAgreements] ON 

INSERT [dbo].[RentalAgreements] ([AgreementId], [UserId], [CarId], [RentalDuration], [TotalCost], [IsAccepted], [IsReturnRequested], [IsReturned], [FromDate], [ToDate]) VALUES (6, 4, 5, 2, 44000, 1, 1, 1, CAST(N'2023-09-17T20:31:43.0826478' AS DateTime2), CAST(N'2023-09-19T20:31:43.0826512' AS DateTime2))
INSERT [dbo].[RentalAgreements] ([AgreementId], [UserId], [CarId], [RentalDuration], [TotalCost], [IsAccepted], [IsReturnRequested], [IsReturned], [FromDate], [ToDate]) VALUES (9, 3, 2, 5, 10000, 1, 1, 1, CAST(N'2023-09-15T15:26:15.7884290' AS DateTime2), CAST(N'2023-09-20T15:26:15.7884375' AS DateTime2))
INSERT [dbo].[RentalAgreements] ([AgreementId], [UserId], [CarId], [RentalDuration], [TotalCost], [IsAccepted], [IsReturnRequested], [IsReturned], [FromDate], [ToDate]) VALUES (11, 2, 3, 5, 40000, 1, 1, 1, CAST(N'2023-09-17T20:31:55.0623504' AS DateTime2), CAST(N'2023-09-22T20:31:55.0623566' AS DateTime2))
INSERT [dbo].[RentalAgreements] ([AgreementId], [UserId], [CarId], [RentalDuration], [TotalCost], [IsAccepted], [IsReturnRequested], [IsReturned], [FromDate], [ToDate]) VALUES (14, 3, 5, 1, 22000, 1, 1, 1, CAST(N'2023-09-15T16:06:52.2308459' AS DateTime2), CAST(N'2023-09-16T16:06:52.2308476' AS DateTime2))
INSERT [dbo].[RentalAgreements] ([AgreementId], [UserId], [CarId], [RentalDuration], [TotalCost], [IsAccepted], [IsReturnRequested], [IsReturned], [FromDate], [ToDate]) VALUES (1014, 3, 5, 2, 44000, 1, 1, 1, CAST(N'2023-09-15T23:40:23.1259762' AS DateTime2), CAST(N'2023-09-17T23:40:23.1259810' AS DateTime2))
INSERT [dbo].[RentalAgreements] ([AgreementId], [UserId], [CarId], [RentalDuration], [TotalCost], [IsAccepted], [IsReturnRequested], [IsReturned], [FromDate], [ToDate]) VALUES (1015, 4, 1, 7, 21000, 1, 1, 1, CAST(N'2023-09-15T22:49:38.7212209' AS DateTime2), CAST(N'2023-09-22T22:49:38.7212241' AS DateTime2))
INSERT [dbo].[RentalAgreements] ([AgreementId], [UserId], [CarId], [RentalDuration], [TotalCost], [IsAccepted], [IsReturnRequested], [IsReturned], [FromDate], [ToDate]) VALUES (1017, 4, 2, 1, 2000, 1, 1, 1, CAST(N'2023-09-15T23:31:19.8821713' AS DateTime2), CAST(N'2023-09-16T23:31:19.8821754' AS DateTime2))
INSERT [dbo].[RentalAgreements] ([AgreementId], [UserId], [CarId], [RentalDuration], [TotalCost], [IsAccepted], [IsReturnRequested], [IsReturned], [FromDate], [ToDate]) VALUES (1018, 1, 1, 2, 6000, 1, 1, 1, CAST(N'2023-09-19T11:22:54.5471240' AS DateTime2), CAST(N'2023-09-21T11:22:54.5471263' AS DateTime2))
INSERT [dbo].[RentalAgreements] ([AgreementId], [UserId], [CarId], [RentalDuration], [TotalCost], [IsAccepted], [IsReturnRequested], [IsReturned], [FromDate], [ToDate]) VALUES (1020, 3, 1001, 2, 4000, 1, 1, 1, CAST(N'2023-09-17T20:32:17.8430393' AS DateTime2), CAST(N'2023-09-19T20:32:17.8430468' AS DateTime2))
INSERT [dbo].[RentalAgreements] ([AgreementId], [UserId], [CarId], [RentalDuration], [TotalCost], [IsAccepted], [IsReturnRequested], [IsReturned], [FromDate], [ToDate]) VALUES (1024, 4, 2, 4, 8000, 0, 0, 0, CAST(N'2023-09-19T11:22:12.4584848' AS DateTime2), CAST(N'2023-09-23T11:22:12.4594246' AS DateTime2))
INSERT [dbo].[RentalAgreements] ([AgreementId], [UserId], [CarId], [RentalDuration], [TotalCost], [IsAccepted], [IsReturnRequested], [IsReturned], [FromDate], [ToDate]) VALUES (1025, 3, 1001, 3, 6000, 1, 1, 0, CAST(N'2023-09-18T12:30:58.3932681' AS DateTime2), CAST(N'2023-09-21T12:30:58.3937571' AS DateTime2))
SET IDENTITY_INSERT [dbo].[RentalAgreements] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserId], [Name], [Email], [Password], [IsAdmin]) VALUES (1, N'Rohan', N'rohan@gmail.com', N'rohan123', 0)
INSERT [dbo].[Users] ([UserId], [Name], [Email], [Password], [IsAdmin]) VALUES (2, N'Shubham', N'shubham@gmail.com', N'shubham', 1)
INSERT [dbo].[Users] ([UserId], [Name], [Email], [Password], [IsAdmin]) VALUES (3, N'Raj', N'raj@gmail.com', N'raj123', 0)
INSERT [dbo].[Users] ([UserId], [Name], [Email], [Password], [IsAdmin]) VALUES (4, N'Ramesh', N'ramesh@gmail.com', N'ramesh123', 0)
INSERT [dbo].[Users] ([UserId], [Name], [Email], [Password], [IsAdmin]) VALUES (5, N'Admin', N'admin@gmail.com', N'admin123', 1)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[RentalAgreements] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [FromDate]
GO
ALTER TABLE [dbo].[RentalAgreements] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [ToDate]
GO
USE [master]
GO
ALTER DATABASE [CarRentDb] SET  READ_WRITE 
GO
