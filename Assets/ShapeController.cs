using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

public class ShapeController : MonoBehaviour
{
    
    private float moveSpeed = 0.5f;
    private float scrollSpeed = 10f;

    private float y = 0f;

    private bool turnLeft;
    private bool turnRight;
    private Color myColor;
    Renderer m_ObjectRenderer;
    Color tempcolor;

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

    // void OnCollisionEnter(Collision collision){
    //     if(col.gameObject.name == "Sphere"){
    //         Destroy(col.gameObject);
    //     }
    // }

    public void spawnCubes(){
        for (int i=0;i<8;i++){
            for (int j=0;j<8;j++){
                for (int k=0;k<8;k++){
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.parent = this.transform;
                    cube.transform.position = new Vector3(-4+i,-4+j, -4+k);
                    cube.GetComponent<Renderer> ().material.color = myColor;
                }
            }
        }
    }

    public void spawnCubesInSphere(){
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.localScale = new Vector3(5, 5, 5);
        //Fetch the GameObject's Renderer component
        m_ObjectRenderer = sphere.GetComponent<Renderer>();
        Color newColor = new Color(255f,255f,255f,.5f);
        m_ObjectRenderer.material.SetColor("_Color", newColor);
        // m_ObjectRenderer.material.color = new Color(255f,255f,255f,.5f);
        // //Change the GameObject's Material Color to red
        // //m_ObjectRenderer.material.color = Color.red;
        // tempcolor = m_ObjectRenderer.material.color;
        // tempcolor.a = 0.8f;
        // m_ObjectRenderer.material.color = tempcolor;


        // GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        // sphere.transform.position = new Vector3(0, 1.5f, 0);
        //sphere.GetComponent<Renderer>().material.color.a = 0.7f;

        //Vector3 center = sphere.GetComponent<Renderer>().bounds.center;

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
        myColor = new Color();
        ColorUtility.TryParseHtmlString("#f2d9a1", out myColor);
        
        //Spawn block
        if(Input.GetKeyDown(KeyCode.X)) {
            spawnCubesInSphere();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            RotateLeft();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            RotateRight();
        }

        ShapeRotation();
    }
}
