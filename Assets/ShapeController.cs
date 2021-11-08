using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeController : MonoBehaviour
{
    
    private float moveSpeed = 0.5f;
    private float scrollSpeed = 10f;

    private float y = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Spawn block
        if(Input.GetKeyDown(KeyCode.X)) {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = new Vector3(0, 0.5f, 0);
        }

        y = 5 + y;
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) {
            transform.localRotation = Quaternion.Euler(0, y, 0);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            y += 45;
            transform.localRotation = Quaternion.Euler(0, y , 0);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            y -= 45;
            transform.localRotation = Quaternion.Euler(0, y, 0);
        }

        
    }
}
