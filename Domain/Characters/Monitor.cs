﻿namespace Domain.Characters;

public class Monitor
{
	public MonitorStress Stress { get; private set; } = new();

	public MonitorTrauma Trauma { get; private set; } = new();

	public MonitorHarm Harm { get; private set; } = new();

	public MonitorArmor Armor { get; private set; } = new();
}