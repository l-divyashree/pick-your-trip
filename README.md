Pick Your Trip - Full-Stack Travel Booking Application
Welcome to Pick Your Trip, a complete, full-stack travel booking web application built with an ASP.NET Core backend and a dynamic, vanilla JavaScript frontend. This project is designed to mirror the core functionalities of a modern travel website, providing a seamless experience for both users and administrators.

(Suggestion: Replace the placeholder above with a screenshot of your running application's hero section!)

✨ Features
This application is feature-complete with all the essential functionalities for a travel booking platform.

User-Facing Features
Dynamic Homepage: A visually appealing hero section with an auto-playing video background.

Live Search & Filtering: Instantly filter tour packages by keyword, destination, and price range. The page automatically scrolls to the results for a smooth user experience.

Trending Destinations: A horizontally scrolling section to showcase popular travel spots.

Detailed Package View: Users can click on any package to view a modal with a full description and itinerary.

Secure User Authentication: Clean, modal-based system for user registration and login using JWT tokens.

End-to-End Booking System: Logged-in users can book any trip, and the booking is securely saved to the database.

"My Bookings" History: A dedicated modal for logged-in users to view a list of all their past bookings.

Polished UI/UX: The application is fully responsive and includes smooth animations and transitions for a professional feel.

Administrator Features
Role-Based Access Control: A secure "Admin" button and dashboard appear only for users with the "Admin" role.

Full Content Management: Admins have complete Create, Read, Update, and Delete (CRUD) control over:

Tour Packages: Add new trips, update itineraries, change prices, and delete old packages.

Destinations: Add new travel locations, update their details, and remove them.

Tabbed Dashboard Interface: A clean, organized admin panel that separates package management from destination management.

💻 Tech Stack
This project is built with a modern, robust tech stack separating backend and frontend concerns.

Backend:

Framework: ASP.NET Core 6+ Web API

Database: Microsoft SQL Server

ORM: Entity Framework Core 6+

Authentication: JWT (JSON Web Tokens)

Frontend:

Languages: HTML5, CSS3, JavaScript (ES6+ Modules)

Styling: Tailwind CSS

Architecture: Single Page Application (SPA) model with dynamic content rendering.

🚀 Getting Started
Follow these instructions to get the project running on your local machine.

Prerequisites
.NET 6 SDK or later

Visual Studio 2022 with the ASP.NET and web development workload

SQL Server Express or any other SQL Server instance

1. Database Setup
Configure Connection String: Open the appsettings.json file in the PickYourTrip.API project. Make sure the DefaultConnection string points to your local SQL Server instance.

Run Database Migrations: Open the Package Manager Console in Visual Studio (View > Other Windows > Package Manager Console). Then, run the following command to create and update your database schema:

Update-Database -Project PickYourTrip.DataAccess -StartupProject PickYourTrip.API

2. Running the Application
Set Startup Projects: In Visual Studio's Solution Explorer, right-click the Solution and select "Configure Startup Projects...".

Choose the "Multiple startup projects" option.

Set the Action for both PickYourTrip.API and PickYourTrip.Web to "Start".

Click OK.

Press F5 or click the "Start" button to run the application. Two browser windows will open—you can close the one for the API (it will likely show a Swagger UI) and use the one displaying the main website.

🔑 Usage Guide
Creating an Admin User
Register a New User: Use the "Login" button and then the "Register" link to create a new account.

Update Role in Database:

Open SQL Server Management Studio and connect to your database.

Find the dbo.Users table.

Locate the user you just created and change their Role column from "User" to "Admin".

Log In as Admin: You can now log in with that user's credentials to see the "Admin" button and access the dashboard.

Admin Credentials
For quick testing, you can use the following pre-configured admin account:

Email: d@gmail.com

Password: 1111

(Note: This assumes this user exists in your database and has the "Admin" role.)


User Credentials

For quick testing, you can use the following pre-configured admin account:

Email: jere@gmail.com

Password: 1111



Project Structure
The solution is organized into logical projects:

PickYourTrip.Domain: Contains the core entity classes (POCOs) like TourPackage, User, etc.

PickYourTrip.DataAccess: Contains the DbContext and repository classes responsible for all database interactions.

PickYourTrip.API: The main backend project, containing API controllers, DTOs, and the Program.cs configuration.

PickYourTrip.Web: The frontend project, which serves the index.html file and other static assets from the wwwroot folder.
