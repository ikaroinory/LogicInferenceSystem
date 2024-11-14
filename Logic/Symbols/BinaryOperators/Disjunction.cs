namespace Logic.Symbols.BinaryOperators;

public class Disjunction : BinaryOperator
{
    public static readonly Disjunction Instance = new();

    private Disjunction() : base("∨", 2) { }
}
