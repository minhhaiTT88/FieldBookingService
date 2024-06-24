USE [QLySanBong]
GO
INSERT [dbo].[AllCode] ([Cdname], [Cdtype], [Cdval], [Content], [Lstodr]) VALUES (N'FIELD', N'STATUS', N'A', N'Đang hoạt động', CAST(0 AS Decimal(18, 0)))
GO
INSERT [dbo].[AllCode] ([Cdname], [Cdtype], [Cdval], [Content], [Lstodr]) VALUES (N'FIELD', N'STATUS', N'U', N'Ngưng hoạt động', CAST(1 AS Decimal(18, 0)))
GO
INSERT [dbo].[AllCode] ([Cdname], [Cdtype], [Cdval], [Content], [Lstodr]) VALUES (N'FIELD_BOOKING', N'STATUS', N'P', N'Chờ duyệt', CAST(0 AS Decimal(18, 0)))
GO
INSERT [dbo].[AllCode] ([Cdname], [Cdtype], [Cdval], [Content], [Lstodr]) VALUES (N'FIELD_BOOKING', N'STATUS', N'A', N'Đặt thành công', CAST(1 AS Decimal(18, 0)))
GO
INSERT [dbo].[AllCode] ([Cdname], [Cdtype], [Cdval], [Content], [Lstodr]) VALUES (N'FIELD_BOOKING', N'STATUS', N'X', N'Từ chối', CAST(4 AS Decimal(18, 0)))
GO
INSERT [dbo].[AllCode] ([Cdname], [Cdtype], [Cdval], [Content], [Lstodr]) VALUES (N'STAFF', N'SEX', N'M', N'Nam', CAST(0 AS Decimal(18, 0)))
GO
INSERT [dbo].[AllCode] ([Cdname], [Cdtype], [Cdval], [Content], [Lstodr]) VALUES (N'STAFF', N'SEX', N'G', N'Nữ', CAST(1 AS Decimal(18, 0)))
GO
INSERT [dbo].[AllCode] ([Cdname], [Cdtype], [Cdval], [Content], [Lstodr]) VALUES (N'STAFF', N'SEX', N'O', N'Khác', CAST(2 AS Decimal(18, 0)))
GO
INSERT [dbo].[AllCode] ([Cdname], [Cdtype], [Cdval], [Content], [Lstodr]) VALUES (N'FIELD_BOOKING', N'STATUS', N'D', N'Đã thanh toán', CAST(2 AS Decimal(18, 0)))
GO
INSERT [dbo].[AllCode] ([Cdname], [Cdtype], [Cdval], [Content], [Lstodr]) VALUES (N'FIELD_BOOKING', N'STATUS', N'L', N'Khách bỏ cọc', CAST(3 AS Decimal(18, 0)))
GO
INSERT [dbo].[AllCode] ([Cdname], [Cdtype], [Cdval], [Content], [Lstodr]) VALUES (N'BILL', N'PAY_TYPE', N'CASH', N'Tiền mặt', CAST(0 AS Decimal(18, 0)))
GO
INSERT [dbo].[AllCode] ([Cdname], [Cdtype], [Cdval], [Content], [Lstodr]) VALUES (N'BILL', N'PAY_TYPE', N'CARD', N'Chuyển khoản', CAST(1 AS Decimal(18, 0)))
GO
INSERT [dbo].[AllCode] ([Cdname], [Cdtype], [Cdval], [Content], [Lstodr]) VALUES (N'BILL_DETAIL', N'TYPE', N'FIELD', N'Thanh toán sân', CAST(0 AS Decimal(18, 0)))
GO
INSERT [dbo].[AllCode] ([Cdname], [Cdtype], [Cdval], [Content], [Lstodr]) VALUES (N'BILL_DETAIL', N'TYPE', N'PRODUCT', N'Thanh toán sản phẩm', CAST(1 AS Decimal(18, 0)))
GO
