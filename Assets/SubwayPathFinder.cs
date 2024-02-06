using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubwayPathFinder : MonoBehaviour
{
    private Dictionary<string, StationNode> stations;
    public Text pathOutputText;

    private void Awake()
    {
        
        stations = new Dictionary<string, StationNode>();

        
        foreach (var stationName in new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "J", "K", "L", "M", "N", "O" })
        {
            stations[stationName] = new StationNode(stationName);
        }

        // Red Line connections
        CreateConnection("A", "B");
        CreateConnection("B", "C");
        CreateConnection("C", "D");
        CreateConnection("D", "E");
        CreateConnection("E", "F");

        // Green Line connections
        CreateConnection("K", "C");
        CreateConnection("C", "L");
        CreateConnection("L", "M");
        CreateConnection("M", "E");
        CreateConnection("E", "J");

        // Blue Line connections
        CreateConnection("J", "L");
        CreateConnection("L", "N");
        CreateConnection("N", "O");
        CreateConnection("O", "D");

        // Dark Line connections
        CreateConnection("B", "H");
        CreateConnection("H", "J");
        CreateConnection("J", "F");
        CreateConnection("F", "G");
    }

    
    private void CreateConnection(string from, string to)
    {
        StationNode fromStation = stations[from];
        StationNode toStation = stations[to];
        fromStation.Connections.Add(toStation);
        toStation.Connections.Add(fromStation); // Comment out if connection is not bidirectional
    }

    // BFS algorithm 
    public List<StationNode> FindShortestPath(string startName, string endName)
    {
        if (!stations.ContainsKey(startName) || !stations.ContainsKey(endName)) return null;

        var startStation = stations[startName];
        var endStation = stations[endName];

        var queue = new Queue<StationNode>();
        var visited = new HashSet<StationNode>();
        var previous = new Dictionary<StationNode, StationNode>();

        queue.Enqueue(startStation);
        visited.Add(startStation);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            if (current == endStation)
            {
                return ReconstructPath(previous, endStation);
            }

            foreach (var neighbor in current.Connections)
            {
                if (!visited.Contains(neighbor))
                {
                    queue.Enqueue(neighbor);
                    visited.Add(neighbor);
                    previous[neighbor] = current;
                }
            }
        }

        // No path found
        return null;
    }

    public void ExecutePathfinding(string startStationName, string endStationName)
    {
        List<StationNode> path = FindShortestPath(startStationName, endStationName);
        if (path != null && path.Count > 0)
        {
            
            DisplayPath(path);
        }
        else
        {
            Debug.LogError("No path found from " + startStationName + " to " + endStationName);
            if (pathOutputText != null)
            {
                pathOutputText.text = "No path found.";
            }
        }
    }

    public void DisplayPath(List<StationNode> path)
    {
        // Implementation 
        Debug.Log("Path found:");
        string pathString = "Path: ";
        foreach (var station in path)
        {
            Debug.Log(station.Name);
            pathString += station.Name + " ";
        }

        // show the path
        if (pathOutputText != null)
        {
            pathOutputText.text = pathString.TrimEnd();
        }
    }

    // reconstruct the path from end to start
    private List<StationNode> ReconstructPath(Dictionary<StationNode, StationNode> previous, StationNode endStation)
    {
        var path = new List<StationNode>();
        for (var at = endStation; at != null; at = previous.ContainsKey(at) ? previous[at] : null)
        {
            path.Add(at);
        }
        path.Reverse();
        return path;
    }
}



    public class StationNode
{
    public string Name { get; set; }
    public List<StationNode> Connections { get; set; }

    public StationNode(string name)
    {
        Name = name;
        Connections = new List<StationNode>();
    }
}
