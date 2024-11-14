namespace Logic.Atom;

public class FunctionArgument(List<AtomFormula> list) : IUnifyMeta
{
    private List<AtomFormula> List { get; set; } = list;

    public int Count => List.Count;

    public AtomFormula this[int index] => List[index];
    public AtomFormula this[Index index] => List[index];
    public FunctionArgument this[Range range] => new(List[range]);

    internal FunctionArgument DeepCopy() => new FunctionArgument([.. List]);

    internal void Replace(IDictionary<AtomFormula, AtomFormula> unifyTable) =>
        List = List.Select(x => unifyTable.TryGetValue(x, out AtomFormula? value) ? value : x).ToList();

    public override string ToString() => $"({string.Join(", ", List)})";

    public bool Equals(IUnifyMeta? other) => other is FunctionArgument obj && List.SequenceEqual(obj.List);

    public override int GetHashCode()
    {
        if (List.Count == 0) return 0;

        // FNV-1a HASH Algorithm
        const int FNV_OFFSET_BASIS = unchecked((int)0x811C9DC5);
        const int FNV_prime = 16777619;

        int hash = FNV_OFFSET_BASIS;
        foreach (var item in List)
        {
            // FNV-1a 算法
            hash ^= item?.GetHashCode() ?? 0;
            hash *= FNV_prime;
        }

        return hash;
    }
}
