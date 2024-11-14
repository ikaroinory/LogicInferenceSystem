using Logic.Atom;
using Logic.Symbols.BinaryOperators;
using Logic.Symbols.Separator;
using Logic.Symbols.UnaryOperators;
using Logic.Utils;

namespace Logic.LogicFormula;

public class ComplexFormula : IOperable<ComplexFormula>, IDeepCopiable<ComplexFormula>
{
    internal List<HashSet<AtomFormula>> tokenList;

    private ComplexFormula(List<HashSet<AtomFormula>> tokenList) => this.tokenList = tokenList;

    public ComplexFormula(AtomFormula atom) => tokenList = [[atom]];

    private ComplexFormula SetTokenList(List<HashSet<AtomFormula>> tokenList)
    {
        this.tokenList = tokenList;
        return this;
    }

    public ComplexFormula BiCondition(AtomFormula atom) => BiCondition(new ComplexFormula(atom));
    public ComplexFormula BiCondition(ComplexFormula value) => Implie(value).Conjoin(value.Implie(this));

    public ComplexFormula Conjoin(AtomFormula atom) => Conjoin(new ComplexFormula(atom));
    public ComplexFormula Conjoin(ComplexFormula value) => SetTokenList([.. DeepCopy().tokenList, .. value.tokenList]);

    public ComplexFormula Disjoin(AtomFormula atom) => Disjoin(new ComplexFormula(atom));

    public ComplexFormula Disjoin(ComplexFormula value)
    {
        var result = tokenList
            .SelectMany(_ => value.tokenList, (left, right) => left.Concat(right).ToHashSet())
            .ToList();

        return SetTokenList(result);
    }

    public ComplexFormula Implie(AtomFormula atom) => Implie(new ComplexFormula(atom));
    public ComplexFormula Implie(ComplexFormula value) => Negate().Disjoin(value);

    public ComplexFormula Negate()
    {
        var tokenListCopy = DeepCopy().tokenList;

        if (tokenListCopy.Count == 1)
        {
            var list = tokenListCopy.SelectMany(x =>
                x.Select(y =>
                    {
                        y.Negative = !y.Negative;
                        return new HashSet<AtomFormula> { y };
                    }
                )
            ).ToList();
            return SetTokenList(list);
        }

        var subSentenceList = tokenListCopy.Select(Temp).ToList();

        var formula = subSentenceList[0];
        for (int i = 1; i < subSentenceList.Count; i++)
            formula.Disjoin(subSentenceList[i]);

        return new ComplexFormula(formula.tokenList);

        static ComplexFormula Temp(HashSet<AtomFormula> x)
        {
            var newFormula = new ComplexFormula([x]);
            newFormula.Negate();
            return newFormula;
        }
    }

    public ComplexFormula DeepCopy() => new(Utils.DeepCopy.Copy(tokenList));

    public override string ToString()
    {
        return string.Join(Conjunction.Instance.ToString(), tokenList.Select(ShowSubSentence));

        static string ShowSubSentence(HashSet<AtomFormula> list)
        {
            if (list.Count == 1) return $"{list.First()}";

            string str = string.Join(Disjunction.Instance.ToString(), list.Select(ShowNegativeAtom));

            return $"{LeftBracket.Instance}{str}{RightBracket.Instance}";
        }

        static string ShowNegativeAtom(AtomFormula atom) => atom.Negative ? $"{Negation.Instance}{atom}" : $"{atom}";
    }
}