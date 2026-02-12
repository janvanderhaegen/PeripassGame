namespace PeripassGame.Domain;

public sealed class WordCombinationFinder(WordBank wordBank) {
  private readonly WordBank _wordBank = wordBank;

  public async Task<IReadOnlyList<string>> GetAllCombinationsAsync(FindOptions options) {
    var partials = _wordBank.Partials;

    var results = _wordBank.Objectives
      .AsParallel()
      .SelectMany(target => {
        var list = new List<string>();
        FindCombinationsRecursive(target, 0, [], partials, options.MaxCombinationLength, list);
        return list;
      })
      .ToList();

    return results;
  }

  private static void FindCombinationsRecursive(ReadOnlySpan<char> target, int start, List<string> path, IReadOnlySet<string> partials, int maxCombinationLength, List<string> results) {
    if (start == target.Length) {
      if (path.Count >= 2 && path.Count <= maxCombinationLength) {
        results.Add(string.Join('+', path) + '=' + target.ToString());
      }
      return;
    }

    for (var len = 1; len <= target.Length - start; len++) {
      var candidate = target.Slice(start, len).ToString();
      if (partials.Contains(candidate)) {
        path.Add(candidate);
        FindCombinationsRecursive(target, start + len, path, partials, maxCombinationLength, results);
        path.RemoveAt(path.Count - 1);
      }
    }
  }
}
