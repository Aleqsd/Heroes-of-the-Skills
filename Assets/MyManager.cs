using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Networking.NetworkSystem;

public class MyManager : NetworkManager
{

    public Button player1Button;
    public Button player2Button;

    int avatarIndex = 0;

    public Canvas characterSelectionCanvas;

    void Start()
    {
        characterSelectionCanvas.enabled = true;
        player1Button.onClick.AddListener(delegate { AvatarPicker(player1Button.name); });
        player2Button.onClick.AddListener(delegate { AvatarPicker(player2Button.name); });
    }

    void AvatarPicker(string buttonName)
    {
        switch (buttonName)
        {
            case "warrior_icon":
                avatarIndex = 0;
                break;
            case "wizard_icon":
                avatarIndex = 1;
                break;
        }

        playerPrefab = spawnPrefabs[avatarIndex];
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        characterSelectionCanvas.enabled = false;
        IntegerMessage msg = new IntegerMessage(avatarIndex);

        if (!clientLoadedScene)
        {
            ClientScene.Ready(conn);
            if (autoCreatePlayer)
            {
                ClientScene.AddPlayer(conn, 0, msg);
            }
        }

    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {
        int id = 0;

        if (extraMessageReader != null)
        {
            IntegerMessage i = extraMessageReader.ReadMessage<IntegerMessage>();
            id = i.value;
        }

        GameObject playerPrefab = spawnPrefabs[id];

        GameObject player;
        Transform startPos = GetStartPosition();
        if (startPos != null)
        {
            player = (GameObject)Instantiate(playerPrefab, startPos.position, startPos.rotation);
        }
        else
        {
            player = (GameObject)Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        }

        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }
}