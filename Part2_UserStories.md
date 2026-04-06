# User Stories - Part 2

## Part 2A: Local Blob Storage (Azurite)
**As a** web developer,  
**I want to** implement image upload functionality using Azurite Emulator (local blob storage),  
**So that** venue and event images are stored in a container named "venue-images" without using live Azure cloud credits.

### Acceptance Criteria:
- Images uploaded for venues and events are stored in Azurite (not saved to disk)
- Connection string in appsettings.json uses `UseDevelopmentStorage=true`
- Images are stored in a container named "venue-images"
- Azure Storage Explorer shows the local container and uploaded blobs
- Application displays uploaded images correctly
- Supports common image file types (JPG, PNG, etc.)

---

## Part 2B: Error Handling & Validation
**As a** booking specialist,  
**I want to** have validation rules that prevent double bookings and restrict deletion of venues/events with active bookings,  
**So that** the system maintains data integrity and provides clear feedback to users.

### Acceptance Criteria:
- System prevents double bookings for the same venue on the same date/time
- Validation alerts are displayed to the user when a booking conflict is detected
- Cannot delete a venue that has associated active bookings
- Cannot delete an event that has associated active bookings
- Clear user alerts are shown when deletion is blocked due to existing bookings
- Application handles invalid inputs gracefully without crashing
- Error messages are clear and informative

---

## Part 2C: Enhanced Display & Search
**As a** booking specialist,  
**I want to** view all bookings in a consolidated view and search by booking ID or event name,  
**So that** I can easily find and manage bookings without navigating between multiple pages.

### Acceptance Criteria:
- Consolidated Booking View displays data from joined tables (Event Name, Venue Name, not just IDs)
- Booking view includes relevant information from both Venue and Event tables
- Search functionality allows filtering by Booking ID
- Search functionality allows filtering by Event Name
- Search works accurately without case-sensitivity issues
- UI is user-friendly and displays booking information clearly

---

## Part 2D: Theory: Search & Normalisation
**As a** cloud computing student,  
**I want to** explain Azure Cognitive Search differences from traditional search engines and discuss database normalisation,  
**So that** I can make informed decisions about search and database design for cloud applications.

### Acceptance Criteria:
- Clearly explains how Azure Cognitive Search differs from traditional search engines
- Provides potential use cases where Cognitive Search would offer a clear advantage
- Explains the importance of database normalisation
- Discusses the impact of normalised structures on performance and scalability
- Discusses the impact of denormalised structures on read vs write heavy workloads
- Theory answers are relevant to cloud-based applications
