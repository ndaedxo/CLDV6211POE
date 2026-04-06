# Part 1 Success Metrics - 97% Pass Rate

## Part 1A: Database Design (ERD & Script) - 25 Marks

### To achieve 21-25 Marks (97% Pass):
- [ ] ERD is professional and complete with ALL of the following:
  - [ ] Three main entities: Venue, Event, Booking
  - [ ] All attributes properly defined (e.g., Venue: Name, Location, Capacity; Event: Name, StartDate, EndDate; Booking: BookingID, EventID, VenueID, BookingDate)
  - [ ] Primary keys correctly identified
  - [ ] Foreign keys properly defined
  - [ ] Relationships with correct cardinality (one-to-many between Venue/Event, Event/Booking)
  - [ ] Multiplicities clearly labelled
- [ ] Database script creates tables successfully with:
  - [ ] Correct data types for all columns
  - [ ] Primary key constraints
  - [ ] Foreign key constraints
  - [ ] Script runs without errors on SQL LocalDB/SQL Express

---

## Part 1B: MVC Application Structure - 25 Marks

### To achieve 21-25 Marks (97% Pass):
- [ ] Project builds successfully without errors
- [ ] ALL Models present with proper naming conventions:
  - [ ] Venue model with all required properties
  - [ ] Event model with all required properties
  - [ ] Booking model with all required properties
- [ ] ALL Controllers present:
  - [ ] VenueController with CRUD action methods
  - [ ] EventController with CRUD action methods
  - [ ] BookingController with CRUD action methods
- [ ] ALL Views present for each entity:
  - [ ] Index, Details, Create, Edit, Delete views
- [ ] Code follows standard ASP.NET Core MVC naming conventions
- [ ] Solution is well-structured and easy to navigate

---

## Part 1C: Functionality & Local Persistence - 25 Marks

### To achieve 21-25 Marks (97% Pass):
- [ ] Full CRUD functionality works seamlessly for Venues:
  - [ ] Create venue with all fields
  - [ ] Read/view all venues
  - [ ] Update venue information
  - [ ] Delete venue
- [ ] Full CRUD functionality works seamlessly for Events:
  - [ ] Create event with all fields
  - [ ] Read/view all events
  - [ ] Update event information
  - [ ] Delete event
- [ ] Data persists in SQL LocalDB after application restart
- [ ] Connection strings correctly configured in appsettings.json (NOT hardcoded)
- [ ] Placeholder image URLs used for venues and events
- [ ] Application runs on localhost without crashes

---

## Part 1D: Cloud Theory - 25 Marks

### To achieve 21-25 Marks (97% Pass):
- [ ] On-Premises vs Cloud comparison with specific examples:
  - [ ] Security differences explained (identity management, compliance, encryption)
  - [ ] Deployment speed differences explained (scalability, provisioning)
  - [ ] Resource management differences explained (cost, maintenance)
- [ ] Clear definitions with relevant examples:
  - [ ] IaaS explained with example (e.g., Virtual Machines)
  - [ ] PaaS explained with example (e.g., Azure App Service)
  - [ ] SaaS explained with example (e.g., Office 365)
- [ ] Why PaaS benefits EventEase:
  - [ ] No server management overhead
  - [ ] Auto-scaling capabilities
  - [ ] Pay-as-you-go model
  - [ ] Faster development/deployment

---

## GitHub Requirement
- [ ] Code must be pushed to GitHub repository
- [ ] Repository link must be included in submission
- [ ] **FAILURE: -5 Marks if not on GitHub**

---

# Part 2 Success Metrics - 97% Pass Rate

## Part 2A: Local Blob Storage (Azurite) - 25 Marks

### To achieve 21-25 Marks (97% Pass):
- [ ] Image upload functionality works perfectly:
  - [ ] Images upload to Azurite (NOT saved to disk locally)
  - [ ] Images are stored in container named "venue-images"
- [ ] appsettings.json configured correctly:
  - [ ] Uses `UseDevelopmentStorage=true`
