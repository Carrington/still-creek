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
		private IObservable<int> ticks;
		private IObservable<Clock> clock;
		private IObservable<EngineDate> dates;

		public CommunityManager (IObservable<int> ticks, IObservable<Clock> clock, IObservable<EngineDate> dates)
		{
			this.ticks = ticks;
			this.clock = clock;
			this.dates = dates;
		}

		public IObservable<CommunityEvent> CommunityStream () {
			return Observable.Create<CommunityEvent> (observer => {

				//CommunityMember is struct of observables
				Array<IObservable<CommunityMember>> = this.SetupCommunityMembers();

				var datesSubscription = this.dates.Subscribe(date => {
					//Check something to see if it's a festival
				},
				observer.OnComplete,
				observer.OnError);

				return Disposable.Create(() => {
					dateSubscription.dispose();
				});
			});
		}

		private Array<IObservable<CommunityMember>> SetupCommunityMembers() {
      
		}
	}
}
