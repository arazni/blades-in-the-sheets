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

	public const string FrenchMissingBackgroundV3 = """
{
  "Id": "546d8104-b3e9-4c2b-8742-3f6a68914290",
  "GameName": "Blades in the Dark",
  "Language": "Français",
  "Version": 3,
  "Dossier": {
    "Name": "ti",
    "CrewId": "",
    "Alias": "ta",
    "Look": "spa",
    "Notes": "",
    "Background": {
      "Name": "",
      "Description": "no"
    },
    "Heritage": {
      "Name": "Skovlan",
      "Description": "no"
    },
    "Vice": {
      "Name": "Plaisir",
      "Description": "noo"
    }
  },
  "Monitor": {
    "Stress": {
      "CurrentStress": 8,
      "MaxStress": 9
    },
    "Trauma": {
      "TraumaSet": []
    },
    "Harm": {
      "BoundedLesserHarms": {
        "Items": [
          null,
          null
        ]
      },
      "BoundedModerateHarms": {
        "Items": [
          null,
          null
        ]
      },
      "BoundedSevereHarms": {
        "Items": [
          null
        ]
      },
      "BoundedFatalHarms": {
        "Items": [
          null
        ]
      },
      "HealingClock": {
        "Time": 0,
        "Rollover": 0,
        "Size": 4
      }
    }
  },
  "Talent": {
    "AttributesByName": {
      "Perception": {
        "ActionsByName": {
          "Chasser": {
            "MaxRating": 4,
            "Rating": 2
          },
          "Etudier": {
            "MaxRating": 4,
            "Rating": 2
          },
          "Observer": {
            "MaxRating": 4,
            "Rating": 1
          },
          "Bricoler": {
            "MaxRating": 4,
            "Rating": 0
          }
        },
        "Experience": {
          "Points": 0,
          "MaxPoints": 6
        }
      },
      "Adresse": {
        "ActionsByName": {
          "Manipuler": {
            "MaxRating": 4,
            "Rating": 0
          },
          "Infiltrer": {
            "MaxRating": 4,
            "Rating": 0
          },
          "Affronter": {
            "MaxRating": 4,
            "Rating": 1
          },
          "Démolir": {
            "MaxRating": 4,
            "Rating": 0
          }
        },
        "Experience": {
          "Points": 0,
          "MaxPoints": 6
        }
      },
      "Volonté": {
        "ActionsByName": {
          "Invoquer": {
            "MaxRating": 4,
            "Rating": 0
          },
          "Commander": {
            "MaxRating": 4,
            "Rating": 1
          },
          "Conspirer": {
            "MaxRating": 4,
            "Rating": 0
          },
          "Influencer": {
            "MaxRating": 4,
            "Rating": 0
          }
        },
        "Experience": {
          "Points": 0,
          "MaxPoints": 6
        }
      }
    }
  },
  "Playbook": {
    "AbilitiesByName": {
      "Survivant": {
        "Name": "Survivant",
        "Description": "Que ce soit grâce à l’expérience chèrement acquise ou à un rituel occulte, vous êtes immunisé contre les miasmes empoisonnés des terres mortes, ce qui vous permet de survivre en vous nourrissant de l’étrange faune et flore qu’on peut y trouver. Vous gagnez en outre +1 case de stress.",
        "TimesTaken": 1
      },
      "Chasseur spectral": {
        "Name": "Chasseur spectral",
        "Description": "Votre animal de compagnie est imprégné d’énergie spirituelle. Cela le rend puissant lorsqu’il piste ou combat des entités surnaturelles, et lui confère une capacité occulte au choix : forme spectrale, lien mental ou rapide comme l’éclair. Choisissez cette capacité plusieurs fois pour conférer une capacité occulte supplémentaire à votre animal.",
        "TimesTaken": 1
      }
    },
    "Experience": {
      "Points": 8,
      "MaxPoints": 8
    },
    "Name": "Traqueur (Hound)"
  },
  "Gear": {
    "LoadoutByName": {},
    "AvailableGearByName": {
      "Paire de pistolets de qualité": {
        "Bulk": 1,
        "Name": "Paire de pistolets de qualité"
      },
      "Fusil long de qualité": {
        "Bulk": 2,
        "Name": "Fusil long de qualité"
      },
      "Cartouches électroplasmiques": {
        "Bulk": 1,
        "Name": "Cartouches électroplasmiques"
      },
      "Animal dressé pour la chasse": {
        "Bulk": 0,
        "Name": "Animal dressé pour la chasse"
      },
      "Longue-vue": {
        "Bulk": 1,
        "Name": "Longue-vue"
      },
      "Une lame ou deux": {
        "Bulk": 1,
        "Name": "Une lame ou deux"
      },
      "Couteaux de lancer": {
        "Bulk": 1,
        "Name": "Couteaux de lancer"
      },
      "Pistolet": {
        "Bulk": 1,
        "Name": "Pistolet"
      },
      "Second pistolet": {
        "Bulk": 1,
        "Name": "Second pistolet"
      },
      "Arme lourde": {
        "Bulk": 2,
        "Name": "Arme lourde"
      },
      "Arme inhabituelle": {
        "Bulk": 1,
        "Name": "Arme inhabituelle"
      },
      "Armure": {
        "Bulk": 2,
        "Name": "Armure"
      },
      "+Lourde": {
        "Bulk": 3,
        "Name": "+Lourde"
      },
      "Matériel de cambriolage": {
        "Bulk": 1,
        "Name": "Matériel de cambriolage"
      },
      "Matériel d’escalade": {
        "Bulk": 2,
        "Name": "Matériel d’escalade"
      },
      "Accessoires occultes": {
        "Bulk": 1,
        "Name": "Accessoires occultes"
      },
      "Documents": {
        "Bulk": 1,
        "Name": "Documents"
      },
      "Matériel de faussaire": {
        "Bulk": 1,
        "Name": "Matériel de faussaire"
      },
      "Outils de démolition": {
        "Bulk": 2,
        "Name": "Outils de démolition"
      },
      "Outils de bricolage": {
        "Bulk": 1,
        "Name": "Outils de bricolage"
      },
      "Lanterne": {
        "Bulk": 1,
        "Name": "Lanterne"
      },
      "Amulette contre les esprits": {
        "Bulk": 0,
        "Name": "Amulette contre les esprits"
      },
      "1 Coin": {
        "Bulk": 1,
        "Name": "1 Coin"
      },
      "2 Coins": {
        "Bulk": 2,
        "Name": "2 Coins"
      },
      "3 Coins": {
        "Bulk": 3,
        "Name": "3 Coins"
      },
      "4 Coins": {
        "Bulk": 4,
        "Name": "4 Coins"
      }
    },
    "Commitment": {
      "MaxBulkByCommitmentOption": {
        "None": 0,
        "Light": 3,
        "Normal": 5,
        "Heavy": 6,
        "Encumbered": 9
      },
      "Commitment": 0
    },
    "IsCommitmentLocked": false
  },
  "Fund": {
    "Satchel": {
      "Coins": 0
    },
    "Stash": {
      "Stash": 0
    }
  },
  "Rolodex": {
    "Friends": [
      {
        "Entry": "Steiner, un assassin",
        "Closeness": 0
      },
      {
        "Entry": "Celene, une sentinelle",
        "Closeness": 2
      },
      {
        "Entry": "Melvir, un médecin",
        "Closeness": 0
      },
      {
        "Entry": "Veleris, un espion",
        "Closeness": 1
      },
      {
        "Entry": "Casta, une chasseuse de prime",
        "Closeness": 0
      }
    ]
  },
  "Session": {
    "PlaybookExpressions": 0,
    "CharacterExpressions": 0,
    "StruggleExpressions": 0
  },
  "Notebook": ""
}
""";

	public static string FrenchAlternateBackgroundV3(string value) =>
		FrenchMissingBackgroundV3.Replace("\"Name\": \"\"", $"\"Name\": \"{value}\"");

	public static string LazyEnglishAlternateBackgroundV3(string value) =>
		FrenchMissingBackgroundV3.Replace("\"Language\": \"Français\"", "\"Language\": \"English\"")
			.Replace("\"Name\": \"\"", $"\"Name\": \"{value}\"");
}