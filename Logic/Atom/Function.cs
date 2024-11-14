namespace Logic.Atom;

public class Function : AtomFormula, IUnifyMeta
{
    public FunctionOperator Operator { get; }
    public FunctionArgument Arguments { get; }

    private Function(FunctionOperator op, FunctionArgument args, bool negative = false) : base(negative)
    {
        Operator = op;
        Arguments = args;
    }

    public Function(string op, params AtomFormula[] list) : this(new FunctionOperator(op), new FunctionArgument([.. list])) { }

    internal override AtomFormula DeepCopy() => new Function(Operator.DeepCopy(), Arguments.DeepCopy(), Negative);

    public override bool IsNegative(AtomFormula atom) =>
        atom is Function other && Negative != atom.Negative && Operator.Equals(other.Operator) && Arguments.Equals(other.Arguments);

    public override string ToString() => $"{Operator}{Arguments}";

    public override bool Equals(IUnifyMeta? other) =>
        other is Function obj
        && Negative == obj.Negative
        && Operator.Equals(obj.Operator)
        && Arguments.Equals(obj.Arguments);

    public override int GetHashCode() => HashCode.Combine(Operator.GetHashCode(), Arguments.GetHashCode());
}
