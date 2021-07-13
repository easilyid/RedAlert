﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SoldierFactory:ICharacterFactory
{
    public ICharacter CreateCharacter<T>(WeaponType weaponType, Vector3 spawnPosition, int lv = 1)where T:ICharacter,new()
    {
        ICharacter character = new T();

        string name = "";
        int maxHP = 0;
        float moveSpeed = 0;
        string iconSprite = "";
        string prefabName = "";
        System.Type t = typeof(T);
        if (t==typeof(SoldierCaptain))
        {
            name = "上尉士兵";
            maxHP = 100;
            moveSpeed = 3;
            iconSprite = "CaptainIcon";
            prefabName = "Soldier1";
        }
        else if (t==typeof(SoldierSergeant))
        {
            name = "中士士兵";
            maxHP = 90;
            moveSpeed = 3;
            iconSprite = "SergeantIcon";
            prefabName = "Soldier3";

        }else if (t==typeof(SoldierRookie))
        {
            name = "新手士兵";
            maxHP = 80;
            moveSpeed = 3;
            iconSprite = "SergeantIcon";
            prefabName = "Soldier2";

        }
        else
        {
            Debug.LogError("类型"+t+"不属于ISoldier,无法创建战士");
            return null;
        }

        ICharacterAttr attr = new SoldierAttr(new SoldierAttrStrategy(), lv, name, maxHP, moveSpeed, iconSprite, prefabName);
        character.attr = attr;

        //创建角色物体
        //加载   实例化
        //添加武器
        GameObject characterGO = FactoryManager.assetFactory.LoadSoldier(prefabName);
        characterGO.transform.position = spawnPosition;
        character.gameobject = characterGO;

        IWeapon weapon = FactoryManager.weaponFactory.CreateWeapon(weaponType);
        character.weapon = weapon;
        return character;
    }
}

