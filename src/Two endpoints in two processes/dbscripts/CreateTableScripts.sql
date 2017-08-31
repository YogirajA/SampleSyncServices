If exists(select 1 from sys.tables where name = 'Users')
Begin
	Drop table Users
End
Create Table Users
(
	Id uniqueidentifier not null primary key,
	FirstName varchar(100) null,
	LastName varchar(100) null,
	ModifiedOn datetime not null,
	CreatedOn datetime not null
)
If exists(select 1 from sys.tables where name = 'Accounts')
Begin
	Drop table Accounts
End
Create Table Accounts
(
	Id uniqueidentifier not null primary key,
	UserId uniqueidentifier not null,
	Balance decimal(11,2) null,
	ModifiedOn datetime not null,
	CreatedOn datetime not null                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     
)