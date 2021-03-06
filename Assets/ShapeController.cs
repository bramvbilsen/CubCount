using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;
using System;
using System.IO;
using UnityEngine.UI;

public class ShapeController : MonoBehaviour
{

    private float x1;
    private float x2;
    private UnityEngine.MeshFilter bruh;
    private UnityEngine.MeshRenderer unity;
    private UnityEngine.BoxCollider has;
    private UnityEngine.SphereCollider bugs;

    public Material material;

    int level;
    public GameObject shape1;
    public float HalfSize1;
    public GameObject shape2;
    public float HalfSize2;

    public GameObject shape3;
    public float HalfSize3;

    public GameObject shape4;
    public float HalfSize4;

    public GameObject shape5;
    public float HalfSize5;

    public GameObject shape6;
    public float HalfSize6;

    public GameObject shape7;
    public float HalfSize7;

    public GameObject shape8;
    public float HalfSize8;

    public GameObject shape9;
    public float HalfSize9;

    private float currentHalfSize;

    
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
            voxelizedMesh.HalfSize = currentHalfSize;
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
                    int vote=0;
                    if(inside(pos,new Vector3(-10,-1,-1),layerMask)){
                        vote++;
                    }
                    if(inside(pos,new Vector3(-1,-10,-1),layerMask)){
                        vote++;
                    }
                    if(inside(pos,new Vector3(-1,-1,-10),layerMask)){
                        vote++;
                    }
                    if(inside(pos,new Vector3(10,-1,-1),layerMask)){
                        vote++;
                    }
                    if(inside(pos,new Vector3(-1,10,-1),layerMask)){
                        vote++;
                    }
                    if(inside(pos,new Vector3(-1,-1,10),layerMask)){
                        vote++;
                    }
                    if (Physics.CheckBox(pos, new Vector3(halfSize, halfSize, halfSize))){
                        vote++;
                        vote++;
                        vote++;
                        vote++;
                    }
                    if(vote>=4){
                        voxelizedMesh.GridPoints.Add(new Vector3Int(x, y, z));
                        amount++;
                    }
                }
            }
        }
        return voxelizedMesh;
    }

    public void spawnModel(GameObject suzanne){
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
        spawnedSuzanne.transform.SetParent(this.transform);
        spawnVoxilizedMesh(spawnedSuzanne);
        Destroy(spawnedSuzanne);
        State.timer = 0.0f;
        State.nbTries = 0;
        State.currentGuesses = new List<int>();
        Text levelTxt = GameObject.Find("LevelTxt").GetComponent<Text>();
        if (State.CurrentLevel == 1) {
            levelTxt.text = "Guess\nthe number of cubes";
        } else {
            levelTxt.text = "Level " + State.CurrentLevel;
        }
    }

    private void SpawnCurrentLevelFile() {
        TextAsset txtData = (TextAsset)Resources.Load("Level" + State.CurrentLevel, typeof(TextAsset));
        StringReader reader = new StringReader(txtData.text);
        String line;
        line = reader.ReadLine();
        float size = float.Parse(line);
        int i = 0;
        while((line = reader.ReadLine()) != null){
            i++;
            string[] lineArr = line.Split(',');
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.parent = this.transform;
            cube.transform.position = new Vector3(float.Parse(lineArr[0]),float.Parse(lineArr[1]),float.Parse(lineArr[2]));
            cube.transform.localScale = new Vector3(size*0.90f,size*0.90f,size*0.90f);
            cube.GetComponent<Renderer> ().material = material;
            // cube.GetComponent<Renderer> ().material.color = myColor;
            cube.layer = 10;
            State.shapeCubes.Add(cube);
        }
        Debug.Log(i);
        // Debug.Log(State.shapeCubes.Count);
    }

    public void spawnVoxilizedMesh(GameObject go){
        int layerMask = 1 << 10;
        layerMask = ~layerMask;
        VoxelizedMesh voxels = VoxelizeMesh(yourMesh,layerMask,out int amount);
        Debug.Log(amount);
        
        float size = voxels.HalfSize *2f;

        State.shapeCubes = new List<GameObject>();

        // string path = "Assets/Resources/Level" + State.CurrentLevel + ".txt";
        // StreamWriter writer = new StreamWriter(path, true);
        // writer.WriteLine(((float)(size)).ToString());

        foreach(Vector3Int gp in voxels.GridPoints){
            Vector3 worldPos = voxels.PointToPosition(gp);

            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.parent = this.transform;
            cube.transform.position = new Vector3(offset.x+worldPos.x,offset.y+worldPos.y,offset.z+worldPos.z);
            cube.transform.localScale = new Vector3(size*0.90f,size*0.90f,size*0.90f);
            cube.GetComponent<Renderer> ().material = material;
            cube.layer = 10;
            State.shapeCubes.Add(cube);

            // writer.WriteLine(((float)(offset.x+worldPos.x)).ToString() + "," + ((float)(offset.y+worldPos.y)).ToString() + "," + ((float)(offset.z+worldPos.z)).ToString());
        }

        // writer.Close();
        
    }

    public bool inside(Vector3 Goal, Vector3 Start,int layerMask) {
         Vector3 Point;
        //  Vector3 Goal = transform.position; // This is the point we want to determine whether or not is inside or outside the collider.
         Vector3 Direction = Goal-Start; // This is the direction from start to goal.
         Direction.Normalize();
         int Itterations = 0; // If we know how many times the raycast has hit faces on its way to the target and back, we can tell through logic whether or not it is inside.
         Point = Start;
         int hangs = 0;
 
         while(Point != Goal) // Try to reach the point starting from the far off point.  This will pass through faces to reach its objective.
         {
             hangs++;
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
             if (hangs>25){
                 Itterations = 2;
                 break;
             }

         }
         while(Point != Start) // Try to return to where we came from, this will make sure we see all the back faces too.
         {
             hangs++;
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
            if (hangs>25){
                 Debug.Log("hangs:" + hangs);
                 Itterations = 2;
                 break;
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
        level = State.CurrentLevel;
        turnLeft = false;
        turnRight = false;
        switch (State.CurrentLevel){
            case 1:
                currentHalfSize = HalfSize1;
                spawnModel(shape1);
                break;
            case 2:
                currentHalfSize = HalfSize2;
                spawnModel(shape2);
                break;
            case 3:
                currentHalfSize = HalfSize3;
                spawnModel(shape3);
                break;
            case 4:
                currentHalfSize = HalfSize4;
                spawnModel(shape4);
                break;
            case 5:
                currentHalfSize = HalfSize5;
                spawnModel(shape5);
                break;
            case 6:
                currentHalfSize = HalfSize6;
                spawnModel(shape6);
                break;
            case 7:
                currentHalfSize = HalfSize7;
                spawnModel(shape7);
                break;
            case 8:
                currentHalfSize = HalfSize8;
                spawnModel(shape8);
                break;
            case 9:
                currentHalfSize = HalfSize9;
                spawnModel(shape9);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {        

        if (level != State.CurrentLevel) {
            level = State.CurrentLevel;
            switch (State.CurrentLevel){
                case 1:
                    currentHalfSize = HalfSize1;
                    spawnModel(shape1);
                    break;
                case 2:
                    currentHalfSize = HalfSize2;
                    spawnModel(shape2);
                    break;
                case 3:
                    currentHalfSize = HalfSize3;
                    spawnModel(shape3);
                    break;
                case 4:
                    currentHalfSize = HalfSize4;
                    spawnModel(shape4);
                    break;
                case 5:
                    currentHalfSize = HalfSize5;
                    spawnModel(shape5);
                    break;
                case 6:
                    currentHalfSize = HalfSize6;
                    spawnModel(shape6);
                    break;
                case 7:
                    currentHalfSize = HalfSize7;
                    spawnModel(shape7);
                    break;
                case 8:
                    currentHalfSize = HalfSize8;
                    spawnModel(shape8);
                    break;
                case 9:
                    currentHalfSize = HalfSize9;
                    spawnModel(shape9);
                    break;
            }
    
        }

        if (Input.GetMouseButton(0))
        {
            float rotationX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
            float rotationY = Input.GetAxis("Mouse Y") * rotSpeed * Mathf.Deg2Rad;
            transform.Rotate(Vector3.up, -rotationX,Space.World);
            transform.Rotate(Vector3.right,rotationY,Space.World);
        }
        ShapeRotation();
    }

    


}
