using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    #region InputHandling
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DetectHeroClick();
        }
    }
    private void DetectHeroClick(Vector2? touchPosition = null)
    {
        Vector2 inputPosition;
        if (touchPosition == null)
        {
            inputPosition = Input.mousePosition;
        }
        else
        {
            inputPosition = touchPosition.Value;
        }

        Ray ray = Camera.main.ScreenPointToRay(inputPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Hero hero = hit.collider.GetComponentInParent<Hero>();  // Detect the Hero component in the parent object

            if (hero != null)
            {
                BattleManager.Instance.SelectHeroForAttack(hero);
            }
        }
    }
    

    #endregion
}
