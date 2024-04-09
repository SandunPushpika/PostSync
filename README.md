# Project Name: PostSync

## Overview:
This project is a backend system developed using .NET Core 8 and PostgreSQL, aimed at facilitating post scheduling and distribution across various platforms. The system allows users to schedule posts and send them to different platforms including Facebook, Instagram, WhatsApp, and LinkedIn.

## Features:
  * **User Authentication:** Secure user authentication system to ensure only authorized users can access the platform.
  * **Post Scheduling:** Capability to schedule posts for future publishing on supported platforms.
  * **Platform Integration:** Integration with Facebook, Instagram, WhatsApp, and LinkedIn APIs for seamless post distribution.
  * **User Connectivity:** Ability for users to connect their accounts from supported platforms for post distribution.
  * **Dashboard:** An intuitive dashboard for users to manage their scheduled posts and connected platforms.

## Technologies Used:
  * **.NET Core 8:** Primary framework for backend development.
  * **PostgreSQL:** Relational database management system used for data storage.
  * **Platform APIs:** Integration with Facebook, Instagram, WhatsApp, and LinkedIn APIs.
  * **Dapper:** ORM used for database interaction.
  * **JWT Authentication:** Token-based authentication system for securing endpoints.
  * **Swagger:** API documentation and testing tool.

## Setup Instructions:
  
  ### Clone Repository:

      
      git clone https://github.com/your/repository.git
      
    
  ### Install Dependencies:

    dotnet restore
  
  ### Database Setup:
  
  Ensure PostgreSQL is installed and running.
  Update database connection string in appsettings.json.
 
  ### Configuration:
  
  Update API keys and secrets for platform integrations in appsettings.json.
  Configure JWT settings.
  
  ### Run Application:
  
    dotnet run
  
  ### Access API Documentation:
  
  Open your browser and navigate to https://localhost:{port}/swagger/index.html.
  Explore available endpoints and test them directly from Swagger UI.

Contact:
For any inquiries or issues regarding the project, feel free to contact us at sandun.pushpika123@gmail.com. I appreciate your feedback!
