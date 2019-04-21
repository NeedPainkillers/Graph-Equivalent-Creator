using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph
{
    class Program
    {
        static void Main(string[] args)
        {
            //List<Vertex> vertices = new List<Vertex>
            //{
            //    new Vertex { Name = 1 },
            //    new Vertex { Name = 2 },
            //    new Vertex { Name = 3 },
            //    new Vertex { Name = 4 },
            //    new Vertex { Name = 5 }
            //};
            //vertices.Find(x => x.Name.Equals(1)).Voisins.AddRange(vertices.FindAll(x => x.Name.Equals(2) || x.Name.Equals(3)));
            //vertices.Find(x => x.Name.Equals(2)).Voisins.AddRange(vertices.FindAll(x => x.Name.Equals(3) || x.Name.Equals(4) || x.Name.Equals(5)));
            //vertices.Find(x => x.Name.Equals(3)).Voisins.AddRange(vertices.FindAll(x => x.Name.Equals(4)));
            //vertices.Find(x => x.Name.Equals(4)).Voisins.AddRange(vertices.FindAll(x => x.Name.Equals(5)));

            //var Graph = CreateEquivalentGraph(ref vertices);

            List<Vertex> vertices = new List<Vertex>
            {
                new Vertex { Name = 0, Status = 0 },
                new Vertex { Name = 1, Status = 1 },
                new Vertex { Name = 2, Status = 2 },
                new Vertex { Name = 3, Status = 3 },
                new Vertex { Name = 4, Status = 4 },
                new Vertex { Name = 5, Status = 5 },
                new Vertex { Name = 6, Status = 6 },
                new Vertex { Name = 7, Status = 7 },
                new Vertex { Name = 8, Status = 8 },
                new Vertex { Name = 9, Status = 9 }
                
            };
            vertices.Find(x => x.Name.Equals(0)).Voisins.AddRange(vertices.FindAll(x => x.Name.Equals(4) || x.Name.Equals(9)));
            vertices.Find(x => x.Name.Equals(1)).Voisins.AddRange(vertices.FindAll(x => x.Name.Equals(3) || x.Name.Equals(4) || x.Name.Equals(6) || x.Name.Equals(8)));
            vertices.Find(x => x.Name.Equals(2)).Voisins.AddRange(vertices.FindAll(x => x.Name.Equals(6) || x.Name.Equals(7)));
            vertices.Find(x => x.Name.Equals(3)).Voisins.AddRange(vertices.FindAll(x => x.Name.Equals(9)));
            vertices.Find(x => x.Name.Equals(4)).Voisins.AddRange(vertices.FindAll(x => x.Name.Equals(5) || x.Name.Equals(9)));
            vertices.Find(x => x.Name.Equals(5)).Voisins.AddRange(vertices.FindAll(x => x.Name.Equals(7) || x.Name.Equals(9)));
            vertices.Find(x => x.Name.Equals(6)).Voisins.AddRange(vertices.FindAll(x => x.Name.Equals(9)));
            vertices.Find(x => x.Name.Equals(7)).Voisins.AddRange(vertices.FindAll(x => x.Name.Equals(9)));

            //var Graph = CreateEquivalentGraph(ref vertices);

            var Graph = CreateMinEqGraph(ref vertices);

            foreach (var item in Graph)
            {
                foreach (var voisin in item.Voisins)
                {
                    System.Console.WriteLine(item.Name.ToString() + voisin.Name.ToString());
                }
            }
        }

        public static List<Vertex> CreateEquivalentGraph(ref List<Vertex> Graph)
        {
            List<Vertex> newGraph = new List<Vertex>();

            foreach (Vertex vertex in Graph)
            {
                newGraph.Add(new Vertex() { Name = vertex.Name, Status = vertex.Status });
            }

            foreach (Vertex vertex in Graph)
            {
                if (vertex.Name == vertex.Status)
                {
                    var toADD = CheckVertex(vertex, ref newGraph, ref Graph);
                }
                //newGraph.Add(toADD);
            }

            return newGraph;
        }

        public static Vertex CheckVertex (Vertex vertex, ref List<Vertex> newGraph, ref List<Vertex> oldGraph)
        {
            //Vertex newVertex = new Vertex() {Name = vertex.Name , Status = vertex.Status};

            foreach (Vertex voisin in vertex.Voisins)
            {
                if(voisin.Status != vertex.Status)
                {
                    if(vertex.Name == 6)
                    {
                       var x = vertex.Status;
                    }

                    //voisin.Status = vertex.Status;
                    int Status = voisin.Status;
                    foreach (var item in oldGraph.FindAll(x => x.Status.Equals(Status)))
                    {
                        item.Status = vertex.Status;
                    }
                    var toAdd = CheckVertex(voisin,ref newGraph,ref oldGraph);
                    //newVertex.Voisins.Add(toAdd);
                    newGraph.Find(x => x.Name.Equals(vertex.Name)).Voisins.Add(newGraph.Find(x => x.Name.Equals(voisin.Name)));
                    
                }
            }
            //newGraph.Add(newVertex);

            return null;
        }

        public static List<Vertex> CreateMinEqGraph(ref List<Vertex> Graph)
        {
            List<Vertex> newGraph = new List<Vertex>();

            foreach (Vertex vertex in Graph)
            {
                newGraph.Add(new Vertex() { Name = vertex.Name, Status = vertex.Status });
            }

            foreach (Vertex vertex in Graph) //copy operation
            {
                foreach (Vertex voisin in vertex.Voisins)
                {
                    newGraph.Find(x => x.Name.Equals(vertex.Name)).Voisins.Add(newGraph.Find(x => x.Name.Equals(voisin.Name)));
                }
            }

            foreach (Vertex from in newGraph)
            {
                
                foreach (Vertex voisin in from.Voisins)
                {
                    foreach (Vertex item in from.Voisins.FindAll(x => !x.Name.Equals(voisin.Name)))
                    {
                        newGraph.ForEach(x => x.Status = 0);
                        if(CheckReachability(item, voisin))
                        {
                            voisin.Status = -2;
                        }
                    }
                }
                //removing reacheable from another neighbour neighbours
                var temp = from.Voisins.FindAll(x => x.Status.Equals(-2));
                temp.ForEach(x => x.Status = 0);
                foreach (Vertex toRemove in temp)
                {
                    from.Voisins.Remove(toRemove);
                }
                temp.Clear();
                temp = null;
            }

            foreach (Vertex from in newGraph)
            {
                
            }

            return newGraph;
        }

        public static bool CheckReachability(Vertex from, Vertex to)
        {
            foreach (Vertex item in from.Voisins)
            {
                if (item.Status == -1 || item.Status == -2)
                    return false;
                item.Status = -1;
                if (item.Name.Equals(to.Name))
                {
                    item.Status = 0;
                    return true;
                }

                if(CheckReachability(item, to))
                {
                    item.Status = 0;
                    return true;
                }
            }

            return false;
        }

    }

    class Vertex
    {
        public int Name { get; set; } = 0;
        public int Status { get; set; } = 0;
        public List<Vertex> Voisins { get; set; } = new List<Vertex>();
    }

}
//https://studfiles.net/html/2706/14/html_LDwCaq1ptU.804u/img-9uVp1H.png 
//https://upload.wikimedia.org/wikipedia/commons/thumb/d/d1/Divisors_12.svg/440px-Divisors_12.svg.png