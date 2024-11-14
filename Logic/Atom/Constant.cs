namespace Logic.Atom;

public class Constant : AtomFormula, IUnifyMeta
{
    private string Value { get; }

    private Constant(string value, bool negative) : base(negative) => Value = value;

    public Constant(string value) : this(value, false) { }

    internal override AtomFormula DeepCopy() => new Constant(Value, Negative);

    public override bool IsNegative(AtomFormula atom) => atom is Constant other && Negative != atom.Negative && Value == other.Value;

    public override string ToString() => Value;

    public override bool Equals(IUnifyMeta? other) => other is Constant obj && Negative == obj.Negative && Value == obj.Value;

    public override int GetHashCode() => Value.GetHashCode();
}
