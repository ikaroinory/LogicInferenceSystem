namespace Logic.Atom;

public class Variable : AtomFormula, IUnifyMeta
{
    private string Name { get; }

    private Variable(string name, bool negative) : base(negative) => Name = name;

    public Variable(string name) : this(name, false) { }

    internal override AtomFormula DeepCopy() => new Variable(Name, Negative);

    public override bool IsNegative(AtomFormula atom) => atom is Variable other && Negative != atom.Negative && Name == other.Name;

    public override string ToString() => Name;

    public override bool Equals(IUnifyMeta? other) => other is Variable obj && Negative == obj.Negative && Name == obj.Name;

    public override int GetHashCode() => Name.GetHashCode();
}
