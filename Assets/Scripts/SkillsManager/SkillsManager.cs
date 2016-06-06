﻿using UnityEngine;
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

    private List<GameObject> _skillsIcons = new List<GameObject>();
    private List<ISkill> _skills = new List<ISkill>();
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

            // cool down effect shown - size of background
            float ratio = 1.0F - _skills[i].Cooldown / _skills[i].MaxCooldown;
            currentSkillIcon.transform.FindChild("SkillBackground").transform.localScale = new Vector3(1.0F, ratio, 1.0F);

            // if skill not ready - gray it
            bool isSkillReady = _skills[i].IsReady;
            Color backgroundColor = isSkillReady ? Color.blue : Color.gray;
            currentSkillIcon.transform.FindChild("SkillBackground").GetComponent<Image>().color = backgroundColor;
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
                        _skillsIcons[tempIndex].transform.FindChild("SkillBackground").GetComponent<Image>().color = Color.blue;
                        _skillsIcons[tempIndex].transform.FindChild("SkillImage").gameObject.SetActive(true);
                        _skillsIcons[tempIndex].transform.FindChild("SkillImage").GetComponent<Image>().sprite = chipScript.Logo.GetComponent<SpriteRenderer>().sprite;
                        chipScript.SkillChildObject.SetActive(true);
                        int skillNumber = Convert.ToInt32(_skillsIcons[tempIndex].transform.FindChild("Text").GetComponent<Text>().text);

                        chipScript.SkillChildObject.GetComponent<ISkill>().Key = getKeyByNumber(skillNumber);

                        _skills.Add(chipScript.SkillChildObject.GetComponent<ISkill>());

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
            skillIcon.transform.FindChild("SkillBackground").GetComponent<Image>().color = Color.black;
            skillIcon.transform.FindChild("SkillImage").gameObject.SetActive(false); // hide the image - TODO... maybe not good

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

                        chipScript.SkillChildObject.GetComponent<ISkill>().Key = KeyCode.None;
                        //chipScript.SkillChildObject.GetComponent<ISkill>().Chip = null;
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
