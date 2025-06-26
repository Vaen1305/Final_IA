using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCave : Item
{
    public float hideDuration = 5f;
    public Transform IntroCavePosition;
    public Transform OutCavePosition;
    private void Start()
    {
        this.LoadComponent();
    }
    public override void LoadComponent()
    {
        base.LoadComponent();
    }
    public void SeekCharacter(Health npc)
    {
        StartCoroutine(HideForDuration(npc));
    }

    private IEnumerator HideForDuration(Health npc)
    {
        Debug.Log($"{npc.name} se está escondiendo...");
        //npc.IfCanView = false;
        //npc.gameObject.SetActive(false);
        yield return new WaitForSeconds(hideDuration);
        Debug.Log($"{npc.name} ha salido de su escondite.");
        //npc.IfCanView = true;
        //npc.gameObject.SetActive(true);
    }
}
