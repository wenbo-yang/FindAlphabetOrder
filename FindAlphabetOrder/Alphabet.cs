using System;
using System.Collections.Generic;

namespace FindAlphabetOrder
{
    /*
        This can be done with both dfs and bfs, i used bfs here for simplicity. 
        DFS will be using a recursive call and another stack (anything that adds to the head). This double inversion makes code hard to understand.
       
        Regardless of DFS or BFS, the idea is to construct a graph as we loop through characters if each pair of words
        and as we loop through, a parent should now have children. 
        Each character is assigned a depth. 
        As a char has more parents, then it will get more depth. (5 different parents, 5 depths).        
        The we will loop through the depth map, and we should find one char at each depth, if the input is valid.

        depth map
        char: depth 
        a:0
        b:1
        c:2
        d:3

        graph map
        
        parent: (children)
        a: (b c d)
        b: (c d)
        c: (d)
        d: (nothing)

        Big O is O(1) space since as in max space is constant 26*26 or numofalphabet^2
        and O(N) time.
    */


    public class Alphabet
    {
        public List<char> FindAlphabetOrder(List<string> wordList)
        {
            var graph = new Dictionary<char, HashSet<char>>();
            var depth = new Dictionary<char, int>();

            InitializeGraph(wordList, graph, depth);

            MapGraph(wordList, graph, depth);

            var result = TopologicalSortBFS(graph, depth);

            return result;
        }

        private void InitializeGraph(List<string> wordList, Dictionary<char, HashSet<char>> graph, Dictionary<char, int> depth)
        {
            foreach (var word in wordList)
            {
                foreach (var c in word)
                {
                    if (!graph.ContainsKey(c))
                    {
                        graph.Add(c, new HashSet<char>());
                        depth.Add(c, 0);
                    }
                }
            }
        }

        private void MapGraph(List<string> wordList, Dictionary<char, HashSet<char>> graph, Dictionary<char, int> depth)
        {
            for (int i = 0; i < wordList.Count - 1; i++)
            {
                var parent = wordList[i];
                var child = wordList[i + 1];

                var length = Math.Min(parent.Length, child.Length);

                for (int j = 0; j < length; j++)
                {
                    var p = parent[j];
                    var c = child[j];

                    if (p != c) // if chars are different
                    {
                        if (!graph[p].Contains(c))
                        {
                            graph[p].Add(c); // parent now have this child
                            depth[c]++;   // child is now + 1 depth from top // 
                        }

                        break; // break here to stop comparing next chars
                    }
                }
            }
        }

        private List<char> TopologicalSortBFS(Dictionary<char, HashSet<char>> graph, Dictionary<char, int> depth)
        {
            var result = new List<char>();
            var queue = new Queue<char>();

            // first find the char with depth of 0 and push on to queue
            foreach (var item in depth)
            {
                if (item.Value == 0)
                {
                    queue.Enqueue(item.Key);
                }
            }

            while (queue.Count != 0)
            {
                var parent = queue.Dequeue();

                if (queue.Count != 0)
                {
                    throw new ArgumentException("we have dangling nodes, this should not happen, please check input");
                }

                result.Add(parent);

                var children = graph[parent];
                // loop through each child deduct their depth, 
                // if particular child is of depth, we should enqueue it. 
                // note: if input is valid, we should always have only one child of 0 depth.
                foreach (var child in children)
                {
                    depth[child]--;
                    if (depth[child] == 0)
                    {
                        queue.Enqueue(child);
                    }
                }
            }

            return result;
        }
    }
}
