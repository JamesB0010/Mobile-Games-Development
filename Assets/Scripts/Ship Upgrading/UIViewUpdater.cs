using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIViewUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI costField, purchaseEquipButtonText;
    
    [SerializeField] private ScrollRect UpgradesScrollRect;

    public PlayerUpgradesState PlayerUpgradesState => this.playerUpgradesState;

    [SerializeField] private FloatReference playerMoney;
    
    [FormerlySerializedAs("playerWeaponsState")] [SerializeField] private PlayerUpgradesState playerUpgradesState;

    [Header("Current equipped stats setters")] [SerializeField]
    private Image EquippedSectionBackground;

    [FormerlySerializedAs("armourEquippedStatSectionView")] [FormerlySerializedAs("armourEquippedStatSection")] [SerializeField] private ArmourItemStatsView armourEquippedStatsView;

    [FormerlySerializedAs("energySystemEquippedStats")] [SerializeField] private EnergySystemItemStatsView energySystemEquippedStatsView;

    [FormerlySerializedAs("engineEquippedStats")] [SerializeField] private EngineItemStatsView engineEquippedStatsView;

    [FormerlySerializedAs("heavyGunEquippedStats")] [SerializeField] private HeavyGunItemStatsView heavyGunEquippedStatsView;

    [FormerlySerializedAs("lightGunEquippedStats")] [SerializeField] private LightGunItemStatsView lightGunEquippedStatsView;
    

    [Header("Item To Purchase Stats Setters")] 
    [SerializeField] private Image toPurchaseSectionBackground;
    
    [FormerlySerializedAs("armourItemStatSectionView")] [FormerlySerializedAs("armourToPurchaseStatSection")] [SerializeField] private ArmourItemStatsView armourItemStatsView;

    [FormerlySerializedAs("energySystemToPurchaseStats")] [SerializeField] private EnergySystemItemStatsView energySystemItemStatsView;

    [FormerlySerializedAs("engineToPurchaseStats")] [SerializeField] private EngineItemStatsView engineItemStatsView;

    [FormerlySerializedAs("heavyGunToPurchaseStats")] [SerializeField] private HeavyGunItemStatsView heavyGunItemStatsView;

    [FormerlySerializedAs("lightGunToPurchaseStats")] [SerializeField] private LightGunItemStatsView lightGunItemStatsView;

    
    private static UIViewUpdater instance = null;

    public static UIViewUpdater GetInstance()
    {
        if (instance == null)
        {
            Debug.LogError("Ui view Updater does not exist please add it in the editor");
            throw new NullReferenceException();
        }

        return instance;
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            return;
        }
        
        Destroy(this.gameObject);
    }

    public void ScrollRectTopAfterSeconds(float waitTime)
    {
        StartCoroutine(nameof(this.SetScrollVerticalAfterWait), waitTime);
    }
    private IEnumerator SetScrollVerticalAfterWait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        this.UpgradesScrollRect.verticalNormalizedPosition = 1;
    }

    public void SetItemStatsUi(LightGun gun)
    {
        Debug.Log("Update ui for light gun");
        this.DisableAllEquippedFields();
        this.EquippedSectionBackground.enabled = true;
        this.lightGunEquippedStatsView.gameObject.SetActive(true);

        if (!gun.AbleToShoot)
        {
            this.lightGunEquippedStatsView.WeaponCantShoot();
            return;
        }
        this.lightGunEquippedStatsView.SetFields(gun.ItemName, gun.TimeBetweenBullets.ToString(), gun.BulletDamage.ToString(), gun.EnergyExpensePerShot.ToString(), gun.Firepower.ToString());
    }

    private void DisableAllEquippedFields()
    {
        this.EquippedSectionBackground.enabled = false;
        this.armourEquippedStatsView.gameObject.SetActive(false);
        this.energySystemEquippedStatsView.gameObject.SetActive(false);
        this.engineEquippedStatsView.gameObject.SetActive(false);
        this.heavyGunEquippedStatsView.gameObject.SetActive(false);
        this.lightGunEquippedStatsView.gameObject.SetActive(false);
    }

    public void SetItemStatsUi(HeavyGun gun)
    {
        this.DisableAllEquippedFields();
        this.EquippedSectionBackground.enabled = true;
        this.heavyGunEquippedStatsView.gameObject.SetActive(true);
        Debug.Log("Update ui for heavy gun");
        if (!gun.AbleToShoot)
        {
            this.heavyGunEquippedStatsView.WeaponCantShoot();
            return;
        }
        
        this.heavyGunEquippedStatsView.SetFields(gun.ItemName, gun.TimeBetweenBullets.ToString(), gun.BulletDamage.ToString(), gun.MaxAmmoCount.ToString(), gun.Firepower.ToString());
    }

    public void SetItemStatsUi(Armour armour)
    {
        Debug.Log("Update ui for armour");
        
        this.DisableAllEquippedFields();
        this.EquippedSectionBackground.enabled = true;
        this.armourEquippedStatsView.gameObject.SetActive(true);
        
        this.armourEquippedStatsView.SetFields(armour.ItemName, armour.EnergyUsedPerHit.ToString(), armour.MinimumOperationalEnergyLevel.ToString(), (( 1 - armour.DamageDampeningMultiplier) * 100).ToString());
    }

    public void SetItemStatsUi(Engine engine)
    {
        Debug.Log("Update ui for engine");
        
        this.DisableAllEquippedFields();
        this.EquippedSectionBackground.enabled = true;
        this.engineEquippedStatsView.gameObject.SetActive(true); 
        
        this.engineEquippedStatsView.SetFields(engine.ItemName, engine.AccelerationSpeed.ToString(), engine.MaxVelocity.ToString(), engine.EnergyDrainRate.ToString());
    }

    public void SetItemStatsUi(EnergySystem energySystem)
    {
        Debug.Log("Update ui for energy system");
        
        this.DisableAllEquippedFields();
        this.EquippedSectionBackground.enabled = true;
        this.energySystemEquippedStatsView.gameObject.SetActive(true); 
        this.energySystemEquippedStatsView.SetFields(energySystem.ItemName, energySystem.MaxEnergy.ToString(), energySystem.RechargeRate.ToString());
    }

    public void CellSelected(UpgradeCell selectedCell)
    {
        this.costField.text = selectedCell.Upgrade.Cost.ToString();
        if (selectedCell.Upgrade.IsPurchaseable)
        {
            bool isEquipped = false;
            isEquipped = selectedCell.Upgrade.GenerateUpgradesStateInteractor(this.playerUpgradesState).IsEqualTo(selectedCell.Upgrade, selectedCell.WeaponIndex);

            bool ownedByDefault = selectedCell.Upgrade.quantityOwnedByDefault > OwnedUpgradesCounter.Instance.GetItemInCirculationCount(this.playerUpgradesState.DefaultLightGun);
            bool enoughOwned = OwnedUpgradesCounter.Instance.GetUpgradeCount(selectedCell.Upgrade) > 0;
            bool isOwned = ownedByDefault || enoughOwned|| isEquipped;
            if (isOwned)
            {
                if (isEquipped)
                    this.purchaseEquipButtonText.text = "Equipped";
                else
                    this.purchaseEquipButtonText.text = "Equip";
            }
            else
                this.purchaseEquipButtonText.text = "Purchase";
        }
        
        
        selectedCell.Upgrade.GenerateUiUpdatorInteractor(this).UpdateItemToPurchaseStats((ShipItem)selectedCell.Upgrade.GetUpgrade());
    }

    public void CellPurchased(SelectedCellHighlight highlight)
    {
        this.purchaseEquipButtonText.text = "Equipped";
        this.UpdateUiBasedOnItem(highlight.SelectedCell);
    }


    public void CellEquipped(SelectedCellHighlight highlight)
    {
        this.UpdateUiBasedOnItem(highlight.SelectedCell);
    }
    private void UpdateUiBasedOnItem(UpgradeCell cell)
    {
        ShipItem item = (ShipItem)cell.Upgrade.GetUpgrade();

        cell.Upgrade.GenerateUiUpdatorInteractor(this).UpdateUi(item);;

    }

    public void UpdateUiArmourToPurchase(string armourName, string energyUsedPerHit, string minEnergyReq, string shieldsStrength)
    {
        this.DisableAllToPurchaseFields();
        this.toPurchaseSectionBackground.enabled = true;
        this.armourItemStatsView.gameObject.SetActive(true);
        this.armourItemStatsView.SetFields(armourName, energyUsedPerHit, minEnergyReq, shieldsStrength);
    }

    public void UpdateUiEnergySystemToPurchase(string energySysName, string maxEnergy, string rechargeRate)
    {
        this.DisableAllToPurchaseFields();
        this.toPurchaseSectionBackground.enabled = true;
        this.energySystemItemStatsView.gameObject.SetActive(true);
        this.energySystemItemStatsView.SetFields(energySysName, maxEnergy, rechargeRate);
    }

    public void UpdateUiEngineToPurchase(string engineName, string accelerationSpeed, string topSpeed, string energyDrainRate, string boostEnergyDrainRate)
    {
        this.DisableAllToPurchaseFields();
        this.toPurchaseSectionBackground.enabled = true;
        this.engineItemStatsView.gameObject.SetActive(true);
        this.engineItemStatsView.SetFields(engineName, accelerationSpeed, topSpeed, energyDrainRate, boostEnergyDrainRate);
    }
    
    public void UpdateUiEngineToPurchase(string engineName, string accelerationSpeed, string topSpeed, string energyDrainRate)
    {
        this.DisableAllToPurchaseFields();
        this.toPurchaseSectionBackground.enabled = true;
        this.engineItemStatsView.gameObject.SetActive(true);
        this.engineItemStatsView.SetFields(engineName, accelerationSpeed, topSpeed, energyDrainRate);
    }

    public void HeavyWeaponToPurchaseIsNoGun()
    {
        this.DisableAllToPurchaseFields();
        this.toPurchaseSectionBackground.enabled = true;
        this.heavyGunItemStatsView.gameObject.SetActive(true);
        this.heavyGunItemStatsView.WeaponCantShoot();
    }

    public void LightWeaponToPurchaseIsNoGun()
    {
        this.DisableAllToPurchaseFields();
        this.toPurchaseSectionBackground.enabled = true;
        this.lightGunItemStatsView.gameObject.SetActive(true);
        this.lightGunItemStatsView.WeaponCantShoot();
    }

    public void UpdateHeavyWeaponToPurchase(string weaponName, string fireRate, string bulletDamage, string ammoCount, string firepower)
    {
        this.DisableAllToPurchaseFields();
        this.toPurchaseSectionBackground.enabled = true;
        this.heavyGunItemStatsView.gameObject.SetActive(true);
        this.heavyGunItemStatsView.SetFields(weaponName, fireRate, bulletDamage, ammoCount, firepower);
    }

    public void UpdateLightGunWeaponToPurchase(string weaponName, string fireRate, string bulletDamage, string energyExpense, string firepower)
    {
        this.DisableAllToPurchaseFields();
        this.toPurchaseSectionBackground.enabled = true;
        this.lightGunItemStatsView.gameObject.SetActive(true);
        this.lightGunItemStatsView.SetFields(weaponName, fireRate, bulletDamage, energyExpense, firepower);
    }

    private void DisableAllToPurchaseFields()
    {
        this.toPurchaseSectionBackground.enabled = false;
        this.armourItemStatsView.gameObject.SetActive(false);
        this.energySystemItemStatsView.gameObject.SetActive(false);
        this.engineItemStatsView.gameObject.SetActive(false);
        this.heavyGunItemStatsView.gameObject.SetActive(false);
        this.lightGunItemStatsView.gameObject.SetActive(false);
    }
    
    public IEnumerator DisableAllToPurchaseFieldsAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        this.DisableAllToPurchaseFields();
    }

    public void StartDisableAllToPurchaseFieldsCoroutine(float timeToWait)
    {
        StartCoroutine(nameof(this.DisableAllToPurchaseFieldsAfter), timeToWait);
    }
    public void CancelDisableAllToPurchaseFieldsCoroutine()
    {
        StopCoroutine(nameof(this.DisableAllToPurchaseFieldsAfter));
    }
}
