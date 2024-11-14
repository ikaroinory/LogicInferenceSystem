using Logic.Atom;
using Logic.Utils;

namespace Logic.LogicFormula;

public class Formula : IOperable<Formula>, IDeepCopiable<Formula>
{
    private readonly DisplayFormula _displayFormula;
    private readonly ComplexFormula _complexFormula;

    internal List<HashSet<AtomFormula>> ClauseList => _complexFormula.tokenList;

    private Formula(DisplayFormula displayFormula, ComplexFormula complexFormula)
    {
        _displayFormula = displayFormula;
        _complexFormula = complexFormula;
    }

    public Formula(AtomFormula atom) : this(new DisplayFormula(atom), new ComplexFormula(atom)) { }

    public Formula BiCondition(AtomFormula atom) => BiCondition(new Formula(atom));

    public Formula BiCondition(Formula value)
    {
        _displayFormula.BiCondition(value._displayFormula);
        _complexFormula.BiCondition(value._complexFormula);
        return this;
    }

    public Formula Conjoin(AtomFormula atom) => Conjoin(new Formula(atom));

    public Formula Conjoin(Formula value)
    {
        _displayFormula.Conjoin(value._displayFormula);
        _complexFormula.Conjoin(value._complexFormula);
        return this;
    }

    public Formula Disjoin(AtomFormula atom) => Disjoin(new Formula(atom));

    public Formula Disjoin(Formula value)
    {
        _displayFormula.Disjoin(value._displayFormula);
        _complexFormula.Disjoin(value._complexFormula);
        return this;
    }

    public Formula Implie(AtomFormula atom) => Implie(new Formula(atom));

    public Formula Implie(Formula value)
    {
        _displayFormula.Implie(value._displayFormula);
        _complexFormula.Implie(value._complexFormula);
        return this;
    }

    public Formula Negate()
    {
        _displayFormula.Negate();
        _complexFormula.Negate();
        return this;
    }

    public Formula DeepCopy() => new(_displayFormula, _complexFormula.DeepCopy());

    public override string ToString() => _displayFormula.ToString();
}
