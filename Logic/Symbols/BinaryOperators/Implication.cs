namespace Logic.Symbols.BinaryOperators;

public class Implication : BinaryOperator
{
    public static readonly Implication Instance = new();

    private Implication() : base("→", 1) { }
}
