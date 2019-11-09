using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Codewars.Kyu2
{
    public class Skyscrapers
    {
        /// <summary>In a grid of 6 by 6 squares you want to place a skyscraper in each square with only some clues:
        /// The height of the skyscrapers is between 1 and 6
        /// No two skyscrapers in a row or column may have the same number of floors
        /// A clue is the number of skyscrapers that you can see in a row or column from the outside
        /// Higher skyscrapers block the view of lower skyscrapers located behind them
        /// <para>https://www.codewars.com/kata/6-by-6-skyscrapers/train/csharp</para>
        /// </summary>
        /// 
        static List<int> defaultNumbers = new List<int>() { 1, 2, 3, 4, 5, 6 };

        public static int[][] SolvePuzzle(int[] clues)
        {
            List<Node> grid = new List<Node>();
            List<int> initialSet = new List<int>(defaultNumbers);

            // Setup grid and seed node
            grid = new List<Node>();
            Node currentNode = new Node();

            foreach (int num in initialSet)
            {
                Stack<Node> path = new Stack<Node>();
                path.Push(new Node(num));
                //for (int i = 0; i < 6; i++)
                //{
                //    Node n = new Node(i + 1, null);
                //    path.Push(n);
                //}

                while (path.Count > 0)
                {
                    currentNode = path.Pop();
                    List<Node> trace = new List<Node>();

                    Node pointer = currentNode;
                    while (pointer.GetParent() != null)
                    {
                        trace.Add(pointer);
                        pointer = pointer.GetParent();
                    }
                    trace.Add(pointer);
                    grid = trace.Select(n => n).Reverse().ToList();

#if DEBUG
                    //DrawGrid(grid);
#endif

                    //Check if there are any hint violations
                    if (ValidateHints(grid, clues))
                    {
                        // Else continue solving....

                        GenerateChildren(grid, currentNode);
                        // If at least 1 node generated, continue
                        if (currentNode.GetChildren().Count >= 1)
                        {
                            //currentNode.GetChildren().ForEach(n => path.Push(n));
                            foreach (Node n in currentNode.GetChildren())
                            {
                                path.Push(n);
                            }
                        }
                        else //deadend
                        {
                            // Check if this is a solution
                            if (grid.Count > 35 && ValidateHints(grid, clues))
                            {


                                Node kNode = currentNode;
                                Stack<int> pathStack = new Stack<int>();
                                while (kNode != null)
                                {
                                    pathStack.Push(kNode.GetValue().GetValueOrDefault());
                                    kNode = kNode.GetParent();
                                }

                                return Skyscrapers.Project(pathStack);
                            }
                            else { grid.Remove(grid.Last()); }
                        }
                    }
                    else
                    {
                        //Debug.WriteLine("Non-viable sequence");
                        if (currentNode.GetParent() != null) { currentNode.GetParent().RemoveChild(currentNode); }
                        grid.Remove(grid.Last());
                        while (grid.Last().GetChildren().Count == 0 && grid.Count > 1)
                        {
                            if (grid.Last().GetParent() != null) { grid.Last().GetParent().RemoveChild(grid.Last()); }
                            grid.Remove(grid.Last());
                        }

                        continue;
                    }


                }
            }



            //Notes: Backtracing maybe?
            return null;
        }

        private static void DrawGrid(List<Node> grid)
        {
            System.Diagnostics.Debug.WriteLine("----");
            for (int i = 0; i < grid.Count; i++)
            {
                Debug.Write(grid[i].GetValue());
                if ((i % 6) == 5 && i != 0)
                {
                    Debug.WriteLine(".");
                }
            }
            System.Diagnostics.Debug.WriteLine("\n----");

        }

        private static int[][] Project(Stack<int> pathStack)
        {
            int[][] grid = new int[6][];
            for (int i = 0; i < 6; i++)
            {
                int[] row = new int[6];
                for (int j = 0; j < 6; j++)
                {
                    row[j] = pathStack.Pop();
                }
                grid[i] = row;
            }
            return grid;
        }

        private static bool ValidateHints(List<Node> grid, int[] clues)
        {
            for (int i = 0; i < clues.Length; i++)
            {
                if (clues[i] == 0) { continue; } // Skip Zeroes
                else
                {
                    if (i >= 0 && i < 6)
                    { // Top 1,2,3,4,5,6
                        int counted = CountVisible(GetColumn(grid, i));
                        if (counted == 0) continue;
                        if (counted != clues[i])
                        {
                            return false;
                        }
                    }
                    else if (i >= 6 && i < 12)
                    { // Right 1,2,3,4,5,6
                        int counted = CountVisible(GetRow(grid, i - 6, true));
                        if (counted == 0) continue;
                        if (counted != clues[i])
                        {
                            return false;
                        }
                    }
                    else if (i >= 12 && i < 18)
                    { // Bottom 6,5,4,3,2,1
                        int counted = CountVisible(GetColumn(grid, 17 - i, true));
                        if (counted == 0) continue;
                        if (counted != clues[i])
                        {
                            return false;
                        }

                    }
                    else
                    { // Left 6,5,4,3,2,1
                        int counted = CountVisible(GetRow(grid, 23 - i));
                        if (counted == 0) continue;
                        if (counted != clues[i])
                        {
                            return false;
                        }
                    }
                }

            }

            return true;
        }

        private static int CountVisible(List<int?> list)
        {
            int visible = 0;
            int lowerLimit = 0;

            //If incomplete list, skip
            if (list.Count < 6)
            {
                return 0;
            }

            foreach (int height in list)
            {
                if (height > lowerLimit)
                {
                    lowerLimit = height;
                    visible++;
                }
            }

            return visible;
        }

        private static bool ValidateRules() => false;

        #region Helpers
        public static List<int?> GetRow(List<Node> grid, int i)
        {
            return GetRow(grid, i, false);
        }

        public static List<int?> GetRow(List<Node> grid, int i, bool reverse)
        {
            List<int?> row = new List<int?>();
            int maxrow = (grid.Count / 6);
            int maxcol = grid.Count % 6;



            if (i < maxrow)
            { //assume full row

                row = grid.GetRange(i * 6, 6).Select(n => n.GetValue()).ToList();
            }
            else if (i == maxrow)
            { // assume non-full
                for (int j = i * 6; j < grid.Count; j++)
                {
                    row.Add(grid[j].GetValue());
                }
                //row = grid.GetRange(i * 6, (i * 6) - grid.Count - 1).Select(n => n.GetValue()).ToList();
                //row = grid.GetRange()
            }


            if (reverse)
            {
                row.Reverse();
            }

            return row;

        }

        public static List<int?> GetColumn(List<Node> grid, int i)
        {
            return GetColumn(grid, i, false);
        }
        public static List<int?> GetColumn(List<Node> grid, int i, bool reverse)
        {
            List<int?> col = grid.Where((x, k) => k % 6 == i).Select(n => n.GetValue()).ToList();
            if (reverse) { col.Reverse(); }
            return col;
        }
        #endregion

        public static void GenerateChildren(List<Node> grid, Node n)
        {
            if (grid.Count >= 36) return;

            int position = grid.IndexOf(n) + 1;
            List<int> colNumbers = GetColumn(grid, position % 6).Where(i => i != null).Cast<int>().ToList();
            List<int> rowNumbers = GetRow(grid, position / 6).Where(i => i != null).Cast<int>().ToList();

            var validNumbers = defaultNumbers.Except(colNumbers).Except(rowNumbers);
            foreach (int i in validNumbers)
            {
                n.AddChild(new Node(i, n));
            }



            return;
        }




        public class Node
        {
            Node parent = null;
            int? value = null;
            List<Node> children = new List<Node>();

            public Node()
            {

            }
            public Node(int? value)
            {
                this.value = value;
            }

            public Node(int? value, Node parent)
            {
                this.value = value;
                this.parent = parent;
            }

            public void AddChild(Node n)
            {
                this.children.Add(n);
            }

            public void AddChild(int value)
            {
                this.children.Add(new Node(value, this));
            }

            public List<Node> GetChildren()
            {
                return children;
            }

            public int? GetValue() => value;

            public int? SetValue(int? value) => this.value = value;

            public Node GetParent() => this.parent;

            internal void RemoveChild(Node deadendNode)
            {
                this.children.Remove(deadendNode);
            }
        }




    }
}
