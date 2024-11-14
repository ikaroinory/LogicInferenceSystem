namespace Logic.Symbols;

public abstract class Operator(string symbol, int precedence) : Symbol(symbol), IComparable<Operator>
{
    private readonly int _precedence = precedence;

    public static bool operator >(Operator left, Operator right) => left.CompareTo(right) > 0;
    public static bool operator <(Operator left, Operator right) => left.CompareTo(right) < 0;
    public static bool operator >=(Operator left, Operator right) => left.CompareTo(right) >= 0;
    public static bool operator <=(Operator left, Operator right) => left.CompareTo(right) <= 0;
    public static bool operator ==(Operator left, Operator right) => left.CompareTo(right) == 0;
    public static bool operator !=(Operator left, Operator right) => left.CompareTo(right) != 0;

    public int CompareTo(Operator? other)
    {
        return other is null ? 1 : _precedence.CompareTo(other._precedence);
    }
}
