//using FishNet;
//using FishNet.Connection;
//using FishNet.Object;
//using FishNet.Object.Delegating;
//using FishNet.Serializing;
//using FishNet.Transporting;
//using System;
//using System.Collections.Generic;
//using TMPro;
//using UnityEngine;
//using UnityEngine.SceneManagement;

//namespace TowerWar
//{
//    public class ServernGameBridge : NetworkBehaviour, IServernGameBridge
//    {
//        public TextMeshProUGUI connectionid;
//        public int sceneHandle;
//        public Scene thisScene;
//        public PhysicsScene physicsScene;
//        public bool onlineGame;
//        public GameObject disableAudioListener;
//        public HashSet<NetworkConnection> connection;
//        public HashSet<NetworkConnection> allSceneConnections;
//        public List<int> PlayersConnIds;
//        public int greenid;
//        public int redid;
//        public int blueid;
//        public HashSet<NetworkConnection> conInThisScene;
//        private bool NetworkInitializeEarly_TowerWar\u002EServernGameBridge_Assembly\u002DCSharp\u002Edll;
//    private bool NetworkInitializeLate_TowerWar\u002EServernGameBridge_Assembly\u002DCSharp\u002Edll;

//    public static ServernGameBridge Instance { get; private set; }

//        private void OnEnable()
//        {
//            this.onlineGame = (UnityEngine.Object)InstanceFinder.NetworkManager != (UnityEngine.Object)null;
//            int num = this.onlineGame ? 1 : 0;
//        }

//        public virtual void Awake()
//        {
//            this.NetworkInitialize___Early();
//            this.Awake___UserLogic();
//            this.NetworkInitialize__Late();
//        }

//        public void Start()
//        {
//        }

//        public override void OnStartServer()
//        {
//            base.OnStartServer();
//            this.onlineGame = true;
//            if (!this.IsServerOnly)
//                return;
//            this.disableAudioListener.GetComponent<AudioListener>().enabled = false;
//            this.sceneHandle = this.gameObject.scene.handle;
//            this.GetthisScene();
//            this.physicsScene = this.thisScene.GetPhysicsScene();
//            this.ServerDebugRpC(this.SceneManager.SceneConnections.Count.ToString() + " on server start");
//        }

//        public override void OnStartClient()
//        {
//            base.OnStartClient();
//            this.checkConnecions();
//            this.AssignCivilizations();
//        }

//        [ServerRpc(RequireOwnership = false)]
//        public void checkConnecions() => this.RpcWriter___Server_checkConnecions_2166136261();

//        public void GetthisScene()
//        {
//            Scene scene = FishNet.Managing.Scened.SceneManager.GetScene(this.sceneHandle);
//            if (!scene.IsValid())
//                return;
//            this.thisScene = scene;
//        }

//        private void Update()
//        {
//            if (!this.IsServerOnly)
//                return;
//            PhysicsScene physicsScene = this.physicsScene;
//            this.physicsScene.Simulate(Time.fixedDeltaTime * 1f);
//        }

//        public void MovethistoScene(GameObject gameObject)
//        {
//            this.ServerDebugRpC("function called on both sides");
//            if (this.IsServer)
//            {
//                Debug.Log((object)"called on server side");
//                UnityEngine.SceneManagement.SceneManager.MoveGameObjectToScene(gameObject, this.thisScene);
//            }
//            if (!this.IsClient)
//                return;
//            this.ServerDebugRpC("called on client side");
//            UnityEngine.SceneManagement.SceneManager.MoveGameObjectToScene(gameObject, this.gameObject.scene);
//        }

//        public void TowerNumberFix() => GameManager.Instance.FixTowersFromTowers();

//        public void AllowCivilizationSwitch(NetworkConnection conn, NetworkObject tower) => throw new NotImplementedException();

//        public void DevreaseTowerHealthnBars() => throw new NotImplementedException();

