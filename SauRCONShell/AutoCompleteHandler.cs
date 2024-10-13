namespace SauRCONShell
{
    class AutoCompletionHandler : IAutoCompleteHandler
    {
        // characters to start completion from
        public char[] Separators { get; set; } = new char[] { ' ', '.', '/' };

        public string[] autos = Properties.Resources.autocomplete.Split(new char[] { (char)'\r' }, StringSplitOptions.TrimEntries);

        public AutoCompletionHandler()
        {
        }

        // text - The current text entered in the console
        // index - The index of the terminal cursor within {text}
        public string[] GetSuggestions(string text, int index)
        {
            var list = autos.Where(c => c.StartsWith(text)).ToArray();
            if (text.Split([' ']).Length > 1)
            {
                var lastSpace = text.LastIndexOf(' ');
                var cutText = text.Substring(0, ++lastSpace);
                for (int i = 0; i < list.Length; i++)
                {
                    list[i] = list[i].Replace(cutText, string.Empty);
                }
            }
            return list;
/*
            if (text.StartsWith("git "))
                return new string[] { "init", "clone", "pull", "push" };
            else
                return null;
*/
        }
    }
}
