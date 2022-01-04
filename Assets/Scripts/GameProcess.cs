using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameProcess : MonoBehaviour
{
    public GameObject BlockSpawnerObject;
    ObjectPoolInstantiator BlockSpawner;
    public CatEnter CatEnter;
    public Health HP;
    public PhaseCount PHCount;
    public AudioSource BGM;
    Vector3 CatSpawnPoint = new Vector3(-15f, 8f, 0f);
    public ClickAndDragWithDynamics MouseDrag;
    public GameObject BlockCounter;
    public GameObject FishPrefab;
    public GameObject SmokePrefab;
    Text BlockCount;
    public GameObject Timer;
    public GameObject EndScreen;
    EndingScreen endingScreen;
    TimeCounter timeCounter;
    public List<int> BlockPhase = new List<int>{10, 9, 8};
    public List<int> SavePhase = new List<int>{3, 2, 1};
    public List<float> FishPhase = new List<float>{6f, 9f, 12f};
    public List<float> TimePhase = new List<float>{30, 25, 20};
    int MaxBlock = 0;
    [ReadOnlyInspecter] public int Phase = 0;
    float FishRangeL = -5f;
    float FishRangeR = 5f;
    [HideInInspector] public bool FishEaten = false;
    [ReadOnlyInspecter] public float EndingTimer = 0f;
    public const float waitTime = 3f;
    bool BlockTimerActive = false, GameOver = false;
    Coroutine currentProcess;
    
    void Start()
    {
        endingScreen = EndScreen.GetComponent<EndingScreen>();
        timeCounter = Timer.GetComponent<TimeCounter>();
        Timer.SetActive(false);
        BlockCount = BlockCounter.GetComponent<Text>();
        BlockSpawner = BlockSpawnerObject.GetComponent<ObjectPoolInstantiator>();
        PHCount.numOfPhases = BlockPhase.Count;
        MaxBlock = 0;
        currentProcess = StartCoroutine(Gameprocess(Phase = 0));
    }

    WaitForSeconds HoldBuffer = new WaitForSeconds(1f);
    IEnumerator Gameprocess(int currentPhase)
    {
        PHCount.Phase = currentPhase;
        yield return HoldBuffer;
        MaxBlock += BlockPhase[Phase];
        FishEaten = false;
        //生成魚
        Instantiate(FishPrefab, new Vector3(UnityEngine.Random.Range(FishRangeL, FishRangeR), FishPhase[Phase], 0f), Quaternion.identity);
        while(FishEaten == false)
        {
            //生成方塊直到方塊到達特定數量
            BlockSpawner.SpawnerProcess = true;
            MouseDrag.enabled = true;
            CatSpawnPoint.y = FishPhase[Phase] + 2f;
            CatEnter.SpawnPoint = CatSpawnPoint;
            yield return new WaitUntil(() => BlockSpawner.BCounter >= MaxBlock);
            //關閉方塊生成並等待所有方塊安定waitTime秒
            BlockSpawner.SpawnerProcess = false;
            BlockTimerActive = true;
            yield return new WaitUntil(() => EndingTimer >= waitTime);
            BlockTimerActive = false;
            //關閉拖動控制 生成貓咪
            MouseDrag.enabled = false;
            CatEnter.CatAnimation();
            yield return new WaitUntil(() => CatEnter.AnimationEnd);

            Timer.SetActive(true);
            //在時限內貓咪需吃到魚 否則扣血 並恢復方塊控制 補充savephase[phase]個方塊給玩家
            timeCounter.RunTimer(false, TimePhase[Phase]);
            yield return new WaitUntil(() => FishEaten || timeCounter.TimerEnd);
            if(!FishEaten)
            {
                HP.health -= (HP.health == 0 ? 0 : 1);
                MaxBlock += SavePhase[Phase];
                CatEnter.PlayerCat.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
                Instantiate(SmokePrefab, CatEnter.PlayerCat.transform.position, Quaternion.identity);
                Destroy(CatEnter.PlayerCat, 0.5f);
            }
            Timer.SetActive(false);
        }
        if(Phase < BlockPhase.Count)
        {
            MouseDrag.enabled = true;
            currentProcess = StartCoroutine(Gameprocess(++Phase));
        }
        else
        {
            BGM.Stop();
            EndScreen.SetActive(true);
            endingScreen.EndingAnimation(true); //play win ending
        }
        yield break;
    }

    public void CatDied()
    {
        HP.hurt(1);
        CatEnter.CatAnimation();
    }

    void Update()
    {
        BlockCount.text = BlockSpawner.BCounter + " / " + MaxBlock;
        if (BlockTimerActive && GameObject.FindWithTag("Freeblocks") == null)
        {
            EndingTimer += Time.deltaTime;
        }
        else
            EndingTimer = 0f;
        
        if (HP.health <= 0 && !GameOver)
        {
            GameOver = true;
            StopCoroutine(currentProcess);
            BGM.Stop();
            EndScreen.SetActive(true);
            endingScreen.EndingAnimation(false); //play lose ending
        }
    }
}
