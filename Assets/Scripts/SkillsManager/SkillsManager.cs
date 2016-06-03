using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SkillsManager : MonoBehaviour 
{
    public GameObject BoardObj;

    public GameObject Skill1;
    public GameObject Skill2;
    public GameObject Skill3;
    public GameObject Skill4;
    public GameObject Skill5;
    public GameObject Skill6;
    public GameObject Skill7;
    public GameObject Skill8;
    public GameObject Skill9;
    public GameObject Skill0;

    private List<GameObject> _skills = new List<GameObject>();
    // ======================================================================================================================================== //
	void Start () 
    {
        _skills.Add(Skill1);
        _skills.Add(Skill2);
        _skills.Add(Skill3);
        _skills.Add(Skill4);
        _skills.Add(Skill5);
        _skills.Add(Skill6);
        _skills.Add(Skill7);
        _skills.Add(Skill8);
        _skills.Add(Skill9);
        _skills.Add(Skill0);

        RearrangeSkills();
    }
    // ======================================================================================================================================== //
	void Update () 
    {
	
	}
    // ======================================================================================================================================== //
    public void RearrangeSkills()
    {
        clearSkills();

        List<int> tempSkillsList = new List<int>();
        int tempIndex = 0;

        // move over all tiles
        foreach (Transform tile in BoardObj.GetComponent<Board>().Tiles)
        {
            // if socket not null
            GameObject socketedChip = tile.GetComponent<BoardTile>().SocketedChip;
            if (socketedChip != null)
            {
                // if chip is skill chip
                Loot chipScript = socketedChip.GetComponent<Loot>();
                if (chipScript.Type == Loot.LootType.SKILL_CHIP)
                {
                    // if not exists in temp list
                    if (!tempSkillsList.Contains((int)chipScript.ChipLogo))
                    {
                        // add to temp list
                        tempSkillsList.Add((int)chipScript.ChipLogo);

                        // add to _skills
                        _skills[tempIndex].GetComponent<Image>().color = Color.blue;
                        _skills[tempIndex].transform.FindChild("SkillImage").gameObject.SetActive(true);
                        _skills[tempIndex].transform.FindChild("SkillImage").GetComponent<Image>().sprite = chipScript.Logo.GetComponent<SpriteRenderer>().sprite;
                        chipScript.SkillChildObject.SetActive(true);
                        tempIndex++;
                    }
                }
            }
        }
    }
    // ======================================================================================================================================== //
    private void clearSkills()
    {
        foreach (GameObject skill in _skills)
        {
            skill.GetComponent<Image>().color = Color.black;
            skill.transform.FindChild("SkillImage").gameObject.SetActive(false); // hide the image - TODO... maybe not good

            // move over all tiles
            foreach (Transform tile in BoardObj.GetComponent<Board>().Tiles)
            {
                // if socket not null
                GameObject socketedChip = tile.GetComponent<BoardTile>().SocketedChip;
                if (socketedChip != null)
                {
                    // if chip is skill chip
                    Loot chipScript = socketedChip.GetComponent<Loot>();
                    if (chipScript.Type == Loot.LootType.SKILL_CHIP)
                    {
                        // deactive the skill child object
                        chipScript.SkillChildObject.SetActive(false);
                    }
                }
            }
        }
    }
    // ======================================================================================================================================== //
    private KeyCode getKeyByNumber(int number)
    {
        return (KeyCode)(48 + number); // 48 is the number of KeyCode.Alpha0
        /*if (number == 0)
            return KeyCode.Alpha0;
        if (number == 1)
            return KeyCode.Alpha1;
        if (number == 2)
            return KeyCode.Alpha2;
        if (number == 3)
            return KeyCode.Alpha3;
        if (number == 4)
            return KeyCode.Alpha4;
        if (number == 5)
            return KeyCode.Alpha5;
        if (number == 6)
            return KeyCode.Alpha6;
        if (number == 7)
            return KeyCode.Alpha7;
        if (number == 8)
            return KeyCode.Alpha8;*/
    }
    // ======================================================================================================================================== //
}
