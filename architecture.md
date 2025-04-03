# ToDo List API Application Architecture

## Overview

This document describes the architecture of a simple CRUD (Create, Read, Update, Delete) ToDo list application built with .NET 9. The application provides a RESTful API for managing tasks in a todo list, with data stored in text file (txt) (no external database required).

## Functional Requirements

The ToDo List API should support the following functionality:

1. Create a new task with title, description, due date, and priority
2. Retrieve a list of all tasks
3. Retrieve a specific task by ID
4. Update an existing task's details
5. Mark a task as completed
6. Delete a task
7. Filter tasks by status (completed/active)
8. Filter tasks by priority
9. Sort tasks by due date

## Technical Architecture

### Technology Stack

- **Framework**: .NET 9.0
- **API**: ASP.NET Core Web API
- **Data Storage**: Stored in text file (no external database)
- **Project Type**: Web API
- **Architecture Pattern**: Clean Architecture


## Notes
- During creation of workspace remember about *.csproj files 
- Application should be ready to build and run after creation workspace
- Domain has no dependencies
- Application depends only on Domain
- Infrastructure depends on Domain and Application
- API depends on all layers
- Remember about all System namespaces
- Implement all Interfaces
- Add required references for Swagger