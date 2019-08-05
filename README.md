# SnackStoreAPI

Thanks for coming here! Please following the next steps to run this projects successfully :) 

1. Clone this Repo 
2. Compile the repo to restore all the Nuget Packages used in this solution
3. Change the connectionstring to your available instance of SQL Server you will find something like this: 
`    "StoreConnection": "Data Source=localhost\\[YOUR_INSTANCE];Initial Catalog=SnackStore;Integrated Security=True"` 
4. Run the solution, you can choose the default profile "IIS Express" or choose "SnackStore"
5. You will find the swagger index page, you can test some of the API Controllers here or see the definition of the methods
6. Open Postman, import collection from Link https://www.getpostman.com/collections/36ac168b15448add21f3
7. After create a new Admin user or Guest user, please copy the Token generated and Edit the collection -> go to authentication and change it for the new token generated, the token expires after 20 minutes

The solutions comes with seed data so you dont have to worry about it. You can also login with this users:
 - User: admin, Password: admin *Admin*
 - User: guest, Password: guest *Guest*

Thanks

