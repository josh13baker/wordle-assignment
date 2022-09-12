//============================================================================
// Tool for determining all the words in the supplied dictionary that satisfy 
//       the observed data provided by Wordle.
//============================================================================
using System;
using System.IO;

class Program
{
    static void Main()
    {
        Console.WriteLine("Wordle Tool");
        //string dictName = "testDict.txt";
        string dictName = "wordle dict.txt";

        // These letters that we know do NOT belong in the solution
        string badLetters = "niu";

        // These are letters that do appear in the solution, but in
        // different positon than the location selected. The five 
        // elements of the array correspond to the letter positions.
        // Each element of the array is a string.
        string[] goodishLetters = new string[] {"","ec","","","e"};
        
        // these are correct letters in the correct location
        string goodLetters = "---s-";

        List<string> solns;
        solns = ReadDict(dictName);

        List<string> matches;
        matches = FindCompatibleWords(badLetters,goodishLetters,goodLetters,solns);
        Console.WriteLine("Number of matches: " + matches.Count() + "/n");
        foreach(string w in matches)
        {
            Console.WriteLine(w);
        }
    }

    //------------------------------------------------------------------------
    // FindCompatibleWords: Find all words in the list of possible solutions
    //       in the dictionary that are compatible with the observed data. 
    //       Returns a list of words that are compatible.
    //------------------------------------------------------------------------
    static List<string> FindCompatibleWords(string bad,string[] goodish, string good,
        List<string> posibles)
    {
        List<string> result = new List<string>();

        if(good.Length != 5)
        {
            Console.WriteLine("ERROR: String of GOOD letters doesn't have " +
                "length of 5.");
            return result;
        }
        if(goodish.Count() != 5)
        {
            Console.WriteLine("ERROR: GOODISH array doesn't have 5 elements");
            return result;
        }

        foreach(string w in posibles)
        {
            if(CheckWord(bad,goodish,good,w))
            {
                //Console.WriteLine(w);
                result.Add(w);
            }
        }
        return result;
    }

    //------------------------------------------------------------------------
    // CheckWord: Check if a single word is compatible with the observed data.
    //      Returns true if so. False otherwise.
    //------------------------------------------------------------------------
    static bool CheckWord(string bad,string[] goodish, 
        string good, string word)
    {
        bool result = true;

        // Check if any of the "bad" letters are in the word
        foreach(char c in bad)
        {
            if(word.Contains(c))
                return false;
        }

        // Check that good letters are in the right place
        for(int i = 0; i<5; ++i)
        {
            char c = good.ElementAt(i);
            if(Char.IsLetter(c)){
                if(c != word.ElementAt(i))
                    return false;
            }
        }

        //*************************************
        // Next, we need to check the "goodish" letters. I will let you write
        // this part.
        int counter = 0;
        foreach(string c in goodish)
        {
            for(int j=0; j<c.Length; ++j)
            {
                char v = c.ElementAt(j);
                if(Char.IsLetter(v))
                {
                    if(v == word.ElementAt(counter))
                    {
                        return false;
                    }
                }
            }
            counter ++;
        }
        return result;
    }

    //------------------------------------------------------------------------
    // ReadDict: Read the dictionary with supplied name and produce a list of
    //       five letter words in the form of a list.
    //------------------------------------------------------------------------
    static List<string> ReadDict(string dName)
    {
        List<string> result = new List<string>();

        if(!File.Exists(dName))
        {
            Console.WriteLine("Dictionary file " + dName +
                " does not exist.");
            return result;
        }

        StreamReader reader = new StreamReader(dName);
        var line = "";
        //string line;

        while((line = reader.ReadLine()) != null)
        {
            if(line.Trim().Length == 5)
            {
                //Console.WriteLine(line.ToLower());
                result.Add(line.ToLower());
            }
        }

        return result;
    }
}