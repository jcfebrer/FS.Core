using Microsoft.VisualStudio.TestTools.UnitTesting;
using FSFuzzyStrings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FSFuzzyStrings.Tests
{
    [TestClass()]
    public class StringExtensionsTests
    {
        [TestMethod()]
        public void SoundExTest()
        {
            Assert.AreEqual("A416", SoundEx.Do("A & Laboratorios de Biotecnologia"), "Fonetización incorrecta. SoundEx");
            Assert.AreEqual("H260", SoundEx.Do("higuera"), "Fonetización incorrecta. SoundEx");
            Assert.AreEqual("O365", SoundEx.Do("Otorrinogoláringologo"), "Fonetización incorrecta. SoundEx");
        }

        [TestMethod()]
        public void SoundEx2Test()
        {
            Assert.AreEqual("A416", SoundEx.SoundEx2("A & Laboratorios de Biotecnologia"), "Fonetización incorrecta. SoundEx");
            Assert.AreEqual("H260", SoundEx.SoundEx2("higuera"), "Fonetización incorrecta. SoundEx");
            Assert.AreEqual("O365", SoundEx.SoundEx2("Otorrinogoláringologo"), "Fonetización incorrecta. SoundEx");
        }

        [TestMethod()]
        public void SoundExEspTest()
        {
            Assert.AreEqual("A416", SoundExEsp.Do("A & Laboratorios de Biotecnologia"), "Fonetización incorrecta. SoundEx");
            Assert.AreEqual("I260", SoundExEsp.Do("higuera"), "Fonetización incorrecta. SoundExEsp");
            Assert.AreEqual("P610", SoundExEsp.Do("prueba"), "Fonetización incorrecta. SoundExEsp");
            Assert.AreEqual("O365", SoundExEsp.Do("Otorrinogoláringologo"), "Fonetización incorrecta. SoundExEsp");
            Assert.AreEqual("K523", SoundExEsp.Do("como estás hola esta es una prueba más compleja"), "Fonetización incorrecta. SoundExEsp");
            Assert.AreEqual("O400", SoundExEsp.Do("hola"), "Fonetización incorrecta. SoundExEsp");
        }

        [TestMethod()]
        public void IssueTest()
        {
            Console.WriteLine("test".FuzzyMatch("w"));
            Console.WriteLine("test".FuzzyMatch("W"));
            Console.WriteLine("test".FuzzyMatch("w "));
            Console.WriteLine("test".FuzzyMatch("W "));
            Console.WriteLine("test".FuzzyMatch(" w"));
            Console.WriteLine("test".FuzzyMatch(" W"));
            Console.WriteLine("test".FuzzyMatch(" w "));
            Console.WriteLine("test".FuzzyMatch(" W "));
        }

        [TestMethod()]
        public void NameMatching()
        {
            //name matching
            string input = "Jensn";
            string[] surnames = new string[] {
                "Adams",
                "Benson",
                "Geralds",
                "Johannson",
                "Johnson",
                "Jensen",
                "Jordon",
                "Madsen",
                "Stratford",
                "Wilkins"
            };

            Console.WriteLine("Dice Coefficient for Jensn:");
            foreach (var name in surnames)
            {
                double dice = DiceCoefficientExtensions.DiceCoefficient(input, name);
                Console.WriteLine("\t{0} against {1}", dice.ToString("###,###.00000"), name);
            }

            Console.WriteLine();
            Console.WriteLine("Levenshtein Edit Distance for Jensn:");
            foreach (var name in surnames)
            {
                int leven = LevenshteinDistanceExtensions.LevenshteinDistance(input, name);
                Console.WriteLine("\t{0} against {1}", leven, name);
            }

            Console.WriteLine();
            Console.WriteLine("Longest Common Subsequence for Jensn:");
            foreach (var name in surnames)
            {
                var lcs = input.LongestCommonSubsequence(name);
                Console.WriteLine("\t{0}, {1} against {2}", lcs.Item2.ToString("###,###.00000"), lcs.Item1, name);
            }

            Console.WriteLine();
            string mp = DoubleMetaphoneExtensions.ToDoubleMetaphone(input);
            Console.WriteLine("Double Metaphone for Jensn: {0}", mp);
            foreach (var name in surnames)
            {
                string nameMp = DoubleMetaphoneExtensions.ToDoubleMetaphone(name);
                Console.WriteLine("\t{0} metaphone for {1}", nameMp, name);
            }

            Console.WriteLine();
            Console.WriteLine("FuzzyEquals and FuzzyMatch for Jensn: {0}", mp);
            foreach (var name in surnames)
            {
                bool isEqual = input.FuzzyEquals(name);
                double coefficient = input.FuzzyMatch(name);
                Console.WriteLine("\tFuzzyEquals is {0}, FuzzyMatch {1} against {2}", isEqual, coefficient, name);
            }
        }

        [TestMethod()]
        public void AddressMatching()
        {
            //name matching
            string input = "2130 South Fort Union Blvd.";
            string[] surnames = new string[] {
                "2689 East Milkin Ave.",
                "85 Morrison",
                "2350 North Main",
                "567 West Center Street",
                "2130 Fort Union Boulevard",
                "2310 S. Ft. Union Blvd.",
                "98 West Fort Union",
                "Rural Route 2 Box 29",
                "PO Box 3487",
                "3 Harvard Square"
            };

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Dice Coefficient for 2130 South Fort Union Blvd.:");
            foreach (var name in surnames)
            {
                double dice = DiceCoefficientExtensions.DiceCoefficient(input, name);
                Console.WriteLine("\t{0} against {1}", dice.ToString("###,###.00000"), name);
            }

            Console.WriteLine();
            Console.WriteLine("Levenshtein Edit Distance for 2130 South Fort Union Blvd.:");
            foreach (var name in surnames)
            {
                int leven = LevenshteinDistanceExtensions.LevenshteinDistance(input, name);
                Console.WriteLine("\t{0} against {1}", leven, name);
            }

            Console.WriteLine();
            Console.WriteLine("Longest Common Subsequence for 2130 South Fort Union Blvd.:");
            foreach (var name in surnames)
            {
                var lcs = input.LongestCommonSubsequence(name);
                Console.WriteLine("\t{0}, {1} against {2}", lcs.Item2.ToString("###,###.00000"), lcs.Item1, name);
            }

            Console.WriteLine();
            string mp = DoubleMetaphoneExtensions.ToDoubleMetaphone(input);
            Console.WriteLine("Double Metaphone for 2130 South Fort Union Blvd.: {0}", mp);
            foreach (var name in surnames)
            {
                string nameMp = DoubleMetaphoneExtensions.ToDoubleMetaphone(name);
                Console.WriteLine("\t{0} metaphone for {1}", nameMp, name);
            }

            Console.WriteLine();
            Console.WriteLine("FuzzyEquals and FuzzyMatch for 2130 South Fort Union Blvd.: {0}", mp);
            foreach (var name in surnames)
            {
                bool isEqual = input.FuzzyEquals(name);
                double coefficient = input.FuzzyMatch(name);
                Console.WriteLine("\tFuzzyEquals is {0}, FuzzyMatch {1} against {2}", isEqual, coefficient, name);
            }
        }
    }
}