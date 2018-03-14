using System.Text.RegularExpressions;

namespace WcfService
{
    public static class CSSNDataMatching
    {
        private static int calculateEditDistance(string string1, string string2)
        {
            if (string1 == string2)
                return 0;
            int[,] matrix = new int[string1.Length + 1, string2.Length + 1];
            for (int i = 0; i <= string1.Length; i++)
                // delete
                matrix[i, 0] = i;
            for (int j = 0; j <= string2.Length; j++)
                // insert
                matrix[0, j] = j;
            for (int i = 0; i < string1.Length; i++)
            {
                for (int j = 0; j < string2.Length; j++)
                {
                    if (string1[i] == string2[j])
                        matrix[i + 1, j + 1] = matrix[i, j];
                    else
                    {
                        // deletion or insertion
                        matrix[i + 1, j + 1] = System.Math.Min(matrix[i, j + 1] + 1, matrix[i + 1, j] + 1);

                        // substitution
                        matrix[i + 1, j + 1] = System.Math.Min(matrix[i + 1, j + 1], matrix[i, j] + 1);
                    }
                }
            }
            return matrix[string1.Length, string2.Length];
        }
        public static int calculateMatch(string string1, string string2)
        {
            string1 = string1.ToLower();
            string2 = string2.ToLower();

            double distance = calculateEditDistance(string1, string2);
            if (distance == 0.0f)
                return (int)(1.0f * 100);
            double longestStringSize = System.Math.Max(string1.Length, string2.Length);
            double percent = distance / longestStringSize;
            return (int)((1.0f - percent) * 100);
        }

        public static string FormatString(string str)
        {
            Regex rgx = new Regex("[^a-zA-Z0-9]");
            str = rgx.Replace(str, ""); // returns only alphanumeric string 
            return str;
        }
    }
}
