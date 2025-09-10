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
        GameObject player = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
        Instantiate(robotPrefab, robotSpawnPoint.position, Quaternion.identity);

        vcam.Follow = player.transform;
    }
}
