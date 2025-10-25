Payroll Project
==============

Project Overview
- Stack: ASP.NET Core (.NET9), Razor Pages + MVC controllers, Entity Framework Core, ASP.NET Core Identity, SQL Server, jQuery DataTables, SweetAlert2, Bootstrap.
- Goal: HR and Finance workflows for Employee, Shift, Attendance, Leave, Allowance/Deduction, Payroll, and Salary Slip.

Current Status (Progress)
- Phase1 (Foundation) complete
 - Solution set up, DB connection configured.
 - All7 core entity models created and added to `ApplicationDbContext`:
 - `Employee`, `Shift`, `Attendance`, `Leave`, `AllowanceDeduction`, `Payroll`, `SalarySlip`.
 - Initial and subsequent migrations added and applied.
 - Identity scaffolding added (login/register); role setup pending.
- Phase2 (Features) in progress
 - Employee module implemented end-to-end with reusable front-end CRUD components.
 - Generic, reusable front-end scripts introduced:
 - `wwwroot/js/crud-datatable.js` – wraps DataTables for generic list actions.
 - `wwwroot/js/crud-modal.js` – generic modal loader and submitter for Create/Edit.
 - Shared AJAX helpers:
 - `wwwroot/js/common-ajax.js` – GET/POST wrappers, confirm/toasts, anti-forgery header injection.

Employee Module (Delivered)
- Controller: `Controllers/EmployeeController.cs`
 - List: `GET /Employee/GetEmployeesJson` returns `{ data: [...] }` for DataTables.
 - Create: 
 - `GET /Employee/Create` renders the form (as a modal body).
 - `POST /Employee/Create` validates and returns JSON `{ success, message }`.
 - Edit:
 - `GET /Employee/Edit/{id}` reuses the Create view with populated data.
 - `POST /Employee/Edit` validates and returns JSON `{ success, message }`.
 - Details: `GET /Employee/Details/{id}` renders a full view.
 - Delete: `POST /Employee/Delete/{id}` returns JSON `{ success, message }`.
- Views:
 - List: `Views/Employee/Index.cshtml` uses `CrudTable` + `CrudModal` and page data-attributes for routes.
 - Form: `Views/Employee/Create.cshtml` (Layout = null) rendered into the modal body.
 - Details: `Views/Employee/Details.cshtml` – professional card layout with formatted Taka amounts.
- Front-end flow
 - DataTables fetches employees via `GetEmployeesJson` and renders columns (dates formatted, Taka currency for salary).
 - Add/Edit load the form into a Bootstrap modal; submit via AJAX; success toast; table reload.
 - Delete confirms via SweetAlert; posts with anti-forgery header; reloads on success.

Reusable CRUD Front-end (How to use for any module)
- Files
 - `wwwroot/js/crud-datatable.js` exposes `CrudTable.init(options)`
 - Required options: `tableSelector`, `listUrl`, `columns`.
 - Optional: `idField` (default `id`), `actions` ({ `renderButtons`, `editSelector`, `deleteSelector`, `onEdit(id)`, `deleteUrl(id)` }), `dataSrc` (default `data`), `dataTables` (extra DT options), `onReload`.
 - `wwwroot/js/crud-modal.js` exposes `CrudModal.init(options)`
 - Required: `modalSelector`, `routes` ({ `createGet`, `editGet(id)` })
 - Optional: `addBtnSelector`, `createTitle`, `editTitle`, `onSaved(resp)`.
- Patterns to follow
 - List endpoint should return `{ data: [...] }`.
 - Create/Edit POST should return JSON `{ success, message }` for consistent UX.
 - Use anti-forgery token – the page sets `window.__AntiForgeryToken`; `CommonAjax` sends `RequestVerificationToken` automatically.
 - Provide module routes to the page via data- attributes or a small `routes` object.

Setup & Run
- Prerequisites: .NET9 SDK, SQL Server.
- Configure DB: set connection string in `appsettings.json`.
- Apply migrations and run the app in your preferred environment.

Roadmap (Upcoming Work)
- HR Module
 - Shift Management: CRUD with `CrudTable`/`CrudModal`.
 - Attendance Management: Daily entries, summaries, and validations.
 - Leave Management: CRUD, policies, and balances.
 - Role-based access for "HR Admin"; secure pages and actions.
- Finance Module
 - Allowance/Deduction Management: CRUD and categories.
 - PayrollService: Core salary calculation engine pulling real data (employee, attendance, leave, allowances/deductions), proration, and tax rules.
 - Payroll Processing UI: run for period, preview, approve.
 - Salary Slip Generation: HTML/PDF output and archive.
 - Role-based access for "Finance Admin".
- Cross-Cutting
 - Seed roles and default admin users; wiring Identity to pages.
 - Client-side validation inside modals (optionally parse unobtrusive validation on modal load).
 - Unit tests for `PayrollService` and key repositories.
 - Logging/telemetry and error-handling improvements.

Conventions & Notes
- JSON formats
 - Lists: `{ data: [...] }`
 - Mutations: `{ success: bool, message: string }`
- Anti-forgery
 - Send `RequestVerificationToken` header on POST; provided by `CommonAjax`.
- Formatting
 - Dates rendered in UI as `yyyy-MM-dd`.
 - Currency: Taka via `Intl.NumberFormat('en-BD', { style: 'currency', currency: 'BDT' })`.
- File Map
 - Controllers: `Controllers/*Controller.cs`
 - Views: `Views/<Module>/*`
 - ViewModels: `ViewModel/*`
 - Data Models: `DataModels/*`
 - DbContext: `Data/ApplicationDbContext.cs`
 - Migrations: `Data/Migrations/*`
 - Reusable JS: `wwwroot/js/crud-datatable.js`, `wwwroot/js/crud-modal.js`, `wwwroot/js/common-ajax.js`

Contributing Workflow
- Branching: `main` (stable), `develop` (integration), feature branches per module.
- Code style: keep controller JSON responses and DataTables contracts consistent.
- Prefer reusing the generic CRUD front-end for all list/form pages to accelerate delivery.