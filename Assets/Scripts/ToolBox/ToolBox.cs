using UnityEngine;

public class Toolbox : Singleton<Toolbox> 
{
    protected Toolbox () {} // guarantee this will be always a singleton only - can't use the constructor!

    public string myGlobalVar = "whatever";

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
