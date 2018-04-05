using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Networking.NetworkSystem;

namespace Heroes
{
    public class MyManager : NetworkManager
    {

        public Button player1Button;
        public Button player2Button;

        int avatarIndex = 0;

        public Canvas characterSelectionCanvas;

        enum GameState
        {
            Won,
            Lost,
            Playing
        };


        public GameObject[] aiPrefabs;                   // prefabs
        public AudioSource gameAudio;                   // The audio source to play.
        public AudioClip[] ambients;                      // Ambient audio
        [HideInInspector] public List<AiManager> bots;     // A collection of managers for enabling and disabling different aspects of the bots.

        public Text messageText;                  // Reference to the overlay Text to display winning text, etc.
        public Text scoreText;                  // Reference to the overlay score
        public Button playButton;
        public RawImage backgroundImage;
        public Texture[] backgroundTextures;
        private int indexTexture;
        public float startDelay = 5f;             // The delay between the start of phases.
        public float endDelay = 5f;               // The delay between the end of phases.
        public float spawnDelay = 2f;

        private GameState gameState;
        private WaitForSeconds startWait;         // Used to have a delay whilst the game starts.
        private WaitForSeconds endWait;           // Used to have a delay whilst the game ends.
        private WaitForSeconds spawnWait;
        private int totalAi;                      // Total AIs that will spawn, changes per difficulty
        private int countAi;
        private int totalWins;                    // Used to increase difficulty every wins

        // Use this for initialization
        void Start()
        {

            Cursor.visible = true; // Needed after finishing game, the cursor need to be turned on again
            Cursor.lockState = CursorLockMode.None;

            // Create the delays so they only have to be made once.
            startWait = new WaitForSeconds(startDelay);
            endWait = new WaitForSeconds(endDelay);
            spawnWait = new WaitForSeconds(spawnDelay);


            totalAi = 10;

            gameState = GameState.Playing;


            indexTexture = 0;
            //InvokeRepeating("ChangeBackground", 0.04f, 0.04f);
            playButton.onClick.AddListener(StartGame);

            characterSelectionCanvas.enabled = true;
            player1Button.onClick.AddListener(delegate { AvatarPicker(player1Button.name); });
            player2Button.onClick.AddListener(delegate { AvatarPicker(player2Button.name); });
            
        }

        void ChangeBackground()
        {
            backgroundImage.texture = backgroundTextures[indexTexture];
            indexTexture = indexTexture < backgroundTextures.Length - 1 ? indexTexture + 1 : 0;
        }



        void StartGame()
        {

            Destroy(playButton.gameObject); // TODO : just hide button
            CancelInvoke("ChangeBackground");
            backgroundImage.enabled = false;
            Destroy(GetComponent<AudioListener>()); // During start screen there is no cameras because it's attached to the character
                                                    // that didn't spawn yet, so we need an audio listener on game manager to
                                                    // hear start music
                                                    //PlayRandomAmbient();
            StartCoroutine(GameLoop());
        }

        void PlayRandomAmbient()
        {
            gameAudio.clip = ambients[Random.Range(0, ambients.Length)];
            gameAudio.Play();
            Invoke("PlayRandomAmbient", gameAudio.clip.length);
        }

        /*
         * This function is used to calculate random points in a circle
         */
        Vector3 RandomCircle(Vector3 center, float radius)
        {
            float ang = Random.value * 360;
            Vector3 pos;
            pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
            pos.y = center.y;
            pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
            return pos;
        }


        /**
         * This function is used to spawn all AIs in a random circle around 0,0,0.
         * The spawning doesn't stop until all AIs has spawned
         */
        public IEnumerator SpawnAllAi()
        {

            if (countAi < totalAi)
            {

                AiManager bot = new AiManager
                {
                    instance = Instantiate(aiPrefabs[Random.Range(0, aiPrefabs.Length)], RandomCircle(Vector3.zero, Random.Range(30, 60)), new Quaternion(0, 0, 0, 0)) as GameObject // TODO : random circle spawn
                };
                bot.SetupAI();
                bots.Add(bot);
                NetworkServer.Spawn(bot.instance);
                countAi++;
                yield return spawnWait;
            }

        }

