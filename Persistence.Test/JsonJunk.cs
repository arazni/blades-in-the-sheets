namespace Persistence.Test;

public class JsonJunk
{
	public const string SpiderJsonV1 = @"
{
	""Id"": ""c46ba7cb-993b-4fc7-974d-fb95eacd5446"",
	""Dossier"": {
		""Name"": ""Brenda Hilton"",
		""CrewId"": """",
		""Alias"": ""Webweaver"",
		""Look"": ""Professional, upkept attire, always ready to impress any contacts she meets"",
		""Notes"": """",
		""Background"": {
			""Background"": 5,
			""Description"": ""Ex-mercenary sailor, ostracized by many members of my family for working with people antagonistic to them""
		},
		""Heritage"": {
			""Heritage"": 2,
			""Description"": ""My family are well-known pirates""
		},
		""Vice"": {
			""Vice"": 4,
			""Description"": ""I donate spare coin to my family whenever possible to compensate for my guilt in betraying them.""
		}
	},
	""Monitor"": {
		""Stress"": {
			""CurrentStress"": 0
		},
		""Trauma"": {
			""TraumaSet"": [
				1,
				3
			]
		},
		""Harm"": {
			""BoundedLesserHarms"": {
				""Items"": [
					""hmm"",
					null
				]
			},
			""BoundedModerateHarms"": {
				""Items"": [
					null,
					null
				]
			},
			""BoundedSevereHarms"": {
				""Items"": [
					null
				]
			},
			""BoundedFatalHarms"": {
				""Items"": [
					null
				]
			},
			""HealingClock"": {
				""Time"": 1,
				""Rollover"": 0
			}
		}
	},
	""Talent"": {
		""Insight"": {
			""Hunt"": {
				""Rating"": 1,
				""PlaybookDefault"": 0
			},
			""Study"": {
				""Rating"": 1,
				""PlaybookDefault"": 1
			},
			""Survey"": {
				""Rating"": 1,
				""PlaybookDefault"": 0
			},
			""Tinker"": {
				""Rating"": 0,
				""PlaybookDefault"": 0
			},
			""Experience"": {
				""Points"": 3,
				""MaxPoints"": 6
			}
		},
		""Prowess"": {
			""Finesse"": {
				""Rating"": 0,
				""PlaybookDefault"": 0
			},
			""Prowl"": {
				""Rating"": 1,
				""PlaybookDefault"": 0
			},
			""Skirmish"": {
				""Rating"": 0,
				""PlaybookDefault"": 0
			},
			""Wreck"": {
				""Rating"": 0,
				""PlaybookDefault"": 0
			},
			""Experience"": {
				""Points"": 3,
				""MaxPoints"": 6
			}
		},
		""Resolve"": {
			""Attune"": {
				""Rating"": 0,
				""PlaybookDefault"": 0
			},
			""Command"": {
				""Rating"": 0,
				""PlaybookDefault"": 0
			},
			""Consort"": {
				""Rating"": 2,
				""PlaybookDefault"": 2
			},
			""Sway"": {
				""Rating"": 1,
				""PlaybookDefault"": 0
			},
			""Experience"": {
				""Points"": 1,
				""MaxPoints"": 6
			}
		}
	},
	""Playbook"": {
		""AbilitiesByName"": {
			""Foresight"": {
				""Name"": ""Foresight"",
				""Description"": ""Two times per score you can assist a teammate without paying stress. Tell us how you prepared for this. I want to test something out."",
				""TimesTaken"": 1,
				""Source"": 6
			},
			""Mastermind"": {
				""Name"": ""Mastermind"",
				""Description"": ""You may expend your special armor to protect a teammate, or to push yourself when you gather information or work on a long-term project."",
				""TimesTaken"": 1,
				""Source"": 6
			}
		},
		""Experience"": {
			""Points"": 7,
			""MaxPoints"": 8
		},
		""Option"": 6
	},
	""Gear"": {
		""LoadoutByName"": {},
		""AvailableGearByName"": {
			""Fine cover identity"": {
				""Bulk"": 0,
				""Name"": ""Fine cover identity"",
				""Source"": 6
			},
			""Fine bottle of whiskey"": {
				""Bulk"": 1,
				""Name"": ""Fine bottle of whiskey"",
				""Source"": 6
			},
			""Blueprints"": {
				""Bulk"": 1,
				""Name"": ""Blueprints"",
				""Source"": 6
			},
			""Vial of slumber essence"": {
				""Bulk"": 0,
				""Name"": ""Vial of slumber essence"",
				""Source"": 6
			},
			""Concealed palm pistol"": {
				""Bulk"": 0,
				""Name"": ""Concealed palm pistol"",
				""Source"": 6
			},
			""Spiritbane charm"": {
				""Bulk"": 0,
				""Name"": ""Spiritbane charm"",
				""Source"": 6
			},
			""A Blade or Two"": {
				""Bulk"": 1,
				""Name"": ""A Blade or Two"",
				""Source"": 0
			},
			""Throwing Knives"": {
				""Bulk"": 1,
				""Name"": ""Throwing Knives"",
				""Source"": 0
			},
			""A Pistol"": {
				""Bulk"": 1,
				""Name"": ""A Pistol"",
				""Source"": 0
			},
			""A 2nd Pistol"": {
				""Bulk"": 1,
				""Name"": ""A 2nd Pistol"",
				""Source"": 0
			},
			""A Large Weapon"": {
				""Bulk"": 2,
				""Name"": ""A Large Weapon"",
				""Source"": 0
			},
			""An Unusual Weapon"": {
				""Bulk"": 1,
				""Name"": ""An Unusual Weapon"",
				""Source"": 0
			},
			""Armor"": {
				""Bulk"": 2,
				""Name"": ""Armor"",
				""Source"": 0
			},
			""+Heavy"": {
				""Bulk"": 3,
				""Name"": ""+Heavy"",
				""Source"": 0
			},
			""Burglary Gear"": {
				""Bulk"": 1,
				""Name"": ""Burglary Gear"",
				""Source"": 0
			},
			""Climbing Gear"": {
				""Bulk"": 2,
				""Name"": ""Climbing Gear"",
				""Source"": 0
			},
			""Arcane Implements"": {
				""Bulk"": 1,
				""Name"": ""Arcane Implements"",
				""Source"": 0
			},
			""Documents"": {
				""Bulk"": 1,
				""Name"": ""Documents"",
				""Source"": 0
			},
			""Subterfuge Supplies"": {
				""Bulk"": 1,
				""Name"": ""Subterfuge Supplies"",
				""Source"": 0
			},
			""Demolition Tools"": {
				""Bulk"": 2,
				""Name"": ""Demolition Tools"",
				""Source"": 0
			},
			""Tinkering Tools"": {
				""Bulk"": 1,
				""Name"": ""Tinkering Tools"",
				""Source"": 0
			},
			""Lantern"": {
				""Bulk"": 1,
				""Name"": ""Lantern"",
				""Source"": 0
			}
		},
		""Commitment"": {
			""Commitment"": 0
		}
	},
	""Fund"": {
		""Satchel"": {
			""Coins"": 4
		},
		""Stash"": {
			""Stash"": 40
		}
	},
	""Rolodex"": {
		""Friends"": [
			{
				""Entry"": ""Salia, an information broker"",
				""Closeness"": 0
			},
			{
				""Entry"": ""Augus, a master architect"",
				""Closeness"": 0
			},
			{
				""Entry"": ""Jennah, a servant"",
				""Closeness"": 2
			},
			{
				""Entry"": ""Riven, a chemist"",
				""Closeness"": 1
			},
			{
				""Entry"": ""Jeren, a bluecoat archivist"",
				""Closeness"": 0
			}
		]
	},
	""Session"": {
		""PlaybookExpressions"": 1,
		""CharacterExpressions"": 2,
		""StruggleExpressions"": 1
	},
	""Notebook"": """"
}
";
}