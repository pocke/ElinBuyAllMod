using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using UnityEngine;

namespace BuyAll;

[HarmonyPatch]
public class Patch
{
    [HarmonyPostfix, HarmonyPatch(typeof(UIInventory), "RefreshMenu")]
    public static void UIInventory_RefreshList_Postfix(UIInventory __instance)
    {
        var uiInv = __instance;
        var mode = uiInv.currentTab.mode;
		WindowMenu menuBottom = uiInv.window.menuBottom;

        if (mode == UIInventory.Mode.Buy)
        {
            menuBottom.AddButton("Buy All", delegate
            {
                foreach (var pair in uiInv.list.buttons)
                {
                    var button = pair.component as ButtonGrid;
                    var card = pair.obj as Card;
                    if (button == null || card == null)
                    {
                        continue;
                    }

                    var trans = new InvOwner.Transaction(button, card.Num);
                    var bought = trans.Process();
                    if (!bought)
                    {
                        break;
                    }
                }
            });
        }
    }
}
