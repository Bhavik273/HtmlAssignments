USE [master]
GO
/****** Object:  Database [HoteManagement]    Script Date: 13-01-2021 21:06:59 ******/
CREATE DATABASE [HoteManagement]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'HoteManagement', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\HoteManagement.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'HoteManagement_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\HoteManagement_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [HoteManagement] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [HoteManagement].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [HoteManagement] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [HoteManagement] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [HoteManagement] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [HoteManagement] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [HoteManagement] SET ARITHABORT OFF 
GO
ALTER DATABASE [HoteManagement] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [HoteManagement] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [HoteManagement] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [HoteManagement] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [HoteManagement] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [HoteManagement] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [HoteManagement] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [HoteManagement] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [HoteManagement] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [HoteManagement] SET  DISABLE_BROKER 
GO
ALTER DATABASE [HoteManagement] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [HoteManagement] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [HoteManagement] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [HoteManagement] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [HoteManagement] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [HoteManagement] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [HoteManagement] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [HoteManagement] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [HoteManagement] SET  MULTI_USER 
GO
ALTER DATABASE [HoteManagement] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [HoteManagement] SET DB_CHAINING OFF 
GO
ALTER DATABASE [HoteManagement] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [HoteManagement] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [HoteManagement] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [HoteManagement] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [HoteManagement] SET QUERY_STORE = OFF
GO
USE [HoteManagement]
GO
/****** Object:  Table [dbo].[Bookings]    Script Date: 13-01-2021 21:06:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bookings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BookingDate] [datetime] NOT NULL,
	[RoomId] [int] NOT NULL,
	[Status] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Bookings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Hotels]    Script Date: 13-01-2021 21:06:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hotels](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](100) NOT NULL,
	[City] [nvarchar](50) NOT NULL,
	[Pincode] [nvarchar](10) NOT NULL,
	[ContactNumber] [nvarchar](50) NOT NULL,
	[ContactPerson] [nvarchar](20) NOT NULL,
	[Website] [nvarchar](50) NOT NULL,
	[Facebook] [nvarchar](50) NOT NULL,
	[Twitter] [nvarchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreateBy] [nvarchar](50) NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_Hotels] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rooms]    Script Date: 13-01-2021 21:06:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rooms](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Category] [nvarchar](20) NOT NULL,
	[Price] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
	[HotelId] [int] NOT NULL,
 CONSTRAINT [PK_Roomw] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Hotels] ADD  CONSTRAINT [DF_Hotels_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Bookings]  WITH CHECK ADD  CONSTRAINT [FK_Bookings_Rooms_Is] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Rooms] ([Id])
GO
ALTER TABLE [dbo].[Bookings] CHECK CONSTRAINT [FK_Bookings_Rooms_Is]
GO
ALTER TABLE [dbo].[Rooms]  WITH CHECK ADD  CONSTRAINT [FK_Rooms_Hotels_Id] FOREIGN KEY([HotelId])
REFERENCES [dbo].[Hotels] ([Id])
GO
ALTER TABLE [dbo].[Rooms] CHECK CONSTRAINT [FK_Rooms_Hotels_Id]
GO
USE [master]
GO
ALTER DATABASE [HoteManagement] SET  READ_WRITE 
GO
