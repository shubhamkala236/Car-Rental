------------------DEMO OF APPLICATION-------------------

https://github.com/shubhamkala236/Car-Rental/assets/84154821/bc88c85e-5c5e-40c1-b6e3-72b93685458f

------------------SETUP APPLICATION-------------------

----------FrontEnd----------
1. Use CLI to open the frontend folder.
2. Use command " npm install" to install all the dependencies.
3. Use "npm serve" to run the frontend on localhost//:4200

----------Backend-----------
1. Open the backend.sol in visual studio code.
2. Build the application.
3. After successfull build run the IIS server.
4. Link the MSSQL database by changing the server="Your_database_name" in AppSetting.json

----------Database-----------
METHOD-1
1. Use the script file provided and open it in SQL management Studio.
2. Before Executing the script change the 'Name=' location to your SQL SERVER location example- "C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\"
3. Run the scripts

METHOD-2
1. Use the CarRent.bak file provided and use it to restore the MSSQL database.
2. On you database right click and select "Restore Files and FileGroups".
3. Then Select the name of database you want to restore.
4. Then select From Device and select he CarRendDb.bak file.
5. Click Ok to restore the database.

"WARNING" ---> Make sure to correct the DATABASE_NAME and SERVER_NAME in CONNECTION_STRING of backend.sol in AppSetting.json file.
