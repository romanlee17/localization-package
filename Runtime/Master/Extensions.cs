using System;

namespace romanlee17.Localization.Master {
    internal static class Extensions {
        public static bool IsSimilarTo(this string stringA, string stringB) {
            return SimilarityScore(stringA, stringB) >= 0.5f;
        }
        public static float SimilarityScore(this string stringA, string stringB) {
            int len1 = stringA.Length;
            int len2 = stringB.Length;
            var matrix = new int[len1 + 1, len2 + 1];
            // Initialize the first row and column.
            for (int i = 0; i <= len1; i++) {
                matrix[i, 0] = i;
            }
            for (int j = 0; j <= len2; j++) {
                matrix[0, j] = j;
            }
            // Fill the rest of the matrix.
            for (int i = 1; i <= len1; i++) {
                for (int j = 1; j <= len2; j++) {
                    int cost = (stringB[j - 1] == stringA[i - 1]) ? 0 : 1;
                    matrix[i, j] = Math.Min(
                        Math.Min(matrix[i - 1, j] + 1, matrix[i, j - 1] + 1),
                        matrix[i - 1, j - 1] + cost);
                }
            }
            // The similarity score is inversely proportional to the Levenshtein distance.
            float score = 1.0f - (float)matrix[len1, len2] / Math.Max(len1, len2);
            // Consider strings as similar if the score is greater than or equal to 0.5.
            return score;
        }
    }
}