using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class ScreamerLunch : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Le Video Player
    public GameObject rawImage; // Le GameObject du RawImage
    public float videoDuration = 10f; // Durée avant de masquer la vidéo

    private void Start()
    {
        if (rawImage != null)
            rawImage.SetActive(false);

        if (videoPlayer != null)
            videoPlayer.gameObject.SetActive(false);
    }

    public void PlayVideo()
    {
        Debug.Log("Lecture de la vidéo...");

        if (videoPlayer == null || rawImage == null)
        {
            Debug.LogError("VideoPlayer ou RawImage n'est pas assigné !");
            return;
        }

        rawImage.SetActive(true);
        videoPlayer.gameObject.SetActive(true);
        videoPlayer.Play();

        // Planifier la désactivation après la durée définie
        Invoke("HideVideo", videoDuration);
    }

    private void HideVideo()
    {
        Debug.Log("Masquage de la vidéo...");
        videoPlayer.Stop();
        rawImage.SetActive(false);
        videoPlayer.gameObject.SetActive(false);
    }
    
    public void LunchSGameScene()
    {
        Debug.Log("Lancement de la scène de jeu...");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
    
    public void StopGame()
    {
        Debug.Log("Arrêt du jeu...");
        Application.Quit();
    }
    
    
}
