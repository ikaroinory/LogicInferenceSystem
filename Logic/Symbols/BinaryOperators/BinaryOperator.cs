namespace Logic.Symbols.BinaryOperators;

public abstract class BinaryOperator(string symbol, int precedence) : Operator(symbol, precedence)
{
    public override string ToString() => $" {base.ToString()} ";
}
