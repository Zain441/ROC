﻿using Invector.vShooter;
using UnityEngine;
namespace Invector.vItemManager
{
    [vClassHeader("Shooter Equipment", openClose = false, useHelpBox = true,helpBoxText ="This is a link for ItemManager")]
    public class vShooterEquipment : vMonoBehaviour, vIEquipment
    {

        vShooterWeapon _weapon;
        bool withoutWeapon;

      
        private vItem item;
        public vShooterWeapon weapon
        {
            get
            {
                if (!_weapon && !withoutWeapon)
                {
                    _weapon = GetComponent<vShooterWeapon>();
                    if (!_weapon) withoutWeapon = true;
                }

                return _weapon;
            }
        }

        public void OnEquip(vItem item)
        {           
            if (!weapon) return;
            this.item = item;
            weapon.changeAmmoHandle = new vShooterWeapon.ChangeAmmoHandle(ChangeAmmo);
            weapon.checkAmmoHandle = new vShooterWeapon.CheckAmmoHandle(CheckAmmo);
            var damageAttribute = item.GetItemAttribute(weapon.isSecundaryWeapon ? vItemAttributes.SecundaryDamage : vItemAttributes.Damage);
            if (damageAttribute != null)
            {
                weapon.maxDamage = damageAttribute.value;
            }
            if (weapon.secundaryWeapon)
            {
                var _equipments = weapon.secundaryWeapon.GetComponents<vIEquipment>();
                for (int i = 0; i < _equipments.Length; i++)
                {
                    if (_equipments[i] != null) _equipments[i].OnEquip(item);
                }
            }
        }

        private bool CheckAmmo(ref bool isValid, ref int totalAmmo)
        {
            if (!item) return false;
            var damageAttribute = item.GetItemAttribute(weapon.isSecundaryWeapon ? vItemAttributes.SecundaryAmmoCount : vItemAttributes.AmmoCount);
            isValid = damageAttribute != null && !damageAttribute.isBool;
            if (isValid) totalAmmo = damageAttribute.value;
            return isValid && damageAttribute.value > 0;
        }

        private void ChangeAmmo(int value)
        {
            if (!item) return;
            var damageAttribute = item.GetItemAttribute(weapon.isSecundaryWeapon ? vItemAttributes.SecundaryAmmoCount : vItemAttributes.AmmoCount);
            if (damageAttribute != null)
            {
                damageAttribute.value += value;
            }
        }

        public void OnUnequip(vItem item)
        {
            if (!weapon) return;
            if (!item) return;
            weapon.changeAmmoHandle = null;
            weapon.checkAmmoHandle = null;
            if (weapon.secundaryWeapon)
            {
                var _equipments = weapon.secundaryWeapon.GetComponents<vIEquipment>();
                for (int i = 0; i < _equipments.Length; i++)
                {
                    if (_equipments[i] != null) _equipments[i].OnUnequip(item);
                }
            }          
        }
    }

}
