using System.Linq;

namespace EvaluateDivision
{
    internal class Program
    {
        public class EvaluateDivision
        {
            private class Pair
            {
                public readonly string edge;
                public readonly double equation;

                public Pair(string edge, double equation)
                {
                    this.edge = edge;
                    this.equation = equation;
                }
            }

            private double BFS(Dictionary<string, List<Pair>> graph, string source, string target)
            {
                if (!graph.ContainsKey(source) || !graph.ContainsKey(target))
                {
                    return -1;
                }
                HashSet<string> visited = new();
                Queue<Pair> queue = new();
                queue.Enqueue(new Pair(source, 1.0));
                visited.Add(source);
                while(queue.Count > 0)
                {
                    Pair currentPair = queue.Dequeue();
                    if (currentPair.edge == target)
                    {
                        return currentPair.equation;
                    }
                    foreach(Pair pair in graph[currentPair.edge])
                    {
                        if (visited.Add(pair.edge))
                        {
                            queue.Enqueue(new Pair(pair.edge, pair.equation * currentPair.equation));
                        }
                    }
                }
                return -1;
            }

            public double[] CalcEquation(IList<IList<string>> equations, double[] values, IList<IList<string>> queries)
            {
                int n = queries.Count;
                double[] calcEquation = new double[n];
                Dictionary<string, List<Pair>> graph = new();
                for (int i = 0; i <  equations.Count; ++i)
                {
                    if (!graph.ContainsKey(equations[i][0]))
                    {
                        graph.Add(equations[i][0], new List<Pair>());
                    }
                    graph[equations[i][0]].Add(new Pair(equations[i][1], values[i]));
                    if (!graph.ContainsKey(equations[i][1]))
                    {
                        graph.Add(equations[i][1], new List<Pair>());
                    }
                    graph[equations[i][1]].Add(new Pair(equations[i][0], 1 / values[i]));
                }

                for (int i = 0; i < queries.Count; ++i)
                {
                    calcEquation[i] = BFS(graph, queries[i][0], queries[i][1]);
                }
                return calcEquation;
            }
        }

        static void Main(string[] args)
        {
            EvaluateDivision evaluateDivision = new();
            Console.WriteLine(string.Join(", ", evaluateDivision.CalcEquation(new string[][]
            {
                new string[] { "a", "b" }, new string[] { "b", "c" }
            }, new double[] { 2.0, 3.0 }, new string[][]
            {
                new string[] { "a", "c" }, new string[] { "b", "a" }, new string[] { "a", "e" }, new string[] { "a", "a" }, new string[] { "x", "x" }
            })));
            Console.WriteLine();
            Console.WriteLine(string.Join(", ", evaluateDivision.CalcEquation(new string[][]
            {
                new string[] { "a", "b" }, new string[] { "b", "c" }, new string[] { "bc", "cd" }
            }, new double[] { 1.5, 2.5, 5.0 }, new string[][]
            {
                new string[] { "a", "c" }, new string[] { "c", "b" }, new string[] { "bc", "cd" }, new string[] { "cd", "bc" }
            })));
            Console.WriteLine();
            Console.WriteLine(string.Join(", ", evaluateDivision.CalcEquation(new string[][]
            {
                new string[] { "a", "b" }
            }, new double[] { 0.5 }, new string[][]
            {
                new string[] { "a", "b" }, new string[] { "b", "a" }, new string[] { "a", "c" }, new string[] { "x", "y" }
            })));
            Console.WriteLine();
        }
    }
}