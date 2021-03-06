/****** Object:  Table [dbo].[Address]    Script Date: 05.04.2014 20:12:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Address](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ContactName] [nvarchar](30) NOT NULL,
	[Country] [nvarchar](20) NOT NULL,
	[Street] [nvarchar](40) NOT NULL,
	[City] [nvarchar](10) NOT NULL,
	[State] [nvarchar](20) NOT NULL,
	[PostalCode] [int] NOT NULL,
	[Telefon] [nvarchar](20) NULL,
	[IsPrimary] [bit] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK__Address__3214EC075B0E86E0] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Category]    Script Date: 05.04.2014 20:12:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Membership]    Script Date: 05.04.2014 20:12:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Membership](
	[UserId] [int] NOT NULL,
	[CreateDate] [datetime] NULL,
	[Password] [nvarchar](128) NOT NULL,
	[PasswordVerificationToken] [nvarchar](128) NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Order]    Script Date: 05.04.2014 20:12:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[IsNew] [bit] NOT NULL,
	[IsProcess] [bit] NOT NULL,
	[IsConfirm] [bit] NOT NULL,
	[DateSend] [datetime] NULL,
	[UserId] [int] NOT NULL,
	[AddressId] [int] NOT NULL,
 CONSTRAINT [PK__Orders__3214EC0729518BD9] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OrderProducts]    Script Date: 05.04.2014 20:12:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderProducts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Count] [int] NOT NULL,
	[OrdersId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Product]    Script Date: 05.04.2014 20:12:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](1000) NOT NULL,
	[Price] [int] NOT NULL,
	[Image] [nvarchar](100) NULL,
	[CategoryId] [int] NOT NULL,
 CONSTRAINT [PK__Product__3214EC0782974D57] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Roles]    Script Date: 05.04.2014 20:12:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](256) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 05.04.2014 20:12:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](20) NULL,
	[LastName] [nvarchar](20) NULL,
	[Image] [nvarchar](20) NULL,
	[IsActivity] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UsersInRoles]    Script Date: 05.04.2014 20:12:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersInRoles](
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Category] ON 

GO
INSERT [dbo].[Category] ([Id], [Name]) VALUES (1, N'Телевизоры')
GO
INSERT [dbo].[Category] ([Id], [Name]) VALUES (2, N'Телефоны')
GO
INSERT [dbo].[Category] ([Id], [Name]) VALUES (3, N'Фотоаппараты')
GO
INSERT [dbo].[Category] ([Id], [Name]) VALUES (4, N'Ноутбуки')
GO
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
INSERT [dbo].[Membership] ([UserId], [CreateDate], [Password], [PasswordVerificationToken]) VALUES (1, GETDATE(), N'E0D6EFC4580E3719FAB2BE23569797BF07397391', NULL)
GO

SET IDENTITY_INSERT [dbo].[Product] ON 

GO
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [Image], [CategoryId]) VALUES (2, N'Samsung UE40F6500', N'ЖК 40" (1920x1080), 400 Hz, 3D (активные очки), Smart TV, медиаплеер, PVR, timeshift, видеозвонки, WiFi', 800, N'/Images/Items/50f8d711bc3d13.jpg', 1)
GO
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [Image], [CategoryId]) VALUES (3, N'LG 42LA620V', N'ЖК 42" (1920x1080), 200 Hz, 3D (поляризационные очки), Smart TV, медиаплеер, видеозвонки, WiFi', 620, N'/Images/Items/006207a534e.jpg', 1)
GO
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [Image], [CategoryId]) VALUES (4, N'Sony KDL-42W705B', N'ЖК 42" (1920x1080), 200 Hz, Sony Entertainment Network, медиаплеер, PVR, timeshift, видеозвонки, WiFi', 780, N'/Images/Items/59ccf0fdc34.jpg', 1)
GO
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [Image], [CategoryId]) VALUES (7, N'Sony Xperia Z1', N'Флагман второй половины 2013 года от компании Sony. Пришел на смену Xperia Z. Главной особенностью преемника стал 20.7-мегапиксельный модуль камеры Exmor RS c дополнительным процессором для обработки изображений BIONZ. Имеет водо-, пыленепроницаемый корпус, из закаленного стекла, с алюминиевой рамкой. Емкость несъемного аккумулятора составляет 3000 мА/ч.  ', 640, N'/Images/Items/c32929ad7f239f.jpg', 2)
GO
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [Image], [CategoryId]) VALUES (8, N'Sony Xperia T2 Ultra dual', N'Смартфон с 6" экраном. По словам Sony, предназначен для развивающихся рынков. Обладает тонким корпусом, имеет батарею 3000 мАч, 13-мегапиксельную камеру и поддержку LTE.  ', 830, N'/Images/Items/45065306c37ef.jpg', 2)
GO
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [Image], [CategoryId]) VALUES (16, N'Samsung Galaxy S4 (16Gb) ', N'
Описание Что такое «Описание»
	Galaxy S4 был представлен в марте 2013 года как флагман смартфонов Samsung. Основным отличием от SGS III является мощный процессор собственной разработки Exynos 5410, имеющий 8 ядер (4 производительных и 4 энергоэффективных) и новое графическое ядро. В версии с поддержкой LTE используется 4-ядерный процессор от Qualcomm. Объём памяти увеличился вдвое, до 2Гб. Увеличился размер экрана до внушительных 5 дюймов с разрешением FullHD 1080x1920, технология AMOLED. Стекло Gorilla Glass 3 защищает экран от царапин. Камера улучшилась благодаря 13 Мп-матрице. Время работы аппарата выросло благодаря более ёмкому аккумулятору.  ', 590, N'/Images/Items/b27ac2c5d52d.jpg', 2)
GO
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [Image], [CategoryId]) VALUES (18, N'Samsung i9300 Galaxy S III (16 Gb)', N'Samsung Galxy S III – флагман компании в 2012 году. Аппарат оснащён 4-ядерным процессором с высокой тактовой частотой. Главной особенностью устройства является экран. 4.8-дюймовый AMOLED, прикрытый закаленным стеклом Gorilla Glass. Разрешение экрана довольно большое – 720х1280.

Существенно увеличена ёмкость съёмного аккумулятора, что позволило улучшить время автономной работы во всех режимах. ', 430, N'/Images/Items/b44972ba2bf25.jpg', 2)
GO
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [Image], [CategoryId]) VALUES (19, N'Samsung N900 Galaxy Note 3 (32GB)', N'Логичное обновление флагманского фаблета Galaxy Note 2 от компании Samsung. Задняя крышка устройства стилизована под кожу. Экран изменился с 5.5" до 5.7", а габариты остались прежними. В LTE модели используется процессор Snapdragon 800, в 3G — Exynos 5 Octa, оба варианта оснащаются 3-мя ГБ оперативной памяти. Фаблет имеет встроенный стилус, а также обновленный комплект софта для его использования.  ', 740, N'/Images/Items/b2314ab5dd85.jpg', 2)
GO
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [Image], [CategoryId]) VALUES (20, N'Samsung N7100 Galaxy Note II (16Gb)', N'Фаблет (гибрид смартфона и планшета) пришёл на смену первой модели в 2012 году. Как и Galaxy S III, устройство оснащено мощным 4-ядерным процессором с увеличенной тактовой частотой. Объём оперативной памяти также увеличен вдвое. По габаритам особых отличий от предшественника нет, но вот диагональ экрана увеличилась. Для защиты экрана используется стекло Gorilla Glass 2. В комплекте к смартфону поставляется специальный стилус.
Ёмкость аккумулятора заметно увеличена, как и итоговое время автономной работы при любых режимах использования смартфона.  ', 580, N'/Images/Items/480747254bfc6.jpg', 2)
GO
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [Image], [CategoryId]) VALUES (21, N'Lenovo P780', N'Смартфон принадлежит линейке Professional в самой Lenovo – это говорит о размере аккумулятора устройства. Именно батарея является главной фишкой устройства, а точнее, её ёмкость.

Экран у смартфона довольно большой, а разрешение типичное для среднего сегмента – 720х1280. Аппарат построен на бюджетном процессоре от MediaTek, хотя и 4-ядерном. Задняя панель выполнена из металла. Также отметим наличие двух слотов под SIM-карты.', 340, N'/Images/Items/9a7fd4cd4c131a7.jpg', 2)
GO
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [Image], [CategoryId]) VALUES (22, N'HTC One Dual Sim', N'Двухсимочный вариант флагманского смартфона компании HTC в 2013 году. Как и HTC One, он построен на платформе Qualcomm Snapdragon 600, оснащен 2 ГБ оперативной памяти и 4.7-дюймовым экраном с разрешением Full HD.

Камера UltraPixel с оптической стабилизацией, корпус из алюминия, защитное стекло Gorilla Glass, ИК-порт для управления ТВ, отличный звук из стереодинамиков на фронтальной части - все точно так же, как и у оригинальной модели HTC One. Из нововведений - две активные SIM-карты, поддержка карт памяти.

HTC One Dual Sim - самый функциональный двухсимочный смартфон на конец 2013 года.  ', 630, N'/Images/Items/58c5ea630d412.jpg', 2)
GO
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [Image], [CategoryId]) VALUES (24, N'HTC One (32Gb)', N'Флагман компании HTC в 2013 году. Пришел на смену прошлогодним One X и One X+. Добавлена поддержка сетей 4го поколения LTE. Qualcomm Snapdragon 600 является сердцем смартфона. Оперативная память увеличилась вдвое.

Аппарат выполнен из алюминия, а экран прикрывает стекло Gorilla Glass. Интересно наличие стереодинамиков внизу и вверху аппарата. Разрешение экрана увеличилось до FullHD, при этом размер не изменился.

Технология UltraPixel призвана улучшить качество снимков путём увеличения размера самих пикселей матрицы камеры. Важным нововведением можно считать оптическую стабилизацию. Ёмкость аккумулятора удалось увеличить на 500 мАч.  ', 585, N'/Images/Items/47fb7b7ba36bd4.jpg', 2)
GO
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [Image], [CategoryId]) VALUES (26, N'Huawei Ascend P6', N'На момент анонса Ascend P6 являлся одним из самых тонких смартфонов на рынке. Корпус аппарата выполнен из металла. Одна из первых имиджевых моделей китайского производителя.

В роли процессора выступает платформа K3V2 – собственная разработка Huawei. Процессор имеет 4 вычислительных ядра с частотой 1.5ГГц. Экран немногим меньше 5 дюймов, при этом разрешение 720x1280 с технологией IPS. Фронтальная камера может похвастаться высоким разрешением 5 мегапикселей.

Используется Android 4.2.2 с фирменной оболочкой Emotion UI.  ', 355, N'/Images/Items/3bfa04c2bc99f53.jpg', 2)
GO
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [Image], [CategoryId]) VALUES (27, N'ASUS X550CC-XO072H', N'15.6" (1366 x 768 глянцевая), Intel Core i3 3217U, 4 Гб, 500 Гб (HDD), NVIDIA GeForce GT 720M 2 ГБ, DVD Multi, батарея 4 ячейки, цвет корпуса чёрный/серебристый, цвет крышки серый ', 540, N'/Images/Items/5f9667ea040b78.jpg', 4)
GO
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [Image], [CategoryId]) VALUES (28, N'Lenovo IdeaPad Z500', N'15.6" (1366 x 768), Intel Core i5 3230M, 6 Гб, 1000 Гб (HDD), NVIDIA GeForce GT 740M 2 ГБ, DVD Multi, батарея 4 ячейки, цвет корпуса серебристый, цвет крышки темно-коричневый ', 715, N'/Images/Items/a6bdb4b7609da.jpg', 4)
GO
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [Image], [CategoryId]) VALUES (29, N'Acer Aspire E1-531-20204G75Mnks', N'15.6" (1366 x 768 глянцевая), Intel Pentium 2020M, 4 Гб, 750 Гб (HDD), Intel HD Graphics, DVD Multi, батарея 6 ячеек, цвет корпуса чёрный/серебристый, цвет крышки черный ', 400, N'/Images/Items/d4004a7ad6698.jpg', 4)
GO
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [Image], [CategoryId]) VALUES (30, N'HP ENVY 6-1101er', N'15.6" сликбук HP, построенный на платформе AMD Comal и упакованный в корпус с применением алюминия. Оснащен 15.6" экраном с узкой рамкой, динамиками Beats с сабвуфером, отличным набором интерфейсов, встроенным либо дискретным видеоадаптером. Выпускается в двух цветовых решениях.  ', 580, N'/Images/Items/71395cb55648c8.jpg', 4)
GO
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [Image], [CategoryId]) VALUES (31, N'Sony VAIO SVF1521D1RW', N'15.5" (1366 x 768 глянцевая), Intel Pentium 2117U, 4 Гб, 500 Гб (HDD), NVIDIA GeForce GT 740M 1 ГБ, DVD Multi, цвет корпуса чёрный/белый, цвет крышки белый. Мультимедийный ноутбук от компании Sony. Модель построена на базе процессора Intel, оснащена 15 - дюймовой матрицей и базовым набором портов. В зависимости от конкретной модели, может поставляться с переключаемой графикой, сенсорным дисплеем и динамиками с сабвуфером. Выпускается в нескольких цветовых решениях.   ', 630, N'/Images/Items/b3fdb506b47.jpg', 4)
GO
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [Image], [CategoryId]) VALUES (32, N'LG 32LN655V', N'ЖК 32" (1920x1080), 400 Hz, Smart TV, медиаплеер, PVR, timeshift, видеозвонки, WiFi ', 540, N'/Images/Items/50f8d711bc3d13.jpg', 1)
GO
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [Image], [CategoryId]) VALUES (36, N'Sony KDL-55W905A', N'ЖК 55" (1920x1080), 800 Hz, 3D (активные очки), медиаплеер, PVR, timeshift, видеозвонки, WiFi. 3D-телевизор Full HD (1080p) с экраном Triluminos, технологией X-Reality PRO и зеркальным отображением одним касанием с NFC.', 2200, N'/Images/Items/c22ca65f59a80f98.jpg', 1)
GO
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [Image], [CategoryId]) VALUES (37, N'Samsung UE40F6800', N'ЖК 40" (1920x1080), 400 Hz, 3D (активные очки), Smart TV, медиаплеер, PVR, timeshift, видеозвонки, WiFi ', 880, N'/Images/Items/44f2e866af28b21.jpg', 1)
GO
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [Image], [CategoryId]) VALUES (46, N'Samsung ATIV Book 6', N'Мультимедийный ноутбук от компании Samsung построен на базе процессора Intel третьего поколения, имеет переключаемую графику от AMD, подсветку клавиатуры и алюминиевый корпус. Выпускается в темном и строгом цвете.  ', 800, N'/Images/Items/2c6e6ef1559108ac.jpg', 4)
GO
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

GO
INSERT [dbo].[Roles] ([Id], [RoleName]) VALUES (1, N'Admin')
GO
INSERT [dbo].[Roles] ([Id], [RoleName]) VALUES (2, N'Moderator')
GO
INSERT [dbo].[Roles] ([Id], [RoleName]) VALUES (3, N'User')
GO
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

GO
INSERT [dbo].[User] ([Id], [Email], [FirstName], [LastName], [Image], [IsActivity]) VALUES (1, N'admin', N'admin', N'', NULL, NULL)
GO

SET IDENTITY_INSERT [dbo].[User] OFF
GO
INSERT [dbo].[UsersInRoles] ([UserId], [RoleId]) VALUES (1, 1)
GO
INSERT [dbo].[UsersInRoles] ([UserId], [RoleId]) VALUES (1, 2)
GO

SET ANSI_PADDING ON

GO
/****** Object:  Index [UQ__webpages__8A2B616030714310]    Script Date: 05.04.2014 20:12:42 ******/
ALTER TABLE [dbo].[Roles] ADD UNIQUE NONCLUSTERED 
(
	[RoleName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Address] ADD  CONSTRAINT [DF_Address_IsPrimary]  DEFAULT ((0)) FOR [IsPrimary]
GO
ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [FK__Address__UserId__1A14E395] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK__Address__UserId__1A14E395]
GO
ALTER TABLE [dbo].[Membership]  WITH CHECK ADD  CONSTRAINT [FK_webpages_Membership_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Membership] CHECK CONSTRAINT [FK_webpages_Membership_User]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK__Orders__UserId__1CF15040] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK__Orders__UserId__1CF15040]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Address] FOREIGN KEY([AddressId])
REFERENCES [dbo].[Address] ([Id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Address]
GO
ALTER TABLE [dbo].[OrderProducts]  WITH CHECK ADD  CONSTRAINT [FK__OrderProd__Order__1B0907CE] FOREIGN KEY([OrdersId])
REFERENCES [dbo].[Order] ([Id])
GO
ALTER TABLE [dbo].[OrderProducts] CHECK CONSTRAINT [FK__OrderProd__Order__1B0907CE]
GO
ALTER TABLE [dbo].[OrderProducts]  WITH CHECK ADD  CONSTRAINT [FK__OrderProd__Produ__1BFD2C07] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
GO
ALTER TABLE [dbo].[OrderProducts] CHECK CONSTRAINT [FK__OrderProd__Produ__1BFD2C07]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK__Product__Categor__1DE57479] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([Id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK__Product__Categor__1DE57479]
GO
ALTER TABLE [dbo].[UsersInRoles]  WITH CHECK ADD  CONSTRAINT [fk_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
GO
ALTER TABLE [dbo].[UsersInRoles] CHECK CONSTRAINT [fk_RoleId]
GO
ALTER TABLE [dbo].[UsersInRoles]  WITH CHECK ADD  CONSTRAINT [fk_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[UsersInRoles] CHECK CONSTRAINT [fk_UserId]
GO
