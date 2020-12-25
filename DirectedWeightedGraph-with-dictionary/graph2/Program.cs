
// A C# program for Bellman-Ford's single source shortest path 
// algorithm. 
  
using System;
using System.Collections.Generic;
using System.Linq;

// A class to represent a connected, directed and weighted graph 
class Graph
{
    // A class to represent a weighted edge in graph 
    class Edge
    {

        public int src, dest, weight, srcIndex, dstIndex;
        public Edge()
        {
            src = dest = weight = srcIndex = dstIndex = 0;
        }
    };
    public int counter = 0;
    int V, E;
    Edge[] edge;

    // Creates a graph with V vertices and E edges 
    Graph(int days)
    {
        V = (int)Math.Pow(2, days)+1;
        int sum = 0;
        for (int i = 0; i < V; i++)
        {
            sum += i;
        }
        E = sum;
        edge = new Edge[E];
        for (int i = 0; i < E; ++i)
            edge[i] = new Edge();

    }
    Graph(int v, int e)
    {
        V = v;
        E = e;
        edge = new Edge[e];
        for (int i = 0; i < e; ++i)
            edge[i] = new Edge();
    }

    // The main function that finds shortest distances from src 
    // to all other vertices using Bellman-Ford algorithm. The 
    // function also detects negative weight cycle 
    void BellmanFord(Graph graph, int src, Dictionary<int,int> map)
    {
        int V = graph.V, E = graph.E;
        int[] dist = new int[V];

        // Step 1: Initialize distances from src to all other 
        // vertices as INFINITE 
        for (int i = 0; i < V; ++i)
            dist[i] = int.MaxValue;
        dist[src] = 0;

        // Step 2: Relax all edges |V| - 1 times. A simple 
        // shortest path from src to any other vertex can 
        // have at-most |V| - 1 edges 
        for (int i = 1; i < V; ++i)
        {
            for (int j = 0; j < E; ++j)
            {
                int u = map.FirstOrDefault(x => x.Value == graph.edge[j].src).Key;// graph.edge[j].src;
                int v = map.FirstOrDefault(x => x.Value == graph.edge[j].dest).Key;// graph.edge[j].dest;
                int weight = graph.edge[j].weight;
                if (dist[u] != int.MaxValue && dist[u] + weight < dist[v])
                    dist[v] = dist[u] + weight;
            }
        }

        // Step 3: check for negative-weight cycles. The above 
        // step guarantees shortest distances if graph doesn't 
        // contain negative weight cycle. If we get a shorter 
        // path, then there is a cycle. 
        //for (int j = 0; j < E; ++j)
        //{
        //    int u = graph.edge[j].src;
        //    int v = graph.edge[j].dest;
        //    int weight = graph.edge[j].weight;
        //    if (dist[u] != int.MaxValue && dist[u] + weight < dist[v])
        //    {
        //        Console.WriteLine("Graph contains negative weight cycle");
        //        return;
        //    }
        //}
        printArr(dist, V);
    }

    // A utility function used to print the solution 
    void printArr(int[] dist, int V)
    {
        Console.WriteLine("Vertex Distance from Source");
        for (int i = 0; i < V; ++i)
            Console.WriteLine(i + "\t\t" + dist[i]);
    }