- [ ] Evidence provided (screenshots):
  - [ ] Azure Storage Explorer showing local venue-images container
  - [ ] Blobs visible in the container
  - [ ] Application displaying uploaded images correctly
- [ ] Supports common image file types (JPG, PNG, GIF)
- [ ] Images display correctly in the application UI

---

## Part 2B: Error Handling & Validation - 25 Marks

### To achieve 21-25 Marks (97% Pass):
- [ ] Robust double booking prevention logic:
  - [ ] System prevents booking same venue on same date/time
  - [ ] Edge cases handled (overlapping dates, partial overlaps)
- [ ] Deletion restrictions enforced:
  - [ ] Cannot delete venue with active bookings (clear error message)
  - [ ] Cannot delete event with active bookings (clear error message)
- [ ] Graceful error handling:
  - [ ] Application handles invalid inputs without crashing
  - [ ] User receives clear alert when validation fails
  - [ ] Error messages are informative and user-friendly

---

## Part 2C: Enhanced Display & Search - 25 Marks

### To achieve 21-25 Marks (97% Pass):
- [ ] Consolidated Booking View:
  - [ ] Displays Event Name (not just Event ID)
  - [ ] Displays Venue Name (not just Venue ID)
  - [ ] Includes all relevant booking information
  - [ ] User-friendly layout
- [ ] Search functionality:
  - [ ] Search by Booking ID works accurately
  - [ ] Search by Event Name works accurately
  - [ ] Search is NOT case-sensitive
  - [ ] Results are accurate and complete

---

## Part 2D: Theory: Search & Normalisation - 25 Marks

### To achieve 21-25 Marks (97% Pass):
- [ ] Azure Cognitive Search explanation:
  - [ ] How it differs from traditional search engines
  - [ ] AI-powered capabilities
  - [ ] Use cases where it offers clear advantage (e.g., unstructured data, faceted search)
- [ ] Database normalisation explanation:
  - [ ] Why normalisation is important
  - [ ] Impact on performance and scalability
  - [ ] Normalised vs denormalised structures
  - [ ] Read vs Write heavy workloads in the cloud context

---

## GitHub Requirement
- [ ] Code must be pushed to GitHub repository
- [ ] Repository link must be included in submission
- [ ] **FAILURE: -5 Marks if not on GitHub**

---

# Part 3 Success Metrics - 97% Pass Rate

## Part 3A: Advanced Filtering - 20 Marks

### To achieve 16-20 Marks (97% Pass):
- [ ] EventType lookup table implemented:
  - [ ] Added as a database table (NOT hardcoded values)
  - [ ] Proper relationship to Event table
  - [ ] Predefined categories (e.g., Wedding, Conference, Concert, Workshop)
- [ ] All filters work perfectly:
  - [ ] Filter by Event Type works
  - [ ] Filter by Date Range works
  - [ ] Filter by Venue Availability works
  - [ ] Filters work together when combined
- [ ] UI is intuitive and user-friendly
- [ ] Filter logic is not hardcoded

---

## Part 3B: The Migration (Local to Cloud) - 20 Marks

### To achieve 16-20 Marks (97% Pass):
- [ ] Azure SQL Database:
  - [ ] Created in Azure portal
  - [ ] Schema migrated successfully
  - [ ] Data migrated (tables NOT empty)
  - [ ] Connection string updated to Azure SQL
- [ ] Azure Storage Account:
  - [ ] Created in Azure portal
  - [ ] Container recreated or images migrated
  - [ ] Connection string updated to live storage
- [ ] Configuration changes:
  - [ ] Uses production keys (NOT debug keys)
  - [ ] No local emulator references in production
  - [ ] Application reads from live resources

---

## Part 3C: Live Deployment and Dropping of Resources - 20 Marks

### To achieve 16-20 Marks (97% Pass):
- [ ] Azure App Service:
  - [ ] Created and web app published
  - [ ] Application accessible via Azure URL
  - [ ] Returns 200 (not 404)
  - [ ] Site loads without "Server Errors"
