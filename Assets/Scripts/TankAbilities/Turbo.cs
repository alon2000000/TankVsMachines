using UnityEngine;
using System.Collections;

public class Turbo : MonoBehaviour, IPassiveSkill
{
    private KeyCode _key = KeyCode.None;
    public KeyCode Key
    {
        get{ return _key; }
        set{ _key = value; }
    }

    public float CostPerSec
    {
        get{ return _params.Get("TurboCostPerSec"); }
    }

    private bool _isActive = false;
    public bool IsActive
    {
        get{ return _isActive; }
        set{ _isActive = value; }
    }

    private TankParams _params;
    // ======================================================================================================================================== //
    void Start () 
    {
        _params = Toolbox.Instance.TankParams;
    }
    // ======================================================================================================================================== //
    void Update () 
    {
        /*if (Cooldown > 0.0F)
        {
            Cooldown -= Time.deltaTime;
            return;
        }

        if (Key == KeyCode.None)
            return;
        if (!Input.GetKeyDown(Key))
            return;

        int level = Mathf.RoundToInt(_params.Get("TeleportLevel"));
        if (level <= 0)
            return;

        if (!IsReady)
            return;

        GameObject tankObj = GameObject.Find("Tank");
        tankObj.transform.position = new Vector3(
            Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 
            Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 
            tankObj.transform.position.z);

        _params.Add("Energy", -Cost);

        Cooldown = MaxCooldown;*/
    }
    // ======================================================================================================================================== //
}
