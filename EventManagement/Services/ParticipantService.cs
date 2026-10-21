using EventManagement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Services
{
    public class ParticipantService
    {
        private List<Participant> participants = new List<Participant>();
        public bool RegisterParticipant(Participant participant)
        {
            if (participant == null || string.IsNullOrEmpty(participant.participantID) || string.IsNullOrEmpty(participant.fullName) || string.IsNullOrEmpty(participant.email)|| string.IsNullOrEmpty(participant.phoneNumber))
            {
                return false;
            }
            if(participants.Exists(p => p.participantID == participant.participantID))
            {
                Console.WriteLine("Participant with the same ID already exists.");
                return false;
            }
            if(participants.Exists(p => p.email == participant.email))
            {
                Console.WriteLine("Participant with the same email already exists.");
                return false;
            }
            if(participants.Exists(p => p.phoneNumber == participant.phoneNumber))
            {
                Console.WriteLine("Participant with the same phone number already exists.");
                return false;
            }
            participants.Add(participant);
            Console.WriteLine("Participant registered successfully!!!");
            return true;
        }
        public void DisplayParticipants()
        {
            if (participants.Count == 0)
            {
                Console.WriteLine("No participants available.");
                return;
            }
            Console.WriteLine("Available Participants:");
            foreach (Participant p in participants)
            {
                Console.WriteLine($"ID: {p.participantID}, Name: {p.fullName}, Email: {p.email}, Phone: {p.phoneNumber}");
            }
        }
        public Participant? GetParticipantById(string participantId)
        {
            if (string.IsNullOrWhiteSpace(participantId))
            {
                return null;
            }

            return participants.FirstOrDefault(p =>
                p.participantID.Equals(
                    participantId.Trim(),
                    StringComparison.OrdinalIgnoreCase));
        }
        public void SearchParticipantByNameorEmail(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                Console.WriteLine("Search text cannot be empty.");
                return;
            }
            var foundParticipants = participants.Where(p => p.fullName.Contains(keyword, StringComparison.OrdinalIgnoreCase) || p.email.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();

            if (foundParticipants.Count == 0)
            {
                Console.WriteLine("No matching participants found.");
                return;
            }

            Console.WriteLine("\n========== SEARCH RESULTS ==========");

            foreach (Participant p in foundParticipants)
            {
                Console.WriteLine($"Name: {p.fullName}, Email: {p.email}, Phone: {p.phoneNumber}");
            }
        }
        public void UpdateParticipant(string fullName, string email, string phoneNumber, string participantId)
        {
            Participant? existingParticipant = GetParticipantById(participantId);
            if (existingParticipant == null)
            {
                Console.WriteLine("Participant not found.");
                return;
            }
            if (!string.IsNullOrWhiteSpace(fullName))
            {
                existingParticipant.fullName = fullName;
            }
            if (!string.IsNullOrWhiteSpace(email))
            {
                existingParticipant.email = email;
            }
            if (!string.IsNullOrWhiteSpace(phoneNumber))
            {
                existingParticipant.phoneNumber = phoneNumber;
            }
            
        }
        public List<Participant> GetAllParticipants()
        {
            return participants;
        }
        public void LoadParticipants(List<Participant> loadedParticipants)
        {
            participants.Clear();
            participants.AddRange(loadedParticipants);
        }

    }
}
