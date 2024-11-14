namespace Logic.Symbols.BinaryOperators;

public class BiConditional : BinaryOperator
{
    public static readonly BiConditional Instance = new();

    private BiConditional() : base("↔", 0) { }
}
