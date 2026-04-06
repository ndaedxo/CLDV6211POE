# User Stories - Part 3

## Part 3A: Advanced Filtering
**As a** booking specialist,  
**I want to** filter bookings by event type, date range, and venue availability,  
**So that** I can quickly find bookings that meet specific criteria.

### Acceptance Criteria:
- EventType lookup table is added to the database (not hardcoded values)
- Search/filter functionality includes filtering by Event Type
- Search/filter functionality includes filtering by Date Range
- Search/filter functionality includes filtering by Venue Availability
- All filters work together when combined
- UI is intuitive and user-friendly
- EventType is implemented as a proper lookup table or enum in the database

---

## Part 3B: The Migration (Local to Cloud)
**As a** cloud developer,  
**I want to** migrate the application from LocalDB/Azurite to Azure SQL Database and Azure Storage Account,  
**So that** the application runs in a live cloud environment with production resources.

### Acceptance Criteria:
- Azure SQL Database is created with schema and data migrated from LocalDB
- Azure Storage Account is created for image storage
- Application configuration is updated to use live Azure connection strings (not Azurite)
- Application successfully connects to Azure SQL Database
- Application successfully reads/writes images to Azure Storage
- Configuration uses production keys instead of development/emulator keys
- Migration preserves all existing data (tables are not empty)

---

## Part 3C: Live Deployment and Dropping of Resources
**As a** cloud developer,  
**I want to** deploy the web application to Azure App Service and then drop all resources after verification,  
**So that** the application is accessible via a live URL and cloud credits are not wasted.

### Acceptance Criteria:
- Azure App Service is created and web application is published
- Application is accessible via the Azure URL (returns 200, not 404)
- Site loads quickly without "Server Errors" on startup
- All features (Search, Booking, Images) work in the live environment
- Azure resources are dropped after verification
- Proof is provided showing all resources have been deleted
- No firewall or connection string issues when accessing the live site

---

## Part 3D: Reflective Report - Technical
**As a** student developer,  
**I want to** write a reflective technical report documenting the development journey from Part 2 to Part 3,  
**So that** I can demonstrate understanding of the migration process and Azure services used.

### Acceptance Criteria:
- Report includes a detailed description of the application's full feature list
- Report includes a component discussion of which Azure services were used and why
- Report reflects on the migration experience from LocalDB/Azurite to Azure SQL/Storage
- Report explains what configuration changes were required
- Report discusses why separation of environments is important in professional development
- Report discusses the technologies used to build the project and why they were chosen
- Report is insightful and not purely descriptive

---

## Part 3E: Theory: Advanced Cloud
**As a** cloud computing student,  
**I want to** explain advanced cloud concepts including CosmosDB, Logic Apps security, and Event Grid workflows,  
**So that** I can demonstrate deep understanding of serverless and cloud-native technologies.

### Acceptance Criteria:
- Clearly discusses how Cosmos DB differs from traditional (relational) databases
- Discusses key considerations when designing Logic Apps that handle sensitive data
- Explains how combining Event Grid with other services can create robust workflows
- Demonstrates deep understanding of serverless concepts
- Theory answers are accurate and not confused (e.g., correctly explains SQL vs NoSQL concepts)
