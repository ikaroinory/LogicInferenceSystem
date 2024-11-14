namespace Logic.Symbols.Quantifiers;

public class ForAll : Quantifier
{
    public static readonly ForAll Instance = new();
    
    private ForAll() : base("∀") { }
}
