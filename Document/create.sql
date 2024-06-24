USE [master]
GO
/****** Object:  Database [QLySanBong]    Script Date: 6/24/2024 11:35:07 PM ******/
CREATE DATABASE [QLySanBong]
 
GO
ALTER DATABASE [QLySanBong] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QLySanBong] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QLySanBong] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QLySanBong] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QLySanBong] SET ARITHABORT OFF 
GO
ALTER DATABASE [QLySanBong] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [QLySanBong] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QLySanBong] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QLySanBong] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QLySanBong] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QLySanBong] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QLySanBong] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QLySanBong] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QLySanBong] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QLySanBong] SET  DISABLE_BROKER 
GO
ALTER DATABASE [QLySanBong] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QLySanBong] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QLySanBong] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QLySanBong] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QLySanBong] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QLySanBong] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QLySanBong] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QLySanBong] SET RECOVERY FULL 
GO
ALTER DATABASE [QLySanBong] SET  MULTI_USER 
GO
ALTER DATABASE [QLySanBong] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QLySanBong] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QLySanBong] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QLySanBong] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [QLySanBong] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [QLySanBong] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'QLySanBong', N'ON'
GO
ALTER DATABASE [QLySanBong] SET QUERY_STORE = ON
GO
ALTER DATABASE [QLySanBong] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [QLySanBong]
GO
USE [QLySanBong]
GO
/****** Object:  Sequence [dbo].[seq_AllCodeInfo]    Script Date: 6/24/2024 11:35:09 PM ******/
CREATE SEQUENCE [dbo].[seq_AllCodeInfo] 
 AS [bigint]
 START WITH 1
 INCREMENT BY 1
 MINVALUE -9223372036854775808
 MAXVALUE 9223372036854775807
 CACHE 
GO
USE [QLySanBong]
GO
/****** Object:  Sequence [dbo].[seq_ChiTietHoaDon]    Script Date: 6/24/2024 11:35:09 PM ******/
CREATE SEQUENCE [dbo].[seq_ChiTietHoaDon] 
 AS [bigint]
 START WITH 1
 INCREMENT BY 1
 MINVALUE -9223372036854775808
 MAXVALUE 9223372036854775807
 CACHE 
GO
USE [QLySanBong]
GO
/****** Object:  Sequence [dbo].[seq_DonDatSan]    Script Date: 6/24/2024 11:35:09 PM ******/
CREATE SEQUENCE [dbo].[seq_DonDatSan] 
 AS [bigint]
 START WITH 1
 INCREMENT BY 1
 MINVALUE -9223372036854775808
 MAXVALUE 9223372036854775807
 CACHE 
GO
USE [QLySanBong]
GO
/****** Object:  Sequence [dbo].[seq_HoaDon]    Script Date: 6/24/2024 11:35:09 PM ******/
CREATE SEQUENCE [dbo].[seq_HoaDon] 
 AS [bigint]
 START WITH 1
 INCREMENT BY 1
 MINVALUE -9223372036854775808
 MAXVALUE 9223372036854775807
 CACHE 
GO
USE [QLySanBong]
GO
/****** Object:  Sequence [dbo].[seq_KhachHang]    Script Date: 6/24/2024 11:35:09 PM ******/
CREATE SEQUENCE [dbo].[seq_KhachHang] 
 AS [bigint]
 START WITH 1
 INCREMENT BY 1
 MINVALUE -9223372036854775808
 MAXVALUE 9223372036854775807
 CACHE 
GO
USE [QLySanBong]
GO
/****** Object:  Sequence [dbo].[seq_KhungGio]    Script Date: 6/24/2024 11:35:09 PM ******/
CREATE SEQUENCE [dbo].[seq_KhungGio] 
 AS [bigint]
 START WITH 1
 INCREMENT BY 1
 MINVALUE -9223372036854775808
 MAXVALUE 9223372036854775807
 CACHE 
GO
USE [QLySanBong]
GO
/****** Object:  Sequence [dbo].[seq_NhanVien]    Script Date: 6/24/2024 11:35:09 PM ******/
CREATE SEQUENCE [dbo].[seq_NhanVien] 
 AS [bigint]
 START WITH 1
 INCREMENT BY 1
 MINVALUE -9223372036854775808
 MAXVALUE 9223372036854775807
 CACHE 
GO
USE [QLySanBong]
GO
/****** Object:  Sequence [dbo].[seq_SanBong]    Script Date: 6/24/2024 11:35:09 PM ******/
CREATE SEQUENCE [dbo].[seq_SanBong] 
 AS [bigint]
 START WITH 1
 INCREMENT BY 1
 MINVALUE -9223372036854775808
 MAXVALUE 9223372036854775807
 CACHE 
