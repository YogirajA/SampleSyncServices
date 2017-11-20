-- ================================================
-- Template generated from Template Explorer using:
-- Create Trigger (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- See additional Create Trigger templates for more
-- examples of different Trigger statements.
--
-- This block of comments will not be included in
-- the definition of the function.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create TRIGGER dbo.OnInsertingUser 
   ON  dbo.Users 
   AFTER Insert
AS 
	BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	insert into [SyncUsers.SqlServerEndpoint]
			  (Id, Recoverable, Headers, Body)
  select convert(uniqueidentifier, hashbytes('MD5',convert(varchar(255),i.Id))) as Id,
  'true' as Recoverable,
  '' as Headers,
  convert(varbinary(255), '{ $type: "Messages.V1.NewUser", Id: "' + convert(varchar(255),i.Id) + '",FirstName:"' + convert(varchar(255),i.FirstName) + '",LastName:"' + convert(varchar(255),i.LastName) + '",ModifiedOn:"' + convert(varchar(255),i.ModifiedOn) + '",CreatedOn:"' + convert(varchar(255),i.CreatedOn) + '"}') as Body
  from inserted i
	END
GO
