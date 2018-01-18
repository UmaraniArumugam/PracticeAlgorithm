using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LongestCompoundWord
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string[] wordsList = File.ReadAllLines(@"c:\Test\wordlist.txt");
            var words = FindLongestWords(wordsList);
            var dict = new HashSet<String>(words);
            Dictionary<string, int> sortedList = new Dictionary<string, int>();
           
            foreach (string word in words)
            {
                int count = 0;
                if (IsCompoundWord(word, dict, ref count))
                    sortedList.Add(word, count);
            }
            var result = sortedList.OrderByDescending(p=>p.Key.Length).GroupBy(p=>p.Key.Length).Take(2);
            foreach (var group in result)
            {
                foreach(var longword in group)
                {
                    Console.WriteLine("Word : " + longword.Key + " Count : " + longword.Value);
                }
            }
            Console.ReadLine();

        }

        public static string[] FindLongestWords(IEnumerable<string> listOfWords)
        {
            if (listOfWords == null) throw new ArgumentException("listOfWords");
            var sortedWords = listOfWords.OrderByDescending(word => word.Length);

            return sortedWords.ToArray();
        }
        private static bool IsCompoundWord(string word, HashSet<string> dict, ref int count)
        {
            if (String.IsNullOrEmpty(word)) { count = 0; return false; }
            if (word.Length == 1)
            {
                count = 1;
                return dict.Contains(word);
            }
            var pairs = generateWordPairs(word);

            foreach (var pair in pairs.Where(pair => dict.Contains(pair.Key)))
            {
                count += 1;
                if (dict.Contains(pair.Value)) count += 1;
                return dict.Contains(pair.Value) || IsCompoundWord(pair.Value, dict, ref count);

            }
            return false;
        }
        private static Dictionary<string, string> generateWordPairs(string word)
        {
           
                Dictionary<string, string> pairsDict = new Dictionary<string, string>();
                for (int i = 1; i < word.Length; i++)
                {
                    pairsDict.Add(word.Substring(0, i), word.Substring(i));
                }
                return pairsDict;
            

        }
    }
}
