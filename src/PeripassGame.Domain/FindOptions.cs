namespace PeripassGame.Domain;

public record struct FindOptions(int MaxCombinationLength = 6) {
  public FindOptions() : this(6) { }
}