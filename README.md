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
- [Prerequisites](#-prerequisites)
- [How to Run](#-how-to-run)
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

## 💻 Prerequisites

Ensure the following tools are installed on your Windows PC:

1. **Operating System:** Windows 10 or Windows 11
2. **Visual Studio:** Visual Studio 2022 (Community, Professional, or Enterprise) with the **.NET desktop development** workload
3. **Database Engine:** Microsoft SQL Server LocalDB (comes pre-bundled with Visual Studio)

---

## ⚡ How to Run

### Option 1: One-Click Launcher (Recommended)
Simply double-click the **`START.bat`** script located in the repository root:
1. Detects your MSBuild installation automatically.
2. Ensures the `MSSQLLocalDB` SQL service is started.
3. Builds the project in Debug mode.
4. Automatically opens the application login screen.

### Option 2: Using Visual Studio 2022
1. Open [`Grifindo Toys.sln`](file:///c:/Users/pasan/Downloads/Grifindo-Toys-master/Grifindo-Toys-master/Grifindo%20Toys.sln).
2. Set build configuration to `Debug` and target CPU to `Any CPU`.
3. Press **F5** or click **Start** to run.

---

## 📂 Project File Structure

```text
Grifindo-Toys/
│
├── Grifindo Toys.sln                     # Visual Studio Solution File
├── START.bat                             # One-click build & launcher script
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