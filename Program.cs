static void Main()
{
    Dictionary<char, int> charDict = CountFrequency();
    Node root = BuildHuffmanTree(charDict);
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


Main();

class Node
{
    public char? Symbol { get; set; }
    public int Frequency { get; set; }
    public Node LeftChild { get; set; }
    public Node RightChild { get; set; }
}

