using EventManagement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Services
{
    public  class RegistrationService
    {
        private List<Registration> registrations = new List<Registration>();
        private EventService eventService;
        private ParticipantService participantService;
        public RegistrationService(
        EventService eventService,
        ParticipantService participantService)
        {
            this.eventService = eventService;
            this.participantService = participantService;

        }
        public void RegisterParticipantForEvent(string registrationId, string eventId, string participantId)
        {
            var eventObj = eventService.GetEventById(eventId);
            var participantObj = participantService.GetParticipantById(participantId);
            if (eventObj == null)
            {
                Console.WriteLine("Event not found.");
                return;
            }
            if (participantObj == null)
            {
                Console.WriteLine("Participant not found.");
                return;
            }
            if (registrations.Exists(r => r.EventID == eventId && r.ParticipantID == participantId))
            {
                Console.WriteLine("Participant is already registered for this event.");
                return;
            }
            if (registrations.Count(r => r.EventID == eventId) >= eventObj.capacity)
            {
                Console.WriteLine("Event is full. Cannot register more participants.");
                return;
            }
            Registration registration = new Registration(
                registrationId,
                eventObj.eventID,
                participantObj.participantID,
                DateTime.Now,
                eventObj.registrationFee
);

            registrations.Add(registration);

            Console.WriteLine("Participant registered successfully.");
        }


    }
}
