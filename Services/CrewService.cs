using System.Text.Json;
using FoodTrailerJsonApp.Models;

namespace FoodTrailerJsonApp.Services;

public class CrewService
{
    private readonly string _filePath;

    public CrewService(string filePath)
    {
        _filePath = filePath;
    }

    // Get all crew members
    public List<CrewMember> GetAllCrewMembers()
    {
        if (!File.Exists(_filePath))
        {
            return new List<CrewMember>();
        }

        string json = File.ReadAllText(_filePath);

        if (string.IsNullOrWhiteSpace(json))
        {
            return new List<CrewMember>();
        }

        return JsonSerializer.Deserialize<List<CrewMember>>(json)
               ?? new List<CrewMember>();
    }

    // Save all crew members
    public void SaveCrewMembers(List<CrewMember> crewMembers)
    {
        string json = JsonSerializer.Serialize(
            crewMembers,
            new JsonSerializerOptions
            {
                WriteIndented = true
            });

        File.WriteAllText(_filePath, json);
    }

    // Find crew member by name
    public CrewMember? FindByName(string name)
    {
        return GetAllCrewMembers()
            .FirstOrDefault(member =>
                member.Name.Equals(
                    name,
                    StringComparison.OrdinalIgnoreCase));
    }

    // Find crew member by ID
    public CrewMember? FindById(int id)
    {
        return GetAllCrewMembers()
            .FirstOrDefault(member => member.Id == id);
    }

    // Get crew members by shift
    public List<CrewMember> GetByShift(string shift)
    {
        return GetAllCrewMembers()
            .Where(member =>
                member.Shift.Equals(
                    shift,
                    StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    // Get certified crew members
    public List<CrewMember> GetCertifiedCrew()
    {
        return GetAllCrewMembers()
            .Where(member => member.Certified)
            .ToList();
    }

    // Add a new crew member
    public void AddCrewMember(CrewMember newMember)
    {
        List<CrewMember> crewMembers = GetAllCrewMembers();

        newMember.Id = crewMembers.Count == 0
            ? 1
            : crewMembers.Max(member => member.Id) + 1;

        crewMembers.Add(newMember);

        SaveCrewMembers(crewMembers);
    }

    // Update an existing crew member
    public bool UpdateCrewMember(CrewMember updatedMember)
    {
        List<CrewMember> crewMembers = GetAllCrewMembers();

        CrewMember? existingMember =
            crewMembers.FirstOrDefault(
                member => member.Id == updatedMember.Id);

        if (existingMember == null)
        {
            return false;
        }

        existingMember.Name = updatedMember.Name;
        existingMember.Role = updatedMember.Role;
        existingMember.Shift = updatedMember.Shift;
        existingMember.Certified = updatedMember.Certified;

        SaveCrewMembers(crewMembers);

        return true;
    }

    // Remove a crew member
    public bool RemoveCrewMember(int id)
    {
        List<CrewMember> crewMembers = GetAllCrewMembers();

        CrewMember? memberToRemove =
            crewMembers.FirstOrDefault(
                member => member.Id == id);

        if (memberToRemove == null)
        {
            return false;
        }

        crewMembers.Remove(memberToRemove);

        SaveCrewMembers(crewMembers);

        return true;
    }
}