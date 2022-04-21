using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterTest
{
    internal static class GameSaver
    {
        public static SaveFile AddSave(Hero hero, int challengeIndex, Monster fighter)
        {
            SaveFile saveFile = new SaveFile()
            {
                heroType = hero.GetType().FullName,
                playerName = hero.heroName,
                playerClass = hero.archetype,
                playerHealth = hero.healthPoints,
                challengeIndex = challengeIndex,
                monsterName = fighter.name
            };
            return saveFile;
        }
        public static Hero LoadSave(SaveFile save)
        {
            Hero hero = null;

            var heroType = System.Reflection.Assembly.GetExecutingAssembly()?.GetType(save.heroType);

            if (heroType != null)
            {
                hero = Activator.CreateInstance(heroType) as Hero;
                if (hero != null)
                {
                    hero.heroName = save.playerName;
                    hero.archetype = save.playerClass;
                    hero.healthPoints = save.playerHealth;
                }
            }

            return hero;
        }

        public static void WriteHeroToFile(SaveFile saveFile)
        {
            string heroAsString = JsonConvert.SerializeObject(saveFile);
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + $"{saveFile.playerName}.txt", heroAsString);
            Console.WriteLine("Saving player data.");
        }

        public static SaveFile? ReadHeroFromFile(string playerName)
        {
            string json = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + $"{playerName}.txt");
            return JsonConvert.DeserializeObject<SaveFile>(json);
        }
    }
}
