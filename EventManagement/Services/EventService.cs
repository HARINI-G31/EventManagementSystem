using EventManagement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Services
{
    public class EventService
    {
        private readonly List<Event> events = new List<Event>();
        public bool CreateEvent(Event newEvent)
        {
            if (newEvent == null)
            {
                Console.WriteLine("Event cannot be null.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(newEvent.eventID))
            {
                Console.WriteLine("Event ID cannot be empty.");
                return false;
            }
            if (events.Any(e =>
                e.eventID.Equals(
                    newEvent.eventID.Trim(),
                    StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("An event with this ID already exists.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(newEvent.eventName))
            {
                Console.WriteLine("Event name cannot be empty.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(newEvent.venue))
            {
                Console.WriteLine("Venue cannot be empty.");
                return false;
            }
            if (newEvent.capacity <= 0)
            {
                Console.WriteLine("Capacity must be greater than zero.");
                return false;
            }

            if (newEvent.registrationFee < 0)
            {
                Console.WriteLine("Registration fee cannot be negative.");
                return false;
            }
            if (newEvent.eventDate < DateTime.Now)
            {
                Console.WriteLine("Event date cannot be in the past.");
                return false;
            }
            events.Add(newEvent);
            Console.WriteLine("Event created successfully!!!");
            return true;
        }
        public void DisplayEvents()
        {
            if (events.Count == 0)
            {
                Console.WriteLine("No events available.");
                return;
            }
            Console.WriteLine("Available Events:");
            foreach (Event e in events)
            {
                Console.WriteLine($"ID: {e.eventID}, Name: {e.eventName}, Date: {e.eventDate}, Venue: {e.venue}, Capacity: {e.capacity}, Fee: {e.registrationFee}");
            }
        }
        public Event? GetEventById(string eventId)
        {
            if (string.IsNullOrWhiteSpace(eventId))
            {
                return null;
            }

            return events.FirstOrDefault(e =>
                e.eventID.Equals(
                    eventId.Trim(),
                    StringComparison.OrdinalIgnoreCase));
        }
        public void SearchEventByNameorVenue(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                Console.WriteLine("Search text cannot be empty.");
                return;
            }
            var foundEvents = events.Where(e => e.eventName.Contains(keyword, StringComparison.OrdinalIgnoreCase) || e.venue.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();

            if (foundEvents.Count == 0)
            {
                Console.WriteLine("No matching events found.");
                return;
            }

            Console.WriteLine("\n========== SEARCH RESULTS ==========");

            foreach (Event e in foundEvents)
            {
                Console.WriteLine($"Name: {e.eventName}, Venue: {e.venue}, Date:{e.eventDate}, Capacity:{e.capacity}, Fee: {e.registrationFee}");
            }
        }
        public void UpdateEvent(string venue, DateTime eventDate, int capacity, decimal registrationFee, string eventId)
        {
            Event? existingEvent = GetEventById(eventId);
            if (existingEvent == null)
            {
                Console.WriteLine("Event not found.");
                return;
            }
            if (!string.IsNullOrWhiteSpace(venue))
            {
                existingEvent.venue = venue;
            }
            if (eventDate > DateTime.Now)
            {
                existingEvent.eventDate = eventDate;
            }
            else
            {
                Console.WriteLine("Event date cannot be in the past.");
                return;
            }
            if (capacity > 0)
            {
                existingEvent.capacity = capacity;
            }
            else
            {
                Console.WriteLine("Capacity must be greater than zero.");
                return;
            }
            if (registrationFee >= 0)
            {
                existingEvent.registrationFee = registrationFee;
            }
            else
            {
                Console.WriteLine("Registration fee cannot be negative.");
                return;
            }
            Console.WriteLine("Event updated successfully.");
        }
        public void DeleteEvent(string eventId)
        {
            Event? existingEvent = GetEventById(eventId);
            if (existingEvent == null)
            {
                Console.WriteLine("Event not found.");
                return;
            }
            events.Remove(existingEvent);
            Console.WriteLine("Event deleted successfully.");
        }
    }
        
}

