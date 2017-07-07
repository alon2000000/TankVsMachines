using UnityEngine;
using UnityEngine.UI;
using System;
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

    // list of icons in the gui of socketed skill chips
    private List<GameObject> _skillsIcons = new List<GameObject>();
    // list of scripts of socketed skill chips
    private List<Skill> _skills = new List<Skill>();

    // list of all skills scripts that the tank contains
    public List<Skill> TankSkills = new List<Skill>();
    // ======================================================================================================================================== //
	void Start () 
    {
        _skillsIcons.Add(Skill1);
        _skillsIcons.Add(Skill2);
        _skillsIcons.Add(Skill3);
        _skillsIcons.Add(Skill4);
        _skillsIcons.Add(Skill5);
        _skillsIcons.Add(Skill6);
        _skillsIcons.Add(Skill7);
        _skillsIcons.Add(Skill8);
        _skillsIcons.Add(Skill9);
        _skillsIcons.Add(Skill0);

        RearrangeSkills();
    }
    // ======================================================================================================================================== //
    void Update()
    {
        
    }
    // ======================================================================================================================================== //
	void LateUpdate () 
    {
        for (int i = 0; i < _skills.Count; ++i)
        {
            // matched skill icon of the current skill
            GameObject currentSkillIcon = _skillsIcons[i];

            // current skill script

            Skill currentSkill = _skills[i] as Skill;

            // not ready state
            if (currentSkill.State == SkillState.NOT_READY)
            {
                currentSkillIcon.transform.Find("SkillBackground").GetComponent<Image>().color = Color.gray;
            }
            // ready state
            else if (currentSkill.State == SkillState.READY)
            {
                currentSkillIcon.transform.Find("SkillBackground").GetComponent<Image>().color = Color.blue;
            }
            // action state
            else if (currentSkill.State == SkillState.ACTION)
            {
                currentSkillIcon.transform.Find("SkillBackground").GetComponent<Image>().color = Color.yellow;
                float ratio = currentSkill.ActionTime / currentSkill.MaxActionTime;
                currentSkillIcon.transform.Find("SkillBackground").transform.localScale = new Vector3(1.0F, ratio, 1.0F);
            }
            // cooldown state
            else if (currentSkill.State == SkillState.COOLDOWN)
            {
                currentSkillIcon.transform.Find("SkillBackground").GetComponent<Image>().color = Color.green;
                float ratio = 1.0F - currentSkill.Cooldown / currentSkill.MaxCooldown;
                currentSkillIcon.transform.Find("SkillBackground").transform.localScale = new Vector3(1.0F, ratio, 1.0F);
            }
            // failure state
            else if (currentSkill.State == SkillState.FAILURE)
            {
                currentSkillIcon.transform.Find("SkillBackground").GetComponent<Image>().color = Color.red;
            }
        }
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
                        _skillsIcons[tempIndex].transform.Find("SkillBackground").GetComponent<Image>().color = Color.blue;
                        _skillsIcons[tempIndex].transform.Find("SkillImage").gameObject.SetActive(true);
                        _skillsIcons[tempIndex].transform.Find("SkillImage").GetComponent<Image>().sprite = chipScript.Logo.GetComponent<SpriteRenderer>().sprite;
                        socketedChip.GetComponent<Skill>().IsActice = true;
                        int skillNumber = Convert.ToInt32(_skillsIcons[tempIndex].transform.Find("Text").GetComponent<Text>().text);

                        socketedChip.GetComponent<Skill>().Key = getKeyByNumber(skillNumber);

                        _skills.Add(socketedChip.GetComponent<Skill>());

                        tempIndex++;
                    }
                }
            }
        }
    }
    // ======================================================================================================================================== //
    private void clearSkills()
    {
        _skills.Clear();

        foreach (GameObject skillIcon in _skillsIcons)
        {
            skillIcon.transform.Find("SkillBackground").GetComponent<Image>().color = Color.black;
            skillIcon.transform.Find("SkillImage").gameObject.SetActive(false); // hide the image - TODO... maybe not good

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
                        socketedChip.GetComponent<Skill>().IsActice = false;

                        socketedChip.GetComponent<Skill>().Key = KeyCode.None;
                    }
                }
            }
        }
    }
    // ======================================================================================================================================== //
    private KeyCode getKeyByNumber(int number)
    {
        return (KeyCode)(48 + number); // 48 is the number of KeyCode.Alpha0
    }
    // ======================================================================================================================================== //
}
