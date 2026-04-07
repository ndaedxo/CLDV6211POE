# EventEase Database Design - Entity-Relationship Diagram (ERD)

## Entities

### Venue
| Attribute | Data Type | Constraints |
|-----------|-----------|-------------|
| VenueId | INT | PRIMARY KEY, IDENTITY(1,1) |
| Name | NVARCHAR(100) | NOT NULL |
| Location | NVARCHAR(200) | NOT NULL |
| Capacity | INT | NOT NULL |
| ImageUrl | NVARCHAR(500) | NULL |

### Event
| Attribute | Data Type | Constraints |
|-----------|-----------|-------------|
| EventId | INT | PRIMARY KEY, IDENTITY(1,1) |
| Name | NVARCHAR(100) | NOT NULL |
| StartDate | DATETIME2 | NOT NULL |
| EndDate | DATETIME2 | NOT NULL |
| ImageUrl | NVARCHAR(500) | NULL |

### Booking
| Attribute | Data Type | Constraints |
|-----------|-----------|-------------|
| BookingId | INT | PRIMARY KEY, IDENTITY(1,1) |
| EventId | INT | FOREIGN KEY → Event(EventId), NOT NULL |
| VenueId | INT | FOREIGN KEY → Venue(VenueId), NOT NULL |
| BookingDate | DATETIME2 | NOT NULL |

## Relationships

```
Venue (1) ────────< (N) Booking >─────── (1) Event
```

- **Venue to Booking**: One-to-Many (1:N)
  - One Venue can have many Bookings
  - Each Booking belongs to exactly one Venue
  - Foreign Key: Booking.VenueId → Venue.VenueId

- **Event to Booking**: One-to-Many (1:N)
  - One Event can have many Bookings
  - Each Booking is associated with exactly one Event
  - Foreign Key: Booking.EventId → Event.EventId

## Cardinality Summary

| Relationship | From | To | Cardinality |
|-------------|------|----|-------------|
| Venue-Booking | Venue | Booking | 1 to N |
| Event-Booking | Event | Booking | 1 to N |

## Notes

- The Booking table acts as a junction table linking Venues and Events
- A Venue can be booked for multiple Events over time
- An Event can be held at multiple Venues over time (through separate bookings)
- ImageUrl fields use placeholder URLs in Part 1, will be replaced with blob storage URLs in Part 2
