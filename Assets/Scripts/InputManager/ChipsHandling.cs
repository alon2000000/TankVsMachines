using UnityEngine;
using System.Collections;

public class ChipsHandling : MonoBehaviour 
{
    private GameObject _objectHandledByMouse = null;

    private Vector3? _mouseDownPosition = null;

    // ======================================================================================================================================== //
	void Start () 
    {
	
	}
    // ======================================================================================================================================== //
	void LateUpdate () 
    {
        dragChip();
        draggedChipHoverOverBoard();
        rotateChipUpdate();
	}
    // ======================================================================================================================================== //
    private void dragChip()
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
                        }
                    }
                }
            }
        }

        // MOUSE UP
        if (Input.GetMouseButtonUp (0)) 
        {
            if (_objectHandledByMouse != null) 
            {
                _objectHandledByMouse = null;
                _mouseDownPosition = null;
            }
        }

        // MOVE HANDLED OBJECT
        if (_objectHandledByMouse != null) 
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 5.0F;//Mathf.Abs(_objectHandledByMouse.transform.position.x - Camera.main.transform.position.x);
            //Debug.Log(mousePos.z);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            _objectHandledByMouse.transform.position = worldPos;
        }
    }
    // ======================================================================================================================================== //
    private void rotateChipUpdate()
    {
        if (Input.GetMouseButtonDown(1)) // check right click
        {
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y), Vector2.zero, 0f);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.GetComponent<Chip>() != null)
                {
                    if (hit.collider.gameObject.GetComponent<Chip>().State != Chip.ChipState.ON_GROUND)
                    {
                        hit.collider.gameObject.transform.Rotate(new Vector3(180.0F, 0.0F, 90.0F));
                    }
                }
            }
        }
    }
    // ======================================================================================================================================== //
    // raycast on board to mark tiles
    private void draggedChipHoverOverBoard()
    {
        if (_objectHandledByMouse == null)
            return;

        BoxCollider2D chipCollider = _objectHandledByMouse.GetComponent<BoxCollider2D>();
        Sprite chipSprite = _objectHandledByMouse.GetComponent<SpriteRenderer>().sprite;

        float f1 = chipCollider.size.x;
        float f2 = chipCollider.size.y;

        float chipWidth = chipSprite.bounds.size.x * chipSprite.pixelsPerUnit;
        float chipHeight = chipSprite.bounds.size.y * chipSprite.pixelsPerUnit;
        Debug.Log(chipWidth + "," + chipHeight + "," + f1 + "," + f2);
    }
    // ======================================================================================================================================== //
}
