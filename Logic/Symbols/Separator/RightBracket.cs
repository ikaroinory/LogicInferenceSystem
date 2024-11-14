namespace Logic.Symbols.Separator;

public class RightBracket : Separator
{
    public static readonly RightBracket Instance = new();

    private RightBracket() : base(")") { }
}
