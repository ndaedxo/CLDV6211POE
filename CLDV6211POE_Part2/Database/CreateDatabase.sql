-- EventEase Database Creation Script
-- Target: SQL LocalDB / SQL Express
-- This script creates the EventEase database with Venue, Event, and Booking tables.

-- Create the database
CREATE DATABASE EventEaseDb;
GO

USE EventEaseDb;
GO

-- Create Venue table
CREATE TABLE Venues (
    VenueId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Location NVARCHAR(200) NOT NULL,
    Capacity INT NOT NULL,
    ImageUrl NVARCHAR(500) NULL
);
GO

-- Create Event table
CREATE TABLE Events (
    EventId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    StartDate DATETIME2 NOT NULL,
    EndDate DATETIME2 NOT NULL,
    ImageUrl NVARCHAR(500) NULL
);
GO

-- Create Booking table
CREATE TABLE Bookings (
    BookingId INT IDENTITY(1,1) PRIMARY KEY,
    EventId INT NOT NULL,
    VenueId INT NOT NULL,
    BookingDate DATETIME2 NOT NULL,
    CONSTRAINT FK_Bookings_Events FOREIGN KEY (EventId) REFERENCES Events(EventId),
    CONSTRAINT FK_Bookings_Venues FOREIGN KEY (VenueId) REFERENCES Venues(VenueId)
);
GO

-- Verify tables were created
SELECT TABLE_NAME, TABLE_TYPE 
FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_TYPE = 'BASE TABLE'
ORDER BY TABLE_NAME;
GO
