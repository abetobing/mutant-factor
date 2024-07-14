using UnityEngine;

public class Weapon : MonoBehaviour
{
    private GameObject _weaponPrefab;
    public Transform weaponPlaceholder;

    private void Awake()
    {
        SwitchTo(_weaponPrefab);
    }

    public void SwitchTo(GameObject prefab)
    {
        if (prefab == null || weaponPlaceholder == null)
            return;

        _weaponPrefab = prefab;
        var weapon = Instantiate(_weaponPrefab, weaponPlaceholder);
        weapon.transform.localPosition = weaponPlaceholder.transform.localPosition;
        weapon.transform.localRotation = Quaternion.identity;
    }
}