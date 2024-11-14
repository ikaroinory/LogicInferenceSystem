using Logic.Atom;
using Logic.Symbols.BinaryOperators;
using Logic.Symbols.Quantifiers;
using Logic.Symbols.Separator;
using Logic.Symbols.UnaryOperators;

namespace Logic.LogicFormula;

public class DisplayFormula : IOperable<DisplayFormula>
{
    private readonly List<Token> _tokenList;

    private DisplayFormula(List<Token> tokenList) => _tokenList = tokenList;

    public DisplayFormula(AtomFormula atom) : this([atom]) { }

    private DisplayFormula Append(DisplayFormula value, BinaryOperator op)
    {
        if (_tokenList.Count > 1)
        {
            var leftLastOp = _tokenList.OfType<BinaryOperator>().LastOrDefault();
            if (leftLastOp is not null && leftLastOp < op)
            {
                _tokenList.Insert(0, LeftBracket.Instance);
                _tokenList.Add(RightBracket.Instance);
            }
        }

        _tokenList.Add(op);

        var addRightBracket = false;
        if (value._tokenList.Count > 1)
        {
            var rightFirstOp = _tokenList.OfType<BinaryOperator>().FirstOrDefault();
            if (rightFirstOp is not null && rightFirstOp < op)
            {
                _tokenList.Add(LeftBracket.Instance);
                addRightBracket = true;
            }
        }

        _tokenList.AddRange(value._tokenList);

        if (addRightBracket)
            _tokenList.Add(RightBracket.Instance);

        return this;
    }

    private DisplayFormula AddQuantifier(Quantifier quantifier, Variable variable)
    {
        _tokenList.Insert(0, LeftBracket.Instance);
        _tokenList.Insert(0, variable);
        _tokenList.Insert(0, quantifier);
        _tokenList.Add(RightBracket.Instance);
        return this;
    }

    public DisplayFormula BiCondition(AtomFormula atom) => BiCondition(new DisplayFormula(atom));
    public DisplayFormula BiCondition(DisplayFormula value) => Append(value, BiConditional.Instance);

    public DisplayFormula Conjoin(AtomFormula atom) => Conjoin(new DisplayFormula(atom));
    public DisplayFormula Conjoin(DisplayFormula value) => Append(value, Conjunction.Instance);

    public DisplayFormula Disjoin(AtomFormula atom) => Disjoin(new DisplayFormula(atom));
    public DisplayFormula Disjoin(DisplayFormula value) => Append(value, Disjunction.Instance);

    public DisplayFormula Implie(AtomFormula atom) => Implie(new DisplayFormula(atom));
    public DisplayFormula Implie(DisplayFormula value) => Append(value, Implication.Instance);

    public DisplayFormula Forall(Variable variable) => AddQuantifier(ForAll.Instance, variable);

    public DisplayFormula Exist(Variable variable) => AddQuantifier(Exists.Instance, variable);

    public DisplayFormula Negate()
    {
        if (_tokenList.Count > 1)
        {
            _tokenList.Insert(0, LeftBracket.Instance);
            _tokenList.Add(RightBracket.Instance);
        }

        _tokenList.Insert(0, Negation.Instance);
        return this;
    }

    public override string ToString() => string.Join("", _tokenList.Select(x => x.ToString()));
}
