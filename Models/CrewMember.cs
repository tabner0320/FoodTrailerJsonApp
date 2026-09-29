namespace FoodTrailerJsonApp.Models;

public class CrewMember
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Role { get; set; } = "";
    public string Shift { get; set; } = "";
    public bool Certified { get; set; }
}