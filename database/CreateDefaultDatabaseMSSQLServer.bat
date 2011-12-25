REM Creating default database...
REM Running DropDatabase.sql...

sqlcmd -i "DropDatabase.sql" -b -o "DropDatabase_output.txt"

REM Database dropped!
REM Running CreateDatabase.sql...

sqlcmd -i "CreateDatabaseMSSQLServer.sql" -b -o "CreateDatabase_output.txt"

REM Database created!
REM Running CreateUser.sql...

sqlcmd -i "CreateUser.sql" -b -o "CreateUser_output.txt"

REM User created!
REM Running CreateTables.sql...

sqlcmd -i "CreateTables.sql" -b -o "CreateTables_output.txt"

REM Tables created!
REM Default database created!