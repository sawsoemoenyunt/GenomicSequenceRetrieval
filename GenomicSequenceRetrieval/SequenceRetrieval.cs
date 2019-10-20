using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenomicSequenceRetrieval
{

    ///<summary>
    ///    Enumeration list for different levels of search
    ///</summary>
    public enum FlagLevel
    {
        Level1 = 1,
        Level2 = 2,
        Level3 = 3,
        Level4 = 4,
        Level5 = 5,
        Level6 = 6,
        Level7 = 7
    }

    ///<summary>
    ///    The main Validator class
    ///</summary>
    public class SequenceRetrieval
    {
        private string[] args;
        private string programName, fileName;
        private FlagLevel level;

        public SequenceRetrieval(string[] args)
        {
            this.args = args;
        }

        /*
        start validating the command arguments
             */
        ///<summary>
        ///    start validating the command arguments
        ///</summary>
        public void StartValidate()
        {
            if (args.Length > 2)
            {
                if (ValidateProgram() && ValidateLevel() && ValidateFileName())
                {
                    InitLevel();
                }
            }
            else
            {
                Console.WriteLine("Pleae enter a valid command.");
            }
        }

        ///<summary>
        ///    Validate the program name
        ///</summary>
        ///<remark>
        ///<returns>
        ///if validate pass return true otherwise false
        ///</returns>
        ///</remark>
        private bool ValidateProgram()
        {
            if (args[0].Equals("Search16s"))
            {
                this.programName = args[0];
                return true;
            }
            else
            {
                ShowError("Please enter correct name for program.");
                return false;
            }
        }

        ///<summary>
        ///    Validate the flag level
        ///</summary>
        ///<remark>
        ///<returns>
        ///if validate pass return true otherwise false
        ///</returns>
        ///</remark>
        private bool ValidateLevel()
        {
            bool result = true;

            if (args[1].Equals("-level1"))
            {
                this.level = FlagLevel.Level1;
            }
            else if (args[1].Equals("-level2"))
            {
                this.level = FlagLevel.Level2;
            }
            else if (args[1].Equals("-level3"))
            {
                this.level = FlagLevel.Level3;
            }
            else if (args[1].Equals("-level4"))
            {
                this.level = FlagLevel.Level4;
            }
            else if (args[1].Equals("-level5"))
            {
                this.level = FlagLevel.Level5;
            }
            else if (args[1].Equals("-level6"))
            {
                this.level = FlagLevel.Level6;
            }
            else if (args[1].Equals("-level7"))
            {
                this.level = FlagLevel.Level7;
            }
            else
            {
                ShowError("Please enter valid level.");
                result = false;
            }
            return result;
        }

        ///<summary>
        ///    Validate the file name
        ///</summary>
        ///<remark>
        ///<returns>
        ///if validate pass return true otherwise false
        ///</returns>
        ///</remark>
        private bool ValidateFileName()
        {
            this.fileName = args[2];

            if (fileName.Contains(".fasta"))
            {

                return true;
            }
            else
            {
                ShowError("An error occured. Wrong file format.");
                return false;
            }
        }

        private void InitLevel()
        {
            switch (this.level)
            {
                case FlagLevel.Level1:
                    ///level 1
                    Level1Search();
                    break;

                case FlagLevel.Level2:
                    ///level 2
                    Level2Search();
                    break;

                case FlagLevel.Level3:
                    ///level 3
                    Level3Search();
                    break;

                case FlagLevel.Level4:
                    ///level 4
                    Level4Search();
                    break;

                case FlagLevel.Level5:
                    ///level 5
                    Level5Search();
                    break;

                case FlagLevel.Level6:
                    ///level 6
                    Level6Search();
                    break;
                case FlagLevel.Level7:
                    ///level 7
                    Level7Search();
                    break;

                default:
                    Console.WriteLine("Level Not Found!");
                    break;
            }
        }

        ///<summary>
        ///Create level1 search after that start searching and showing resutls
        ///</summary>
        private void Level1Search()
        {
            if (args.Length == 5)
            {
                int startIndex, totalRow;
                bool isStartInt, isTotalInt;
                try
                {
                    isStartInt = int.TryParse(args[3], out startIndex);
                    if (isStartInt)
                    {
                        isTotalInt = int.TryParse(args[4], out totalRow);
                        if (isTotalInt)
                        {
                            SearchLevel1 search1 = new SearchLevel1(programName, level, fileName, startIndex, totalRow);
                            search1.StartSearching();
                            search1.ShowResult();
                        }
                        else
                        {
                            ShowError("Row count must be integer.");
                        }
                    }
                    else
                    {
                        ShowError("Start row number must be integer.");
                    }
                }
                catch (Exception ex)
                {
                    ShowError(ex.Message + "\nPlease enter valid command.");
                }
            }
            else
            {
                ShowError("-level1 required 5 valid arguments.");
            }
        }

        ///<summary>
        ///Create level2 search after that start searching and showing resutls
        ///</summary>
        private void Level2Search()
        {
            if (args.Length == 4)
            {
                string searchId = args[3];
                try
                {
                    SearchLevel2 search2 = new SearchLevel2(programName, level, fileName, searchId);
                    search2.StartSearching();
                    search2.ShowResult();
                }
                catch (Exception ex)
                {
                    ShowError(ex.Message);
                }
            }
            else
            {
                ShowError("-level2 required 4 valid arguments.");
            }
        }

        ///<summary>
        ///Create level3 search after that start searching and showing resutls
        ///</summary>
        private void Level3Search()
        {
            if (args.Length == 5)
            {
                string queryFileName, resultFileName;
                try
                {
                    queryFileName = args[3];
                    resultFileName = args[4];

                    if (queryFileName.Contains(".txt") && resultFileName.Contains(".txt"))
                    {
                        try
                        {
                            SearchLevel3 search3 = new SearchLevel3(programName, level, fileName, queryFileName, resultFileName);
                            search3.StartSearching();
                            search3.ShowResult();
                        }
                        catch (Exception ex)
                        {
                            ShowError(ex.Message);
                        }
                    }
                    else
                    {
                        ShowError("An error occured. Wrong file format.");
                    }
                }
                catch (Exception ex)
                {
                    ShowError(ex.Message + "\nPlease enter a valid command.");
                }
            }
            else
            {
                ShowError("-level3 required 5 valid arguments.");
            }
        }

        ///<summary>
        ///Create level5 search after that start searching and showing resutls
        ///</summary>
        private void Level4Search()
        {
            if (args.Length == 6)
            {
                string indexFileName, queryFileName, resultFileName;
                try
                {
                    indexFileName = args[3];
                    queryFileName = args[4];
                    resultFileName = args[5];

                    if (indexFileName.Contains(".index") && queryFileName.Contains(".txt") && resultFileName.Contains(".txt"))
                    {
                        try
                        {
                            SearchLevel4 search4 = new SearchLevel4(programName, level, fileName, indexFileName, queryFileName, resultFileName);
                            search4.StartSearching();
                            search4.ShowResult();
                        }
                        catch (Exception ex)
                        {
                            ShowError(ex.Message);
                        }
                    }
                    else
                    {
                        ShowError("An error occured. Wrong file format.");
                    }
                }
                catch (Exception ex)
                {
                    ShowError(ex.Message + "\nPlease enter a valid command.");
                }
            }
            else
            {
                ShowError("-level4 required 6 valid arguments.");
            }
        }

        ///<summary>
        ///Create level5 search after that start searching and showing resutls
        ///</summary>
        private void Level5Search()
        {
            if (args.Length == 4)
            {
                string dna = args[3];
                try
                {
                    SearchLevel5 search2 = new SearchLevel5(programName, level, fileName, dna);
                    search2.StartSearching();
                    search2.ShowResult();
                }
                catch (Exception ex)
                {
                    ShowError(ex.Message);
                }
            }
            else
            {
                ShowError("-level5 required 4 valid arguments.");
            }
        }

        ///<summary>
        ///Create level6 search after that start searching and showing resutls
        ///</summary>
        private void Level6Search()
        {
            if (args.Length == 4)
            {
                string metaData = args[3];
                try
                {
                    SearchLevel6 search2 = new SearchLevel6(programName, level, fileName, metaData);
                    search2.StartSearching();
                    search2.ShowResult();
                }
                catch (Exception ex)
                {
                    ShowError(ex.Message);
                }
            }
            else
            {
                ShowError("-level6 required 4 valid arguments.");
            }
        }

        ///<summary>
        ///Create level7 search after that start searching and showing resutls
        ///</summary>
        private void Level7Search()
        {
            if (args.Length == 4)
            {
                string regexPattern = args[3];
                try
                {
                    SearchLevel7 search = new SearchLevel7(programName, level, fileName, regexPattern);
                    search.StartSearching();
                    search.ShowResult();
                }
                catch (Exception ex)
                {
                    ShowError(ex.Message);
                }
            }
            else
            {
                ShowError("-level6 required 4 valid arguments.");
            }
        }

        private void ShowError(string errorText)
        {
            if (errorText.Equals(""))
            {
                Console.WriteLine("An error occured. Please enter a valid command");
            }
            else
            {
                Console.WriteLine(errorText);
            }
        }
    }
}
