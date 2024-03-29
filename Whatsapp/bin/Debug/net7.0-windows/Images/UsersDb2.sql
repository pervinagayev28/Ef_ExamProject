USE [master]
GO
/****** Object:  Database [users]    Script Date: 12/14/2023 6:59:08 PM ******/
CREATE DATABASE [users]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'users', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQL\MSSQL\DATA\users.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'users_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQL\MSSQL\DATA\users_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [users] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [users].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [users] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [users] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [users] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [users] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [users] SET ARITHABORT OFF 
GO
ALTER DATABASE [users] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [users] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [users] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [users] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [users] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [users] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [users] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [users] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [users] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [users] SET  DISABLE_BROKER 
GO
ALTER DATABASE [users] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [users] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [users] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [users] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [users] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [users] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [users] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [users] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [users] SET  MULTI_USER 
GO
ALTER DATABASE [users] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [users] SET DB_CHAINING OFF 
GO
ALTER DATABASE [users] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [users] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [users] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [users] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [users] SET QUERY_STORE = ON
GO
ALTER DATABASE [users] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [users]
GO
USE [users]
GO
/****** Object:  Sequence [dbo].[my_sequence]    Script Date: 12/14/2023 6:59:08 PM ******/
CREATE SEQUENCE [dbo].[my_sequence] 
 AS [int]
 START WITH 1
 INCREMENT BY 1
 MINVALUE -2147483648
 MAXVALUE 2147483647
 CACHE 
