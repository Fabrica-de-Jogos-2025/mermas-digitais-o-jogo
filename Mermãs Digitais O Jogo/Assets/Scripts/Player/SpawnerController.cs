using Unity.Cinemachine;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject robotPrefab;
    [SerializeField] private Transform playerSpawnPoint;
    [SerializeField] private Transform robotSpawnPoint;
    [SerializeField] private CinemachineCamera vcam;

    public Transform PlayerSpawnPoint { get => playerSpawnPoint; set => playerSpawnPoint = value; }

    private void Awake()
    {
        /*GameObject player = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
        Instantiate(robotPrefab, robotSpawnPoint.position, Quaternion.identity);

        if (vcam != null && player != null)
        vcam.Follow = player.transform;*/

        // Verifica se já existe um Player na cena
        GameObject player = GameObject.FindWithTag("Player");
        GameObject robot = GameObject.FindWithTag("Robot");
        if (player == null && robot == null)
        {
            player = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
            robot = Instantiate(robotPrefab, robotSpawnPoint.position, Quaternion.identity);
        }

        // Configura a câmera para seguir o Player
        if (vcam != null && player != null && robot != null)
        {
            vcam.Follow = player.transform;
        }
    }
}
