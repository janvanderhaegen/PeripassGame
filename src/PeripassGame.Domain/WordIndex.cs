namespace PeripassGame.Domain;

public sealed record WordIndex {
  public IReadOnlyDictionary<int, IReadOnlySet<string>> Partials;
  public IReadOnlySet<string> Objectives;

  private WordIndex(IReadOnlyDictionary<int, IReadOnlySet<string>> partials, IReadOnlySet<string> objectives) {
    Partials = partials;
    Objectives = objectives;
  }

  //using a factory method because I don't like throwing logical validation errors from a constructor, and I want to ensure that the object is always in a valid state when created
  public static WordIndex Create(IEnumerable<string> input) {
    ArgumentNullException.ThrowIfNull(input);
    if (!input.Any()) throw new ArgumentException("Input collection must contain at least one word.", nameof(input));

    var groupedByLength = input.GroupBy(word => word.Length)
                         .ToDictionary(group => group.Key, group => (IReadOnlySet<string>)group.ToHashSet());

    if (groupedByLength.Count == 1) throw new ArgumentException("Input collection must contain at least two words of different length.", nameof(input));

    var lengthOfObjectives = groupedByLength.Keys.Max();
    var objectives = groupedByLength[lengthOfObjectives];
    var partials = groupedByLength.Where(group => group.Key < lengthOfObjectives)
                                 .ToDictionary(group => group.Key, group => group.Value);

    if (partials.Count == 1 && partials.Single().Value.Count < 2) throw new ArgumentException("The group of shorter words must contain at least two words.", nameof(input));

    //add empty groups for any missing lengths between 1 and lengthOfObjectives - 1, to avoid null checks later when using indexer[length]
    for (var i = 1; i < lengthOfObjectives; i++) {
      if (!partials.ContainsKey(i)) {
        partials[i] = new HashSet<string>();
      }
    }

    return new WordIndex(partials, objectives);
  }
}
