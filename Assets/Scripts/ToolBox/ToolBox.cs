using UnityEngine;

public class Toolbox : Singleton<Toolbox> 
{
    protected Toolbox () {} // guarantee this will be always a singleton only - can't use the constructor!

    public string myGlobalVar = "whatever";

    public int MaxChipOrderInLayer = 0; // jumps in 2 when chip inserted into inventory + when chip is dragged (in 2 because we wnat the logo to be one over the body)

    //public ChipBonusManager ChipBonusManager;

    public ChipsResources ChipsResources;
    public ScrapOnGroundResources ScrapOnGroundResources;
    public SkillsManager SkillsManager;
    public TankParams TankParams;


    void Awake () 
    {
        // Your initialization code here

        //ChipBonusManager = gameObject.AddComponent<ChipBonusManager>();

        ChipsResources = GameObject.Find("Resources").GetComponent<ChipsResources>();
        ScrapOnGroundResources = GameObject.Find("Resources").GetComponent<ScrapOnGroundResources>();
        SkillsManager = GameObject.Find("SkillsManager").GetComponent<SkillsManager>();
        TankParams = GameObject.Find("Tank").GetComponent<TankParams>();
    }
}
