# Grifindo Toys - Payroll & Employee Management System

![C#](https://img.shields.io/badge/Language-C%23-239120?style=for-the-badge&logo=c-sharp)
![.NET Framework](https://img.shields.io/badge/.NET%20Framework-4.7.2%2B-512BD4?style=for-the-badge&logo=dotnet)
![SQL Server](https://img.shields.io/badge/Database-SQL%20LocalDB-CC292B?style=for-the-badge&logo=microsoft-sql-server)
![UI](https://img.shields.io/badge/UI-Krypton%20Suite-007ACC?style=for-the-badge)

A complete desktop management solution developed for **Grifindo Toys**, providing full automation for employee administration, salary calculation, payroll settings, branch details, and authentication.

---

## 📌 Table of Contents

- [Overview](#-overview)
- [System Features & Modules](#-system-features--modules)
- [Default Login Credentials](#-default-login-credentials)
- [Salary Calculation Logic](#-salary-calculation-logic)
- [Database Schema](#-database-schema)
- [Connection String Configuration](#-connection-string-configuration)
- [Prerequisites](#-prerequisites)
- [Setup & Run Guide for Any Computer](#-setup--run-guide-for-any-computer)
- [Troubleshooting & Common Errors](#-troubleshooting--common-errors)
- [Project File Structure](#-project-file-structure)

---

## 📖 Overview

The **Grifindo Toys Management System** streamlines day-to-day HR operations and monthly payroll execution. It replaces manual salary and attendance logging with automated calculations for overtime, taxes, allowances, and no-pay deductions, backed by SQL Server LocalDB and an interactive Krypton Suite Windows Forms UI.

---

## 🚀 System Features & Modules

### 1. 🔐 User Authentication & Registration (`Form1.cs` & `Registration.cs`)
- Secure login verification against registered users.
- New user registration module to add administrators with validation (First Name, Last Name, Email, DOB, Contact, Username, and Password).

### 2. 📊 Executive Dashboard (`Dashboardcs.cs`)
- Live employee count metrics (Total Employees, Total Managers).
- Quick navigation shortcuts to all functional modules.

### 3. 👥 Employee Management (`Employee.cs`)
- Add, update, view, and delete employee records.
- Records captured:
  - `Emp_ID`, `Name`, `Address`, `Gender`, `DOB`, `Contact`, `Position`, `Join_Date`, and `Monthly_Salary`.
- Interactive DataGridView with auto-filled fields on row selection.

### 4. ⚙️ Payroll & System Settings (`Setting.cs`)
- Configure payroll cycles and financial parameters:
  - `Setting_ID`
  - `Start_date` & `End_date` (Pay cycle date range)
  - `Ot_rate` (Hourly overtime rate)
  - `Tax_rate` (Government tax deduction percentage)
- Fully manageable with Add, Update, Delete, and Search operations.

### 5. 💰 Salary Calculation & Issuance (`Issue Salary.cs`)
- Dynamically fetches employee basic salary and active setting rates.
- Attendance tracker: typing **Present Days** automatically computes **Absent Days** (`30 - Present`).
- Computes **No Pay**, **OT Pay**, **Gross Pay**, and **Net Salary**.
- Saves and updates salary records in `Salary_Tbl`.
- Interactive salary history DataGridView.

### 6. 🏢 Branch Management (`Branch.cs`)
- Manage company branches across locations.
- Attributes: `Branch_ID`, `Branch_Name`, `Location`, and `Contact`.

### 7. 🎁 Bonus Management (`Bonus.cs`)
- Track and update annual or performance bonuses.

---

## 🔑 Default Login Credentials

Use the following default account to sign in after launching the system:

| Username | Password | Role |
| :--- | :--- | :--- |
| **`admin`** | **`admin`** | Administrator |

*(You can also register a new user from the registration window).*

---

## 🧮 Salary Calculation Logic

The payroll module calculates employee payouts using the following rules:

$$ \text{Daily Rate} = \frac{\text{Monthly Salary}}{30} $$

$$ \text{No Pay Deduction} = \text{Daily Rate} \times \text{Absent Days} $$

$$ \text{OT Pay} = \text{OT Hours} \times \text{OT Rate} $$

$$ \text{Gross Pay} = \text{Monthly Salary} + \text{Allowances} + \text{OT Pay} $$

$$ \text{Government Tax} = \text{Gross Pay} \times \left(\frac{\text{Tax Rate}}{100}\right) $$

$$ \mathbf{\text{Net Salary}} = \text{Gross Pay} - \text{No Pay} - \text{Government Tax} $$

---

## 🗄️ Database Schema

The database (`Grifindo_Toys`) runs locally on **SQL Server LocalDB** (`(localdb)\MSSQLLocalDB`).

| Table Name | Description | Key Columns |
| :--- | :--- | :--- |
| **`Login_Tbl`** | System user credentials | `User_Name`, `Password`, `F_Name`, `L_Name`, `Email`, `DOB`, `Contect` |
| **`Employee_Tbl`** | Employee directory | `Emp_ID`, `Name`, `Address`, `Gender`, `Join_Date`, `DOB`, `Contact`, `Position`, `Monthly_Salary` |
| **`Setting_Tbl`** | Payroll configurations | `Setting_ID`, `Start_date`, `End_date`, `Ot_rate`, `Tax_rate` |
| **`Salary_Tbl`** | Salary transaction logs | `Salary_ID`, `Setting_ID`, `Emp_ID`, `Present`, `Absent`, `OT_Hours`, `Allowances` |
| **`Branch_Tbl`** | Company branch records | `Branch_ID`, `Branch_Name`, `Location`, `Contect` |
| **`Bonus_Tbl`** | Bonus allocations | `Bonus_ID`, `Emp_ID`, `Bonus_Amount`, `Date` |
| **`Attendance_Tbl`** | Attendance logs | Attendance records |

---

## 🔗 Connection String Configuration

The system uses a centralized database connection string, ensuring that updating it in one place updates all system forms (`Form1`, `Employee`, `Issue Salary`, `Setting`, `Branch`, `Bonus`, `Registration`).

### 📍 File Locations:
1. **Central C# Provider:** [`Grifindo Toys/DbConfig.cs`](Grifindo%20Toys/DbConfig.cs) *(Primary connection string used across all forms)*
2. **Application Configuration:** [`Grifindo Toys/App.config`](Grifindo%20Toys/App.config) *(Used for DataSets & report controls)*

---

### ⚙️ How to Change / Configure the Connection String

Open [`Grifindo Toys/DbConfig.cs`](Grifindo%20Toys/DbConfig.cs) in Visual Studio or any text editor:

```csharp
namespace Grifindo_Toys
{
    public static class DbConfig
    {
        public static readonly string ConnectionString = @"<YOUR_CONNECTION_STRING_HERE>";
    }
}
```

Choose the connection string format matching your local SQL setup:

#### 1. SQL Server LocalDB with Initial Catalog (Default):
Used when `Grifindo_Toys` is attached to your local MSSQLLocalDB instance:
```csharp
public static readonly string ConnectionString = 
    @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=Grifindo_Toys;Integrated Security=True";
```

#### 2. SQL Server LocalDB with Direct File Path:
Used to directly mount the `Grifindo_Toys.mdf` file from the project directory without manually attaching to SQL Server:
```csharp
public static readonly string ConnectionString = 
    @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Grifindo_Toys.mdf;Integrated Security=True";
```

#### 3. Standard SQL Server / SQL Express (Windows Authentication):
Used when running a local standalone SQL Server or SQL Express:
```csharp
// Standard SQL Server:
public static readonly string ConnectionString = 
    @"Data Source=localhost;Initial Catalog=Grifindo_Toys;Integrated Security=True";

// Or for SQL Server Express instance:
public static readonly string ConnectionString = 
    @"Data Source=.\SQLEXPRESS;Initial Catalog=Grifindo_Toys;Integrated Security=True";
```

#### 4. SQL Server Authentication (Username & Password):
Used when connecting to a remote or password-protected SQL server:
```csharp
public static readonly string ConnectionString = 
    @"Data Source=localhost;Initial Catalog=Grifindo_Toys;User ID=sa;Password=YourPassword123;";
```

---

### 🔍 How to Find Your Server Connection String in Visual Studio:
1. Open Visual Studio 2022 and go to **View** ➡️ **Server Explorer** (or **SQL Server Object Explorer**).
2. Right-click **Data Connections** and choose **Add Connection...**
3. Choose **Microsoft SQL Server** as Data source.
4. In **Server name**, enter `(localdb)\MSSQLLocalDB` or your machine name (e.g., `localhost`).
5. Under **Select or enter a database name**, select `Grifindo_Toys` (or browse to `Grifindo_Toys.mdf`).
6. Click **Test Connection** to confirm.
7. Once connected, click the connection in Server Explorer, look at the **Properties window** (bottom right), and copy the value from **Connection String**.
8. Paste this string into [`DbConfig.cs`](Grifindo%20Toys/DbConfig.cs) and [`App.config`](Grifindo%20Toys/App.config).

---

## 💻 Prerequisites

Before running the application, make sure your computer has the following installed:

1. **Operating System:** Windows 10 or Windows 11 (64-bit recommended)
2. **Visual Studio 2022:** (Community, Professional, or Enterprise)
   - Must have the **.NET desktop development** workload checked during installation.
   - Includes **.NET Framework 4.7.2+ Developer Pack** and **MSBuild**.
3. **Microsoft SQL Server LocalDB:**
   - Bundled with Visual Studio (under *.NET desktop development* -> *SQL Server Express 2019 LocalDB*).
   - Can also be checked via terminal: `sqllocaldb info`

---

## ⚡ Setup & Run Guide for Any Computer

If you or a colleague have cloned or downloaded this project to a new computer, follow these exact steps to run it smoothly:

---

### 📥 Step 1: Clone or Download the Project
- **Option A (Using Git):**
  ```bash
  git clone https://github.com/Pasan-Pramuditha/Grifindo-Toys.git
  cd Grifindo-Toys
  ```
- **Option B (Download ZIP):**
  Click the green **Code** button on GitHub ➡️ **Download ZIP**, and extract it into a folder on your computer.

---

### 🗄️ Step 2: Start SQL Server LocalDB
The project database runs on Microsoft SQL Server LocalDB. Ensure the LocalDB instance is created and running:

1. Open **Command Prompt (CMD)** or **PowerShell** as Administrator or normal user.
2. Run:
   ```cmd
   sqllocaldb create MSSQLLocalDB
   sqllocaldb start MSSQLLocalDB
   ```
3. Verify that the state says `State: Running` by running:
   ```cmd
   sqllocaldb info MSSQLLocalDB
   ```

---

### 🔗 Step 3: Attach the Database (`Grifindo_Toys.mdf`)
The database files (`Grifindo_Toys.mdf` and `Grifindo_Toys_log.ldf`) are already included inside the `Grifindo Toys/` folder. On a new machine, attach it to your SQL Server using one of these simple methods:

#### 🔹 Method A: Using Visual Studio (Easiest - 1 Minute)
1. Open Visual Studio 2022.
2. Go to top menu: **View** ➡️ **SQL Server Object Explorer** (or **Server Explorer**).
3. Expand **SQL Server** ➡️ **(localdb)\MSSQLLocalDB**.
4. Right-click on **Databases** ➡️ Select **Attach Database...**
5. Click **Add** ➡️ Browse and select:
   `...\Grifindo-Toys\Grifindo Toys\Grifindo_Toys.mdf`
6. Click **OK**. The database `Grifindo_Toys` is now attached and ready!

#### 🔹 Method B: Zero-Manual-Attach Mode (Via Code)
If you don't want to attach the MDF manually in SQL Server, open [`Grifindo Toys/DbConfig.cs`](Grifindo%20Toys/DbConfig.cs) and change the line to:
```csharp
public static readonly string ConnectionString = 
    @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Grifindo_Toys.mdf;Integrated Security=True";
```
*(This automatically mounts the MDF file located in your project folder when the app starts).*

#### 🔹 Method C: Using SQL Server Management Studio (SSMS)
1. Open SSMS and connect to Server Name: `(localdb)\MSSQLLocalDB`.
2. Right-click **Databases** ➡️ **Attach...** ➡️ Click **Add**.
3. Select `Grifindo_Toys.mdf` from your cloned folder and click **OK**.

---

### 💻 Step 4: Open the Solution in Visual Studio 2022
1. In File Explorer, go to the project folder and double-click:
   [`Grifindo Toys.sln`](Grifindo%20Toys.sln)
2. In Solution Explorer, ensure all project files are loaded.

---

### 📦 Step 5: Restore Dependencies & Krypton UI Suite
- The **Krypton Suite 4.5.9** UI controls are bundled inside the `packages/` folder.
- In Visual Studio, right-click the solution in **Solution Explorer** (top item) and click **Restore NuGet Packages**.
- Check `References` under the `Grifindo Toys` project:
  - `ComponentFactory.Krypton.Toolkit`
  - `ComponentFactory.Krypton.Navigator`
  - If any reference has a yellow warning icon, right-click **References** ➡️ **Add Reference...** ➡️ **Browse** ➡️ navigate to `packages\Krypton.Components.Suite.4.5.9\lib\net40\` and select the DLL files.

---

### 🚀 Step 6: Build and Run
1. Set the Solution Configuration to **Debug** and Solution Platform to **Any CPU**.
2. Press **`Ctrl + Shift + B`** to build the solution. Ensure you get:
   `========== Build: 1 succeeded, 0 failed ==========`
3. Press **`F5`** (or click the green **Start** button).

---

### 🔑 Step 7: Log In to the System
When the application launches, the Login window (`Form1`) will appear:
- **Username:** `admin`
- **Password:** `admin`
- Click **Login** to enter the main **Executive Dashboard**.
- *(You can also click **Register** to create your own new administrator account).*

---

## 🛠️ Troubleshooting & Common Errors

If anyone encounters an error when launching on their PC, find the fix below:

| # | Error Message / Symptom | Why it happens | How to Fix |
| :- | :--- | :--- | :--- |
| **1** | **`Cannot open database "Grifindo_Toys" requested by the login`** | The database MDF file is not yet attached to your machine's LocalDB. | Follow **Step 3** above to attach `Grifindo_Toys.mdf` in Visual Studio or SSMS, or switch to `AttachDbFilename` in `DbConfig.cs`. |
| **2** | **`A network-related or instance-specific error occurred while establishing a connection to SQL Server`** | The SQL LocalDB service is not currently running. | Open Command Prompt and run: <br>`sqllocaldb create MSSQLLocalDB`<br>`sqllocaldb start MSSQLLocalDB` |
| **3** | **`The type or namespace name 'ComponentFactory' could not be found`** | Visual Studio hasn't resolved the Krypton Suite DLL references. | Right-click the Solution ➡️ **Restore NuGet Packages**. Make sure the `packages/` folder exists in your cloned repository. |
| **4** | **`The targeted framework '.NETFramework,Version=v4.7.2' (or v4.8) is not installed`** | Your Visual Studio is missing the .NET Framework Developer Pack. | Open **Visual Studio Installer** ➡️ Click **Modify** on VS 2022 ➡️ Under **Individual Components**, search for `.NET Framework 4.7.2 Targeting Pack` or `4.8`, check it, and click Modify/Install. |
| **5** | **Login fails or user doesn't exist** | Default admin account is not present in the DB table. | Click the **Register** button on the Login screen and create a new username and password. |

---

## 📂 Project File Structure

```text
Grifindo-Toys/
│
├── Grifindo Toys.sln                     # Visual Studio Solution File
├── README.md                             # Project documentation
├── .gitignore                            # Git ignore configuration
│
├── packages/                             # NuGet dependencies (Krypton Suite)
│   └── Krypton.Components.Suite.4.5.9/
│
└── Grifindo Toys/                        # Main Application Project
    ├── App.config                        # Application & DB connection configuration
    ├── DbConfig.cs                       # Centralized SQL Connection provider
    ├── Program.cs                        # Application entry point
    ├── Grifindo Toys.csproj              # C# Project configuration
    │
    ├── Form1.cs                          # Login Window
    ├── Registration.cs                   # User Registration Window
    ├── Dashboardcs.cs                    # Main Dashboard Window
    ├── Employee.cs                       # Employee Management Window
    ├── Setting.cs                        # Salary & Cycle Settings Window
    ├── Issue Salary.cs                   # Salary Issuance & Calculation Window
    ├── Branch.cs                         # Branch Management Window
    ├── Bonus.cs                          # Bonus Management Window
    │
    ├── Grifindo_Toys.mdf                 # SQL Server Database primary data file
    ├── Grifindo_Toys_log.ldf             # SQL Server Database log file
    ├── Grifindo_ToysDataSet.xsd          # ADO.NET Typed DataSet schema
    └── Resources/                        # UI Icons and graphic assets
```
