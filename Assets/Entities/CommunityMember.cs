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
	//TODO a lot of this class is way more stateful than it should be for Rx
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

		// TODO this could be done better by using merged streams as the state of attached observable, instead of AttachBlahEvent
		public readonly ISubject<int> tolerance;
		public readonly ISubject<int[]> attractionScale;
		public readonly ISubject<int> trust;

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

			this.trust = new BehaviorSubject<int>(this.initTrust);
			this.tolerance = new BehaviorSubject<int>(this.initTolerance);
			this.attractionScale = new BehaviorSubject<int[]>(this.initAttractionScale);

			Debug.Log(string.Format("{0} loaded.", this.name));
		}

		public void AttachToleranceEvent (IObservable<int> toleranceEventStream)
		{
			var subscription = toleranceEventStream.Subscribe(toleranceChange =>
																{
																	this.tolerance.Subscribe(currentTolerance => {
																		this.tolerance.OnNext(currentTolerance + toleranceChange);
																	}).Dispose();
																});
		}

		public void AttachTrustEvent(IObservable<int> trustEventStream)
		{
			var subscription = trustEventStream.Subscribe(trustChange => 
															{
																this.trust.Subscribe(currentTrust => {
																	this.trust.OnNext(currentTrust + trustChange);
																}).Dispose();
															});
			return Disposable.Create(() => { subscription.Dispose(); });
		}

		public void AttachAttractionScaleevent (IObservable<int[]> attractionScaleEventStream)
		{
			var subscription = attractionScaleEventStream.Subscribe(attractionScaleChange =>
																		{
																			this.attractionScale.Subscribe(currentAttractionScale => {
																				var sexuality = currentAttractionScale[0] + attractionScale[0];
																				var orientation = currentAttractionScale[1] + attractionScaleChange[1];
																				int[] newScale = { sexuality, orientation };
																				this.attractionScale.OnNext(newScale);
																			}).Dispose();
																		});
		}
	}
}