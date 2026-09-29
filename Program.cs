using FoodTrailerJsonApp.Models;
using FoodTrailerJsonApp.Services;

// --------------------------------------------------
// Application setup
// --------------------------------------------------

string filePath = Path.Combine("Data", "crew.json");

CrewService crewService = new CrewService(filePath);

bool running = true;


// --------------------------------------------------
// Main application loop
// --------------------------------------------------

while (running)
{
    Console.Clear();

    DisplayMainMenu();

    Console.Write("Select an option: ");
    string? choice = Console.ReadLine();

    Console.WriteLine();

    switch (choice)
    {
        case "1":
            ViewAllCrewMembers();
            break;

        case "2":
            ViewCrewMemberByName();
            break;

        case "3":
            AddCrewMember();
            break;

        case "4":
            UpdateCrewMember();
            break;

        case "5":
            RemoveCrewMember();
            break;

        case "6":
            ViewCrewByShift();
            break;

        case "7":
            ViewCertifiedCrew();
            break;

        case "8":
            running = false;

            Console.WriteLine(
                "Exiting Theo's Food Trailer Crew...");

            Console.WriteLine("Goodbye!");
            break;

        default:
            Console.WriteLine(
                "Invalid option. Please select 1 through 8.");

            Pause();
            break;
    }
}


// --------------------------------------------------
// Display main menu
// --------------------------------------------------

void DisplayMainMenu()
{
    Console.WriteLine("================================");
    Console.WriteLine("   THEO'S FOOD TRAILER CREW");
    Console.WriteLine("================================");
    Console.WriteLine();

    Console.WriteLine("1. View all crew members");
    Console.WriteLine("2. View crew member by name");
    Console.WriteLine("3. Add crew member");
    Console.WriteLine("4. Update crew member");
    Console.WriteLine("5. Remove crew member");
    Console.WriteLine("6. View crew by shift");
    Console.WriteLine("7. View certified crew");
    Console.WriteLine("8. Exit");

    Console.WriteLine();
}


// --------------------------------------------------
// Option 1 - View all crew members
// --------------------------------------------------

void ViewAllCrewMembers()
{
    List<CrewMember> crewMembers =
        crewService.GetAllCrewMembers();

    Console.WriteLine("=== ALL CREW MEMBERS ===");
    Console.WriteLine();

    if (crewMembers.Count == 0)
    {
        Console.WriteLine("No crew members were found.");
        Pause();
        return;
    }

    foreach (CrewMember member in crewMembers)
    {
        DisplayCrewMember(member);
    }

    Pause();
}


// --------------------------------------------------
// Option 2 - Find crew member by name
// --------------------------------------------------

void ViewCrewMemberByName()
{
    Console.WriteLine("=== FIND CREW MEMBER ===");
    Console.WriteLine();

    Console.Write("Enter crew member name: ");
    string? name = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(name))
    {
        Console.WriteLine();
        Console.WriteLine("A name is required.");

        Pause();
        return;
    }

    CrewMember? member =
        crewService.FindByName(name.Trim());

    Console.WriteLine();

    if (member == null)
    {
        Console.WriteLine(
            $"Crew member '{name}' was not found.");

        Pause();
        return;
    }

    DisplayCrewMember(member);

    Pause();
}


// --------------------------------------------------
// Option 3 - Add crew member
// --------------------------------------------------

void AddCrewMember()
{
    Console.WriteLine("=== ADD CREW MEMBER ===");
    Console.WriteLine();

    Console.Write("Enter name: ");
    string? name = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(name))
    {
        Console.WriteLine("Name is required.");
        Pause();
        return;
    }

    Console.Write("Enter role: ");
    string? role = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(role))
    {
        Console.WriteLine("Role is required.");
        Pause();
        return;
    }

    Console.Write("Enter shift: ");
    string? shift = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(shift))
    {
        Console.WriteLine("Shift is required.");
        Pause();
        return;
    }

    Console.Write(
        "Is this crew member certified? (y/n): ");

    string? certifiedInput = Console.ReadLine();

    bool certified;

    if (certifiedInput?.Equals(
            "y",
            StringComparison.OrdinalIgnoreCase) == true)
    {
        certified = true;
    }
    else if (certifiedInput?.Equals(
                 "n",
                 StringComparison.OrdinalIgnoreCase) == true)
    {
        certified = false;
    }
    else
    {
        Console.WriteLine();
        Console.WriteLine("Please enter y or n.");

        Pause();
        return;
    }

    CrewMember newMember = new CrewMember
    {
        Name = name.Trim(),
        Role = role.Trim(),
        Shift = shift.Trim(),
        Certified = certified
    };

    crewService.AddCrewMember(newMember);

    Console.WriteLine();
    Console.WriteLine(
        "Crew member added successfully!");

    Console.WriteLine();

    DisplayCrewMember(newMember);

    Pause();
}


// --------------------------------------------------
// Option 4 - Update crew member
// --------------------------------------------------

