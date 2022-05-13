using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ProjectionSquare : MonoBehaviour
{

    private List<Vector3> verticies;
    private List<int> triangles;
    private List<Vector3> normals;
    private List<Vector2> uv;

    private Maze2DSquare maze;
    void OnEnable () {
		var mesh = new Mesh {
			name = "Procedural Mesh"
		};
        this.verticies = new List<Vector3>();
        this.triangles = new List<int>();
        this.normals = new List<Vector3>();
        this.uv = new List<Vector2>();

        this.maze = new Maze2DSquare(10, 10);

        this.AddYPQuad(0,1,0);
        this.AddYNQuad(0,0,0);
        this.AddXNQuad(0,0,0);
        this.AddZNQuad(0,0,0);
        int j;
        for (j=0; j<this.maze.maze[0].GetLength(0)-1; j++) {
            this.AddYPQuad(0,1,2*j+1);
            this.AddYNQuad(0,0,2*j+1);
            this.AddXNQuad(0,0,2*j+1);
            this.AddXPQuad(1,0,2*j+1);
            this.AddYPQuad(0,1,2*j+2);
            this.AddYNQuad(0,0,2*j+2);
            this.AddXNQuad(0,0,2*j+2);
            if(!this.maze.maze[0][j].IsConnectedByWall(this.maze.maze[0][j+1])) {
                this.AddXPQuad(1,0,2*j+2);
            }
        }
        this.AddYPQuad(0,1,2*j+1);
        this.AddYNQuad(0,0,2*j+1);
        this.AddXNQuad(0,0,2*j+1);
        this.AddXPQuad(1,0,2*j+1);
        this.AddYPQuad(0,1,2*j+2);
        this.AddYNQuad(0,0,2*j+2);
        this.AddXNQuad(0,0,2*j+2);
        this.AddZPQuad(0,0,2*j+3);

        int i;
        for (i=0; i<this.maze.maze.GetLength(0)-1; i++) {
            this.AddYPQuad(2*i+1,1,0);
            this.AddYNQuad(2*i+1,0,0);
            this.AddZNQuad(2*i+1,0,0);
            this.AddZPQuad(2*i+1,0,1);
            for(j=0; j<this.maze.maze[0].GetLength(0)-1; j++) {
                if(this.maze.maze[i][j].IsConnectedByWall(this.maze.maze[i][j+1])) {
                    this.AddYPQuad(2*i+1,1,2*j+2);
                    this.AddYNQuad(2*i+1,0,2*j+2);
                    this.AddZNQuad(2*i+1,0,2*j+2);
                    this.AddZPQuad(2*i+1,0,2*j+3);
                }
            }
            this.AddYPQuad(2*i+1,1,2*j+2);
            this.AddYNQuad(2*i+1,0,2*j+2);
            this.AddZNQuad(2*i+1,0,2*j+2);
            this.AddZPQuad(2*i+1,0,2*j+3);

            
            this.AddYPQuad(2*i+2,1,0);
            this.AddYNQuad(2*i+2,0,0);
            this.AddZNQuad(2*i+2,0,0);
            for(j=0; j<this.maze.maze[0].GetLength(0)-1; j++) {
                if(this.maze.maze[i][j].IsConnectedByWall(this.maze.maze[i+1][j])) {
                    this.AddYPQuad(2*i+2,1,2*j+1);
                    this.AddYNQuad(2*i+2,0,2*j+1);
                    this.AddXPQuad(2*i+3,0,2*j+1);
                    this.AddXNQuad(2*i+2,0,2*j+1);
                }
                else {
                    this.AddZNQuad(2*i+2,0,2*j+2);
                    this.AddZPQuad(2*i+2,0,2*j+1);
                }
                this.AddYPQuad(2*i+2,1,2*j+2);
                this.AddYNQuad(2*i+2,0,2*j+2);
                this.AddXPQuad(2*i+3,0,2*j+2);
                this.AddXNQuad(2*i+2,0,2*j+2);
            }
            if(this.maze.maze[i][j].IsConnectedByWall(this.maze.maze[i+1][j])) {
                this.AddYPQuad(2*i+2,1,2*j+1);
                this.AddYNQuad(2*i+2,0,2*j+1);
                this.AddXPQuad(2*i+3,0,2*j+1);
                this.AddXNQuad(2*i+2,0,2*j+1);
            }
            else {
                this.AddZNQuad(2*i+2,0,2*j+2);
                this.AddZPQuad(2*i+2,0,2*j+1);
            }
            this.AddYPQuad(2*i+2,1,2*j+2);
            this.AddYNQuad(2*i+2,0,2*j+2);
            this.AddZPQuad(2*i+2,0,2*j+3);
        }
        this.AddYPQuad(2*i+1,1,0);
        this.AddYNQuad(2*i+1,0,0);
        this.AddZNQuad(2*i+1,0,0);
        this.AddZPQuad(2*i+1,0,1);
        for(j=0; j<this.maze.maze[0].GetLength(0)-1; j++) {
            if(this.maze.maze[i][j].IsConnectedByWall(this.maze.maze[i][j+1])) {
                this.AddYPQuad(2*i+1,1,2*j+2);
                this.AddYNQuad(2*i+1,0,2*j+2);
                this.AddZNQuad(2*i+1,0,2*j+2);
                this.AddZPQuad(2*i+1,0,2*j+3);
            }
        }
        this.AddYPQuad(2*i+1,1,2*j+2);
        this.AddYNQuad(2*i+1,0,2*j+2);
        this.AddZNQuad(2*i+1,0,2*j+2);
        this.AddZPQuad(2*i+1,0,2*j+3);

        this.AddYPQuad(2*i+2,1,0);
        this.AddYNQuad(2*i+2,0,0);
        this.AddXPQuad(2*i+3,0,0);
        this.AddZNQuad(2*i+2,0,0);
        for (j=0; j<this.maze.maze[0].GetLength(0)-1; j++) {
            this.AddYPQuad(2*i+2,1,2*j+1);
            this.AddYNQuad(2*i+2,0,2*j+1);
            this.AddXPQuad(2*i+3,0,2*j+2);
            this.AddXPQuad(2*i+3,0,2*j+1);
            this.AddYPQuad(2*i+2,1,2*j+2);
            this.AddYNQuad(2*i+2,0,2*j+2);
            this.AddXNQuad(2*i+2,0,2*j+1);
            if(!this.maze.maze[i][j].IsConnectedByWall(this.maze.maze[i][j+1])) {
                this.AddXNQuad(2*i+2,0,2*j+2);
            }
        }
        this.AddYPQuad(2*i+2,1,2*j+1);
        this.AddYNQuad(2*i+2,0,2*j+1);
        this.AddXNQuad(2*i+2,0,2*j+1);
        this.AddXPQuad(2*i+3,0,2*j+1);
        this.AddYPQuad(2*i+2,1,2*j+2);
        this.AddYNQuad(2*i+2,0,2*j+2);
        this.AddXPQuad(2*i+3,0,2*j+2);
        this.AddZPQuad(2*i+2,0,2*j+3);

		mesh.vertices = this.verticies.ToArray();

        mesh.triangles = this.triangles.ToArray();
        mesh.normals = this.normals.ToArray();
        mesh.uv = this.uv.ToArray();
		GetComponent<MeshFilter>().mesh = mesh;
	}

    void AddYPQuad(float x, float y, float z) {
        this.triangles.AddRange(new int[] {this.verticies.Count+0, this.verticies.Count+2, this.verticies.Count+1});
        this.verticies.AddRange(new Vector3[] {new Vector3(x+0f, y+0f, z+0f), new Vector3(x+1f, y+0f, z+0f), new Vector3(x+0f, y+0f, z+1f)});
        this.normals.AddRange(new Vector3[] {Vector3.up, Vector3.up, Vector3.up});
        this.triangles.AddRange(new int[] {this.verticies.Count+0, this.verticies.Count+1, this.verticies.Count+2});
        this.verticies.AddRange(new Vector3[] {new Vector3(x+1f, y+0f, z+0f), new Vector3(x+0f, y+0f, z+1f), new Vector3(x+1f, y+0f, z+1f)});
        this.normals.AddRange(new Vector3[] {Vector3.up, Vector3.up, Vector3.up});
        this.uv.AddRange(new Vector2[] {Vector2.zero, Vector2.right, Vector2.up,Vector2.right, Vector2.up, Vector2.one});
    }
    void AddYNQuad(float x, float y, float z) {
        this.triangles.AddRange(new int[] {this.verticies.Count+0, this.verticies.Count+1, this.verticies.Count+2});
        this.verticies.AddRange(new Vector3[] {new Vector3(x+0f, y+0f, z+0f), new Vector3(x+1f, y+0f, z+0f), new Vector3(x+0f, y+0f, z+1f)});
        this.normals.AddRange(new Vector3[] {Vector3.down, Vector3.down, Vector3.down});
        this.triangles.AddRange(new int[] {this.verticies.Count+0, this.verticies.Count+2, this.verticies.Count+1});
        this.verticies.AddRange(new Vector3[] {new Vector3(x+1f, y+0f, z+0f), new Vector3(x+0f, y+0f, z+1f), new Vector3(x+1f, y+0f, z+1f)});
        this.normals.AddRange(new Vector3[] {Vector3.down, Vector3.down, Vector3.down});
        this.uv.AddRange(new Vector2[] {Vector2.zero, Vector2.right, Vector2.up,Vector2.right, Vector2.up, Vector2.one});
    }
    void AddXPQuad(float x, float y, float z) {
        this.triangles.AddRange(new int[] {this.verticies.Count+0, this.verticies.Count+1, this.verticies.Count+2});
        this.verticies.AddRange(new Vector3[] {new Vector3(x+0f, y+0f, z+0f), new Vector3(x+0f, y+1f, z+0f), new Vector3(x+0f, y+0f, z+1f)});
        this.normals.AddRange(new Vector3[] {Vector3.left, Vector3.left, Vector3.left});
        this.triangles.AddRange(new int[] {this.verticies.Count+0, this.verticies.Count+2, this.verticies.Count+1});
        this.verticies.AddRange(new Vector3[] {new Vector3(x+0f, y+1f, z+0f), new Vector3(x+0f, y+0f, z+1f), new Vector3(x+0f, y+1f, z+1f)});
        this.normals.AddRange(new Vector3[] {Vector3.left, Vector3.left, Vector3.left});
        this.uv.AddRange(new Vector2[] {Vector2.zero, Vector2.right, Vector2.up,Vector2.right, Vector2.up, Vector2.one});
    }
    void AddXNQuad(float x, float y, float z) {
        this.triangles.AddRange(new int[] {this.verticies.Count+0, this.verticies.Count+2, this.verticies.Count+1});
        this.verticies.AddRange(new Vector3[] {new Vector3(x+0f, y+0f, z+0f), new Vector3(x+0f, y+1f, z+0f), new Vector3(x+0f, y+0f, z+1f)});
        this.normals.AddRange(new Vector3[] {Vector3.right, Vector3.right, Vector3.right});
        this.triangles.AddRange(new int[] {this.verticies.Count+0, this.verticies.Count+1, this.verticies.Count+2});
        this.verticies.AddRange(new Vector3[] {new Vector3(x+0f, y+1f, z+0f), new Vector3(x+0f, y+0f, z+1f), new Vector3(x+0f, y+1f, z+1f)});
        this.normals.AddRange(new Vector3[] {Vector3.right, Vector3.right, Vector3.right});
        this.uv.AddRange(new Vector2[] {Vector2.zero, Vector2.right, Vector2.up,Vector2.right, Vector2.up, Vector2.one});
    }
    void AddZPQuad(float x, float y, float z) {
        this.triangles.AddRange(new int[] {this.verticies.Count+0, this.verticies.Count+1, this.verticies.Count+2});
        this.verticies.AddRange(new Vector3[] {new Vector3(x+0f, y+0f, z+0f), new Vector3(x+1f, y+0f, z+0f), new Vector3(x+0f, y+1f, z+0f)});
        this.normals.AddRange(new Vector3[] {Vector3.forward, Vector3.forward, Vector3.forward});
        this.triangles.AddRange(new int[] {this.verticies.Count+0, this.verticies.Count+2, this.verticies.Count+1});
        this.verticies.AddRange(new Vector3[] {new Vector3(x+1f, y+0f, z+0f), new Vector3(x+0f, y+1f, z+0f), new Vector3(x+1f, y+1f, z+0f)});
        this.normals.AddRange(new Vector3[] {Vector3.forward, Vector3.forward, Vector3.forward});
        this.uv.AddRange(new Vector2[] {Vector2.zero, Vector2.right, Vector2.up,Vector2.right, Vector2.up, Vector2.one});
    }
    void AddZNQuad(float x, float y, float z) {
        this.triangles.AddRange(new int[] {this.verticies.Count+0, this.verticies.Count+2, this.verticies.Count+1});
        this.verticies.AddRange(new Vector3[] {new Vector3(x+0f, y+0f, z+0f), new Vector3(x+1f, y+0f, z+0f), new Vector3(x+0f, y+1f, z+0f)});
        this.normals.AddRange(new Vector3[] {Vector3.back, Vector3.back, Vector3.back});
        this.triangles.AddRange(new int[] {this.verticies.Count+0, this.verticies.Count+1, this.verticies.Count+2});
        this.verticies.AddRange(new Vector3[] {new Vector3(x+1f, y+0f, z+0f), new Vector3(x+0f, y+1f, z+0f), new Vector3(x+1f, y+1f, z+0f)});
        this.normals.AddRange(new Vector3[] {Vector3.back, Vector3.back, Vector3.back});
        this.uv.AddRange(new Vector2[] {Vector2.zero, Vector2.right, Vector2.up,Vector2.right, Vector2.up, Vector2.one});
    }
}