//        public void GameEndScenario(NetworkConnection winnerConn, NetworkConnection[] losingConns) => throw new NotImplementedException();

//        public void IncreaseTowerHealthnBars() => throw new NotImplementedException();

//        [ObserversRpc]
//        public void MakePathOnClients(int fromTower, int toTower) => this.RpcWriter___Observers_MakePathOnClients_1692629761(fromTower, toTower);

//        [ServerRpc(RequireOwnership = false)]
//        public void MakePathOnServer(int fromTower, int toTower) => this.RpcWriter___Server_MakePathOnServer_1692629761(fromTower, toTower);

//        [ServerRpc(RequireOwnership = false)]
//        public void RemovePathOnServer(int fromTower, int toTower) => this.RpcWriter___Server_RemovePathOnServer_1692629761(fromTower, toTower);

//        [ObserversRpc]
//        public void RemovePathOnClients(int fromTower, int toTower) => this.RpcWriter___Observers_RemovePathOnClients_1692629761(fromTower, toTower);

//        [ServerRpc(RequireOwnership = false)]
//        public void Increasehealth() => this.RpcWriter___Server_Increasehealth_2166136261();

//        public void MakeTugpoint() => throw new NotImplementedException();

//        public void ChangeTowerCivilizationOnServer(int towerIndex, int civilization)
//        {
//            if (!this.IsServerOnly)
//                return;
//            Civilization toCivilization = (Civilization)civilization;
//            LevelDetails.Instance.Towers[towerIndex].GetComponent<ParentTowerBehaviour>().ConvertTower(toCivilization);
//            this.ChangeTowerCivilizationOnClients(towerIndex, civilization);
//            GameManager.Instance.CheckAllTowers();
//        }

//        [ObserversRpc]
//        public void ChangeTowerCivilizationOnClients(int towerIndex, int civilization) => this.RpcWriter___Observers_ChangeTowerCivilizationOnClients_1692629761(towerIndex, civilization);

//        [ServerRpc(RequireOwnership = false)]
//        public void SendPlayerConnection(NetworkConnection conn = null) => this.RpcWriter___Server_SendPlayerConnection_328543758(conn);

//        public void SetPlayers()
//        {
//        }

//        [ServerRpc(RequireOwnership = false)]
//        public void AssignCivilizations() => this.RpcWriter___Server_AssignCivilizations_2166136261();

//        [ObserversRpc]
//        public void SetCivilization(int playerid, int civilizationIndex) => this.RpcWriter___Observers_SetCivilization_1692629761(playerid, civilizationIndex);

//        public void ServerDebugRpC(string roomname) => Debug.Log((object)roomname);

//        public void SyncAllHealths() => throw new NotImplementedException();

//        public void IncreaseHealth()
//        {
//        }

//        public void SwitchOwner(NetworkConnection conn, NetworkObject tower) => throw new NotImplementedException();

//        public void SyncAllTowers() => throw new NotImplementedException();

//        public void WonOrLost(bool Won, int playerid)
//        {
//            Debug.Log((object)"WON OR LOST");
//            if (Won)
//            {
//                Debug.Log((object)("WON PLAYER " + playerid.ToString()));
//                this.PlayerWon(playerid);
//            }
//            else
//            {
//                Debug.Log((object)("LostPlayer" + playerid.ToString()));
//                this.PlayerLostGame(playerid);
//            }
//        }

//        [ObserversRpc]
//        public void PlayerLostGame(int playerid) => this.RpcWriter___Observers_PlayerLostGame_3316948804(playerid);

//        [ObserversRpc]
//        public void PlayerWon(int playerid) => this.RpcWriter___Observers_PlayerWon_3316948804(playerid);

