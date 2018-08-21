--RESTORE FILELISTONLY
--FROM DISK ='C:\Data\backups\InvestmentBuilderTest'


RESTORE DATABASE PassportWalletLocal
   FROM DISK = 'C:\Data\SQLServer\MSSQL12.SQLEXPRESS\MSSQL\Backup\PassportWallet.bak'
   WITH NORECOVERY, 
      MOVE 'PassportWallet_Data' TO 
'C:\Data\SQLServer\FileDB\PassportWalletLocal.mdf', 
      MOVE 'PassportWallet_Log' 
TO 
'C:\Data\SQLServer\FileDB\PassportWalletLocal_log.mdf';
RESTORE LOG PassportWalletLocal
   FROM DISK = 'C:\Data\SQLServer\MSSQL12.SQLEXPRESS\MSSQL\Backup\PassportWallet.bak'
   WITH RECOVERY;