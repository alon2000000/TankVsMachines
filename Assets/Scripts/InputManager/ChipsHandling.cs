using UnityEngine;
using System.Collections;

public class ChipsHandling : MonoBehaviour 
{
    public GameObject ChipsBoardObj;

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
        // all tiles to white
        foreach (Transform child in ChipsBoardObj.transform)
        {
            child.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }

        if (_objectHandledByMouse == null)
            return;

        BoxCollider2D chipCollider = _objectHandledByMouse.GetComponent<BoxCollider2D>();
        Sprite chipSprite = _objectHandledByMouse.GetComponent<SpriteRenderer>().sprite;

        Vector3 minPoint = _objectHandledByMouse.GetComponent<SpriteRenderer>().bounds.min;
        Vector3 maxPoint = _objectHandledByMouse.GetComponent<SpriteRenderer>().bounds.max;
        Vector3 rayPoint = new Vector3(minPoint.x, maxPoint.y, maxPoint.z);

        float chipWidth = chipSprite.bounds.size.x * chipSprite.pixelsPerUnit;
        float chipHeight = chipSprite.bounds.size.y * chipSprite.pixelsPerUnit;


        /*LayerMask layerMask = (1 << LayerMask.NameToLayer("TankLayer"));
        layerMask |= (1 << LayerMask.NameToLayer("LootOnGroundLayer"));
        layerMask |= (1 << LayerMask.NameToLayer("LootInInventoryLayer"));*/

        LayerMask layerMask = (1 << LayerMask.NameToLayer("LootInInventoryLayer"));
        layerMask = ~layerMask;



        int rows = Mathf.RoundToInt(Mathf.Abs(minPoint.x - maxPoint.x) * 100) / 32;
        int cols = Mathf.RoundToInt(Mathf.Abs(minPoint.y - maxPoint.y) * 100) / 32;

        for (int i = 0; i < rows; ++i)
        {
            for (int j = 0; j < cols; ++j)
            {
                rayPoint += new Vector3(i * 32.0F / 100.0F, -j * 32.0F / 100.0F);

                RaycastHit2D hit = Physics2D.Raycast(rayPoint, Vector2.zero, 0f, layerMask);
                if (hit.collider != null)
                {
                    //Debug.Log("HIT! " + hit.collider.gameObject.name);
                    if (hit.collider.gameObject.tag == "BoardTile")
                    {
                        //Debug.Log("HIT TILE!");
                        hit.collider.gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
                    }
                }
            }
        }
    }
    // ======================================================================================================================================== //
}
