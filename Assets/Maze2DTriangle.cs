using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze2DTriangle {
    public MazeNode[][] maze;
    private List<MazeNode> visited;

    public Maze2DTriangle(int xSize, int ySize) {
        this.maze = new MazeNode[ySize*2][];
        int i = 0;
        int j = 0;
        for(i=0; i<ySize*2; i+=4) {
            this.maze[i]=new MazeNode[xSize-1];
            this.maze[i+1]=new MazeNode[xSize];
            for(j=0; j<xSize; j++) {
                this.maze[i+1][j]=new MazeNode();
            }
            for(j=0; j<xSize-1; j++) {
                this.maze[i][j]=new MazeNode();
                this.maze[i+1][j].ConnectNodes(this.maze[i][j]);
                this.maze[i+1][j+1].ConnectNodes(this.maze[i][j]);
            }
            this.maze[i+1][j].ConnectNodes(this.maze[i][j-1]);

        }
        for(i=2; i<ySize*2; i+=4) {
            this.maze[i]=new MazeNode[xSize];
            this.maze[i+1]=new MazeNode[xSize-1];
            for(j=0; j<xSize; j++) {
                this.maze[i][j]=new MazeNode();
            }
            for(j=0; j<xSize-1; j++) {
                this.maze[i+1][j]=new MazeNode();
                this.maze[i][j].ConnectNodes(this.maze[i+1][j]);
                this.maze[i][j+1].ConnectNodes(this.maze[i+1][j]);
            }
            this.maze[i][j].ConnectNodes(this.maze[i+1][j-1]);
        }

        for(i=1; i<ySize*2; i+=4) {
            if((i+1)/2<ySize) {
                for(j=0; j<xSize; j++) {
                    this.maze[i][j].ConnectNodes(this.maze[i+1][j]);
                }
            }
        }
        for(i=3; i<ySize*2-1; i+=4) {
            for(j=0; j<xSize-1; j++) {
                this.maze[i][j].ConnectNodes(this.maze[i+1][j]);
            }
        }
        Debug.Log(this.ToString());
        this.visited = new List<MazeNode>();
        this.maze[0][0].VisitSet();
        this.visited.Add(this.maze[0][0]);
        while(this.visited.Count<(xSize*2-1)*ySize) {
            int rand = Random.Range(0,visited.Count);
            MazeNode source = this.visited[rand];
            MazeNode destination = source.RandomNeighbor();
            if(source.RemoveWallToUnvisited(destination)){
                this.visited.Add(destination);
            }
        }
        Debug.Log(this.ToString());
    }

    override public string ToString() {
        string str = "";
        int i = 0;
        int j = 0;
        str+="█";
        for(j=0; j<this.maze[i].GetLength(0)+this.maze[i+1].GetLength(0)-1; j+=2) {
            str+="████";
        }
        str+="██\n";
        for(i=0; i<this.maze.GetLength(0); i+=4) {
            for(j=0; j<this.maze[i].GetLength(0)+this.maze[i+1].GetLength(0)-1; j+=2) {
                str+="███░";
            }
            str+="███\n";
            str+="█";
            for(j=0; j<this.maze[i].GetLength(0); j++) {
                if(!this.maze[i+1][j].IsConnectedByWall(this.maze[i][j])) {
                    str+="█░";
                }
                else {
                    str+="██";
                }
                if(!this.maze[i+1][j+1].IsConnectedByWall(this.maze[i][j])) {
                    str+="█░";
                }
                else {
                    str+="██";
                }
            }
            str+="██\n";
            for(j=0; j<this.maze[i].GetLength(0)+this.maze[i+1].GetLength(0)-2; j+=2) {
                str+="█░██";
            }
            str+="█░█\n";
            if((i+2)<this.maze.GetLength(0)) {
                str+="█";
                for(j=0; j<this.maze[i+1].GetLength(0); j++) {
                    if(!this.maze[i+1][j].IsConnectedByWall(this.maze[i+2][j])) {
                        str+="░█";
                    }
                    else {
                        str+="██";
                    }
                    if(j<this.maze[i].GetLength(0)) {
                        str+="██";
                    }
                }
                str+="\n";
                for(j=0; j<this.maze[i].GetLength(0)+this.maze[i+1].GetLength(0)-1; j+=2) {
                    str+="█░██";
                }
                str+="█░█\n";
                str+="█";
                
                for(j=0; j<this.maze[i+3].GetLength(0); j++) {
                    if(!this.maze[i+2][j].IsConnectedByWall(this.maze[i+3][j])) {
                        str+="█░";
                    }
                    else {
                        str+="██";
                    }
                    if(!this.maze[i+2][j+1].IsConnectedByWall(this.maze[i+3][j])) {
                        str+="█░";
                    }
                    else {
                        str+="██";
                    }
                }
                str+="██\n";
                for(j=0; j<this.maze[i+2].GetLength(0)+this.maze[i+3].GetLength(0)-1; j+=2) {
                    str+="███░";
                }
                str+="███\n";
                if((i+4)<this.maze.GetLength(0)) {
                    str+="█";
                    for(j=0; j<this.maze[i+3].GetLength(0); j++) {
                        if(!this.maze[i+3][j].IsConnectedByWall(this.maze[i+4][j])) {
                            str+="██░█";
                        }
                        else {
                            str+="████";
                        }
                    }
                    str+="██\n";
                }
            }
        }
        i=0;
        str+="█";
        for(j=0; j<this.maze[i].GetLength(0)+this.maze[i+1].GetLength(0)-1; j+=2) {
            str+="████";
        }
        str+="██\n";
        return str;
    }
}