using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;
using System;

public class ShapeController : MonoBehaviour
{
    public GameObject suzanne;
    
    MeshFilter yourMesh;

    float rotSpeed = 200;
    
    private float moveSpeed = 0.5f;
    private float scrollSpeed = 10f;

    private float y = 0f;

    private bool turnLeft;
    private bool turnRight;
    private Color myColor = new Color(0.95f,0.82f,0.63f,0.6f);
    Renderer m_ObjectRenderer;
    Color tempcolor;

    Vector3 offset = new Vector3(0,0,0);

    public void RotateLeft()
    {
        turnLeft = true;
    }
    public void RotateRight()
    {
        turnRight = true;
    }

    public void ShapeRotation() {
        if (turnLeft){
             y += 45;
            transform.localRotation = Quaternion.Euler(0, y , 0);
            turnLeft = false;
        }
        else if (turnRight){
            y -= 45;
            transform.localRotation = Quaternion.Euler(0, y, 0);
            turnRight = false;
        }
    }

    public VoxelizedMesh VoxelizeMesh(MeshFilter meshFilter,int layerMask, out int amount)
    {
        if (!meshFilter.TryGetComponent(out MeshCollider meshCollider))
        {
            meshCollider = meshFilter.gameObject.AddComponent<MeshCollider>();
        }

        if (!meshFilter.TryGetComponent(out VoxelizedMesh voxelizedMesh))
        {
            voxelizedMesh = meshFilter.gameObject.AddComponent<VoxelizedMesh>();
        } else{
            Debug.Log("voxelizedmeshfout");
        }
        Bounds bounds = meshCollider.bounds;
        Vector3 minExtents = bounds.center - bounds.extents;
        float halfSize = voxelizedMesh.HalfSize;
        Vector3 count = 2* bounds.extents / halfSize;

        int xGridSize = Mathf.CeilToInt(count.x);
        int yGridSize = Mathf.CeilToInt(count.y);
        int zGridSize = Mathf.CeilToInt(count.z);

        voxelizedMesh.GridPoints.Clear();
        voxelizedMesh.LocalOrigin = voxelizedMesh.transform.InverseTransformPoint(minExtents);

        amount = 0;

        for (int x = 0; x < xGridSize; ++x)
        {
            for (int z = 0; z < zGridSize; ++z)
            {
                for (int y = 0; y < yGridSize; ++y){
                    // voxelizedMesh.GridPoints.Add(new Vector3Int(x, y, z));
                    // amount++;
                    Vector3 pos = voxelizedMesh.PointToPosition(new Vector3Int(x, y, z));
                    if(inside(pos,new Vector3(-100,-1,-1),layerMask) && inside(pos,new Vector3(-1,-100,-1),layerMask)){
                        voxelizedMesh.GridPoints.Add(new Vector3Int(x, y, z));
                        amount++;
                    }
                    else if (Physics.CheckBox(pos, new Vector3(halfSize, halfSize, halfSize)))
                    {
                        voxelizedMesh.GridPoints.Add(new Vector3Int(x, y, z));
                        amount++;
                    }
                }
            }
        }
        return voxelizedMesh;
    }

    public void spawnModel(){
        GameObject spawnedSuzanne = Instantiate(suzanne, this.transform.position, Quaternion.identity);
        if (!spawnedSuzanne.TryGetComponent(out MeshFilter meshFilter)){
            yourMesh = spawnedSuzanne.AddComponent<MeshFilter>();
        } else{
            yourMesh = spawnedSuzanne.GetComponent<MeshFilter>();
        }

        if (!yourMesh.TryGetComponent(out MeshCollider meshCollider))
        {
            meshCollider = yourMesh.gameObject.AddComponent<MeshCollider>();
        }
        Debug.Log(meshCollider.bounds.center);
        Vector3 currentpos = spawnedSuzanne.transform.position;
        offset = new Vector3(-meshCollider.bounds.center.x+currentpos.x,-meshCollider.bounds.center.y+currentpos.y,-meshCollider.bounds.center.z+currentpos.z);
        //spawnedSuzanne.transform.SetParent(this.transform);
        spawnVoxilizedMesh(spawnedSuzanne);
        Destroy(spawnedSuzanne);
        Debug.Log(this.transform.position);
    }

    public void spawnVoxilizedMesh(GameObject go){
        // if (!go.TryGetComponent(out MeshFilter meshFilter)){
        //     yourMesh = go.AddComponent<MeshFilter>();
        // } else{
        //     yourMesh = go.GetComponent<MeshFilter>();
        // }
        int layerMask = 1 << 10;
        layerMask = ~layerMask;
        VoxelizedMesh voxels = VoxelizeMesh(yourMesh,layerMask,out int amount);
        Debug.Log(amount);
        
        float size = voxels.HalfSize *2f;

        foreach(Vector3Int gp in voxels.GridPoints){
            Vector3 worldPos = voxels.PointToPosition(gp);

            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.parent = this.transform;
            cube.transform.position = new Vector3(offset.x+worldPos.x,offset.y+worldPos.y,offset.z+worldPos.z);
            cube.transform.localScale = new Vector3(size*0.90f,size*0.90f,size*0.90f);
            cube.GetComponent<Renderer> ().material.color = myColor;
            cube.GetComponent<Renderer>().material.shader = Shader.Find( "Transparent/Diffuse" );
            cube.layer = 10;
        }
        
    }

    public bool inside(Vector3 Goal, Vector3 Start,int layerMask) {
         Vector3 Point;
        //  Vector3 Goal = transform.position; // This is the point we want to determine whether or not is inside or outside the collider.
         Vector3 Direction = Goal-Start; // This is the direction from start to goal.
         Direction.Normalize();
         int Itterations = 0; // If we know how many times the raycast has hit faces on its way to the target and back, we can tell through logic whether or not it is inside.
         Point = Start;
 
 
         while(Point != Goal) // Try to reach the point starting from the far off point.  This will pass through faces to reach its objective.
         {
             RaycastHit hit;
             if( Physics.Linecast(Point, Goal, out hit, layerMask)) // Progressively move the point forward, stopping everytime we see a new plane in the way.
             {
                 Itterations ++;
                 Point = hit.point + (Direction/100.0f); // Move the Point to hit.point and push it forward just a touch to move it through the skin of the mesh (if you don't push it, it will read that same point indefinately).
             }
             else
             {
                 Point = Goal; // If there is no obstruction to our goal, then we can reach it in one step.
             }
         }
         while(Point != Start) // Try to return to where we came from, this will make sure we see all the back faces too.
         {
             RaycastHit hit;
             if( Physics.Linecast(Point, Start, out hit, layerMask))
             {
                 Itterations ++;
                 Point = hit.point + (-Direction/100.0f);
             }
             else
             {
                 Point = Start;
             }
         }
         if(Itterations % 2 == 0)
         {
             return false;
         }
         if(Itterations % 2 == 1)
         {
             return true;
         }
         else{
             return true;
         }
    }

    // Start is called before the first frame update
    void Start()
    {
        turnLeft = false;
        turnRight = false;
    }

    // Update is called once per frame
    void Update()
    {        
        //Spawn model
        if(Input.GetKeyDown(KeyCode.X)) {
            spawnModel();
        }
        if (Input.GetMouseButton(0))
        {
            float rotationX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
            float rotationY = Input.GetAxis("Mouse Y") * rotSpeed * Mathf.Deg2Rad;
            transform.Rotate(Vector3.up, -rotationX);
            transform.Rotate(Vector3.right,rotationY);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            RotateLeft();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            RotateRight();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            y=0;
        }

        ShapeRotation();
    }

    


}
