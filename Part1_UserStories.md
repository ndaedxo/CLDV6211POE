# User Stories - Part 1

## Part 1A: Database Design (ERD & Script)
**As a** database designer,  
**I want to** create an Entity-Relationship Diagram (ERD) with Venue, Event, and Booking tables and an accompanying database script,  
**So that** the EventEase system has a proper foundation for storing venues, events, and bookings data.

### Acceptance Criteria:
- ERD includes three main entities: Venue, Event, and Booking
- All entities have appropriate attributes (e.g., Venue: Location, Capacity, Name; Event: Name, StartDate, EndDate; Booking: BookingID, EventID, VenueID)
- Primary keys and foreign keys are correctly defined
- Relationships between tables are clearly labelled with cardinality
- Database script creates tables successfully with correct data types and constraints
- Script runs without errors on SQL LocalDB or SQL Express

---

## Part 1B: MVC Application Structure
**As a** ASP.NET Core developer,  
**I want to** build an ASP.NET Core MVC web application with Models, Controllers, and Views for the EventEase system,  
**So that** the application follows proper MVC architecture and can perform CRUD operations.

### Acceptance Criteria:
- Project builds successfully without errors
- Models are created for Venue, Event, and Booking with appropriate properties
- Controllers are created for Venue, Event, and Booking with CRUD action methods
- Views are created for Create, Read, Update, and Delete operations
- Code follows standard ASP.NET Core MVC naming conventions
- Application can be run on localhost (IIS Express or Kestrel)

---

## Part 1C: Functionality & Local Persistence
**As a** booking specialist,  
**I want to** perform full CRUD operations on venues and events with data persisting in SQL LocalDB,  
**So that** I can manage venue and event information that persists between application restarts.

### Acceptance Criteria:
- Can create new venues and events with all required fields
- Can read/view all venues and events from the database
- Can update existing venue and event information
- Can delete venues and events (with validation for Part 2)
- Data persists in SQL LocalDB after application restart
- Connection string is properly configured in appsettings.json (not hardcoded)
- Placeholder image URLs are used for venues and events

---

## Part 1D: Cloud Theory
**As a** cloud computing student,  
**I want to** explain the differences between on-premises and cloud deployment, and understand IaaS, PaaS, and SaaS models,  
**So that** I can recommend appropriate cloud solutions for EventEase.

### Acceptance Criteria:
- Explains how cloud deployment differs from on-premises in terms of security, deployment speed, and resource management with examples
- Clearly defines Infrastructure as a Service (IaaS), Platform as a Service (PaaS), and Software as a Service (SaaS)
- Provides relevant examples for each service model
- Explains why PaaS would benefit EventEase over IaaS and SaaS for their venue booking system
- Theoretical answers are well-structured and demonstrate understanding of cloud computing basics
