namespace PeripassGame.Domain;

public sealed class WordCombinationFinder(WordIndex wordBank) {
  private readonly WordIndex _wordBank = wordBank ?? throw new ArgumentNullException(nameof(wordBank));

  public async Task<IReadOnlyList<string>> GetAllCombinationsAsync(FindOptions options) {
    var maxCombinationLength = options.MaxCombinationLength == -1 ? _wordBank.Objectives.First().Length : options.MaxCombinationLength;

    if (maxCombinationLength < 2)
      throw new ArgumentException("options.MaxCombinationLength must be at least 2.");

    var results = _wordBank.Objectives
      .AsParallel()
      .SelectMany(target => {
        var list = new List<string>();
        FindCombinationsRecursive(target, 0, [], _wordBank.Partials, maxCombinationLength, list);
        return list;
      })
      .ToList();

    return results;
  }

  private static void FindCombinationsRecursive(ReadOnlySpan<char> target, int start, IList<string> path, IReadOnlyDictionary<int, IReadOnlySet<string>> partials, int maxCombinationLength, List<string> results) {
    for (
      var len = 1; 
      len < target.Length && len <= target.Length - start; 
      len++) {
      var candidate = target.Slice(start, len).ToString();
      if (partials[len].Contains(candidate)) {

        path.Add(candidate); //tried queue with push & pop but it was actually slower than list with add & removeat

        if (start + len == target.Length)
          results.Add(string.Join('+', path) + '=' + target.ToString());
        else if (path.Count < maxCombinationLength)
          FindCombinationsRecursive(target, start + len, path, partials, maxCombinationLength, results);

        path.RemoveAt(path.Count - 1); 
      }
    }
  }
}
