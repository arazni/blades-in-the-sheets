namespace Server;

public interface IEnvironmentInfo
{
	string Revision { get; }
	string Service { get; }
}
public class EnvironmentInfo(string service, string revision) : IEnvironmentInfo
{
	public string Revision { get; private set; } = revision;
	public string Service { get; private set; } = service;
}