//        public virtual void NetworkInitialize___Early()
//        {
//            if (this.NetworkInitializeEarly_TowerWar\u002EServernGameBridge_Assembly\u002DCSharp\u002Edll)
//        return;
//            this.NetworkInitializeEarly_TowerWar\u002EServernGameBridge_Assembly\u002DCSharp\u002Edll = true;
//            this.RegisterServerRpc(0U, new ServerRpcDelegate(((ServernGameBridge)null).RpcReader___Server_checkConnecions_2166136261));
//            this.RegisterObserversRpc(1U, new ClientRpcDelegate(((ServernGameBridge)null).RpcReader___Observers_MakePathOnClients_1692629761));
//            this.RegisterServerRpc(2U, new ServerRpcDelegate(((ServernGameBridge)null).RpcReader___Server_MakePathOnServer_1692629761));
//            this.RegisterServerRpc(3U, new ServerRpcDelegate(((ServernGameBridge)null).RpcReader___Server_RemovePathOnServer_1692629761));
//            this.RegisterObserversRpc(4U, new ClientRpcDelegate(((ServernGameBridge)null).RpcReader___Observers_RemovePathOnClients_1692629761));
//            this.RegisterServerRpc(5U, new ServerRpcDelegate(((ServernGameBridge)null).RpcReader___Server_Increasehealth_2166136261));
//            this.RegisterObserversRpc(6U, new ClientRpcDelegate(((ServernGameBridge)null).RpcReader___Observers_ChangeTowerCivilizationOnClients_1692629761));
//            this.RegisterServerRpc(7U, new ServerRpcDelegate(((ServernGameBridge)null).RpcReader___Server_SendPlayerConnection_328543758));
//            this.RegisterServerRpc(8U, new ServerRpcDelegate(((ServernGameBridge)null).RpcReader___Server_AssignCivilizations_2166136261));
//            this.RegisterObserversRpc(9U, new ClientRpcDelegate(((ServernGameBridge)null).RpcReader___Observers_SetCivilization_1692629761));
//            this.RegisterObserversRpc(10U, new ClientRpcDelegate(((ServernGameBridge)null).RpcReader___Observers_PlayerLostGame_3316948804));
//            this.RegisterObserversRpc(11U, new ClientRpcDelegate(((ServernGameBridge)null).RpcReader___Observers_PlayerWon_3316948804));
//        }

//        public virtual void NetworkInitialize__Late()
//        {
//            if (this.NetworkInitializeLate_TowerWar\u002EServernGameBridge_Assembly\u002DCSharp\u002Edll)
//        return;
//            this.NetworkInitializeLate_TowerWar\u002EServernGameBridge_Assembly\u002DCSharp\u002Edll = true;
//        }

//        public override void NetworkInitializeIfDisabledInternal()
//        {
//            this.NetworkInitialize___Early();
//            this.NetworkInitialize__Late();
//        }

//        private void RpcWriter___Server_checkConnecions_2166136261()
//        {
//            if (!this.IsClient)
//            {
//                (this.NetworkManager ?? InstanceFinder.NetworkManager)?.LogWarning("Cannot complete action because client is not active. This may also occur if the object is not yet initialized or if it does not contain a NetworkObject component. This message may be disabled by setting the Logging field in your attribute to LoggingType.Off.");
//            }
//            else
//            {
//                Channel channel = Channel.Reliable;
//                PooledWriter writer = WriterPool.GetWriter();
//                this.SendServerRpc(0U, writer, channel);
//                writer.Dispose();
//            }
//        }

//        public void RpcLogic___checkConnecions_2166136261()
//        {
//            this.ServerDebugRpC(this.SceneManager.SceneConnections.Count.ToString() + " on client start");
//            this.conInThisScene = this.SceneManager.SceneConnections[this.gameObject.scene];
//            foreach (NetworkConnection networkConnection in this.conInThisScene)
//            {
//                if (!this.PlayersConnIds.Contains(networkConnection.ClientId) && networkConnection.ClientId != -1)
//                    this.PlayersConnIds.Add(networkConnection.ClientId);
//            }
//        }

