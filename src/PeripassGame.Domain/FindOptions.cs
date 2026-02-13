namespace PeripassGame.Domain;

public record struct FindOptions(int MaxCombinationLength = -1) {
  [System.Diagnostics.DebuggerStepThrough]
  public FindOptions() : this(-1) { }
}