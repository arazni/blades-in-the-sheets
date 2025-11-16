namespace Models.Common;

public record LoadError(string UserMessage, string DevMessage, string CharacterJson, string CharacterKey);