//        private void RpcReader___Server_checkConnecions_2166136261(
//          PooledReader PooledReader0,
//          Channel channel,
//          NetworkConnection conn)
//        {
//            if (!this.IsServer)
//                return;
//            this.RpcLogic___checkConnecions_2166136261();
//        }

//        private void RpcWriter___Observers_MakePathOnClients_1692629761(int fromTower, int toTower)
//        {
//            if (!this.IsServer)
//            {
//                (this.NetworkManager ?? InstanceFinder.NetworkManager)?.LogWarning("Cannot complete action because server is not active. This may also occur if the object is not yet initialized or if it does not contain a NetworkObject component. This message may be disabled by setting the Logging field in your attribute to LoggingType.Off");
//            }
//            else
//            {
//                Channel channel = Channel.Reliable;
//                PooledWriter writer = WriterPool.GetWriter();
//                writer.WriteInt32(fromTower);
//                writer.WriteInt32(toTower);
//                this.SendObserversRpc(1U, writer, channel, false);
//                writer.Dispose();
//            }
//        }

//        public void RpcLogic___MakePathOnClients_1692629761(int fromTower, int toTower) => PathMaker.Instance.CreatePath(fromTower, toTower);

//        private void RpcReader___Observers_MakePathOnClients_1692629761(
//          PooledReader PooledReader0,
//          Channel channel)
//        {
//            int fromTower = PooledReader0.ReadInt32();
//            int toTower = PooledReader0.ReadInt32();
//            if (!this.IsClient)
//                return;
//            this.RpcLogic___MakePathOnClients_1692629761(fromTower, toTower);
//        }

//        private void RpcWriter___Server_MakePathOnServer_1692629761(int fromTower, int toTower)
//        {
//            if (!this.IsClient)
//            {
//                (this.NetworkManager ?? InstanceFinder.NetworkManager)?.LogWarning("Cannot complete action because client is not active. This may also occur if the object is not yet initialized or if it does not contain a NetworkObject component. This message may be disabled by setting the Logging field in your attribute to LoggingType.Off.");
//            }
//            else
//            {
//                Channel channel = Channel.Reliable;
//                PooledWriter writer = WriterPool.GetWriter();
//                writer.WriteInt32(fromTower);
//                writer.WriteInt32(toTower);
//                this.SendServerRpc(2U, writer, channel);
//                writer.Dispose();
//            }
//        }

//        public void RpcLogic___MakePathOnServer_1692629761(int fromTower, int toTower)
//        {
//            PathMaker.Instance.CreatePath(fromTower, toTower);
//            this.MakePathOnClients(fromTower, toTower);
//        }

//        private void RpcReader___Server_MakePathOnServer_1692629761(
//          PooledReader PooledReader0,
//          Channel channel,
//          NetworkConnection conn)
//        {
//            int fromTower = PooledReader0.ReadInt32();
//            int toTower = PooledReader0.ReadInt32();
//            if (!this.IsServer)
//                return;
//            this.RpcLogic___MakePathOnServer_1692629761(fromTower, toTower);
//        }

//        private void RpcWriter___Server_RemovePathOnServer_1692629761(int fromTower, int toTower)
//        {
//            if (!this.IsClient)
//            {
//                (this.NetworkManager ?? InstanceFinder.NetworkManager)?.LogWarning("Cannot complete action because client is not active. This may also occur if the object is not yet initialized or if it does not contain a NetworkObject component. This message may be disabled by setting the Logging field in your attribute to LoggingType.Off.");
//            }
//            else
//            {
//                Channel channel = Channel.Reliable;
//                PooledWriter writer = WriterPool.GetWriter();
//                writer.WriteInt32(fromTower);
//                writer.WriteInt32(toTower);
//                this.SendServerRpc(3U, writer, channel);
//                writer.Dispose();
//            }
//        }

