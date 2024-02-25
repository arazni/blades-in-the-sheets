using Microsoft.AspNetCore.Components;
using Models.Characters;

namespace UI.Components.CharacterSheet;
public partial class SheetFundCard
{
	[Parameter, EditorRequired]
	public Fund Fund { get; set; } = new();

	[Parameter]
	public string CoinSynonym { get; set; } = Models.Settings.Constants.DefaultNames.Coin;

	bool IsFixMode { get; set; } = false;

	int CoinChangeInput { get; set; } = 0;

	string CoinChangeInputLabel => $"{CoinSynonym} to change";

	int InputMaximum => Math.Max(Fund.CoinSpaceRemaining, Fund.MaxAffordable);

	bool GainButtonEnabled => Fund.CanGain(CoinChangeInput) && CoinChangeInput > 0 && !IsFixMode;

	bool SpendButtonEnabled => Fund.CanSpend(CoinChangeInput) && CoinChangeInput > 0 && !IsFixMode;

	string SatchelLabel => $"{CoinSynonym} on hand";
}