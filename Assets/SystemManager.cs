using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Managers {

	public class SystemManager : MonoBehaviour {
		private Dictionary<string, IManager> managers { get; set; }

		//the streamsbank should be an ObservableCollection (of observables!) and fire to interested subscribers whenever a new stream is registered as provided...
		
		void Awake() 
		{
			DontDestroyOnLoad(transform.gameObject);
		}

		void Start() 
		{
			//TODO finish this so Managers (and therefore streams) are discoverable and added to the streamsbank dynamically
			var type = typeof(IManager);
			var managerClasses = AppDomain.CurrentDomain.GetAssemblies ().SelectMany (s => s.GetTypes ()).Where (p => type.IsAssignableFrom (p) && !p.IsInterface);

			//Make conversion factor configuration based
			managers.Add (Managers.TimeManager.managerKey, new Managers.TimeManager (35.0f));
			managers.Add (Managers.ClockManager.managerKey, new Managers.ClockManager ());
		}

		private void RegisterStreams()
		{

		}
	}
 
}