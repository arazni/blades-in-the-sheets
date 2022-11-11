using Models.Characters;
using UI.Conveniences;

namespace UI.ViewModels;

public class VHeritage
{
	public HeritageOption Option { get; set; } = HeritageOption.Unknown;

	public string DisplayName => Option.DisplayName();

	public string Elaboration { get; set; } = string.Empty;

	public static IReadOnlyCollection<VHeritage> Options { get; } = new VHeritage[]
	{
		new VHeritage
		{
			Option = HeritageOption.Unknown,
			Elaboration = "Choose a heritage to learn more about it."
		},
		new VHeritage
		{
			Option = HeritageOption.Akoros,
			Elaboration = "Akoros is the largest and most industrialized land in the Imperium, and is home to the capitol city as well as Duskwall itself. They’re known as a diverse conglomeration of cultures that have grown together in close proximity for centuries, somewhat like Europe."
		},
		new VHeritage
		{
			Option = HeritageOption.TheDaggerIsles,
			Elaboration = "If you want to be a rootless wanderer you could be from the Dagger Isles. People there often grow up on ships and travel a lot before settling down. They’re known as corsairs and merchants who live without lightning barriers—dealing with spirits in other ways."
		},
		new VHeritage
		{
			Option = HeritageOption.Iruvia,
			Elaboration = "If you want to be from a culture considered “foreign” by the locals, you could be from Iruvia, a rich and powerful desert kingdom far to the south. It’s another diverse land of varying cultures similar to old Persia, Egypt, and India."
		},
		new VHeritage
		{
			Option = HeritageOption.Severos,
			Elaboration = "If you want to be from a place considered “wild” by the rest of the empire, you could be from Severos. Outside the few Imperial settlements, most Severosi live in nomadic horse-tribes scattered across the blasted deathlands, surviving within the ruins of ancient arcane fortresses which still repel spirits."
		},
		new VHeritage
		{
			Option = HeritageOption.Skovlan,
			Elaboration = "If you want to be from a marginalized people, you could be from Skovlan, the island kingdom just across the sea from Doskvol. Skovlan was last to be brought under Imperial rule, over the course of the 36-year Unity War (which ended only a few years ago). Many Skovlander refugees who lost their homes and jobs in the destruction of the war have come to Doskvol seeking new opportunities."
		},
		new VHeritage
		{
			Option = HeritageOption.Tycheros,
			Elaboration = "If you want to be weird, you can be from Tycheros. It’s a semi-mythical place, far away beyond the northern Void Sea. Everyone says that the people there are part-demon. If you choose Tycherosi heritage, also create a demonic telltale (like black shark eyes, feathers instead of hair, etc.) that marks your character."
		}
	};
}
