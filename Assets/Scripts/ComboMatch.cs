using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComboMatch : MonoBehaviour
{

    [Header("UI Elements")]
    public RectTransform gameboard;
    public RectTransform turnBox;
    public RectTransform killedBoard;
    public RectTransform inventory;
    public RectTransform shoplist;
    public RectTransform highScoreGrid;
    public RectTransform unlockGrid;
    public RectTransform timerPanel;
    public Button startButton;
    public Button saveButton;
    public Button settingsButton;
    public Toggle chaos;
    public Toggle zen;
    public Slider redKeyBar;
    public int redBarCount;
    public Slider blueKeyBar;
    public int blueBarCount;
    public TextMeshProUGUI redKeysText;
    public TextMeshProUGUI blueKeysText;
    public Image timerImage;
    public Image timerBackgroundImage;
    public GameObject timerBreak;
    public GameObject chaosPanel;
    public GameObject movesHighScorePanel;
    public GameObject pointsHighScorePanel;
    public GameObject chaosGoldPanel;
    public GameObject chaosDescriptionPanel;
    public GameObject zenDescriptionPanel;
    public Text chaosGoldText;
    public Text selectedPowerups;
    public Text tokenNumber;
    public Sprite blankErrorHide; //blank Sprite
    public Sprite[] pieces;
    public Sprite[] upgradedPieces;
    public Sprite[] unlockedPieces;
    public Sprite[] pointsUnlockItems;
    public int[] unlockGoals;
    public string[] unlockDescriptions;
    public Sprite[] chaosUnlockedPieces;
    public int[] chaosUnlockGoals;
    public string[] chaosUnlockDescriptions;
    public string[] pointsUnlocks;
    public GameObject[] tokenButtons;
    public string[] powerupDescriptions;
    public GameObject descriptionPanel;
    public int[] tokenPrices;
    public Sprite[] powerupSprites;
    public string[] powerups;
    public AudioManager audioManager;
    public CameraShake cameraShake;
    public int[] levelPoints;
    public int currentLevel;
    public int redKeys;
    public int redKeyValue;
    public int blueKeys;
    public int blueKeyValue;
    public int movesEndCount;
    public int goldKeys;
    public int currentPoints;

    [Header("Prefabs")]
    public GameObject nodePiece;
    public GameObject powerUpItem;
    public GameObject shopItem;
    public GameObject shopCosmetic;
    public GameObject killedPiece;
    public GameObject destroyEffect;
    public GameObject token;
    public GameObject explosive;
    public GameObject tempPowerUp;
    public GameObject crate;
    public GameObject projectile;
    //public GameObject moveHighScoreEffect;
    //public GameObject pointsHighScoreEffect;
    public GameObject unlockItem;
    public GameObject[] sprinkleEffects;
    public GameObject rippleEffect;
    public GameObject reverseRippleEffect;
    public GameObject ripple2Effect;
    public GameObject ripple2LargeEffect;
    public GameObject sparkEffect;
    public GameObject downArrow;
    public GameObject upArrow;
    public GameObject redKeyEffect;
    public GameObject blueKeyEffect;
    public GameObject extraTurn;
    public GameObject turnExtended;
    public GameObject threeLeft;
    public GameObject twoLeft;
    public GameObject oneLeft;
    public GameObject shockwave;
    public GameObject[] explosionAnimations;
    public GameObject[] explosionAnimationsSmall;
    public GameObject tutorialPanel;

    Sprite[] piecesHold = new Sprite[9];
    GameObject extendObj;
    Bomb activeBomb;
    static float gravityWaitTimer = 0f;
    static float scoringWaitTimer = 1000f;
    static float tutorialWaitTimer = 0f;

    int maxLevel = 27;
    int latestTokenDescription;
    int inventorySlots = 1;
    int chaosGold = 0;
    int score = 0;
    int width = 9;
    int height = 13;
    int[] fills;
    int numberOfTypesToSpawn = 4;
    int redMatchCount = 0;
    int numberOfPieces = 9;
    public int tokens;
    public int totalTokens;
    public bool zenMode;
    public bool gameEnd = false;
    public bool gameRunning = false;
    public bool tutorialStop = false; //prevents stones moving for certain parts of tutorial.
    public bool chaosMode = false;
    public bool haveTempPowerUp = false;
    public bool spread;
    public int timerBroken;
    public int armedBombStatus = 0;
    public int armedBombIndexX;
    public int armedBombIndexY;
    public int magicIndexX;
    public int magicIndexY;
    public bool magicTouch;
    public bool skinChange;
    public int skinChangeMoves;
    bool armedButNotFired;
    bool flashRedInProgress;
    bool matchFourRecorded;
    bool cashoutFinished = false;
    bool cashoutStarted = false;
    bool tutorialError;
    bool bombUsed = false;
    bool tutorial;
    public int tutorialPhase = 1;
    int tutorialMatchCount = 0;
    int crateMagicPowerup = 0;
    int blackStopPowerup = 0;
    bool scored = false;
    bool finishedScoring = false;
    bool infected;
    int blackPieceCount = 0;
    Node[,] board;
    int[] typecount;
    public HashSet<string> popups; //format eg: x9y5
    NodePiece[,] scorePieces;


    List<int> inventoryCheck;
    List<NodePiece> update;
    List<NodePiece> infectedPieces;
    List<FlippedPieces> flipped;
    List<NodePiece> dead;
    List<KilledPiece> killed;
    public List<TimerEdit> timerEdits;

    System.Random random;

    void Start()
    {
        //for (int i = 0; i < pointsUnlocks.Length; i++)
        //{
        //    PlayerPrefs.SetInt(pointsUnlocks[i], 1);
        //    PlayerPrefs.SetInt("Piece" + i + "Chaos", 1);
        //    PlayerPrefs.SetInt("Piece" + i + "Unlocked", 1);
        //}
        tutorialPanel.SetActive(false);
        StartGame();
        if (audioManager.tutorial)
        {
            tutorialWaitTimer = 0f;
            tutorialPanel.SetActive(true);
            tutorialError = false;
            audioManager.tutorial = false;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Point p = new Point(x, y);
                    Node n = getNodeAtPoint(p);
                    NodePiece np = n.getPiece();
                    int newVal;
                    newVal = pieces.Length;
                    if (y >= 8)
                    {
                        newVal = pieces.Length - 2;
                    }
                    if (x == 2)
                    {
                        if(y==2 || y == 4)
                        {
                            newVal = 2;
                        }
                    }
                    if (x == 3)
                    {
                        if (y == 3)
                        {
                            newVal = 2;
                        }
                        if (y == 5)
                        {
                            newVal = 3;
                        }
                        if (y == 6)
                        {
                            newVal = 4;
                        }
                        if (y == 7)
                        {
                            newVal = 6;
                        }
                    }
                    if (x == 4)
                    {
                        if (y == 4)
                        {
                            newVal = 3;
                        }
                        if (y == 5)
                        {
                            newVal = 4;
                        }
                        if (y == 7)
                        {
                            newVal = 5;
                        }
                        if (y == 8)
                        {
                            newVal = 6;
                        }
                    }
                    if(x == 5 && y == 6)
                    {
                        newVal = 5;
                    }
                    np.Initialize(newVal, p, pieces[newVal - 1]);
                    setValueAtPoint(p, newVal);
                }
            }

            FindObjectOfType<GameManager>().ResumeFromSave();

            FindObjectOfType<CountdownTimer>().BreakTimer();
            timerBreak.SetActive(true);
            timerBroken = 99;
            armedButNotFired = false;
            tutorial = true;
            tutorialStop = true;
            TurnTracker.turnsRemaining += 92;
            AdjustTimer();
            StartPlaying();
        }


        if (audioManager.resume)
        {
            audioManager.resume = false;
            if(PlayerPrefs.GetInt("gameMode") == 3)
            {
                zenMode = true;
                timerBreak.SetActive(true);
                foreach (Transform node in gameboard)
                {
                    Destroy(node.gameObject);
                }
                InitializeBoard();
                VerifyBoard();
                InstantiateBoard();
            }
            else if (PlayerPrefs.GetInt("gameMode") == 2)
            {
                chaosMode = true;
            }
            TurnTracker.turnsRemaining = int.Parse(PlayerPrefs.GetString("StatsSave").Substring(0, 5));
            MoveCounter.movesMade = int.Parse(PlayerPrefs.GetString("StatsSave").Substring(5, 5));
            tokens = int.Parse(PlayerPrefs.GetString("StatsSave").Substring(10, 5));
            int counter = 0;
            int infectionTrack = 0;
            string infection = PlayerPrefs.GetString("infectionSave");
            int infectionCount = infection.Length / 5;
            int infectionX = -1;
            int infectionY = -1;
            int infectionPhase = -1;
            if (infectionCount > 0)
            {
                infectionX = int.Parse(infection.Substring(infectionTrack, 2));
                infectionY = int.Parse(infection.Substring(infectionTrack + 2, 2));
                infectionPhase = int.Parse(infection.Substring(infectionTrack + 4, 1));
                infected = true;
            }
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Point p = new Point(x, y);
                    Node n = getNodeAtPoint(p);
                    NodePiece np = n.getPiece();

                    int newVal;

                    if (PlayerPrefs.GetString("GameSave").Substring(counter, 1) == "-")
                    {
                        newVal = 0;
                        n.value = -1;
                        counter++;
                    }
                    else
                    {
                        newVal = int.Parse(PlayerPrefs.GetString("GameSave").Substring(counter, 1));
                        np.Initialize(newVal, p, pieces[newVal - 1]);
                        setValueAtPoint(p, newVal);
                    }
                    if (infectionCount > 0 && x == infectionX  && y == infectionY)
                    {
                        np.infectionPhase = infectionPhase;
                        np.infection.SetActive(true);
                        infectionTrack += 5;
                        infectionCount--;
                        if(infectionCount > 0)
                        {
                            infectionX = int.Parse(infection.Substring(infectionTrack, 2));
                            infectionY = int.Parse(infection.Substring(infectionTrack + 2, 2));
                            infectionPhase = int.Parse(infection.Substring(infectionTrack + 4, 1));
                        }
                    }
                    counter++;
                }
            }

            for(int i = 0; i<PlayerPrefs.GetString("InventorySave").Length; i+=2)
            {
                AddPowerup(int.Parse(PlayerPrefs.GetString("InventorySave").Substring(i, 2)));
            }
            if(PlayerPrefs.GetString("TempInvSave").Length > 0)
            {
                CreateTempPowerup(int.Parse(PlayerPrefs.GetString("TempInvSave").Substring(0, 2)));
            }
            
            for (int i = 0; i < PlayerPrefs.GetInt("timerEdits"); i++)
            {
                timerEdits.Add(new TimerEdit(PlayerPrefs.GetFloat("timerEditTime" + i), PlayerPrefs.GetInt("timerEditTurns" + i)));

            }
            blackStopPowerup = PlayerPrefs.GetInt("blackStop");
            crateMagicPowerup = PlayerPrefs.GetInt("crateMagic");
            FindObjectOfType<GameManager>().ResumeFromSave();
            if (PlayerPrefs.GetInt("timerBroken", 0) > 0)
            {
                FindObjectOfType<CountdownTimer>().BreakTimer();
                timerBreak.SetActive(true);
                timerBroken = PlayerPrefs.GetInt("timerBroken");
                PlayerPrefs.SetInt("timerBroken",0);
            }
            
            PlayerPrefs.SetInt("GameSaved", 0);
            armedButNotFired = false;
            AdjustTimer();
            StartPlaying();
        }
    }

    void StartGame()
    {
        //int total = 0;
        //foreach(int i in levelPoints)
        //{
        //    total += i;
        //    Debug.Log(total);
        //}

        for (int i = 0; i<numberOfPieces; i++) //sets piece to upgraded version if enabled.
        {
            int skinNumber = i + 1;
            if(PlayerPrefs.GetInt("Piece" + i + "Enabled") == 1 && PlayerPrefs.GetInt("Skin " + skinNumber + " Unlocked") == 1)
            {
                pieces[i] = upgradedPieces[i];
            }
            if (PlayerPrefs.GetInt("Piece" + i + "Enabled") == 2 && PlayerPrefs.GetInt("Piece" + i + "Unlocked") == 1)
            {
                pieces[i] = unlockedPieces[i];
            }
            if (PlayerPrefs.GetInt("Piece" + i + "Enabled") == 3 && PlayerPrefs.GetInt("Piece" + i + "Chaos") == 1)
            {
                pieces[i] = chaosUnlockedPieces[i];
            }
        }

        audioManager = FindObjectOfType<AudioManager>();
        armedButNotFired = false;
        zenMode = false;
        timerImage.sprite = pieces[pieces.Length - 1];
        timerBackgroundImage.sprite = pieces[pieces.Length - 1];
        popups = new HashSet<string>();
        tokens = 0;
        totalTokens = 0;
        FindObjectOfType<SettingsMenu>().SetSlider(PlayerPrefs.GetFloat("MainVolume"));
        FindObjectOfType<SettingsMenu>().SetMusicSlider(PlayerPrefs.GetFloat("MusicVolume"));
        numberOfTypesToSpawn = 4;
        redMatchCount = 0;
        blackStopPowerup = 0;
        crateMagicPowerup = 0;
        haveTempPowerUp = false;
        chaosMode = false;
        spread = false;
        if(PlayerPrefs.GetInt("Chaos Mode Unlocked") == 1)
        {
            chaosPanel.SetActive(true);
        }
        else
        {
            chaosPanel.SetActive(false);
        }
        if (zenMode)
        {
            timerBroken = 1;
            timerBreak.SetActive(true);
        }
        else
        {
            timerBroken = 0;
            timerBreak.SetActive(false);
        }
        chaosGold = 0;
        gameEnd = false;
        gameRunning = false;
        scored = false;
        finishedScoring = false;
        skinChange = false;
        skinChangeMoves = 0;
        blackPieceCount = 0;
        armedBombStatus = 0;
        PlayerPrefs.SetInt("PlusBombs", 1);
        armedBombIndexX = -1;
        armedBombIndexY = -1;
        magicTouch = false;
        magicIndexX = -1;
        magicIndexY = -1;
        scoringWaitTimer = 1000f;
        gravityWaitTimer = 0f;
        FindObjectOfType<ScoreCounter>().Reset();
        FindObjectOfType<CountdownTimer>().Reset();
        FindObjectOfType<MoveCounter>().Reset();
        FindObjectOfType<TurnTracker>().Reset();
        FindObjectOfType<GameManager>().gameOverMenu.SetActive(false);
        FindObjectOfType<GameManager>().pauseMenu.SetActive(false);
        FindObjectOfType<GameManager>().shopMenu.SetActive(true);
        settingsButton.interactable = false;
        chaosGoldPanel.SetActive(false);
        movesHighScorePanel.SetActive(false);
        pointsHighScorePanel.SetActive(false);
        cashoutFinished = false;
        fills = new int[width];
        string seed = getRandomSeed();
        random = new System.Random(seed.GetHashCode());
        killed = new List<KilledPiece>();
        update = new List<NodePiece>();
        flipped = new List<FlippedPieces>();
        dead = new List<NodePiece>();
        infectedPieces = new List<NodePiece>();
        timerEdits = new List<TimerEdit>();
        inventoryCheck = new List<int>();
        InitializeBoard();
        VerifyBoard();
        InstantiateBoard();

        //string inv = PlayerPrefs.GetString("Inventory");
        //for (int i = 0; i < inv.Length; i++)
        //{
        //    string key = "abcdefghijklmnopqrstuvwxyz";
        //    char k = inv[i];

        //    RestorePowerup(key.IndexOf(k));
        //}
        //PlayerPrefs.SetString("Inventory", "");

        for (int i = 0; i < powerups.Length; i++) //Instantiate the Shop.
        {
            startButton.interactable = false;
            chaosDescriptionPanel.SetActive(false);
            zenDescriptionPanel.SetActive(false);
            string key = powerups[i] + " Unlocked";
            GameObject s = Instantiate(shopItem, shoplist);
            ShopItem item = shopItem.GetComponent<ShopItem>();
            item.Initialize(i, powerupSprites[i], powerups[i], powerupDescriptions[i], tokenPrices[i]);
            item.gameObject.SetActive(true);
            if(item.name.Substring(0, 4) != "time")
            {
                item.name = i + " powerup";
            }
            item.itemLock.gameObject.SetActive(false);
            if (i == 0 || i == 2)
            {
                item.itemLock.gameObject.SetActive(false);
            }
            else if (PlayerPrefs.GetInt(key, 0) != 1) //unlock powerups
            {
                item.itemLock.gameObject.SetActive(true);

            }
            else
            {
                item.itemLock.gameObject.SetActive(false);
            }
        }
        //DETECT AND RECTIFY BUGGED POWERUP
        GameObject buggedPowerUp = GameObject.Find("Shop Item(Clone)");
        if(buggedPowerUp != null)
        {
            buggedPowerUp = GameObject.Find(powerups.Length-1 + " powerup");
            Destroy(buggedPowerUp);
            int i = powerups.Length - 1;
            GameObject s = Instantiate(shopItem, shoplist);
            ShopItem item = shopItem.GetComponent<ShopItem>();
            item.Initialize(i, powerupSprites[i], powerups[i], powerupDescriptions[i], tokenPrices[i]);
            item.gameObject.SetActive(true);
            item.name = i + " powerup";
            item.itemLock.gameObject.SetActive(false);
            string key = powerups[i] + " Unlocked";
            if (PlayerPrefs.GetInt(key, 0) != 1)
            {
                item.itemLock.gameObject.SetActive(true);
            }
        }
        int topIndex = 0; //move unlocked powerups to the top
        foreach (Transform child in shoplist) 
        {
            ShopItem s = child.GetComponent<ShopItem>();
            if (!s.itemLock.IsActive())
            {
                child.SetSiblingIndex(topIndex);
                topIndex++;
            }
        }
        for(int i = 2; i<=8; i++)
        {
            int unlockcheck = PlayerPrefs.GetInt("Slot " + i + " Unlocked", 0);
            if(unlockcheck == 1)
            {
                inventorySlots = i;
            }
            else
            {
                break;
            }
        }
        selectedPowerups.text = inventoryCheck.Count + "/" + inventorySlots + " SELECTED";


    }

    void Update()
    {
        tokenNumber.text = tokens.ToString();
        if (infected && !spread)
        {
            spreadInfection();
        }
        if (magicTouch && magicIndexX >= 0)
        {
            Point p = new Point(magicIndexX, magicIndexY);
            Node n = getNodeAtPoint(p);
            NodePiece np = n.getPiece();
            int newVal;
            if (np.value <= numberOfPieces - 1)
            {
                newVal = np.value + 1;
            }
            else
            {
                newVal = 1;
            }
            np.Initialize(newVal, p, pieces[newVal - 1]);
            setValueAtPoint(p, newVal);

            GameObject shockObj = Instantiate(sprinkleEffects[2], np.GetComponent<RectTransform>());      //
            RectTransform shockRect = shockObj.GetComponent<RectTransform>();
            Destroy(shockObj, 3f);

            audioManager.Play("MagicTouch");
            armedButNotFired = false;
            magicTouch = false;
            magicIndexX = -1;
            magicIndexY = -1;
        }
        if (armedBombStatus > 0 && armedBombIndexX >= 0)
        {
            if (gravityWaitTimer <= 0)
            {
                Point p = new Point(armedBombIndexX, armedBombIndexY);
                Node n = getNodeAtPoint(p);
                NodePiece np = n.getPiece();
                StartCoroutine(cameraShake.Shake(.08f,.04f));

                if (np.bombStatus == 1) //if a red bomb
                {
                    audioManager.Play("bomb");
                    np.justExploded = true;
                    PlayerPrefs.SetInt("CrazyBomb", PlayerPrefs.GetInt("CrazyBomb", 0) + 1);

                    AdjustTimer();
                    foreach (Node node in board)
                    {
                        if (node.getPiece() != null)
                        {
                            Vector2 newPos = getPositionFromPoint(node.getPiece().index);

                            if (node.getPiece().index.x == np.index.x && node.getPiece().index.y == np.index.y) //Create large explosion where clicked.
                            {
                                CreateExplosion("large", newPos);
                            }

                            if (node.getPiece().value < numberOfPieces - 4 )//|| node.getPiece().value == numberOfPieces)
                            {
                                gravityWaitTimer = 1f;
                                node.getPiece().justExploded = true;
                                nodePiece.gameObject.SetActive(false);
                                dead.Add(node.getPiece());
                                node.SetPiece(null);
                                node.value = 0;


                                GameObject desObj = Instantiate(destroyEffect, gameboard);      //Create Destroy Effect
                                RectTransform desRect = desObj.GetComponent<RectTransform>();
                                desRect.anchoredPosition = newPos;
                                Destroy(desObj, 1f);
                                CreateExplosion("small", newPos);
                            }
                        }

                    }
                    armedBombStatus = 0;
                    armedBombIndexX = -1;
                    armedBombIndexY = -1;
                } //crazy bomb
                if (np.bombStatus == 2) //if a square bomb
                {
                    
                    audioManager.Play("bomb");
                    np.justExploded = true;
                    AdjustTimer();
                    foreach (Node node in board)
                    {
                        if (node.getPiece() != null)
                        {
                            Vector2 newPos = getPositionFromPoint(node.getPiece().index);
                            if (node.getPiece().index.x == np.index.x && node.getPiece().index.y == np.index.y) //Create large explosion where clicked.
                            {
                                CreateExplosion("large", newPos);
                            }
                            if (node.getPiece().index.x >= np.index.x - 1 && node.getPiece().index.x <= np.index.x + 1 && node.getPiece().index.y >= np.index.y - 1 && node.getPiece().index.y <= np.index.y + 1)
                            {
                                gravityWaitTimer = 1f;
                                node.getPiece().justExploded = true;
                                nodePiece.gameObject.SetActive(false);
                                dead.Add(node.getPiece());
                                node.SetPiece(null);
                                node.value = 0;


                                GameObject desObj = Instantiate(destroyEffect, gameboard);      //Create Destroy Effect
                                RectTransform desRect = desObj.GetComponent<RectTransform>();
                                desRect.anchoredPosition = newPos;
                                Destroy(desObj, 1f);
                                CreateExplosion("small", newPos);
                            }
                        }

                    }
                    armedBombStatus = 0;
                    armedBombIndexX = -1;
                    armedBombIndexY = -1;
                } //square bomb
                if (np.bombStatus == 3) //if a row bomb
                {
                    armedButNotFired = false;
                    audioManager.Play("bomb");
                    np.justExploded = true;
                    PlayerPrefs.SetInt("RowBomb", PlayerPrefs.GetInt("RowBomb", 0) + 1);
                    AdjustTimer();
                    foreach (Node node in board)
                    {
                        if (node.getPiece() != null)
                        {
                            Vector2 newPos = getPositionFromPoint(node.getPiece().index);
                            if (node.getPiece().index.x == np.index.x && node.getPiece().index.y == np.index.y) //Create large explosion where clicked.
                            {
                                CreateExplosion("large", newPos);
                            }
                            if (node.getPiece().index.y == np.index.y)
                            {
                                gravityWaitTimer = 1f;
                                node.getPiece().justExploded = true;
                                nodePiece.gameObject.SetActive(false);
                                dead.Add(node.getPiece());
                                node.SetPiece(null);
                                node.value = 0;

                                GameObject desObj = Instantiate(destroyEffect, gameboard);      //Create Destroy Effect
                                RectTransform desRect = desObj.GetComponent<RectTransform>();
                                desRect.anchoredPosition = newPos;
                                Destroy(desObj, 1f);

                                CreateExplosion("small", newPos);
                            }
                        }

                    }
                    if (tutorial)
                    {
                        tutorialPanel.SetActive(true);
                    }
                    armedBombStatus = 0;
                    armedBombIndexX = -1;
                    armedBombIndexY = -1;
                } //row bomb
                if (np.bombStatus == 4) //if a Black Stop Powerup
                {
                    audioManager.Play("blackStop");
                    np.justExploded = true;
                    PlayerPrefs.SetInt("BlackStop", PlayerPrefs.GetInt("BlackStop", 0) + 1);

                    AdjustTimer();
                    foreach (Node node in board)
                    {
                        if (node.getPiece() != null)
                        {
                            Vector2 newPos = getPositionFromPoint(node.getPiece().index);
                            if (node.getPiece().index.x == np.index.x && node.getPiece().index.y == np.index.y) //Create Shockwave Effect where clicked.
                            {
                                GameObject shockObj = Instantiate(shockwave, gameboard);      //
                                RectTransform shockRect = shockObj.GetComponent<RectTransform>();
                                shockRect.anchoredPosition = newPos;
                            }
                        }
                    }
                    blackStopPowerup = 20;

                    armedBombStatus = 0;
                    armedBombIndexX = -1;
                    armedBombIndexY = -1;
                } //black stop powerup
                if (np.bombStatus == 5) //if black bomb
                {
                    audioManager.Play("bomb");
                    np.justExploded = true;
                    PlayerPrefs.SetInt("BlackBomb", PlayerPrefs.GetInt("BlackBomb", 0) + 1);
                    AdjustTimer();
                    foreach (Node node in board)
                    {
                        if (node.getPiece() != null)
                        {
                            Vector2 newPos = getPositionFromPoint(node.getPiece().index);


                            if (node.value == 1) //|| node.value == numberOfPieces)  //*code to confine to small area* &&node.getPiece().index.y >= np.index.y - 2 && node.getPiece().index.y <= np.index.y + 2 && node.getPiece().index.x >= np.index.x - 2 && node.getPiece().index.x <= np.index.x + 2
                            {
                                gravityWaitTimer = 1f;
                                node.getPiece().justExploded = true;
                                nodePiece.gameObject.SetActive(false);
                                dead.Add(node.getPiece());
                                node.SetPiece(null);
                                node.value = 0;


                                GameObject desObj = Instantiate(destroyEffect, gameboard);      //Create Destroy Effect
                                RectTransform desRect = desObj.GetComponent<RectTransform>();
                                desRect.anchoredPosition = newPos;
                                Destroy(desObj, 1f);


                                CreateExplosion("small", newPos);
                            }
                        }

                    }
                    tokens = tokens - tokenPrices[2];
                    audioManager.Play("gold");
                    armedBombStatus = 0;
                    armedBombIndexX = -1;
                    armedBombIndexY = -1;
                } //black bomb
                if (np.bombStatus == 6) //if a dot bomb
                {
                    armedButNotFired = false;
                    audioManager.Play("bomb");
                    np.justExploded = true;
                    PlayerPrefs.SetInt("DotBomb", PlayerPrefs.GetInt("DotBomb", 0) + 1);
                    AdjustTimer();
                    foreach (Node node in board)
                    {
                        if (node.getPiece() != null)
                        {
                            Vector2 newPos = getPositionFromPoint(node.getPiece().index);
                            if (node.getPiece().index.x == np.index.x && node.getPiece().index.y == np.index.y)
                            {
                                gravityWaitTimer = 1f;
                                node.getPiece().justExploded = true;
                                nodePiece.gameObject.SetActive(false);
                                dead.Add(node.getPiece());
                                node.SetPiece(null);
                                node.value = 0;
                                
                                GameObject desObj = Instantiate(destroyEffect, gameboard);      //Create Destroy Effect
                                RectTransform desRect = desObj.GetComponent<RectTransform>();
                                desRect.anchoredPosition = newPos;
                                Destroy(desObj, 1f);

                                CreateExplosion("small", newPos);


                            }
                        }

                    }
                    armedBombStatus = 0;
                    armedBombIndexX = -1;
                    armedBombIndexY = -1;
                } //dot bomb
                if (np.bombStatus == 7) //if a gold bomb
                {

                    audioManager.Play("bomb");
                    np.justExploded = true;
                    PlayerPrefs.SetInt("GoldBomb", PlayerPrefs.GetInt("GoldBomb", 0) + 1);
                    AdjustTimer();
                    foreach (Node node in board)
                    {
                        if (node.getPiece() != null)
                        {
                            Vector2 newPos = getPositionFromPoint(node.getPiece().index);
                            //if (node.getPiece().index.x == np.index.x && node.getPiece().index.y == np.index.y) //Create large explosion where clicked.
                            //{
                            //    CreateExplosion("large", newPos);
                            //}
                            if (node.getPiece().value == numberOfPieces - 2) //destroys gold not diamond
                            {
                                gravityWaitTimer = 1f;
                                node.getPiece().justExploded = true;
                                nodePiece.gameObject.SetActive(false);
                                dead.Add(node.getPiece());
                                node.SetPiece(null);
                                node.value = 0;


                                GameObject desObj = Instantiate(destroyEffect, gameboard);      //Create Destroy Effect
                                RectTransform desRect = desObj.GetComponent<RectTransform>();
                                desRect.anchoredPosition = newPos;
                                Destroy(desObj, 1f);
                                CreateExplosion("small", newPos);
                            }
                        }

                    }
                    tokens = tokens - tokenPrices[8];
                    audioManager.Play("gold");
                    armedBombStatus = 0;
                    armedBombIndexX = -1;
                    armedBombIndexY = -1;
                } //gold bomb
                if (np.bombStatus == 8) //if Slow Time
                {
                    foreach (Node node in board)
                    {
                        if (node.getPiece() != null)
                        {
                            Vector2 newPos = getPositionFromPoint(node.getPiece().index);
                            if (node.getPiece().index.x == np.index.x && node.getPiece().index.y == np.index.y)
                            {
                                GameObject effectObj = Instantiate(ripple2Effect, gameboard);      //ripple  effect
                                RectTransform effectRect = effectObj.GetComponent<RectTransform>();
                                effectRect.anchoredPosition = node.getPiece().pos;


                            }
                        }
                    }

                    audioManager.Play("slow");
                    np.justExploded = true;
                    PlayerPrefs.SetInt("SlowTime", PlayerPrefs.GetInt("SlowTime", 0) + 1);

                    //CountdownTimer.time = CountdownTimer.time + 5f;
                    timerEdits.Add(new TimerEdit(2f, 20));
                    AdjustTimer();
                    armedBombStatus = 0;
                    armedBombIndexX = -1;
                    armedBombIndexY = -1;
                } //slow time powerup
                if (np.bombStatus == 9) //if Stop Time
                {
                    foreach (Node node in board)
                    {
                        if (node.getPiece() != null)
                        {
                            Vector2 newPos = getPositionFromPoint(node.getPiece().index);
                            if (node.getPiece().index.x == np.index.x && node.getPiece().index.y == np.index.y)
                            {
                                GameObject effectObj = Instantiate(ripple2LargeEffect, gameboard);      //ripple  effect
                                RectTransform effectRect = effectObj.GetComponent<RectTransform>();
                                effectRect.anchoredPosition = node.getPiece().pos;


                            }
                        }
                    }
                    StartCoroutine(BreakTimer(20));
                    audioManager.Play("timeStop");
                    np.justExploded = true;
                    PlayerPrefs.SetInt("StopTime", PlayerPrefs.GetInt("StopTime", 0) + 1);

                    armedBombStatus = 0;
                    armedBombIndexX = -1;
                    armedBombIndexY = -1;
                } //stop time powerup

                Invoke("ApplyGravityToBoard", .2f);
                if (activeBomb != null)
                {
                    activeBomb.gameObject.SetActive(false);
                }
                bombUsed = true;
            }
            else //if didn't fire due to pieces in motion, reset to unfired.
            {
                armedBombIndexX = -1;
                armedBombIndexY = -1;
            }
        }
        List<NodePiece> finishedUpdating = new List<NodePiece>();
        for (int i = 0; i < update.Count; i++)
        {
            if (update[i] != null)
            {
                NodePiece piece = update[i];
                if (!piece.UpdatePiece())
                {
                    finishedUpdating.Add(piece);
                }
            }
        }
        for (int i = 0; i < finishedUpdating.Count; i++)
        {
            NodePiece piece = finishedUpdating[i];
            FlippedPieces flip = getFlipped(piece);
            NodePiece flippedPiece = null;
            int flippedPieceVal = -1;
            int x = (int)piece.index.x;
            fills[x] = Mathf.Clamp(fills[x] - 1, 0, width);

            List<Point> connected = isConnected(piece.index, true);
            bool wasFlipped = (flip != null);
            bool legalPlayerMove = false;

            if (wasFlipped) //if we flipped to make this update
            {
                TurnTracker.triggeredByBomb = false;
                legalPlayerMove = true;
                TurnTracker.turnInProgress = true;
                matchFourRecorded = false;
                flippedPiece = flip.getOtherPiece(piece);
                flippedPieceVal = flippedPiece.value;
                AddPoints(ref connected, isConnected(flippedPiece.index, true)); //checking if flipped piece also forms a match and adding it.
            }
            if (bombUsed)
            {
                legalPlayerMove = true;
                bombUsed = false;
                TurnTracker.turnInProgress = true;

                matchFourRecorded = false;
                TurnTracker.triggeredByBomb = true;
            }

            if (connected.Count == 0) //If we didn't make a match
            {
                if (wasFlipped) //If we flipped
                {
                    FlipPieces(piece.index, flippedPiece.index, false); //Flip back
                    if (TurnTracker.turnInProgress)
                    {
                        legalPlayerMove = false;
                    }
                    TurnTracker.turnInProgress = false;
                }
            }

            else //If we made a match
            {
                audioManager.Play("match");
                List<int> matchLengthTracker = new List<int>(); //list position equal to value, list item counts how many how each value in match
                for (int q = 0; q <= numberOfPieces; q++)
                {
                    matchLengthTracker.Insert(q, 0);
                }
                Point p = new Point(piece.index.x, piece.index.y);
                Point pFlipped = new Point(-1, 0);
                Vector2 flippedNewPos = getPositionFromPoint(piece.index);
                if (flippedPiece != null)
                {
                    pFlipped = new Point(flippedPiece.index.x, flippedPiece.index.y);
                    flippedNewPos = getPositionFromPoint(flippedPiece.index);
                }
                Vector2 newPos = getPositionFromPoint(piece.index);

                bool mainPieceIsMatch = false;
                bool flippedPieceIsMatch = false;
                bool extraTurnAnimation = false;
                int newVal;
                int flippedNewVal = 0;
                if (piece.value == 5 && redMatchCount < 3) //start spawning reds after certain number of red matches made
                {
                    redMatchCount += 1;
                    if (redMatchCount == 2)
                    {
                        numberOfTypesToSpawn = 5;
                    }
                }
                if (piece.value < numberOfPieces - 2) //check for highest 3 value pieces. these can't be upgraded.
                {
                    newVal = piece.value + 1;
                }
                else
                {
                    newVal = piece.value;
                }
                if (flippedPiece != null)
                {
                    if (flippedPiece.value < numberOfPieces - 2) //check for highest 3 value pieces. these can't be upgraded.
                    {
                        flippedNewVal = flippedPiece.value + 1;
                    }
                    else
                    {
                        flippedNewVal = flippedPiece.value;
                    }
                }
                foreach (Point pnt in connected) //Remove the node pieces connected
                {
                    
                    gravityWaitTimer = 1f;
                    tutorialWaitTimer = .35f;
                    Node node = getNodeAtPoint(pnt);
                    NodePiece nodePiece = node.getPiece();
                    if (pnt.x == p.x && pnt.y == p.y || pnt.x == pFlipped.x && pnt.y == pFlipped.y) //main piece does not have kill animation
                    {

                    }
                    else //do kill animation.
                    {
                        if (!piece.justExploded)
                        {
                            KillPiece(pnt);
                        }
                    }
                    if (nodePiece != null)
                    {


                        int count = matchLengthTracker[nodePiece.value];
                        matchLengthTracker.Insert(nodePiece.value, count + 1);
                        if (nodePiece.value == piece.value && !mainPieceIsMatch)
                        {
                            mainPieceIsMatch = true;
                        }
                        if (nodePiece.value == flippedPieceVal && !flippedPieceIsMatch)
                        {
                            flippedPieceIsMatch = true;
                        }

                        nodePiece.gameObject.SetActive(false);
                        dead.Add(nodePiece);
                    }
                    node.SetPiece(null);
                }
                int turnChange = 0;
                if (legalPlayerMove && !TurnTracker.triggeredByBomb)
                {
                    turnChange -= 1;
                    MoveCounter.movesMade += 1;

                    AdjustTimer();
                    if (TurnTracker.turnsRemaining < 5)
                    {

                        audioManager.Play("endWarning");
                    }

                }
                int tutorialTracker = 0;
                foreach (int count in matchLengthTracker)
                {
                    if (count > 4 && TurnTracker.turnInProgress && !gameEnd)
                    {
                        if (TurnTracker.triggeredByBomb)
                        {
                            turnChange += 1;
                        }
                        else
                        {
                            if (matchFourRecorded)
                            {
                                turnChange += 1;
                            }
                            else
                            {
                                turnChange += 2;
                            }
                        }
                        TurnTracker.turnInProgress = false;
                        audioManager.Play("five");
                        if (tutorial && tutorialPhase == 7)
                        {
                            tutorialMatchCount++;
                            tutorialTracker++;
                            if (tutorialMatchCount == 2)
                            {
                                tutorialPanel.SetActive(true);
                                timerBroken = 0;
                                FindObjectOfType<CountdownTimer>().FixTimer();
                                timerEdits.Add(new TimerEdit(-5f, 3));
                                AdjustTimer();
                                timerBreak.SetActive(false);
                            }
                        }
                        GameObject arrowObj = Instantiate(upArrow, turnBox);      //Create Up Arrow
                        RectTransform arrowRect = arrowObj.GetComponent<RectTransform>();
                        arrowRect.anchoredPosition = new Vector2(10, -200);
                        Destroy(arrowObj, 3f);

                        Vector2 extraTurnPos = newPos;
                        if(piece.index.x == 0) //if the animation is going to play too close to edge move it in 1.
                        {
                            Point newPosPoint = new Point(1, piece.index.y);
                            extraTurnPos = getPositionFromPoint(newPosPoint);
                        }
                        if (piece.index.x == width - 1)
                        {
                            Point newPosPoint = new Point(width - 2, piece.index.y);
                            extraTurnPos = getPositionFromPoint(newPosPoint);
                        }
                        if(extendObj != null) //get rid of match 4 text if match 5 happens on same turn. otherwise they can overlap.
                        {
                            Destroy(extendObj);
                        }
                        GameObject extraObj = Instantiate(extraTurn, gameboard);      //Create ExtraTurn Effect
                        RectTransform extraRect = extraObj.GetComponent<RectTransform>();
                        extraRect.anchoredPosition = extraTurnPos;
                        Destroy(extraObj, 3f);
                        extraTurnAnimation = true;
                    }
                    else if (count > 3 && TurnTracker.turnInProgress && !gameEnd && !matchFourRecorded)
                    {
                        if (!TurnTracker.triggeredByBomb)
                        {
                            turnChange += 1;
                        }
                        if (!matchFourRecorded)
                        {
                            Vector2 turnExtendPos = newPos;
                            if (piece.index.x == 0) //if the animation is going to play too close to edge move it in 1.
                            {
                                Point newPosPoint = new Point(1, piece.index.y);
                                turnExtendPos = getPositionFromPoint(newPosPoint);
                            }
                            if (piece.index.x == width - 1)
                            {
                                Point newPosPoint = new Point(width - 2, piece.index.y);
                                turnExtendPos = getPositionFromPoint(newPosPoint);
                            }
                            extendObj = Instantiate(turnExtended, gameboard);      //Create ExtraTurn Effect
                            RectTransform extendRect = extendObj.GetComponent<RectTransform>();
                            extendRect.anchoredPosition = turnExtendPos;
                            Destroy(extendObj, 3f);
                        }
                        matchFourRecorded = true;                     //ensures 1 match 4 per move but keeps looking for match5
                        audioManager.Play("four");
                        if (tutorial && tutorialPhase == 7)
                        {
                            tutorialMatchCount++;
                            tutorialTracker++;
                            if(tutorialMatchCount == 2)
                            {
                                tutorialPanel.SetActive(true);

                                timerEdits.Add(new TimerEdit(-7f, 3));
                                timerBroken = 0;
                                FindObjectOfType<CountdownTimer>().FixTimer();
                                AdjustTimer();
                                timerBreak.SetActive(false);
                            }
                        }
                    }

                }
                if(tutorial && tutorialPhase == 7 && tutorialTracker == 0)
                {
                    tutorialError = true;
                    tutorialMatchCount = 0;
                    audioManager.Play("CantAfford");
                }

                if (turnChange < 0 && TurnTracker.turnInProgress)
                {

                    GameObject arrowObj = Instantiate(downArrow, turnBox);      //Create Down Arrow
                    RectTransform arrowRect = arrowObj.GetComponent<RectTransform>();
                    arrowRect.anchoredPosition = new Vector2(0, 64);
                    Destroy(arrowObj, 3f);
                }
                TurnTracker.turnsRemaining += turnChange;

                if (TurnTracker.turnsRemaining == 3 && legalPlayerMove && !extraTurnAnimation && !matchFourRecorded)
                {
                    GameObject threeObj = Instantiate(threeLeft, gameboard);      //3turnwarning
                    RectTransform threeRect = threeObj.GetComponent<RectTransform>();
                    threeRect.anchoredPosition = newPos;
                    Destroy(threeObj, 3f);
                }
                if (TurnTracker.turnsRemaining == 2 && legalPlayerMove && !extraTurnAnimation && !matchFourRecorded)
                {
                    GameObject twoObj = Instantiate(twoLeft, gameboard);      //2turnwarning
                    RectTransform twoRect = twoObj.GetComponent<RectTransform>();
                    twoRect.anchoredPosition = newPos;
                    Destroy(twoObj, 3f);
                }
                if (TurnTracker.turnsRemaining == 1 && legalPlayerMove && !extraTurnAnimation && !matchFourRecorded)
                {
                    GameObject oneObj = Instantiate(oneLeft, gameboard);      //1turnwarning
                    RectTransform oneRect = oneObj.GetComponent<RectTransform>();
                    oneRect.anchoredPosition = newPos;
                    Destroy(oneObj, 3f);
                }



                //generate new leveled up piece
                if (mainPieceIsMatch)
                {
                    GenerateLeveledUpPiece(p, newVal, newPos);

                    GameObject desObj = Instantiate(destroyEffect, gameboard);      //Create Destroy Effect
                    RectTransform desRect = desObj.GetComponent<RectTransform>();
                    desRect.anchoredPosition = newPos;

                    GameObject effectObj = Instantiate(sparkEffect, gameboard);      //Create Destroy Effect
                    RectTransform effect = effectObj.GetComponent<RectTransform>();
                    effect.anchoredPosition = newPos;
                    //Instantiate(projectile, desRect.transform.position, transform.rotation);
                    //GameObject obj = Instantiate(projectile, gameboard);
                    //Projectile proj = obj.GetComponent<Projectile>();
                    //RectTransform rect = obj.GetComponent<RectTransform>();
                    //rect.anchoredPosition = newPos;
                    //proj.Initialize(1000, pieces[1]);

                }
                if (flippedPieceIsMatch)
                {

                    
                    GenerateLeveledUpPiece(pFlipped, flippedNewVal, flippedNewPos);

                    GameObject desObj = Instantiate(destroyEffect, gameboard);      //Create Destroy Effect
                    RectTransform desRect = desObj.GetComponent<RectTransform>();
                    desRect.anchoredPosition = flippedNewPos;

                    GameObject effectObj = Instantiate(sparkEffect, gameboard);      //spark effect
                    RectTransform effectRect = effectObj.GetComponent<RectTransform>();
                    effectRect.anchoredPosition = newPos;

                    //GameObject bullet = Instantiate(projectile);      //Create Destroy Effect
                    //RectTransform bulletRect = bullet.GetComponent<RectTransform>();
                    //bulletRect.anchoredPosition = newPos;
                    //Debug.Log(newPos);
                }

                Invoke("ApplyGravityToBoard", .2f);
            }


            if (piece.justExploded)
            {
                piece.justExploded = false;
            }
            flipped.Remove(flip);
            update.Remove(piece);
        }
        if (tutorialPhase == 4)
        {
            if (tutorialWaitTimer > 0)
            {
                tutorialWaitTimer -= 1 * Time.deltaTime;
                tutorialStop = true;
            }
            else
            {
                tutorialStop = false;
            }
        }
        if (gravityWaitTimer > 0)
        {
            gravityWaitTimer -= 1 * Time.deltaTime;
            saveButton.interactable = false;
        }
        else
        {
            saveButton.interactable = true;
        }
        if (scoringWaitTimer > 0)
        {
            scoringWaitTimer -= 1 * Time.deltaTime;
        }
        if (!gameEnd)
        {
            CheckForEndGame();
        }
        if (CountdownTimer.currentTime == 0f && !gameEnd)
        {
            int pieceCount;
            if (zenMode)
            {
                pieceCount = 49;
            }
            else
            {
                pieceCount = 117;
            }
            while (!TurnRandomPieceBlack() && blackPieceCount < pieceCount)
            {
                if (gravityWaitTimer <= 0)
                {
                     TurnRandomPieceBlack();
                   
                }
            }

            CountdownTimer.currentTime = CountdownTimer.time;
        }
        if (gameEnd && !scored && gravityWaitTimer <= 0)
        {
            /*
            currentLevel = PlayerPrefs.GetInt("CurrentLevel", 0);
            currentPoints = PlayerPrefs.GetInt("CurrentPoints", 0);
            if (currentLevel < maxLevel)
            {
                levelText.text = "LEVEL " + currentLevel.ToString("n0");
                progressBar.maxValue = levelPoints[currentLevel];
                progressBar.value = currentPoints;
            }
            else
            {
                levelText.text = "MAX LEVEL";
                progressBar.value = progressBar.maxValue;
            }

            */
            redBarCount = 0;
            redKeyBar.maxValue = 1;
            redKeyBar.value = 0;
            redKeysText.text = "RED KEYS: " + PlayerPrefs.GetInt("RedKeys", 0);

            blueBarCount = 0;
            blueKeyBar.maxValue = 1;
            blueKeyBar.value = 0;
            blueKeysText.text = "BLUE KEYS: " + PlayerPrefs.GetInt("BlueKeys", 0);

            StartCoroutine(Cashout());
            scored = true;
        }
        if (gameEnd && cashoutFinished && !finishedScoring)  //Set all the end game stats.
        {
            finishedScoring = true;
            PlayerPrefs.SetInt("GameSaved", 0);
            audioManager.resume = false;
            PlayerPrefs.SetInt("TotalMoves", PlayerPrefs.GetInt("TotalMoves", 0) + MoveCounter.movesMade);
            PlayerPrefs.SetInt("TotalPoints", PlayerPrefs.GetInt("TotalPoints", 0) + score);
            PlayerPrefs.SetInt("GamesPlayed", PlayerPrefs.GetInt("GamesPlayed", 0) + 1);
            PlayerPrefs.SetInt("TotalTokens", PlayerPrefs.GetInt("TotalTokens", 0) + totalTokens);
            PlayerPrefs.SetInt("LastMoves", MoveCounter.movesMade);
            PlayerPrefs.SetInt("LastPoints", score);

            if (chaosMode)
            {
                audioManager.PlayThemeMusic();
                chaosGoldText.text = chaosGold + " GOLD!";
                chaosGoldPanel.SetActive(true);
                PlayerPrefs.SetString("LastMode", "Chaos");
                PlayerPrefs.SetInt("ChaosGamesPlayed", PlayerPrefs.GetInt("ChaosGamesPlayed", 0) + 1);
                if (MoveCounter.movesMade <= PlayerPrefs.GetInt("ChaosMovesHighScore"))
                {
                    movesHighScorePanel.SetActive(false);
                }
                if (MoveCounter.movesMade > PlayerPrefs.GetInt("ChaosMovesHighScore3", 0))
                {
                    if (MoveCounter.movesMade > PlayerPrefs.GetInt("ChaosMovesHighScore2", 0))
                    {
                        if (MoveCounter.movesMade > PlayerPrefs.GetInt("ChaosMovesHighScore", 0))
                        {
                            PlayerPrefs.SetInt("ChaosMovesHighScore3", PlayerPrefs.GetInt("ChaosMovesHighScore2", 0));
                            PlayerPrefs.SetInt("ChaosMovesHighScore2", PlayerPrefs.GetInt("ChaosMovesHighScore", 0));
                            PlayerPrefs.SetInt("ChaosMovesHighScore", MoveCounter.movesMade);
                            movesHighScorePanel.SetActive(true);
                        }
                        else
                        {

                            PlayerPrefs.SetInt("ChaosMovesHighScore3", PlayerPrefs.GetInt("ChaosMovesHighScore2", 0));
                            PlayerPrefs.SetInt("ChaosMovesHighScore2", MoveCounter.movesMade);
                        }
                    }
                    else
                    {
                        PlayerPrefs.SetInt("ChaosMovesHighScore3", MoveCounter.movesMade);
                    }
                }


                if (score <= PlayerPrefs.GetInt("ChaosHighScore"))
                {
                    pointsHighScorePanel.SetActive(false);
                }
                if (score > PlayerPrefs.GetInt("ChaosHighScore3", 0))
                {
                    if (score > PlayerPrefs.GetInt("ChaosHighScore2", 0))
                    {
                        if (score > PlayerPrefs.GetInt("ChaosHighScore", 0))
                        {
                            PlayerPrefs.SetInt("ChaosHighScore3", PlayerPrefs.GetInt("ChaosHighScore2", 0));
                            PlayerPrefs.SetInt("ChaosHighScore2", PlayerPrefs.GetInt("ChaosHighScore", 0));
                            PlayerPrefs.SetInt("ChaosHighScore", score);
                            pointsHighScorePanel.SetActive(true);
                        }
                        else
                        {
                            PlayerPrefs.SetInt("ChaosHighScore3", PlayerPrefs.GetInt("ChaosHighScore2", 0));
                            PlayerPrefs.SetInt("ChaosHighScore2", score);
                        }
                    }
                    else
                    {
                        PlayerPrefs.SetInt("ChaosHighScore3", score);
                    }
                }
                if (chaosGold > PlayerPrefs.GetInt("ChaosGold", 0))
                {
                    PlayerPrefs.SetInt("ChaosGold", chaosGold);
                }
                for (int i = 0; i < chaosUnlockGoals.Length; i++) //unlock anything goal achieved. show unlock panels.
                {
                    if (i < 4 && MoveCounter.movesMade >= chaosUnlockGoals[i] && PlayerPrefs.GetInt("Piece" + i + "Chaos") != 1)
                    {
                        PlayerPrefs.SetInt("Piece" + i + "Chaos", 1);
                        unlockGrid.Find("Chaos" + i.ToString() + "(Clone)").gameObject.SetActive(true);

                    }
                    if (i >= 4 && chaosGold >= chaosUnlockGoals[i] && PlayerPrefs.GetInt("Piece" + i + "Chaos") != 1)
                    {
                        PlayerPrefs.SetInt("Piece" + i + "Chaos", 1);
                        unlockGrid.Find("Chaos" + i.ToString() + "(Clone)").gameObject.SetActive(true);

                    }
                }

            }

            else if (zenMode)
            {
                chaosGoldPanel.SetActive(false);
                PlayerPrefs.SetInt("ZenGamesPlayed", PlayerPrefs.GetInt("ZenGamesPlayed", 0) + 1);
                PlayerPrefs.SetString("LastMode", "Zen");
                if (MoveCounter.movesMade <= PlayerPrefs.GetInt("ZenMovesHighScore"))
                {
                    movesHighScorePanel.SetActive(false);
                }
                if (MoveCounter.movesMade > PlayerPrefs.GetInt("ZenMovesHighScore3", 0))
                {
                    if (MoveCounter.movesMade > PlayerPrefs.GetInt("ZenMovesHighScore2", 0))
                    {
                        if (MoveCounter.movesMade > PlayerPrefs.GetInt("ZenMovesHighScore", 0))
                        {
                            PlayerPrefs.SetInt("ZenMovesHighScore3", PlayerPrefs.GetInt("ZenMovesHighScore2", 0));
                            PlayerPrefs.SetInt("ZenMovesHighScore2", PlayerPrefs.GetInt("ZenMovesHighScore", 0));
                            PlayerPrefs.SetInt("ZenMovesHighScore", MoveCounter.movesMade);
                            movesHighScorePanel.SetActive(true);
                        }
                        else
                        {

                            PlayerPrefs.SetInt("ZenMovesHighScore3", PlayerPrefs.GetInt("ZenMovesHighScore2", 0));
                            PlayerPrefs.SetInt("ZenMovesHighScore2", MoveCounter.movesMade);
                        }
                    }
                    else
                    {
                        PlayerPrefs.SetInt("ZenMovesHighScore3", MoveCounter.movesMade);
                    }
                }


                if (score <= PlayerPrefs.GetInt("ZenHighScore"))
                {
                    pointsHighScorePanel.SetActive(false);
                }
                if (score > PlayerPrefs.GetInt("ZenHighScore3", 0))
                {
                    if (score > PlayerPrefs.GetInt("ZenHighScore2", 0))
                    {
                        if (score > PlayerPrefs.GetInt("ZenHighScore", 0))
                        {
                            PlayerPrefs.SetInt("ZenHighScore3", PlayerPrefs.GetInt("ZenHighScore2", 0));
                            PlayerPrefs.SetInt("ZenHighScore2", PlayerPrefs.GetInt("ZenHighScore", 0));
                            PlayerPrefs.SetInt("ZenHighScore", score);
                            pointsHighScorePanel.SetActive(true);
                        }
                        else
                        {
                            PlayerPrefs.SetInt("ZenHighScore3", PlayerPrefs.GetInt("ZenHighScore2", 0));
                            PlayerPrefs.SetInt("ZenHighScore2", score);
                        }
                    }
                    else
                    {
                        PlayerPrefs.SetInt("ZenHighScore3", score);
                    }
                }
                //for (int i = 0; i < ZenUnlockGoals.Length; i++) //unlock anything goal achieved. show unlock panels.
                //{
                //    if (i < 4 && MoveCounter.movesMade >= ZenUnlockGoals[i] && PlayerPrefs.GetInt("Piece" + i + "Zen") != 1)
                //    {
                //        PlayerPrefs.SetInt("Piece" + i + "Zen", 1);
                //        unlockGrid.Find("Zen" + i.ToString() + "(Clone)").gameObject.SetActive(true);

                //    }
                //    if (i >= 4 && ZenGold >= ZenUnlockGoals[i] && PlayerPrefs.GetInt("Piece" + i + "Zen") != 1)
                //    {
                //        PlayerPrefs.SetInt("Piece" + i + "Zen", 1);
                //        unlockGrid.Find("Zen" + i.ToString() + "(Clone)").gameObject.SetActive(true);

                //    }
                //}

            }



            else
            {
                chaosGoldPanel.SetActive(false);
                PlayerPrefs.SetString("LastMode", "Standard");
                if (MoveCounter.movesMade <= PlayerPrefs.GetInt("MovesHighScore"))
                {
                    movesHighScorePanel.SetActive(false);
                }
                if (MoveCounter.movesMade > PlayerPrefs.GetInt("MovesHighScore3", 0))
                {
                    if (MoveCounter.movesMade > PlayerPrefs.GetInt("MovesHighScore2", 0))
                    {
                        if (MoveCounter.movesMade > PlayerPrefs.GetInt("MovesHighScore", 0))
                        {
                            PlayerPrefs.SetInt("MovesHighScore3", PlayerPrefs.GetInt("MovesHighScore2", 0));
                            PlayerPrefs.SetInt("MovesHighScore2", PlayerPrefs.GetInt("MovesHighScore", 0));
                            PlayerPrefs.SetInt("MovesHighScore", MoveCounter.movesMade);
                            movesHighScorePanel.SetActive(true);
                        }
                        else
                        {

                            PlayerPrefs.SetInt("MovesHighScore3", PlayerPrefs.GetInt("MovesHighScore2", 0));
                            PlayerPrefs.SetInt("MovesHighScore2", MoveCounter.movesMade);
                        }
                    }
                    else
                    {
                        PlayerPrefs.SetInt("MovesHighScore3", MoveCounter.movesMade);
                    }
                }


                if (score <= PlayerPrefs.GetInt("HighScore"))
                {
                    pointsHighScorePanel.SetActive(false);
                }
                if (score > PlayerPrefs.GetInt("HighScore3", 0))
                {
                    if (score > PlayerPrefs.GetInt("HighScore2", 0))
                    {
                        if (score > PlayerPrefs.GetInt("HighScore", 0))
                        {
                            PlayerPrefs.SetInt("HighScore3", PlayerPrefs.GetInt("HighScore2", 0));
                            PlayerPrefs.SetInt("HighScore2", PlayerPrefs.GetInt("HighScore", 0));
                            PlayerPrefs.SetInt("HighScore", score);
                            pointsHighScorePanel.SetActive(true);
                        }
                        else
                        {
                            PlayerPrefs.SetInt("HighScore3", PlayerPrefs.GetInt("HighScore2", 0));
                            PlayerPrefs.SetInt("HighScore2", score);
                        }
                    }
                    else
                    {
                        PlayerPrefs.SetInt("HighScore3", score);
                    }
                }

                for (int i = 0; i < unlockGoals.Length; i++) //unlock anything goal achieved. show unlock panels.
                {
                    if (MoveCounter.movesMade >= unlockGoals[i] && PlayerPrefs.GetInt("Piece" + i + "Unlocked") != 1)
                    {
                        PlayerPrefs.SetInt("Piece" + i + "Unlocked", 1);
                        unlockGrid.Find(i.ToString()+"(Clone)").gameObject.SetActive(true);
                        //GameObject s = Instantiate(unlockItem, unlockGrid);
                        //UnlockItem item = unlockItem.GetComponent<UnlockItem>();

                        //item.Initialize(unlockedPieces[i], unlockGoals[i] + "+ MOVES", unlockDescriptions[i]);
                        //item.gameObject.SetActive(true);
                        //item.gameObject.name = i.ToString();
                    }
                }
            }
            FindObjectOfType<GameManager>().EnableEndgameButtons();

        }

        if(tutorial && tutorialPhase == 7 && tutorialError && gravityWaitTimer <= 0)
        {
            TutorialPhase5SetUp();
            tutorialError = false;
        }

        if (cashoutStarted)
        {
            //Debug.Log("REDBAR: " + redKeyBar.value + " BLUBAR: " + blueKeyBar.value);
            if (redKeyBar.value == 1) //Update Red Key progress bar.
            {

                redBarCount += 1;
                redKeyBar.value = 0;
                audioManager.Play("Key");
                redKeysText.text = "RED KEYS: " + (PlayerPrefs.GetInt("RedKeys", 0) + redBarCount);

                GameObject redKeyObj = Instantiate(redKeyEffect, gameboard);      //Create Red Key Effect
                RectTransform redKeyRect = redKeyObj.GetComponent<RectTransform>();
                redKeyRect.anchoredPosition = new Vector2(200, -100);
                Destroy(redKeyObj, 3f);
            }

            if (redBarCount < redKeys)
            {
                //redKeyBar.value = Mathf.Lerp(redKeyBar.value, 1, Time.deltaTime * 20f);
                float currentValue = redKeyBar.value;
                float targetValue = 1f;
                float lerpSpeed = 40f;
                float t = 0f;
                float tolerance = 0.001f;
                t += Time.deltaTime * lerpSpeed;
                redKeyBar.value = Mathf.Lerp(currentValue, targetValue, t);
                if (Mathf.Abs(redKeyBar.value - targetValue) < tolerance)
                {
                    redKeyBar.value = 1;
                }
            }
            else
            {
                redKeyBar.value = Mathf.Lerp(redKeyBar.value, (float)(score % redKeyValue) / redKeyValue, Time.deltaTime * 3f);
            }

            if (blueKeyBar.value == 1)  //Update Blue Key progress bar.
            {

                blueBarCount += 1;
                blueKeyBar.value = 0;
                audioManager.Play("Key");
                blueKeysText.text = "BLUE KEYS: " + (PlayerPrefs.GetInt("BlueKeys", 0) + blueBarCount);

                GameObject blueKeyObj = Instantiate(blueKeyEffect, gameboard);      //Create Blue Key Effect
                RectTransform blueKeyRect = blueKeyObj.GetComponent<RectTransform>();
                blueKeyRect.anchoredPosition = new Vector2(200, -120);
                Destroy(blueKeyObj, 3f);
            }

            if (blueBarCount < blueKeys)
            {
                float tolerance = 0.001f;
                blueKeyBar.value = Mathf.Lerp(blueKeyBar.value, 1, Time.deltaTime * 50f);
                if (Mathf.Abs(blueKeyBar.value - 1) < tolerance)
                {
                    blueKeyBar.value = 1;
                }
            }
            else
            {
                blueKeyBar.value = Mathf.Lerp(blueKeyBar.value, (float)(movesEndCount % blueKeyValue) / blueKeyValue, Time.deltaTime * 3f);
            }
        }
    }

    public void StartPlaying()
    {
        gameRunning = true;
        CountdownTimer.chaosMode = false;
        cashoutStarted = false;
        if (chaosMode)
        {
            audioManager.PlayChaosMusic();
            CountdownTimer.time = 2.8f;
            CountdownTimer.chaosMode = true;
        }
        KickTimerUpAss();
        if (zenMode)
        {
            FindObjectOfType<CountdownTimer>().BreakTimer();
        }
        //Transform bombs = inventory.transform.GetComponentInChildren<Transform>(true);
        //foreach(Transform t in inventory)
        //{
        //    Bomb b = t.GetComponent<Bomb>();
        //    if (b.id == 2) //if chaos mode purchased.
        //    {
        //        b.gameObject.SetActive(false);
        //        chaosMode = true;

        //        KickTimerUpAss();
        //        break;

        //    }
        //}

        for (int i = 0; i < unlockGoals.Length; i++) //Instantiating Unlock Items at start due to random issues when doing at the end of game THAT DON'T MAKE SENSE
        {
            GameObject s = Instantiate(unlockItem, unlockGrid);
            s.SetActive(false);
            UnlockItem item = unlockItem.GetComponent<UnlockItem>();
            item.Initialize(unlockedPieces[i], unlockGoals[i] + "+ MOVES", unlockDescriptions[i]);
            item.gameObject.name = i.ToString();
            
        }
        for(int i = 0; i < maxLevel; i++)
        {
            GameObject s = Instantiate(unlockItem, unlockGrid);
            UnlockItem item = unlockItem.GetComponent<UnlockItem>();
            item.Initialize(pointsUnlockItems[i], "LEVEL " + i, pointsUnlocks[i]);
            item.gameObject.name = "LEVEL " + i;
            s.SetActive(false);
        }
        for (int i = 0; i < chaosUnlockGoals.Length; i++) 
        {
            if (i < 4)
            {
                GameObject s = Instantiate(unlockItem, unlockGrid);
                s.SetActive(false);
                UnlockItem item = unlockItem.GetComponent<UnlockItem>();
                item.Initialize(chaosUnlockedPieces[i], chaosUnlockGoals[i] + "+ MOVES", chaosUnlockDescriptions[i]);
                item.gameObject.name = "Chaos" + i.ToString();
            }
            if (i >= 4)
            {
                GameObject s = Instantiate(unlockItem, unlockGrid);
                s.SetActive(false);
                UnlockItem item = unlockItem.GetComponent<UnlockItem>();
                item.Initialize(chaosUnlockedPieces[i], "FORGED " + chaosUnlockGoals[i] + " GOLD", chaosUnlockDescriptions[i]);
                item.gameObject.name = "Chaos"+i.ToString();
            }
        }
    }

    public void SaveGame()
    {
        if (tutorial)
        {
            return;
        }
        string statsString;
        statsString = "" + TurnTracker.turnsRemaining.ToString("00000") + MoveCounter.movesMade.ToString("00000") + tokens.ToString("00000");
        PlayerPrefs.SetString("StatsSave", statsString);

        string gameString = "";
        string infectionString = "";
        for (int y = 0; y<height; y++)
        {
            for(int x=0;x<width; x++)
            {
                Point p = new Point(x, y);
                Node n = getNodeAtPoint(p);
                NodePiece piece = n.getPiece();
                gameString += n.value;
                if(piece != null &&piece.infectionPhase > 0)
                {
                    infectionString += piece.index.x.ToString("00") + piece.index.y.ToString("00") + piece.infectionPhase;
                }
            }
        }
        PlayerPrefs.SetString("infectionSave", infectionString);
        PlayerPrefs.SetString("GameSave", gameString);
        string inventoryString = "";
        string tempString = "";
        foreach (Transform child in inventory)
        {
            PowerUpItem p = child.GetComponent<PowerUpItem>();
            if (!p.temp)
            {
                inventoryString += p.id.ToString("00");
            }
            else
            {
                tempString = p.id.ToString("00");
            }
        }
        PlayerPrefs.SetString("TempInvSave", tempString);
        PlayerPrefs.SetString("InventorySave", inventoryString);
        PlayerPrefs.SetInt("GameSaved", 1);
        PlayerPrefs.SetInt("timerEdits", timerEdits.Count);
        for (int i = 0; i < timerEdits.Count; i++)
        {
            PlayerPrefs.SetInt("timerEditTurns" + i, timerEdits[i].turns);
            PlayerPrefs.SetFloat("timerEditTime" + i, timerEdits[i].time);
        }
        PlayerPrefs.SetInt("blackStop", blackStopPowerup);
        PlayerPrefs.SetInt("crateMagic", crateMagicPowerup);

        if (zenMode)
        {
            PlayerPrefs.SetInt("gameMode", 3);
        }
        else if (chaosMode)
        {
            PlayerPrefs.SetInt("gameMode", 2);
        }
        else
        {
            PlayerPrefs.SetInt("gameMode", 1);
        }

        if (timerBroken > 0)
        {
            PlayerPrefs.SetInt("timerBroken", timerBroken);
        }
        if (skinChangeMoves > 0)
        {
            skinChangeMoves = -1;
            for (int i = 0; i < pieces.Length; i++)
            {
                pieces[i] = piecesHold[i];
            }
            UpdateSprites();
        }
    }


    public void AdjustTimer()
    {


        float timerAmount;
        if (chaosMode)
        {
            timerAmount = 3.5f;
        }
        else
        {
            timerAmount = 8.5f;
        }
        List<TimerEdit> toRemove = new List<TimerEdit>();
        foreach (TimerEdit t in timerEdits) //enacts any time edits. removes them if finished.
        {
            float adjustAmount = t.EditTime();
            if (adjustAmount == 0f)
            {
                toRemove.Add(t);
            }
            else
            {
                timerAmount += adjustAmount;
            }
        }
        foreach(TimerEdit t in toRemove)
        {
            timerEdits.Remove(t);
        }
        if(timerAmount < 1.5f)
        {
            timerAmount = 1.5f;
        }
        CountdownTimer.time = timerAmount;
        CountdownTimer.currentTime = CountdownTimer.time;
        if (skinChange)
        {
            for (int i = 0; i<pieces.Length; i++)
            {
                piecesHold[i] = pieces[i];

                //if(rand == 2)
                //{
                //    pieces[i] = upgradedPieces[i];
                //}
                //if (rand == 1)
                //{
                //    pieces[i] = unlockedPieces[i];
                //}
                //if (rand == 0)
                //{
                //    pieces[i] = chaosUnlockedPieces[i];
                //}
            }
            StartCoroutine(SkinChange());
            skinChange = false;
        }  //NOTHING TO DO WITH timer. but is called every time an action made so lazily putting here.
        

        blackStopPowerup--;
        crateMagicPowerup--;
        skinChangeMoves--;
        spread = false;
        bool changed = false;
        if(timerBroken > 0 && !zenMode)
        {
            timerBroken--;
            changed = true;
        }
        if(timerBroken == 0 && changed)
        {
            FindObjectOfType<CountdownTimer>().FixTimer();
            timerBreak.SetActive(false);
        }

        if(skinChangeMoves == 0)
        {
            for (int i = 0; i < pieces.Length; i++)
            {
                pieces[i] = piecesHold[i];
            }
            UpdateSprites();
        }
        //StartCoroutine(SkinChange());
    }
    public void BombAtPosition(int x, int y)
    {
        if (!gameEnd)
        {
            audioManager.Play("bomb");
            //np.justExploded = true;
            //PlayerPrefs.SetInt("ColumnBomb", PlayerPrefs.GetInt("ColumnBomb", 0) + 1);
            foreach (Node node in board)
            {
                if (node.getPiece() != null)
                {
                    Vector2 newPos = getPositionFromPoint(node.getPiece().index);
                    if (node.getPiece().index.x == x && node.getPiece().index.y == y) //Create large explosion where clicked.
                    {
                        CreateExplosion("large", newPos);
                    }
                    if (node.getPiece().index.x >= x - 1 && node.getPiece().index.x <= x + 1 && node.getPiece().index.y >= y - 1 && node.getPiece().index.y <= y + 1)
                    {
                        gravityWaitTimer = 1f;
                        node.getPiece().justExploded = true;
                        nodePiece.gameObject.SetActive(false);
                        dead.Add(node.getPiece());
                        node.SetPiece(null);
                        node.value = 0;


                        GameObject desObj = Instantiate(destroyEffect, gameboard);      //Create Destroy Effect
                        RectTransform desRect = desObj.GetComponent<RectTransform>();
                        desRect.anchoredPosition = newPos;
                        Destroy(desObj, 1f);
                        CreateExplosion("small", newPos);
                    }
                }

            }
            ApplyGravityToBoard();
        }
    }

    public void ChaosClicked(bool enabled)
    {
        if (enabled)
        {
            chaosMode = true;
            zenDescriptionPanel.SetActive(false);
            chaosDescriptionPanel.SetActive(true);
            zen.isOn = false;
            foreach (Transform child in shoplist)
            {
                if(child.gameObject.name.Substring(0,4) == "time")
                {
                    ShopItem i = child.GetComponent<ShopItem>();
                    i.selected.gameObject.SetActive(false);
                    child.gameObject.SetActive(false);
                }
            }
            foreach (Transform child in inventory)
            {
                if (child.gameObject.name.Substring(0, 4) == "time")
                {
                    Destroy(child.gameObject);
                }
            }
            inventoryCheck.Remove(0);
            inventoryCheck.Remove(1);
            selectedPowerups.text = inventoryCheck.Count + "/" + inventorySlots + " SELECTED";
        }
        if (!enabled)
        {
            chaosMode = false;
            chaosDescriptionPanel.SetActive(false);
            if (!zenMode)
            {
                foreach (Transform child in shoplist)
                {
                    if (child.gameObject.name.Substring(0, 4) == "time")
                    {
                        child.gameObject.SetActive(true);
                    }
                }
            }
        }

        
    }
    public void ZenClicked(bool enabled)
    {
        if (enabled)
        {
            zenDescriptionPanel.SetActive(true);
            chaosDescriptionPanel.SetActive(false);
            zenMode = true;
            timerBreak.SetActive(true);
            chaos.isOn = false;
            foreach(Transform node in gameboard)
            {
                Destroy(node.gameObject);
                
                    
            }
            InitializeBoard();
            VerifyBoard();
            InstantiateBoard();
            foreach (Transform child in shoplist)
            {
                if (child.gameObject.name.Substring(0, 4) == "time")
                {
                    ShopItem i = child.GetComponent<ShopItem>();
                    i.selected.gameObject.SetActive(false);
                    child.gameObject.SetActive(false);
                    
                }
            }
            foreach (Transform child in inventory)
            {
                if (child.gameObject.name.Substring(0, 4) == "time")
                {
                    Destroy(child.gameObject);
                    
                }
            }
            inventoryCheck.Remove(0);
            inventoryCheck.Remove(1);
            selectedPowerups.text = inventoryCheck.Count + "/" + inventorySlots + " SELECTED";
        }
        if (!enabled)
        {
            zenMode = false;
            zenDescriptionPanel.SetActive(false);
            timerBreak.SetActive(false);
            foreach (Transform node in gameboard)
            {
                Destroy(node.gameObject);


            }
            InitializeBoard();
            VerifyBoard();
            InstantiateBoard();
            if (!chaosMode)
            {
                foreach (Transform child in shoplist)
                {
                    if (child.gameObject.name.Substring(0, 4) == "time")
                    {
                        child.gameObject.SetActive(true);
                    }
                }
            }
        }

        
    }
    private void CreateExplosion(string size, Vector2 pos)
    {

        if (size == "small")
        {

            int explosionNumber = random.Next(25, 100) / (100 / (explosionAnimationsSmall.Length));
            GameObject explosion = explosionAnimationsSmall[explosionNumber];
            GameObject explObj = Instantiate(explosion, gameboard);
            RectTransform explRect = explObj.GetComponent<RectTransform>();
            explRect.anchoredPosition = pos;
            Destroy(explObj, 1f);
        }
        if (size == "large")
        {

            int explosionNumber = random.Next(25, 100) / (100 / (explosionAnimations.Length));
            GameObject explosion = explosionAnimations[explosionNumber];
            GameObject explObj = Instantiate(explosion, gameboard);
            RectTransform explRect = explObj.GetComponent<RectTransform>();
            explRect.anchoredPosition = pos;
            Destroy(explObj, 1f);
        }

    }

    private IEnumerator FlashRed(Text t)
    {
        flashRedInProgress = true;
        WaitForSeconds redFlash = new WaitForSeconds(.3f);
        Color original = t.color;
        t.color = Color.red;
        yield return redFlash;
        t.color = original; //new Color(.2f,.2f,.2f,1);
        flashRedInProgress = false;
    }

    private IEnumerator BreakTimer(int actions)
    {

        yield return new WaitForSeconds(.4f);

        FindObjectOfType<CountdownTimer>().BreakTimer();
        timerBreak.SetActive(true);
        timerBroken += actions;
        GameObject desObj = Instantiate(destroyEffect, timerPanel);      //Create Destroy Effect
        RectTransform desRect = desObj.GetComponent<RectTransform>();
        desRect.anchoredPosition = timerPanel.anchoredPosition;
        Destroy(desObj, 1f);
    }

    private IEnumerator Cashout()
    {
        cashoutStarted = true;
        scorePieces = new NodePiece[numberOfPieces + 1, width * height / 2];
        typecount = new int[numberOfPieces + 1];
        foreach (Node n in board) //group nodes into types track number of each
        {
            if (n.getPiece() != null)
            {
                scorePieces[n.value, typecount[n.value]] = n.getPiece();
                typecount[n.value] += 1;
            }

        }
        for(int i =tokens; i>=0; i--)
        {
            score += 1000;
            ScoreCounter.score = score;
        }
        WaitForSeconds typePause = new WaitForSeconds(.3f);
        for (int t = 2; t < numberOfPieces; t++)
        {
            WaitForSeconds wait = new WaitForSeconds((float)t / 300);
            for (int i = 0; i < typecount[t]; i++)
            {
                scoringWaitTimer = 1.5f;
                if (t < numberOfPieces - 2 && i % 2 == 0)
                {          //reduces sound for lower valued pieces cause there are heaps of them
                    audioManager.Play("gold");
                }
                if (t >= numberOfPieces - 2)
                {
                    audioManager.Play("gold");
                }
                if (chaosMode && t == numberOfPieces-2)
                {
                    chaosGold += 1;
                }
                scoreAndRemove(scorePieces[t, i]);

                redKeys = score / redKeyValue;
                yield return wait;
            }
            yield return typePause;
        }
        int number = MoveCounter.movesMade;  // Count moves made for Blue Key conversion.
        movesEndCount = 0;
        number = 45;
        audioManager.Play("Count");
        MoveCounter.movesMade = movesEndCount;
        float timePerDecrement = 1.5f / number;
        float timer = 0;
        while (number > 0)   
        {
            timer += Time.deltaTime;
            if (timer >= timePerDecrement)
            {
                number--;
                movesEndCount++;
                MoveCounter.movesMade = movesEndCount;
                audioManager.Play("Count");
                blueKeys = movesEndCount / blueKeyValue;
                timer = 0;
            }
            yield return null;

        }

        PlayerPrefs.SetInt("RedKeys", PlayerPrefs.GetInt("RedKeys", 0) + redKeys);
        PlayerPrefs.SetInt("BlueKeys", PlayerPrefs.GetInt("BlueKeys", 0) + blueKeys);
        redKeys = 0;
        blueKeys = 0;
        PlayerPrefs.SetInt("CurrentLevel", currentLevel);
        PlayerPrefs.SetInt("CurrentPoints", currentPoints);
        cashoutFinished = true;
    }
    public void KickTimerUpAss()
    {
        CountdownTimer.currentTime = CountdownTimer.time;
    }
    //public void recordInventory()
    //{
    //    string invRec = "";
    //    foreach (Transform child in inventory)
    //    {
    //        PowerUpItem p = child.gameObject.GetComponent<PowerUpItem>();
    //        string key = "abcdefghijklmnopqrstuvwxyz";
    //        char k = key[p.id];
    //        invRec += k;
            
    //    }
    //    PlayerPrefs.SetString("Inventory", invRec);
    //}

    void scoreAndRemove(NodePiece p)
    {
        int val = (8 * (int)Mathf.Pow(5, p.value - 2));
        score += val;
        currentPoints += val;
        ScoreCounter.score = score;
        p.Initialize(1, p.index, pieces[0]);
        GameObject desObj = Instantiate(destroyEffect, gameboard);      //Create Destroy Effect
        RectTransform desRect = desObj.GetComponent<RectTransform>();
        desRect.anchoredPosition = p.pos;
        Destroy(desObj, 1f);
    }

    void CheckForEndGame()
    {
        if (TurnTracker.turnsRemaining <= 0)
        {

            currentLevel = PlayerPrefs.GetInt("CurrentLevel", 0);
            currentPoints = PlayerPrefs.GetInt("CurrentPoints", 0);
            /*
            if (currentLevel < maxLevel)
            {
                progressBar.maxValue = levelPoints[currentLevel];
                progressBar.value = currentPoints;
                levelText.text = "LEVEL " + currentLevel.ToString("n0");
            }
            */
            FindObjectOfType<GameManager>().GameOver();
            gameEnd = true;
            CountdownTimer.currentTime = 0f;
        }
    }
    public void EndTheGame()
    {
        if (tutorial)
        {
            FindObjectOfType<GameManager>().ToMenu();
            return;
        }
        TurnTracker.turnsRemaining = 0;
        FindObjectOfType<GameManager>().GameOver();
        gameEnd = true;
        CountdownTimer.currentTime = 0f;
        
    }
    void GenerateLeveledUpPiece(Point p, int newVal, Vector2 newPos)
    {
       
        NodePiece newPiece;
        if (dead.Count > 0)
        {
            NodePiece revived = dead[0];
            revived.gameObject.SetActive(true);
            revived.rect.anchoredPosition = newPos;
            newPiece = revived;

            dead.RemoveAt(0);
        }
        else
        {
            GameObject obj = Instantiate(nodePiece, gameboard);
            NodePiece n = obj.GetComponent<NodePiece>();
            RectTransform rect = obj.GetComponent<RectTransform>();
            rect.anchoredPosition = newPos;
            newPiece = n;

        }
        if (newVal == 7)
        {
            audioManager.Play("gold");
            PlayerPrefs.SetInt("GoldCreated", PlayerPrefs.GetInt("GoldCreated", 0) + 1);
            if (!tutorial)
            {
                TokenScript newToken;
                int random = Random.Range(0, 30);
                if (random < 5)
                {
                    Infect();
                    Infect();
                }
                random = Random.Range(0, 30);
                if (random < 3)
                {
                    Infect();
                }
                GameObject tokenObj = Instantiate(token, gameboard);      //spawn token
                TokenScript t = tokenObj.GetComponent<TokenScript>();
                RectTransform tokenRect = tokenObj.GetComponent<RectTransform>();
                tokenRect.anchoredPosition = newPos;
                newToken = t;
                if (!newToken.Initialize(2, 2f, newPiece.index.x, newPiece.index.y))
                {
                    Destroy(tokenObj);
                }
            }
            else
            {
                if(tutorialPhase == 4)
                {
                    tutorialPanel.SetActive(true);
                    tutorialStop = true;
                    
                }
            }
        }
        if (newVal <= 7 && newVal >= 3)
        {
            GameObject effectObj = Instantiate(sprinkleEffects[newVal], gameboard);
            RectTransform effectRect = effectObj.GetComponent<RectTransform>();
            effectRect.anchoredPosition = newPos;

        }

        if(newVal == 6 && !tutorial) //spawn tokens when red created
        {
            if (inventory.childCount <= inventorySlots) //correct bug where haveTempPowerUp bool is incorrect. random check. is called here just because.
            {
                haveTempPowerUp = false;
            }
            TokenScript newToken;
            GameObject tokenObj = Instantiate(token, gameboard);      //spawn token
            TokenScript t = tokenObj.GetComponent<TokenScript>();
            RectTransform tokenRect = tokenObj.GetComponent<RectTransform>();
            tokenRect.anchoredPosition = newPos;
            newToken = t;
            if(!newToken.Initialize(1, 2f, newPiece.index.x, newPiece.index.y))
            {
                Destroy(tokenObj);
            }

        }
        if(newVal == 5 && inventory.childCount < 8 && !tutorial) //spawn Tempoary power ups.
        {
            int rand = Random.Range(0, 80);
            if (zenMode)
            {
                rand = Random.Range(0, 50);
            }
            if(rand < 3)
            {
                if (!haveTempPowerUp)
                {
                    haveTempPowerUp = true;
                    rand = Random.Range(0, 11);
                    if((zenMode || chaosMode) && rand <= 1)
                    {
                    }
                    else if(rand != 8)
                    {
                        TempPowerUp newPU;
                        GameObject powerUpObj = Instantiate(tempPowerUp, gameboard);
                        TempPowerUp t = powerUpObj.GetComponent<TempPowerUp>();
                        RectTransform powerUpRect = powerUpObj.GetComponent<RectTransform>();
                        powerUpRect.anchoredPosition = newPos;
                        newPU = t;
                        if(!newPU.Initialize(newPiece.index.x, newPiece.index.y, 6f, powerupSprites[rand], rand)){
                            Destroy(powerUpObj);
                        }
                    }
                }
            }
        }

        if (newVal == 4 && !tutorial) //spawn crate when blue stones combined
        {
            int rand = Random.Range(0, 80);
            if (zenMode)
            {
                rand = Random.Range(0, 40);
            }
            if (rand < 15 || crateMagicPowerup > 0) //or powerup cratemagic is active
            {
                Crate newCrate;
                GameObject crateObj = Instantiate(crate, gameboard);      //spawn crate
                Crate c = crateObj.GetComponent<Crate>();
                RectTransform crateRect = crateObj.GetComponent<RectTransform>();
                crateRect.anchoredPosition = newPos;
                newCrate = c;
                if (!newCrate.Initialize(newPos, newPiece.index.x, newPiece.index.y, 3f))
                {
                    Destroy(crateObj);
                }
            }

        }



        newPiece.Initialize(newVal, p, pieces[newVal - 1]);

        if (newVal == 3 && !tutorial)
        {
            int rand = Random.Range(0, 6);
            if (rand > 3)
            {
                Explosive newExplosive;
                GameObject explosiveObj = Instantiate(explosive, gameboard);      //spawn explosive
                Explosive e = explosiveObj.GetComponent<Explosive>();
                RectTransform explosiveRect = explosiveObj.GetComponent<RectTransform>();
                explosiveRect.anchoredPosition = newPos;
                newExplosive = e;
                if(!newExplosive.Initialize(newPiece.index.x, newPiece.index.y, 1.5f))
                {
                    Destroy(explosiveObj);
                }
            }
        }

        Node hole = getNodeAtPoint(p);
        hole.SetPiece(newPiece);
        ResetPiece(newPiece);
    }

    public void TempSkinChange(int moves)
    {
        skinChange = true;
        skinChangeMoves = moves;
    }

    public void TokenRush()
    {
        StartCoroutine(TR(.01f, 2.5f, 40));
    }
    private IEnumerator SkinChange()
    {
        WaitForSeconds piecePause = new WaitForSeconds(.4f);
        
        for (int i = 0; i < pieces.Length; i++)
        {
            int rand = Random.Range(0, 3);

            if (rand == 2)
            {
                pieces[i] = upgradedPieces[i];
            }
            if (rand == 1)
            {
                pieces[i] = unlockedPieces[i];
            }
            if (rand == 0)
            {
                pieces[i] = chaosUnlockedPieces[i];
            }
            UpdateSprites();
            yield return piecePause;
        }
    }

    private IEnumerator TR(float pause, float life, int quantity)
    {
        WaitForSeconds tokenPause = new WaitForSeconds(pause);


        for (int i = 0; i < quantity; i++)
        {
            int randomX = Random.Range(0, width);
            int randomY = Random.Range(0, height);
            Point randomPoint = new Point(randomX, randomY);
            Node node = getNodeAtPoint(randomPoint);
            NodePiece nodePiece = node.getPiece();
            if (popups.Contains("x" + randomX + "y" + randomY) || nodePiece == null)
            {
            }
            else
            {
                TokenScript newToken;
                GameObject tokenObj = Instantiate(token, gameboard);      //spawn token
                TokenScript t = tokenObj.GetComponent<TokenScript>();
                RectTransform tokenRect = tokenObj.GetComponent<RectTransform>();
                tokenRect.anchoredPosition = nodePiece.pos;
                newToken = t;
                if(!newToken.Initialize(1, life, randomX, randomY))
                {
                    Destroy(tokenObj);
                }

            }
            yield return tokenPause;
        }
    }
    public void BombRush()
    {
        StartCoroutine(BR(.02f, 3.8f, 10));
    }
    
    private IEnumerator BR(float pause, float life, int quantity)
    {
        WaitForSeconds bombPause = new WaitForSeconds(pause);


        for (int i = 0; i < Random.Range(quantity-2,quantity+2); i++)
        {
            int randomX = Random.Range(0, width);
            int randomY = Random.Range(0, height);
            Point randomPoint = new Point(randomX, randomY);
            Node node = getNodeAtPoint(randomPoint);
            NodePiece nodePiece = node.getPiece();
            if (popups.Contains("x" + randomX + "y" + randomY) || nodePiece == null)
            {
            }
            else
            {
                Explosive newExplosive;
                GameObject explosiveObj = Instantiate(explosive, gameboard);      //spawn explosive
                Explosive e = explosiveObj.GetComponent<Explosive>();
                RectTransform explosiveRect = explosiveObj.GetComponent<RectTransform>();
                explosiveRect.anchoredPosition = nodePiece.pos;
                newExplosive = e;
                if(!newExplosive.Initialize(randomX, randomY, life))
                {
                    Destroy(explosiveObj);
                }

            }
            yield return bombPause;
        }
    }
    private IEnumerator InfectAndSpread()
    {
        WaitForSeconds infectPause = new WaitForSeconds(5f);
        Infect();
        Infect();
        for(int i =0; i < 60; i++)
        {
            spreadInfection();
            yield return infectPause;
        }
    }
    public void ExplosiveTouch()
    {
        //if (activeBomb != null)
        //{
        //    Powerups.instance.deActivateBomb(activeBomb);
        //}
        armedBombStatus = 2;
    }


    public void ExtraTurn(Vector2 pos)
    {

        GameObject arrowObj = Instantiate(upArrow, turnBox);      //Create Up Arrow
        RectTransform arrowRect = arrowObj.GetComponent<RectTransform>();
        arrowRect.anchoredPosition = new Vector2(10, -200);
        Destroy(arrowObj, 3f);

        Point displayPoint = getPointFromPosition(pos);
        Node node = getNodeAtPoint(displayPoint);
        NodePiece piece = node.getPiece();

        Vector2 extraTurnPos = piece.pos;
        if (piece.index.x == 0) //if the animation is going to play too close to edge move it in 1.
        {
            Point newPosPoint = new Point(1, piece.index.y);
            extraTurnPos = getPositionFromPoint(newPosPoint);
        }
        if (piece.index.x == width - 1)
        {
            Point newPosPoint = new Point(width - 2, piece.index.y);
            extraTurnPos = getPositionFromPoint(newPosPoint);
        }
        GameObject extraObj = Instantiate(extraTurn, gameboard);      //Create ExtraTurn Effect
        RectTransform extraRect = extraObj.GetComponent<RectTransform>();
        extraRect.anchoredPosition = extraTurnPos;
        Destroy(extraObj, 3f);
        TurnTracker.turnsRemaining += 1;
    }

    bool TurnRandomPieceBlack()
    {
        int randomX = Random.Range(0, width); 
        int randomY;
        if(zenMode){
            randomY = Random.Range(6, height);
        }
        else
        {
            randomY = Random.Range(0, height);
        }
        Point randomPoint = new Point(randomX, randomY);
        Node node = getNodeAtPoint(randomPoint);
        NodePiece nodePiece = node.getPiece();
        if (chaosMode) //also spawns bombs in chaos mode.
        {
            int randomX2 = Random.Range(0, width);
            int randomY2 = Random.Range(0, height);
            Point randomPoint2 = new Point(randomX2, randomY2);
            Node node2 = getNodeAtPoint(randomPoint2);
            NodePiece nodePiece2 = node2.getPiece();
            if (node2.value != 8 && nodePiece2 != null)
            {
                GameObject effectObj = Instantiate(rippleEffect, gameboard);      //ripple  effect
                RectTransform effectRect = effectObj.GetComponent<RectTransform>();
                effectRect.anchoredPosition = nodePiece2.pos;
            }
            if (nodePiece2 != null)
            {
                blackPieceCount += 1;
                nodePiece2.Initialize(numberOfPieces, randomPoint2, pieces[numberOfPieces - 1]);
                setValueAtPoint(randomPoint2, numberOfPieces);
                audioManager.Play("black");
            }
            int bombsounds = 0;
            foreach (Node n in board)
            {
                if (n == null || n.getPiece() == null || node == null || node.getPiece() == null)
                {
                   // Invoke("ApplyGravityToBoard", .2f);
                }
                else
                {
                    Vector2 newPos = getPositionFromPoint(n.getPiece().index);
                    if (n.value != 8 && ((nodePiece.index.y > n.index.y - 2 && nodePiece.index.y < n.index.y + 2 && nodePiece.index.x >= n.index.x - 2 && nodePiece.index.x <= n.index.x + 2) || nodePiece.index.y == n.index.x))
                    {
                        gravityWaitTimer = 1f;
                        n.getPiece().justExploded = true;
                        //nodePiece.gameObject.SetActive(false);
                        dead.Add(n.getPiece());
                        n.SetPiece(null);
                        n.value = 0;


                        GameObject desObj = Instantiate(destroyEffect, gameboard);      //Create Destroy Effect
                        RectTransform desRect = desObj.GetComponent<RectTransform>();
                        desRect.anchoredPosition = newPos;
                        Destroy(desObj, 1f);
                        if (bombsounds < 3)
                        {
                            bombsounds++;
                            audioManager.Play("bomb");
                        }


                        CreateExplosion("small", newPos);
                        StartCoroutine(cameraShake.Shake(.08f, .04f));

                    }

                }
            }
            Invoke("ApplyGravityToBoard", .2f);
            return true;
        }
        else
        {
            if (nodePiece == null || nodePiece.value == numberOfPieces) //if there's already a black piece in this position
            {
                return false;
            }
            else
            {
                GameObject effectObj = Instantiate(rippleEffect, gameboard);      //ripple  effect
                RectTransform effectRect = effectObj.GetComponent<RectTransform>();
                effectRect.anchoredPosition = nodePiece.pos;

                blackPieceCount += 1;
                nodePiece.Initialize(numberOfPieces, randomPoint, pieces[numberOfPieces - 1]);
                setValueAtPoint(randomPoint, numberOfPieces);


                audioManager.Play("black");
                return true;
            }
        }
    }

    void ApplyGravityToBoard()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = (height - 1); y >= 0; y--)
            {
                Point p = new Point(x, y);
                Node node = getNodeAtPoint(p);
                int val = getValueAtPoint(p);
                if (val != 0) continue; //If not hole, do nothing
                for (int ny = (y - 1); ny >= -1; ny--)
                {

                    Point next = new Point(x, ny);
                    int nextVal = getValueAtPoint(next);
                    if (nextVal == 0)
                        continue;
                    if (nextVal != -1) //if we find a piece above
                    {
                        Node got = getNodeAtPoint(next);
                        NodePiece piece = got.getPiece();
                        //set the hole
                        node.SetPiece(piece);
                        update.Add(piece);

                        //Replace the hole
                        got.SetPiece(null); //Node.SetPiece() will set null to 0 (hole)
                    }
                    else //Hit an end
                    {
                        int newVal = fillPiece(false);
                        NodePiece piece;
                        Point fallPnt = new Point(x, (-1 - fills[x]));
                        if (dead.Count > 0)
                        {
                            NodePiece revived = dead[0];
                            revived.gameObject.SetActive(true);
                            revived.rect.anchoredPosition = getPositionFromPoint(fallPnt);
                            piece = revived;

                            dead.RemoveAt(0);
                        }
                        else
                        {
                            GameObject obj = Instantiate(nodePiece, gameboard);
                            NodePiece n = obj.GetComponent<NodePiece>();
                            RectTransform rect = obj.GetComponent<RectTransform>();
                            rect.anchoredPosition = getPositionFromPoint(fallPnt);
                            piece = n;

                        }

                        piece.Initialize(newVal, p, pieces[newVal - 1]);

                        Node hole = getNodeAtPoint(p);
                        hole.SetPiece(piece);
                        ResetPiece(piece);
                        fills[x]++;
                    }
                    break;

                }

            }
        }
    }

    FlippedPieces getFlipped(NodePiece p)
    {
        FlippedPieces flip = null;
        for (int i = 0; i < flipped.Count; i++)
        {
            if (flipped[i].getOtherPiece(p))
            {
                flip = flipped[i];
                break;
            }
        }
        return flip;
    }

    public void Deselect(int i)
    {
        foreach (Transform child in inventory)
        {
            PowerUpItem p = child.GetComponent<PowerUpItem>();
            if (p.id == i)
            {
                Destroy(p.gameObject);
                inventoryCheck.Remove(i);
                startButton.interactable = false;
            }
        }
        selectedPowerups.text = inventoryCheck.Count + "/" + inventorySlots + " SELECTED";
    }

    public bool AddPowerup(int i)
    {
        bool added = false;
        if(inventory.childCount < inventorySlots && !inventoryCheck.Contains(i))
        {
            audioManager.Play("PowerUp"); 
            GameObject g = Instantiate(powerUpItem, inventory);
            PowerUpItem powerUp = g.GetComponent<PowerUpItem>();
            powerUp.Initialize(i, powerupSprites[i], tokenPrices[i], false);
            powerUp.gameObject.SetActive(true);
            inventoryCheck.Add(i);
            if(inventory.childCount == inventorySlots)
            {
                startButton.interactable = true;
            }
            added = true;
        }
        else if (inventoryCheck.Contains(i))
        {
            foreach (Transform child in inventory)
            {
                PowerUpItem p = child.GetComponent<PowerUpItem>();
                if (p.id == i)
                {
                    Destroy(p.gameObject);
                    inventoryCheck.Remove(i);
                    startButton.interactable = false;
                }
            }

        }
        selectedPowerups.text = inventoryCheck.Count + "/" + inventorySlots + " SELECTED";
        return added;
    }
    public bool AddPowerup(int i, bool tut)
    {
        bool added = false;
        if (inventory.childCount < inventorySlots && !inventoryCheck.Contains(i))
        {
            audioManager.Play("gold");
            GameObject g = Instantiate(powerUpItem, inventory);
            PowerUpItem powerUp = g.GetComponent<PowerUpItem>();
            powerUp.Initialize(i, powerupSprites[i], tokenPrices[i], false, tut);
            powerUp.gameObject.SetActive(true);
            inventoryCheck.Add(i);
            if (inventory.childCount == inventorySlots)
            {
                startButton.interactable = true;
            }
            added = true;
        }
        else if (inventoryCheck.Contains(i))
        {
            foreach (Transform child in inventory)
            {
                PowerUpItem p = child.GetComponent<PowerUpItem>();
                if (p.id == i)
                {
                    Destroy(p.gameObject);
                    inventoryCheck.Remove(i);
                    startButton.interactable = false;
                }
            }

        }
        selectedPowerups.text = inventoryCheck.Count + "/" + inventorySlots + " SELECTED";
        return added;
    }
    public void RestorePowerup(int i)
    {
        GameObject g = Instantiate(powerUpItem, inventory);
        PowerUpItem powerUp = g.GetComponent<PowerUpItem>();
        powerUp.Initialize(i, powerupSprites[i], tokenPrices[i], false);
        powerUp.gameObject.SetActive(true);
        inventoryCheck.Add(i);
    }

    public void CreateTempPowerup(int i)
    {
        GameObject g = Instantiate(powerUpItem, inventory);
        PowerUpItem powerUp = g.GetComponent<PowerUpItem>();
        powerUp.Initialize(i, powerupSprites[i], tokenPrices[i], true);
        powerUp.gameObject.SetActive(true);
    }

    public void ArmBomb(Bomb b)
    {
        armedBombStatus = b.id;
        activeBomb = b;
    }

    public void disarmBomb()
    {
        armedBombStatus = 0;
        activeBomb = null;
    }


    public void ResetPiece(NodePiece piece)
    {
        if (piece != null) {
            piece.ResetPosition();
            update.Add(piece);
        }
    }

    public void FlipPieces(Point one, Point two, bool main)
    {
        if (getValueAtPoint(one) < 0) return;
        Node nodeOne = getNodeAtPoint(one);
        NodePiece pieceOne = nodeOne.getPiece();
        if (getValueAtPoint(two) > 0)
        {
            Node nodeTwo = getNodeAtPoint(two);
            NodePiece pieceTwo = nodeTwo.getPiece();
            nodeOne.SetPiece(pieceTwo);
            nodeTwo.SetPiece(pieceOne);

            if (main)
                flipped.Add(new FlippedPieces(pieceOne, pieceTwo));


            update.Add(pieceOne);
            update.Add(pieceTwo);
        }
        else
        {
            ResetPiece(pieceOne);
        }
    }

    void KillPiece(Point p)
    {
        List<KilledPiece> available = new List<KilledPiece>();
        for (int i = 0; i < killed.Count; i++)
        {
            if (!killed[i].moving) available.Add(killed[i]);
        }
        KilledPiece set = null;
        if (available.Count > 0)
        {
            set = available[0];
        }
        else
        {
            GameObject kill = GameObject.Instantiate(killedPiece, killedBoard);
            KilledPiece kPiece = kill.GetComponent<KilledPiece>();
            set = kPiece;
            killed.Add(kPiece);
        }

        int val = p.valWhenKilled;

        if ((set != null) && val > 0 && val < numberOfPieces)
        {

            set.Initialize(pieces[val - 1], getPositionFromPoint(p), getFromPositionFromPoint(p));
        }
        else if ((set != null) && val == 0) // THIS IS TO OVERRIDE A BUG. Sometimes valueWhenKlled will be 0.
        {
           set.Initialize(blankErrorHide, getPositionFromPoint(p), getFromPositionFromPoint(p));
        }
        
    }



    void InitializeBoard()
    {
        board = new Node[width, height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                // board[x, y] = new Node((boardLayout.rows[y].row[x]) ? -1 : fillPiece(true), new Point(x, y));
                if (zenMode)
                {
                    if (y < 6)
                    {
                        board[x, y] = new Node(-1, new Point(x, y));
                    }
                    else
                    {
                        board[x, y] = new Node(fillPiece(true), new Point(x, y));
                    }
                }
                else
                {
                    board[x, y] = new Node(fillPiece(true), new Point(x, y));
                }
            }
        }

    }

    void VerifyBoard()
    {
        List<int> remove;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Point p = new Point(x, y);
                int val = getValueAtPoint(p);
                if (val <= 0) continue;

                remove = new List<int>();
                while (isConnected(p, true).Count > 0)
                {
                    val = getValueAtPoint(p);
                    if (!remove.Contains(val))
                        remove.Add(val);
                    setValueAtPoint(p, newValue(ref remove));
                }
            }

        }
    }

    void InstantiateBoard()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Node node = getNodeAtPoint(new Point(x, y));
                int val = node.value;
                if (val <= 0) continue;
                GameObject p = Instantiate(nodePiece, gameboard);
                NodePiece piece = p.GetComponent<NodePiece>();
                RectTransform rect = p.GetComponent<RectTransform>();
                rect.anchoredPosition = new Vector2(32 + (64 * x), -32 - (64 * y));
                piece.Initialize(val, new Point(x, y), pieces[val - 1]);
                piece.gameObject.SetActive(true);
                node.SetPiece(piece);
            }
        }
    }

    void UpdateSprites()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Node node = getNodeAtPoint(new Point(x, y));
                int i = node.value-1;
                if (i >= 0)
                {
                    if (node.getPiece() != null)
                    {
                        node.getPiece().UpdateSprite(pieces[i]);
                    }
                }
            }
        }
    }

    List<Point> isConnected(Point p, bool main)
    {
        List<Point> connected = new List<Point>();
        int val = getValueAtPoint(p);
        int highest = numberOfPieces - 2; //highest three pieces can't be matched
        Point[] directions =
            {
              Point.up,
              Point.right,
              Point.down,
              Point.left
            };

        foreach (Point dir in directions) //Checking if there is 2 or more same shapes in the directions
        {
            List<Point> line = new List<Point>();

            int same = 0;
            for (int i = 1; i < 3; i++)
            {
                Point check = Point.add(p, Point.mult(dir, i));
                if (getValueAtPoint(check) == val && val < highest)
                {
                    check.fromX = p.x; //recording source point for merge animation
                    check.fromY = p.y;
                    check.valWhenKilled = val;
                    line.Add(check);
                    same++;
                }
            }
            if (same > 1) //If there are more than 1 of the same shape in the direction then we know it is a match
                AddPoints(ref connected, line); //Add these points to the overarching connected list

        }

        for (int i = 0; i < 2; i++) //Checking if we are in the middle of two of the same shapes
        {
            List<Point> line = new List<Point>();
            int same = 0;
            Point[] check = { Point.add(p, directions[i]), Point.add(p, directions[i + 2]) };

            foreach (Point next in check)
            { //check both sides of the piece, if they are the same add to the list
                if (getValueAtPoint(next) == val && val < highest)
                {
                    next.fromX = p.x; //recording source point for merge animation
                    next.fromY = p.y;
                    next.valWhenKilled = val;
                    line.Add(next);
                    same++;
                }
            }

            if (same > 1)
                AddPoints(ref connected, line);
        }

        if (main) //Check for other matches along the currect match
        {
            for (int i = 0; i < connected.Count; i++)
                AddPoints(ref connected, isConnected(connected[i], false));
        }

        return connected;


    }

    void AddPoints(ref List<Point> points, List<Point> add)
    {
        foreach (Point p in add)
        {
            bool doAdd = true;
            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].Equals(p))
                {
                    doAdd = false;
                    break;
                }
            }
            if (doAdd) points.Add(p);
        }



    }

    int fillPiece(bool initial)
    {
        int val = 1;
        if (tutorial)
        {
            if (tutorialPhase < 7)
            {
                return pieces.Length;
            }
            else
            {
                return pieces.Length - 2;
            }
        }
        if (initial) //will fill board with first 4 colours
        {
            val = (random.Next(0, 100) / (100 / (4))) + 1; 
        }
        else //new drops from top can inclue 5th colour
        {
            if (blackStopPowerup > 0) //stops black stones spawning.
            {
                if(numberOfTypesToSpawn == 4)
                {
                    val = (random.Next(25, 100) / (100 / (numberOfTypesToSpawn))) + 1;
                }
                else
                {
                    val = (random.Next(20, 90) / (100 / (numberOfTypesToSpawn))) + 1; //90 instead of 100 to make red stones less frequent.
                }
            }
            else
            {
                if (numberOfTypesToSpawn == 4)
                {
                    val = (random.Next(0, 100) / (100 / (numberOfTypesToSpawn))) + 1;
                }
                else
                {
                    val = (random.Next(0, 90) / (100 / (numberOfTypesToSpawn))) + 1;//90 instead of 100 to make red stones less frequent.
                }
            }
        }
        return val;
    }

    int getValueAtPoint(Point p)
    {
        if (p.x < 0 || p.x >= width || p.y < 0 || p.y >= height) return -1;
        return board[p.x, p.y].value;
    }

    void setValueAtPoint(Point p, int v)
    {
        board[p.x, p.y].value = v;
    }

    Node getNodeAtPoint(Point p)
    {
        return board[p.x, p.y];
    }

    int newValue(ref List<int> remove)
    {
        List<int> available = new List<int>();
        for (int i = 0; i < (numberOfTypesToSpawn); i++)
            available.Add(i + 1);
        foreach (int i in remove)
            available.Remove(i);
        if (available.Count <= 0) return 0;
        return available[random.Next(0, available.Count)];
    }


    string getRandomSeed()
    {
        string seed = "";
        string acceptableChars = "ABCDFEGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890!@#$%^&*()";
        for (int i = 0; i < 20; i++)
        {
            seed += acceptableChars[Random.Range(0, acceptableChars.Length)];
        }
        return seed;
    }

    public Vector2 getPositionFromPoint(Point p)
    {
        return new Vector2(32 + (64 * p.x), -32 - (64 * p.y));
    }
    public Point getPointFromPosition(Vector2 v)
    {
        return new Point(((int)v.x-32)/64, -((int)v.y+32)/64);
    }
    public Vector2 getFromPositionFromPoint(Point p)
    {
        return new Vector2(32 + (64 * p.fromX), -32 - (64 * p.fromY));
    }

    //public void ItemClicked(Button b)
    //{

    //  StartCoroutine(iClick(b));
        
    //}

    //private IEnumerator iClick(Button b, Button a)
    //{
    //    WaitForSeconds displayPause = new WaitForSeconds(2f);
    //    b.gameObject.SetActive(false);
    //    TextMeshProUGUI price = tokenButtons[int.Parse(b.name)].GetComponentInChildren<TextMeshProUGUI>();
    //    price.text = tokenPrices[int.Parse(b.name)].ToString();
    //    tokenButtons[int.Parse(b.name)].SetActive(true);
    //    TextMeshProUGUI description = descriptionPanel.GetComponentInChildren<TextMeshProUGUI>();
    //    description.text = powerupDescriptions[int.Parse(b.name)].ToString();
    //    descriptionPanel.SetActive(true);
    //    latestTokenDescription = int.Parse(b.name);
    //    yield return displayPause;
    //    b.gameObject.SetActive(true);
    //    if (latestTokenDescription == int.Parse(b.name))
    //    {
    //        descriptionPanel.SetActive(false);
    //    }
    //    tokenButtons[int.Parse(b.name)].SetActive(false);
    //}

    public bool CheckForPiece(int i)
    {
        foreach(Transform child in gameboard)
        {
            NodePiece n = child.GetComponent<NodePiece>();
            if(n == null) { return false; }
            else if(n.value == i)
            {
                return true;
            }
        }
        return false;
    }

    public void ItemBuy(Button b, bool temp)
    {
        if (gameRunning && !armedButNotFired)
        {
            if (temp)
            {
                tokens += tokenPrices[int.Parse(b.name.Substring(5))];
            }
            if (tokenPrices[int.Parse(b.name.Substring(5))] > tokens)
            {
                audioManager.Play("CantAfford");
                if (!flashRedInProgress)
                {
                    StartCoroutine(FlashRed(tokenNumber));
                }
            }
            else if (int.Parse(b.name.Substring(5)) == 0)
            {
                PlayerPrefs.SetInt("SlowTime", PlayerPrefs.GetInt("SlowTime", 0) + 1);
                AdjustTimer();
                GameObject effectObj = Instantiate(ripple2Effect, b.GetComponent<RectTransform>());      //ripple  effect
                RectTransform effectRect = effectObj.GetComponent<RectTransform>();
                Destroy(effectObj, 3f);
                audioManager.Play("slow");
                timerEdits.Add(new TimerEdit(3f, 20));
                tokens = tokens - tokenPrices[int.Parse(b.name.Substring(5))];
                audioManager.Play("gold");
            }
            else if (int.Parse(b.name.Substring(5)) == 1)
            {
                PlayerPrefs.SetInt("DisableTimer", PlayerPrefs.GetInt("DisableTimer", 0) + 1);
                AdjustTimer();
                GameObject effectObj = Instantiate(ripple2LargeEffect, b.GetComponent<RectTransform>());      //ripple  effect
                RectTransform effectRect = effectObj.GetComponent<RectTransform>();
                Destroy(effectObj, 3f);

                StartCoroutine(BreakTimer(20));
                audioManager.Play("timeStop");
                tokens = tokens - tokenPrices[int.Parse(b.name.Substring(5))];
                audioManager.Play("gold");
            }
            else if (int.Parse(b.name.Substring(5)) == 2)//blackBomb
            {
                GameObject shockObj = Instantiate(shockwave, b.GetComponent<RectTransform>());      //
                RectTransform shockRect = shockObj.GetComponent<RectTransform>();
                Destroy(shockObj, 3f);
                if (CheckForPiece(1) || CheckForPiece(numberOfPieces))
                {
                    armedBombStatus = 5;
                    AdjustTimer();
                    Point p = new Point(0, 10); //random piece
                    Node n = getNodeAtPoint(p);
                    NodePiece piece = n.getPiece();
                    piece.bombStatus = armedBombStatus;  //set piece as armed.
                    armedBombIndexX = piece.index.x;
                    armedBombIndexY = piece.index.y;  //let Game know  its position
                    //tokens = tokens - tokenPrices[int.Parse(b.name.Substring(5))];
                    //audioManager.Play("gold");
                }
            }
            else if (int.Parse(b.name.Substring(5)) == 3)
            {
                PlayerPrefs.SetInt("BlackStop", PlayerPrefs.GetInt("BlackStop", 0) + 1);
                GameObject shockObj = Instantiate(shockwave, b.GetComponent<RectTransform>());      //
                RectTransform shockRect = shockObj.GetComponent<RectTransform>();
                Destroy(shockObj, 3f);
                audioManager.Play("blackStop");
                AdjustTimer();
                blackStopPowerup = 20;
                tokens = tokens - tokenPrices[int.Parse(b.name.Substring(5))];
                audioManager.Play("gold");
            }
            else if (int.Parse(b.name.Substring(5)) == 4)
            {
                armedButNotFired = true;
                PlayerPrefs.SetInt("DotBomb", PlayerPrefs.GetInt("DotBomb", 0) + 1);
                AdjustTimer();
                armedBombStatus = 6;
                tokens = tokens - tokenPrices[int.Parse(b.name.Substring(5))];
                audioManager.Play("gold");
            }
            else if (int.Parse(b.name.Substring(5)) == 5)
            {
                PlayerPrefs.SetInt("ExtraTurn", PlayerPrefs.GetInt("ExtraTurn", 0) + 1);
                AdjustTimer();
                ExtraTurn(getPositionFromPoint(new Point(0, 10)));
                tokens = tokens - tokenPrices[int.Parse(b.name.Substring(5))];
                audioManager.Play("gold");
            }
            else if (int.Parse(b.name.Substring(5)) == 6)
            {
                armedButNotFired = true;
                PlayerPrefs.SetInt("MagicTouch", PlayerPrefs.GetInt("MagicTouch", 0) + 1);
                AdjustTimer();
                GameObject shockObj = Instantiate(sprinkleEffects[2], b.GetComponent<RectTransform>());      //
                RectTransform shockRect = shockObj.GetComponent<RectTransform>();
                Destroy(shockObj, 3f);

                magicTouch = true;
                tokens = tokens - tokenPrices[int.Parse(b.name.Substring(5))];
                audioManager.Play("gold");
            }
            else if (int.Parse(b.name.Substring(5)) == 7)
            {
                GameObject shockObj = Instantiate(shockwave, b.GetComponent<RectTransform>());      //
                RectTransform shockRect = shockObj.GetComponent<RectTransform>();
                Destroy(shockObj, 3f);
                PlayerPrefs.SetInt("CrazyBomb", PlayerPrefs.GetInt("CrazyBomb", 0) + 1);
                armedBombStatus = 1;
                AdjustTimer();
                Point p = new Point(0, 10); //random piece
                Node n = getNodeAtPoint(p);
                NodePiece piece = n.getPiece();
                piece.bombStatus = armedBombStatus;  //set piece as armed.
                armedBombIndexX = piece.index.x;
                armedBombIndexY = piece.index.y;  //let Game know  its position
                tokens = tokens - tokenPrices[int.Parse(b.name.Substring(5))];
                audioManager.Play("gold");
            }
            else if (int.Parse(b.name.Substring(5)) == 8) //GoldBomb
            {
                GameObject shockObj = Instantiate(shockwave, b.GetComponent<RectTransform>());      //
                RectTransform shockRect = shockObj.GetComponent<RectTransform>();
                Destroy(shockObj, 3f);
                if (CheckForPiece(numberOfPieces - 2) || CheckForPiece(numberOfPieces - 3))
                {
                    armedBombStatus = 7;
                    AdjustTimer();
                    Point p = new Point(0, 10); //random piece
                    Node n = getNodeAtPoint(p);
                    NodePiece piece = n.getPiece();
                    piece.bombStatus = armedBombStatus;  //set piece as armed.
                    armedBombIndexX = piece.index.x;
                    armedBombIndexY = piece.index.y;  //let Game know  its position

                }
            }
            else if (int.Parse(b.name.Substring(5)) == 9) //row Bomb
            {
                
                armedButNotFired = true;
                PlayerPrefs.SetInt("RowBomb", PlayerPrefs.GetInt("RowBomb", 0) + 1);
                AdjustTimer();
                armedBombStatus = 3;
                tokens = tokens - tokenPrices[int.Parse(b.name.Substring(5))];
                audioManager.Play("gold");
                
            }
            else if (int.Parse(b.name.Substring(5)) == 10) //disarm
            {
                GameObject shockObj = Instantiate(shockwave, b.GetComponent<RectTransform>());      //
                RectTransform shockRect = shockObj.GetComponent<RectTransform>();
                Destroy(shockObj, 3f);
                PlayerPrefs.SetInt("Disarm", PlayerPrefs.GetInt("Disarm", 0) + 1);
                foreach (Transform t in gameboard)
                {
                    if (t.gameObject.name == "Explosive(Clone)")
                    {
                        Destroy(t.gameObject);
                    }
                }
                AdjustTimer();
                tokens = tokens - tokenPrices[int.Parse(b.name.Substring(5))];
                audioManager.Play("gold");

            }
            else if (int.Parse(b.name.Substring(5)) == 11) //cratemagic
            {
                PlayerPrefs.SetInt("MoreCrates", PlayerPrefs.GetInt("MoreCrates", 0) + 1);
                AdjustTimer();
                crateMagicPowerup = 20;
                tokens = tokens - tokenPrices[int.Parse(b.name.Substring(5))];
                audioManager.Play("gold");

            }
        }
    }
    public void Infect()
    {
        int randX = Random.Range(1, width-1);
        int randY = Random.Range(1, height - 5);
        if (zenMode)
        {
            randY = Random.Range(6, height - 4);
        }

        Point randomPoint = new Point(randX, randY);
        Node node = getNodeAtPoint(randomPoint);
        NodePiece nodePiece = node.getPiece();


        if (nodePiece != null)
        {
            GameObject effectObj = Instantiate(rippleEffect, gameboard);      //ripple  effect
            RectTransform effectRect = effectObj.GetComponent<RectTransform>();
            effectRect.anchoredPosition = nodePiece.pos;

            nodePiece.infect();
            //infectedPieces.Add(nodePiece);
            infected = true;

            audioManager.Play("Infect");
        }

    }
    public void tutorialPhaseChange(int phase)
    {
        tutorialPhase = phase;
        if(phase == 4)
        {
            tutorialStop = false;
            tutorialPanel.SetActive(false);
        }
        if(phase == 5)
        {
            tutorialStop = true;
            TutorialPhase5SetUp();
        }
        if(phase == 7)
        {
            tutorialStop = false;
            tutorialPanel.SetActive(false);
        }
        if(phase == 9)
        {
            StartCoroutine(InfectAndSpread());
            timerEdits.Add(new TimerEdit(8f, 30));
            AdjustTimer();
        }
        if (phase == 11)
        {
            StartCoroutine(BR(3f, 5f, 5));
        }
        if (phase == 12)
        {
            StartCoroutine(TR(1f, 12f, 100));
        }
        if (phase == 13)
        {
            AddPowerup(9, true);
        }
        if(phase == 15)
        {
            tutorialStop = false;
            tutorialPanel.SetActive(false);
        }
    }

    public void TutorialPhase5SetUp()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Point point = new Point(x, y);
                Node n = getNodeAtPoint(point);
                NodePiece np = n.getPiece();
                int newValue;
                newValue = pieces.Length - 2;
                if (x == 2)
                {
                    if (y == 2 || y == 4 || y == 5)
                    {
                        newValue = 3;
                    }
                }
                if (x == 3)
                {
                    if (y == 3)
                    {
                        newValue = 3;
                    }
                }
                if (x == 5)
                {
                    if (y == 2 || y == 4 || y == 5)
                    {
                        newValue = 4;
                    }
                }
                if (x == 6 || x == 7)
                {
                    if (y == 3)
                    {
                        newValue = 4;
                    }
                }
                np.Initialize(newValue, point, pieces[newValue - 1]);
                setValueAtPoint(point, newValue);
            }
        }
    }
    public void spreadInfection()
    {
        int infectionCount = 0;
        foreach(Node node in board)
        {
            NodePiece n = node.getPiece();
            if (n != null && n.infectionPhase > 0)
            {
                infectionCount++;
                int phase = n.infect();
                if(phase == 0)
                {
                    infectionCount--;

                    int newValue;
                    if(n.value == 1 || n.value == numberOfPieces)
                    {
                        newValue = numberOfPieces;
                    }
                    else
                    {
                        newValue = n.value - 1;
                    }
                    GameObject effectObj = Instantiate(reverseRippleEffect, gameboard);      //ripple  effect
                    RectTransform effectRect = effectObj.GetComponent<RectTransform>();
                    effectRect.anchoredPosition = n.pos;

                    audioManager.Play("InfectionDestroy");
                    Point p = n.GetIndex();
                    n.Initialize(newValue, p, pieces[newValue - 1], phase);
                    setValueAtPoint(p, newValue);
                    update.Add(n);

                }
                else if (phase == 3 || phase == 2)
                {
                    int infectionChance = infectionCount * infectionCount + infectionCount*400;
                    if(TurnTracker.turnsRemaining < 7)
                    {
                        infectionChance = infectionChance / 3;
                    }
                    if (Random.Range(0, width * height * width * height) > infectionChance)
                    {
                        int spreadX = Random.Range(n.index.x - 1, n.index.x + 2);
                        int spreadY = Random.Range(n.index.y - 1, n.index.y + 2);
                        if ((spreadX == n.index.x && spreadY == n.index.y) || spreadX < 0 || spreadY < 0 || spreadX > width - 1 || spreadY > height - 1)
                        {

                        }
                        else
                        {
                            Point spreadPoint = new Point(spreadX, spreadY);
                            Node spreadNode = getNodeAtPoint(spreadPoint);
                            NodePiece nodePiece = spreadNode.getPiece();


                            if (nodePiece != null && nodePiece.infectionPhase <= 0)
                            {
                                GameObject effectObj = Instantiate(rippleEffect, gameboard);      //ripple  effect
                                RectTransform effectRect = effectObj.GetComponent<RectTransform>();
                                effectRect.anchoredPosition = nodePiece.pos;

                                nodePiece.infect();
                                //infectedPieces.Add(nodePiece);
                                infectionCount++;

                                audioManager.Play("Infect");
                            }
                        }
                    }
                }

            }
        }
        if(infectionCount <= 0)
        {
            infected = false;
        }
        spread = true;
    }
}




public class TimerEdit
{
    public float time;
    public int turns;

    public TimerEdit(float ti, int tu)
    {
        time = ti;
        turns = tu;
    }
    public float EditTime()
    {
        if (turns <= 0)
        {
            return 0f;
        }
        else
        {
            
            turns -= 1;
            return time;
        }

    }
}
[System.Serializable]
public class Node
{
    public int value; //0= blank, 1 = cube, 2 = sphere, 3 = cylinder, 4 = pyramid, 5= diamond, -1= hole
    public Point index;
    NodePiece piece;

    public Node(int v, Point i)
    {
        value = v;
        index = i;
    }

    public void SetPiece(NodePiece p)
    {
        piece = p;
        value = (piece == null) ? 0 : piece.value;
        if (piece == null) return;
        piece.SetIndex(index);
    }

    public NodePiece getPiece()
    {
        return piece;
    }
}

[System.Serializable]
public class FlippedPieces
{
    public NodePiece one;
    public NodePiece two;

    public FlippedPieces(NodePiece o, NodePiece t)
    {
        one = o; two = t;
    }

    public NodePiece getOtherPiece(NodePiece p)
    {
        if (p == one)
            return two;
        else if (p == two)
            return one;
        else
            return null;
    }
}