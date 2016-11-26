using System;
using System.IO;
using System.Linq;
using UniRx;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Managers;


namespace Entities
{
	public class CommunityMember
	{
		private readonly int initTolerance;
		private readonly int[] initAttractionScale;
		private readonly int initTrust;
		private readonly IObservable<long> ticks;
		private readonly IObservable<Clock> clock;
		private readonly IObservable<EngineDate> calendar;

		public readonly string name;
		public readonly string descriptor;
		public readonly string dialogueName;

		public readonly IObservable<int> tolerance;
		public readonly IObservable<int[]> attractionScale;
		public readonly IObservable<int> trust;

		public IDisposable tickSubscription;
		public IDisposable clockSubscription;
		public IDisposable calendarSubscription;

		public CommunityMember (IObservable<long> ticks, IObservable<Clock> clock, IObservable<EngineDate> calendar, string fileLocation)
		{
			using (StreamReader file = File.OpenText (fileLocation))
			using (JsonTextReader reader = new JsonTextReader (file)) {
				JObject characterManifest = (JObject)JToken.ReadFrom(reader);
				this.name = (string)characterManifest["name"];
				this.dialogueName = (string)characterManifest["dialogueName"];
				this.descriptor = (string)characterManifest["descriptor"];
				this.initTolerance = (int)characterManifest["tolerance"];
				JArray intermediate = (JArray)characterManifest["attractionScale"];
				this.initAttractionScale = intermediate.Select(c => (int)c).ToArray();
				this.initTrust = (int)characterManifest["trust"];
			}


			this.ticks = ticks;
			this.clock = clock;
			this.calendar = calendar;
		}
	}
}