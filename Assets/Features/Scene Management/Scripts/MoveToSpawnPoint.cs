using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToSpawnPoint : MonoBehaviour
{
    [SerializeField] private Transform TrackerOffsets;
    [SerializeField] private Autohand.AutoHandPlayer Player;
    private void Start()
    {
        AppSceneManager.OnEnvironmentLoaded += AppSceneManager_OnEnvironmentLoaded;
    }

    private void OnDestroy()
    {
        AppSceneManager.OnEnvironmentLoaded -= AppSceneManager_OnEnvironmentLoaded;
    }

    private void AppSceneManager_OnEnvironmentLoaded()
    {
        moveToSpawn();
    }

    private void moveToSpawn()
    {
        Vector3 targetPoint;
        Quaternion targetRot;

        var spawnPoints = GameObject.FindGameObjectsWithTag("Respawn");
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            targetPoint = Vector3.zero;
            targetRot = Quaternion.identity;
        }
        else
        {
            var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            targetPoint = spawnPoint.transform.position; // + Vector3.up * VerticalOffset;
            targetRot = spawnPoint.transform.rotation;
        }

        transform.rotation = targetRot;

        var playerOffset = (transform.position - Camera.main.transform.position).FlattenY();
        transform.position = targetPoint + playerOffset;

        TrackerOffsets.localPosition = Vector3.zero;
        TrackerOffsets.localRotation = Quaternion.identity;
        //Autohand.AutoHandPlayer player;
        Player.SetPosition(transform.position, transform.rotation);
    }
}
