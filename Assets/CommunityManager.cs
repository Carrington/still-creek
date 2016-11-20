using UniRx;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Managers {
	public class CommunityManager
	{
		private IObservable<int> ticks;
		private IObservable<Clock> clock;
		private IObservable<EngineDate> dates;

		public CommunityManager(IObservable<int> ticks, IObservable<Clock> clock, IObservable<Calendar>)
		{
			
		}
	}
}