using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Spawning;
using MLAPI.Transports.UNET;
using System;
using UnityEngine.SceneManagement;


public class Connection_Manager : MonoBehaviour
{
    //buttons
    public GameObject pauseMenuPanel;
    public GameObject multiplayerMenuPanel;
    public GameObject minimapPanel;
    public GameObject playerRef;
    public GameObject errorMes;
    //public GameObject in_field;
    public string ipAddress = "127.0.0.1";
    UNetTransport transport;

    public void Awake()
    {
    }

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
        //spawn player host
        NetworkManager.Singleton.StartHost(new Vector3(0, -1000, 0), Quaternion.identity);//Vector3.zero

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
        
        //Debug.Log("ip adress: " + ipAddress);
        transport.ConnectAddress = ipAddress;//ipAddress //"127.0.0.1"
        NetworkManager.Singleton.NetworkConfig.ConnectionData = System.Text.Encoding.ASCII.GetBytes("words123");
        //bool boi = NetworkManager.Singleton.StartClient().Success;
        //Debug.Log("SUCCESS" + NetworkManager.Singleton.StartClient().Success);
        if(NetworkManager.Singleton.StartClient().Success)
        {
            //NetworkManager.Singleton.StartClient();
            //update hud
            pauseMenuPanel.SetActive(false);
            multiplayerMenuPanel.SetActive(false);
            minimapPanel.SetActive(true);
            if (GameObject.Find("Camera"))
                GameObject.Find("Camera").SetActive(false);
            //Debug.Log("Hello");
            //Debug.Log("There");
        }
        else
        {
            
            GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
            cam.name = "Camera";
            Instantiate(cam);
            GameObject.Find("Camera(Clone)").GetComponent<Camera>().enabled = true;
            GameObject.Find("Camera(Clone)").GetComponent<AudioListener>().enabled = true;
            //GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().enabled = true;//SetActive(true);
            NetworkManager.Singleton.StopClient();
            //GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().enabled = true;//SetActive(true);
            //Debug.Log(GameObject.FindGameObjectWithTag("MainCamera").name);
            //GameObject.Find("invalid_address_message").GetComponent<TextMesh>();
            errorMes.SetActive(true);
        }


    }

    public void IPAddress_Changed(string newAddress)
    {
        this.ipAddress = newAddress;
        Debug.Log("connect address changed to: " + ipAddress);

    }
}
