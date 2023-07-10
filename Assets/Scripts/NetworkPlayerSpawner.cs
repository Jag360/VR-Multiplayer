using UnityEngine;
using Photon.Pun;

public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
{
    private GameObject spawnedPlayerPrefab;

    // Called after the local player successfully joins a room
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        // Instantiate the network player prefab for the local player at the specified position and rotation
        spawnedPlayerPrefab = PhotonNetwork.Instantiate("Network Player", transform.position, transform.rotation);
    }

    // Called after the local player leaves a room
    public override void OnLeftRoom()
    {
        base.OnLeftRoom();

        // Destroy the instantiated network player prefab for the local player
        PhotonNetwork.Destroy(spawnedPlayerPrefab);
    }
}
