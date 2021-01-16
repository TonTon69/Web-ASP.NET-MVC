use master
IF EXISTS (SELECT NAME FROM SYS.DATABASES WHERE NAME='QUANLY_FASHION_FINISH_WEB')
DROP DATABASE QUANLY_FASHION_FINISH_WEB
GO 
CREATE DATABASE QUANLY_FASHION_FINISH_WEB
ON 
(NAME='QLFS.DATA',FILENAME='D:\IT\web\asp.net\đồ án môn học công nghệ phần mềm\DB\QLFF.MDF')
LOG ON 
(NAME='QLFS.LOG',FILENAME='D:\IT\web\asp.net\đồ án môn học công nghệ phần mềm\DB\QLFF.LDF')
GO 
USE QUANLY_FASHION_FINISH_WEB
go

Create table ProductCetegory/*hoàn thành*/
(
	CategoryCode int IDENTITY(1,1) NOT NULL primary key,
	Name Nvarchar(250) NOT NULL,
	MetaTitle Nvarchar(250) NULL, /* Thay đổi thanh tiêu đề*/
	DisplayOrder Integer NULL, /* Hiển thị sao tăng dần danh sách mục */
	SeoTitle Nvarchar(250) NULL,
	CreateDate Datetime NULL,  /* Ngày khởi tạo*/
	CreateBy nvarchar(250), /* Người khởi tạo*/
	ShowOnHome bit /*Chiếu danh mục lên Trang chủ*/
) 
go
SELECT convert(varchar, getdate(), 103)
Create table Product/*hoàn thành*/
(
	ProductCode int IDENTITY(1,1)  NOT NULL ,
	Name Nvarchar(250) NOT NULL,
	MetaTitle Nvarchar(250) NULL,
	ProductDescription Ntext NULL,
	Image nvarchar(250) NULL,
	Image1 nvarchar(250) NULL,
	Image2 nvarchar(250) NULL,
	Image3 nvarchar(250) NULL,
	Image4 nvarchar(250) NULL,
	Price decimal(18,0) NULL,
	PromotionPrice decimal(18,0) NULL, /*Giá khuyến mãi */
	Quanlity Integer NOT NULL,   /* Số lượng tồn*/
	CategoryCode int NULL,  /* Chỉ ra danh mục sp nằm ở đó */
	CreatedDate Datetime NULL, /* Ngày khởi tạo*/
	CreateBy Nvarchar(250) NULL, /* Người khởi tạo*/
	TopHot datetime NULL,/* Dùng ngày để đưa sản phẩm là sản phẩm mới nhất lên Trang Chủ*/
	Primary Key (ProductCode),
	FOREIGN KEY (CategoryCode) REFERENCES ProductCetegory(CategoryCode) ON UPDATE CASCADE
	
) 
go

Create table WebUser /*hoàn thành*/
(
	UserCode int IDENTITY(1,1) NOT NULL,
	FullName Nvarchar(50) NOT NULL,
	Account varchar(50) NOT NULL unique,
	UserPassword Nvarchar(50) NOT NULL,
	Email Nvarchar(100) NULL UNIQUE,
	Address nvarchar(250),
	Phone varchar(11) NULL,
	BirthDay Datetime NULL,
	Primary Key (UserCode)
)
go
Create table Review /*hoàn thành*/
(
	ReviewCode int IDENTITY(1,1) NOT NULL,
	[Content] ntext NOT Null,
	Rating float NOT NULL,
	DatePost datetime NOT NULL,
	ProductCode int NOT null,
	Primary Key (ReviewCode),
	foreign key (ProductCode) references Product(ProductCode) on update cascade
)

Create table FSOrder/*hoàn thành*/
(
	OrderCode int NOT NULL IDENTITY(1,1) ,
	Paid Bit NULL, /* đã thanh toán hay chưa*/
	Status bit NOT NULL, /*Trạng thái đã order hay chưa (Yes/NO)*/
	OrderDay Datetime NOT NULL,  /* Ngày order*/
	DeliveryDay Datetime NOT NULL, /* Ngày vận chuyển*/
	UserCode int NOT NULL, 
	Primary Key (OrderCode),
	foreign key (UserCode) references WebUser(UserCode) on update cascade
) 

go

Create table OrderDetail/*hoàn thành*/
(
	ProductCode int NOT NULL,
	OrderCode int NOT NULL,
	Number int,
	TotalPrice decimal(18,0),
	FOREIGN KEY (ProductCode) REFERENCES Product(ProductCode) ON UPDATE CASCADE,
	FOREIGN KEY (OrderCode) REFERENCES FSOrder(OrderCode) ON UPDATE CASCADE
) 
go

Create table Administrator/*hoàn thành*/
(
	UserAdmin Nvarchar(50) NOT NULL,
	PasswordAdmin Nvarchar(50) NULL,
	FullNameAdmin Nvarchar(50) NULL,
	Primary Key (UserAdmin)
) 
go

Create table FeedBack /*hoàn thành*/
(
	FeedBackCode int IDENTITY(1,1) NOT NULL,
	Name Nvarchar(50) NULL,
	Phone Nvarchar(12) NULL,
	Email Nvarchar(100) NULL,
	Address Nvarchar(250) NULL,
	[Content] ntext Null,
	FbStatus bit NULL, /*trạng thái đã đọc hay chưa*/
	CreateDate datetime NULL,
	Primary Key (FeedBackCode)
) 
go

Create table Slide /*hoàn thành*/
(
	SlideCode int NOT NULL IDENTITY(1,1) ,
	SlideImage nvarchar(MAX) NULL,/*tại sao trong cs thiết kế là nvarchar*/
	DisplayOrder Integer NULL,
	Link Nvarchar(MAX) NULL,
	Discription Ntext NULL,
	CreatedDate Datetime NULL,
	CreatedBy Nvarchar(250) NULL,
	Primary Key (SlideCode)
) 
go 
create table Company /*hoàn thành*/
(
	CompanyCode int not null primary key,
	Name Nvarchar(100),
	Email varchar(100),
	Phone varchar(12),
	Address nvarchar(250),
)
