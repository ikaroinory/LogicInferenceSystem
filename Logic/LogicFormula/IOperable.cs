using Logic.Atom;

namespace Logic.LogicFormula;

public interface IOperable<T>
{
    T BiCondition(AtomFormula atom);
    T BiCondition(T value);

    T Conjoin(AtomFormula atom);
    T Conjoin(T value);

    T Disjoin(AtomFormula atom);
    T Disjoin(T value);

    T Implie(AtomFormula atom);
    T Implie(T value);

    T Negate();
}
