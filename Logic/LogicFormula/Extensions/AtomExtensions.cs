using Logic.Atom;

namespace Logic.LogicFormula.Extensions;

public static class AtomExtensions
{
    private static DisplayFormula Operate(this AtomFormula atom, Func<DisplayFormula, DisplayFormula> operation) => operation(new DisplayFormula(atom));

    public static DisplayFormula Negate(this AtomFormula atom) => atom.Operate(f => f.Negate());

    public static DisplayFormula Conjoin(this AtomFormula atom, AtomFormula value) => atom.Operate(f => f.Conjoin(value));
    public static DisplayFormula Conjoin(this AtomFormula atom, DisplayFormula value) => atom.Operate(f => f.Conjoin(value));

    public static DisplayFormula Disjoin(this AtomFormula atom, AtomFormula value) => atom.Operate(f => f.Disjoin(value));
    public static DisplayFormula Disjoin(this AtomFormula atom, DisplayFormula value) => atom.Operate(f => f.Disjoin(value));

    public static DisplayFormula Implie(this AtomFormula atom, AtomFormula value) => atom.Operate(f => f.Implie(value));
    public static DisplayFormula Implie(this AtomFormula atom, DisplayFormula value) => atom.Operate(f => f.Implie(value));

    public static DisplayFormula BiCondition(this AtomFormula atom, AtomFormula value) => atom.Operate(f => f.BiCondition(value));
    public static DisplayFormula BiCondition(this AtomFormula atom, DisplayFormula value) => atom.Operate(f => f.BiCondition(value));
}
