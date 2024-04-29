public class User
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public bool Internal { get; set; }
    public IReadOnlyList<string> ReadOnlyList { get; set; } = ["SimpleRole1", "SimpleRole2"];
    public ICollection<string> ListSimpleTypes { get; set; } = new List<string> { "Test", "User" };

    public IDictionary<string, string> Dictionary { get; set; } = new Dictionary<string, string>
        { { "Key1", "Value1" }, { "Key2", "Value2" } };

    public List<ComplexType> ComplexTypes { get; set; } =
    [
        new ComplexType(Guid.NewGuid(), "ComplexType1"),
        new ComplexType(Guid.NewGuid(), "ComplexType2")
    ];

    private List<ComplexType> _complexTypesPrivate { get; } =
    [
        new ComplexType(Guid.NewGuid(), "ComplexType1"),
        new ComplexType(Guid.NewGuid(), "ComplexType2")
    ];

    public IReadOnlyList<ComplexType> ComplexTypesPublicGet => _complexTypesPrivate;
}

public record ComplexType(Guid Id, string Name);