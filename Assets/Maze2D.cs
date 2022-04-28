using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze2D {
    public MazeNode[][] maze;
    private List<MazeNode> visited;

    public Maze2D(int xSize, int ySize) {
        this.maze = new MazeNode[ySize][];
        for(int i=0; i<ySize; i++) {
            this.maze[i]=new MazeNode[xSize];
            for(int j=0; j<xSize; j++) {
                this.maze[i][j]=new MazeNode();
            }
        }
        for(int i=0; i<ySize-1; i++) {
            for(int j=0; j<xSize-1; j++) {
                this.maze[i][j].ConnectNodes(this.maze[i+1][j]);
                this.maze[i][j].ConnectNodes(this.maze[i][j+1]);
            }
        }
        for(int i=0; i<ySize-1; i++) {
            this.maze[i][xSize-1].ConnectNodes(this.maze[i+1][xSize-1]);
        }
        for(int j=0; j<xSize-1; j++) {
            this.maze[ySize-1][j].ConnectNodes(this.maze[ySize-1][j+1]);
        }
        Debug.Log(this.ToString());
        this.visited = new List<MazeNode>();
        this.maze[0][0].VisitSet();
        this.visited.Add(this.maze[0][0]);
        while(this.visited.Count<xSize*ySize) {
            MazeNode source = this.visited[Random.Range(0,visited.Count)];
            MazeNode destination = source.RandomNeighbor();
            if(source.RemoveWallToUnvisited(destination)){
                this.visited.Add(destination);
            }
        }
        Debug.Log(this.ToString());
    }

    override public string ToString() {
        string str = "";
        for(int i=0; i<this.maze[0].GetLength(0); i++) {
            str+="██";
        }
        str+="█\n";
        for(int i=0; i<this.maze.GetLength(0)-1; i++) {
            str+="█";
            for(int j=0; j<this.maze[0].GetLength(0)-1; j++) {
                if(this.maze[i][j].IsConnectedByWall(this.maze[i][j+1])){
                    str+="░█";
                }
                else {
                    str+="░░";
                }
            }
            str+="░█\n";
            str+="█";
            for(int j=0; j<this.maze[0].GetLength(0); j++) {
                if(this.maze[i][j].IsConnectedByWall(this.maze[i+1][j])){
                    str+="██";
                }
                else {
                    str+="░█";
                }
            }
            str+="\n";
        }
        str+="█";
        for(int j=0; j<this.maze[0].GetLength(0)-1; j++) {
            if(this.maze[this.maze.GetLength(0)-1][j].IsConnectedByWall(this.maze[this.maze.GetLength(0)-1][j+1])){
                str+="░█";
            }
            else {
                str+="░░";
            }
        }
        str+="░█\n";
        
        for(int i=0; i<this.maze[0].GetLength(0); i++) {
            str+="██";
        }
        str+="█\n";
        return str;
    }
}