void UpdateCrewMember()
{
    Console.WriteLine("=== UPDATE CREW MEMBER ===");
    Console.WriteLine();

    Console.Write("Enter crew member ID to update: ");

    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine();
        Console.WriteLine("Please enter a valid ID.");

        Pause();
        return;
    }

    CrewMember? member = crewService.FindById(id);

    if (member == null)
    {
        Console.WriteLine();
        Console.WriteLine(
            $"No crew member with ID {id} was found.");

        Pause();
        return;
    }

    Console.WriteLine();
    Console.WriteLine("Current information:");
    Console.WriteLine();

    DisplayCrewMember(member);

    Console.WriteLine();
    Console.WriteLine(
        "Press Enter to keep the current value.");
    Console.WriteLine();

    Console.Write($"Name ({member.Name}): ");
    string? name = Console.ReadLine();

    Console.Write($"Role ({member.Role}): ");
    string? role = Console.ReadLine();

    Console.Write($"Shift ({member.Shift}): ");
    string? shift = Console.ReadLine();

    Console.Write(
        $"Certified ({(member.Certified ? "Yes" : "No")}) " +
        "(y/n or Enter to keep): ");

    string? certifiedInput = Console.ReadLine();

    string updatedName =
        string.IsNullOrWhiteSpace(name)
            ? member.Name
            : name.Trim();

    string updatedRole =
        string.IsNullOrWhiteSpace(role)
            ? member.Role
            : role.Trim();

    string updatedShift =
        string.IsNullOrWhiteSpace(shift)
            ? member.Shift
            : shift.Trim();

    bool updatedCertified = member.Certified;

    if (certifiedInput?.Equals(
            "y",
            StringComparison.OrdinalIgnoreCase) == true)
    {
        updatedCertified = true;
    }
    else if (certifiedInput?.Equals(
                 "n",
                 StringComparison.OrdinalIgnoreCase) == true)
    {
        updatedCertified = false;
    }

    CrewMember updatedMember = new CrewMember
    {
        Id = member.Id,
        Name = updatedName,
        Role = updatedRole,
        Shift = updatedShift,
        Certified = updatedCertified
    };

    bool updated =
        crewService.UpdateCrewMember(updatedMember);

    Console.WriteLine();

    if (updated)
    {
        Console.WriteLine(
            "Crew member updated successfully!");

        Console.WriteLine();

        DisplayCrewMember(updatedMember);
    }
    else
    {
        Console.WriteLine(
            "The crew member could not be updated.");
    }

    Pause();
}


// --------------------------------------------------
// Option 5 - Remove crew member
// --------------------------------------------------

void RemoveCrewMember()
{
    Console.WriteLine("=== REMOVE CREW MEMBER ===");
    Console.WriteLine();

    Console.Write("Enter crew member ID to remove: ");

    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine();
        Console.WriteLine("Please enter a valid ID.");

        Pause();
        return;
    }

    CrewMember? member = crewService.FindById(id);

    if (member == null)
    {
        Console.WriteLine();
        Console.WriteLine(
            $"No crew member with ID {id} was found.");

        Pause();
        return;
    }

    Console.WriteLine();
    Console.WriteLine("Crew member selected:");
    Console.WriteLine();

    DisplayCrewMember(member);

    Console.WriteLine();

    Console.Write(
        $"Are you sure you want to remove {member.Name}? (y/n): ");

    string? confirmation = Console.ReadLine();

    if (!confirmation?.Equals(
            "y",
            StringComparison.OrdinalIgnoreCase) == true)
    {
        Console.WriteLine();
        Console.WriteLine("Removal cancelled.");

        Pause();
        return;
    }

    bool removed =
        crewService.RemoveCrewMember(id);

    Console.WriteLine();

    if (removed)
    {
        Console.WriteLine(
            $"{member.Name} was removed successfully!");
    }
    else
    {
        Console.WriteLine(
            "The crew member could not be removed.");
    }

    Pause();
}


// --------------------------------------------------
// Option 6 - View crew by shift
// --------------------------------------------------

void ViewCrewByShift()
{
    Console.WriteLine("=== VIEW CREW BY SHIFT ===");
    Console.WriteLine();

    Console.Write("Enter shift: ");
    string? shift = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(shift))
    {
        Console.WriteLine();
        Console.WriteLine("A shift is required.");

        Pause();
        return;
    }

    List<CrewMember> crewMembers =
        crewService.GetByShift(shift.Trim());

    Console.WriteLine();

    Console.WriteLine(
        $"=== {shift.Trim().ToUpper()} SHIFT ===");

    Console.WriteLine();

    if (crewMembers.Count == 0)
    {
        Console.WriteLine(
            $"No crew members were found for the {shift} shift.");

        Pause();
        return;
    }

    foreach (CrewMember member in crewMembers)
    {
        DisplayCrewMember(member);
    }

    Pause();
}


// --------------------------------------------------
// Option 7 - View certified crew
// --------------------------------------------------

void ViewCertifiedCrew()
{
    List<CrewMember> crewMembers =
        crewService.GetCertifiedCrew();

    Console.WriteLine("=== CERTIFIED CREW ===");
    Console.WriteLine();

    if (crewMembers.Count == 0)
    {
        Console.WriteLine(
            "No certified crew members were found.");

        Pause();
        return;
    }

    foreach (CrewMember member in crewMembers)
    {
        DisplayCrewMember(member);
    }

    Pause();
}


// --------------------------------------------------
// Display one crew member
// --------------------------------------------------

void DisplayCrewMember(CrewMember member)
{
    Console.WriteLine($"ID:        {member.Id}");
    Console.WriteLine($"Name:      {member.Name}");
    Console.WriteLine($"Role:      {member.Role}");
    Console.WriteLine($"Shift:     {member.Shift}");

    Console.WriteLine(
        $"Certified: {(member.Certified ? "Yes" : "No")}");

    Console.WriteLine("--------------------------------");
}


// --------------------------------------------------
// Pause before returning to menu
// --------------------------------------------------

void Pause()
{
    Console.WriteLine();
    Console.Write(
        "Press Enter to return to the menu...");

    Console.ReadLine();
}