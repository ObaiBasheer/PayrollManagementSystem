![di](https://github.com/ObaiBasheer/PayrollManagementSystem/assets/53649412/4bd8193a-ff6a-4a3b-ac6e-5935af65d538)




# Salary Request Management API Documentation
Overview
The Salary Request Management API is designed to handle salary requests for employees. The API allows the creation of salary requests, the addition of salary items to requests, and approval processes by accountants and managers. It is built using .NET Core with a layered architecture including repository, service, and controller layers. Authentication is handled using JWT Bearer tokens, and Swagger is used for API documentation.

# Project Structure
- Models
    Models represent the data structure used in the application.

![image](https://github.com/ObaiBasheer/PayrollManagementSystem/assets/53649412/c8b832df-68d7-49fa-83d7-3dc83347002f)

# Repositories
   Repositories are responsible for data access and encapsulate the logic required to access data sources.

- Interfaces:


![image](https://github.com/ObaiBasheer/PayrollManagementSystem/assets/53649412/8f462fdc-662b-43e8-ace8-803e4b14558a)

 - Implementations:
![image](https://github.com/ObaiBasheer/PayrollManagementSystem/assets/53649412/3272626b-aeff-48de-9952-dc30e70d7039)
![image](https://github.com/ObaiBasheer/PayrollManagementSystem/assets/53649412/18fd048d-0da0-466a-8181-4995d137bad0)
![image](https://github.com/ObaiBasheer/PayrollManagementSystem/assets/53649412/d240b234-0ebe-4a99-977e-ffb2f287c2cb)

# Services
   Services contain business logic and interact with repositories.

- Interfaces:

- ![image](https://github.com/ObaiBasheer/PayrollManagementSystem/assets/53649412/54978229-9cbd-493a-92d7-8559df5cb83b)


etc...


# Logic and Flow
 - Create Salary Request:

 - The accountant creates a salary request with a name (e.g., "May Salary Request").
Controller: CreateSalaryRequest method.
Service: CreateSalaryRequestAsync method.
Repository: AddSalaryRequestAsync method.
Add Salary Items to Request:

- The accountant adds salary items to the request from the salary list.
Controller: AddSalaryRequestItem method.
Service: AddSalaryRequestItemAsync method.
Repository: GetSalaryByIdAsync, GetSalaryRequestByIdAsync, AddSalaryRequestItemAsync methods.
Approval by Accountant:

- The accountant approves the entire salary request.
Controller: ApproveByAccountant method.
Service: ApproveSalaryRequestByAccountantAsync method.
Repository: GetSalaryRequestByIdAsync, UpdateSalaryRequestAsync methods.
Approval by Manager:

- The manager approves the salary request after the accountant's approval.
Controller: ApproveByManager method.
Service: ApproveSalaryRequestByManagerAsync method.
Repository: GetSalaryRequestByIdAsync, UpdateSalaryRequestAsync methods.
Rejection of Request:

- An accountant or manager can reject a request.
Controller: RejectRequest method.
Service: RejectSalaryRequestAsync method.
Repository: GetSalaryRequestByIdAsync, UpdateSalaryRequestAsync methods.


# Conclusion
This documentation outlines the structure,  and logic used in the Salary Request Management API. The layered architecture ensures the separation of concerns, making the application maintainable and scalable. JWT Bearer authentication secures the API, ensuring that only authorized users can perform actions. Swagger integration provides an easy way to test and document the API.


# Test Users 
 - admin@example.com (Admin) => Password123!
 - manager@example.com (Manager) => Password123!
 - accountant@example.com (accountant) => Password123!





