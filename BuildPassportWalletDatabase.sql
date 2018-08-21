use master

if exists(select 1 from sysdatabases where name = 'PasswordWallet')
begin
	alter database PasswordWallet set single_user with
	rollback immediate
	drop database PasswordWallet
end

go

create database PasswordWallet
on 
(name = PasswordWallet_Data, 
	filename = 'C:\Data\SQLServer\FileDB\PasswordWallet.mdf',
	size = 10,
	maxsize = 50,
	filegrowth = 5)
log on
(name = PasswordWallet_Log, 
	filename = 'C:\Data\SQLServer\FileDB\PasswordWallet.ldf',
	size = 5mb,
	maxsize = 25mb,
	filegrowth = 5mb);

go

use PasswordWallet
go

create table dbo.Accounts
(
	[Name] nvarchar(256) not null primary key clustered,
	[Website] nvarchar(256),
	[Username] nvarchar(256) not null,
	[Password] nvarchar(256) not null,
	[Passcode] nvarchar(256),
	[Other] nvarchar(256)
)

go
