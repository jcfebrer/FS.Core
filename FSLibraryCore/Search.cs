using System;

namespace FSLibraryCore
{
    /// <summary>
    ///     Métodos de búsqueda.
    /// </summary>
    public class Search
    {
        /// <summary>
        /// Búsqueda por similitud.
        /// </summary>
        /// <param name="string1">The string1.</param>
        /// <param name="string2">The string2.</param>
        /// <returns></returns>
        public float GetSimilarity(string string1, string string2)
        {
            float dis = Levenshtein(string1, string2);
            float maxLen = string1.Length;
            if (maxLen < string2.Length)
                maxLen = string2.Length;

            float minLen = string1.Length;
            if (minLen > string2.Length)
                minLen = string2.Length;


            if (maxLen == 0.0F)
                return 1.0F;
            return maxLen - dis;
        }

        /// <summary>
        ///     Compute Levenshtein distance
        /// </summary>
        /// <param name="search1">The string1.</param>
        /// <param name="search2">The string2.</param>
        /// <returns>
        ///     Distance between the two strings.
        ///     The larger the number, the bigger the difference.
        /// </returns>
        private int Levenshtein(string search1, string search2)
        {
            var len1 = search1.Length; //length of search1
            var len2 = search2.Length; //length of search2
            var d = new int[len1 + 1, len2 + 1]; // matrix
            int cost; // cost

            // Step 1
            if (len1 == 0)
                return len2;

            if (len2 == 0)
                return len1;

            // Step 2
            for (var i = 0; i <= len1; d[i, 0] = i++)
                ;
            for (var j = 0; j <= len2; d[0, j] = j++)
                ;

            // Step 3
            for (var i = 1; i <= len1; i++) //Step 4
            for (var j = 1; j <= len2; j++)
            {
                // Step 5
                cost = search2.Substring(j - 1, 1) == search1.Substring(i - 1, 1) ? 0 : 1;

                // Step 6
                d[i, j] = Math.Min(Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                    d[i - 1, j - 1] + cost);
            }

            // Step 7
            return d[len1, len2];
        }
    }
}