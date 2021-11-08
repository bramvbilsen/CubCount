using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeController : MonoBehaviour
{
    
    private float moveSpeed = 0.5f;
    private float scrollSpeed = 10f;

    private float y = 0f;

    private bool turnLeft;
    private bool turnRight;
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
    // Start is called before the first frame update
    void Start()
    {
        turnLeft = false;
        turnRight = false;
    }

    // Update is called once per frame
    void Update()
    {

        Color myColor = new Color();
        ColorUtility.TryParseHtmlString("#f2d9a1", out myColor);
        
        //Spawn block
        if(Input.GetKeyDown(KeyCode.X)) {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.parent = this.transform;
            cube.transform.position = new Vector3(0, 0.5f, 0);
            cube.GetComponent<Renderer> ().material.color = myColor;

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