    public static void AddInMap(int src, Dictionary<int, int> map, ref int counter)
    {
        if (!map.ContainsValue(src))
        {
            map.Add(counter, src);
            counter++;
        }
    }
    // Driver method to test above function 
    public static void Main()
    {
        int V = 26; // Number of vertices in graph 
        int E = 10; // Number of edges in graph 
        int counter = 0;
        Graph graph = new Graph(4);
        Dictionary<int, int> map = new Dictionary<int, int>();
        List<int> vertexes = new List<int>();

        // add edge 0-1 (or A-B in above figure) 
        graph.edge[0].src = 32354;
        graph.edge[0].dest = 12002;
        graph.edge[0].weight = 0;
        AddInMap(graph.edge[0].src, map, ref counter);
        AddInMap(graph.edge[0].dest, map, ref counter);

        // add edge 0-2 (or A-C in above figure) 
        graph.edge[0].srcIndex = 0;
        graph.edge[0].dstIndex = 0;
        graph.edge[1].src = 32354;
        graph.edge[1].dest = 59646;
        graph.edge[1].weight = 2028;
        AddInMap(graph.edge[1].src, map, ref counter);
        AddInMap(graph.edge[1].dest, map, ref counter);


        // add edge 1-2 (or B-C in above figure) 
        graph.edge[2].dstIndex++;
        graph.edge[0].srcIndex = 0;
        graph.edge[2].src = 59646;
        graph.edge[2].dest = 47679;
        graph.edge[2].weight = -3429;
        AddInMap(graph.edge[2].src, map, ref counter);
        AddInMap(graph.edge[2].dest, map, ref counter);

        // add edge 1-3 (or B-D in above figure) 
        graph.edge[3].dstIndex++;
        graph.edge[0].srcIndex = 0;
        graph.edge[3].src = 59646;
        graph.edge[3].dest = 27325;
        graph.edge[3].weight = 0;
        AddInMap(graph.edge[3].src, map, ref counter);
        AddInMap(graph.edge[3].dest, map, ref counter);

        // add edge 1-4 (or A-E in above figure) 
        graph.edge[4].src = 12002;
        graph.edge[4].dest = 47679;
        graph.edge[4].weight = 6009;
        AddInMap(graph.edge[4].src, map, ref counter);
        AddInMap(graph.edge[4].dest, map, ref counter);

        // add edge 3-2 (or D-C in above figure) 
        graph.edge[5].src = 47679;
        graph.edge[5].dest = 75000;
        graph.edge[5].weight = -1035;
        AddInMap(graph.edge[5].src, map, ref counter);
        AddInMap(graph.edge[5].dest, map, ref counter);

        // add edge 3-1 (or D-B in above figure) 
        graph.edge[6].src = 27325;
        graph.edge[6].dest = 75000;
        graph.edge[6].weight = 0;
        AddInMap(graph.edge[6].src, map, ref counter);
        AddInMap(graph.edge[6].dest, map, ref counter);

         //add edge 4-3 (or E-D in above figure) 
        graph.edge[7].src = 47679;
        graph.edge[7].dest = 42679;
        graph.edge[7].weight = 0;
        AddInMap(graph.edge[7].src, map, ref counter);
        AddInMap(graph.edge[7].dest, map, ref counter);

        graph.edge[8].src = 27325;
        graph.edge[8].dest = 22325;
        graph.edge[8].weight = 0;
        AddInMap(graph.edge[8].src, map, ref counter);
        AddInMap(graph.edge[8].dest, map, ref counter);

        graph.edge[9].src = 75000;
        graph.edge[9].dest = 74000;
        graph.edge[9].weight = 0;
        AddInMap(graph.edge[9].src, map, ref counter);
        AddInMap(graph.edge[9].dest, map, ref counter);

        graph.edge[10].src = 75000;
        graph.edge[10].dest = 79000;
        graph.edge[10].weight = -65000;
        AddInMap(graph.edge[10].src, map, ref counter);
        AddInMap(graph.edge[10].dest, map, ref counter);


        graph.edge[11].src = 42679;
        graph.edge[11].dest = 41679;
        graph.edge[11].weight = 0;
        AddInMap(graph.edge[11].src, map, ref counter);
        AddInMap(graph.edge[11].dest, map, ref counter);

        graph.edge[12].src = 22325;
        graph.edge[12].dest = 21325;
        graph.edge[12].weight = 0;
        AddInMap(graph.edge[12].src, map, ref counter);
        AddInMap(graph.edge[12].dest, map, ref counter);

        graph.edge[13].src = 22325;
        graph.edge[13].dest = 79000;
        graph.edge[13].weight = 4035;
        AddInMap(graph.edge[13].src, map, ref counter);
        AddInMap(graph.edge[13].dest, map, ref counter);

        graph.edge[14].src = 75000;
        graph.edge[14].dest = 74000;
        graph.edge[14].weight = 0;
        AddInMap(graph.edge[14].src, map, ref counter);
        AddInMap(graph.edge[14].dest, map, ref counter);

        graph.BellmanFord(graph, 0,map);

    }
    // This code is contributed by Ryuga 
}