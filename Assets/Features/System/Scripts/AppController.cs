using Firesplash.UnityAssets.SocketIO;
using Normal.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class AppController : AppControllerBase
{
    [SerializeField] private TMPro.TMP_Text debugLabel;

    [SerializeField] private Autohand.AutoHandPlayer _autoHandPlayer;

    [SerializeField] private Camera _mainCamera;
    public override Camera MainCamera => _mainCamera;

    [SerializeField] private DestinationPresenter _destinationPresenter;

    [SerializeField] private Realtime _realtimeNetwork;
    public Realtime RealtimeNetwork => _realtimeNetwork;

    public override Transform PlayerTransform => _autoHandPlayer.transform;

    public override Rigidbody PlayerBody => _autoHandPlayer.body;

    public override string BundleVersionCode => GeneratedInfo.BundleVersionCode;

    private UIManager _uiManager = new UIManager();
    public override IUIManager UIManager => _uiManager;

    public override bool IsPancake => false;

    [SerializeField] private Keyboard _keyboard;
    public override IKeyboard Keyboard => _keyboard;

    void Start()
    {
        UserInfo.OnCurrentUserChanged += UserInfo_OnCurrentUserChanged;
        WebSocketListener.OnSetUser += WebSocketListener_OnSetUser;
        DestinationPresenter.UrlChanged += DestinationPresenter_UrlChanged;

        CoreModule.Initialize();
        XrUIModule.Initialize();
        PresenceModule.Initialize();

        //try
        //{
        //    debugLabel.text = DeviceRegistrationController.checkFile();
        //}
        //catch(Exception ex)
        //{
        //    debugLabel.text = ex.Message;
        //}
    }

    private void OnDestroy()
    {
        UserInfo.OnCurrentUserChanged -= UserInfo_OnCurrentUserChanged;
        WebSocketListener.OnSetUser -= WebSocketListener_OnSetUser;
        DestinationPresenter.UrlChanged -= DestinationPresenter_UrlChanged;
    }

    private void DestinationPresenter_UrlChanged(string Url)
    {
        WebSocketListener.Socket.Emit("userLocationChanged", Url);
    }

    private void WebSocketListener_OnSetUser(UserDto obj)
    {
        Debug.Log("App Controller sees that the user has been set (logged in). Currently ignoring it");
        //DeviceRegistrationController.RegisterDeviceWithLegacyServer();
    }

    private void UserInfo_OnCurrentUserChanged(UserInfo obj)
    {
        if(UserInfo.CurrentUser == null) AppSceneManager.LoadLocalScene("Login");
    }

    public override void SetPlayerPosition(Vector3 WorldPosition)
    {
        _autoHandPlayer.SetPosition(WorldPosition);
    }

    public override void SetPlayerPosition(Vector3 WorldPosition, Quaternion WorldRotation)
    {
        _autoHandPlayer.SetPosition(WorldPosition, WorldRotation);
    }

    public override void SetPlayerRotation(Quaternion WorldRotation)
    {
        _autoHandPlayer.SetRotation(WorldRotation);
    }
}
