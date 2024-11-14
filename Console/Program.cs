using Logic.Atom;
using Logic.LogicFormula;
using Logic.LogicInference;

Console.OutputEncoding = System.Text.Encoding.UTF8;

void Inference(List<Formula> knowledgeList, Formula conclusion)
{
    Console.WriteLine("Knowledge:");
    knowledgeList.ForEach(x => Console.WriteLine($"    {x}"));

    var inference = new Inference();
    knowledgeList.ForEach(x => inference.AddKnowledge(x));

    Console.WriteLine($"Conclusion:\n    {conclusion}");

    var result = inference.Infer(conclusion);
    Console.WriteLine($"Result:\n    {result}\n");
}

Inference([
    new Formula(new Function("F", new Variable("x"))).Negate()
],
    new Formula(new Function("F", new Variable("x")))
);

Inference([
    new Formula(new Function("HardWorker", new Constant("小明"))).Negate(),
    new Formula(new Function("Lucky", new Constant("小明"))),
    new Formula(new Function("HardWorker", new Variable("x")))
        .Disjoin(new Function("Lucky", new Variable("x")))
        .Implie(new Function("PassExam", new Variable("x"))),
    new Formula(new Function("PassExam", new Variable("x")))
        .Implie(new Function("Happy", new Variable("x")))
],
    new Formula(new Function("Happy", new Variable("小明")))
);

Inference([
    new Formula(new Function("GradStudent", new Constant("sue"))),
    new Formula(new Function("GradStudent", new Variable("x")))
        .Implie(new Function("Student", new Variable("x"))),
    new Formula(new Function("Student", new Variable("x")))
        .Implie(new Function("HardWorker", new Variable("x")))
],
    new Formula(new Function("HardWorker", new Constant("sue")))
);

Inference([
    new Formula(new Function("A", new Constant("tony"))),
    new Formula(new Function("A", new Constant("mike"))),
    new Formula(new Function("A", new Constant("john"))),
    new Formula(new Function("L", new Constant("tony"), new Constant("rain"))),
    new Formula(new Function("L", new Constant("tony"), new Constant("snow"))),
    new Formula(new Function("A", new Variable("x"))).Negate()
        .Conjoin(new Function("S", new Variable("x")))
        .Conjoin(new Function("C", new Variable("x"))),
    new Formula(new Function("C", new Variable("y"))).Negate()
        .Conjoin(new Formula(new Function("L", new Variable("y"), new Constant("rain"))).Negate()),
    new Formula(new Function("L", new Variable("z"), new Constant("snow")))
        .Conjoin(new Formula(new Function("S", new Variable("z"))).Negate()),
    new Formula(new Function("L", new Constant("tony"), new Variable("u"))).Negate()
        .Conjoin(new Formula(new Function("L", new Constant("mike"), new Variable("u"))).Negate()),
    new Formula(new Function("L", new Constant("tony"), new Variable("v")))
        .Conjoin(new Formula(new Function("L", new Constant("mike"), new Variable("v"))))
],
    new Formula(new Function("A", new Variable("w"))).Negate()
        .Conjoin(new Function("S", new Variable("x")))
        .Conjoin(new Formula(new Function("C", new Variable("x"))).Negate())
);

Inference([
    new Formula(new Function("On", new Constant("tony"), new Constant("mike"))),
    new Formula(new Function("On", new Constant("mike"), new Constant("john"))),
    new Formula(new Function("Green", new Constant("tony"))),
    new Formula(new Function("Green", new Constant("john"))).Negate(),
],
    new Formula(new Function("On", new Variable("xx"), new Variable("yy"))).Negate()
        .Conjoin(new Formula(new Function("Green", new Variable("yy"))).Negate())
        .Conjoin(new Function("Green", new Variable("yy")))
);
