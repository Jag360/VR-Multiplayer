using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        ConnectToServer(); // Call the ConnectToServer method to initiate the connection to the Photon server
    }

    // try to connect with PHOTON server
    void ConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings(); // Connect to the Photon server using the settings configured in the project
        Debug.Log("Connecting to server!..."); // Print a log message indicating that the connection process has started
    }


    //  After the connection to the server was successful
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster(); // Call the base implementation of the method

        Debug.Log("Successfully connected to server!"); // Print a log message

        RoomOptions roomOptions = new RoomOptions(); // Create a new RoomOptions object to customize room settings

        roomOptions.MaxPlayers = 10; // Set the maximum number of players allowed in the room to 10
        roomOptions.IsVisible = true; // Set the room to be visible in the lobby
        roomOptions.IsOpen = true; // Set the room to be open for other clients to join

        PhotonNetwork.JoinOrCreateRoom("Room 1", roomOptions, TypedLobby.Default);
        // Join an existing room named "Room 1" or create a new room with the specified name and options
    }


    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom(); // Call the base implementation of the method

        Debug.Log("Just joined a room!"); // Print a log message indicating that the client has successfully joined a room
    }

    //  When a new player connects to the room
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer); // Call the base implementation of the method

        Debug.Log("A new player joined the room!"); // Print a log message indicating that a new player has joined the room
    }


}
