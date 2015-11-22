using UnityEngine;
using System.Collections;

public class EventManager
{
	public delegate void GameEvent(object sender, GameEventArgs args);


    public static event GameEvent OnNPC;
    public static void NotifyNPC(object sender, GameEventArgs args)
    {
        if (OnNPC != null)
        {
            OnNPC(sender, args); 
        }
    }

    public static event GameEvent OnSpaceBar;
    public static void NotifySpaceBar(object sender, GameEventArgs args)
    {
        if (OnSpaceBar != null)
            OnSpaceBar(sender, args);
    }
	
	public static event GameEvent OnDialogChoiceMade;
	public static void NotifyDialogChoiceMade(object sender, GameEventArgs args)
	{
		if (OnDialogChoiceMade != null)
			OnDialogChoiceMade(sender, args);
	}
	
	public static event GameEvent OnItemPickup;
    public static void NotifyItemTaken(object sender, GameEventArgs args)
    {
        if (OnItemPickup != null)
            OnItemPickup(sender, args);
    }

}
