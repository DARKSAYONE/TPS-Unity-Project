using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private List<MobSpawner> Spawner = new List<MobSpawner>();
    [SerializeField] private int RoundCounter = 0;
    [SerializeField] private bool allMobsDead = false;
    [SerializeField] private GameObject PortalToShop;
    [SerializeField] private GameObject PortalToBattle;
    [SerializeField] private bool PlayerInShop = false;
    [Header("BattleRound")]
    [SerializeField] public bool BattleRoundOn = false;
    [SerializeField] public bool BattleRoundProcess = false;
    [SerializeField] public bool BattleRoundComplete = false;
    [Header("ShopTime")]
    [SerializeField] public bool ShopTimeOn = false;
    [SerializeField] public int MobsCount = 2;
    [SerializeField] public List<GameObject> MobsInAction = new List<GameObject>();

    void Start()
    {
        
    }


    void Update()
    {
        if (!BattleRoundOn && !BattleRoundComplete && !BattleRoundProcess)
            BattleRoundStarted(MobsCount);
        if (BattleRoundProcess)
            BattleRoundInProgress();
        if (BattleRoundOn && BattleRoundComplete && BattleRoundProcess)
            BattleRoundCompetly();

        if (PlayerInShop)
            ShoppingTime();
            


    }

    public void BattleRoundStarted(int Mobs)
    {
        BattleRoundOn = true;
        RoundCounter++;
        foreach (var mob in Spawner)
        {
            mob.SpawnMobs(Mobs);
        }
        BattleRoundOn = false;
        BattleRoundProcess = true;
    }

    private bool CheckAllMobsDead()
    {
        return MobsInAction.All(mob => !mob.GetComponent<MobStatLogical>().isAlive);
    }
    public void BattleRoundInProgress()
    {
        BattleRoundOn = true;
        allMobsDead = CheckAllMobsDead();
        if (allMobsDead)
        {
            Debug.Log("Round Complete");
            BattleRoundComplete = true;
        }
    }

    public void BattleRoundCompetly()
    {
        PortalToShop.SetActive(true);
        if(PortalToShop.GetComponent<PortalScript>().isTeleported)
        {
            PlayerInShop = true;
            foreach (var mob in MobsInAction)
            {
                Destroy(mob);
            }
            MobsInAction.Clear();
        }

    }

    public void ShoppingTime()
    {
        if(PortalToBattle.GetComponent<PortalScript>().isTeleported)
        {

        }
    }

    public void RestorePlayer()
    {
        var Stats = Player.GetComponent<PlayerStats>();
        Stats.Health = Stats.MaxHealth;
        Stats.Mana = Stats.MaxMana;
    }
}
