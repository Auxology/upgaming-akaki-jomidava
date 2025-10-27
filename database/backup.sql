BACKUP DATABASE master
TO DISK = '/var/opt/mssql/backup/master.bak'
WITH FORMAT,
     INIT,
     NAME = 'Full Backup of master';
