using UnityEngine;

public abstract class Estructura : MonoBehaviour
{
    public int NivelMaximo;
    public int nivelActual;
    public int nivelCastilloParaConstruir;
    // todos estos atributos dependen del nivel al cual esta
    public int[] nivelMinimoCastilloParaMejorar;
    public int[] vidaPorNivel;
    [Range(0, 1)]// min y max de la variable
    [SerializeField] // para que se pueda modificar del editor
    public float vidaActual = 1f;// entre 0 y 1 // la vida a mostrar será multiplicar este valor por la vida del nivel correspondiente
    public int[] costeOroConstruirMejorar; // el primer valor es el de construir, los siguientes el de mejorar

    public abstract void mejorar();

    


}