//        public void RpcLogic___RemovePathOnServer_1692629761(int fromTower, int toTower)
//        {
//            PathMaker.Instance.PathRemovalCheckInList(fromTower, toTower);
//            this.RemovePathOnClients(fromTower, toTower);
//        }

//        private void RpcReader___Server_RemovePathOnServer_1692629761(
//          PooledReader PooledReader0,
//          Channel channel,
//          NetworkConnection conn)
//        {
//            int fromTower = PooledReader0.ReadInt32();
//            int toTower = PooledReader0.ReadInt32();
//            if (!this.IsServer)
//                return;
//            this.RpcLogic___RemovePathOnServer_1692629761(fromTower, toTower);
//        }

//        private void RpcWriter___Observers_RemovePathOnClients_1692629761(int fromTower, int toTower)
//        {
//            if (!this.IsServer)
//            {
//                (this.NetworkManager ?? InstanceFinder.NetworkManager)?.LogWarning("Cannot complete action because server is not active. This may also occur if the object is not yet initialized or if it does not contain a NetworkObject component. This message may be disabled by setting the Logging field in your attribute to LoggingType.Off");
//            }
//            else
//            {
//                Channel channel = Channel.Reliable;
//                PooledWriter writer = WriterPool.GetWriter();
//                writer.WriteInt32(fromTower);
//                writer.WriteInt32(toTower);
//                this.SendObserversRpc(4U, writer, channel, false);
//                writer.Dispose();
//            }
//        }

//        public void RpcLogic___RemovePathOnClients_1692629761(int fromTower, int toTower) => PathMaker.Instance.PathRemovalCheckInList(fromTower, toTower);

//        private void RpcReader___Observers_RemovePathOnClients_1692629761(
//          PooledReader PooledReader0,
//          Channel channel)
//        {
//            int fromTower = PooledReader0.ReadInt32();
//            int toTower = PooledReader0.ReadInt32();
//            if (!this.IsClient)
//                return;
//            this.RpcLogic___RemovePathOnClients_1692629761(fromTower, toTower);
//        }

//        private void RpcWriter___Server_Increasehealth_2166136261()
//        {
//            if (!this.IsClient)
//            {
//                (this.NetworkManager ?? InstanceFinder.NetworkManager)?.LogWarning("Cannot complete action because client is not active. This may also occur if the object is not yet initialized or if it does not contain a NetworkObject component. This message may be disabled by setting the Logging field in your attribute to LoggingType.Off.");
//            }
//            else
//            {
//                Channel channel = Channel.Reliable;
//                PooledWriter writer = WriterPool.GetWriter();
//                this.SendServerRpc(5U, writer, channel);
//                writer.Dispose();
//            }
//        }

//        public void RpcLogic___Increasehealth_2166136261()
//        {
//            int num = this.IsServerOnly ? 1 : 0;
//        }

//        private void RpcReader___Server_Increasehealth_2166136261(
//          PooledReader PooledReader0,
//          Channel channel,
//          NetworkConnection conn)
//        {
//            if (!this.IsServer)
//                return;
//            this.RpcLogic___Increasehealth_2166136261();
//        }

//        private void RpcWriter___Observers_ChangeTowerCivilizationOnClients_1692629761(
//          int towerIndex,
//          int civilization)
//        {
//            if (!this.IsServer)
//            {
//                (this.NetworkManager ?? InstanceFinder.NetworkManager)?.LogWarning("Cannot complete action because server is not active. This may also occur if the object is not yet initialized or if it does not contain a NetworkObject component. This message may be disabled by setting the Logging field in your attribute to LoggingType.Off");
//            }
//            else
//            {
//                Channel channel = Channel.Reliable;
//                PooledWriter writer = WriterPool.GetWriter();
//                writer.WriteInt32(towerIndex);
//                writer.WriteInt32(civilization);
//                this.SendObserversRpc(6U, writer, channel, false);
//                writer.Dispose();
//            }
//        }

