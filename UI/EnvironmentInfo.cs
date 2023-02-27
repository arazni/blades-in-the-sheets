namespace UI;

public interface IEnvironmentInfo
{
	string Revision { get; }
	string Service { get; }
}
public class EnvironmentInfo : IEnvironmentInfo
{
	public EnvironmentInfo(string service, string revision)
	{
		Revision = revision;
		Service = service;
	}

	public string Revision { get; private set; }
	public string Service { get; private set; }
}
