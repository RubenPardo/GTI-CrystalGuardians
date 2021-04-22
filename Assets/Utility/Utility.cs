using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Utility 
{
    // Devuelve un punto aleatorio dentro de un perimetro de un rectangulo de lado 2*distance
    public static Vector3 getPuntoPerimetroRectangulo(float distance)
    {
        // para generar un punto random dentro del perimetro de un rectangulo
        // debemos escoger un lado x o z para fijarlo a 3 o -3 (el signo es aleatorio)
        // y la otra coordenada debe ser un valor entre -3 o 3 aleatorio
        Vector3 r;
        r.y = 0;
        int ladoFijo = Random.Range(0, 2);
        if (ladoFijo == 0)
        {
            // x fijo
            int signo = Random.Range(0, 2) == 0 ? 1 : -1;
            r.x = distance * signo;
            r.z = Random.Range(-distance, distance);
        }
        else
        {
            // z fijo
            int signo = Random.Range(0, 2) == 0 ? 1 : -1;
            r.z = distance * signo;
            r.x = Random.Range(-distance, distance);
        }


        return r;
    }


    public static float getCircleRadiusByArea(float area)
    {
        // 2 pi r^2 = area, r = sqrt(area/2pi)

        return Mathf.Sqrt(area / 2 * Mathf.PI);
    }
    public static Vector3[] getPuntosEquidistribuidosDentroCirculo(float radio,int n)
    {
        Vector3[] puntos = new Vector3[n];

        Debug.Log("Puntos---------------------- ");
        int alpha = 1;
        float b = Mathf.Round(alpha * Mathf.Sqrt(n));
        float phi = (Mathf.Sqrt(5) + 1) / 2; // golden ratio
        for(int k = 0; k < n; k++)
        {
            float r = Utility.radio(k, n, b);
            float theta = (2 * Mathf.PI) / Mathf.Pow(phi, 2);

            Debug.Log("Radio: "+r);
            float x = r * Mathf.Cos(theta);
            float y = 0;
            float z = r * Mathf.Sin(theta);
            Debug.Log("Punto: " + x + " " + y + " " + z);
            puntos[k] = new Vector3(x, y, z);
        }
        return puntos;
    }

    private static float radio(int k, float n, float b)
    {
        float r;
        if (k > n - b)
            r = 1;            // put on the boundary
        else
            r = Mathf.Sqrt(k - 1 / 2) / Mathf.Sqrt(n - (b + 1) / 2);

        return Mathf.Sqrt(k - 1 / 2) / Mathf.Sqrt(n - (b + 1) / 2);
    }

    public static bool rayCastUI()
    {

        PointerEventData pointerData = new PointerEventData(EventSystem.current);

        pointerData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);
        if (results.Count > 0)
        {
            //WorldUI is my layer name
            if (results[0].gameObject.layer == LayerMask.NameToLayer("UI"))
            {
                return true;
            }
        }
        return false;
    }

    // Metodos genericos para la funcionalidad de seleccion de unidades
    static Texture2D _whiteTexture;
    public static Texture2D WhiteTexture
    {
        get
        {
            if (_whiteTexture == null)
            {
                _whiteTexture = new Texture2D(1, 1);
                _whiteTexture.SetPixel(0, 0, Color.white);
                _whiteTexture.Apply();
            }

            return _whiteTexture;
        }
    }
    public static void DrawScreenRect(Rect rect, Color color)
    {
        GUI.color = color;
        GUI.DrawTexture(rect, WhiteTexture);
        GUI.color = Color.white;
    }
    public static void DrawScreenRectBorder(Rect rect, float thickness, Color color)
    {
        // Top
        Utility.DrawScreenRect(new Rect(rect.xMin, rect.yMin, rect.width, thickness), color);
        // Left
        Utility.DrawScreenRect(new Rect(rect.xMin, rect.yMin, thickness, rect.height), color);
        // Right
        Utility.DrawScreenRect(new Rect(rect.xMax - thickness, rect.yMin, thickness, rect.height), color);
        // Bottom
        Utility.DrawScreenRect(new Rect(rect.xMin, rect.yMax - thickness, rect.width, thickness), color);
    }
    public static Rect GetScreenRect(Vector3 screenPosition1, Vector3 screenPosition2)
    {
        // Move origin from bottom left to top left
        screenPosition1.y = Screen.height - screenPosition1.y;
        screenPosition2.y = Screen.height - screenPosition2.y;
        // Calculate corners
        var topLeft = Vector3.Min(screenPosition1, screenPosition2);
        var bottomRight = Vector3.Max(screenPosition1, screenPosition2);
        // Create Rect
        return Rect.MinMaxRect(topLeft.x, topLeft.y, bottomRight.x, bottomRight.y);
    }

}