//        public void RpcLogic___ChangeTowerCivilizationOnClients_1692629761(
//          int towerIndex,
//          int civilization)
//        {
//            Civilization toCivilization = (Civilization)civilization;
//            LevelDetails.Instance.Towers[towerIndex].GetComponent<ParentTowerBehaviour>().ConvertTower(toCivilization);
//        }

//        private void RpcReader___Observers_ChangeTowerCivilizationOnClients_1692629761(
//          PooledReader PooledReader0,
//          Channel channel)
//        {
//            int towerIndex = PooledReader0.ReadInt32();
//            int civilization = PooledReader0.ReadInt32();
//            if (!this.IsClient)
//                return;
//            this.RpcLogic___ChangeTowerCivilizationOnClients_1692629761(towerIndex, civilization);
//        }

//        private void RpcWriter___Server_SendPlayerConnection_328543758(NetworkConnection conn = null)
//        {
//            if (!this.IsClient)
//            {
//                (this.NetworkManager ?? InstanceFinder.NetworkManager)?.LogWarning("Cannot complete action because client is not active. This may also occur if the object is not yet initialized or if it does not contain a NetworkObject component. This message may be disabled by setting the Logging field in your attribute to LoggingType.Off.");
//            }
//            else
//            {
//                Channel channel = Channel.Reliable;
//                PooledWriter writer = WriterPool.GetWriter();
//                this.SendServerRpc(7U, writer, channel);
//                writer.Dispose();
//            }
//        }

//        public void RpcLogic___SendPlayerConnection_328543758(NetworkConnection conn = null)
//        {
//        }

//        private void RpcReader___Server_SendPlayerConnection_328543758(
//          PooledReader PooledReader0,
//          Channel channel,
//          NetworkConnection conn)
//        {
//            if (!this.IsServer)
//                return;
//            this.RpcLogic___SendPlayerConnection_328543758(conn);
//        }

//        private void RpcWriter___Server_AssignCivilizations_2166136261()
//        {
//            if (!this.IsClient)
//            {
//                (this.NetworkManager ?? InstanceFinder.NetworkManager)?.LogWarning("Cannot complete action because client is not active. This may also occur if the object is not yet initialized or if it does not contain a NetworkObject component. This message may be disabled by setting the Logging field in your attribute to LoggingType.Off.");
//            }
//            else
//            {
//                Channel channel = Channel.Reliable;
//                PooledWriter writer = WriterPool.GetWriter();
//                this.SendServerRpc(8U, writer, channel);
//                writer.Dispose();
//            }
//        }

//        public void RpcLogic___AssignCivilizations_2166136261()
//        {
//            for (int index = 0; index < this.PlayersConnIds.Count; ++index)
//            {
//                this.SetCivilization(this.PlayersConnIds[index], index);
//                if (!GameManager.Instance.playerCivAndIDs.ContainsKey((Civilization)index))
//                    GameManager.Instance.playerCivAndIDs.Add((Civilization)index, this.PlayersConnIds[index]);
//            }
//        }

//        private void RpcReader___Server_AssignCivilizations_2166136261(
//          PooledReader PooledReader0,
//          Channel channel,
//          NetworkConnection conn)
//        {
//            if (!this.IsServer)
//                return;
//            this.RpcLogic___AssignCivilizations_2166136261();
//        }

//        private void RpcWriter___Observers_SetCivilization_1692629761(
//          int playerid,
//          int civilizationIndex)
//        {
//            if (!this.IsServer)
//            {
//                (this.NetworkManager ?? InstanceFinder.NetworkManager)?.LogWarning("Cannot complete action because server is not active. This may also occur if the object is not yet initialized or if it does not contain a NetworkObject component. This message may be disabled by setting the Logging field in your attribute to LoggingType.Off");
//            }
//            else
//            {
//                Channel channel = Channel.Reliable;
//                PooledWriter writer = WriterPool.GetWriter();
//                writer.WriteInt32(playerid);
//                writer.WriteInt32(civilizationIndex);
//                this.SendObserversRpc(9U, writer, channel, false);
//                writer.Dispose();
//            }
//        }

