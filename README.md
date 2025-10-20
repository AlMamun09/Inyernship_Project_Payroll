Payroll Project: Phased Task Plan
=================================

This plan is structured in two distinct phases. Phase 1 is a joint effort to build the project's foundation and database schema. Phase 2 enables parallel, independent work on the core application features.

**Core Strategy: Foundation First, Then Parallel Development**Both team members will first collaborate to create and deploy the complete database schema. With the database and all models in place, you can then split the work to build the application's features in parallel, working against the same shared database.

### **Phase 1: Foundation & Model Creation (Joint Task)**

This initial phase is a sequential effort to be completed by both members before feature development begins. The goal is to have a fully functional project base with a complete database schema.

**Key Tasks:**

1.  **Project & Repo Setup (Member 1):**
    
    *   Create the initial ASP.NET Core MVC project in Visual Studio.
        
    *   Set up the GitHub repository and establish a branching strategy (main, develop).
        
    *   Configure the SQL Server database connection string.
        
2.  **Create All 7 Entity Models (Joint Effort):**
    
    *   Together, define and create the C# entity classes for all 7 models: Employee, Shift, Attendance, Leave, AllowanceDeduction, Payroll, and SalarySlip.
        
    *   **Crucial:** Agree on all properties, data types, and relationships (foreign keys) during this step.
        
3.  **Set Up Database Context (Joint Effort):**
    
    *   Create the ApplicationDbContext class.
        
    *   Add DbSet properties for all 7 models.
        
    *   Configure any necessary relationships using Fluent API.
        
4.  **Initial Database Migration (Member 1):**
    
    *   Run the add-migration and update-database commands to generate the initial SQL schema with all 7 tables.
        
    *   Push the final project base with all models and the migration file to the develop branch.
        

**Milestone for Completion:** The project is on GitHub, and a successful database migration has created all 7 required tables in the SQL Server database.

### **Phase 2: Parallel Feature Development**

With the foundation complete, you can now work in parallel on your assigned modules.

#### **Member 1 (Team Lead): HR Module Development**

Focuses on the user interface and business logic for managing employees and their time-related data.

**Modules Owned:**

*   Employee Management
    
*   Shift Management
    
*   Attendance Management
    
*   Leave Management
    

**Key Tasks:**

1.  Create a feature/hr-module branch from develop.
    
2.  Implement login, registration, and role management for an "HR Admin" role using ASP.NET Core Identity.
    
3.  Build the MVC Controllers, Views, and business logic (CRUD operations) for the four modules assigned to you.
    
4.  Develop any necessary reports related to HR data (e.g., attendance summary).
    

#### **Member 2: Finance Module Development**

Focuses on the financial setup, the core calculation engine, and generating the final pay slips.

**Modules Owned:**

*   AllowanceDeduction Management
    
*   Payroll Processing
    
*   SalarySlip Generation
    

**Key Tasks:**

1.  Create a feature/finance-module branch from develop.
    
2.  Implement login and registration for a "Finance Admin" role.
    
3.  Build the MVC Controllers and Views for managing Allowances and Deductions.
    
4.  Create the core PayrollService containing the main salary calculation logic. This service will use the real ApplicationDbContext to fetch employee, attendance, and leave data.
    
5.  Develop the functionality to run the payroll for a given period, process calculations, and generate salary slips.