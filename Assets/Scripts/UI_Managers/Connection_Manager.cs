using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Spawning;
using MLAPI.Transports.UNET;
using System;

public class Connection_Manager : MonoBehaviour
{
    //buttons
    public GameObject pauseMenuPanel;
    public GameObject multiplayerMenuPanel;
    public GameObject minimapPanel;
    public GameObject playerRef;
    public string ipAddress = "127.0.0.1";
    UNetTransport transport;
    public void Pause_Game()
    {
        pauseMenuPanel.SetActive(true);
        multiplayerMenuPanel.SetActive(false);
        minimapPanel.SetActive(false);
    }

    public void Access_Multiplayer_Menu()
    {
        pauseMenuPanel.SetActive(false);
        multiplayerMenuPanel.SetActive(true);
        minimapPanel.SetActive(false);
    }

    public void Resume_Game()
    {
        pauseMenuPanel.SetActive(false);
        multiplayerMenuPanel.SetActive(false);
        minimapPanel.SetActive(true);

    }
    //server side
    public void Host()
    {
        //approve
        NetworkManager.Singleton.ConnectionApprovalCallback += Approval_Check;
        NetworkManager.Singleton.StartHost(Vector3.zero, Quaternion.identity);//Vector3.zero

        //destroy default camera

        Destroy(GameObject.FindGameObjectWithTag("MainCamera"));
        
        pauseMenuPanel.SetActive(false);
        multiplayerMenuPanel.SetActive(false);
        minimapPanel.SetActive(true);
    }
    //server side
    private void Approval_Check(byte[] connection_data, ulong client_id, NetworkManager.ConnectionApprovedDelegate callback)
    {
        //check connection data
        bool approve = System.Text.Encoding.ASCII.GetString(connection_data) == "words123";
        callback(true, null, approve, Vector3.zero, Quaternion.identity);
    }

    public void Join()
    {
        //destroy default camera
        Destroy(GameObject.FindGameObjectWithTag("MainCamera"));

        //join
        transport = NetworkManager.Singleton.GetComponent<UNetTransport>();
        transport.ConnectAddress = "127.0.0.1";//ipAddress
        NetworkManager.Singleton.NetworkConfig.ConnectionData = System.Text.Encoding.ASCII.GetBytes("words123");
        NetworkManager.Singleton.StartClient();

        //update hud
        pauseMenuPanel.SetActive(false);
        multiplayerMenuPanel.SetActive(false);
        minimapPanel.SetActive(true);
    }

    public void IPAddress_Changed(string newAddress)
    {
        this.ipAddress = newAddress;
    }
}
