public class Toolbox : Singleton<Toolbox> 
{
    protected Toolbox () {} // guarantee this will be always a singleton only - can't use the constructor!

    public string myGlobalVar = "whatever";

    //public Language language = new Language();

    public TankParams TankParams;

    void Awake () 
    {
        // Your initialization code here

        TankParams = gameObject.AddComponent<TankParams>();
    }
}

/*[System.Serializable]
public class Language {
    public string current;
    public string lastLang;
}*/