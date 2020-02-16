echo off
sqlcmd -E -S . -i CreateDatabase.sql
sqlcmd -E -S . -i CreateTables.sql
sqlcmd -E -S . -i DataGenerate.sql
exit

