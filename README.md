# Introduction 
Payslip service is REST API that has two functionalities

1. Submit an employee to save
2. Obtain employees Payslips

#Requirements

1. Submitting an employee requires admin claims
2. Tax for salaries more than 150k is 40% otherwise 30% (keep it simple)
3. No need for actual database


#Your Task

* Use your own suppositions to implement the service and test it
* Don't spend long time on repeated things (like lengthy tests)
* Create PR


Solution structure

│   .gitignore
│   README.md
│
└───src
    │   Cw.PayslipService.sln
    │   CW.postman_collection.json
    │
    ├───Cw.PayslipService
    │   │   appsettings.Development.json
    │   │   appsettings.json
    │   │   Cw.PayslipService.csproj
    │   │   Cw.PayslipService.csproj.user
    │   │   Program.cs
    │   │   Startup.cs
    │   │
    │   │
    │   ├───Controllers
    │   │       AuthenticationController.cs
    │   │       PayslipController.cs
    │   │
    │   ├───Models
    │   │       AuthCredentials.cs
    │   │       Employee.cs
    │   │       Payslip.cs
    │   │
    │   │
    │   ├───Properties
    │   │       launchSettings.json
    │   │
    │   ├───Repository
    │   │       IPayslipRepository.cs
    │   │       PayslipRepository.cs
    │   │
    │   └───Services
    │           AuthenticationManager.cs
    │           IAuthenticationManager.cs
    │           PayslipCalculator.cs
    │           ServicesExtensions.cs
    │
    ├───Cw.PayslipService.Tests
    │   │   Cw.PayslipService.Tests.csproj
    │   │
    │   │
    │   ├───Integration
    │   │       PayslipControllerTests.cs
    │   │
    │   └───Unit
    │           PayslipCalculatorTests.cs
    │
    └───Cw.Platform.Jwt
	Cw.Platform.Jwt.csproj
	IJwtProvider.cs
	JwtProvider.cs

Notes:
I did not use the separate project Cw.Platform.Jwt to generate the jwt token but included that in the AuthenticationManager service.
I tried to enable JWT Token in SwaggerUI so I could authenticate the Swagger session but it was not working. 
I switched to Postman.
You will find in the solution the CW.postman_collection.json postman collection with the requests for the different endpoints.
