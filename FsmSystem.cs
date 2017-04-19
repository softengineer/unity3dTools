using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  A Fsm system for a game object in different state to transfer
///  @Author : Davoid Fan
///  @Date   : 5 Apr 2017
/// </summary>
public class FsmSystem <T> {

	Dictionary<T, FsmState<T>> map = new Dictionary<T, FsmState<T>> ();
	FsmState<T> _current = null;

	public FsmState<T> current{
		get{ 
			return _current;
		}
	}

	public void addState(T id, FsmState<T> state) {
		if (!map.ContainsKey (id))
			map.Add (id, state);
	}

	public void init(T id) {

		_current = map[id];
	}
		

	public void tick(Dictionary<string, string> map) {
		if (_current != null) {
			TransactionData<T> data = _current.action (map);

			if (data.id.ToString() != _current.Id.ToString()) {
				//Debug.Log ("Transcation is triggered!" + data.data);
				this.performTransation (data.id, data);
			}
		}
	}

	public void remove(T id) {
		FsmState<T> tmp = map [id];
		map.Remove (id);
		if (tmp == _current)
			_current = null;
	}

	public void performTransation(T newStateID, TransactionData<T> data) {
		this._current = map[newStateID];
		this._current.init (data.data);
	}
}
	
public class TransactionData<T> {
	public T id;
	public Dictionary<string, string> data = null;

	public TransactionData(T id) {
		this.id = id;
	}

	public TransactionData(T id, Dictionary<string, string> map) {
		this.id = id;
		this.data = map;
	}
}

public abstract class FsmState<T> {
	T _id;
	public T Id{
		get {
			return _id;
		}
	}

	public FsmState(T id) {
		this._id = id;
	}	

	public virtual void init(Dictionary<string, string> data) {
		Debug.Log ("Default init is called!");
	}

	public abstract TransactionData<T> action(Dictionary<string, string> obj);
}
 