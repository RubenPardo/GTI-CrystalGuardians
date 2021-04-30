using UnityEngine;

public class CameraeController : MonoBehaviour
{
    //public Camera myCamera;
    public float cameraSpeed = 20f;
    public float edgeThickness = 10f;
    public float movementTime;

    public Vector2 cameraLimit;

    public float scrollSpeed = 20f;
    public float minYScroll = 20f;
    public float maxYScroll = 120f;
    public bool isActive = true;

    private Vector3 pos;

    public float rotationAmount;

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {

            if (Input.GetKey("w"))
            {
                pos += (transform.forward * cameraSpeed);
                //calibracion
                pos += (transform.right * cameraSpeed);
            }
            if (Input.GetKey("s") || Input.mousePosition.y <= edgeThickness)
            {
                pos += (transform.forward * -cameraSpeed);
                //calibracion
                pos += (transform.right * -cameraSpeed);
            }

            if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - edgeThickness)
            {

                pos += (transform.right * cameraSpeed);
                //calibracion
                pos += (transform.forward * -cameraSpeed);

            }

            if (Input.GetKey("a") || Input.mousePosition.x <= edgeThickness)
            {
                pos += (transform.right * -cameraSpeed);
                //calibracion
                pos += (transform.forward * cameraSpeed);
            }
            pos.x = Mathf.Clamp(pos.x, -cameraLimit.x, cameraLimit.x);
            pos.z = Mathf.Clamp(pos.z, -(cameraLimit.y + 10), cameraLimit.y);

            transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * movementTime);

            //rotacion de la camara

            Quaternion rotation = new Quaternion();
            rotation = transform.rotation;
            if (Input.GetKeyDown(KeyCode.Q))
            {
                //rotation.y += 80;
                rotation *= Quaternion.Euler(Vector3.up * rotationAmount);
                //otation.y += rotationAmount;
                //transform.Rotate(transform.rotation.x, transform.rotation.y+rotationAmount, transform.rotation.z);
                //transform.RotateAround(transform.position, Vector3.up, rotationAmount);
            }
            if (Input.GetKeyDown(KeyCode.E)) 
            {
                rotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
                //rotation.y -= 80;
                //transform.Rotate(transform.rotation.x, transform.rotation.y - rotationAmount, transform.rotation.z);
                //transform.RotateAround(transform.position, Vector3.down, rotationAmount);
            }
            //transform.RotateAround(transform.position, Vector3.back, -45);
            transform.rotation = rotation;
            
            //transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime);
        }
    }


}
