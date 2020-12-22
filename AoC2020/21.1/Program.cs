using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _21._1
{
    class Word
    {
        public string word;
        public List<string> PossibleAllergens;
        public bool ignore;
    }


    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadLines("in.txt");

            List<Word> wordlist = new List<Word>();

            foreach (var item in lines)
            {
                var split1 = item.Split("(");

                var words = split1[0].Trim().Split(" ").Select(f => f.Trim());
                var allergens = split1[1].Substring(8).Split(",").Select(f => f.Trim(' ', ')'));

                foreach (var w in words)
                {
                    var newWord = new Word() { word = w };
                    newWord.PossibleAllergens = new List<string>(allergens);
                    wordlist.Add(newWord);
                }
            }


            int t = 0;

            for (int i = 0; i < wordlist.Count; i++)
            {
                var x = wordlist.Except(new List<Word>() { wordlist[i] })
                    .Where(
                        (
                        f => f.word == wordlist[i].word && 
                        f.PossibleAllergens.Intersect(wordlist[i].PossibleAllergens).Count() > 0)
                        ).Count();

                var y = wordlist.Except(new List<Word>() { wordlist[i] })
                      .Where(
                          f => f.word == wordlist[i].word
                          )
                          .Count();


                if (x == 0 || y == 0)
                {
                    t++;
                }
            }

            Console.WriteLine(t);
            Console.ReadKey();
        }
    }
}
