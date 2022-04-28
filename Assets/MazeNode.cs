using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeNode {
    private bool seen;
    private List<MazeNode> connections;
    private List<bool> wall;

    public MazeNode() {
        this.seen = false;
        this.connections = new List<MazeNode>();
        this.wall = new List<bool>();
    }

    public void ConnectNodes(MazeNode m) {
        this.connections.Add(m);
        this.wall.Add(true);
        m.ConnectNodes2ndHand(this);
    }

    private void ConnectNodes2ndHand(MazeNode m) {
        this.connections.Add(m);
        this.wall.Add(true);
    }

    public bool IsConnectedByWall(MazeNode m) {
        return this.wall[this.connections.IndexOf(m)];
    }

    public void VisitSet() {
        this.seen=true;
    }

    public MazeNode RandomNeighbor() {
        return this.connections[Random.Range(0,this.connections.Count)];
    }

    public bool RemoveWallToUnvisited(MazeNode m) {
        if(this.IsConnectedByWall(m) && m.seen==false) {
            this.wall[this.connections.IndexOf(m)]=false;
            m.WallRemoved(this);
            return true;
        }
        return false;
    }
    private void WallRemoved(MazeNode m) {
        if(this.IsConnectedByWall(m)) {
            this.wall[this.connections.IndexOf(m)]=false;
            this.seen=true;
        }
    }

}