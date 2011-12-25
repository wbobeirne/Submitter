sqlcmd -S .\SQLExpress -i "DropDatabase.sql" -b -o "DropDatabase_output.txt"

sqlcmd -S .\SQLExpress -i "CreateDatabase.sql" -b -o "CreateDatabase_output.txt"

sqlcmd -S .\SQLExpress -i "CreateUser.sql" -b -o "CreateUser_output.txt"

sqlcmd -S .\SQLExpress -i "CreateTables.sql" -b -o "CreateTables_output.txt"

sqlcmd -S .\SQLExpress -i "InsertTestData.sql" -b -o "InsertTestData_output.txt"