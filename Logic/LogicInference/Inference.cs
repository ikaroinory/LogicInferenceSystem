using Logic.Atom;
using Logic.LogicFormula;
using Logic.Utils;

namespace Logic.LogicInference;

public class Inference
{
    private readonly List<HashSet<AtomFormula>> _knowledgeBase;

    public Inference(params Formula[] formula) => _knowledgeBase = formula.SelectMany(x => x.ClauseList).ToList();

    public Inference AddKnowledge(Formula knowledge)
    {
        _knowledgeBase.AddRange(knowledge.ClauseList);
        return this;
    }

    private IDictionary<AtomFormula, AtomFormula> Unify(IUnifyMeta x, IUnifyMeta y)
    {
        return Unify(x, y, new Dictionary<AtomFormula, AtomFormula>());
    }

    private IDictionary<AtomFormula, AtomFormula> Unify(IUnifyMeta x, IUnifyMeta y, IDictionary<AtomFormula, AtomFormula> substitution)
    {
        if (x.Equals(y))
            return substitution;

        if (x is Variable varX)
            return UnifyVar(varX, (AtomFormula)y, substitution);
        if (y is Variable varY)
            return UnifyVar(varY, (AtomFormula)x, substitution);
        if (x is Function fx && y is Function fy)
            return Unify(fx.Arguments, fy.Arguments, Unify(fx.Operator, fy.Operator, substitution));
        if (x is FunctionArgument fxArgs && y is FunctionArgument fyArgs)
            return Unify(fxArgs[0], fyArgs[0], Unify(fxArgs[1..], fyArgs[1..], substitution));

        return substitution;
    }

    private IDictionary<AtomFormula, AtomFormula> UnifyVar(AtomFormula var, AtomFormula x, IDictionary<AtomFormula, AtomFormula> substitution)
    {
        if (substitution.TryGetValue(var, out var v1))
            return Unify(v1, x, substitution);
        if (substitution.TryGetValue(x, out var v2))
            return Unify(var, v2, substitution);

        if (x is Variable && var == x)
            return substitution;

        substitution[var] = x;

        return substitution;
    }

    private HashSet<AtomFormula>? Resolution(HashSet<AtomFormula> clauseLeft, HashSet<AtomFormula> clauseRight)
    {
        foreach (var x in clauseLeft)
        {
            bool isUnify = false;
            foreach (var y in clauseRight)
            {
                if (x is not Function fx || y is not Function fy) break;
                if (!fx.Operator.Equals(fy.Operator)) break;

                var UnifyDict = Unify(x, y);
                foreach (var temp in clauseLeft)
                {
                    if (temp is not Function ftemp) continue;
                    ftemp.Arguments.Replace(UnifyDict);
                }
                foreach (var temp in clauseRight)
                {
                    if (temp is not Function ftemp) continue;
                    ftemp.Arguments.Replace(UnifyDict);
                }
                isUnify = true;
                break;
            }
            if (isUnify) break;
        }

        var concatClause = clauseLeft.Concat(clauseRight);

        var result = (from x in clauseLeft.Concat(clauseRight)
                      where !concatClause.Any(y => y.Equals(x.GetNegativeAtom()))
                      select x)
                      .Distinct()
                      .ToHashSet();

        if (result.Count == clauseLeft.Count + clauseRight.Count) return null;

        return result;
    }

    public bool Infer(Formula formula)
    {
        var knowledgeBaseCopy = DeepCopy.Copy(_knowledgeBase);
        knowledgeBaseCopy.AddRange(formula.Negate().ClauseList);

        var hasNewKnowledge = true;
        while (hasNewKnowledge)
        {
            hasNewKnowledge = false;
            List<HashSet<AtomFormula>> newKnowledgeList = [];
            foreach (var clauseLeft in knowledgeBaseCopy)
            {
                foreach (var clauseRight in knowledgeBaseCopy)
                {
                    if (clauseLeft.SetEquals(clauseRight)) continue;

                    var newKnowledge = Resolution(clauseLeft, clauseRight);

                    if (newKnowledge == null) continue;

                    if (newKnowledge.Count == 0) return true;

                    if (knowledgeBaseCopy.Any(x => x.SetEquals(newKnowledge)) ||
                        newKnowledgeList.Any(x => x.SetEquals(newKnowledge))) continue;

                    newKnowledgeList.Add(newKnowledge);
                    hasNewKnowledge = true;
                }
            }

            knowledgeBaseCopy.AddRange(newKnowledgeList);
        }

        return false;
    }
}
