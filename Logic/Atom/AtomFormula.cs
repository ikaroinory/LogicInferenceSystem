using Logic.LogicFormula;

namespace Logic.Atom;

public abstract class AtomFormula : Token, IUnifyMeta
{
    public bool Negative { get; set; }

    internal AtomFormula(bool negative) => Negative = negative;

    public AtomFormula GetNegativeAtom()
    {
        var temp = DeepCopy();
        temp.Negative = !Negative;
        return temp;
    }

    internal abstract AtomFormula DeepCopy();

    public abstract bool IsNegative(AtomFormula atom);

    public abstract override string? ToString();

    public override bool Equals(object? obj)
    {
        if (obj is null or not IUnifyMeta) return false;
        return Equals(obj as IUnifyMeta);
    }

    public abstract bool Equals(IUnifyMeta? other);

    public abstract override int GetHashCode();
}
