using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{



    public Transform[] spawningPoints;

    public GameObject enemyPrefab;

    public int waveSize, waveIncrease;

    public int enemiesAlive, maxPlanetHealth = 1;

    public AudioController ac;
    public UIController uic;

    public AudioClip laser, explosion, alarm, beep, click, purchase_comp;

    public static int shipID = 4;

    public List<ShipList> shipList = new List<ShipList>();

    private static GameController _lock;

    private int waveNum;

    public static string LoadedScene;


    public static float money;


    private void Start()
    {

        if (_lock == null)
        {
            DontDestroyOnLoad(gameObject);
            _lock = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Init()
    {
        if(LoadedScene == "game")
        {
            enemiesAlive = 0;



            for (int i = 0; i < waveSize; i++)
            {
                Instantiate(enemyPrefab, spawningPoints[Random.Range(0, spawningPoints.Length - 1)].position, Quaternion.identity);
            }

            StartCoroutine(WaveSystem());
        }

    }

    IEnumerator WaveSystem()
    {



        while(LoadedScene == "game")
        {
            if(enemiesAlive <= (waveSize * 0.2f) && Planet.health > 0)
            {
                waveSize *= (waveIncrease+100)/100;
                waveNum++;


                yield return new WaitForSeconds(Random.Range(1,2.5f));

                for (int i = 0; i < waveSize; i++)
                {
                    GameObject enemy = Instantiate(enemyPrefab, spawningPoints[Random.Range(0, spawningPoints.Length - 1)].position, Quaternion.identity);

                    enemy.GetComponent<Enemy>().speed += (.1f * waveNum);

                    yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
                }
            }

            yield return null;
        }


    }


    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {

        LoadedScene = scene.name;

        if (ac == null)
            ac = GameObject.Find("UI & Audio controller").GetComponent<AudioController>();

        if (uic == null)
            uic = GameObject.Find("UI & Audio controller").GetComponent<UIController>();

        Debug.Log(scene.name);

        if (scene.name == "game")
        {
           

            UIController.inited = false;


            uic.planetSlider.maxValue = maxPlanetHealth;
            uic.planetSlider.value = uic.planetSlider.maxValue;


            GameObject player = Instantiate(shipList[shipID].obj, new Vector3(0, 0, 90), Quaternion.identity);

            player.GetComponent<PlayerController>().energySlider = uic.energySlider;

            CameraScript.target = player.transform;
        }
    }

    [System.Serializable]
    public struct ShipList
    {
        public Sprite picture;
        public string title, desc;
        public float cost;
        public GameObject obj;
    }

}
