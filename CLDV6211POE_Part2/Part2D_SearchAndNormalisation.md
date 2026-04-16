# Part 2D: Search & Database Normalisation Theory

## Azure Cognitive Search vs Traditional Search Engines

### Traditional Search Engines

Traditional search engines like Elasticsearch, Solr, or SQL Server Full-Text Search work by indexing data and matching keywords against indexed content. They provide:

- **Keyword matching:** Simple text matching against indexed fields
- **Boolean operators:** AND, OR, NOT logic for query refinement
- **Stemming/lemmatization:** Basic linguistic normalisation
- **Ranked results:** Scoring based on term frequency and inverse document frequency (TF-IDF)

**Limitations of Traditional Search:**
- Limited understanding of context or meaning
- No support for complex queries across multiple data sources
- Basic faceted navigation without AI-powered insights
- Requires manual configuration of synonyms and language rules

---

### Azure Cognitive Search (Azure AI Search)

Azure Cognitive Search is an AI-powered cloud search service that extends beyond traditional keyword matching:

- **AI-powered enrichment:** Can extract entities, sentiment, key phrases, and images from documents during indexing
- **Semantic search:** Uses machine learning models to understand query intent and context
- **Vector search:** Supports embedding-based similarity search for AI applications
- **Multi-language support:** Built-in support for 56+ languages with linguistic analysis
- **Faceted navigation:** Automatic categorisation of search results
- **Integrated with Azure AI:** Seamlessly works with Azure AI Services for image recognition, text analytics, and more

### Key Differences

| Feature | Traditional Search | Azure Cognitive Search |
|---------|-------------------|------------------------|
| **Query Understanding** | Keyword matching only | Semantic/AI-powered intent |
| **Data Sources** | Single index | Multiple data sources |
| **Scalability** | Manual scaling | Auto-scaling in cloud |
| **AI Capabilities** | None | Built-in AI enrichment |
| **Deployment** | Self-managed | Fully managed PaaS |
| **Cost** | Infrastructure + maintenance | Pay-per-query |

---

## When to Use Azure Cognitive Search

### Use Cases with Clear Advantage

1. **E-commerce Product Search**
   - Semantic understanding of "running shoes" matches "jogging footwear"
   - Automatic sentiment analysis on product reviews
   - Faceted search by price, brand, rating without manual tagging

2. **Knowledge Base / Document Search**
   - PDFs, Word documents, images with OCR capability
   - Question-answer extraction from documents
   - Multi-language document repositories

3. **Medical/Healthcare Records**
   - Privacy-compliant search across sensitive data
   - Entity recognition for diagnoses, medications, procedures
   - Integration with FHIR-compliant data sources

4. **Customer Support Knowledge Base**
   - Intent detection for support queries
   - Automatic routing to relevant articles
   - Sentiment analysis on customer feedback

---

## Database Normalisation

### What is Database Normalisation?

Database normalisation is the process of organising data in a database to reduce redundancy and improve data integrity. It involves dividing large tables into smaller, related tables and defining relationships between them.

### Normal Forms (1NF, 2NF, 3NF)

**First Normal Form (1NF):**
- Each column contains atomic (indivisible) values
- Each column contains values of a single type
- Each row is unique (primary key defined)

**Second Normal Form (2NF):**
- Satisfies 1NF
- All non-key columns depend on the entire primary key (no partial dependencies)
- Typically applies to tables with composite primary keys

**Third Normal Form (3NF):**
- Satisfies 2NF
- No transitive dependencies (non-key columns depend only on the primary key)
- Example: In a table with (StudentID, CourseID, CourseName, Instructor), CourseName and Instructor depend on CourseID, not directly on StudentID — this creates a transitive dependency

---

## Impact on Performance and Scalability

### Normalised Structures (3NF+)

**Advantages:**
- **Reduced data redundancy:** Single source of truth, no duplicate data
- **Easier data maintenance:** Updates only needed in one place
- **Better data integrity:** Constraints ensure consistency
- **Smaller storage per record:** No duplicated columns

**Disadvantages:**
- **More JOINs required:** Complex queries need multiple table joins
- **Potential performance impact:** JOINs on large tables can be slow
- **More complex queries:** Simple data retrieval may require multiple operations

### Denormalised Structures

**Advantages:**
- **Faster reads:** Data pre-joined, fewer tables to query
- **Simpler queries:** Single-table SELECTs for common operations
- **Better for reporting:** Aggregated data readily available

**Disadvantages:**
- **Data redundancy:** Same data stored multiple times
- **Update anomalies:** Changes must be made in multiple places
- **Storage costs:** Duplicate data increases storage requirements
- **Integrity risks:** More places for data to become inconsistent

---

## Choosing the Right Approach

### Use Normalisation When:
- Write-heavy workloads (frequent INSERT/UPDATE/DELETE)
- Data integrity is critical
- Storage efficiency is important
- Complex relationships between entities

### Use Denormalisation When:
- Read-heavy workloads (analytics, reporting)
- Query performance is paramount
- Real-time aggregations needed
- Working with data warehousing (star schemas)

### Hybrid Approaches:
- **Read replicas:** Normalised primary database with denormalised read replicas
- **Materialised views:** Pre-computed denormalised results for common queries
- **Caching layers:** Redis or similar for frequently accessed denormalised data

---

## Relevance to EventEase

For the EventEase venue booking system:

- **Current normalised design** (3NF) works well because:
  - Write operations (create bookings, manage venues) are frequent
  - Data integrity is critical (no double bookings)
  - Each entity (Venue, Event, Booking) has clear relationships

- **Potential denormalisation scenarios:**
  - Reporting dashboard showing bookings with full venue/event details could use a materialised view
  - Search functionality (Part 2C) benefits from indexed queries but doesn't require full denormalisation

The decision to normalise was correct for a transactional system like EventEase. As the system scales, caching or read-replicas can be added for performance optimisation without sacrificing the integrity benefits of normalisation.
