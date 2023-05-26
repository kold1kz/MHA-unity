using UnityEngine;

public class Parallax_background : MonoBehaviour
{ 	 
    public GameObject cam;
    
    float startPosX;
    float startPosY;
    
    public float parallax;

    
    void Start()
    {
    	startPosX = transform.position.x;    
    	startPosY = transform.position.y;  
    }

    // Update is called once per frame
    void Update()
    {
        float distX = (cam.transform.position.x * (1 - parallax));
        // float distY = (cam.transform.position.y * (1 - parallax));
		transform.position = new Vector3(startPosX + distX, startPosY, transform.position.z); 
	}
}