        // This is called from start and will run each phase of the game one after another.
        private IEnumerator GameLoop()
        {
            // Start off by running the 'RoundStarting' coroutine but don't return until it's finished.
            yield return StartCoroutine(GameStarting());

            // Once the 'RoundStarting' coroutine is finished, run the 'RoundPlaying' coroutine but don't return until it's finished.
            yield return StartCoroutine(GamePlaying());

            // Once execution has returned here, run the 'RoundEnding' coroutine, again don't return until it's finished.
            yield return StartCoroutine(GameEnding());

            // This code is not run until 'RoundEnding' has finished.  At which point, check if a game winner has been found.
            switch (gameState)
            {
                case GameState.Won:
                    // If there is a game winner, restart the level.
                    // SceneManager.LoadScene(0);
                    break;
                case GameState.Lost:
                    // If game is lost, restart the level.
                    // SceneManager.LoadScene(0);
                    break;
                case GameState.Playing:
                    // If there isn't a winner yet, restart this coroutine so the loop continues.
                    // Note that this coroutine doesn't yield.  This means that the current version of the GameLoop will end.
                    StartCoroutine(GameLoop());
                    break;
            }
        }

        private IEnumerator GameStarting()
        {
            countAi = 0;
            //player = playerPrefab.GetComponent<PlayerManager>();
            //player.instance = Instantiate(playerPrefab, new Vector3(0, 1, 0), new Quaternion(0, 0, 0, 0));
            SpawnAllAi();
            // DisableControl();
            gameState = GameState.Playing;
            messageText.text = "KILL ALL";


            // Wait for the specified length of time until yielding control back to the game loop.
            yield return startWait;
        }


        private IEnumerator GamePlaying()
        {
            // As soon as the round begins
            // EnableControl();

            // Clear the text from the screen.
            messageText.text = string.Empty;

            do
            {
                // ... return on the next frame.
                yield return StartCoroutine(SpawnAllAi());
            } while (!GameFinished());
        }

        private bool GameFinished()
        {
            return CountBotInstances() == 0;//|| !player.instance; // All AIs are dead or player is dead
        }


        private IEnumerator GameEnding()
        {
            // Stop from moving.
            // DisableControl();

            // If we killed all AI = won, else we lost
            if (CountBotInstances() == 0)
            {
                messageText.text = "YOU WIN";
                gameState = GameState.Won;
            }
            else
            {
                messageText.text = "GAME OVER";
                gameState = GameState.Lost;
            }

            // Wait for the specified length of time until yielding control back to the game loop.
            yield return endWait;
        }



        private void DisableControl()
        {
            for (int i = 0; i < bots.Count; i++)
            {
                bots[i].DisableControl();
            }
            //player.control = false;
        }

        private void EnableControl()
        {
            for (int i = 0; i < bots.Count; i++)
            {
                bots[i].EnableControl();
            }
            //player.control = true;
        }

        private int CountBotInstances()
        {
            int count = 0;
            for (int i = 0; i < bots.Count; i++)
            {
                if (bots[i].instance) count++;
            }
            if (countAi > count) scoreText.text = "SCORE " + (countAi - count);
            return count;
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

        public override void OnStartServer()
        {
            
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
            /*
            Transform startPos = GetStartPosition();
            if (startPos != null)
            {
                player = (GameObject)Instantiate(playerPrefab, startPos.position, startPos.rotation);
            }
            else
            {
                player = (GameObject)Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            }*/

            player = (GameObject)Instantiate(playerPrefab, new Vector3(0, 1, 0), Quaternion.identity);


            NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        }
    }
}