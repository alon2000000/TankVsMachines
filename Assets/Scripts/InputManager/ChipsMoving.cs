using UnityEngine;
using System.Collections;

public class ChipsMoving : MonoBehaviour 
{
    private GameObject _objectHandledByMouse = null;
    private enum HandledObjectState
    {
        NONE,
        DRAGGED,
        LIFTED
    }
    private HandledObjectState _objectState = HandledObjectState.NONE;
    private Vector3? _mouseDownPosition = null;

    // ======================================================================================================================================== //
	void Start () 
    {
	
	}
    // ======================================================================================================================================== //
	void LateUpdate () 
    {
        // MOUSE DOWN
        if (Input.GetMouseButtonDown (0)) 
        {
            if (_objectHandledByMouse == null)
            {
                RaycastHit2D hit = Physics2D.Raycast(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y), Vector2.zero, 0f);
                if (hit.collider != null)
                {
                    if (hit.collider.gameObject.GetComponent<Chip>() != null)
                    {
                        if (hit.collider.gameObject.GetComponent<Chip>().State != Chip.ChipState.ON_GROUND)
                        {
                            _mouseDownPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                            _objectHandledByMouse = hit.collider.gameObject;
                            _objectState = HandledObjectState.DRAGGED;
                        }
                    }
                }
            }
            else
            {
                if (_objectState == HandledObjectState.LIFTED) 
                {
                    _objectHandledByMouse = null;
                    _objectState = HandledObjectState.NONE;
                    _mouseDownPosition = null;
                }
            }
        }

        // MOUSE UP
        if (Input.GetMouseButtonUp (0)) 
        {
            if (_objectHandledByMouse != null) 
            {
                if (_mouseDownPosition == Camera.main.ScreenToWorldPoint (Input.mousePosition)) 
                {
                    _objectState = HandledObjectState.LIFTED;
                } 
                else 
                {
                    if (_objectState == HandledObjectState.DRAGGED) 
                    {
                        _objectHandledByMouse = null;
                        _objectState = HandledObjectState.NONE;
                        _mouseDownPosition = null;
                    }
                }
            }
        }

        // MOVE HANDLED OBJECT
        if (_objectHandledByMouse != null) 
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Mathf.Abs(_objectHandledByMouse.transform.position.x - Camera.main.transform.position.x);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            _objectHandledByMouse.transform.position = worldPos;
        }
	}
    // ======================================================================================================================================== //
}
