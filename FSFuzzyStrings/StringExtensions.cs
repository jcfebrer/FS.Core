﻿#if NET40_OR_GREATER || NETCOREAPP

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FSFuzzyStrings
{
    public static class StringExtensions
    {
        public static bool FuzzyEquals(this string strA, string strB, double requiredProbabilityScore = 0.75)
        {
            return strA.FuzzyMatch(strB) > requiredProbabilityScore;
        }

        public static double FuzzyMatch(this string strA, string strB)
        {
            string localA = Strip(strA.Trim().ToLower());
            string localB = Strip(strB.Trim().ToLower());
            if (localA.Contains(space) && localB.Contains(space))
            {
                var partsA = localA.Split(' ');
                var partsB = localB.Split(' ');
                var weightedHighCoefficients = new double[partsA.Length];
                var distanceRatios = new double[partsA.Length];
                for (int i = 0; i < partsA.Length; i++)
                {
                    double high = 0.0;
                    int indexDistance = 0;
                    for (int x = 0; x < partsB.Length; x++)
                    {
                        var coef = CompositeCoefficient(partsA[i], partsB[x]);
                        if (coef > high)
                        {
                            high = coef;
                            indexDistance = Math.Abs(i - x);
                        }
                    }
                    double distanceWeight = indexDistance == 0 ? 1.0 : 1.0 - (indexDistance / (partsA.Length * 1.0));
                    weightedHighCoefficients[i] = high * distanceWeight;
                }
                double avgWeightedHighCoefficient = weightedHighCoefficients.Sum() / (partsA.Length * 1.0);
                return avgWeightedHighCoefficient < 0.999999 ? avgWeightedHighCoefficient : 0.999999; //fudge factor
            }
            else
            {
                var singleComposite = CompositeCoefficient(localA, localB);
                return singleComposite < 0.999999 ? singleComposite : 0.999999; //fudge factor
            }
        }

        private static readonly string space = " ";
        private static Regex stripRegex = new Regex(@"[^a-zA-Z0-9 -]*");

        private static string Strip(string str)
        {
            return stripRegex.Replace(str, string.Empty);
        }

        private static double CompositeCoefficient(string strA, string strB)
        {
            double dice = DiceCoefficientExtensions.DiceCoefficient(strA, strB);
            var lcs = strA.LongestCommonSubsequence(strB);
            int leven = LevenshteinDistanceExtensions.LevenshteinDistance(strA, strB);
            double levenCoefficient = 1.0 / (1.0 * (leven + 0.2)); //may want to tweak offset
            string strAMp = DoubleMetaphoneExtensions.ToDoubleMetaphone(strA);
            string strBMp = DoubleMetaphoneExtensions.ToDoubleMetaphone(strB);
            int matchCount = 0;
            if (strAMp.Length == 4 && strBMp.Length == 4)
            {
                for (int i = 0; i < strAMp.Length; i++)
                {
                    if (strAMp[i] == strBMp[i]) matchCount++;
                }
            }
            double mpCoefficient = matchCount == 0 ? 0.0 : matchCount / 4.0;
            double avgCoefficent = (dice + lcs.Item2 + levenCoefficient + mpCoefficient) / 4.0;
            return avgCoefficent;
        }
    }
}

#endif