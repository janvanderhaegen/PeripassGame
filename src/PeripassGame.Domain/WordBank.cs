namespace PeripassGame.Domain;

public record WordBank(IReadOnlySet<string> Partials, IReadOnlySet<string> Objectives);
