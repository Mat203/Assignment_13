static void Main()
{
    Dictionary<char, int> charDict = CountFrequency();
    Node root = BuildHuffmanTree(charDict);

    Dictionary<char, string> encodingTable = new Dictionary<char, string>();
    Encoding(root, "", encodingTable);

    Console.WriteLine("Symbol   Code");
    foreach (KeyValuePair<char, string> kvp in encodingTable)
    {
        Console.WriteLine($"{kvp.Key}:{kvp.Value}");
    }
    Console.WriteLine("000000000000000000000000000000000000000");

    EncodeFile(encodingTable);
    DecodeFile(root);
    WriteEncodingTableToFile(encodingTable);
    Dictionary<char, string> encodingTable1 = ReadEncodingTableFromFile();
    foreach (KeyValuePair<char, string> kvp in encodingTable1)
    {
        Console.WriteLine($"{kvp.Key}:{kvp.Value}");
    }
}

static Dictionary<char, int> CountFrequency()
{
    string text = File.ReadAllText("text.txt");
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

    return charFrequencies;
}

static Node BuildHuffmanTree(Dictionary<char, int> charFrequencies)
{
    var queue = new PriorityQueue<Node, int>();

    foreach (KeyValuePair<char, int> kvp in charFrequencies)
    {
        queue.Enqueue(new Node { Symbol = kvp.Key, Frequency = kvp.Value }, kvp.Value);
    }

    while (queue.Count > 1)
    {
        Node left = queue.Dequeue();
        Node right = queue.Dequeue();

        Node parent = new Node { Frequency = left.Frequency + right.Frequency };
        parent.LeftChild = left;
        parent.RightChild = right;

        queue.Enqueue(parent, parent.Frequency);
    }

    return queue.Dequeue();
}

static void Encoding(Node node, string code, Dictionary<char, string> encodingTable)
{
    if (node.Symbol != null)
    {
        encodingTable[(char)node.Symbol] = code;
    }
    else
    {
        Encoding(node.LeftChild, code + "0", encodingTable);
        Encoding(node.RightChild, code + "1", encodingTable);
    }
}

static void EncodeFile(Dictionary<char, string> encodingTable)
{
    string text = File.ReadAllText("text.txt");
    List<string> encodedText = new List<string>();
    foreach (char c in text)
    {
        encodedText.Add(encodingTable[c]);
    }
    string encodedString = string.Join("", encodedText);
    File.AppendAllText("etext.txt", encodedString);
}

static void DecodeFile(Node root)
{
    string encodedString = File.ReadAllText("etext.txt");
    List<char> decodedText = new List<char>();

    Node currentNode = root;
    foreach (char bit in encodedString)
    {
        if (bit == '0')
        {
            currentNode = currentNode.LeftChild;
        }
        else if (bit == '1')
        {
            currentNode = currentNode.RightChild;
        }

        if (currentNode.Symbol != null)
        {
            decodedText.Add((char)currentNode.Symbol);
            currentNode = root;
        }
    }
    string decodedString = new string(decodedText.ToArray());
    File.WriteAllText("dtext.txt", decodedString);
}
static void WriteEncodingTableToFile(Dictionary<char, string> encodingTable)
{
    using (StreamWriter writer = new StreamWriter("t.txt"))
    {
        foreach (KeyValuePair<char, string> kvp in encodingTable)
        {
            writer.WriteLine($"{kvp.Key}:{kvp.Value}");
        }
    }
}

static Dictionary<char, string> ReadEncodingTableFromFile()
{
    Dictionary<char, string> encodingTable = new Dictionary<char, string>();

    using (StreamReader reader = new StreamReader("t.txt"))
    {
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            line = line.Trim();
            if (line.Length == 0) continue;
            
            string[] parts = line.Split(':');
            if (parts.Length != 2)
            {
                continue;
            }
            if (parts[0] == "") 
            {
                char key = ':';
                string value = parts[1].Trim();
                encodingTable[key] = value;
            }
            else
            {
                char key = parts[0][0];
                string value = parts[1].Trim();
                encodingTable[key] = value;
            }
            
        }
    }

    return encodingTable;
}

Main();

public class Node
{
    public char? Symbol { get; set; }
    public int Frequency { get; set; }
    public Node LeftChild { get; set; }
    public Node RightChild { get; set; }
}



