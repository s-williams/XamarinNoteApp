using System;
using System.Collections.Generic;
using System.Text;

namespace CodedUINav
{
    class Common
    {
        private const int maxTitleLength = 20;

        public static String createTitle(String text)
        {
            String title = text;

            if (text.Length < maxTitleLength)
            {
                if (text.Contains(Environment.NewLine))
                {
                    String[] parts = text.Split(
                        new[] { Environment.NewLine },
                        StringSplitOptions.None
                    );
                    title = parts[0];
                } else
                {
                    title = text;
                }
            } else
            {
                if (text.Substring(0, maxTitleLength).Contains(Environment.NewLine))
                {
                    String[] parts = text.Split(
                        new[] { Environment.NewLine },
                        StringSplitOptions.None
                    );
                    title = parts[0];
                }
                else
                {
                    title = text.Substring(0, maxTitleLength) + "...";
                }
            }

            return title;
        }
    }
}
