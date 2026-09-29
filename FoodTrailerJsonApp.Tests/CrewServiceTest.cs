
using FoodTrailerJsonApp.Models;
using FoodTrailerJsonApp.Services;

namespace FoodTrailerJsonApp.Tests;

public class CrewServiceTests : IDisposable
{
    private readonly string _testFilePath;
    private readonly CrewService _crewService;

    public CrewServiceTests()
    {
        // Create a separate temporary JSON file for testing.
        _testFilePath = Path.Combine(
            Path.GetTempPath(),
            $"crew-test-{Guid.NewGuid()}.json");

        _crewService = new CrewService(_testFilePath);
    }

    [Fact]
    public void GetAllCrewMembers_WhenFileDoesNotExist_ReturnsEmptyList()
    {
        // Act
        List<CrewMember> result =
            _crewService.GetAllCrewMembers();

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void AddCrewMember_AssignsIdAndSavesMember()
    {
        // Arrange
        CrewMember member = new CrewMember
        {
            Name = "John",
            Role = "Assistant",
            Shift = "Morning",
            Certified = true
        };

        // Act
        _crewService.AddCrewMember(member);

        // Assert
        List<CrewMember> result =
            _crewService.GetAllCrewMembers();

        Assert.Single(result);
        Assert.Equal(1, result[0].Id);
        Assert.Equal("John", result[0].Name);
        Assert.True(result[0].Certified);
    }

    [Fact]
    public void FindById_WhenMemberExists_ReturnsCorrectMember()
    {
        // Arrange
        _crewService.AddCrewMember(new CrewMember
        {
            Name = "Theo",
            Role = "Head Chef",
            Shift = "Morning",
            Certified = true
        });

        // Act
        CrewMember? result = _crewService.FindById(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Theo", result.Name);
    }

    [Fact]
    public void FindByName_IsCaseInsensitive()
    {
        // Arrange
        _crewService.AddCrewMember(new CrewMember
        {
            Name = "Maria",
            Role = "Driver",
            Shift = "Evening",
            Certified = true
        });

        // Act
        CrewMember? result = _crewService.FindByName("maria");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Maria", result.Name);
    }

    [Fact]
    public void UpdateCrewMember_ChangesExistingInformation()
    {
        // Arrange
        _crewService.AddCrewMember(new CrewMember
        {
            Name = "Jordan",
            Role = "Cashier",
            Shift = "Afternoon",
            Certified = false
        });

        CrewMember updatedMember = new CrewMember
        {
            Id = 1,
            Name = "Jordan",
            Role = "Assistant Manager",
            Shift = "Afternoon",
            Certified = true
        };

        // Act
        bool updated =
            _crewService.UpdateCrewMember(updatedMember);

        CrewMember? result = _crewService.FindById(1);

        // Assert
        Assert.True(updated);
        Assert.NotNull(result);
        Assert.Equal("Assistant Manager", result.Role);
        Assert.True(result.Certified);
    }

    [Fact]
    public void RemoveCrewMember_DeletesExistingMember()
    {
        // Arrange
        _crewService.AddCrewMember(new CrewMember
        {
            Name = "Test User",
            Role = "Assistant",
            Shift = "Morning",
            Certified = false
        });

        // Act
        bool removed = _crewService.RemoveCrewMember(1);

        // Assert
        Assert.True(removed);
        Assert.Empty(_crewService.GetAllCrewMembers());
    }

    [Fact]
    public void GetByShift_ReturnsMatchingCrew()
    {
        // Arrange
        _crewService.AddCrewMember(new CrewMember
        {
            Name = "Theo",
            Shift = "Morning"
        });

        _crewService.AddCrewMember(new CrewMember
        {
            Name = "Maria",
            Shift = "Evening"
        });

        // Act
        List<CrewMember> result =
            _crewService.GetByShift("morning");

        // Assert
        Assert.Single(result);
        Assert.Equal("Theo", result[0].Name);
    }

    [Fact]
    public void GetCertifiedCrew_ReturnsOnlyCertifiedMembers()
    {
        // Arrange
        _crewService.AddCrewMember(new CrewMember
        {
            Name = "Theo",
            Certified = true
        });

        _crewService.AddCrewMember(new CrewMember
        {
            Name = "Jordan",
            Certified = false
        });

        // Act
        List<CrewMember> result =
            _crewService.GetCertifiedCrew();

        // Assert
        Assert.Single(result);
        Assert.Equal("Theo", result[0].Name);
    }

    // Delete the temporary test file after each test.
    public void Dispose()
    {
        if (File.Exists(_testFilePath))
        {
            File.Delete(_testFilePath);
        }
    }
}
