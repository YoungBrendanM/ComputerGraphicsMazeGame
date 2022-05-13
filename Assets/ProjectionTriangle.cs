using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ProjectionTriangle : MonoBehaviour
{

    private List<Vector3> verticies;
    private List<int> triangles;
    private List<Vector3> normals;
    private List<Vector2> uv;

    private Maze2DTriangle maze;
    void OnEnable () {
		var mesh = new Mesh {
			name = "Procedural Mesh"
		};
        this.verticies = new List<Vector3>();
        this.triangles = new List<int>();
        this.normals = new List<Vector3>();
        this.uv = new List<Vector2>();

        this.maze = new Maze2DTriangle(14,14);
        int i=0;
        int j=0;
        this.AddXNQuad(0,0,0);
        this.AddZNQuad(0,0,0);
        this.AddYPQuad(0,1,0);
        for(j = 0; j<this.maze.maze[1].GetLength(0)+this.maze.maze[0].GetLength(0); j++ ) {
            this.AddZNQuad(j*2+1,0,0);
            this.AddZNQuad(j*2+2,0,0);
            this.AddYPQuad(j*2+1,1,0);
            this.AddYPQuad(j*2+2,1,0);
            if(j%2==1) {
                this.AddZPQuad(j*2+1,0,1);
                this.AddZPQuad(j*2+2,0,1);
            }
        }
        this.AddZNQuad(j*2+1,0,0);
        this.AddXPQuad(j*2+2,0,0);
        this.AddYPQuad(j*2+1,1,0);
        for(i=0; i<this.maze.maze.GetLength(0); i+=4) {
            this.AddXNQuad(0,0,i*2+1);
            this.AddXNQuad(0,0,i*2+2);
            this.AddXNQuad(0,0,i*2+3);
            this.AddYPQuad(0,1,i*2+1);
            this.AddYPQuad(0,1,i*2+2);
            this.AddYPQuad(0,1,i*2+3);
            j=0;

            this.AddYPBLTri(j*2+1,1,i*2+2);
            this.AddYPQuad(j*2+1,1,i*2+1);
            if(!this.maze.maze[i+1][j].IsConnectedByWall(this.maze.maze[i][j])) {
                this.AddYPBLTriSmall(j*2+2,1,i*2+1);
            }
            else {
                this.AddYPBRTri(j*2+2,1,i*2+2);
                this.AddYPQuad(j*2+2,1,i*2+1);
            }
            
            for(j=0; j<this.maze.maze[i].GetLength(0); j++ ) {
                if(!this.maze.maze[i+1][j].IsConnectedByWall(this.maze.maze[i][j])) {
                    this.AddYPTRTriSmall(j*4+3,1,i*2+3);
                }
                else {   
                    this.AddYPTLTri(j*4+3,1,i*2+1);
                    this.AddYPQuad(j*4+3,1,i*2+3);
                }
                if(!this.maze.maze[i+1][j+1].IsConnectedByWall(this.maze.maze[i][j])) {
                    this.AddYPTLTriSmall(j*4+4,1,i*2+3);
                    this.AddYPBRTriSmall(j*4+5,1,i*2+1);
                }
                else{
                    this.AddYPTRTri(j*4+4,1,i*2+1);
                    this.AddYPBLTri(j*4+5,1,i*2+2);
                    this.AddYPQuad(j*4+4,1,i*2+3);
                    this.AddYPQuad(j*4+5,1,i*2+1);
                }
                if(j+1<this.maze.maze[i].GetLength(0) && !this.maze.maze[i+1][j+1].IsConnectedByWall(this.maze.maze[i][j+1])) {
                    this.AddYPBLTriSmall(j*4+6,1,i*2+1);
                }
                else {
                    this.AddYPBRTri(j*4+6,1,i*2+2);
                    this.AddYPQuad(j*4+6,1,i*2+1);
                }
            }
            
            this.AddXPQuad((j-1)*4+8,0,i*2+1);
            this.AddXPQuad((j-1)*4+8,0,i*2+2);
            this.AddXPQuad((j-1)*4+8,0,i*2+3);
            this.AddYPQuad((j-1)*4+7,1,i*2+1);
            this.AddYPQuad((j-1)*4+7,1,i*2+2);
            this.AddYPQuad((j-1)*4+7,1,i*2+3);

            if(i+2<this.maze.maze.GetLength(0)) {
                this.AddXNQuad(0,0,i*2+4);
                this.AddYPQuad(0,1,i*2+4);
                for(j = 0; j<this.maze.maze[1].GetLength(0)+this.maze.maze[0].GetLength(0); j++ ) {
                    if(j%2==1 || this.maze.maze[i+1][j/2].IsConnectedByWall(this.maze.maze[i+2][j/2])) {
                        this.AddYPQuad(j*2+1,1,i*2+4);
                        this.AddYPQuad(j*2+2,1,i*2+4);
                        if(j%2==0) {
                            this.AddZPQuad(j*2+1,0,i*2+5);
                            this.AddZPQuad(j*2+2,0,i*2+5);
                            this.AddZNQuad(j*2+1,0,i*2+4);
                            this.AddZNQuad(j*2+2,0,i*2+4);
                        }
                    }
                    else {
                        this.AddXNQuad(j*2+3,0,i*2+4);
                        this.AddXPQuad(j*2+1,0,i*2+4);
                    }
                }
                this.AddYPQuad(j*2+1,1,i*2+4);
                this.AddXPQuad(j*2+2,0,i*2+4);

                this.AddXNQuad(0,0,i*2+5);
                this.AddXNQuad(0,0,i*2+6);
                this.AddXNQuad(0,0,i*2+7);
                this.AddYPQuad(0,1,i*2+5);
                this.AddYPQuad(0,1,i*2+6);
                this.AddYPQuad(0,1,i*2+7);
                j=0;

                
                if(!this.maze.maze[i+2][j].IsConnectedByWall(this.maze.maze[i+3][j])) {
                    this.AddYPTLTriSmall(j*2+2,1,i*2+7);
                }
                else {
                    this.AddYPTRTri(j*2+2,1,i*2+5);
                    this.AddYPQuad(j*2+2,1,i*2+7);
                }

                this.AddYPTLTri(j*2+1,1,i*2+5);
                this.AddYPQuad(j*2+1,1,i*2+7);
                
                for(j = 0; j<this.maze.maze[0].GetLength(0); j++ ) {
                    if(!this.maze.maze[i+2][j].IsConnectedByWall(this.maze.maze[i+3][j])) {
                        this.AddYPBRTriSmall(j*4+3,1,i*2+5);
                    }
                    else {
                        this.AddYPBLTri(j*4+3,1,i*2+6);
                        this.AddYPQuad(j*4+3,1,i*2+5);
                    }
                    if(!this.maze.maze[i+2][j+1].IsConnectedByWall(this.maze.maze[i+3][j])) {
                        this.AddYPBLTriSmall(j*4+4,1,i*2+5);
                        this.AddYPTRTriSmall(j*4+5,1,i*2+7);
                    }
                    else{
                        this.AddYPBRTri(j*4+4,1,i*2+6);
                        this.AddYPQuad(j*4+4,1,i*2+5);
                        this.AddYPTLTri(j*4+5,1,i*2+5);
                        this.AddYPQuad(j*4+5,1,i*2+7);
                    }
                    if(j+1<this.maze.maze[i].GetLength(0) && !this.maze.maze[i+2][j+1].IsConnectedByWall(this.maze.maze[i+3][j+1])) {
                        this.AddYPTLTriSmall(j*4+6,1,i*2+7);
                    }
                    else {
                        this.AddYPTRTri(j*4+6,1,i*2+5);
                        this.AddYPQuad(j*4+6,1,i*2+7);
                    }
                }
                this.AddXPQuad((j-1)*4+8,0,i*2+5);
                this.AddXPQuad((j-1)*4+8,0,i*2+6);
                this.AddXPQuad((j-1)*4+8,0,i*2+7);
                this.AddYPQuad((j-1)*4+7,1,i*2+5);
                this.AddYPQuad((j-1)*4+7,1,i*2+6);
                this.AddYPQuad((j-1)*4+7,1,i*2+7);
                if(i+4<this.maze.maze.GetLength(0)) {
                    this.AddXNQuad(0,0,i*2+8);
                    this.AddYPQuad(0,1,i*2+8);
                    for(j = 0; j<this.maze.maze[1].GetLength(0)+this.maze.maze[0].GetLength(0); j++ ) {
                        if(j%2==0 || this.maze.maze[i+3][(j-1)/2].IsConnectedByWall(this.maze.maze[i+4][(j-1)/2])) {
                            this.AddYPQuad(j*2+1,1,i*2+8);
                            this.AddYPQuad(j*2+2,1,i*2+8);
                            if(j%2==1) {
                                this.AddZPQuad(j*2+1,0,i*2+9);
                                this.AddZPQuad(j*2+2,0,i*2+9);
                                this.AddZNQuad(j*2+1,0,i*2+8);
                                this.AddZNQuad(j*2+2,0,i*2+8);
                            }
                        }
                        else {
                            this.AddXNQuad(j*2+3,0,i*2+8);
                            this.AddXPQuad(j*2+1,0,i*2+8);
                        }
                    }
                    this.AddYPQuad(j*2+1,1,i*2+8);
                    this.AddXPQuad(j*2+2,0,i*2+8);
                }
            }
        }
        if(this.maze.maze.GetLength(0)%4==0){
            this.AddYPQuad(0,1,i*2);
            this.AddZPQuad(0,0,i*2+1);
            this.AddXNQuad(0,0,i*2);
            for(j = 0; j<this.maze.maze[1].GetLength(0)+this.maze.maze[0].GetLength(0); j++ ) {
                this.AddYPQuad(j*2+1,1,i*2);
                this.AddYPQuad(j*2+2,1,i*2);
                this.AddZPQuad(j*2+1,0,i*2+1);
                this.AddZPQuad(j*2+2,0,i*2+1);
                if(j%2==1) {
                    this.AddZNQuad(j*2+1,0,i*2);
                    this.AddZNQuad(j*2+2,0,i*2);
                }
            }
            this.AddYPQuad(j*2+1,1,i*2);
            this.AddZPQuad(j*2+1,0,i*2+1);
            this.AddXPQuad(j*2+2,0,i*2);
        }
        else {
            this.AddYPQuad(0,1,i*2-4);
            for(j = 0; j<this.maze.maze[1].GetLength(0)+this.maze.maze[0].GetLength(0); j++ ) {
                this.AddYPQuad(j*2+1,1,i*2-4);
                this.AddYPQuad(j*2+2,1,i*2-4);
            }
            this.AddYPQuad(j*2+1,1,i*2-4);
        }

		mesh.vertices = this.verticies.ToArray();

        mesh.triangles = this.triangles.ToArray();
        mesh.normals = this.normals.ToArray();
        mesh.uv = this.uv.ToArray();
		GetComponent<MeshFilter>().mesh = mesh;


        //https://forum.unity.com/threads/attach-mesh-collider-via-script-at-runtime.301918/
        // The above forum post is the source of this snippet.  This grabs the game object and adds a mesh collider
        Component[] meshrenderer;
        MeshCollider mc = gameObject.AddComponent<MeshCollider>();
        meshrenderer = GetComponentsInChildren(typeof(MeshRenderer));
        if (meshrenderer != null)
        {
            foreach (MeshRenderer rend in meshrenderer)
            {
                rend.gameObject.AddComponent<MeshCollider>();
                mc.convex = true;
            }
        }
    }
    
    void AddYPBLTri(float x, float y, float z) {
        this.triangles.AddRange(new int[] {this.verticies.Count+0, this.verticies.Count+2, this.verticies.Count+1});
        this.verticies.AddRange(new Vector3[] {new Vector3(x+0f, y+0f, z+0f), new Vector3(x+1f, y+0f, z+0f), new Vector3(x+0f, y+0f, z+2f)});
        this.normals.AddRange(new Vector3[] {Vector3.up, Vector3.up, Vector3.up});
        this.uv.AddRange(new Vector2[] {Vector2.zero, Vector2.right, 2*Vector2.up});
        this.triangles.AddRange(new int[] {this.verticies.Count+0, this.verticies.Count+1, this.verticies.Count+2});
        this.verticies.AddRange(new Vector3[] {new Vector3(x+0f, y-1f, z+2f), new Vector3(x+1f, y-1f, z+0f), new Vector3(x+0f, y+0f, z+2f)});
        this.normals.AddRange(new Vector3[] {Vector3.forward, Vector3.forward, Vector3.forward});
        this.triangles.AddRange(new int[] {this.verticies.Count+0, this.verticies.Count+2, this.verticies.Count+1});
        this.verticies.AddRange(new Vector3[] {new Vector3(x+1f, y-1f, z+0f), new Vector3(x+0f, y+0f, z+2f), new Vector3(x+1f, y+0f, z+0f)});
        this.normals.AddRange(new Vector3[] {Vector3.forward, Vector3.forward, Vector3.forward});
        this.uv.AddRange(new Vector2[] {2*Vector2.right, Vector2.zero, Vector2.one+Vector2.right,Vector2.zero, Vector2.one+Vector2.right, Vector2.up});
    }

    void AddYPBRTri(float x, float y, float z) {
        this.triangles.AddRange(new int[] {this.verticies.Count+0, this.verticies.Count+2, this.verticies.Count+1});
        this.verticies.AddRange(new Vector3[] {new Vector3(x+0f, y+0f, z+0f), new Vector3(x+1f, y+0f, z+0f), new Vector3(x+1f, y+0f, z+2f)});
        this.normals.AddRange(new Vector3[] {Vector3.up, Vector3.up, Vector3.up});
        this.uv.AddRange(new Vector2[] {Vector2.zero, Vector2.right, Vector2.one+Vector2.up});
        this.triangles.AddRange(new int[] {this.verticies.Count+0, this.verticies.Count+2, this.verticies.Count+1});
        this.verticies.AddRange(new Vector3[] {new Vector3(x+0f, y-1f, z+0f), new Vector3(x+0f, y+0f, z+0f), new Vector3(x+1f, y-1f, z+2f)});
        this.normals.AddRange(new Vector3[] {Vector3.right, Vector3.right, Vector3.right});
        this.triangles.AddRange(new int[] {this.verticies.Count+0, this.verticies.Count+1, this.verticies.Count+2});
        this.verticies.AddRange(new Vector3[] {new Vector3(x+0f, y+0f, z+0f), new Vector3(x+1f, y-1f, z+2f), new Vector3(x+1f, y+0f, z+2f)});
        this.normals.AddRange(new Vector3[] {Vector3.right, Vector3.right, Vector3.right});
        this.uv.AddRange(new Vector2[] {Vector2.right*2, Vector2.one+Vector2.right, Vector2.zero,Vector2.one+Vector2.right, Vector2.zero, Vector2.up});
    }

    void AddYPTRTri(float x, float y, float z) {
        this.triangles.AddRange(new int[] {this.verticies.Count+0, this.verticies.Count+2, this.verticies.Count+1});
        this.verticies.AddRange(new Vector3[] {new Vector3(x+1f, y+0f, z+0f), new Vector3(x+1f, y+0f, z+2f), new Vector3(x+0f, y+0f, z+2f)});
        this.normals.AddRange(new Vector3[] {Vector3.up, Vector3.up, Vector3.up});
        this.uv.AddRange(new Vector2[] {Vector2.right, Vector2.one+Vector2.up, 2*Vector2.up});
        this.triangles.AddRange(new int[] {this.verticies.Count+0, this.verticies.Count+2, this.verticies.Count+1});
        this.verticies.AddRange(new Vector3[] {new Vector3(x+0f, y-1f, z+2f), new Vector3(x+1f, y-1f, z+0f), new Vector3(x+0f, y+0f, z+2f)});
        this.normals.AddRange(new Vector3[] {Vector3.back, Vector3.back, Vector3.back});
        this.triangles.AddRange(new int[] {this.verticies.Count+0, this.verticies.Count+1, this.verticies.Count+2});
        this.verticies.AddRange(new Vector3[] {new Vector3(x+1f, y-1f, z+0f), new Vector3(x+0f, y+0f, z+2f), new Vector3(x+1f, y+0f, z+0f)});
        this.normals.AddRange(new Vector3[] {Vector3.back, Vector3.back, Vector3.back});
        this.uv.AddRange(new Vector2[] {Vector2.zero, 2*Vector2.right, Vector2.up,2*Vector2.right, Vector2.up, Vector2.one+Vector2.right});
    }
    
    void AddYPTLTri(float x, float y, float z) {
        this.triangles.AddRange(new int[] {this.verticies.Count+0, this.verticies.Count+1, this.verticies.Count+2});
        this.verticies.AddRange(new Vector3[] {new Vector3(x+0f, y+0f, z+2f), new Vector3(x+1f, y+0f, z+2f), new Vector3(x+0f, y+0f, z+0f)});
        this.normals.AddRange(new Vector3[] {Vector3.up, Vector3.up, Vector3.up});
        this.uv.AddRange(new Vector2[] {2*Vector2.up, Vector2.one+Vector2.up, Vector2.zero});
        this.triangles.AddRange(new int[] {this.verticies.Count+0, this.verticies.Count+1, this.verticies.Count+2});
        this.verticies.AddRange(new Vector3[] {new Vector3(x+0f, y-1f, z+0f), new Vector3(x+0f, y+0f, z+0f), new Vector3(x+1f, y-1f, z+2f)});
        this.normals.AddRange(new Vector3[] {Vector3.left, Vector3.left, Vector3.left});
        this.triangles.AddRange(new int[] {this.verticies.Count+0, this.verticies.Count+2, this.verticies.Count+1});
        this.verticies.AddRange(new Vector3[] {new Vector3(x+0f, y+0f, z+0f), new Vector3(x+1f, y+-1f, z+2f), new Vector3(x+1f, y+0f, z+2f)});
        this.normals.AddRange(new Vector3[] {Vector3.left, Vector3.left, Vector3.left});
        this.uv.AddRange(new Vector2[] {Vector2.zero, Vector2.up, 2*Vector2.right,Vector2.up, 2*Vector2.right, Vector2.one+Vector2.right});
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
        this.uv.AddRange(new Vector2[] {Vector2.zero, Vector2.up, Vector2.right,Vector2.up, Vector2.right, Vector2.one});
    }
    void AddXP45Quad(float x, float y, float z) {
        this.triangles.AddRange(new int[] {this.verticies.Count+0, this.verticies.Count+1, this.verticies.Count+2});
        this.verticies.AddRange(new Vector3[] {new Vector3(x+0f, y+0f, z+0f), new Vector3(x+0f, y+1f, z+0f), new Vector3(x+1f, y+0f, z+1f)});
        this.normals.AddRange(new Vector3[] {Vector3.left, Vector3.left, Vector3.left});
        this.triangles.AddRange(new int[] {this.verticies.Count+0, this.verticies.Count+2, this.verticies.Count+1});
        this.verticies.AddRange(new Vector3[] {new Vector3(x+0f, y+1f, z+0f), new Vector3(x+1f, y+0f, z+1f), new Vector3(x+1f, y+1f, z+1f)});
        this.normals.AddRange(new Vector3[] {Vector3.left, Vector3.left, Vector3.left});
        this.uv.AddRange(new Vector2[] {Vector2.zero, Vector2.up, Vector2.right,Vector2.up, Vector2.right, Vector2.one});
    }
    void AddXNQuad(float x, float y, float z) {
        this.triangles.AddRange(new int[] {this.verticies.Count+0, this.verticies.Count+2, this.verticies.Count+1});
        this.verticies.AddRange(new Vector3[] {new Vector3(x+0f, y+0f, z+0f), new Vector3(x+0f, y+1f, z+0f), new Vector3(x+0f, y+0f, z+1f)});
        this.normals.AddRange(new Vector3[] {Vector3.right, Vector3.right, Vector3.right});
        this.triangles.AddRange(new int[] {this.verticies.Count+0, this.verticies.Count+1, this.verticies.Count+2});
        this.verticies.AddRange(new Vector3[] {new Vector3(x+0f, y+1f, z+0f), new Vector3(x+0f, y+0f, z+1f), new Vector3(x+0f, y+1f, z+1f)});
        this.normals.AddRange(new Vector3[] {Vector3.right, Vector3.right, Vector3.right});
        this.uv.AddRange(new Vector2[] {Vector2.right, Vector2.one, Vector2.zero,Vector2.one, Vector2.zero, Vector2.up});
    }
    void AddXN45Quad(float x, float y, float z) {
        this.triangles.AddRange(new int[] {this.verticies.Count+0, this.verticies.Count+2, this.verticies.Count+1});
        this.verticies.AddRange(new Vector3[] {new Vector3(x+0f, y+0f, z+0f), new Vector3(x+0f, y+1f, z+0f), new Vector3(x+1f, y+0f, z+1f)});
        this.normals.AddRange(new Vector3[] {Vector3.right, Vector3.right, Vector3.right});
        this.triangles.AddRange(new int[] {this.verticies.Count+0, this.verticies.Count+1, this.verticies.Count+2});
        this.verticies.AddRange(new Vector3[] {new Vector3(x+0f, y+1f, z+0f), new Vector3(x+1f, y+0f, z+1f), new Vector3(x+1f, y+1f, z+1f)});
        this.normals.AddRange(new Vector3[] {Vector3.right, Vector3.right, Vector3.right});
        this.uv.AddRange(new Vector2[] {Vector2.right, Vector2.one, Vector2.zero,Vector2.one, Vector2.zero, Vector2.up});
    }
    void AddZPQuad(float x, float y, float z) {
        this.triangles.AddRange(new int[] {this.verticies.Count+0, this.verticies.Count+1, this.verticies.Count+2});
        this.verticies.AddRange(new Vector3[] {new Vector3(x+0f, y+0f, z+0f), new Vector3(x+1f, y+0f, z+0f), new Vector3(x+0f, y+1f, z+0f)});
        this.normals.AddRange(new Vector3[] {Vector3.forward, Vector3.forward, Vector3.forward});
        this.triangles.AddRange(new int[] {this.verticies.Count+0, this.verticies.Count+2, this.verticies.Count+1});
        this.verticies.AddRange(new Vector3[] {new Vector3(x+1f, y+0f, z+0f), new Vector3(x+0f, y+1f, z+0f), new Vector3(x+1f, y+1f, z+0f)});
        this.normals.AddRange(new Vector3[] {Vector3.forward, Vector3.forward, Vector3.forward});
        this.uv.AddRange(new Vector2[] {Vector2.right, Vector2.zero, Vector2.one,Vector2.zero, Vector2.one, Vector2.up});
    }
    void AddZP45Quad(float x, float y, float z) {
        this.triangles.AddRange(new int[] {this.verticies.Count+0, this.verticies.Count+1, this.verticies.Count+2});
        this.verticies.AddRange(new Vector3[] {new Vector3(x+0f, y+0f, z+1f), new Vector3(x+1f, y+0f, z+0f), new Vector3(x+0f, y+1f, z+1f)});
        this.normals.AddRange(new Vector3[] {Vector3.forward, Vector3.forward, Vector3.forward});
        this.triangles.AddRange(new int[] {this.verticies.Count+0, this.verticies.Count+2, this.verticies.Count+1});
        this.verticies.AddRange(new Vector3[] {new Vector3(x+1f, y+0f, z+0f), new Vector3(x+0f, y+1f, z+1f), new Vector3(x+1f, y+1f, z+0f)});
        this.normals.AddRange(new Vector3[] {Vector3.forward, Vector3.forward, Vector3.forward});
        this.uv.AddRange(new Vector2[] {Vector2.right, Vector2.zero, Vector2.one,Vector2.zero, Vector2.one, Vector2.up});
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
    void AddZN45Quad(float x, float y, float z) {
        this.triangles.AddRange(new int[] {this.verticies.Count+0, this.verticies.Count+2, this.verticies.Count+1});
        this.verticies.AddRange(new Vector3[] {new Vector3(x+0f, y+0f, z+1f), new Vector3(x+1f, y+0f, z+0f), new Vector3(x+0f, y+1f, z+1f)});
        this.normals.AddRange(new Vector3[] {Vector3.back, Vector3.back, Vector3.back});
        this.triangles.AddRange(new int[] {this.verticies.Count+0, this.verticies.Count+1, this.verticies.Count+2});
        this.verticies.AddRange(new Vector3[] {new Vector3(x+1f, y+0f, z+0f), new Vector3(x+0f, y+1f, z+1f), new Vector3(x+1f, y+1f, z+0f)});
        this.normals.AddRange(new Vector3[] {Vector3.back, Vector3.back, Vector3.back});
        this.uv.AddRange(new Vector2[] {Vector2.zero, Vector2.right, Vector2.up,Vector2.right, Vector2.up, Vector2.one});
    }
    
    void AddYPBLTriSmall(float x, float y, float z) {
        this.AddZP45Quad(x,y-1,z);
        this.triangles.AddRange(new int[] {this.verticies.Count+0, this.verticies.Count+2, this.verticies.Count+1});
        this.verticies.AddRange(new Vector3[] {new Vector3(x+0f, y+0f, z+0f), new Vector3(x+1f, y+0f, z+0f), new Vector3(x+0f, y+0f, z+1f)});
        this.normals.AddRange(new Vector3[] {Vector3.up, Vector3.up, Vector3.up});
        this.uv.AddRange(new Vector2[] {Vector2.zero, Vector2.right, Vector2.up});
    }

    void AddYPBRTriSmall(float x, float y, float z) {
        this.AddXN45Quad(x,y-1,z);
        this.triangles.AddRange(new int[] {this.verticies.Count+0, this.verticies.Count+2, this.verticies.Count+1});
        this.verticies.AddRange(new Vector3[] {new Vector3(x+0f, y+0f, z+0f), new Vector3(x+1f, y+0f, z+0f), new Vector3(x+1f, y+0f, z+1f)});
        this.normals.AddRange(new Vector3[] {Vector3.up, Vector3.up, Vector3.up});
        this.uv.AddRange(new Vector2[] {Vector2.zero, Vector2.right, Vector2.one});
    }

    void AddYPTRTriSmall(float x, float y, float z) {
        this.AddZN45Quad(x,y-1,z);
        this.triangles.AddRange(new int[] {this.verticies.Count+0, this.verticies.Count+2, this.verticies.Count+1});
        this.verticies.AddRange(new Vector3[] {new Vector3(x+1f, y+0f, z+0f), new Vector3(x+1f, y+0f, z+1f), new Vector3(x+0f, y+0f, z+1f)});
        this.normals.AddRange(new Vector3[] {Vector3.up, Vector3.up, Vector3.up});
        this.uv.AddRange(new Vector2[] {Vector2.right, Vector2.one, Vector2.up});
    }
    
    void AddYPTLTriSmall(float x, float y, float z) {
        this.AddXP45Quad(x,y-1,z);
        this.triangles.AddRange(new int[] {this.verticies.Count+0, this.verticies.Count+1, this.verticies.Count+2});
        this.verticies.AddRange(new Vector3[] {new Vector3(x+0f, y+0f, z+1f), new Vector3(x+1f, y+0f, z+1f), new Vector3(x+0f, y+0f, z+0f)});
        this.normals.AddRange(new Vector3[] {Vector3.up, Vector3.up, Vector3.up});
        this.uv.AddRange(new Vector2[] {Vector2.up, Vector2.one, Vector2.zero});
    }
}

