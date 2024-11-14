namespace Logic.Symbols.BinaryOperators;

public class Conjunction : BinaryOperator
{
    public static readonly Conjunction Instance = new();

    private Conjunction() : base("∧", 3) { }
}
