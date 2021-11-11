using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace TurtleChallenge.ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 3)
                throw new ArgumentException("Expected config & moves files, invalid parameters");

            string settingsFile = args[1];
            string movesFile = args[2];

            if (!File.Exists(settingsFile))
            {
                Console.WriteLine($"Config file not found. Check README.md for expected format.");
                return;
            }

            if (!File.Exists(movesFile))
            {
                Console.WriteLine($"Moves file not found. Check README.md for expected format.");
                return;
            }


            ITurtleSettings settings = LoadSettings(settingsFile);
            IEnumerable<TurtleAction> actions = LoadActions(movesFile);

            TurtleService turtleService = new TurtleService(new TurtleFactory());

            turtleService.MoveTurtleAcrossMap(settings, actions);
        }

        private static IEnumerable<TurtleAction> LoadActions(string movesFile)
        {
            List<TurtleAction> actions = new List<TurtleAction>();

            var contents = File.ReadAllText(movesFile);
            var entries = contents.Split(',');
            foreach (string entry in entries)
            {
                if (entry.ToUpper().Equals("M"))
                {
                    actions.Add(TurtleAction.Move);
                    continue;
                }
                if (entry.ToUpper().Equals("R"))
                {
                    actions.Add(TurtleAction.Rotate);
                }
            }

            return actions;
        }

        private static ITurtleSettings LoadSettings(string settingsFile)
        {
            TurtleSettings settings;
            XmlSerializer reader = new XmlSerializer(typeof(TurtleSettings));
            using (StreamReader file = new StreamReader(settingsFile))
            {
                settings = (TurtleSettings)reader.Deserialize(file);
                file.Close();
            }

            return settings;
        }
    }
}
