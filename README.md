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

Before running the application, make sure your computer has the following installed:

1. **Operating System:** Windows 10 or Windows 11 (64-bit recommended)
2. **Visual Studio 2022:** (Community, Professional, or Enterprise)
   - Must have the **.NET desktop development** workload checked during installation.
   - Includes **.NET Framework 4.7.2+ Developer Pack** and **MSBuild**.
3. **Microsoft SQL Server LocalDB:**
   - Bundled with Visual Studio (under *.NET desktop development* -> *SQL Server Express 2019 LocalDB*).
   - Can also be checked via terminal: `sqllocaldb info`

---

## ⚡ How to Run the Project

### 📥 Step 1: Clone the Repository
Open PowerShell or Command Prompt and clone the repository:
```bash
git clone https://github.com/Pasan-Pramuditha/Grifindo-Toys.git
cd Grifindo-Toys
```

---

### 🚀 Step 2: Choose Your Way to Run

#### Option A: One-Click Automated Run via `START.bat` (Recommended)

The project includes an automated launcher script [`START.bat`](START.bat) that prepares and starts everything for you.

1. Navigate to the project root folder.
2. Double-click **`START.bat`** (or execute `.\START.bat` in CMD / PowerShell).
3. The script will automatically perform:
   - **Step 1:** Automatically locate `MSBuild.exe` from your Visual Studio 2022 installation.
   - **Step 2:** Start the `MSSQLLocalDB` SQL Server instance if it is not already running.
   - **Step 3:** Compile and build `Grifindo Toys.sln` in `Debug` mode.
   - **Step 4:** Launch the application (`Grifindo Toys.exe`).

---

#### Option B: Running via Visual Studio 2022

If you want to edit code, design forms, or debug inside Visual Studio:

1. **Start SQL LocalDB (First Time Only):**
   Open PowerShell or Command Prompt and run:
   ```cmd
   sqllocaldb create MSSQLLocalDB
   sqllocaldb start MSSQLLocalDB
   ```
2. **Open the Solution:**
   - Double-click [`Grifindo Toys.sln`](Grifindo%20Toys.sln) to open it in Visual Studio 2022.
3. **Verify NuGet / Component References:**
   - The project uses the **Krypton Suite 4.5.9** UI library, which is pre-configured in the `packages/` directory with relative HintPaths. No extra manual assembly linking is required.
4. **Build the Solution:**
   - Click **Build** > **Build Solution** (or press `Ctrl + Shift + B`).
5. **Run the Application:**
   - Ensure the solution configuration is set to **Debug** and **Any CPU**.
   - Click the green **Start** button (or press **`F5`**).

---

### 🔑 Step 3: Log In to the System

Once the application launches, the Login window (`Form1`) will appear:
- **Username:** `admin`
- **Password:** `admin`
- Click **Login** to enter the **Executive Dashboard**.
- To add additional administrative users, click **Register** to open the registration module.

---

## 🛠️ Troubleshooting & FAQs

| Issue | Solution |
| :--- | :--- |
| **"MSBuild not found" error when running `START.bat`** | Verify Visual Studio 2022 is installed in the default location (`C:\Program Files\Microsoft Visual Studio\2022\...`). Ensure the **.NET desktop development** workload is checked in the Visual Studio Installer. |
| **"Cannot connect to SQL Server" / Database error** | Open CMD and run `sqllocaldb start MSSQLLocalDB`. Verify the database file `Grifindo_Toys.mdf` is located in the `Grifindo Toys/` folder. |
| **"ComponentFactory.Krypton" missing reference error** | Check that the `packages/` directory exists in the solution root containing `Krypton.Components.Suite.4.5.9`. The `.csproj` is pre-configured with relative HintPaths pointing to this folder. |

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
