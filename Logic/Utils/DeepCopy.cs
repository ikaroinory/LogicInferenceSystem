using Logic.Atom;

namespace Logic.Utils;

internal static class DeepCopy
{
    internal static List<List<AtomFormula>> Copy(List<List<AtomFormula>> list) => list.Select(x => x.Select(y => y.DeepCopy()).ToList()).ToList();
    internal static List<HashSet<AtomFormula>> Copy(List<HashSet<AtomFormula>> list) => list.Select(x => x.Select(y => y.DeepCopy()).ToHashSet()).ToList();
    internal static HashSet<AtomFormula> Copy(HashSet<AtomFormula> list) => list.Select(x => x.DeepCopy()).ToHashSet();
}
