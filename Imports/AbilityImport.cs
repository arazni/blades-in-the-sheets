using Newtonsoft;
using Newtonsoft.Json;

namespace Imports;

internal class AbilityImport
{
	private const string TakeableBox = "";
	private const string NameDelimiter = ":";
	private static readonly string AbilityDistinguisher = Environment.NewLine + TakeableBox;
	private const string DataDirectoryName = "Data";

	public static void Main()
	{
		ProcessDirectory();
	}

	private static void ProcessDirectory()
	{
		string inputDirectory = FindInputDirectory();
		Console.WriteLine(inputDirectory);
		Console.WriteLine(string.Join(Environment.NewLine, Directory.GetFiles(inputDirectory)));
		string outputDirectory = FindOutputDirectory();
		Console.WriteLine(outputDirectory);

		var tasks = Directory.GetFiles(inputDirectory)
			.Where(file => file.StartsWith("playbook.abilities"))
			.Select
			(
				inputFile => new
				{
					In = inputFile,
					Out = ToOutputFile(outputDirectory, inputFile)
				}
			)
			.Select(pair => ProcessFile(pair.In, pair.Out))
			.ToArray();

		Task.WaitAll(tasks);
	}

	private static string FindInputDirectory()
	{
		var importDirectory = FindImportProjectDirectory();
		var dataDirectory = importDirectory.EnumerateDirectories().First(d => d.Name == DataDirectoryName);
		return dataDirectory.FullName;
	}

	private static DirectoryInfo FindImportProjectDirectory()
	{
		var startingDirectory = Directory.GetCurrentDirectory();
		var debug = Directory.GetParent(startingDirectory) ?? throw new NullReferenceException("debug");
		var bin = debug.Parent ?? throw new NullReferenceException("bin");
		var import = bin.Parent ?? throw new NullReferenceException("import");
		return import;
	}

	private static string FindOutputDirectory()
	{
		var importDirectory = FindImportProjectDirectory();
		var solution = importDirectory.Parent ?? throw new NullReferenceException("solution");
		var domain = solution.EnumerateDirectories().First(d => d.Name == "Persistence");
		var dataDirectory = domain.EnumerateDirectories().First(d => d.Name == DataDirectoryName);
		return dataDirectory.FullName;
	}

	private static string ToOutputFile(string outputDirectory, string inputFile) =>
		Path.Combine(outputDirectory, new FileInfo(inputFile).Name.Replace(".txt", ".json"));

	private static async Task ProcessFile(string filePath, string outputFilePath)
	{
		Console.WriteLine("Doing " + outputFilePath);

		var fileData = await File.ReadAllTextAsync(filePath);
		var abilityData = ProcessData(fileData);
		var json = JsonConvert.SerializeObject(abilityData, Formatting.Indented);

		await File.WriteAllTextAsync(outputFilePath, json);

		Console.WriteLine("Finished " + outputFilePath);
	}

	private static AbilityData[] ProcessData(string fileData) => 
		fileData.Split(AbilityDistinguisher)
			.Select
			(
				a => new AbilityData
				{
					TimesTakeable = a.Split(TakeableBox).Length,
					Name = a[(a.LastIndexOf(TakeableBox)+1)..a.IndexOf(NameDelimiter)],
					Description = string.Join(" ", a[(a.IndexOf(NameDelimiter)+1)..].Split(Environment.NewLine).Select(s => s.Trim()))
				}
			)
			.ToArray();
}
