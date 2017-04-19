using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FsmDemo : MonoBehaviour {

	private FsmSystem<StateID> fsm;



	// Use this for initialization
	void Start () 	{
 	MakeFSM ();
	}
	
	// Update is called once per frame
	void Update () {
		fsm.tick (obj);
	}


	private void MakeFSM()
	{
		fsm = new FsmSystem<StateID>();
		NormalMove crawl = new NormalMove(this);
		fsm.addState (StateID.CRAWL, crawl);
	
		StealMode steal = new StealMode(this);
		fsm.addState (StateID.STEAL, steal);

		fsm.init (StateID.CRAWL);
	}

}

public enum StateID {
	CRAWL = 1,
	STEAL = 2,
	USINGITEM = 3
}

public class NormalMove : FsmState<StateID>
{
	ClipCtl clip;


	public NormalMove(ClipCtl obj) : base(StateID.CRAWL) 
	{ 
	
	}

	public override void init(Dictionary<string, string> data) {
		if (data == null)
			return;
	}

	private void fillMap(int direction, Dictionary<string, string> dict)
	{
		
	}

	public override TransactionData<StateID> action(Dictionary<string,string> obj)
	{
		if (Input.GetKeyDown (KeyCode.Space) ) {
			return new TransactionData<StateID> (StateID.STEAL);
		}
			
		return new TransactionData<StateID> (this.Id);
	}

}


public class StealMode : FsmState<StateID>
{

	public StealMode(ClipCtl obj) : base(StateID.STEAL) 
	{ 
		clip = obj;
		_steal = GameObject.Find ("clip");
		}

	public override void init(Dictionary<string, string> data) {
	
	}



	public override TransactionData<StateID> action(Dictionary<string,string> obj)
	{
          return new TransactionData<StateID> (this.Id);
		
	}
}

