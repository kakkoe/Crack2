using PlayerIOClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class RacknetMultiplayer
{
    public static class LobbyConnectionStatus
    {
        public const int DISCONNECTED = 0;

        public const int REGISTERING = 1;

        public const int LOGGING_IN = 2;

        public const int LOOKING_FOR_LOBBIES = 3;

        public const int JOINING_LOBBY = 4;

        public const int CONNECTED = 5;
    }

    public static class DataTransferStatus
    {
        public const int NONE = 0;

        public const int UPLOADING_CHARACTER_DATA = 1;

        public const int VOTING = 2;
    }

    public static class CharacterLoadStatus
    {
        public const int NOT_LOADED = 0;

        public const int FETCHING_LIST = 1;

        public const int FETCHING_CHARACTERS = 2;

        public const int GOT_CHARACTERS = 3;

        public const int BUILT = 4;
    }

    public static string username;

    public static string email;

    public static string passwordHashed;

    public static Client client;

    public static Multiplayer MP;

    public static Connection connectionToLobby;

    public static BigDB bigDB;

    public static DatabaseObject PlayerObject;

    public static string myAccountUID;

    public static int lobbyConnectionStatus = 0;

    public static DatabaseArray upvotes = new DatabaseArray();

    public static DatabaseArray downvotes = new DatabaseArray();

    public static int dataTransferStatus = 0;

    public static Connection connectionToGame;

    public static Vector3 v3;

    public static Game game;

    public static bool expectingDisconnect = false;

    public static bool loggingIn = false;

    public static Transform loginWindow;

    public static bool usingSavedPasswordHash = false;

    public static bool loginWasOpen = false;

    public static Transform connectionStatusWindow;

    public static ScienceTextAnimator connectionStatusText;

    public static Text connectionSpecificStatusText;

    public static Image[] connectionStatusHexes = new Image[6];

    public static Transform errorWindow;

    public static GameObject errorWindowBG;

    public static GameObject errorWindowBGgood;

    public static Text errorWindowTextT;

    public static ScienceTextAnimator errorWindowText;

    public static string errorMessage = string.Empty;

    public static bool goodError = false;

    public static bool needFilterColorBlockInit = true;

    public static ColorBlock filterColorBlock;

    public static ColorBlock filterColorBlockSelected;

    public static List<long> pageStarts = new List<long>();

    public static bool inRackNet = false;

    public static Transform homeWindow;

    public static GameObject homeWindowLoginPrompt;

    public static GameObject homeWindowCharacterMenu;

    public static int myCharacterListStatus = 0;

    public static int publicCharacterListStatus = 0;

    public static int filterMode = 0;

    public static GameObject RacknetBrowserMyCharacterTemplate;

    public static GameObject RacknetBrowserCharacterTemplate;

    public static bool racknetBrowserInitted = false;

    public static string[] myCharacterIDs;

    public static List<GameObject> myCharacterPanels = new List<GameObject>();

    public static DatabaseObject[] myCharacterDOs;

    public static List<GameObject> publicCharacterPanels = new List<GameObject>();

    public static DatabaseObject[] publicCharacterDOs;

    public static GameObject cmdUploadSelf;

    public static Transform myCharactersContainer;

    public static Transform myCharactersContainerStuff;

    public static Transform publicCharactersContainer;

    public static Transform publicCharactersContainerStuff;

    public static float myCharactersContentHeight;

    public static float myCharactersScrollAmount = 0f;

    public static float publicCharactersContentHeight;

    public static float publicCharactersScrollAmount = 0f;

    public static GameObject characterPreviewImage;

    public static GameObject characterPreviewLoadingIndicator;

    public static float timeSinceManualPreviewSpin = 10f;

    public static float autoSpinCharacterPreviewVel = 0f;

    public static bool characterPreviewWasOpen = false;

    public static Transform previewEnjoymentPanel;

    public static GameObject previewCharacterPreferenceTemplate;

    public static GameObject cmdFavorites;

    public static int mostRecentSearchFilter = 2;

    public static List<GameObject> previewCharacterPreferencePreviews = new List<GameObject>();

    public static int lastFilterMode = -1;

    public static int curPublicPage = 0;

    public static string lastSearchQuery = string.Empty;

    public static long valueOfLastEntryOnCurrentPage = 0L;

    public static int modifyScoreAmount = 0;

    public static int whichCharacterVote;

    public static int mod;

    public static RackCharacter previewCharacter;

    public static GameObject previewCamera;

    public static bool needToMovePreviewCharacterToPreviewBox = true;

    public static byte[] avatarBytes;

    public static bool waitingForAvatarUpload = false;

    public static bool waitingForXMLUpload = false;

    public static bool waitingForCustomTexUpload = false;

    public static List<string> customTexturesUploading = new List<string>();

    public static int customTexUploadStep = 0;

    public static string uploadedXMLfilename;

    public static string uploadedAvatarFilename;

    public static DatabaseArray customTextureURLs = new DatabaseArray();

    public static GameObject RNUI;

    public static bool anyRacknetUIopen = false;

    [CompilerGenerated]
    private static Callback<Client> _003C_003Ef__mg_0024cache0;

    [CompilerGenerated]
    private static Callback<PlayerIORegistrationError> _003C_003Ef__mg_0024cache1;

    [CompilerGenerated]
    private static Callback<Client> _003C_003Ef__mg_0024cache2;

    [CompilerGenerated]
    private static Callback<PlayerIOError> _003C_003Ef__mg_0024cache3;

    [CompilerGenerated]
    private static Callback<RoomInfo[]> _003C_003Ef__mg_0024cache4;

    [CompilerGenerated]
    private static Callback<Connection> _003C_003Ef__mg_0024cache5;

    [CompilerGenerated]
    private static Callback<PlayerIOError> _003C_003Ef__mg_0024cache6;

    [CompilerGenerated]
    private static Callback<Connection> _003C_003Ef__mg_0024cache7;

    [CompilerGenerated]
    private static Callback<PlayerIOError> _003C_003Ef__mg_0024cache8;

    [CompilerGenerated]
    private static MessageReceivedEventHandler _003C_003Ef__mg_0024cache9;

    [CompilerGenerated]
    private static DisconnectEventHandler _003C_003Ef__mg_0024cacheA;

    [CompilerGenerated]
    private static Callback<DatabaseObject> _003C_003Ef__mg_0024cacheB;

    [CompilerGenerated]
    private static Callback<PlayerIOError> _003C_003Ef__mg_0024cacheC;

    [CompilerGenerated]
    private static Callback<DatabaseObject[]> _003C_003Ef__mg_0024cacheD;

    [CompilerGenerated]
    private static Callback<DatabaseObject> _003C_003Ef__mg_0024cacheE;

    [CompilerGenerated]
    private static Callback<PlayerIOError> _003C_003Ef__mg_0024cacheF;

    [CompilerGenerated]
    private static Callback<DatabaseObject[]> _003C_003Ef__mg_0024cache10;

    [CompilerGenerated]
    private static Callback<DatabaseObject[]> _003C_003Ef__mg_0024cache11;

    [CompilerGenerated]
    private static Callback<DatabaseObject[]> _003C_003Ef__mg_0024cache12;

    [CompilerGenerated]
    private static Callback<DatabaseObject[]> _003C_003Ef__mg_0024cache13;

    [CompilerGenerated]
    private static Callback _003C_003Ef__mg_0024cache14;

    [CompilerGenerated]
    private static Game.NFDelegate _003C_003Ef__mg_0024cache15;

    [CompilerGenerated]
    private static Callback<DatabaseObject> _003C_003Ef__mg_0024cache16;

    [CompilerGenerated]
    private static Callback<PlayerIOError> _003C_003Ef__mg_0024cache17;

    [CompilerGenerated]
    private static Callback _003C_003Ef__mg_0024cache18;

    [CompilerGenerated]
    private static Callback<PlayerIOError> _003C_003Ef__mg_0024cache19;

    [CompilerGenerated]
    private static Callback<DatabaseObject[]> _003C_003Ef__mg_0024cache1A;

    [CompilerGenerated]
    private static Callback<PlayerIOError> _003C_003Ef__mg_0024cache1B;

    [CompilerGenerated]
    private static Callback _003C_003Ef__mg_0024cache1C;

    public static void init()
    {
        RacknetMultiplayer.game = Game.gameInstance;
    }

    public static void login(string username_, string rawPassword, bool registering = false, bool usingSavedHash = false)
    {
        if (RacknetMultiplayer.game.PC() != null && RacknetMultiplayer.game.PC().initted)
        {
            RacknetMultiplayer.game.playSound("enter_hologram", 1f, 1f);
        }
        RacknetMultiplayer.email = username_;
        RacknetMultiplayer.username = RacknetMultiplayer.email.Replace("@", ".AT.");
        PlayerPrefs.SetString("savedEmail", RacknetMultiplayer.email);
        if (usingSavedHash)
        {
            RacknetMultiplayer.passwordHashed = rawPassword;
        }
        else
        {
            RacknetMultiplayer.passwordHashed = RacknetMultiplayer.MD5Hash(GameID.salt.ToString() + rawPassword + GameID.pepper.ToString());
        }
        if (((Component)RacknetMultiplayer.loginWindow.Find("chkRemember")).GetComponent<Toggle>().isOn)
        {
            PlayerPrefs.SetString("savedPasswordHash", RacknetMultiplayer.passwordHashed);
        }
        else if (PlayerPrefs.HasKey("savedPasswordHash"))
        {
            PlayerPrefs.DeleteKey("savedPasswordHash");
        }
        rawPassword = string.Empty;
        string text = PlayerIO.CalcAuth256(RacknetMultiplayer.username, GameID.SS);
        if (registering)
        {
            RacknetMultiplayer.lobbyConnectionStatus = 1;
            PlayerIO.QuickConnect.SimpleRegister(GameID.ID, RacknetMultiplayer.username, RacknetMultiplayer.passwordHashed, RacknetMultiplayer.email, string.Empty, string.Empty, new Dictionary<string, string>(), string.Empty, new string[0], RacknetMultiplayer.OnConnect, RacknetMultiplayer.OnRegisterError);
        }
        else
        {
            RacknetMultiplayer.lobbyConnectionStatus = 2;
            PlayerIO.QuickConnect.SimpleConnect(GameID.ID, RacknetMultiplayer.username, RacknetMultiplayer.passwordHashed, new string[0], RacknetMultiplayer.OnConnect, RacknetMultiplayer.OnConnectError);
        }
    }

    public static string MD5Hash(string input)
    {
        MD5 mD = MD5.Create();
        byte[] bytes = Encoding.ASCII.GetBytes(input);
        byte[] array = mD.ComputeHash(bytes);
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < array.Length; i++)
        {
            stringBuilder.Append(array[i].ToString("X2"));
        }
        return stringBuilder.ToString();
    }

    public static void OnConnect(Client _client)
    {
        RacknetMultiplayer.loggingIn = false;
        RacknetMultiplayer.client = _client;
        RacknetMultiplayer.MP = RacknetMultiplayer.client.Multiplayer;
        RacknetMultiplayer.bigDB = RacknetMultiplayer.client.BigDB;
        RacknetMultiplayer.connectToLobby();
    }

    public static void OnConnectError(PlayerIOError error)
    {
        if (RacknetMultiplayer.errorMessage == string.Empty)
        {
            RacknetMultiplayer.game.playSound("ui_error", 1f, 1f);
        }
        RacknetMultiplayer.errorMessage = error.Message;
        if (RacknetMultiplayer.errorMessage == "NetworkIssue; ")
        {
            RacknetMultiplayer.errorMessage = "RackNet is temporarily unavailable. Check back in the next build!";
        }
        Debug.Log(error.ErrorCode);
        RacknetMultiplayer.disconnectFromLobby(false);
    }

    public static void OnRegisterError(PlayerIORegistrationError error)
    {
        if (RacknetMultiplayer.errorMessage == string.Empty)
        {
            RacknetMultiplayer.game.playSound("ui_error", 1f, 1f);
        }
        if (error.UsernameError != null)
        {
            RacknetMultiplayer.errorMessage = "Invalid email address: " + error.UsernameError;
        }
        else if (error.EmailError != null)
        {
            RacknetMultiplayer.errorMessage = "Invalid email address: " + error.EmailError;
        }
        else if (error.PasswordError != null)
        {
            RacknetMultiplayer.errorMessage = "Invalid password: " + error.PasswordError;
        }
        else
        {
            RacknetMultiplayer.errorMessage = error.Message;
        }
        RacknetMultiplayer.disconnectFromLobby(false);
    }

    public static void clearCache()
    {
        int num = 0;
        bool flag = false;
        while (num < 50 && !flag)
        {
            num++;
            try
            {
                Directory.Delete(Application.persistentDataPath + "/racknetcache/", true);
                flag = true;
            }
            catch
            {
                Thread.Sleep(200);
            }
        }
        FileInfo fileInfo = new FileInfo(Application.persistentDataPath + "/racknetcache/");
        while (fileInfo.Exists)
        {
            Thread.Sleep(100);
            fileInfo.Refresh();
        }
        RacknetMultiplayer.disconnectFromLobby(true);
        RacknetMultiplayer.lookForLobby();
    }

    public static void disconnectFromLobby(bool butStayLoggedInBecauseWeAreImmediatelyJoiningLobby = false)
    {
        RacknetMultiplayer.expectingDisconnect = (RacknetMultiplayer.expectingDisconnect || butStayLoggedInBecauseWeAreImmediatelyJoiningLobby);
        if (RacknetMultiplayer.connectionToLobby != null)
        {
            RacknetMultiplayer.connectionToLobby.Disconnect();
            RacknetMultiplayer.connectionToLobby = null;
        }
        if (RacknetMultiplayer.client != null && !butStayLoggedInBecauseWeAreImmediatelyJoiningLobby)
        {
            try
            {
                RacknetMultiplayer.client.Logout();
            }
            catch
            {
            }
            RacknetMultiplayer.client = null;
        }
        RacknetMultiplayer.lobbyConnectionStatus = 0;
        RacknetMultiplayer.dataTransferStatus = 0;
        RacknetMultiplayer.game.needPauseRebuild = true;
    }

    public static void connectToLobby()
    {
        RacknetMultiplayer.disconnectFromLobby(true);
        if (RacknetMultiplayer.lobbyConnectionStatus == 0)
        {
            RacknetMultiplayer.lookForLobby();
        }
    }

    public static void lookForLobby()
    {
        RacknetMultiplayer.lobbyConnectionStatus = 3;
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        dictionary.Add("initted", "true");
        dictionary.Add("full", "false");
        RacknetMultiplayer.MP.ListRooms("RacknetLobby", dictionary, 0, 0, RacknetMultiplayer.GotListOfLobbies);
    }

    public static void GotListOfLobbies(RoomInfo[] roomInfos)
    {
        if (RacknetMultiplayer.lobbyConnectionStatus == 3)
        {
            RacknetMultiplayer.lobbyConnectionStatus = 4;
            if (roomInfos.Length == 0)
            {
                RacknetMultiplayer.MP.CreateJoinRoom("Lobby " + Guid.NewGuid(), "RacknetLobby", true, null, null, RacknetMultiplayer.ConnectedToLobby, RacknetMultiplayer.ErrorConnectingToLobby);
            }
            else
            {
                RacknetMultiplayer.MP.CreateJoinRoom(roomInfos[0].Id, "RacknetLobby", true, null, null, RacknetMultiplayer.ConnectedToLobby, RacknetMultiplayer.ErrorConnectingToLobby);
            }
        }
    }

    public static void ConnectedToLobby(Connection connection)
    {
        if (RacknetMultiplayer.lobbyConnectionStatus == 4)
        {
            RacknetMultiplayer.lobbyConnectionStatus = 5;
            RacknetMultiplayer.connectionToLobby = connection;
            RacknetMultiplayer.connectionToLobby.AddOnMessage(RacknetMP_Lobby.OnMessage);
            RacknetMultiplayer.connectionToLobby.AddOnDisconnect(RacknetMultiplayer.DisconnectedFromLobby);
            RacknetMultiplayer.bigDB.LoadOrCreate("Accounts", RacknetMultiplayer.username, RacknetMultiplayer.gotPlayerObject, RacknetMultiplayer.errorFetchingPlayerObject);
        }
    }

    public static void DisconnectedFromLobby(object sender, string reason)
    {
        if (RacknetMultiplayer.expectingDisconnect)
        {
            RacknetMultiplayer.expectingDisconnect = false;
        }
        else
        {
            if (reason != "Disconnect")
            {
                if (RacknetMultiplayer.errorMessage == string.Empty)
                {
                    RacknetMultiplayer.game.playSound("ui_error", 1f, 1f);
                }
                RacknetMultiplayer.errorMessage = "Disconnected from RackNet with reason: '" + reason + "'";
            }
            RacknetMultiplayer.game.playSound("exit_hologram", 1f, 1f);
            RacknetMultiplayer.disconnectFromLobby(false);
        }
    }

    public static void ErrorConnectingToLobby(PlayerIOError error)
    {
        if (RacknetMultiplayer.errorMessage == string.Empty)
        {
            RacknetMultiplayer.game.playSound("ui_error", 1f, 1f);
        }
        RacknetMultiplayer.errorMessage = error.Message;
        RacknetMultiplayer.disconnectFromLobby(false);
    }

    public static void process()
    {
        RacknetMultiplayer.processUI();
    }

    public static void initUI()
    {
        RacknetMultiplayer.RNUI = RacknetMultiplayer.game.UI.transform.Find("Racknet").gameObject;
    }

    public static void openLoginWindow()
    {
        RacknetMultiplayer.loggingIn = true;
    }

    public static void loginClicked(bool registering = false)
    {
        if (RacknetMultiplayer.usingSavedPasswordHash && ((Component)RacknetMultiplayer.loginWindow.Find("txtPassword")).GetComponent<InputField>().text != PlayerPrefs.GetString("savedPasswordHash", string.Empty))
        {
            RacknetMultiplayer.usingSavedPasswordHash = false;
        }
        RacknetMultiplayer.loginWindow.Find("atInvalidEmail").gameObject.SetActive(false);
        RacknetMultiplayer.loginWindow.Find("atInvalidPassword").gameObject.SetActive(false);
        if (RacknetMultiplayer.verifyEmail(((Component)RacknetMultiplayer.loginWindow.Find("txtEmail")).GetComponent<InputField>().text))
        {
            if (RacknetMultiplayer.verifyPassword(((Component)RacknetMultiplayer.loginWindow.Find("txtPassword")).GetComponent<InputField>().text) || RacknetMultiplayer.usingSavedPasswordHash)
            {
                if (RacknetMultiplayer.usingSavedPasswordHash)
                {
                    RacknetMultiplayer.login(((Component)RacknetMultiplayer.loginWindow.Find("txtEmail")).GetComponent<InputField>().text, PlayerPrefs.GetString("savedPasswordHash", string.Empty), registering, true);
                }
                else
                {
                    RacknetMultiplayer.login(((Component)RacknetMultiplayer.loginWindow.Find("txtEmail")).GetComponent<InputField>().text, ((Component)RacknetMultiplayer.loginWindow.Find("txtPassword")).GetComponent<InputField>().text, registering, false);
                }
            }
            else
            {
                RacknetMultiplayer.loginWindow.Find("atInvalidPassword").gameObject.SetActive(true);
                RacknetMultiplayer.game.playSound("ui_error", 1f, 1f);
            }
        }
        else
        {
            RacknetMultiplayer.loginWindow.Find("atInvalidEmail").gameObject.SetActive(true);
            RacknetMultiplayer.game.playSound("ui_error", 1f, 1f);
        }
    }

    public static bool verifyEmail(string e)
    {
        return e.IndexOf("@") != -1 && e.IndexOf(".") != -1;
    }

    public static bool verifyPassword(string p)
    {
        return p.Length >= 6;
    }

    public static void processLogin()
    {
        if ((UnityEngine.Object)RacknetMultiplayer.loginWindow == (UnityEngine.Object)null)
        {
            RacknetMultiplayer.loginWindow = RacknetMultiplayer.RNUI.transform.Find("LoginWindow");
            ((Component)RacknetMultiplayer.loginWindow.Find("chkRemember")).GetComponent<Toggle>().isOn = PlayerPrefs.HasKey("savedPasswordHash");
        }
        if (RacknetMultiplayer.loggingIn && RacknetMultiplayer.lobbyConnectionStatus == 0 && RacknetMultiplayer.errorMessage == string.Empty)
        {
            if (!RacknetMultiplayer.loginWasOpen)
            {
                RacknetMultiplayer.usingSavedPasswordHash = false;
                if (PlayerPrefs.HasKey("savedEmail"))
                {
                    ((Component)RacknetMultiplayer.loginWindow.Find("txtEmail")).GetComponent<InputField>().text = PlayerPrefs.GetString("savedEmail", string.Empty);
                }
                if (PlayerPrefs.HasKey("savedPasswordHash"))
                {
                    ((Component)RacknetMultiplayer.loginWindow.Find("txtPassword")).GetComponent<InputField>().text = PlayerPrefs.GetString("savedPasswordHash", string.Empty);
                    RacknetMultiplayer.usingSavedPasswordHash = true;
                }
            }
            RacknetMultiplayer.loginWindow.gameObject.SetActive(true);
            RacknetMultiplayer.game.showBackButton = true;
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                RacknetMultiplayer.loginClicked(false);
            }
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (((Component)RacknetMultiplayer.loginWindow.Find("txtEmail")).GetComponent<InputField>().isFocused)
                {
                    ((Component)RacknetMultiplayer.loginWindow.Find("txtPassword")).GetComponent<InputField>().Select();
                    ((Component)RacknetMultiplayer.loginWindow.Find("txtPassword")).GetComponent<InputField>().ActivateInputField();
                }
                else
                {
                    ((Component)RacknetMultiplayer.loginWindow.Find("txtEmail")).GetComponent<InputField>().Select();
                    ((Component)RacknetMultiplayer.loginWindow.Find("txtEmail")).GetComponent<InputField>().ActivateInputField();
                }
            }
            RacknetMultiplayer.anyRacknetUIopen = true;
            RacknetMultiplayer.loginWasOpen = true;
        }
        else
        {
            RacknetMultiplayer.loginWindow.gameObject.SetActive(false);
            if (RacknetMultiplayer.loginWasOpen)
            {
                ((Component)RacknetMultiplayer.loginWindow.Find("txtPassword")).GetComponent<InputField>().text = string.Empty;
            }
            RacknetMultiplayer.loginWasOpen = false;
        }
    }

    public static void processConnectionStatus()
    {
        if ((UnityEngine.Object)RacknetMultiplayer.connectionStatusWindow == (UnityEngine.Object)null)
        {
            RacknetMultiplayer.connectionStatusWindow = RacknetMultiplayer.RNUI.transform.Find("ConnectionStatusWindow");
            RacknetMultiplayer.connectionStatusText = ((Component)RacknetMultiplayer.connectionStatusWindow.Find("txtStatus")).GetComponent<ScienceTextAnimator>();
            RacknetMultiplayer.connectionSpecificStatusText = ((Component)RacknetMultiplayer.connectionStatusWindow.Find("txtSpecificStatus")).GetComponent<Text>();
            for (int i = 0; i < 6; i++)
            {
                RacknetMultiplayer.connectionStatusHexes[i] = ((Component)RacknetMultiplayer.connectionStatusWindow.Find("ThinkingIndicator").Find("hex" + i)).GetComponent<Image>();
            }
        }
        if (RacknetMultiplayer.lobbyConnectionStatus == 5 && RacknetMultiplayer.dataTransferStatus != 0 && RacknetMultiplayer.errorMessage == string.Empty)
        {
            int num = RacknetMultiplayer.dataTransferStatus;
            if (num == 1)
            {
                RacknetMultiplayer.connectionStatusText.setText(Localization.getPhrase("UPLOADING_CHARACTER_DATA", string.Empty), 0f, 10f, true);
            }
            RacknetMultiplayer.connectionStatusWindow.gameObject.SetActive(true);
            for (int j = 0; j < 6; j++)
            {
                RacknetMultiplayer.connectionStatusHexes[j].color = Color.Lerp(RacknetMultiplayer.game.loadingOrange, RacknetMultiplayer.game.loadingBlue, 0.5f + Mathf.Cos((float)j * 0.2f + Time.time * 3f) * 0.5f);
            }
            RacknetMultiplayer.anyRacknetUIopen = true;
        }
        else if (RacknetMultiplayer.lobbyConnectionStatus != 0 && RacknetMultiplayer.lobbyConnectionStatus != 5 && RacknetMultiplayer.errorMessage == string.Empty)
        {
            switch (RacknetMultiplayer.lobbyConnectionStatus)
            {
                case 1:
                    RacknetMultiplayer.connectionStatusText.setText(Localization.getPhrase("REGISTERING", string.Empty), 0f, 10f, true);
                    break;
                case 2:
                    RacknetMultiplayer.connectionStatusText.setText(Localization.getPhrase("LOGGING_IN", string.Empty), 0f, 10f, true);
                    break;
                case 3:
                    RacknetMultiplayer.connectionStatusText.setText(Localization.getPhrase("LOOKING_FOR_LOBBIES", string.Empty), 0f, 10f, true);
                    break;
                case 4:
                    RacknetMultiplayer.connectionStatusText.setText(Localization.getPhrase("JOINING_LOBBY", string.Empty), 0f, 10f, true);
                    break;
            }
            RacknetMultiplayer.connectionStatusWindow.gameObject.SetActive(true);
            for (int k = 0; k < 6; k++)
            {
                RacknetMultiplayer.connectionStatusHexes[k].color = Color.Lerp(RacknetMultiplayer.game.loadingOrange, RacknetMultiplayer.game.loadingBlue, 0.5f + Mathf.Cos((float)k * 0.2f + Time.time * 3f) * 0.5f);
            }
            RacknetMultiplayer.anyRacknetUIopen = true;
        }
        else
        {
            RacknetMultiplayer.connectionStatusWindow.gameObject.SetActive(false);
            RacknetMultiplayer.connectionStatusText.setText(string.Empty, 0f, 10f, true);
        }
    }

    public static void processErrorWindow()
    {
        if ((UnityEngine.Object)RacknetMultiplayer.errorWindow == (UnityEngine.Object)null)
        {
            RacknetMultiplayer.errorWindow = RacknetMultiplayer.RNUI.transform.Find("ErrorWindow");
            RacknetMultiplayer.errorWindowText = ((Component)RacknetMultiplayer.errorWindow.Find("txtError")).GetComponent<ScienceTextAnimator>();
            RacknetMultiplayer.errorWindowTextT = ((Component)RacknetMultiplayer.errorWindow.Find("txtError")).GetComponent<Text>();
            RacknetMultiplayer.errorWindowBG = RacknetMultiplayer.errorWindow.Find("bg").gameObject;
            RacknetMultiplayer.errorWindowBGgood = RacknetMultiplayer.errorWindow.Find("goodBG").gameObject;
        }
        if (RacknetMultiplayer.errorMessage != string.Empty)
        {
            RacknetMultiplayer.errorWindow.gameObject.SetActive(true);
            RacknetMultiplayer.errorWindowBG.SetActive(!RacknetMultiplayer.goodError);
            RacknetMultiplayer.errorWindowBGgood.SetActive(RacknetMultiplayer.goodError);
            if (RacknetMultiplayer.goodError)
            {
                RacknetMultiplayer.errorWindowTextT.color = Color.black;
            }
            else
            {
                RacknetMultiplayer.errorWindowTextT.color = Color.white;
            }
            RacknetMultiplayer.errorWindowText.setText(RacknetMultiplayer.errorMessage, 0f, 10f, true);
            RacknetMultiplayer.game.showBackButton = true;
            RacknetMultiplayer.anyRacknetUIopen = true;
        }
        else
        {
            RacknetMultiplayer.goodError = false;
            RacknetMultiplayer.errorWindow.gameObject.SetActive(false);
        }
    }

    public static void updateFilter()
    {
        if (RacknetMultiplayer.filterMode >= 2)
        {
            RacknetMultiplayer.mostRecentSearchFilter = RacknetMultiplayer.filterMode;
        }
        RacknetMultiplayer.publicCharacterListStatus = 0;
        if (RacknetMultiplayer.needFilterColorBlockInit)
        {
            RacknetMultiplayer.filterColorBlock = ((Component)RacknetMultiplayer.homeWindowCharacterMenu.transform.Find("PublicCharacters").Find("cmdNewest")).GetComponent<Button>().colors;
            RacknetMultiplayer.filterColorBlock.normalColor = Color.white * 0.6f;
            RacknetMultiplayer.filterColorBlockSelected = ((Component)RacknetMultiplayer.homeWindowCharacterMenu.transform.Find("PublicCharacters").Find("cmdNewest")).GetComponent<Button>().colors;
            RacknetMultiplayer.needFilterColorBlockInit = false;
        }
        switch (RacknetMultiplayer.filterMode)
        {
            case 0:
                ((Component)RacknetMultiplayer.homeWindowCharacterMenu.transform.Find("PublicCharacters").Find("cmdNewest")).GetComponent<Button>().colors = RacknetMultiplayer.filterColorBlockSelected;
                ((Component)RacknetMultiplayer.homeWindowCharacterMenu.transform.Find("PublicCharacters").Find("cmdBest")).GetComponent<Button>().colors = RacknetMultiplayer.filterColorBlock;
                ((Component)RacknetMultiplayer.homeWindowCharacterMenu.transform.Find("PublicCharacters").Find("cmdName")).GetComponent<Button>().colors = RacknetMultiplayer.filterColorBlock;
                ((Component)RacknetMultiplayer.homeWindowCharacterMenu.transform.Find("PublicCharacters").Find("cmdSpecies")).GetComponent<Button>().colors = RacknetMultiplayer.filterColorBlock;
                break;
            case 1:
                ((Component)RacknetMultiplayer.homeWindowCharacterMenu.transform.Find("PublicCharacters").Find("cmdNewest")).GetComponent<Button>().colors = RacknetMultiplayer.filterColorBlock;
                ((Component)RacknetMultiplayer.homeWindowCharacterMenu.transform.Find("PublicCharacters").Find("cmdBest")).GetComponent<Button>().colors = RacknetMultiplayer.filterColorBlockSelected;
                ((Component)RacknetMultiplayer.homeWindowCharacterMenu.transform.Find("PublicCharacters").Find("cmdName")).GetComponent<Button>().colors = RacknetMultiplayer.filterColorBlock;
                ((Component)RacknetMultiplayer.homeWindowCharacterMenu.transform.Find("PublicCharacters").Find("cmdSpecies")).GetComponent<Button>().colors = RacknetMultiplayer.filterColorBlock;
                break;
            case 2:
                ((Component)RacknetMultiplayer.homeWindowCharacterMenu.transform.Find("PublicCharacters").Find("cmdNewest")).GetComponent<Button>().colors = RacknetMultiplayer.filterColorBlock;
                ((Component)RacknetMultiplayer.homeWindowCharacterMenu.transform.Find("PublicCharacters").Find("cmdBest")).GetComponent<Button>().colors = RacknetMultiplayer.filterColorBlock;
                ((Component)RacknetMultiplayer.homeWindowCharacterMenu.transform.Find("PublicCharacters").Find("cmdName")).GetComponent<Button>().colors = RacknetMultiplayer.filterColorBlockSelected;
                ((Component)RacknetMultiplayer.homeWindowCharacterMenu.transform.Find("PublicCharacters").Find("cmdSpecies")).GetComponent<Button>().colors = RacknetMultiplayer.filterColorBlock;
                break;
            case 3:
                ((Component)RacknetMultiplayer.homeWindowCharacterMenu.transform.Find("PublicCharacters").Find("cmdNewest")).GetComponent<Button>().colors = RacknetMultiplayer.filterColorBlock;
                ((Component)RacknetMultiplayer.homeWindowCharacterMenu.transform.Find("PublicCharacters").Find("cmdBest")).GetComponent<Button>().colors = RacknetMultiplayer.filterColorBlock;
                ((Component)RacknetMultiplayer.homeWindowCharacterMenu.transform.Find("PublicCharacters").Find("cmdName")).GetComponent<Button>().colors = RacknetMultiplayer.filterColorBlock;
                ((Component)RacknetMultiplayer.homeWindowCharacterMenu.transform.Find("PublicCharacters").Find("cmdSpecies")).GetComponent<Button>().colors = RacknetMultiplayer.filterColorBlockSelected;
                break;
        }
    }

    public static void processHomeWindow()
    {
        if ((UnityEngine.Object)RacknetMultiplayer.homeWindow == (UnityEngine.Object)null)
        {
            RacknetMultiplayer.homeWindow = RacknetMultiplayer.RNUI.transform.Find("HomeScreen");
            RacknetMultiplayer.homeWindowLoginPrompt = RacknetMultiplayer.RNUI.transform.Find("HomeScreen").Find("LoginPrompt").gameObject;
            RacknetMultiplayer.homeWindowCharacterMenu = RacknetMultiplayer.RNUI.transform.Find("HomeScreen").Find("CharacterMenu").gameObject;
        }
        if (RacknetMultiplayer.inRackNet && RacknetMultiplayer.errorMessage == string.Empty && (RacknetMultiplayer.lobbyConnectionStatus == 5 || RacknetMultiplayer.lobbyConnectionStatus == 0) && !RacknetMultiplayer.loggingIn && RacknetMultiplayer.dataTransferStatus == 0)
        {
            RacknetMultiplayer.homeWindow.gameObject.SetActive(true);
            RacknetMultiplayer.homeWindowCharacterMenu.SetActive(RacknetMultiplayer.lobbyConnectionStatus == 5);
            RacknetMultiplayer.homeWindowLoginPrompt.SetActive(RacknetMultiplayer.lobbyConnectionStatus != 5);
            if (RacknetMultiplayer.lobbyConnectionStatus == 5)
            {
                RacknetMultiplayer.processRacknetHomepage();
            }
            else
            {
                RacknetMultiplayer.myCharacterListStatus = 0;
                RacknetMultiplayer.publicCharacterListStatus = 0;
            }
            RacknetMultiplayer.game.showBackButton = true;
            RacknetMultiplayer.anyRacknetUIopen = true;
        }
        else
        {
            RacknetMultiplayer.homeWindow.gameObject.SetActive(false);
            RacknetMultiplayer.myCharacterListStatus = 0;
            RacknetMultiplayer.publicCharacterListStatus = 0;
            if (RacknetMultiplayer.characterPreviewWasOpen)
            {
                RacknetMultiplayer.previewCamera.SetActive(false);
                RacknetMultiplayer.game.removeCharacter(RacknetMultiplayer.previewCharacter);
                RacknetMultiplayer.previewCharacter = null;
                RacknetMultiplayer.characterPreviewWasOpen = false;
            }
        }
    }

    public static IEnumerator loadTexIntoRawImage(string url, RawImage rawImage)
    {
        WWW texIMG = new WWW(url);
        yield return (object)texIMG;
        bool flag = texIMG.error == null || texIMG.error == string.Empty;
        if (flag)
        {
            Texture2D texture = new Texture2D(128, 128);
            texIMG.LoadImageIntoTexture(texture);
            bool flag2 = rawImage != null;
            if (flag2)
            {
                rawImage.texture = texture;
            }
            texture = null;
        }
        yield break;
    }

    public static void updateCache()
    {
        if (RacknetMP_Lobby.serverTime != 0)
        {
            RacknetMultiplayer.bigDB.LoadRange("Characters", "timestamp", null, RacknetMP_Lobby.serverTime, UserSettings.RNcacheData.racknetCacheTimestamp, 1000, RacknetMultiplayer.gotCacheCharacters);
        }
    }

    public static void gotCacheCharacters(DatabaseObject[] DOs)
    {
        if (DOs.Length > 0)
        {
            for (int i = 0; i < DOs.Length; i++)
            {
                if (!UserSettings.RNcacheData.racknetCharacterCache.Contains(DOs[i].Key))
                {
                    UserSettings.RNcacheData.racknetCharacterCache.Add(DOs[i].Key);
                }
                else
                {
                    RacknetMultiplayer.wipeCache(DOs[i].Key);
                }
            }
            long num = DOs[0].GetLong("timestamp") + 1;
            if (num > UserSettings.RNcacheData.racknetCacheTimestamp)
            {
                UserSettings.RNcacheData.racknetCacheTimestamp = num;
            }
            UserSettings.saveSettings();
        }
    }

    public static void processRacknetHomepage()
    {
        if (!RacknetMultiplayer.racknetBrowserInitted)
        {
            RacknetMultiplayer.RacknetBrowserMyCharacterTemplate = RacknetMultiplayer.homeWindowCharacterMenu.transform.Find("MyCharacters").Find("container").Find("stuff")
                .Find("CharacterTemplate")
                .gameObject;
            RacknetMultiplayer.RacknetBrowserMyCharacterTemplate.SetActive(false);
            RacknetMultiplayer.RacknetBrowserCharacterTemplate = RacknetMultiplayer.homeWindowCharacterMenu.transform.Find("PublicCharacters").Find("container").Find("stuff")
                .Find("CharacterTemplate")
                .gameObject;
            RacknetMultiplayer.RacknetBrowserCharacterTemplate.SetActive(false);
            RacknetMultiplayer.cmdUploadSelf = RacknetMultiplayer.homeWindowCharacterMenu.transform.Find("MyCharacters").Find("container").Find("stuff")
                .Find("cmdSubmit")
                .gameObject;
            RacknetMultiplayer.myCharactersContainer = RacknetMultiplayer.homeWindowCharacterMenu.transform.Find("MyCharacters").Find("container");
            RacknetMultiplayer.myCharactersContainerStuff = RacknetMultiplayer.homeWindowCharacterMenu.transform.Find("MyCharacters").Find("container").Find("stuff");
            RacknetMultiplayer.publicCharactersContainer = RacknetMultiplayer.homeWindowCharacterMenu.transform.Find("PublicCharacters").Find("container");
            RacknetMultiplayer.characterPreviewImage = RacknetMultiplayer.homeWindowCharacterMenu.transform.Find("CharacterPreview").gameObject;
            RacknetMultiplayer.characterPreviewImage.SetActive(false);
            RacknetMultiplayer.previewEnjoymentPanel = RacknetMultiplayer.homeWindowCharacterMenu.transform.Find("EnjoymentPanel");
            RacknetMultiplayer.previewEnjoymentPanel.gameObject.SetActive(false);
            RacknetMultiplayer.cmdFavorites = RacknetMultiplayer.homeWindowCharacterMenu.transform.Find("cmdFavorites").gameObject;
            RacknetMultiplayer.cmdFavorites.gameObject.SetActive(false);
            RacknetMultiplayer.characterPreviewLoadingIndicator = RacknetMultiplayer.homeWindowCharacterMenu.transform.Find("PreviewLoadingIndicator").gameObject;
            RacknetMultiplayer.characterPreviewLoadingIndicator.SetActive(false);
            RacknetMultiplayer.publicCharactersContainerStuff = RacknetMultiplayer.homeWindowCharacterMenu.transform.Find("PublicCharacters").Find("container").Find("stuff");
            RacknetMultiplayer.racknetBrowserInitted = true;
        }
        if ((UnityEngine.Object)RacknetMultiplayer.cmdUploadSelf == (UnityEngine.Object)null)
        {
            RacknetMultiplayer.cmdUploadSelf = RacknetMultiplayer.homeWindowCharacterMenu.transform.Find("MyCharacters").Find("container").Find("stuff")
                .Find("cmdSubmit")
                .gameObject;
        }
        if (RacknetMultiplayer.myCharacterListStatus == 0)
        {
            for (int i = 0; i < RacknetMultiplayer.myCharacterPanels.Count; i++)
            {
                UnityEngine.Object.Destroy(RacknetMultiplayer.myCharacterPanels[i]);
            }
            RacknetMultiplayer.myCharacterPanels = new List<GameObject>();
            RacknetMultiplayer.myCharacterListStatus = 1;
            RacknetMultiplayer.bigDB.LoadOrCreate("Accounts", RacknetMultiplayer.username, RacknetMultiplayer.gotPlayerObject, RacknetMultiplayer.errorFetchingPlayerObject);
            RacknetMultiplayer.cmdUploadSelf.SetActive(false);
            RacknetMultiplayer.previewEnjoymentPanel.gameObject.SetActive(false);
        }
        if (RacknetMultiplayer.myCharacterListStatus == 3)
        {
            for (int j = 0; j < RacknetMultiplayer.myCharacterDOs.Length; j++)
            {
                GameObject gameObject = UnityEngine.Object.Instantiate(RacknetMultiplayer.RacknetBrowserMyCharacterTemplate);
                gameObject.SetActive(true);
                gameObject.transform.SetParent(RacknetMultiplayer.myCharactersContainerStuff);
                gameObject.transform.localScale = RacknetMultiplayer.RacknetBrowserMyCharacterTemplate.transform.localScale;
                gameObject.transform.localRotation = Quaternion.identity;
                RacknetMultiplayer.v3 = Vector3.zero;
                RacknetMultiplayer.v3.y += 128f;
                RacknetMultiplayer.v3.y -= (float)j * 90f;
                RacknetMultiplayer.game.StartCoroutine(RacknetMultiplayer.loadTexIntoRawImage(RacknetMultiplayer.getRacknetAvatar(RacknetMultiplayer.myCharacterDOs[j].Key), ((Component)gameObject.transform.Find("headshot").Find("imgHeadshot")).GetComponent<RawImage>()));
                ((Component)gameObject.transform.Find("txtName")).GetComponent<Text>().text = RacknetMultiplayer.myCharacterDOs[j].GetString("name", string.Empty);
                ((Component)gameObject.transform.Find("txtAuthor")).GetComponent<Text>().text = Localization.getSubPhrase("BY_AUTHOR", RacknetMultiplayer.myCharacterDOs[j].GetString("author", string.Empty), string.Empty, string.Empty);
                gameObject.transform.Find("GenderTokens").Find("penisOn").gameObject.SetActive(RacknetMultiplayer.myCharacterDOs[j].GetBool("hasPenis", false));
                gameObject.transform.Find("GenderTokens").Find("penisOff").gameObject.SetActive(!RacknetMultiplayer.myCharacterDOs[j].GetBool("hasPenis", false));
                gameObject.transform.Find("GenderTokens").Find("vaginaOn").gameObject.SetActive(RacknetMultiplayer.myCharacterDOs[j].GetBool("hasVagina", false));
                gameObject.transform.Find("GenderTokens").Find("vaginaOff").gameObject.SetActive(!RacknetMultiplayer.myCharacterDOs[j].GetBool("hasVagina", false));
                gameObject.transform.Find("GenderTokens").Find("boobsOn").gameObject.SetActive(RacknetMultiplayer.myCharacterDOs[j].GetBool("hasBreasts", false));
                gameObject.transform.Find("GenderTokens").Find("boobsOff").gameObject.SetActive(!RacknetMultiplayer.myCharacterDOs[j].GetBool("hasBreasts", false));
                ((Component)gameObject.transform.Find("ScorePanel").Find("txtScore")).GetComponent<Text>().text = RacknetMultiplayer.myCharacterDOs[j].GetInt("score", 10).ToString();
                gameObject.transform.Find("ScorePanel").Find("cmdUp").gameObject.SetActive(false);
                gameObject.transform.Find("ScorePanel").Find("cmdDown").gameObject.SetActive(false);
                gameObject.transform.Find("cmdDelete").Find("bg").name = "offlineButton" + j;
                gameObject.transform.localPosition = RacknetMultiplayer.v3;
                RacknetMultiplayer.myCharacterPanels.Add(gameObject);
            }
            RacknetMultiplayer.v3 = Vector3.zero;
            RacknetMultiplayer.v3.y += 128f;
            RacknetMultiplayer.v3.y -= 90f * (float)RacknetMultiplayer.myCharacterDOs.Length;
            RacknetMultiplayer.cmdUploadSelf.transform.localPosition = RacknetMultiplayer.v3;
            RacknetMultiplayer.cmdUploadSelf.SetActive(true);
            RacknetMultiplayer.myCharactersContentHeight = 160f + 90f * (float)RacknetMultiplayer.myCharacterDOs.Length;
            RacknetMultiplayer.myCharactersScrollAmount = 0f;
            RacknetMultiplayer.myCharacterListStatus = 4;
        }
        if (RacknetMultiplayer.myCharacterListStatus == 4)
        {
            float num = RacknetMultiplayer.myCharactersScrollAmount;
            Vector2 mouseScrollDelta = Input.mouseScrollDelta;
            RacknetMultiplayer.myCharactersScrollAmount = num - mouseScrollDelta.y * 25.5f;
            if (RacknetMultiplayer.myCharactersScrollAmount > RacknetMultiplayer.myCharactersContentHeight - 251f)
            {
                RacknetMultiplayer.myCharactersScrollAmount = RacknetMultiplayer.myCharactersContentHeight - 251f;
            }
            if (RacknetMultiplayer.myCharactersScrollAmount < 0f)
            {
                RacknetMultiplayer.myCharactersScrollAmount = 0f;
            }
            RacknetMultiplayer.v3 = Vector3.zero;
            RacknetMultiplayer.v3.y += RacknetMultiplayer.myCharactersScrollAmount;
            Transform transform = RacknetMultiplayer.myCharactersContainerStuff;
            transform.localPosition += (RacknetMultiplayer.v3 - RacknetMultiplayer.myCharactersContainerStuff.localPosition) * Game.cap(Time.deltaTime * 11f, 0f, 1f);
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
        {
            RacknetMultiplayer.filterMode = RacknetMultiplayer.mostRecentSearchFilter;
            RacknetMultiplayer.updateFilter();
        }
        if (RacknetMultiplayer.publicCharacterListStatus == 0)
        {
            RacknetMultiplayer.updateFilter();
            RacknetMultiplayer.homeWindowCharacterMenu.transform.Find("PublicCharacters").Find("txtNoResults").gameObject.SetActive(false);
            if (RacknetMultiplayer.characterPreviewWasOpen)
            {
                RacknetMultiplayer.previewCamera.SetActive(false);
                RacknetMultiplayer.game.removeCharacter(RacknetMultiplayer.previewCharacter);
                RacknetMultiplayer.previewCharacter = null;
                RacknetMultiplayer.characterPreviewWasOpen = false;
            }
            for (int k = 0; k < RacknetMultiplayer.publicCharacterPanels.Count; k++)
            {
                UnityEngine.Object.Destroy(RacknetMultiplayer.publicCharacterPanels[k]);
            }
            RacknetMultiplayer.publicCharacterPanels = new List<GameObject>();
            switch (RacknetMultiplayer.filterMode)
            {
                case 0:
                    if (RacknetMultiplayer.lastFilterMode != 0)
                    {
                        RacknetMultiplayer.lastFilterMode = 0;
                        RacknetMultiplayer.curPublicPage = 0;
                        RacknetMultiplayer.valueOfLastEntryOnCurrentPage = 0L;
                        RacknetMultiplayer.pageStarts = new List<long>();
                        RacknetMultiplayer.pageStarts.Add(9223372036854775807L);
                    }
                    if (RacknetMultiplayer.curPublicPage >= RacknetMultiplayer.pageStarts.Count)
                    {
                        RacknetMultiplayer.pageStarts.Add(RacknetMultiplayer.valueOfLastEntryOnCurrentPage);
                    }
                    RacknetMultiplayer.bigDB.LoadRange("Characters", "timestamp", null, RacknetMultiplayer.pageStarts[RacknetMultiplayer.curPublicPage], -9223372036854775808L, 20, RacknetMultiplayer.gotLatestCharacters);
                    break;
                case 1:
                    if (RacknetMultiplayer.lastFilterMode != 1)
                    {
                        RacknetMultiplayer.lastFilterMode = 1;
                        RacknetMultiplayer.curPublicPage = 0;
                        RacknetMultiplayer.valueOfLastEntryOnCurrentPage = 0L;
                        RacknetMultiplayer.pageStarts = new List<long>();
                        RacknetMultiplayer.pageStarts.Add(2147483647L);
                    }
                    if (RacknetMultiplayer.curPublicPage >= RacknetMultiplayer.pageStarts.Count)
                    {
                        RacknetMultiplayer.pageStarts.Add(RacknetMultiplayer.valueOfLastEntryOnCurrentPage);
                    }
                    RacknetMultiplayer.bigDB.LoadRange("Characters", "score", null, (int)RacknetMultiplayer.pageStarts[RacknetMultiplayer.curPublicPage], -2147483648, 30, RacknetMultiplayer.gotLatestCharacters);
                    break;
                case 2:
                    if (RacknetMultiplayer.lastFilterMode != 2 || RacknetMultiplayer.lastSearchQuery != ((Component)RacknetMultiplayer.homeWindowCharacterMenu.transform.Find("PublicCharacters").Find("txtSearch")).GetComponent<InputField>().text.ToLower())
                    {
                        RacknetMultiplayer.lastFilterMode = 2;
                        RacknetMultiplayer.lastSearchQuery = ((Component)RacknetMultiplayer.homeWindowCharacterMenu.transform.Find("PublicCharacters").Find("txtSearch")).GetComponent<InputField>().text.ToLower();
                        RacknetMultiplayer.curPublicPage = 0;
                    }
                    RacknetMultiplayer.bigDB.LoadRange("Characters", "name", null, ((Component)RacknetMultiplayer.homeWindowCharacterMenu.transform.Find("PublicCharacters").Find("txtSearch")).GetComponent<InputField>().text.ToLower(), ((Component)RacknetMultiplayer.homeWindowCharacterMenu.transform.Find("PublicCharacters").Find("txtSearch")).GetComponent<InputField>().text.ToLower() + "zzzzzzzzzzzzzzzzzzzz", 50, RacknetMultiplayer.gotLatestCharacters);
                    break;
                case 3:
                    if (RacknetMultiplayer.lastFilterMode != 3 || RacknetMultiplayer.lastSearchQuery != ((Component)RacknetMultiplayer.homeWindowCharacterMenu.transform.Find("PublicCharacters").Find("txtSearch")).GetComponent<InputField>().text.ToLower())
                    {
                        RacknetMultiplayer.lastFilterMode = 3;
                        RacknetMultiplayer.lastSearchQuery = ((Component)RacknetMultiplayer.homeWindowCharacterMenu.transform.Find("PublicCharacters").Find("txtSearch")).GetComponent<InputField>().text.ToLower();
                        RacknetMultiplayer.curPublicPage = 0;
                    }
                    RacknetMultiplayer.bigDB.LoadRange("Characters", "species", null, ((Component)RacknetMultiplayer.homeWindowCharacterMenu.transform.Find("PublicCharacters").Find("txtSearch")).GetComponent<InputField>().text.ToLower(), ((Component)RacknetMultiplayer.homeWindowCharacterMenu.transform.Find("PublicCharacters").Find("txtSearch")).GetComponent<InputField>().text.ToLower() + "zzzzzzzzzzzzzzzzzzzz", 50, RacknetMultiplayer.gotLatestCharacters);
                    break;
            }
            ((Component)RacknetMultiplayer.homeWindowCharacterMenu.transform.Find("PublicCharacters").Find("txtPage")).GetComponent<Text>().text = Localization.getPhrase("PAGE", string.Empty) + " " + (RacknetMultiplayer.curPublicPage + 1);
            RacknetMultiplayer.homeWindowCharacterMenu.transform.Find("PublicCharacters").Find("cmdPageLeft").gameObject.SetActive(RacknetMultiplayer.lastFilterMode < 2 && RacknetMultiplayer.curPublicPage > 0);
            RacknetMultiplayer.homeWindowCharacterMenu.transform.Find("PublicCharacters").Find("cmdPageRight").gameObject.SetActive(RacknetMultiplayer.lastFilterMode < 2);
            RacknetMultiplayer.homeWindowCharacterMenu.transform.Find("PublicCharacters").Find("txtPage").gameObject.SetActive(RacknetMultiplayer.lastFilterMode < 2);
            RacknetMultiplayer.publicCharacterListStatus = 2;
        }
        if (RacknetMultiplayer.publicCharacterListStatus == 3)
        {
            for (int l = 0; l < RacknetMultiplayer.publicCharacterDOs.Length; l++)
            {
                GameObject gameObject2 = UnityEngine.Object.Instantiate(RacknetMultiplayer.RacknetBrowserCharacterTemplate);
                gameObject2.SetActive(true);
                gameObject2.transform.SetParent(RacknetMultiplayer.publicCharactersContainerStuff);
                gameObject2.transform.localScale = RacknetMultiplayer.RacknetBrowserCharacterTemplate.transform.localScale;
                gameObject2.transform.localRotation = Quaternion.identity;
                RacknetMultiplayer.v3 = Vector3.zero;
                RacknetMultiplayer.v3.y += 192f;
                RacknetMultiplayer.v3.y -= (float)l * 81f;
                RacknetMultiplayer.game.StartCoroutine(RacknetMultiplayer.loadTexIntoRawImage(RacknetMultiplayer.getRacknetAvatar(RacknetMultiplayer.publicCharacterDOs[l].Key), ((Component)gameObject2.transform.Find("headshot").Find("imgHeadshot")).GetComponent<RawImage>()));
                ((Component)gameObject2.transform.Find("txtName")).GetComponent<Text>().text = RacknetMultiplayer.publicCharacterDOs[l].GetString("name", string.Empty);
                ((Component)gameObject2.transform.Find("txtAuthor")).GetComponent<Text>().text = Localization.getSubPhrase("BY_AUTHOR", RacknetMultiplayer.publicCharacterDOs[l].GetString("author", string.Empty), string.Empty, string.Empty);
                gameObject2.transform.Find("GenderTokens").Find("penisOn").gameObject.SetActive(RacknetMultiplayer.publicCharacterDOs[l].GetBool("hasPenis", false));
                gameObject2.transform.Find("GenderTokens").Find("penisOff").gameObject.SetActive(!RacknetMultiplayer.publicCharacterDOs[l].GetBool("hasPenis", false));
                gameObject2.transform.Find("GenderTokens").Find("vaginaOn").gameObject.SetActive(RacknetMultiplayer.publicCharacterDOs[l].GetBool("hasVagina", false));
                gameObject2.transform.Find("GenderTokens").Find("vaginaOff").gameObject.SetActive(!RacknetMultiplayer.publicCharacterDOs[l].GetBool("hasVagina", false));
                gameObject2.transform.Find("GenderTokens").Find("boobsOn").gameObject.SetActive(RacknetMultiplayer.publicCharacterDOs[l].GetBool("hasBreasts", false));
                gameObject2.transform.Find("GenderTokens").Find("boobsOff").gameObject.SetActive(!RacknetMultiplayer.publicCharacterDOs[l].GetBool("hasBreasts", false));
                ((Component)gameObject2.transform.Find("ScorePanel").Find("txtScore")).GetComponent<Text>().text = RacknetMultiplayer.publicCharacterDOs[l].GetInt("score", 10).ToString();
                gameObject2.transform.Find("ScorePanel").Find("cmdUp").gameObject.SetActive(true);
                gameObject2.transform.Find("ScorePanel").Find("cmdDown").gameObject.SetActive(true);
                gameObject2.transform.Find("ScorePanel").Find("cmdUp").name = "upvote" + l;
                gameObject2.transform.Find("ScorePanel").Find("cmdDown").name = "downvote" + l;
                gameObject2.transform.Find("headshotFrame").name = "headshot" + l;
                gameObject2.transform.localPosition = RacknetMultiplayer.v3;
                RacknetMultiplayer.publicCharacterPanels.Add(gameObject2);
            }
            RacknetMultiplayer.v3 = Vector3.zero;
            RacknetMultiplayer.v3.y += 192f;
            RacknetMultiplayer.v3.y -= 81f * (float)RacknetMultiplayer.publicCharacterDOs.Length;
            RacknetMultiplayer.publicCharactersContentHeight = 81f * (float)RacknetMultiplayer.publicCharacterDOs.Length;
            RacknetMultiplayer.publicCharactersScrollAmount = 0f;
            if (RacknetMultiplayer.publicCharacterDOs.Length > 0)
            {
                if (RacknetMultiplayer.lastFilterMode == 0)
                {
                    RacknetMultiplayer.valueOfLastEntryOnCurrentPage = RacknetMultiplayer.publicCharacterDOs[RacknetMultiplayer.publicCharacterDOs.Length - 1].GetLong("timestamp");
                }
                if (RacknetMultiplayer.lastFilterMode == 1)
                {
                    RacknetMultiplayer.valueOfLastEntryOnCurrentPage = RacknetMultiplayer.publicCharacterDOs[RacknetMultiplayer.publicCharacterDOs.Length - 1].GetInt("score");
                }
            }
            RacknetMultiplayer.updateVoteStatus();
            RacknetMultiplayer.publicCharacterListStatus = 4;
        }
        if (RacknetMultiplayer.publicCharacterListStatus == 4)
        {
            bool active = false;
            RacknetMultiplayer.homeWindowCharacterMenu.transform.Find("PublicCharacters").Find("txtNoResults").gameObject.SetActive(RacknetMultiplayer.publicCharacterPanels.Count == 0);
            float num2 = RacknetMultiplayer.publicCharactersScrollAmount;
            Vector2 mouseScrollDelta2 = Input.mouseScrollDelta;
            RacknetMultiplayer.publicCharactersScrollAmount = num2 - mouseScrollDelta2.y * 25.5f;
            if (RacknetMultiplayer.publicCharactersScrollAmount > RacknetMultiplayer.publicCharactersContentHeight - 385f)
            {
                RacknetMultiplayer.publicCharactersScrollAmount = RacknetMultiplayer.publicCharactersContentHeight - 385f;
            }
            if (RacknetMultiplayer.publicCharactersScrollAmount < 0f)
            {
                RacknetMultiplayer.publicCharactersScrollAmount = 0f;
            }
            RacknetMultiplayer.v3 = Vector3.zero;
            RacknetMultiplayer.v3.x = 400f;
            RacknetMultiplayer.v3.y += RacknetMultiplayer.publicCharactersScrollAmount;
            Transform transform2 = RacknetMultiplayer.publicCharactersContainerStuff;
            transform2.localPosition += (RacknetMultiplayer.v3 - RacknetMultiplayer.publicCharactersContainerStuff.localPosition) * Game.cap(Time.deltaTime * 11f, 0f, 1f);
            if (RacknetMultiplayer.previewCharacter != null)
            {
                RacknetMultiplayer.characterPreviewLoadingIndicator.SetActive(true);
                if (RacknetMultiplayer.previewCharacter.initted)
                {
                    RacknetMultiplayer.characterPreviewWasOpen = true;
                    active = true;
                    RacknetMultiplayer.characterPreviewLoadingIndicator.SetActive(RacknetMultiplayer.previewCharacter.buildingTexture || RacknetMultiplayer.previewCharacter.customTexturesWeNeedToDownload.Count > 0);
                    if (RacknetMultiplayer.needToMovePreviewCharacterToPreviewBox)
                    {
                        RackCharacter rackCharacter = RacknetMultiplayer.previewCharacter;
                        Vector3 position = GameObject.Find("CharacterPreview").transform.position;
                        float x = position.x;
                        Vector3 position2 = GameObject.Find("CharacterPreview").transform.position;
                        float y = position2.y;
                        Vector3 position3 = GameObject.Find("CharacterPreview").transform.position;
                        rackCharacter.teleport(x, y, position3.z, 0f, false);
                        RacknetMultiplayer.previewCharacter.GO.transform.SetParent(GameObject.Find("CharacterPreview").transform);
                        for (int m = 0; m < RacknetMultiplayer.previewCharacter.parts.Count; m++)
                        {
                            RacknetMultiplayer.previewCharacter.parts[m].layer = 16;
                        }
                        RacknetMultiplayer.needToMovePreviewCharacterToPreviewBox = false;
                    }
                    if ((UnityEngine.Object)RacknetMultiplayer.previewCamera == (UnityEngine.Object)null)
                    {
                        RacknetMultiplayer.previewCamera = UnityEngine.Object.Instantiate(RacknetMultiplayer.game.renderCam);
                        UnityEngine.Object.Destroy(RacknetMultiplayer.previewCamera.GetComponent<RenderCam>());
                        RacknetMultiplayer.previewCamera.transform.SetParent(GameObject.Find("CharacterPreview").transform);
                        RacknetMultiplayer.previewCamera.GetComponent<Camera>().targetTexture = RacknetMultiplayer.game.characterPreviewRT;
                        RacknetMultiplayer.v3.x = -1.5f;
                        RacknetMultiplayer.v3.y = 3.9f;
                        RacknetMultiplayer.v3.z = 5f;
                        RacknetMultiplayer.previewCamera.transform.localPosition = RacknetMultiplayer.v3 * 1.3f;
                        RacknetMultiplayer.previewCamera.transform.LookAt(RacknetMultiplayer.previewCharacter.GO.transform.position + Vector3.up * 3f);
                        RacknetMultiplayer.previewCamera.SetActive(true);
                    }
                    if (Input.GetMouseButton(0) && RacknetMultiplayer.game.mouseChange.magnitude > 0.05f)
                    {
                        RacknetMultiplayer.previewCharacter.GO.transform.Rotate(0f, RacknetMultiplayer.game.mouseChange.x * 10f, 0f);
                        RacknetMultiplayer.v3 = RacknetMultiplayer.previewCamera.transform.localPosition;
                        RacknetMultiplayer.v3.y += RacknetMultiplayer.game.mouseChange.y * -0.2f;
                        RacknetMultiplayer.v3.y = Game.cap(RacknetMultiplayer.v3.y, 1.1f, 6.5f);
                        RacknetMultiplayer.previewCamera.transform.localPosition = RacknetMultiplayer.v3;
                        RacknetMultiplayer.previewCamera.transform.LookAt(RacknetMultiplayer.previewCharacter.GO.transform.position + Vector3.up * 3f);
                        RacknetMultiplayer.timeSinceManualPreviewSpin = 0f;
                    }
                    else
                    {
                        RacknetMultiplayer.timeSinceManualPreviewSpin += Time.deltaTime;
                        if (RacknetMultiplayer.timeSinceManualPreviewSpin > 8f)
                        {
                            RacknetMultiplayer.timeSinceManualPreviewSpin = 8f;
                            RacknetMultiplayer.autoSpinCharacterPreviewVel += Time.deltaTime;
                            if (RacknetMultiplayer.autoSpinCharacterPreviewVel > 8f)
                            {
                                RacknetMultiplayer.autoSpinCharacterPreviewVel = 8f;
                            }
                        }
                        else
                        {
                            RacknetMultiplayer.autoSpinCharacterPreviewVel -= Time.deltaTime * 5f;
                            if (RacknetMultiplayer.autoSpinCharacterPreviewVel < 0f)
                            {
                                RacknetMultiplayer.autoSpinCharacterPreviewVel = 0f;
                            }
                        }
                    }
                    RacknetMultiplayer.previewCharacter.GO.transform.Rotate(0f, RacknetMultiplayer.autoSpinCharacterPreviewVel * Time.deltaTime * 2f, 0f);
                    RacknetMultiplayer.previewCamera.transform.LookAt(RacknetMultiplayer.previewCharacter.GO.transform.position + Vector3.up * 3f);
                }
            }
            RacknetMultiplayer.characterPreviewImage.SetActive(active);
            RacknetMultiplayer.cmdFavorites.SetActive(active);
            RacknetMultiplayer.previewEnjoymentPanel.gameObject.SetActive(active);
        }
    }

    public static void wipeCache(string characterID)
    {
        new FileInfo(Application.persistentDataPath + "/racknetcache/").Directory.Create();
        try
        {
            if (File.Exists(Application.persistentDataPath + "/racknetcache/" + characterID + ".xml"))
            {
                File.Delete(Application.persistentDataPath + "/racknetcache/" + characterID + ".xml");
            }
            if (File.Exists(Application.persistentDataPath + "/racknetcache/" + characterID + ".png"))
            {
                File.Delete(Application.persistentDataPath + "/racknetcache/" + characterID + ".png");
            }
        }
        catch
        {
        }
    }

    public static string getRacknetCharacter(string characterID)
    {
        new FileInfo(Application.persistentDataPath + "/racknetcache/").Directory.Create();
        if (File.Exists(Application.persistentDataPath + "/racknetcache/" + characterID + ".xml"))
        {
            return "file:///" + Application.persistentDataPath + "/racknetcache/" + characterID + ".xml";
        }
        WebClient webClient = new WebClient();
        webClient.DownloadFileAsync(new Uri("http://fekrack.net/rack2/characters/xml/" + characterID + ".xml"), Application.persistentDataPath + "/racknetcache/" + characterID + ".xml");
        return "http://fekrack.net/rack2/characters/xml/" + characterID + ".xml";
    }

    public static string getRacknetAvatar(string characterID)
    {
        new FileInfo(Application.persistentDataPath + "/racknetcache/").Directory.Create();
        if (File.Exists(Application.persistentDataPath + "/racknetcache/" + characterID + ".png"))
        {
            return "file:///" + Application.persistentDataPath + "/racknetcache/" + characterID + ".png";
        }
        WebClient webClient = new WebClient();
        webClient.DownloadFileAsync(new Uri("http://fekrack.net/rack2/characters/png/" + characterID + ".png"), Application.persistentDataPath + "/racknetcache/" + characterID + ".png");
        return "http://fekrack.net/rack2/characters/png/" + characterID + ".png";
    }

    public static void savePreviewCharacterToFavorites()
    {
        if (RacknetMultiplayer.previewCharacter != null && RacknetMultiplayer.previewCharacter.initted)
        {
            if (!Inventory.data.favoriteCharacters.Contains(RacknetMultiplayer.previewCharacter.data.uid))
            {
                Inventory.data.favoriteCharacters.Add(RacknetMultiplayer.previewCharacter.data.uid);
                Inventory.saveInventoryData();
            }
            RacknetMultiplayer.previewCharacter.saveMe(true);
            RacknetMultiplayer.errorMessage = Localization.getPhrase("CHARACTER_HAS_BEEN_SAVED_TO_FAVORITES", string.Empty);
            RacknetMultiplayer.goodError = true;
        }
    }

    public static void updatePreviewCharacterPreferences()
    {
        RacknetMultiplayer.homeWindowCharacterMenu.transform.Find("cmdFavorites").gameObject.SetActive(true);
        ((Component)RacknetMultiplayer.previewEnjoymentPanel.Find("icons").Find("txtName")).GetComponent<Text>().text = RacknetMultiplayer.previewCharacter.data.name;
        RacknetMultiplayer.previewEnjoymentPanel.gameObject.SetActive(true);
        RacknetMultiplayer.v3 = Vector3.one * RacknetMultiplayer.previewCharacter.preferences["category_sensations"] / RacknetMultiplayer.previewCharacter.highestPreferenceCategoryValue;
        RacknetMultiplayer.v3 += (Vector3.one - RacknetMultiplayer.v3) * 0.25f;
        RacknetMultiplayer.previewEnjoymentPanel.Find("icons").Find("sensation").localScale = RacknetMultiplayer.v3;
        RacknetMultiplayer.v3 = Vector3.one * RacknetMultiplayer.previewCharacter.preferences["category_experiences"] / RacknetMultiplayer.previewCharacter.highestPreferenceCategoryValue;
        RacknetMultiplayer.v3 += (Vector3.one - RacknetMultiplayer.v3) * 0.25f;
        RacknetMultiplayer.previewEnjoymentPanel.Find("icons").Find("experience").localScale = RacknetMultiplayer.v3;
        RacknetMultiplayer.v3 = Vector3.one * RacknetMultiplayer.previewCharacter.preferences["category_attraction"] / RacknetMultiplayer.previewCharacter.highestPreferenceCategoryValue;
        RacknetMultiplayer.v3 += (Vector3.one - RacknetMultiplayer.v3) * 0.25f;
        RacknetMultiplayer.previewEnjoymentPanel.Find("icons").Find("attraction").localScale = RacknetMultiplayer.v3;
        if ((UnityEngine.Object)RacknetMultiplayer.previewCharacterPreferenceTemplate == (UnityEngine.Object)null)
        {
            RacknetMultiplayer.previewCharacterPreferenceTemplate = RacknetMultiplayer.previewEnjoymentPanel.Find("icons").Find("preferences").Find("Preference")
                .gameObject;
            RacknetMultiplayer.previewCharacterPreferenceTemplate.SetActive(false);
        }
        for (int i = 0; i < RacknetMultiplayer.previewCharacterPreferencePreviews.Count; i++)
        {
            UnityEngine.Object.Destroy(RacknetMultiplayer.previewCharacterPreferencePreviews[i]);
        }
        RacknetMultiplayer.previewCharacterPreferencePreviews = new List<GameObject>();
        int num = 0;
        foreach (string item in RacknetMultiplayer.previewCharacter.preferences.Keys.ToList())
        {
            if (item.Contains("category_"))
            {
                GameObject gameObject = UnityEngine.Object.Instantiate(RacknetMultiplayer.previewCharacterPreferenceTemplate);
                gameObject.SetActive(true);
                gameObject.transform.SetParent(RacknetMultiplayer.previewCharacterPreferenceTemplate.transform.parent);
                gameObject.transform.SetAsFirstSibling();
                RacknetMultiplayer.v3 = Vector3.zero;
                num++;
                RacknetMultiplayer.v3.y -= 17f * (float)(RacknetMultiplayer.previewCharacterPreferencePreviews.Count + num);
                gameObject.transform.localPosition = RacknetMultiplayer.v3;
                gameObject.transform.localScale = RacknetMultiplayer.previewCharacterPreferenceTemplate.transform.localScale;
                gameObject.transform.localRotation = RacknetMultiplayer.previewCharacterPreferenceTemplate.transform.localRotation;
                gameObject.transform.Find("plus").gameObject.SetActive(false);
                gameObject.transform.Find("minus").gameObject.SetActive(false);
                switch (item)
                {
                    case "category_experiences":
                        ((Component)gameObject.transform.Find("txt")).GetComponent<Text>().text = Localization.getPhrase("EXPERIENCE:", string.Empty).ToUpper();
                        break;
                    case "category_attraction":
                        ((Component)gameObject.transform.Find("txt")).GetComponent<Text>().text = Localization.getPhrase("ATTRACTION:", string.Empty).ToUpper();
                        break;
                    case "category_sensations":
                        ((Component)gameObject.transform.Find("txt")).GetComponent<Text>().text = Localization.getPhrase("SENSATION:", string.Empty).ToUpper();
                        break;
                }
                RacknetMultiplayer.previewCharacterPreferencePreviews.Add(gameObject);
            }
            if (SexualPreferences.getPreference(item).hideFromPreview != 1 && (SexualPreferences.getPreference(item).hideFromPreview != 2 || RacknetMultiplayer.previewCharacter.showPenis) && (SexualPreferences.getPreference(item).hideFromPreview != 3 || RacknetMultiplayer.previewCharacter.showVagina) && (RacknetMultiplayer.previewCharacter.preferences[item] > 0.5f + SexualPreferences.preferenceIndifferenceRange || RacknetMultiplayer.previewCharacter.preferences[item] < 0.5f - SexualPreferences.preferenceIndifferenceRange))
            {
                GameObject gameObject2 = UnityEngine.Object.Instantiate(RacknetMultiplayer.previewCharacterPreferenceTemplate);
                gameObject2.SetActive(true);
                gameObject2.transform.SetParent(RacknetMultiplayer.previewCharacterPreferenceTemplate.transform.parent);
                gameObject2.transform.SetAsFirstSibling();
                RacknetMultiplayer.v3 = Vector3.zero;
                RacknetMultiplayer.v3.y -= 17f * (float)(RacknetMultiplayer.previewCharacterPreferencePreviews.Count + num);
                gameObject2.transform.localPosition = RacknetMultiplayer.v3;
                gameObject2.transform.localScale = RacknetMultiplayer.previewCharacterPreferenceTemplate.transform.localScale;
                gameObject2.transform.localRotation = RacknetMultiplayer.previewCharacterPreferenceTemplate.transform.localRotation;
                string str = string.Empty;
                if (RacknetMultiplayer.previewCharacter.preferenceKnowledge[item] == 0)
                {
                    str = " (Latent)";
                }
                if (RacknetMultiplayer.previewCharacter.preferenceKnowledge[item] == 1)
                {
                    str = " (Secret)";
                }
                ((Component)gameObject2.transform.Find("txt")).GetComponent<Text>().text = Localization.getPhrase("PREFERENCE_" + item, string.Empty) + str;
                gameObject2.transform.Find("plus").gameObject.SetActive(RacknetMultiplayer.previewCharacter.preferences[item] >= 0.5f);
                gameObject2.transform.Find("minus").gameObject.SetActive(RacknetMultiplayer.previewCharacter.preferences[item] < 0.5f);
                if (RacknetMultiplayer.previewCharacter.preferences[item] >= 0.5f)
                {
                    string phrase = Localization.getPhrase("THIS_SUBJECT_ENJOYS", string.Empty);
                }
                else
                {
                    string phrase = Localization.getPhrase("THIS_SUBJECT_DOES_NOT_ENJOY", string.Empty);
                }
                RacknetMultiplayer.previewCharacterPreferencePreviews.Add(gameObject2);
            }
        }
    }

    public static void updateVoteStatus()
    {
        if (RacknetMultiplayer.upvotes != null && RacknetMultiplayer.downvotes != null)
        {
            for (int i = 0; i < RacknetMultiplayer.publicCharacterDOs.Length; i++)
            {
                RacknetMultiplayer.publicCharacterPanels[i].transform.Find("ScorePanel").Find("votedIndicator_up").gameObject.SetActive(false);
                RacknetMultiplayer.publicCharacterPanels[i].transform.Find("ScorePanel").Find("upvote" + i).gameObject.SetActive(true);
                for (int j = 0; j < RacknetMultiplayer.upvotes.Count; j++)
                {
                    if (RacknetMultiplayer.publicCharacterDOs[i].Key == RacknetMultiplayer.upvotes.GetString(j))
                    {
                        RacknetMultiplayer.publicCharacterPanels[i].transform.Find("ScorePanel").Find("votedIndicator_up").gameObject.SetActive(true);
                        RacknetMultiplayer.publicCharacterPanels[i].transform.Find("ScorePanel").Find("upvote" + i).gameObject.SetActive(false);
                    }
                }
                RacknetMultiplayer.publicCharacterPanels[i].transform.Find("ScorePanel").Find("votedIndicator_down").gameObject.SetActive(false);
                RacknetMultiplayer.publicCharacterPanels[i].transform.Find("ScorePanel").Find("downvote" + i).gameObject.SetActive(true);
                for (int k = 0; k < RacknetMultiplayer.downvotes.Count; k++)
                {
                    if (RacknetMultiplayer.publicCharacterDOs[i].Key == RacknetMultiplayer.downvotes.GetString(k))
                    {
                        RacknetMultiplayer.publicCharacterPanels[i].transform.Find("ScorePanel").Find("votedIndicator_down").gameObject.SetActive(true);
                        RacknetMultiplayer.publicCharacterPanels[i].transform.Find("ScorePanel").Find("downvote" + i).gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    public static void upvote(Transform buttonClicked)
    {
        RacknetMultiplayer.whichCharacterVote = int.Parse(buttonClicked.name.Replace("upvote", string.Empty));
        string key = RacknetMultiplayer.publicCharacterDOs[RacknetMultiplayer.whichCharacterVote].Key;
        RacknetMultiplayer.sendUpvote(key);
        for (int i = 0; i < RacknetMultiplayer.publicCharacterDOs.Length; i++)
        {
            if (RacknetMultiplayer.publicCharacterDOs[i].Key == key)
            {
                ((Component)RacknetMultiplayer.publicCharacterPanels[i].transform.Find("ScorePanel").Find("txtScore")).GetComponent<Text>().text = (int.Parse(((Component)RacknetMultiplayer.publicCharacterPanels[i].transform.Find("ScorePanel").Find("txtScore")).GetComponent<Text>().text) + RacknetMultiplayer.mod).ToString();
            }
        }
        RacknetMultiplayer.updateVoteStatus();
    }

    public static void sendUpvote(string characterID)
    {
        RacknetMultiplayer.connectionToLobby.Send("upvote", characterID);
        RacknetMultiplayer.mod = 1;
        for (int i = 0; i < RacknetMultiplayer.upvotes.Count; i++)
        {
            if (RacknetMultiplayer.upvotes.GetString(i) == characterID)
            {
                RacknetMultiplayer.upvotes.RemoveAt(i);
                RacknetMultiplayer.mod--;
            }
        }
        for (int j = 0; j < RacknetMultiplayer.downvotes.Count; j++)
        {
            if (RacknetMultiplayer.downvotes.GetString(j) == characterID)
            {
                RacknetMultiplayer.downvotes.RemoveAt(j);
                RacknetMultiplayer.mod++;
            }
        }
        RacknetMultiplayer.upvotes.Add(characterID);
    }

    public static void downvote(Transform buttonClicked)
    {
        RacknetMultiplayer.whichCharacterVote = int.Parse(buttonClicked.name.Replace("downvote", string.Empty));
        string key = RacknetMultiplayer.publicCharacterDOs[RacknetMultiplayer.whichCharacterVote].Key;
        RacknetMultiplayer.sendDownvote(key);
        for (int i = 0; i < RacknetMultiplayer.publicCharacterDOs.Length; i++)
        {
            if (RacknetMultiplayer.publicCharacterDOs[i].Key == key)
            {
                ((Component)RacknetMultiplayer.publicCharacterPanels[i].transform.Find("ScorePanel").Find("txtScore")).GetComponent<Text>().text = (int.Parse(((Component)RacknetMultiplayer.publicCharacterPanels[i].transform.Find("ScorePanel").Find("txtScore")).GetComponent<Text>().text) + RacknetMultiplayer.mod).ToString();
            }
        }
        RacknetMultiplayer.updateVoteStatus();
    }

    public static void sendDownvote(string characterID)
    {
        RacknetMultiplayer.connectionToLobby.Send("downvote", characterID);
        RacknetMultiplayer.mod = -1;
        for (int i = 0; i < RacknetMultiplayer.upvotes.Count; i++)
        {
            if (RacknetMultiplayer.upvotes.GetString(i) == characterID)
            {
                RacknetMultiplayer.upvotes.RemoveAt(i);
                RacknetMultiplayer.mod--;
            }
        }
        for (int j = 0; j < RacknetMultiplayer.downvotes.Count; j++)
        {
            if (RacknetMultiplayer.downvotes.GetString(j) == characterID)
            {
                RacknetMultiplayer.downvotes.RemoveAt(j);
                RacknetMultiplayer.mod++;
            }
        }
        RacknetMultiplayer.downvotes.Add(characterID);
    }

    public static void loadPublicCharacterPreview(Transform buttonClicked)
    {
        if (RacknetMultiplayer.previewCharacter != null)
        {
            RacknetMultiplayer.game.removeCharacter(RacknetMultiplayer.previewCharacter);
        }
        RacknetMultiplayer.game.StartCoroutine(RacknetMultiplayer.loadPreviewCharacterFromURL(RacknetMultiplayer.getRacknetCharacter(RacknetMultiplayer.publicCharacterDOs[int.Parse(buttonClicked.name.Replace("headshot", string.Empty))].Key)));
    }

    public static IEnumerator loadPreviewCharacterFromURL(string url)
    {
        WWW www = new WWW(url);
        yield return (object)www;
        RacknetMultiplayer.previewCharacter = new RackCharacter(RacknetMultiplayer.game, CharacterManager.deserializeCharacterData(www.text, url), false, null, 0f, string.Empty);
        RacknetMultiplayer.previewCharacter.isPreviewCharacter = true;
        RacknetMultiplayer.previewCharacter.racknetAccountID = url.Split(new char[]
        {
        '/'
        })[url.Split(new char[]
        {
        '/'
        }).Length - 1].Split(new char[]
        {
        '.'
        })[0];
        RacknetMultiplayer.game.addCharacter(RacknetMultiplayer.previewCharacter);
        RacknetMultiplayer.needToMovePreviewCharacterToPreviewBox = true;
        RacknetMultiplayer.updatePreviewCharacterPreferences();
        bool flag = RacknetMultiplayer.previewCamera != null;
        if (flag)
        {
            RacknetMultiplayer.previewCamera.SetActive(true);
        }
        yield break;
    }

    public static void gotLatestCharacters(DatabaseObject[] DOs)
    {
        RacknetMultiplayer.publicCharacterDOs = DOs;
        RacknetMultiplayer.publicCharacterListStatus = 3;
    }

    public static void takeMyCharacterOffline(Transform deleteButtonClicked)
    {
        string text = RacknetMultiplayer.myCharacterIDs[int.Parse(deleteButtonClicked.name.Replace("offlineButton", string.Empty))];
        RacknetMultiplayer.bigDB.DeleteKeys("Characters", new string[1]
        {
                                text
        }, RacknetMultiplayer.removedBadCharacters);
    }

    public static void uploadSelfToRacknet()
    {
        if (RacknetMultiplayer.dataTransferStatus == 0)
        {
            if (((Component)RacknetMultiplayer.cmdUploadSelf.transform.Find("chkLegal0")).GetComponent<Toggle>().isOn)
            {
                ((Component)RacknetMultiplayer.cmdUploadSelf.transform.Find("chkLegal0").Find("Label")).GetComponent<Text>().color = Color.white;
                RacknetMultiplayer.dataTransferStatus = 1;
                RacknetMultiplayer.game.createHeadshot(RacknetMultiplayer.game.PC(), RacknetMultiplayer.continueUploadingSelfToRacknet, 0.8f);
            }
            else
            {
                ((Component)RacknetMultiplayer.cmdUploadSelf.transform.Find("chkLegal0").Find("Label")).GetComponent<Text>().color = Color.red;
                RacknetMultiplayer.game.playSound("ui_error", 1f, 1f);
            }
        }
    }

    public static void continueUploadingSelfToRacknet()
    {
        TextureScale.Bilinear(RacknetMultiplayer.game.mostRecentSnapshot, 128, 128);
        RacknetMultiplayer.avatarBytes = RacknetMultiplayer.game.mostRecentSnapshot.EncodeToPNG();
        RacknetMultiplayer.waitingForXMLUpload = true;
        RacknetMultiplayer.waitingForAvatarUpload = true;
        Game.gameInstance.StartCoroutine(RacknetMultiplayer.UploadXML());
        Game.gameInstance.StartCoroutine(RacknetMultiplayer.UploadAvatar());
        RacknetMultiplayer.customTexturesUploading = new List<string>();
        RacknetMultiplayer.customTexUploadStep = 0;
        RacknetMultiplayer.customTextureURLs = new DatabaseArray();
        for (int i = 0; i < RacknetMultiplayer.game.PC().data.textureLayers.Count; i++)
        {
            if (RacknetMultiplayer.game.PC().data.textureLayers[i].isDecal || PatternIcons.isCustom(RacknetMultiplayer.game.PC().data.textureLayers[i].texture.Split('_')[0]))
            {
                RacknetMultiplayer.customTexturesUploading.Add(RacknetMultiplayer.game.PC().data.textureLayers[i].texture);
            }
            for (int j = 0; j < RacknetMultiplayer.game.PC().data.textureLayers[i].masks.Count; j++)
            {
                if (PatternIcons.isCustom(RacknetMultiplayer.game.PC().data.textureLayers[i].masks[j].texture))
                {
                    RacknetMultiplayer.customTexturesUploading.Add(RacknetMultiplayer.game.PC().data.textureLayers[i].masks[j].texture);
                }
            }
        }
        if (RacknetMultiplayer.customTexturesUploading.Count > 0)
        {
            RacknetMultiplayer.waitingForCustomTexUpload = true;
            Game.gameInstance.StartCoroutine(RacknetMultiplayer.UploadCustomTexture());
        }
        else
        {
            RacknetMultiplayer.waitingForCustomTexUpload = false;
        }
        RacknetMultiplayer.checkAvatarAndXMLUpload();
    }

    public static IEnumerator UploadCustomTexture()
    {
        string suffix = string.Empty;
        switch (RacknetMultiplayer.customTexUploadStep)
        {
            case 0:
                suffix = "_body";
                break;
            case 1:
                suffix = "_head";
                break;
            case 2:
                suffix = "_wings";
                break;
            case 3:
                suffix = "_bodyfx";
                break;
            case 4:
                suffix = "_headfx";
                break;
            case 5:
                suffix = "_wingsfx";
                break;
        }
        string fileName2 = RacknetMultiplayer.myAccountUID + RacknetMultiplayer.customTexturesUploading[0] + suffix + ".png";
        fileName2 = fileName2.Replace("decal_cache/", string.Empty);
        string existingFilename = Application.persistentDataPath + "/characterTextures/" + RacknetMultiplayer.customTexturesUploading[0] + suffix + ".png";
        if (!File.Exists(existingFilename))
        {
            RacknetMultiplayer.finishedUploadingCustomTexture();
            yield break;
        }
        WWWForm form = new WWWForm();
        form.AddField("action", "custom texture upload");
        form.AddField("fullfilename", fileName2);
        form.AddField("verify", RacknetMultiplayer.MD5Hash(GameID.racknet_upload_salt.ToString() + fileName2.Replace("/", string.Empty).Replace("\\", string.Empty) + GameID.racknet_upload_pepper.ToString()).ToLower());
        form.AddField("file", "file");
        form.AddBinaryData("file", File.ReadAllBytes(existingFilename), fileName2, "image/png");
        UnityWebRequest w = UnityWebRequest.Post("http://fekrack.net/upload_rack2_character_texture.php", form);
        yield return (object)w.Send();
        bool isNetworkError = w.isNetworkError;
        if (isNetworkError)
        {
            Debug.Log("Error uploading custom tex '" + existingFilename + "': " + w.error);
            RacknetMultiplayer.waitingForCustomTexUpload = false;
            RacknetMultiplayer.errorMessage = Localization.getPhrase("ERROR_UPLOADING_YOURSELF", string.Empty);
            RacknetMultiplayer.checkAvatarAndXMLUpload();
        }
        else
        {
            bool isDone = w.isDone;
            if (isDone)
            {
                yield return new WaitForSeconds(1f);
                RacknetMultiplayer.uploadedAvatarFilename = "http://fekrack.net/rack2/characters/customtextures/" + fileName2;
                RacknetMultiplayer.customTextureURLs.Add(RacknetMultiplayer.uploadedAvatarFilename);
                WWW w2 = new WWW("http://fekrack.net/rack2/characters/customtextures/" + fileName2);
                yield return w2;
                bool flag2 = w2.error != null && w2.error != string.Empty;
                if (flag2)
                {
                    Debug.Log("Error confirming upload:");
                    Debug.Log(w2.error);
                    RacknetMultiplayer.waitingForCustomTexUpload = false;
                    RacknetMultiplayer.errorMessage = Localization.getPhrase("ERROR_UPLOADING_YOURSELF", string.Empty);
                    RacknetMultiplayer.checkAvatarAndXMLUpload();
                }
                else
                {
                    RacknetMultiplayer.finishedUploadingCustomTexture();
                }
                w2 = null;
            }
        }
        ;
    }

    public static void finishedUploadingCustomTexture()
    {
        if (RacknetMultiplayer.customTexUploadStep == 6)
        {
            RacknetMultiplayer.customTexUploadStep = 0;
            RacknetMultiplayer.customTexturesUploading.RemoveAt(0);
            if (RacknetMultiplayer.customTexturesUploading.Count > 0)
            {
                RacknetMultiplayer.checkAvatarAndXMLUpload();
                Game.gameInstance.StartCoroutine(RacknetMultiplayer.UploadCustomTexture());
            }
            else
            {
                RacknetMultiplayer.waitingForCustomTexUpload = false;
                RacknetMultiplayer.checkAvatarAndXMLUpload();
            }
        }
        else
        {
            RacknetMultiplayer.customTexUploadStep++;
            RacknetMultiplayer.checkAvatarAndXMLUpload();
            Game.gameInstance.StartCoroutine(RacknetMultiplayer.UploadCustomTexture());
        }
    }

    public static void checkAvatarAndXMLUpload()
    {
        RacknetMultiplayer.connectionSpecificStatusText.text = "_AVATAR: ";
        if (RacknetMultiplayer.waitingForAvatarUpload)
        {
            Text text = RacknetMultiplayer.connectionSpecificStatusText;
            text.text += "UPLOADING\r\n";
        }
        else
        {
            Text text2 = RacknetMultiplayer.connectionSpecificStatusText;
            text2.text += "OK\r\n";
        }
        Text text3 = RacknetMultiplayer.connectionSpecificStatusText;
        text3.text += "_DATA: ";
        if (RacknetMultiplayer.waitingForXMLUpload)
        {
            Text text4 = RacknetMultiplayer.connectionSpecificStatusText;
            text4.text += "UPLOADING\r\n";
        }
        else
        {
            Text text5 = RacknetMultiplayer.connectionSpecificStatusText;
            text5.text += "OK\r\n";
        }
        Text text6 = RacknetMultiplayer.connectionSpecificStatusText;
        text6.text += "_CUSTOMTEX: ";
        if (RacknetMultiplayer.waitingForCustomTexUpload)
        {
            Text text7 = RacknetMultiplayer.connectionSpecificStatusText;
            text7.text = text7.text + "UPLOADING " + RacknetMultiplayer.customTexturesUploading.Count.ToString() + "\r\n";
        }
        else
        {
            Text text8 = RacknetMultiplayer.connectionSpecificStatusText;
            text8.text += "OK\r\n";
        }
        if (!RacknetMultiplayer.waitingForAvatarUpload && !RacknetMultiplayer.waitingForXMLUpload && !RacknetMultiplayer.waitingForCustomTexUpload)
        {
            if (RacknetMultiplayer.errorMessage == string.Empty)
            {
                RacknetMultiplayer.uploadCharacterInfoToBigDB();
            }
            else
            {
                RacknetMultiplayer.dataTransferStatus = 0;
                RacknetMultiplayer.myCharacterListStatus = 0;
            }
            RacknetMultiplayer.connectionSpecificStatusText.text = string.Empty;
        }
    }

    public static IEnumerator UploadXML()
    {
        byte[] uploadData = Encoding.UTF8.GetBytes(CharacterManager.serializeCharacter(RacknetMultiplayer.game.PC()));
        string characterID = RacknetMultiplayer.myAccountUID + "." + RacknetMultiplayer.game.PC().data.name;
        string fileName = characterID + ".xml";
        WWWForm form = new WWWForm();
        form.AddField("action", "character upload");
        form.AddField("verify", RacknetMultiplayer.MD5Hash(GameID.racknet_upload_salt.ToString() + fileName + GameID.racknet_upload_pepper.ToString()).ToLower());
        form.AddField("file", "file");
        form.AddBinaryData("file", uploadData, fileName, "text/xml");
        UnityWebRequest w = UnityWebRequest.Post("http://fekrack.net/upload_rack2_character_xml.php", form);
        yield return (object)w.Send();
        bool isNetworkError = w.isNetworkError;
        if (isNetworkError)
        {
            Debug.Log("Error uploading XML: " + w.error);
            RacknetMultiplayer.waitingForXMLUpload = false;
            RacknetMultiplayer.errorMessage = Localization.getPhrase("ERROR_UPLOADING_YOURSELF", string.Empty);
            RacknetMultiplayer.checkAvatarAndXMLUpload();
        }
        else
        {
            bool isDone = w.isDone;
            if (isDone)
            {
                yield return new WaitForSeconds(1f);
                RacknetMultiplayer.uploadedXMLfilename = "http://fekrack.net/rack2/characters/xml/" + fileName;
                WWW w2 = new WWW("http://fekrack.net/rack2/characters/xml/" + fileName);
                yield return w2;
                bool flag = w2.error != null && w2.error != string.Empty;
                if (flag)
                {
                    Debug.Log("Error confirming upload:");
                    Debug.Log(w2.error);
                    RacknetMultiplayer.waitingForXMLUpload = false;
                    RacknetMultiplayer.errorMessage = Localization.getPhrase("ERROR_UPLOADING_YOURSELF", string.Empty);
                    RacknetMultiplayer.checkAvatarAndXMLUpload();
                }
                else
                {
                    bool flag2 = w2.text != null && w2.text != string.Empty;
                    if (flag2)
                    {
                        bool flag3 = w2.text.Contains("<CharacterData") && w2.text.Contains("</CharacterData>");
                        if (flag3)
                        {
                            RacknetMultiplayer.waitingForXMLUpload = false;
                            RacknetMultiplayer.checkAvatarAndXMLUpload();
                        }
                        else
                        {
                            Debug.Log("Error: Character File " + fileName + " is Invalid");
                            RacknetMultiplayer.waitingForXMLUpload = false;
                            RacknetMultiplayer.errorMessage = Localization.getPhrase("ERROR_UPLOADING_YOURSELF", string.Empty);
                            RacknetMultiplayer.checkAvatarAndXMLUpload();
                        }
                    }
                    else
                    {
                        Debug.Log("Error: Character File " + fileName + " is Empty");
                        RacknetMultiplayer.waitingForXMLUpload = false;
                        RacknetMultiplayer.errorMessage = Localization.getPhrase("ERROR_UPLOADING_YOURSELF", string.Empty);
                        RacknetMultiplayer.checkAvatarAndXMLUpload();
                    }
                }
                w2 = null;
            }
        }
    }

    public static IEnumerator UploadAvatar()
    {
        string characterID = RacknetMultiplayer.myAccountUID + "." + RacknetMultiplayer.game.PC().data.name;
        string fileName = characterID + ".png";
        WWWForm form = new WWWForm();
        form.AddField("action", "avatar upload");
        form.AddField("verify", RacknetMultiplayer.MD5Hash(GameID.racknet_upload_salt.ToString() + fileName + GameID.racknet_upload_pepper.ToString()).ToLower());
        form.AddField("file", "file");
        form.AddBinaryData("file", RacknetMultiplayer.avatarBytes, fileName, "image/png");
        UnityWebRequest w = UnityWebRequest.Post("http://fekrack.net/upload_rack2_character_avatar.php", form);
        yield return (object)w.Send();
        bool isNetworkError = w.isNetworkError;
        if (isNetworkError)
        {
            Debug.Log("Error uploading avatar: " + w.error);
            RacknetMultiplayer.waitingForAvatarUpload = false;
            RacknetMultiplayer.errorMessage = Localization.getPhrase("ERROR_UPLOADING_YOURSELF", string.Empty);
            RacknetMultiplayer.checkAvatarAndXMLUpload();
        }
        else
        {
            bool isDone = w.isDone;
            if (isDone)
            {
                yield return new WaitForSeconds(1f);
                RacknetMultiplayer.uploadedAvatarFilename = "http://fekrack.net/rack2/characters/png/" + fileName;
                WWW w2 = new WWW("http://fekrack.net/rack2/characters/png/" + fileName);
                yield return w2;
                bool flag = w2.error != null && w2.error != string.Empty;
                if (flag)
                {
                    Debug.Log("Error confirming upload:");
                    Debug.Log(w2.error);
                    RacknetMultiplayer.waitingForAvatarUpload = false;
                    RacknetMultiplayer.errorMessage = Localization.getPhrase("ERROR_UPLOADING_YOURSELF", string.Empty);
                    RacknetMultiplayer.checkAvatarAndXMLUpload();
                }
                else
                {
                    RacknetMultiplayer.waitingForAvatarUpload = false;
                    RacknetMultiplayer.checkAvatarAndXMLUpload();
                }
                w2 = null;
            }
        }
        yield break;
    }

    public static void uploadCharacterInfoToBigDB()
    {
        string key = RacknetMultiplayer.myAccountUID + "." + RacknetMultiplayer.game.PC().data.name;
        RacknetMultiplayer.bigDB.LoadOrCreate("Characters", key, RacknetMultiplayer.gotCharacterForUpload, RacknetMultiplayer.errorGettingCharacterForUpload);
    }

    public static void gotCharacterForUpload(DatabaseObject CO)
    {
        DatabaseObject[] array = new DatabaseObject[2];
        RacknetMultiplayer.stuffCharacterIntoDatabaseObject(RacknetMultiplayer.game.PC(), CO);
        array[0] = CO;
        string text = RacknetMultiplayer.myAccountUID + "." + RacknetMultiplayer.game.PC().data.name;
        string text2 = RacknetMultiplayer.PlayerObject.GetString("myCharacters", string.Empty);
        if (text2 == null || text2 == string.Empty)
        {
            text2 = text;
        }
        else
        {
            bool flag = false;
            string[] array2 = text2.Split(',');
            int num = 0;
            while (num < array2.Length)
            {
                if (!(array2[num] == text))
                {
                    num++;
                    continue;
                }
                flag = true;
                break;
            }
            if (!flag)
            {
                text2 = text2 + "," + text;
            }
        }
        RacknetMultiplayer.PlayerObject.Set("myCharacters", text2);
        array[1] = RacknetMultiplayer.PlayerObject;
        RacknetMultiplayer.bigDB.SaveChanges(false, true, array, RacknetMultiplayer.uploadedSelf, RacknetMultiplayer.failedToUploadSelf);
    }

    public static void stuffCharacterIntoDatabaseObject(RackCharacter character, DatabaseObject DO)
    {
        if (((Component)RacknetMultiplayer.cmdUploadSelf.transform.Find("chkLegal2")).GetComponent<Toggle>().isOn)
        {
            DO.Set("author", RacknetMultiplayer.email);
        }
        else
        {
            DO.Set("author", "Anonymous");
        }
        DO.Set("authorAccount", RacknetMultiplayer.username);
        DO.Set("name", character.data.name.ToLower());
        DO.Set("species", character.data.species.ToLower());
        DO.Set("gameVersion", Game.gameVersion);
        DO.Set("timestamp", RacknetMP_Lobby.serverTime);
        DO.Set("hasPenis", character.data.genitalType == 0 || character.data.genitalType == 3);
        DO.Set("hasVagina", character.data.genitalType == 1 || character.data.genitalType == 3);
        DO.Set("hasBreasts", character.data.breastSize >= RackCharacter.breastThreshhold);
        DO.Set("customTextureURLs", RacknetMultiplayer.customTextureURLs);
        int @int = DO.GetInt("score", 10);
        DO.Set("score", @int);
    }

    public static void uploadedSelf()
    {
        RacknetMultiplayer.dataTransferStatus = 0;
        RacknetMultiplayer.errorMessage = Localization.getPhrase("UPLOADED_SELF_SUCCESSFULLY", string.Empty);
        RacknetMultiplayer.goodError = true;
        RacknetMultiplayer.myCharacterListStatus = 0;
    }

    public static void gotPlayerObject(DatabaseObject DO)
    {
        RacknetMultiplayer.PlayerObject = DO;
        RacknetMultiplayer.myAccountUID = RacknetMultiplayer.PlayerObject.GetString("uid", string.Empty);
        RacknetMultiplayer.upvotes = RacknetMultiplayer.PlayerObject.GetArray("upvotes");
        RacknetMultiplayer.downvotes = RacknetMultiplayer.PlayerObject.GetArray("downvotes");
        if (RacknetMultiplayer.upvotes == null)
        {
            RacknetMultiplayer.upvotes = new DatabaseArray();
        }
        if (RacknetMultiplayer.downvotes == null)
        {
            RacknetMultiplayer.downvotes = new DatabaseArray();
        }
        if (RacknetMultiplayer.myCharacterListStatus == 1)
        {
            string @string = RacknetMultiplayer.PlayerObject.GetString("myCharacters", string.Empty);
            RacknetMultiplayer.myCharacterIDs = @string.Split(',');
            List<string> list = RacknetMultiplayer.myCharacterIDs.ToList();
            for (int num = list.Count - 1; num >= 0; num--)
            {
                if (list[num] == string.Empty)
                {
                    list.RemoveAt(num);
                }
            }
            RacknetMultiplayer.myCharacterIDs = list.ToArray();
            if (@string == null || @string == string.Empty)
            {
                RacknetMultiplayer.myCharacterListStatus = 3;
                RacknetMultiplayer.myCharacterDOs = new DatabaseObject[0];
            }
            else
            {
                RacknetMultiplayer.bigDB.LoadKeysOrCreate("Characters", RacknetMultiplayer.myCharacterIDs, RacknetMultiplayer.gotMyCharacters, RacknetMultiplayer.errorGettingMyCharacters);
                RacknetMultiplayer.myCharacterListStatus = 2;
            }
        }
    }

    public static void gotMyCharacters(DatabaseObject[] DOs)
    {
        RacknetMultiplayer.myCharacterDOs = DOs;
        List<string> list = new List<string>();
        for (int i = 0; i < RacknetMultiplayer.myCharacterDOs.Length; i++)
        {
            if (RacknetMultiplayer.myCharacterDOs[i].GetString("author", string.Empty) == string.Empty)
            {
                list.Add(RacknetMultiplayer.myCharacterDOs[i].Key);
            }
        }
        if (list.Count > 0)
        {
            List<string> list2 = new List<string>();
            RacknetMultiplayer.bigDB.DeleteKeys("Characters", list.ToArray());
            string[] array = RacknetMultiplayer.PlayerObject.GetString("myCharacters", string.Empty).Split(',');
            for (int j = 0; j < array.Length; j++)
            {
                bool flag = false;
                int num = 0;
                while (num < list.Count)
                {
                    if (!(list[num] == array[j]))
                    {
                        num++;
                        continue;
                    }
                    flag = true;
                    break;
                }
                if (!flag)
                {
                    list2.Add(array[j]);
                }
            }
            string empty = string.Empty;
            if (list2.Count == 0)
            {
                empty = string.Empty;
            }
            else if (list2.Count == 1)
            {
                empty = list2[0];
            }
            else
            {
                empty = list2[0];
                for (int k = 1; k < list2.Count; k++)
                {
                    empty = "," + list2[k];
                }
            }
            RacknetMultiplayer.PlayerObject.Set("myCharacters", empty);
            RacknetMultiplayer.PlayerObject.Save(false, false, RacknetMultiplayer.removedBadCharacters);
        }
        else
        {
            RacknetMultiplayer.myCharacterListStatus = 3;
        }
    }

    public static void removedBadCharacters()
    {
        RacknetMultiplayer.myCharacterListStatus = 0;
    }

    public static void errorGettingCharacterForUpload(PlayerIOError error)
    {
        if (RacknetMultiplayer.errorMessage == string.Empty)
        {
            RacknetMultiplayer.game.playSound("ui_error", 1f, 1f);
        }
        RacknetMultiplayer.errorMessage = Localization.getPhrase("ERROR_UPLOADING_YOURSELF", string.Empty) + "\r\n" + error.Message;
        Debug.Log(error.ErrorCode);
    }

    public static void failedToUploadSelf(PlayerIOError error)
    {
        if (RacknetMultiplayer.errorMessage == string.Empty)
        {
            RacknetMultiplayer.game.playSound("ui_error", 1f, 1f);
        }
        RacknetMultiplayer.errorMessage = Localization.getPhrase("ERROR_UPLOADING_YOURSELF", string.Empty) + "\r\n" + error.Message;
        Debug.Log(error.ErrorCode);
        RacknetMultiplayer.dataTransferStatus = 0;
    }

    public static void errorFetchingPlayerObject(PlayerIOError error)
    {
        if (RacknetMultiplayer.errorMessage == string.Empty)
        {
            RacknetMultiplayer.game.playSound("ui_error", 1f, 1f);
        }
        RacknetMultiplayer.errorMessage = Localization.getPhrase("ERROR_LOADING_YOUR_CHARACTERS", string.Empty) + "\r\n" + error.Message;
        Debug.Log(error.ErrorCode);
    }

    public static void errorGettingMyCharacters(PlayerIOError error)
    {
        if (RacknetMultiplayer.errorMessage == string.Empty)
        {
            RacknetMultiplayer.game.playSound("ui_error", 1f, 1f);
        }
        RacknetMultiplayer.errorMessage = Localization.getPhrase("ERROR_LOADING_YOUR_CHARACTERS", string.Empty) + "\r\n" + error.Message;
        Debug.Log(error.ErrorCode);
    }

    public static void processUI()
    {
        if ((UnityEngine.Object)RacknetMultiplayer.RNUI == (UnityEngine.Object)null)
        {
            RacknetMultiplayer.initUI();
        }
        RacknetMultiplayer.anyRacknetUIopen = false;
        RacknetMultiplayer.processLogin();
        RacknetMultiplayer.processConnectionStatus();
        RacknetMultiplayer.processHomeWindow();
        RacknetMultiplayer.processErrorWindow();
        RacknetMultiplayer.RNUI.SetActive(RacknetMultiplayer.anyRacknetUIopen);
    }

    public static void kill()
    {
        RacknetMultiplayer.disconnectFromLobby(false);
    }
}