GO
USE [QLySanBong]
GO
/****** Object:  Sequence [dbo].[seq_SanPham]    Script Date: 6/24/2024 11:35:09 PM ******/
CREATE SEQUENCE [dbo].[seq_SanPham] 
 AS [bigint]
 START WITH 1
 INCREMENT BY 1
 MINVALUE -9223372036854775808
 MAXVALUE 9223372036854775807
 CACHE 
GO
/****** Object:  Table [dbo].[KhachHang]    Script Date: 6/24/2024 11:35:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KhachHang](
	[CustomerId] [decimal](18, 0) NOT NULL,
	[CustomerName] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[IdentityNumber] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](max) NULL,
	[ModifiedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SanBong]    Script Date: 6/24/2024 11:35:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SanBong](
	[FieldId] [decimal](18, 0) NOT NULL,
	[FieldName] [nvarchar](max) NULL,
	[Position] [int] NULL,
	[Status] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](max) NULL,
	[ModifiedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[FieldId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HoaDon]    Script Date: 6/24/2024 11:35:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HoaDon](
	[BillId] [decimal](18, 0) NOT NULL,
	[DateCheckout] [datetime] NULL,
	[CustomerId] [decimal](18, 0) NULL,
	[FieldId] [decimal](18, 0) NULL,
	[PaymentMethod] [nvarchar](10) NULL,
	[Total] [decimal](18, 0) NULL,
	[Fee] [decimal](18, 0) NULL,
	[Discount] [decimal](18, 0) NULL,
	[TotalBeforeDiscount] [decimal](18, 0) NULL,
	[Description] [nvarchar](2000) NULL,
	[CreatedBy] [nvarchar](200) NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK__HoaDon__11F2FC6ABDEB270F] PRIMARY KEY CLUSTERED 
(
	[BillId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vw_HoaDon]    Script Date: 6/24/2024 11:35:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_HoaDon] AS
SELECT 
    b.BillId, b.DateCheckout, b.CustomerId, c.CustomerName AS CustomerName,
    b.FieldId, f.FieldName AS FieldName,
    b.PaymentType, b.Total
FROM 
    HoaDon b
LEFT JOIN 
    KhachHang c ON b.CustomerId = c.CustomerId
LEFT JOIN 
    SanBong f ON b.FieldId = f.FieldId;
GO
/****** Object:  Table [dbo].[ChiTietHoaDon]    Script Date: 6/24/2024 11:35:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietHoaDon](
	[BillDetailId] [decimal](18, 0) NOT NULL,
	[BillId] [decimal](18, 0) NULL,
	[ProductId] [decimal](18, 0) NULL,
	[Type] [nvarchar](10) NULL,
	[Count] [decimal](18, 0) NULL,
	[Total] [decimal](18, 0) NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK__ChiTietH__793CAF951A13DDA7] PRIMARY KEY CLUSTERED 
(
	[BillDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[vw_ChiTietHoaDon]    Script Date: 6/24/2024 11:35:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_ChiTietHoaDon] AS
SELECT 
    bd.BillDetailId, bd.BillId, b.DateCheckout, bd.Type, bd.ProductId, bd.Count, bd.Total
FROM 
    ChiTietHoaDon bd
LEFT JOIN 
    HoaDon b ON bd.BillId = b.BillId;
GO
/****** Object:  View [dbo].[vw_KhachHang]    Script Date: 6/24/2024 11:35:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_KhachHang] AS
SELECT * FROM KhachHang;
GO
/****** Object:  Table [dbo].[DonDatSan]    Script Date: 6/24/2024 11:35:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DonDatSan](
	[FieldBookingId] [decimal](18, 0) NOT NULL,
	[Code] [nvarchar](max) NULL,
	[FieldId] [decimal](18, 0) NULL,
	[TimeSlotId] [decimal](18, 0) NULL,
	[CustomerId] [decimal](18, 0) NULL,
	[BookingDate] [datetime] NULL,
	[TimeFrom] [datetime] NULL,
	[TimeTo] [datetime] NULL,
	[Deposit] [decimal](18, 0) NULL,
	[FieldPrice] [decimal](18, 0) NULL,
	[Status] [nvarchar](max) NULL,
	[RejectReason] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](max) NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK__DonDatSa__9520640DD99978B9] PRIMARY KEY CLUSTERED 
(
	[FieldBookingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AllCode]    Script Date: 6/24/2024 11:35:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AllCode](
	[Cdname] [nvarchar](max) NULL,
	[Cdtype] [nvarchar](max) NULL,
	[Cdval] [nvarchar](max) NULL,
	[Content] [nvarchar](max) NULL,
	[Lstodr] [decimal](18, 0) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[vw_DonDatSan]    Script Date: 6/24/2024 11:35:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[vw_DonDatSan] AS
SELECT a.* , FORMAT(a.TimeFrom,'HH:mm') + ' - ' + FORMAT(a.TimeTo,'HH:mm') AS TimeSlotText, f.FieldName, c.CustomerName, c.PhoneNumber, al1.Content AS StatusText  

FROM  DonDatSan a
JOIN  SanBong f ON a.FieldId = f.FieldId
JOIN  KhachHang c ON a.CustomerId = c.CustomerId
LEFT JOIN AllCode al1 ON al1.Cdname = 'FIELD_BOOKING' AND al1.Cdtype = 'STATUS' AND al1.Cdval = a.Status;
GO
/****** Object:  View [dbo].[vw_SanBong]    Script Date: 6/24/2024 11:35:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vw_SanBong] AS
SELECT a.*, al1.Content AS StatusText FROM SanBong a
LEFT JOIN AllCode al1 ON al1.Cdname = 'FIELD' AND al1.Cdtype = 'STATUS' AND al1.Cdval = a.Status;
GO
/****** Object:  Table [dbo].[NhanVien]    Script Date: 6/24/2024 11:35:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhanVien](
	[StaffId] [decimal](18, 0) NOT NULL,
	[StaffName] [nvarchar](200) NULL,
	[PhoneNumber] [nvarchar](20) NULL,
	[IdentityNumber] [nvarchar](30) NULL,
	[StaffPosition] [nvarchar](1000) NULL,
	[BirthOfDate] [datetime] NULL,
	[Sex] [nvarchar](10) NULL,
	[Salary] [decimal](18, 0) NULL,
	[CreatedBy] [nvarchar](200) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](200) NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK__NhanVien__96D4AB17ABC08781] PRIMARY KEY CLUSTERED 
(
	[StaffId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vw_NhanVien]    Script Date: 6/24/2024 11:35:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vw_NhanVien] AS
SELECT a.*, al.Content AS SexText FROM NhanVien a
LEFT JOIN AllCode al ON al.Cdname = 'STAFF' AND al.Cdtype = 'SEX' AND al.Cdval = a.Sex;
GO
/****** Object:  Table [dbo].[KhungGio]    Script Date: 6/24/2024 11:35:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KhungGio](
	[TimeSlotId] [decimal](18, 0) NOT NULL,
	[TimeFrom] [datetime] NULL,
	[TimeTo] [datetime] NULL,
	[Price] [decimal](18, 0) NULL,
	[Enable] [bit] NULL,
	[Position] [int] NULL,
	[FieldId] [decimal](18, 0) NULL,
	[Description] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[TimeSlotId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[vw_KhungGio]    Script Date: 6/24/2024 11:35:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_KhungGio] AS
SELECT 
    ts.TimeSlotId, ts.TimeFrom, ts.TimeTo, ts.Price, ts.Enable, ts.Position,
    ts.FieldId, f.FieldName AS FieldName
FROM 
    KhungGio ts
LEFT JOIN 
    SanBong f ON ts.FieldId = f.FieldId;
GO
/****** Object:  Table [dbo].[SanPham]    Script Date: 6/24/2024 11:35:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SanPham](
	[ProductId] [decimal](18, 0) NOT NULL,
	[ImageUrl] [nvarchar](max) NULL,
	[ProductName] [nvarchar](500) NULL,
	[Price] [decimal](18, 0) NULL,
	[Count] [decimal](18, 0) NULL,
	[Status] [nvarchar](20) NULL,
	[Description] [nvarchar](2000) NULL,
	[CreatedBy] [nvarchar](200) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](200) NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK__SanPham__B40CC6CD04CC5D48] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[vw_SanPham]    Script Date: 6/24/2024 11:35:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vw_SanPham] AS
SELECT 
    *
FROM 
    SanPham;
GO
ALTER TABLE [dbo].[ChiTietHoaDon] ADD  CONSTRAINT [DF__ChiTietHo__Descr__01142BA1]  DEFAULT ('') FOR [Description]
GO
ALTER TABLE [dbo].[DonDatSan] ADD  CONSTRAINT [DF__DonDatSan__Descr__4AB81AF0]  DEFAULT ('') FOR [Description]
GO
ALTER TABLE [dbo].[DonDatSan] ADD  CONSTRAINT [DF__DonDatSan__Creat__4BAC3F29]  DEFAULT ('') FOR [CreatedBy]
GO
ALTER TABLE [dbo].[DonDatSan] ADD  CONSTRAINT [DF__DonDatSan__Modif__4CA06362]  DEFAULT ('') FOR [ModifiedBy]
GO
ALTER TABLE [dbo].[HoaDon] ADD  CONSTRAINT [DF__HoaDon__Descript__5165187F]  DEFAULT ('') FOR [Description]
GO
ALTER TABLE [dbo].[HoaDon] ADD  CONSTRAINT [DF__HoaDon__CreatedB__52593CB8]  DEFAULT ('') FOR [CreatedBy]
GO
ALTER TABLE [dbo].[KhachHang] ADD  DEFAULT ('') FOR [Description]
GO
ALTER TABLE [dbo].[KhachHang] ADD  DEFAULT ('') FOR [CreatedBy]
GO
ALTER TABLE [dbo].[KhachHang] ADD  DEFAULT ('') FOR [ModifiedBy]
GO
ALTER TABLE [dbo].[KhungGio] ADD  DEFAULT ('') FOR [Description]
GO
ALTER TABLE [dbo].[NhanVien] ADD  CONSTRAINT [DF__NhanVien__Create__5EBF139D]  DEFAULT ('') FOR [CreatedBy]
GO
ALTER TABLE [dbo].[NhanVien] ADD  CONSTRAINT [DF__NhanVien__Modifi__5FB337D6]  DEFAULT ('') FOR [ModifiedBy]
GO
ALTER TABLE [dbo].[SanBong] ADD  DEFAULT ('') FOR [Description]
GO
ALTER TABLE [dbo].[SanBong] ADD  DEFAULT ('') FOR [CreatedBy]
GO
ALTER TABLE [dbo].[SanBong] ADD  DEFAULT ('') FOR [ModifiedBy]
GO
ALTER TABLE [dbo].[SanPham] ADD  CONSTRAINT [DF__SanPham__Descrip__76969D2E]  DEFAULT ('') FOR [Description]
GO
ALTER TABLE [dbo].[SanPham] ADD  CONSTRAINT [DF__SanPham__Created__778AC167]  DEFAULT ('') FOR [CreatedBy]
GO
ALTER TABLE [dbo].[SanPham] ADD  CONSTRAINT [DF__SanPham__Modifie__787EE5A0]  DEFAULT ('') FOR [ModifiedBy]
GO
ALTER TABLE [dbo].[ChiTietHoaDon]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietHoaDon_HoaDon] FOREIGN KEY([BillId])
REFERENCES [dbo].[HoaDon] ([BillId])
GO
ALTER TABLE [dbo].[ChiTietHoaDon] CHECK CONSTRAINT [FK_ChiTietHoaDon_HoaDon]
GO
ALTER TABLE [dbo].[DonDatSan]  WITH CHECK ADD  CONSTRAINT [FK_DonDatSan_KhachHang] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[KhachHang] ([CustomerId])
GO
ALTER TABLE [dbo].[DonDatSan] CHECK CONSTRAINT [FK_DonDatSan_KhachHang]
GO
ALTER TABLE [dbo].[DonDatSan]  WITH CHECK ADD  CONSTRAINT [FK_DonDatSan_SanBong] FOREIGN KEY([FieldId])
REFERENCES [dbo].[SanBong] ([FieldId])
GO
ALTER TABLE [dbo].[DonDatSan] CHECK CONSTRAINT [FK_DonDatSan_SanBong]
GO
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD  CONSTRAINT [FK_HoaDon_KhachHang] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[KhachHang] ([CustomerId])
GO
ALTER TABLE [dbo].[HoaDon] CHECK CONSTRAINT [FK_HoaDon_KhachHang]
GO
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD  CONSTRAINT [FK_HoaDon_SanBong] FOREIGN KEY([FieldId])
REFERENCES [dbo].[SanBong] ([FieldId])
GO
ALTER TABLE [dbo].[HoaDon] CHECK CONSTRAINT [FK_HoaDon_SanBong]
GO
ALTER TABLE [dbo].[KhungGio]  WITH CHECK ADD  CONSTRAINT [FK_KhungGio_SanBong] FOREIGN KEY([FieldId])
REFERENCES [dbo].[SanBong] ([FieldId])
GO
ALTER TABLE [dbo].[KhungGio] CHECK CONSTRAINT [FK_KhungGio_SanBong]
GO
USE [master]
GO
ALTER DATABASE [QLySanBong] SET  READ_WRITE 
GO
