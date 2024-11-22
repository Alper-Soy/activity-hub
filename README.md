
# ActivityHub

This project is a full-stack application that I developed using .NET Core for the backend and
React for the frontend, deployed on Microsoft Azure, with both the application and the database
hosted on Azure services. The platform enables users to create, join, and manage activities
while also following other users to stay updated with their activities. Instead of direct messaging,
users can communicate within event detail pages, where they can post comments and chat
about the specific event. This approach enhances the collaborative nature of event planning and
participation.

---

## Prerequisites

Before you begin, make sure you have the following installed on your system:

- **[Docker](https://www.docker.com/products/docker-desktop)**: For containerizing and running the database.
- **[.NET SDK 8](https://dotnet.microsoft.com/download/dotnet/8.0)**: To run the application.
- **IDE (Optional)**: Visual Studio, JetBrains Rider, or Visual Studio Code.

---

## Setup Instructions

### 1. Clone the Repository

Clone this repository to your local machine and navigate into the project directory.

```bash
git clone <repository-url>
cd ActivityHub
```

---

### 2. Configure the Environment

Before running the application, set up the required environment variables. You can use the `appsettings.sample.json` file as a reference.

#### Steps:

1. Copy `appsettings.sample.json` to `appsettings.json`:
   ```bash
   cp appsettings.sample.json appsettings.json


### 3. Start the Database

The database is configured to run using Docker and is defined in the `docker-compose.yml` file.

#### Steps:

1. Run the following command to start the Azure SQL Edge container:
   ```bash
   docker-compose up -d
   ```
   This will:
   - Pull the `mcr.microsoft.com/azure-sql-edge` image (if not already available).
   - Start the database service on port `1433`.

2. Verify the container is running:
   ```bash
   docker ps
   ```
   You should see a container named `sql` running.

---

### 4. Run the API

Navigate to the `API` directory and start the application using the .NET CLI:

```bash
cd API
dotnet watch run
```

This command will:
- Start the API on `http://localhost:5000`.
- Automatically reload the application upon detecting code changes.

---

### 5. Access the Application

Once the API is running, open your web browser and go to:

```
http://localhost:5000
```

If everything is set up correctly, you should see the application running.

---

## Shutting Down

To stop the application and the database container:

1. Stop the database container:
   ```bash
   docker-compose down
   ```

2. Stop the API process by pressing `Ctrl + C` in the terminal running `dotnet watch run`.

---

