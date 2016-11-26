using UniRx;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using Entities;

namespace Managers {

	public enum CommunityEventType
	{
		Arrival, Departure, Death, Birth, Relationship, Festival
	}

	public struct CommunityEvent
	{
		public readonly CommunityEventType EventType;

		public CommunityEvent (CommunityEventType EventType)
		{
			this.EventType = EventType;
		}
	}

	public class CommunityManager
	{
		private IObservable<long> ticks;
		private IObservable<Clock> clock;
		private IObservable<EngineDate> dates;

		public CommunityManager (IObservable<long> ticks, IObservable<Clock> clock, IObservable<EngineDate> dates, string communityIdentifier)
		{
			Debug.Log("Loaded CommunityManager");
			//use community identifier to get the manifest
			this.SetupCommunityMembers("town");

			this.ticks = ticks;
			this.clock = clock;
			this.dates = dates;
		}

		public IObservable<CommunityEvent> CommunityStream () 
		{
			return Observable.Create<CommunityEvent> (observer => {
						
				var datesSubscription = this.dates.Subscribe(date => {
					//Check something to see if it's a festival
				},
				observer.OnError,
				observer.OnCompleted);

				return Disposable.Create(() => {
					datesSubscription.Dispose();
				});
			});
		}

		private Dictionary<string, CommunityMember> SetupCommunityMembers (string communityIdentifier)
		{
			Dictionary<string, CommunityMember> dictionary = new Dictionary<string, CommunityMember> ();

			if (communityIdentifier == "town") {

				CommunityMember adeleen = new CommunityMember(this.ticks, this.clock, this.dates, "/home/luciusagatho/still-creek-data/characters/town/adeleen.json");

				return new IObservable<CommunityMember>[] { adeleen };
			}

			return new IObservable<CommunityMember>[0];
		}
	}
}
