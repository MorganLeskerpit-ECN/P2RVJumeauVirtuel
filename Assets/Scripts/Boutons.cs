using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

// Lien entre input sur un bouton et appel de la fonction associee dans Pendant.cs
// Auteurs : Eugene Castelneau et Luc Pares

public class Boutons : MonoBehaviour
{
    public GameObject pendant;
    private float amplitude = 0.5f;
    private float duration = 0.002f;

    #region Pour l'interaction 2D uniquement (utile au Debug)
    // Declenchement d'un appui avec la souris
    public void OnClick()
    {
        GameObject mouse = new GameObject();
        mouse.AddComponent<BoxCollider>();
        mouse.tag = "Mouse";
        OnTriggerEnter(mouse.GetComponent<Collider>());
    }

    // Appui prolonge avec la souris
    public void OnLongClick()
    {
        GameObject mouse = new GameObject();
        mouse.AddComponent<BoxCollider>();
        mouse.tag = "Mouse";
        OnTriggerStay(mouse.GetComponent<Collider>());
    }

    public void OnStopClick()
    {
        GameObject mouse = new GameObject();
        mouse.AddComponent<BoxCollider>();
        mouse.tag = "Mouse";
        OnTriggerExit(mouse.GetComponent<Collider>());
    }
    #endregion

    #region Interactions 2D et 3D

    // Lorsqu'on appuie sur un bouton
    private void OnTriggerEnter(Collider other)
    {
        // Si l'objet qui entre en collision avec le bouton est bien la main du joueur (ou la souris pour l'interaction 2D),
        // on envoie un retour haptique et on appelle la fonction appropriee
        if (other.tag == "Hand" || other.tag == "Mouse")
        {
            // Vibrations de la manette
            if (other.tag == "Hand")
            {
                var device = InputSystem.GetDevice<XRController>(CommonUsages.RightHand);
                var command = UnityEngine.InputSystem.XR.Haptics.SendHapticImpulseCommand.Create(0, amplitude, duration);
                device.ExecuteCommand(ref command);
            }

            // Nettoyage
            if (other.tag == "Mouse")
            {
                Destroy(other.gameObject);
            }

            string bouton = this.tag;
            Debug.Log(bouton);
            // On appelle la fonction d'appui correspondante
            switch (bouton)
            {
                case "Up":
                    pendant.GetComponent<Pendant>().OnBUpTriggerEnter();
                    break;
                case "Down":
                    pendant.GetComponent<Pendant>().OnBDownTriggerEnter();
                    break;
                case "Mode":
                    pendant.GetComponent<Pendant>().OnBModeTriggerEnter();
                    break;
                case "Plus":
                    pendant.GetComponent<Pendant>().OnBPlusTriggerEnter();
                    break;
                case "Minus":
                    pendant.GetComponent<Pendant>().OnBMinusTriggerEnter();
                    break;
                case "Play":
                    pendant.GetComponent<Pendant>().OnBPlayTriggerEnter();
                    break;
                case "FF":
                    pendant.GetComponent<Pendant>().OnBFFTriggerEnter();
                    break;
                case "STOP":
                    pendant.GetComponent<Pendant>().OnBSTOPTriggerEnter();
                    break;
            }
        }
    }

    // Pendant un appui prolonge sur un bouton
    private void OnTriggerStay(Collider other)
    {
        if (other && other.gameObject.tag == "Hand" || other.tag == "Mouse")
        {
            // Nettoyage
            if (other.tag == "Mouse")
            {
                Destroy(other.gameObject);
            }

            string bouton = this.tag;
            // On appelle la fonction d'appui prolonge correspondante
            switch (bouton)
            {
                case "Plus":
                    pendant.GetComponent<Pendant>().OnBPlusTriggerStay();
                    break;
                case "Minus":
                    pendant.GetComponent<Pendant>().OnBMinusTriggerStay();
                    break;
                case "FF":
                    pendant.GetComponent<Pendant>().OnBFFTriggerStay();
                    break;
            }
        }
    }

    // Quand on relache un bouton
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Hand" || other.tag == "Mouse")
        {
            // Nettoyage
            if (other.tag == "Mouse")
            {
                Destroy(other.gameObject);
            }

            string bouton = this.tag;
            // On appelle la fonction de relachement correspondante
            switch (bouton)
            {
                case "Plus":
                    pendant.GetComponent<Pendant>().OnBPlusTriggerExit();
                    break;
                case "Minus":
                    pendant.GetComponent<Pendant>().OnBMinusTriggerExit();
                    break;
                case "FF":
                    pendant.GetComponent<Pendant>().OnBFFTriggerExit();
                    break;
            }
        }
    }
    #endregion
}
