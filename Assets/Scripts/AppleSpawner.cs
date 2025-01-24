using UnityEngine;

public class AppleSpawner : MonoBehaviour
{
    public GameObject appleObject; // Référence à l'image de la pomme (désactivée)
    public float spawnInterval = 10f; // Intervalle de spawn (10 secondes)
    public Vector2 spawnRangeX = new Vector2(-5f, 5f); // Limites pour la position X
    public Vector2 spawnRangeY = new Vector2(-3f, 3f); // Limites pour la position Y

    private float spawnTimer = 0f; // Timer pour le spawn
    private float appleTimer = 0f; // Timer pour la durée de la pomme
    private bool isAppleActive = false; // Vérifie si une pomme est active

    void Update()
    {
        // Timer pour le spawn des pommes
        spawnTimer += Time.deltaTime;

        if (!isAppleActive && spawnTimer >= spawnInterval)
        {
            SpawnApple();
            spawnTimer = 0f;
            appleTimer = 0f; // Réinitialise le timer de la pomme
        }

        // Si la pomme est active, vérifie si elle expire
        if (isAppleActive)
        {
            appleTimer += Time.deltaTime;

            if (appleTimer >= spawnInterval)
            {
                Debug.Log("Rater !");
                appleObject.SetActive(false); // Désactive la pomme
                isAppleActive = false; // Indique qu'il n'y a plus de pomme active
            }
        }
    }

    void SpawnApple()
    {
        // Génère une position aléatoire
        float x = Random.Range(spawnRangeX.x, spawnRangeX.y);
        float y = Random.Range(spawnRangeY.x, spawnRangeY.y);
        Vector2 spawnPosition = new Vector2(x, y);

        // Place et active la pomme
        appleObject.transform.position = spawnPosition;
        appleObject.SetActive(true);
        isAppleActive = true;
    }
}
