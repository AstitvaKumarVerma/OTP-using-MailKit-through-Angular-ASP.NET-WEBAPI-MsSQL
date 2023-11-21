create table Register0909(
	id  int primary key identity(1,1),
	Email varchar(100) Not Null,
	Password varchar(20) Not Null,
	IsActive bit Default 1,
	IsDeleted bit Default 0,
	IsAuthenticated bit Default 0
)

truncate table Register0909

Select * From Register0909