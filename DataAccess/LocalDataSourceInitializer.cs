using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using AzureSearchStarter.Models;

namespace AzureSearchStarter.DataAccess
{
    public class LocalDataSourceInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<LocalDataSourceContext>
    {
        protected override void Seed(LocalDataSourceContext context)
        {
            var heroes = new List<SuperHeroModel>
                {
                    new SuperHeroModel { SuperHeroName = "Spider-man", RealName = "Peter Parker", PowersAndAbilities = "Super human strength and agility, plus the ability to cling to most surfaces. An accomplished scientist, inventor and photographer. Does whatever a spider can.", SuperHeroModelID = "1" },
                    new SuperHeroModel { SuperHeroName = "Captain Marvel", RealName = "Carol Danvers", PowersAndAbilities = "Flight, super human strength and durability, plus the ability to shoot concussive energy bursts from her hands. A skilled pilot and hand-to-hand combatant.", SuperHeroModelID = "2" },
                    new SuperHeroModel { SuperHeroName = "Hulk", RealName = "Bruce Banner", PowersAndAbilities = "Super human strength and durability (proportional to his anger - potentially limitless). Excellent healer - can regenerate limbs and vital organs. Dr. Banner is a genius.", SuperHeroModelID = "3" },
                    new SuperHeroModel { SuperHeroName = "Thor", RealName = "Thor Odinson", PowersAndAbilities = "Super human strength, endurance, and resistance to injury. Can tap into the Odinforce for cosmic and mystical energies, including control of lightning.", SuperHeroModelID = "4" },
                    new SuperHeroModel { SuperHeroName = "Iron Man", RealName = "Tony Stark", PowersAndAbilities = "A genius level intellect, allowing him to develop the Iron Man suit and other advanced weapons and armor.", SuperHeroModelID = "5" },
                    new SuperHeroModel { SuperHeroName = "Luke Cage", RealName = "Luke Cage", PowersAndAbilities = "Super human strength and resistance, mostly impervious to damage from sharp or blunt objects, close range gun fire, and explosions.", SuperHeroModelID = "6" },
                    new SuperHeroModel { SuperHeroName = "Black Widow", RealName = "Natasha Romanova", PowersAndAbilities = "An excellent spy, athlete, and assassin.", SuperHeroModelID = "7" },
                    new SuperHeroModel { SuperHeroName = "Daredevil", RealName = "Matt Murdock", PowersAndAbilities = "Blind, but his other senses are enhanced, including smell, touch, and hearing, including a sonar-like ability. Olympic-level speed, agility, strength, and endurance.", SuperHeroModelID = "8" },
                    new SuperHeroModel { SuperHeroName = "Captain America", RealName = "Steve Rogers", PowersAndAbilities = "Pinnacle of human physical perfection, including agility, strength, speed, and endurance. Skilled in hand-to-hand combat.", SuperHeroModelID = "9" },
                    new SuperHeroModel { SuperHeroName = "Wolverine", RealName = "James Howlett", PowersAndAbilities = "Superb healing factor and endurance. Enhanced agility, reflexes, hearing, and sense of smell. Skeleton is covered in adamantium, a virtually indestructible metal, including 3 retractable claws per hand. Due to his extended lifespan, he has mastered most hand-to-hand fighting styles.", SuperHeroModelID = "10" }
                };
            heroes.ForEach(h => context.MarvelSuperHeroes.Add(h));
            context.SaveChanges();
        }
    }
}