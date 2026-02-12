using System.Diagnostics;
using PeripassGame.Domain;

Console.WriteLine("Starting...");


var inputPath = args.Length > 0 ? args[0] : "input.txt";
var lines = await File.ReadAllLinesAsync(inputPath);
var words = lines
    .Select(l => l.Trim())
    .Where(w => w.Length > 0)
    .ToHashSet(StringComparer.Ordinal);

var stopwatch = Stopwatch.StartNew();
var partials = words.Where(w => w.Length < 6).ToHashSet(StringComparer.Ordinal);
var objectives = words.Where(w => w.Length == 6).ToHashSet(StringComparer.Ordinal);
var wordBank = new WordBank(partials, objectives);
var finder = new WordCombinationFinder(wordBank);
var combinations = await finder.GetAllCombinationsAsync( new FindOptions(MaxCombinationLength: 6));

stopwatch.Stop();

foreach (var c in combinations) {
  Console.WriteLine(c);
}
Console.WriteLine($"Found {combinations.Count} combinations in {stopwatch.ElapsedMilliseconds} ms");
Console.WriteLine();




