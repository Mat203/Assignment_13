
static void Main(string args)
{
    string text = File.ReadAllText(args);
    Dictionary<char, int> charFrequencies = new Dictionary<char, int>();

    foreach (char c in text)
    {
        if (charFrequencies.ContainsKey(c))
        {
            charFrequencies[c]++;
        }
        else
        {
            charFrequencies[c] = 1;
        }
    }

    foreach (KeyValuePair<char, int> kvp in charFrequencies)
    {
        Console.WriteLine("{0}: {1}", kvp.Key, kvp.Value);
    }
}