- [ ] All features work in live environment:
  - [ ] Search functionality works
  - [ ] Booking functionality works
  - [ ] Images load correctly
  - [ ] Database connectivity works
- [ ] Resources dropped:
  - [ ] All Azure resources deleted
  - [ ] Proof provided showing deletion

---

## Part 3D: Reflective Report - Technical - 20 Marks

### To achieve 16-20 Marks (97% Pass):
- [ ] Comprehensive feature list:
  - [ ] All features listed with descriptions
- [ ] Azure services justification:
  - [ ] Which services were used (App Service, SQL Database, Storage Account)
  - [ ] Why each service was chosen
- [ ] Migration experience discussion:
  - [ ] Detailed description of LocalDB/Azurite to Azure SQL/Storage migration
  - [ ] Configuration changes required explained
  - [ ] Challenges faced during migration
- [ ] Environment separation importance:
  - [ ] Why dev/prod separation matters in professional development
- [ ] Technology discussion:
  - [ ] Technologies used and why (ASP.NET Core MVC, Azure services)

---

## Part 3E: Theory: Advanced Cloud - 20 Marks

### To achieve 16-20 Marks (97% Pass):
- [ ] CosmosDB vs SQL explanation:
  - [ ] How CosmosDB differs from traditional relational databases
  - [ ] When to use NoSQL vs SQL
  - [ ] Global distribution capabilities
- [ ] Logic Apps security considerations:
  - [ ] Handling sensitive data in Logic Apps
  - [ ] Authentication and authorization
  - [ ] Encryption and compliance
- [ ] Event Grid workflows:
  - [ ] How Event Grid combines with other services
  - [ ] Robust workflow creation
  - [ ] Event-driven architecture benefits

---

## GitHub Requirement
- [ ] Code must be pushed to GitHub repository
- [ ] Repository link must be included in submission
- [ ] **FAILURE: -5 Marks if not on GitHub**

---

# Summary: Key Success Metrics

## Part 1 (100 Marks Total)
| Section | Max Marks | Target (97%) | Key Deliverables |
|---------|-----------|--------------|------------------|
| A. Database Design | 25 | 21-25 | Complete ERD + working SQL script |
| B. MVC Structure | 25 | 21-25 | Full MVC architecture |
| C. Functionality | 25 | 21-25 | Full CRUD + data persistence |
| D. Cloud Theory | 25 | 21-25 | Comprehensive theory answers |
| GitHub | -5 | 0 | Must be on GitHub |

## Part 2 (100 Marks Total)
| Section | Max Marks | Target (97%) | Key Deliverables |
|---------|-----------|--------------|------------------|
| A. Azurite Storage | 25 | 21-25 | Images in local blob storage |
| B. Validation | 25 | 21-25 | Double booking prevention |
| C. Search/Display | 25 | 21-25 | Consolidated view + search |
| D. Theory | 25 | 21-25 | Search + normalisation theory |
| GitHub | -5 | 0 | Must be on GitHub |

## Part 3 (100 Marks Total)
| Section | Max Marks | Target (97%) | Key Deliverables |
|---------|-----------|--------------|------------------|
| A. Filtering | 20 | 16-20 | EventType table + filters |
| B. Migration | 20 | 16-20 | Azure SQL + Storage |
| C. Deployment | 20 | 16-20 | Live URL + resource cleanup |
| D. Reflective | 20 | 16-20 | Technical report |
| E. Theory | 20 | 16-20 | Advanced cloud concepts |
| GitHub | -5 | 0 | Must be on GitHub |

---

# Critical Requirements for All Parts

1. **GitHub Submission** - Mandatory for all parts (-5 marks if missing)
2. **YouTube Video** - Required for any marks (must show full application running)
3. **Screenshots** - Required for each part showing working application
4. **Consistent Referencing** - Harvard style, in-text + bibliography
5. **No Plagiarism** - Maximum 10% direct quotes
