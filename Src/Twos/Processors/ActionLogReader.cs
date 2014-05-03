using System;
using System.Collections.Generic;
using System.IO;
using Twos.Models;

namespace Twos.Processors
{
    public static class ActionLogReader
    {
        public static LoggedGame ReadLog(string logFileName)
        {
            int seed = -1;
            var actions = new List<GameAction>();

            using (var stream = File.Open(logFileName, FileMode.Open))
            {
                var seedRead = false;
                var buffer = new byte[sizeof(int) * 1000];
                int bytesRead;
                
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    int index = 0;
                    while (index < bytesRead)
                    {
                        int value = BitConverter.ToInt32(buffer, index);
                        if (!seedRead)
                        {
                            seed = value;
                            seedRead = true;
                        }
                        else
                        {
                            actions.Add((GameAction) value);
                        }

                        index += sizeof (int);
                    }
                }
            }

            return seed == -1
                       ? null
                       : new LoggedGame {Actions = actions.ToArray(), GameSeed = seed};
        }
    }
}
