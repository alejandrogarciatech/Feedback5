using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoJugador : MonoBehaviour
{
    public Transform controladorDisparo;
    public GameObject bala;

    [SerializeField] private AudioClip disparoSonido;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Disparar();
            ControladorSonido.Instance.EjecutarSonido(disparoSonido);
        }
    }

    private void Disparar()
    {
        Instantiate(bala, controladorDisparo.position, controladorDisparo.rotation);
    }
}
