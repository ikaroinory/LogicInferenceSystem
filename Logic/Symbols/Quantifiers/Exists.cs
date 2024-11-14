namespace Logic.Symbols.Quantifiers;

public class Exists : Quantifier
{
    public static readonly Exists Instance = new();

    private Exists() : base("∃") { }
}