//        public void RpcLogic___SetCivilization_1692629761(int playerid, int civilizationIndex)
//        {
//            if (this.LocalConnection.ClientId != playerid)
//                return;
//            LevelDetails.Instance.playerCivilization = (Civilization)civilizationIndex;
//            this.connectionid.text = LevelDetails.Instance.playerCivilization.ToString();
//        }

//        private void RpcReader___Observers_SetCivilization_1692629761(
//          PooledReader PooledReader0,
//          Channel channel)
//        {
//            int playerid = PooledReader0.ReadInt32();
//            int civilizationIndex = PooledReader0.ReadInt32();
//            if (!this.IsClient)
//                return;
//            this.RpcLogic___SetCivilization_1692629761(playerid, civilizationIndex);
//        }

//        private void RpcWriter___Observers_PlayerLostGame_3316948804(int playerid)
//        {
//            if (!this.IsServer)
//            {
//                (this.NetworkManager ?? InstanceFinder.NetworkManager)?.LogWarning("Cannot complete action because server is not active. This may also occur if the object is not yet initialized or if it does not contain a NetworkObject component. This message may be disabled by setting the Logging field in your attribute to LoggingType.Off");
//            }
//            else
//            {
//                Channel channel = Channel.Reliable;
//                PooledWriter writer = WriterPool.GetWriter();
//                writer.WriteInt32(playerid);
//                this.SendObserversRpc(10U, writer, channel, false);
//                writer.Dispose();
//            }
//        }

//        public void RpcLogic___PlayerLostGame_3316948804(int playerid)
//        {
//            if (this.LocalConnection.ClientId != playerid)
//                return;
//            this.connectionid.text = "i lost";
//            Time.timeScale = 0.0f;
//        }

//        private void RpcReader___Observers_PlayerLostGame_3316948804(
//          PooledReader PooledReader0,
//          Channel channel)
//        {
//            int playerid = PooledReader0.ReadInt32();
//            if (!this.IsClient)
//                return;
//            this.RpcLogic___PlayerLostGame_3316948804(playerid);
//        }

//        private void RpcWriter___Observers_PlayerWon_3316948804(int playerid)
//        {
//            if (!this.IsServer)
//            {
//                (this.NetworkManager ?? InstanceFinder.NetworkManager)?.LogWarning("Cannot complete action because server is not active. This may also occur if the object is not yet initialized or if it does not contain a NetworkObject component. This message may be disabled by setting the Logging field in your attribute to LoggingType.Off");
//            }
//            else
//            {
//                Channel channel = Channel.Reliable;
//                PooledWriter writer = WriterPool.GetWriter();
//                writer.WriteInt32(playerid);
//                this.SendObserversRpc(11U, writer, channel, false);
//                writer.Dispose();
//            }
//        }

//        public void RpcLogic___PlayerWon_3316948804(int playerid)
//        {
//            if (this.LocalConnection.ClientId != playerid)
//                return;
//            this.connectionid.text = "i won" + playerid.ToString() + this.LocalConnection.ClientId.ToString();
//            Time.timeScale = 0.0f;
//        }

//        private void RpcReader___Observers_PlayerWon_3316948804(
//          PooledReader PooledReader0,
//          Channel channel)
//        {
//            int playerid = PooledReader0.ReadInt32();
//            if (!this.IsClient)
//                return;
//            this.RpcLogic___PlayerWon_3316948804(playerid);
//        }

//        public virtual void Awake___UserLogic()
//        {
//            Time.timeScale = 1f;
//            if (!((UnityEngine.Object)ServernGameBridge.Instance == (UnityEngine.Object)null))
//                return;
//            ServernGameBridge.Instance = this;
//        }
//    }
//}
