using UnityEngine;
using System.Collections;

public class EventManager
{
	public delegate void GameEvent(object sender, GameEventArgs args);
	public static event GameEvent OnDialogChoiceMade;
    /*public static event GameEvent OnUserHotspotTap;
	public static event GameEvent OnUserHotspotTapResult;
	public static event GameEvent OnActivateReplayButton;
	public static event GameEvent OnProgression;
	public static event GameEvent OnRestart;
	public static event GameEvent OnRoundEnds;*/


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
	

	public static void NotifyDialogChoiceMade(object sender, GameEventArgs args)
	{
		if (OnDialogChoiceMade != null)
			OnDialogChoiceMade(sender, args);
	}
	
	/*public static void NotifyUserHotspotTap(object sender, GameEventArgs args)
	{
		if (OnUserHotspotTap != null)
			OnUserHotspotTap(sender, args);
	}
	
	public static void NotifyUserHotspotTapResult(object sender, GameEventArgs args)
	{
		if (OnUserHotspotTapResult != null)
			OnUserHotspotTapResult(sender, args);
	}
	
	public static void NotifyActivateReplayButton(object sender, GameEventArgs args)
	{
		if (OnActivateReplayButton != null)
			OnActivateReplayButton(sender, args);
	}
	
	public static void NotifyProgression(object sender, GameEventArgs args)
	{
		if (OnProgression != null)
			OnProgression(sender, args);
	}
	
	public static void NotifyRestart(object sender, GameEventArgs args)
	{
		if (OnRestart != null)
            OnRestart(sender, args);
    }
    
	public static void NotifyEndRound(object sender, GameEventArgs args)
    {
        if (OnRoundEnds != null)
            OnRoundEnds(sender, args);
    }*/
}
