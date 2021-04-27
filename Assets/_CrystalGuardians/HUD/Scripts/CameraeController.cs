using UnityEngine;

public class CameraeController : MonoBehaviour{
    public Camera myCamera;
    public float cameraSpeed = 20f;
    public float edgeThickness = 10f;

    public Vector2 cameraLimit;

    public float scrollSpeed = 20f;
    public float minYScroll = 20f;
    public float maxYScroll=120f;
    public bool isActive = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            Vector3 pos = transform.position;
            if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - edgeThickness)
            {
                pos.z += cameraSpeed * Time.deltaTime;
                pos.x += cameraSpeed * Time.deltaTime;
            }
            if (Input.GetKey("s") || Input.mousePosition.y <= edgeThickness)
            {
                pos.z -= cameraSpeed * Time.deltaTime;
                pos.x -= cameraSpeed * Time.deltaTime;
            }

            if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - edgeThickness)
            {
                pos.x += cameraSpeed * Time.deltaTime;
                pos.z -= cameraSpeed * Time.deltaTime;

            }

            if (Input.GetKey("a") || Input.mousePosition.x <= edgeThickness)
            {
                pos.x -= cameraSpeed * Time.deltaTime;
                pos.z += cameraSpeed * Time.deltaTime;
            }

            float scroll = Input.GetAxis("Mouse ScrollWheel");

            //myCamera.orthographicSize -= scroll * scrollSpeed * 100f * Time.deltaTime;
            scroll = Mathf.Clamp(myCamera.orthographicSize - scroll * scrollSpeed * 100f * Time.deltaTime, minYScroll, maxYScroll);

            myCamera.orthographicSize = scroll;
            //pos.y -= scroll * scrollSpeed * 100f * Time.deltaTime;
            //pos.y = Mathf.Clamp(pos.y, minYScroll, maxYScroll);


            pos.x = Mathf.Clamp(pos.x, -cameraLimit.x, cameraLimit.x);
            pos.z = Mathf.Clamp(pos.z, -(cameraLimit.y + 10), cameraLimit.y);

            transform.position = pos;
        }
    }

 
}
