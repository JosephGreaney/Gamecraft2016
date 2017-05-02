using UnityEngine;
using UnityStandardAssets._2D;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject cameraPrefab;

    private void Start()
    {
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        GameObject cam = (GameObject)Instantiate(cameraPrefab, transform.position, Quaternion.identity);
        Camera2DFollow cam2D = cam.GetComponent<Camera2DFollow>();
        GameObject player = (GameObject)Instantiate(playerPrefab, transform.position, Quaternion.identity);
        cam2D.target = player.transform;
        cam2D.Initialise();
    }
}
