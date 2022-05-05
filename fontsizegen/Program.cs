using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace fontsizegen
{
    // simple throw away program to generate font-size.css stylesheet
    public class Program
    {
        public static void Main(string[] args)
        {
            const int maxPx = 256;
            Dictionary<string, int> breakpoints = new Dictionary<string, int>
            {
                //{ "", 0 },
                { "sm", 576 },
                { "md", 768 },
                { "lg", 992 },
                { "xl", 1200 },
                { "xxl", 1400 },
            };

            // this block covers xs styles since no media querys are required
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i <= maxPx; i++)
            {
                int minWidth = i;
                sb.AppendLine($".fs-{minWidth}px\n{{\n\tfont-size: {minWidth}px;\n}}\n");
            }

            foreach (var kv in breakpoints)
            {
                int minWidth = kv.Value;
                sb.AppendLine($"@media (min-width: {minWidth}px)\n{{\t");
                for (int i = 1; i <= maxPx; i++)
                {   
                    sb.AppendLine($"\t.fs-{kv.Key}-{i}px\n\t{{\n\t\tfont-size: {i}px;\n\t}}\n");
                }
                sb.AppendLine($"}}\n");
            }
            File.WriteAllText("../../../../font-size.css", sb.ToString());
        }
    }
}
