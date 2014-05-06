using System;
using System.IO;
using System.Linq;
using System.Text;
using Twos.Models;
using Twos.Processors;

namespace Twos.Menus
{
    class ReplayLoggedGameMenu : IMenu
    {
        private const int PageSize = 9;

        private readonly FileInfo[] _loggedGameFilenames;
        private int _currentPage = 1;
        private int _totalPages;

        public ReplayLoggedGameMenu()
        {
            var directoryPath = GameSettings.LogOutputDirectory;
            _loggedGameFilenames = Directory.GetFiles(directoryPath, "*" + GameSettings.LogExtension)
                                            .Select(x => Path.Combine(GameSettings.LogOutputDirectory, x))
                                            .Select(x => new FileInfo(x))
                                            .OrderByDescending(x => x.LastWriteTimeUtc)
                                            .ToArray();

            _totalPages = (int)Math.Ceiling(_loggedGameFilenames.Length / (decimal)PageSize);
        }

        public string Question
        {
            get
            {
                var output = new StringBuilder("<blank>) Back To Main Menu");
                output.Append(Environment.NewLine);

                if (_currentPage > 1)
                {
                    output.AppendFormat("0) Previous Page{0}", Environment.NewLine);
                }

                var currentLoggedFiles = _loggedGameFilenames.Skip((_currentPage - 1)*PageSize)
                                                             .Take(PageSize)
                                                             .ToArray();

                for (int x = 0; x < currentLoggedFiles.Length; x++)
                {
                    output.AppendFormat("{0}) {1}{2}", x + 1, currentLoggedFiles[x].Name, Environment.NewLine);
                }

                if (_currentPage < _totalPages)
                {
                    output.AppendFormat("10) Next Page{0}", Environment.NewLine);
                }

                return output.ToString();
            }
        }

        public IMenu ProcessAnswer(GameRunnerParameters gameRunnerParameters, string answer)
        {
            if (string.IsNullOrWhiteSpace(answer))
            {
                return new MainMenu();
            }

            int answerNumber;
            if (int.TryParse(answer, out answerNumber))
            {
                if (answerNumber == 0)
                {
                    if (_currentPage > 1)
                    {
                        _currentPage--;
                        return this;
                    }
                }
                else if (answerNumber == 10)
                {
                    if (_currentPage < _totalPages)
                    {
                        _currentPage++;
                        return this;
                    }
                }
                else if (answerNumber > 0 && answerNumber < 10)
                {
                    int index = (answerNumber - 1) + ((_currentPage - 1) * PageSize);
                    if (index < _loggedGameFilenames.Length)
                    {
                        var gameFile = _loggedGameFilenames[index];
                        try
                        {
                            var loggedGame = ActionLogReader.ReadLog(gameFile.FullName);
                            gameRunnerParameters.GameSeed = loggedGame.GameSeed;
                            gameRunnerParameters.ReplayActions = loggedGame.Actions;

                            return null;
                        }
                        catch (Exception ex)
                        {
                            if (ex is IOException || ex is UnauthorizedAccessException)
                            {
                                Console.WriteLine("The selected file could not be read");
                                return this;
                            }

                            throw;
                        }
                    }
                }
            }

            // If we got here, we had invalid input
            Console.WriteLine("Invalid input");
            return this;
        }
    }
}
