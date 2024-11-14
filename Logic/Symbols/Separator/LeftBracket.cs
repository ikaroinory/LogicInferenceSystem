namespace Logic.Symbols.Separator;

public class LeftBracket : Separator
{
    public static readonly LeftBracket Instance = new();

    private LeftBracket() : base("(") { }
}
