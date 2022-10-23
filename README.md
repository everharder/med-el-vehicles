[![.NET](https://github.com/everharder/med-el-vehicles/actions/workflows/dotnet.yml/badge.svg)](https://github.com/everharder/med-el-vehicles/actions/workflows/dotnet.yml)



# MED-EL Application Assignment 

This repository contains a vehicle management demo CLI that was developed to display good software architecture.

The minimum requirements are listed [here](assignment.pdf) and can be summerized as follows:

- Support for the following manufacturers:  KTM (motorcycles), Toyota (cars) and Honda (motorcycles and cars)
- Creation of new vehicles
- Changig of tires from summer tires (default) to winter tires
- Summerizing a cars setup by calling a `Move()` 

```
toyota.Move(); 
  “You are driving a car from Toyota.” 
honda.Move(); 
  “You are driving a motorcycle from Honda.” 
```

In addition to these requirements the following features have been implemented:

- CRUD operations on manufacturers and vehicles
- A persistence layer decoupled through a repository pattern
- Multiple repository implementations (`FileSystemRepository`, `InMemoryRepository`)
- Optional full in-memory caching for a repository
- A configuration setup where it is possible to override each default value via the appsettings.json
- Architecture based on OOP, dependency injection (DI), separation of concerns and inversion of control (IoC)
- Extensive unit testing with xUnit
