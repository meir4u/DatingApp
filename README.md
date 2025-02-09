# DatingApp Documentation

## Overview
DatingApp is a web-based application designed to facilitate connections between users through a modern matchmaking system. It follows a **Clean Architecture** approach and utilizes .NET 8 for backend services.

## Features
- **User Authentication**: Supports JWT-based authentication and Google OAuth login.
- **Role-Based Access Control (RBAC)**: Fine-grained role management for different user types.
- **Matchmaking System**: Users can like/dislike profiles.
- **Messaging**: Secure, real-time chat system.
- **Profile Management**: Users can edit and update their personal details.
- **Activity Tracking**: Records user last active timestamps.
- **Real-time Communication**: Uses SignalR for real-time messaging and notifications.
- **Image Upload & Storage**: User profile images are stored securely using Cloudinary.
- **Logging & Monitoring**: Tracks application events and errors.
- **Email Notifications**: Sends automated emails for account verification and notifications.

## Technologies Used
- **.NET 8**
- **Entity Framework Core** (if applicable)
- **MediatR** (for CQRS pattern and pipeline behaviors)
- **FluentValidation** (for DTO validation)
- **SignalR** (for real-time WebSockets communication)
- **Docker** (for containerization)
- **Cloudinary** (for image storage)
- **Serilog** (for logging)
- **SMTP / SendGrid** (for email notifications)

## Logging
### How Logging Works
Logging is handled using **Serilog**, which captures application events, errors, and debugging information.

### Logging Configuration
Example of logging settings in `appsettings.json`:
```json
"Serilog": {
  "Using": ["Serilog.Sinks.Console", "Serilog.Sinks.File"],
  "MinimumLevel": "Information",
  "WriteTo": [
    { "Name": "Console" },
    { "Name": "File", "Args": { "path": "logs/log-.txt", "rollingInterval": "Day" } }
  ]
}
```

### Log Storage
- **Console Logs**: Used for real-time debugging.
- **File Logs**: Stored in `logs/` directory with daily rotation.
- **Cloud Logging (Optional)**: Can be integrated with **Application Insights**, **Seq**, or **Elasticsearch**.

## Email Notifications
### How Emails Are Sent
Emails are sent using an **SMTP provider** (e.g., SendGrid, Mailgun, or custom SMTP server).

### Email Configuration
To configure email settings, update `appsettings.json`:
```json
"EmailSettings": {
  "SmtpServer": "smtp.sendgrid.net",
  "SmtpPort": 587,
  "SenderEmail": "no-reply@datingapp.com",
  "SenderName": "DatingApp Team",
  "ApiKey": "your-sendgrid-api-key"
}
```

### Automated Emails Sent
| Email Type       | Trigger Condition           |
|-----------------|----------------------------|
| Account Verification | User registers a new account |
| Password Reset | User requests a password reset |
| Match Notification | User receives a new match |
| Message Notification | User gets a new message |

## Project Structure
```
DatingApp.sln
│── Api/
│   ├── Core/
│   │   ├── DatingApp.Application/
│   │   │   ├── DTOs/
│   │   │   ├── Behaviors/
│   │   │   ├── Services/
│   │   ├── DatingApp.Domain/
│   ├── Infrastructure/  (if exists)
│   ├── Presentation/
│── .dockerignore
│── .editorconfig
│── .gitignore
│── logs/ (if logging is enabled)
```

## Installation & Setup
1. **Clone the repository**:
   ```sh
   git clone https://github.com/your-repo/DatingApp.git
   cd DatingApp
   ```
2. **Set up environment variables**:
   - Create an `.env` file in the root directory with the following variables:
     ```env
     CONNECTION_STRING=your_database_connection_string
     JWT_SECRET=your_jwt_secret_key
     GOOGLE_CLIENT_ID=your_google_client_id
     GOOGLE_CLIENT_SECRET=your_google_client_secret
     CLOUDINARY_CLOUD_NAME=your_cloud_name
     CLOUDINARY_API_KEY=your_api_key
     CLOUDINARY_API_SECRET=your_api_secret
     SMTP_SERVER=smtp.sendgrid.net
     SMTP_PORT=587
     EMAIL_SENDER=no-reply@datingapp.com
     EMAIL_API_KEY=your-sendgrid-api-key
     ```

3. **Run the application**:
   ```sh
   dotnet run --project Api/Presentation/DatingApp.Api
   ```
4. **Run the application with Docker**:
   ```sh
   docker-compose up --build
   ```

## API Endpoints
### Authentication
| Method | Endpoint               | Description          |
|--------|------------------------|----------------------|
| POST   | /api/auth/login        | Login with email & password |
| POST   | /api/auth/register     | Register new user |
| POST   | /api/auth/google-login | Google OAuth login |

### User Management
| Method | Endpoint             | Description          |
|--------|----------------------|----------------------|
| GET    | /api/users           | Get list of users   |
| GET    | /api/users/{id}      | Get user by ID      |
| PUT    | /api/users/{id}      | Update user profile |
| DELETE | /api/users/{id}      | Delete user account |

### Matchmaking
| Method | Endpoint                | Description             |
|--------|-------------------------|-------------------------|
| POST   | /api/likes              | Like a user            |
| GET    | /api/likes              | Get liked users        |
| DELETE | /api/likes/{id}         | Unlike a user          |

### Messaging
| Method | Endpoint                | Description             |
|--------|-------------------------|-------------------------|
| GET    | /api/messages           | Get messages           |
| POST   | /api/messages           | Send a message         |
| DELETE | /api/messages/{id}      | Delete a message       |

## Contributors
- Meir Achildiev

## License
This project is licensed under a **Proprietary License**.

### License Terms
- The source code may be used **only for study and reference purposes**.
- **Copying, modifying, distributing, or using this project for commercial purposes is strictly prohibited**.
- Any use beyond studying the code requires explicit permission from the project owner.

For inquiries regarding permissions or licensing, please contact Meir Achildiev.



