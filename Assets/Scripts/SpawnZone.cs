using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class SpawnZone : MonoBehaviour
{
    [SerializeField] private GameObject appleObject; // La pomme à instancier
    [SerializeField] private Vector2 spawnRangeX = new Vector2(-5f, 5f); // Limite pour X
    [SerializeField] private Vector2 spawnRangeY = new Vector2(-3f, 3f); // Limite pour Y
    [SerializeField] private float spawnInterval = 10f; // Temps entre chaque spawn
    public VideoPlayer videoPlayer; // Le Video Player
    public GameObject rawImage; // Le GameObject du RawImage
    public float videoDuration = 10f; // Durée avant de masquer la vidéo et retourner à l'écran Home

    private float spawnTimer = 0f;
    private GameObject currentApple; // La pomme actuellement active
    private bool appleTouched = false; // Indique si la pomme a été touchée
    private bool isPlayingVideo = false; // Indique si la vidéo est en cours de lecture

    private void Start()
    {
        // Désactiver le RawImage et le Video Player au lancement
        if (rawImage != null)
            rawImage.SetActive(false);

        if (videoPlayer != null)
            videoPlayer.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isPlayingVideo || videoPlayer == null) return;

        if (currentApple != null)
        {
            spawnTimer += Time.deltaTime;

            if (spawnTimer >= spawnInterval)
            {
                if (!appleTouched)
                {
                    Debug.Log("Temps écoulé, vidéo lancée !");
                    PlayVideo();
                    Destroy(currentApple);
                }

                ResetCycle();
            }
        }
        else
        {
            spawnTimer += Time.deltaTime;

            if (spawnTimer >= spawnInterval && currentApple == null)
            {
                SpawnApple();
                spawnTimer = 0f;
            }
        }
    }

    void SpawnApple()
    {
        float randomX = Random.Range(spawnRangeX.x, spawnRangeX.y) + transform.position.x;
        float randomY = Random.Range(spawnRangeY.x, spawnRangeY.y) + transform.position.y;
        Vector2 spawnPosition = new Vector2(randomX, randomY);

        currentApple = Instantiate(appleObject);
        currentApple.transform.position = spawnPosition;

        Debug.Log("Pomme spawnée à : " + spawnPosition);
        appleTouched = false;
    }

    void ResetCycle()
    {
        currentApple = null;
        spawnTimer = 0f;
    }

    public void PlayVideo()
    {
        if (videoPlayer == null || rawImage == null)
        {
            Debug.LogError("Le VideoPlayer ou le RawImage est manquant ou détruit !");
            return;
        }

        Debug.Log("Lecture de la vidéo...");
        isPlayingVideo = true;

        rawImage.SetActive(true);
        Debug.Log("RawImage activé !");

        videoPlayer.gameObject.SetActive(true);
        videoPlayer.Play();
        Debug.Log("VideoPlayer activé et lecture démarrée !");

        Invoke(nameof(ReturnToHome), videoDuration);
    }

    private void ReturnToHome()
    {
        Debug.Log("Retour à l'écran Home...");

        videoPlayer.Stop();
        rawImage.SetActive(false);
        videoPlayer.gameObject.SetActive(false);

        UnityEngine.SceneManagement.SceneManager.LoadScene("Home");
        isPlayingVideo = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Pomme hit !");
            Destroy(currentApple);
            appleTouched = true;
            ResetCycle();
        }
    }
}