GO
/****** Object:  Table [dbo].[course]    Script Date: 12/14/2023 6:59:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[course](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_course] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CoursesAndstudents]    Script Date: 12/14/2023 6:59:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CoursesAndstudents](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CourseId] [int] NOT NULL,
	[StudentId] [int] NOT NULL,
 CONSTRAINT [PK_CoursesAndstudents] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Gen]    Script Date: 12/14/2023 6:59:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gen](
	[Id] [int] NOT NULL,
	[Kind] [nvarchar](10) NULL,
 CONSTRAINT [PK_Gen] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[studentss]    Script Date: 12/14/2023 6:59:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[studentss](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](20) NOT NULL,
	[surname] [nvarchar](20) NOT NULL,
	[GenId] [int] NULL,
	[IsDeleted] [int] NULL,
 CONSTRAINT [PK_studentss] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[teachers]    Script Date: 12/14/2023 6:59:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[teachers](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](20) NOT NULL,
	[surname] [nvarchar](20) NOT NULL,
	[profession] [nvarchar](20) NOT NULL
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[course] ON 

INSERT [dbo].[course] ([id], [name]) VALUES (3, N'step_it')
INSERT [dbo].[course] ([id], [name]) VALUES (4, N'code')
INSERT [dbo].[course] ([id], [name]) VALUES (5, N'prestji_s')
INSERT [dbo].[course] ([id], [name]) VALUES (6, N'accent_ac')
SET IDENTITY_INSERT [dbo].[course] OFF
GO
SET IDENTITY_INSERT [dbo].[CoursesAndstudents] ON 

INSERT [dbo].[CoursesAndstudents] ([Id], [CourseId], [StudentId]) VALUES (1, 3, 1)
INSERT [dbo].[CoursesAndstudents] ([Id], [CourseId], [StudentId]) VALUES (2, 3, 2)
INSERT [dbo].[CoursesAndstudents] ([Id], [CourseId], [StudentId]) VALUES (3, 4, 6)
INSERT [dbo].[CoursesAndstudents] ([Id], [CourseId], [StudentId]) VALUES (4, 4, 7)
INSERT [dbo].[CoursesAndstudents] ([Id], [CourseId], [StudentId]) VALUES (5, 5, 10)
INSERT [dbo].[CoursesAndstudents] ([Id], [CourseId], [StudentId]) VALUES (6, 5, 11)
INSERT [dbo].[CoursesAndstudents] ([Id], [CourseId], [StudentId]) VALUES (7, 6, 12)
INSERT [dbo].[CoursesAndstudents] ([Id], [CourseId], [StudentId]) VALUES (8, 3, 12)
INSERT [dbo].[CoursesAndstudents] ([Id], [CourseId], [StudentId]) VALUES (10, 6, 2)
INSERT [dbo].[CoursesAndstudents] ([Id], [CourseId], [StudentId]) VALUES (11, 4, 2)
SET IDENTITY_INSERT [dbo].[CoursesAndstudents] OFF
GO
INSERT [dbo].[Gen] ([Id], [Kind]) VALUES (1, N'male')
INSERT [dbo].[Gen] ([Id], [Kind]) VALUES (2, N'female')
GO
SET IDENTITY_INSERT [dbo].[studentss] ON 

INSERT [dbo].[studentss] ([id], [name], [surname], [GenId], [IsDeleted]) VALUES (1, N'samir', N'agayev', 1, 1)
INSERT [dbo].[studentss] ([id], [name], [surname], [GenId], [IsDeleted]) VALUES (2, N'isa', N'aliyev', 1, 0)
INSERT [dbo].[studentss] ([id], [name], [surname], [GenId], [IsDeleted]) VALUES (6, N'eli', N'memmedli', 1, 0)
INSERT [dbo].[studentss] ([id], [name], [surname], [GenId], [IsDeleted]) VALUES (7, N'vasif', N'kerimli', 1, 0)
INSERT [dbo].[studentss] ([id], [name], [surname], [GenId], [IsDeleted]) VALUES (10, N'for', N'example', 1, 0)
INSERT [dbo].[studentss] ([id], [name], [surname], [GenId], [IsDeleted]) VALUES (11, N'Salman', N'Salmanov', 1, 1)
INSERT [dbo].[studentss] ([id], [name], [surname], [GenId], [IsDeleted]) VALUES (12, N'nazli', N'rzazade', 2, 0)
INSERT [dbo].[studentss] ([id], [name], [surname], [GenId], [IsDeleted]) VALUES (13, N'nurane', N'ismayilzade', 2, 0)
INSERT [dbo].[studentss] ([id], [name], [surname], [GenId], [IsDeleted]) VALUES (14, N'leman', N'islamli', 2, 0)
INSERT [dbo].[studentss] ([id], [name], [surname], [GenId], [IsDeleted]) VALUES (15, N'aqil', N'memmedov', 1, 1)
INSERT [dbo].[studentss] ([id], [name], [surname], [GenId], [IsDeleted]) VALUES (16, N'fermayil', N'hesenov', 1, 1)
INSERT [dbo].[studentss] ([id], [name], [surname], [GenId], [IsDeleted]) VALUES (17, N'sdfsdf', N'sdsdfsd', NULL, 1)
INSERT [dbo].[studentss] ([id], [name], [surname], [GenId], [IsDeleted]) VALUES (18, N'changed', N'"hello"', NULL, 0)
INSERT [dbo].[studentss] ([id], [name], [surname], [GenId], [IsDeleted]) VALUES (20, N'sasd', N'sasd', NULL, NULL)
INSERT [dbo].[studentss] ([id], [name], [surname], [GenId], [IsDeleted]) VALUES (22, N'Hesen', N'Nagizade', 1, 0)
INSERT [dbo].[studentss] ([id], [name], [surname], [GenId], [IsDeleted]) VALUES (25, N'updated', N'updated', 1, 0)
INSERT [dbo].[studentss] ([id], [name], [surname], [GenId], [IsDeleted]) VALUES (26, N'asdas', N'updated', 1, 0)
SET IDENTITY_INSERT [dbo].[studentss] OFF
GO
SET IDENTITY_INSERT [dbo].[teachers] ON 

INSERT [dbo].[teachers] ([id], [name], [surname], [profession]) VALUES (1, N'cavid', N'atamoglanov', N'back-end')
INSERT [dbo].[teachers] ([id], [name], [surname], [profession]) VALUES (2, N'ismayil', N'seyidmemmedli', N'back-end')
INSERT [dbo].[teachers] ([id], [name], [surname], [profession]) VALUES (3, N'rustem', N'efendiyev', N'russian-language')
INSERT [dbo].[teachers] ([id], [name], [surname], [profession]) VALUES (4, N'umud', N'alcanov', N'math-language')
SET IDENTITY_INSERT [dbo].[teachers] OFF
GO
ALTER TABLE [dbo].[CoursesAndstudents]  WITH CHECK ADD FOREIGN KEY([CourseId])
REFERENCES [dbo].[course] ([id])
GO
ALTER TABLE [dbo].[CoursesAndstudents]  WITH CHECK ADD FOREIGN KEY([StudentId])
REFERENCES [dbo].[studentss] ([id])
GO
ALTER TABLE [dbo].[studentss]  WITH CHECK ADD FOREIGN KEY([GenId])
REFERENCES [dbo].[Gen] ([Id])
GO
USE [master]
GO
ALTER DATABASE [users] SET  READ_WRITE 
GO
