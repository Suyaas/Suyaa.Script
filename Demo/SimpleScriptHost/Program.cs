// See https://aka.ms/new-console-template for more information

string code = "return(let(a, \"Hello World\"), a)"; 
Console.WriteLine(code);
using (var script = Suyaa.Script.ScriptParser.Parse(code))
using (var funcs = new Suyaa.Script.ScriptFunctions())
{
    using (var engine = new Suyaa.Script.ScriptEngine(script, funcs))
    {
        Console.WriteLine(engine.Execute());
    }
}