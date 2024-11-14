namespace Logic.Symbols.UnaryOperators;

public class Negation : UnaryOperator
{
    public static readonly Negation Instance = new();

    private Negation() : base("¬", 4) { }
}
