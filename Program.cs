using System;
using System.Collections.Generic;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

Dictionary<string, List<string>> graph = new()
{
    ["Центр"] = ["Парк", "Ринок"],
    ["Парк"] = ["Центр", "Вокзал"],
    ["Ринок"] = ["Центр", "Вокзал", "Стадіон"],
    ["Вокзал"] = ["Парк", "Ринок", "Озеро"],
    ["Стадіон"] = ["Ринок"],
    ["Озеро"] = ["Вокзал"],
    ["Гай"] = ["Міст"],
    ["Міст"] = ["Гай"]
};

HashSet<string> VisitAll(Dictionary<string, List<string>> g, string start)
{
    HashSet<string> visited = [];
    if (!g.ContainsKey(start))
        return visited;

    Queue<string> queue = new();
    visited.Add(start);
    queue.Enqueue(start);

    while (queue.Count > 0)
    {
        string current = queue.Dequeue();
        foreach (string neighbor in g[current])
        {
            if (visited.Add(neighbor))
                queue.Enqueue(neighbor);
        }
    }

    return visited;
}

bool Reachable(Dictionary<string, List<string>> g, string from, string to)
{
    HashSet<string> visited = VisitAll(g, from);
    return visited.Contains(to);
}

List<string> FindPath(Dictionary<string, List<string>> g, string from, string to)
{
    if (!g.ContainsKey(from) || !g.ContainsKey(to))
        return [];

    if (from == to)
        return [from];

    HashSet<string> visited = [from];
    Dictionary<string, string> parent = [];
    Queue<string> queue = new();
    queue.Enqueue(from);

    bool found = false;
    while (queue.Count > 0)
    {
        string current = queue.Dequeue();
        if (current == to)
        {
            found = true;
            break;
        }

        foreach (string neighbor in g[current])
        {
            if (visited.Add(neighbor))
            {
                parent[neighbor] = current;
                queue.Enqueue(neighbor);
            }
        }
    }

    if (!found)
        return [];

    List<string> path = [];
    string? step = to;
    while (step is not null)
    {
        path.Add(step);
        parent.TryGetValue(step, out step);
    }

    path.Reverse();
    return path;
}
