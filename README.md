# DVLD (Driving & Vehicle License Department) System

This is a C# Windows Forms desktop application built to manage a driving and vehicle license department. It's a complete system that handles everything from applicants and tests to issuing licenses and detaining them.

This project was migrated from a SQL Server database to a portable SQLite database to make it a self-contained, shareable application.

![Screenshot of the DVLD login screen](URL-to-your-screenshot-goes-here)
![Screenshot of the DVLD main dashboard](URL-to-your-screenshot-goes-here)

## Features

* **User Management:** Secure login and user management.
* **People Management:** Add, edit, and delete user profiles.
* **Applications:** Manage all local and international driving license applications.
* **Test Management:** Schedule and grade vision, written, and street tests.
* **License Management:** Issue new licenses, detain licenses, and manage license classes.
* **Portable Database:** Uses SQLite, so the entire application and its data are portable.

## Technologies Used

* **C#** (.NET Framework)
* **Windows Forms (WinForms)** for the user interface.
* **SQLite** for the database.
* **ADO.NET** for all database communication (using `System.Data.SQLite`).

## How to Run

1.  Clone or download the repository as a ZIP file.
2.  Open the `YourSolutionName.sln` file in Visual Studio.
3.  Rebuild the solution (Build > Rebuild Solution).
4.  Press **F5** or click the "Start" button to run.

The `DVLD.db` database file is included in the project and will be copied to the output directory automatically.
