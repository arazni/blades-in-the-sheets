//namespace Domain.Characters;

//public class Armor
//{
//	public Armor()
//	{
//		this.canUse = () => false;
//	}

//	public Armor(Func<bool> canUse)
//	{
//		this.canUse = canUse;
//	}

//	private bool isUsed = false;

//	private readonly Func<bool> canUse;

//	public bool IsUsed
//	{
//		get => this.isUsed;
//		set
//		{
//			if (value == this.isUsed)
//				return;

//			if (!value)
//				Restore();

//			Use();
//		}
//	}

//	private void Use()
//	{
//		if (!this.canUse())
//			throw new InvalidOperationException("Trying to use armor inappropriately.");

//		this.isUsed = true;
//	}

//	private void Restore() =>
//		this.isUsed = false;
//}
