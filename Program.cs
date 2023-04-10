static void Main()
{
    Dictionary<char, int> charDict = CountFrequency();
    Node root = BuildHuffmanTree(charDict);

    Dictionary<char, string> encodingTable = new Dictionary<char, string>();
    Encoding(root, "", encodingTable);

    Console.WriteLine("Symbol\tCode");
    foreach (KeyValuePair<char, string> kvp in encodingTable)
    {
        Console.WriteLine($"{kvp.Key}\t{kvp.Value}");
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

Main();

public class Node
{
    public char? Symbol { get; set; }
    public int Frequency { get; set; }
    public Node LeftChild { get; set; }
    public Node RightChild { get; set; }
}

