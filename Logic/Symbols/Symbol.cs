using Logic.LogicFormula;

namespace Logic.Symbols;

public abstract class Symbol(string symbol) : Token
{
    public override string? ToString() => symbol;
}
