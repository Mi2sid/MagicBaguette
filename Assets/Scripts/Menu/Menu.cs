using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement; // Nécessaire pour gérer les scènes

public class Menu : MonoBehaviour
{
    // Fonction pour lancer une scène (le jeu par exemple)
    public void StartGame()
    {
        SceneManager.LoadScene("InGame"); // Remplace "Game" par le nom exact de ta scène de jeu
    }

    // Fonction pour quitter le jeu
    public void QuitGame()
    {
        Application.Quit(); // Quitte l'application (fonctionne uniquement dans une build)
        Debug.Log("Le jeu a été quitté"); // Utile pour tester dans l'éditeur (car Application.Quit() ne fonctionne pas en mode Play)
    }
}
