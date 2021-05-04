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
    public Camera myCamera;

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

            if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height- edgeThickness)
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
            //Limitar el movimiento de la camara
            pos.x = Mathf.Clamp(pos.x, -cameraLimit.x, cameraLimit.x);
            pos.z = Mathf.Clamp(pos.z, -(cameraLimit.y + 10), cameraLimit.y);
            transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * movementTime);



            //Fijar variable rotationAmount a 20
            rotacionProgresivaCamara();
            //Fijar variable rotationAmount a 90
            //rotacionFijaCamara();


            //zoom
            float scroll = Input.GetAxis("Mouse ScrollWheel");

            //myCamera.orthographicSize -= scroll * scrollSpeed * 100f * Time.deltaTime;
            scroll = Mathf.Clamp(myCamera.orthographicSize - scroll * scrollSpeed * 100f * Time.deltaTime, minYScroll, maxYScroll);

            myCamera.orthographicSize = scroll;

        }

    }

    private void rotacionProgresivaCamara()
    {
        Quaternion rotation = new Quaternion();
        rotation = transform.rotation;
        if (Input.GetKey(KeyCode.Q))
        {
            rotation *= Quaternion.Euler(Vector3.up * rotationAmount);
        }
        if (Input.GetKey(KeyCode.E))
        {
            rotation *= Quaternion.Euler(Vector3.up * -rotationAmount);

        }

        //Rotacion progresiva
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime);
    }

    private void rotacionFijaCamara()
    {
        Quaternion rotation = new Quaternion();
        rotation = transform.rotation;
        if (Input.GetKeyDown(KeyCode.Q))
        {
            rotation *= Quaternion.Euler(Vector3.up * rotationAmount);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            rotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
        }


        //--Rotacion estatica a 90 grados siempre pasara por los mismos 4 puntos
        transform.rotation = rotation;
    }


}
