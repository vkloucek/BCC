using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BCC01
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileName = @"..\..\Pass_the_message-input.txt";

            if (args.Length > 0)
            {
                fileName = args[0];
            }

            if (File.Exists(fileName))
            {
                ProcessFile(fileName);
            }
            else
            {
                Console.WriteLine("Cannot open '{0}' file", Path.GetFullPath(fileName));
            }
        }

        private static void ProcessFile(string fileName)
        {
            using (var file = new StreamReader(fileName))
            {
                string line;

                if ((line = file.ReadLine()) == null)
                {
                    return;
                }

                int testCases;
                if (!Int32.TryParse(line, out testCases))
                {
                    return;
                }

                for (var c = 0; c < testCases; c++)
                {
                    if ((line = file.ReadLine()) == null)
                    {
                        return;
                    }

                    int friends;
                    if (!Int32.TryParse(line, out friends))
                    {
                        return;
                    }

                    var connections = new List<List<int>>();

                    for (var f = 0; f < friends; f++)
                    {
                        if ((line = file.ReadLine()) == null)
                        {
                            return;
                        }

                        var listItem = new List<int>();

                        foreach (var strConnestion in line.Split(' '))
                        {
                            int connection;
                            if (!Int32.TryParse(strConnestion, out connection))
                            {
                                return;
                            }

                            listItem.Add(connection);
                        }

                        connections.Add(listItem);
                    }

                    var setsOfConnectedFriedns = new List<HashSet<int>>();

                    for (var f = 1; f <= connections.Count; f++)
                    {
                        setsOfConnectedFriedns.Add(new HashSet<int> {f});
                    }

                    int i;

                    for (i = 1; i <= 100; i++)
                    {
                        for (var f = 1; f <= connections.Count; f++)
                        {
                            foreach (var alreadyConnected in setsOfConnectedFriedns[f - 1].ToArray())
                            {
                                foreach (var newConnection in connections[alreadyConnected - 1])
                                {
                                    setsOfConnectedFriedns[f - 1].Add(newConnection);
                                }
                            }
                        }

                        if (setsOfConnectedFriedns.Any(set => set.Count == connections.Count))
                        {
                            var friendsFullyConnected = new List<int>();

                            for (int f = 1; f <= connections.Count; f++)
                            {
                                if (setsOfConnectedFriedns[f - 1].Count == connections.Count)
                                {
                                    friendsFullyConnected.Add(f);
                                }
                            }

                            Console.WriteLine(String.Join(" ", friendsFullyConnected));

                            break;
                        }
                    }

                    if (i > 100)
                    {
                        Console.WriteLine("0");
                    }
                }
            }
        }
    }
}
