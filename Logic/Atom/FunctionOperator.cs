namespace Logic.Atom;

public class FunctionOperator(string name) : IUnifyMeta
{
    private string Name { get; init; } = name;

    internal FunctionOperator DeepCopy() => new(Name);

    public override string ToString() => Name;

    public bool Equals(IUnifyMeta? other) => other is FunctionOperator obj && Name == obj.Name;

    public override int GetHashCode() => Name.GetHashCode();
}
