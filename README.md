# InventoryAndShipmentMgmtSystemAPI
Overview
----------
This Inventory & Shipment Management System is developed using Windows Forms and utilizes an API to perform CRUD (Create, Read, Update, Delete) operations. The application enables users to manage inventory items efficiently by providing a user-friendly interface and robust backend functionality.

Features
----------
- Add, Update and delete inventory(Product) items.
- Logging of operation to handle exception.
- User-friendly interface with Windows Forms
- Unit test for code coverage(NUnit Framework). 
- Dependency Injection(Constructor Injection).

Technical Stack
-----------------
- Frontend - .NET Windows Form, C#, WEB API(REST API)
- Backend - .NET Core(.NET 8), C#, SQL Server, ADO.NET, Entity Framework

Setup Instructions
--------------------
Prerequisites
- Visual Studio 2022
- Windows Forms: net8.0-windows
- API: net8.0
- SQL Server 2019 or any compatible database

Installation
---------------
1. Clone the repository:
   git clone API -> https://github.com/imanupamsingh/InventoryAndShipmentMgmtSystemAPI 
   git clone UI -> https://github.com/imanupamsingh/InventoryAndShipmentMgmtSystemUI

2. Open the Solution:
   - API -> Open InventoryShipmentMgmtAPI.sln in Visual Studio.
   - UI -> Open InventoryShipmentMgmtUI.sln in Visual Studio.

3. Configure the Database:
   - Update the connection string in appsettings.json to point to your database.

4. Run Migrations:
   - Open the Package Manager Console in Visual Studio and run:

5. Build the solution and run the application from Visual Studio.

Usage
--------
1. Add Inventory Items:
   - Use the form to input product details for new inventory items and add them to the database.

2. Inventory List   
   - Display the inventory details for the product.
   - Product details can be delete & edit from the inventory list.

2. Edit and Delete Items:
   - Select an item from the list and edit its details on another form. Delete the product from list.

3. Logging
   - All operations are logged in Logs folder in separate text file on the basis of dates to handle exception & debugging purposes.


