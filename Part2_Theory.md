# Part 2D: Theory - Search & Normalisation

## Azure Cognitive Search vs Traditional Search Engines

### How Azure Cognitive Search Differs

Azure Cognitive Search is a cloud-based search-as-a-service solution that differs from traditional search engines (like Google or Bing) in several key ways:

| Aspect | Azure Cognitive Search | Traditional Search Engines |
|--------|------------------------|----------------------------|
| **Purpose** | Private, application-specific search over proprietary content | Public web search indexing internet content |
| **Data Source** | Your databases, blobs, documents (structured/unstructured) | Publicly accessible web pages |
| **Control** | Full control over indexing, scoring, and results | Algorithm controlled by search engine provider |
| **Integration** | REST API/SDK integration into custom applications | Accessed via web interface or limited APIs |
| **AI Capabilities** | Built-in AI enrichment (OCR, entity recognition, key phrases) | General-purpose web crawling and indexing |

### AI-Powered Capabilities

Azure Cognitive Search includes **cognitive skills** that enhance search results:

- **Entity Recognition**: Extracts people, places, organizations from text
- **Key Phrase Extraction**: Identifies main topics in documents
- **Language Detection**: Automatically detects document language
- **OCR**: Extracts text from images (relevant for EventEase venue images)
- **Sentiment Analysis**: Determines positive/negative sentiment

### Use Cases Where Cognitive Search Offers Clear Advantage

1. **Unstructured Data Search**: Searching across PDFs, Word docs, and images in a venue management system
   - Example: Finding all venues mentioned in uploaded contracts or brochures

2. **Faceted Search**: Providing drill-down filters (by capacity, location, event type)
   - Example: "Show venues in Johannesburg with capacity > 200"

3. **Multi-source Search**: Indexing data from Azure SQL, Blob Storage, and CosmosDB simultaneously
   - Example: Searching across venue details (SQL), images (Blobs), and bookings (SQL)

4. **Localized Search**: Searching content in multiple languages with automatic translation
   - Example: EventEase expanding to support venues in French, Afrikaans, and English

5. **Semantic Search**: Understanding user intent rather than just keyword matching
   - Example: Searching "places to host 200 people" returns venues with capacity 180-220

---

## Database Normalisation

### What is Database Normalisation?

Database normalisation is the process of organizing data to reduce redundancy and improve data integrity. It involves dividing large tables into smaller, related tables and defining relationships between them.

### Why Normalisation is Important

1. **Reduces Data Redundancy**: Same data isn't repeated across multiple rows
   - Example: Venue name "Sandton Convention Centre" stored once in Venue table, not repeated in every booking

2. **Maintains Data Integrity**: Updates only need to happen in one place
   - Example: Changing a venue's capacity updates automatically for all bookings

3. **Prevents Anomalies**:
   - **Insertion Anomaly**: Cannot add venue without a booking (if denormalized)
   - **Update Anomaly**: Changing venue name requires updating multiple rows
   - **Deletion Anomaly**: Deleting a booking might delete venue information

4. **Improves Consistency**: Foreign key constraints ensure valid relationships
   - Example: Cannot create a booking for a non-existent venue

### EventEase Normalised Structure (3NF)

```
Venue (VenueId PK, Name, Location, Capacity, ImageUrl)
Event (EventId PK, Name, StartDate, EndDate, ImageUrl)
Booking (BookingId PK, EventId FK, VenueId FK, BookingDate)
```

This structure ensures:
- Each venue/event stored once (no repetition in bookings)
- Bookings reference valid venues and events via foreign keys
- Changes to venue/event details propagate to all related bookings

### Impact on Performance and Scalability

#### Normalised Structures (Read Heavy Considerations)

**Advantages for Read-Heavy Workloads:**
- Smaller tables fit in memory cache more easily
- Indexes are more efficient on narrower tables
- Data consistency reduces need for application-level validation

**Disadvantages for Read-Heavy Workloads:**
- **JOIN overhead**: Queries require joining Booking → Venue → Event
- **Multiple disk reads**: Data spread across multiple tables
- **Complex queries**: Need to join 3+ tables for consolidated views

**EventEase Example:**
```sql
-- Normalised query requires JOINs
SELECT b.BookingId, v.Name as VenueName, e.Name as EventName
FROM Booking b
JOIN Venue v ON b.VenueId = v.VenueId
JOIN Event e ON b.EventId = e.EventId
```

#### Denormalised Structures (Read Heavy Optimization)

**Advantages for Read-Heavy Workloads:**
- **Single table reads**: No JOINs required
- **Faster queries**: All data in one place
- **Simpler queries**: No complex JOIN logic

**Disadvantages for Read-Heavy Workloads:**
- **Storage overhead**: Repeated data increases database size
- **Inconsistency risk**: Updates must touch multiple rows
- **Write overhead**: Inserting booking requires copying venue/event details

**EventEase Example (Denormalised):**
```sql
-- Single table, no JOINs needed
SELECT BookingId, VenueName, EventName
FROM BookingDenormalized
```

### Normalised vs Denormalised: Read vs Write Heavy Workloads

| Workload Type | Recommended Structure | Reasoning |
|---------------|----------------------|-----------|
| **Read-Heavy** (EventEase: viewing bookings) | Slightly Denormalised | Fewer JOINs, faster SELECT queries for consolidated views |
| **Write-Heavy** (EventEase: creating bookings) | Normalised | INSERT/UPDATE faster, no duplication, maintains integrity |
| **Balanced** | Normalised with Indexing | Best of both worlds with proper indexing strategy |

### Cloud Context: Scaling Considerations

In cloud environments (Azure SQL Database):

1. **Normalised + Read Replicas**: 
   - Primary handles writes (normalised for integrity)
   - Read replicas handle queries (with caching for JOIN-heavy views)

2. **Denormalised + CosmosDB**:
   - Document database naturally stores denormalised data
   - EventEase could store Booking documents with embedded Venue/Event details
   - Trades consistency for read performance

3. **Hybrid Approach (Recommended for EventEase)**:
   - Keep normalised structure for data integrity
   - Create **Indexed Views** or **Materialized Views** for frequent read operations
   - Use Azure Cache for Redis to cache consolidated booking views

### Conclusion

For EventEase, the **normalised 3NF structure** is appropriate because:
- Data integrity is critical (cannot double-book venues)
- Write operations (creating bookings) happen frequently
- Read operations can be optimized with proper indexing and includes (EF Core `.Include()`)
- Cloud scaling (Azure SQL) handles normalised queries efficiently with proper indexing

If the application grows to millions of bookings with heavy read traffic, consider adding a read-optimized denormalised view or caching layer.
