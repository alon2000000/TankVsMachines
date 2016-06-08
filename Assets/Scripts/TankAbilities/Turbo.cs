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
        set
        { 
            _isActive = value; 

            if (_isActive)
            {
                Time.timeScale /= _params.Get("TurboSpeedMultiplyer");
                _params.Set("TankSpeed", _params.Get("TankSpeed") * _params.Get("TurboSpeedMultiplyer"));
                _params.Set("TurretRotateSpeed",_params.Get("TurretRotateSpeed") * _params.Get("TurboSpeedMultiplyer"));
                _params.Set("TankTurnSpeed",_params.Get("TankTurnSpeed") * _params.Get("TurboSpeedMultiplyer"));
            }
            else
            {
                Time.timeScale *= _params.Get("TurboSpeedMultiplyer");
                _params.Set("TankSpeed", _params.Get("TankSpeed") / _params.Get("TurboSpeedMultiplyer"));
                _params.Set("TurretRotateSpeed",_params.Get("TurretRotateSpeed") / _params.Get("TurboSpeedMultiplyer"));
                _params.Set("TankTurnSpeed",_params.Get("TankTurnSpeed") / _params.Get("TurboSpeedMultiplyer"));
            }
        }
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
        if (Input.GetKeyDown(Key))
        {
            IsActive = !IsActive;
        }

        if (IsActive)
        {
            if (_params.Get("Energy") > 0.0F)
            {
                _params.Add("Energy", -CostPerSec * Time.deltaTime);

            }
            else
            {
                IsActive = false;
            }
        }
    }
    // ======================================================================================================================================== //
}
