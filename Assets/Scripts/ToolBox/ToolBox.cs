public class Toolbox : Singleton<Toolbox> 
{
    protected Toolbox () {} // guarantee this will be always a singleton only - can't use the constructor!

    public string myGlobalVar = "whatever";

    //public ChipBonusManager ChipBonusManager;

    void Awake () 
    {
        // Your initialization code here

        //ChipBonusManager = gameObject.AddComponent<ChipBonusManager>();
    }
}
