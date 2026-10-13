 prog-part2
restful web api for managing community events# 🏃‍♂️ Races - Event Management System

A RESTful Web API built with ASP.NET Core for managing community racing events (Run, Walk, Cycle).

This system allows organisers to create events and participants to enrol and track results.

 Features

- User Management: Register/Login with hashed passwords & role-based auth (organiser / participant)
- Event Management: CRUD for events with location, date, type
- Categories: Age and distance categories per event (e.g., 5km, 10km, Half Marathon)
- Routes: Distance and route mapping per event
- *Enrolments: Participants enrol in events/categories
- Results: Track finishTime, position, recordedAt
- Weather Integration: Store temperature & rain probability per event

Tech Stack

- Backend: ASP.NET Core Web API (C#)
- Database: SQL Server - `Races` DB
- Auth: Session-based Auth with `SessionCheck` Middleware
- Password Security: BCrypt Hashing

Database Schema

- `Users(userID, fullName, password, role, email)`
- `Events(eventID, userID, EventName, description, EventDate, location, eventType)`
- `categories(categoryID, eventID, categoryType, age)`
- `eventRoute(routeID, eventID, routeName, distance)`
- `Enrolments(enrolmentID, userID, eventID, categoryID, enrolmemtDate, status)`
- `Results(resultID, enrolmentID, finishTime, position, recordedeAT)`
- `weather(weatherID, eventID, temperature, rainProbability)`

Getting Started

1. Clone the repo:

    https://github.com/kagishontita10/prog-part2
