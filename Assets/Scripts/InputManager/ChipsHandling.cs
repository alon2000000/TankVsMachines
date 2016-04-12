using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ChipsHandling : MonoBehaviour 
{
    public GameObject ChipsBoardObj;

    private GameObject _objectHandledByMouse = null;

    private Vector3? _mouseDownPosition = null;

    private bool _isHandledChipCanBeSocketed = false;
    private bool _isHandledChipAboveSalvage = false;
    private List<GameObject> _hoveredBoardTiles = new List<GameObject>();

    public Text ChipDescriptionText;

    public TankParams TankParamsScript;

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
        showChipDescriptionInUI();
        salvageChips();
	}
    // ======================================================================================================================================== //
    private void dragChip()
    {
        // MOUSE DOWN
        if (Input.GetMouseButtonDown (0)) 
        {
            if (_objectHandledByMouse == null)
            {
                LayerMask layerMask = (1 << LayerMask.NameToLayer("LootInInventoryLayer"));
                RaycastHit2D hit = Physics2D.Raycast(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y), Vector2.zero, 0f, layerMask);
                if (hit.collider != null)
                {
                    //Debug.Log(hit.collider.gameObject.name);

                    if (hit.collider.gameObject.GetComponent<Chip>() != null)
                    {
                        //if (hit.collider.gameObject.GetComponent<Chip>().State != Chip.ChipState.ON_GROUND)
                        {
                            _mouseDownPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                            _objectHandledByMouse = hit.collider.gameObject;
                            unsocketChip();
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
                // if above salvage - destroy + add chips cash
                if (_isHandledChipAboveSalvage)
                {
                    Destroy(_objectHandledByMouse.gameObject);
                    TankParamsScript.CashChips += 5; // TODO: change to be different in unique etc.
                }

                // if on legal board tiles - socket!
                if (_isHandledChipCanBeSocketed)
                {
                    // insert chip to all the hovered tiles
                    foreach (GameObject boardTile in _hoveredBoardTiles)
                    {
                        boardTile.GetComponent<BoardTile>().SocketedChip = _objectHandledByMouse;
                    }
                    // place the chip on the first tile (the top left)
                    socketChip();
                }

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
            LayerMask layerMask = (1 << LayerMask.NameToLayer("LootInInventoryLayer"));
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y), Vector2.zero, 0f, layerMask);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.GetComponent<Chip>() != null)
                {
                    if (hit.collider.gameObject.GetComponent<Chip>().State == Chip.ChipState.IN_BAG)
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

        Vector3 minPoint = _objectHandledByMouse.GetComponent<SpriteRenderer>().bounds.min; // bottom-left point
        Vector3 maxPoint = _objectHandledByMouse.GetComponent<SpriteRenderer>().bounds.max; // top-right point
        Vector3 firstRayPoint = new Vector3(minPoint.x, maxPoint.y, maxPoint.z) + new Vector3(16.0F / 100.0F, -16.0F / 100.0F); // top-left + 16 to center of chip-unit

        int rows = Mathf.RoundToInt(Mathf.Abs(minPoint.x - maxPoint.x) * 100) / 32;
        int cols = Mathf.RoundToInt(Mathf.Abs(minPoint.y - maxPoint.y) * 100) / 32;

        _isHandledChipCanBeSocketed = true;
        _hoveredBoardTiles.Clear();

        for (int i = 0; i < rows; ++i)
        {
            for (int j = 0; j < cols; ++j)
            {
                Vector3 rayPoint;
                rayPoint = firstRayPoint + new Vector3(i * 32.0F / 100.0F, -j * 32.0F / 100.0F);

                LayerMask layerMask = (1 << LayerMask.NameToLayer("InventoryLayer"));
                RaycastHit2D hit = Physics2D.Raycast(rayPoint, Vector2.zero, 0f, layerMask);
                if (hit.collider != null)
                {
                    if (hit.collider.gameObject.tag == "BoardTile")
                    {
                        if (hit.collider.gameObject.GetComponent<BoardTile>().SocketedChip == null)
                        {
                            hit.collider.gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
                            _hoveredBoardTiles.Add(hit.collider.gameObject);
                        }
                        else
                        {
                            _isHandledChipCanBeSocketed = false;
                        }
                    }
                    else
                    {
                        _isHandledChipCanBeSocketed = false;
                    }
                }
                else
                {
                    _isHandledChipCanBeSocketed = false;
                }
            }
        }
    }
    // ======================================================================================================================================== //
    private void socketChip()
    {
        Vector3 minPoint = _objectHandledByMouse.GetComponent<SpriteRenderer>().bounds.min;
        Vector3 maxPoint = _objectHandledByMouse.GetComponent<SpriteRenderer>().bounds.max;

        int rows = Mathf.RoundToInt(Mathf.Abs(minPoint.x - maxPoint.x) * 100) / 32;
        int cols = Mathf.RoundToInt(Mathf.Abs(minPoint.y - maxPoint.y) * 100) / 32;

        _objectHandledByMouse.transform.position = _hoveredBoardTiles[0].transform.position + new Vector3((rows-1) * 16.0F / 100.0F, -(cols-1) * 16.0F / 100.0F);

        TankParam chipBonus = _objectHandledByMouse.GetComponent<Chip>().ChipBonus;
        TankParamsScript.AddBonus2Param(chipBonus.Name, chipBonus.Bonus);
        TankParamsScript.AddPercentBonus2Param(chipBonus.Name, chipBonus.PercentBonus);

        // change state
        _objectHandledByMouse.GetComponent<Chip>().State = Chip.ChipState.IN_BOARD;
    }
    // ======================================================================================================================================== //
    private void unsocketChip()
    {
        bool isUnsocked = false;

        // remove chip from all tiles (if socketed)
        foreach (Transform child in ChipsBoardObj.transform)
        {
            if (child.gameObject.GetComponent<BoardTile>().SocketedChip == _objectHandledByMouse)
            {
                isUnsocked = true;
                child.gameObject.GetComponent<BoardTile>().SocketedChip = null;
            }
        }

        // if unsocked - remove chip effects
        if (isUnsocked)
        {
            TankParam chipBonus = _objectHandledByMouse.GetComponent<Chip>().ChipBonus;
            TankParamsScript.AddBonus2Param(chipBonus.Name, -chipBonus.Bonus);
            TankParamsScript.AddPercentBonus2Param(chipBonus.Name, -chipBonus.PercentBonus);

            // change state
            _objectHandledByMouse.GetComponent<Chip>().State = Chip.ChipState.IN_BAG;
        }
    }
    // ======================================================================================================================================== //
    private void showChipDescriptionInUI()
    {
        ChipDescriptionText.text = "";

        LayerMask layerMask = (1 << LayerMask.NameToLayer("LootInInventoryLayer"));
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y), Vector2.zero, 0f, layerMask);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.GetComponent<Chip>() != null)
            {
                TankParam chipBonus = hit.collider.gameObject.GetComponent<Chip>().ChipBonus;

                ChipDescriptionText.text += chipBonus.Name + ": ";
                ChipDescriptionText.text += chipBonus.Bonus > 0.0F ? "+" : "";
                ChipDescriptionText.text += chipBonus.Bonus.ToString() + ", ";
                ChipDescriptionText.text += chipBonus.PercentBonus.ToString() + "%";
            }
        }
    }
    // ======================================================================================================================================== //
    private void salvageChips()
    {
        if (_objectHandledByMouse == null)
            return;

        _objectHandledByMouse.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        _isHandledChipAboveSalvage = false;

        LayerMask layerMask = (1 << LayerMask.NameToLayer("UI"));
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y), Vector2.zero, 0f, layerMask);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "SalvageChips")
            {
                _objectHandledByMouse.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                _isHandledChipAboveSalvage = true;
            }
        }
    }
    // ======================================================================================================================================== //
}
