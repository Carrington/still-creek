using UniRx;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Managers {

	public enum CommunityEventType
	{
		foo
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
				observer.OnNext(new CommunityEvent(0));
				return Disposable.Create(() => {});
			});
		}
	}
}