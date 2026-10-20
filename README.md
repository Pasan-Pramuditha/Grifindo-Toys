# Grifindo Toys Management System

A Windows Forms desktop application for managing Grifindo Toys company operations including employee management, salary processing, branch management, and more.

## Features

- 👥 **Employee Management** - Add, update, delete employee records
- 💰 **Salary Management** - Calculate and issue salaries with OT and deductions
- 🏢 **Branch Management** - Manage company branches
- ⚙️ **Settings** - Configure OT rates and tax rates
- 🎁 **Bonus Management** - Handle employee bonuses
- 📋 **Registration** - User registration and login

## Tech Stack

- **Platform:** Windows Forms (.NET Framework)
- **Language:** C#
- **Database:** SQL Server LocalDB
- **UI Library:** ComponentFactory Krypton Suite

## Requirements

- Windows 10 or later
- Visual Studio 2022 (Community / Professional / Enterprise)
- SQL Server LocalDB (included with Visual Studio)

## How to Run

1. Clone this repository
2. Double-click **`START.bat`**
3. The app will automatically:
   - Find MSBuild
   - Start SQL LocalDB
   - Build the project
   - Launch the application

## Salary Calculation Formula

```
Daily Rate  = Basic Salary ÷ 30
No Pay      = Daily Rate × Absent Days
OT Pay      = OT Hours × OT Rate
Gross Pay   = Basic + Allowances + OT Pay
Net Salary  = Gross Pay - No Pay - (Gross Pay × Tax%)
```

## Project Structure

```
Grifindo Toys/
├── Form1.cs            # Login form
├── Dashboardcs.cs      # Main dashboard
├── Employee.cs         # Employee management
├── Branch.cs           # Branch management
├── Registration.cs     # User registration
├── Issue Salary.cs     # Salary issuance
├── Setting.cs          # System settings
├── Bonus.cs            # Bonus management
├── DbConfig.cs         # Database connection config
└── START.bat           # One-click launcher
```