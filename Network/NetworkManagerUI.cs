using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField]
    private Button hostBtn;
    [SerializeField]
    private Button serverBtn;
    [SerializeField]
    private Button clientBtn;
    [SerializeField]
    private Button room1;
    [SerializeField]
    private Button room2;
    [SerializeField]
    private UnityTransport unityTransport;
    // Start is called before the first frame update
    void Start()
    {
        var args = System.Environment.GetCommandLineArgs(); //获取命令行参数

        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "-port")
            {
                ushort port = ushort.Parse(args[i + 1]);
                unityTransport.ConnectionData.Address = "121.41.174.139"; // 云服务器公网IP
                unityTransport.ConnectionData.Port = port;
                unityTransport.ConnectionData.ServerListenAddress = "0.0.0.0";
            }

        }

        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "-lauch-as-server")
            {
                NetworkManager.Singleton.StartServer();
                DestroyAllButtons();
            }
        }


        // 为房间按钮添加事件监听器
        room1.onClick.AddListener(() =>
        {
            unityTransport.ConnectionData.Port = 7777;
            NetworkManager.Singleton.StartClient();
            DestroyAllButtons();
        });

        room2.onClick.AddListener(() =>
        {
            unityTransport.ConnectionData.Port = 7778;
            NetworkManager.Singleton.StartClient();
            DestroyAllButtons();
        });

        hostBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
            DestroyAllButtons();

        });
        serverBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartServer();
            DestroyAllButtons();
        });
        clientBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
            DestroyAllButtons();
        });
    }

    private void DestroyAllButtons()
    {
        Destroy(hostBtn.gameObject);
        Destroy(serverBtn.gameObject);
        Destroy(clientBtn.gameObject);
        Destroy(room1.gameObject);
        Destroy(room2.gameObject);
    }

}