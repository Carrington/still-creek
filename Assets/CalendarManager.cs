using System;
using System.Diagnostics;
using System.IO;
using UniRx;
using Newtonsoft.Json.Linq;


namespace Managers
{
	public struct EngineDate
	{
		public readonly int Month;
		public readonly int Day;
		public readonly int Year;

		public EngineDate (int year, int month, int day)
		{
			this.Year = year;
			this.Month = month;
			this.Day = day;
		}
	}

	public class CalendarManager
	{
		private readonly string CalendarConfigDocPath;
		private IConnectableObservable<Clock> clock;
		private JObject calendarConfig;

		public CalendarManager (IConnectableObservable<Clock> clock)
		{
			this.clock = clock;
			this.CalendarConfigDocPath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + @"\config\calendar.json";
			using (StreamReader file = File.OpenText(@"c:\videogames.json"));
			using (JsonTextReader reader = new JsonTextReader(file))
			{
				this.calendarConfig = (JObject)JToken.ReadFrom(reader);
			}
		}

		public IConnectableObservable<EngineDate> CalendarStream ()
		{
			return Observable.Create<EngineDate> (observer => {
				var day = 1;
				var month = 1;
				var year = 1;

				var clockSub = this.clock
									.Where(clock => { clock.Minute == 0 && clock.Hour == 0; })
									.Subscribe(
										_ => { 
											day++;
											if (day > 90) {
												day = 1;
												month++;
											}
											if (month > 4) {
												month = 1;
												year++;
											}

											EngineDate date = new EngineDate(year, month, day);

											observer.OnNext(EngineDate);
										},
										observer.OnError,
										observer.OnCompleted
									);



				return Disposable.Create(() => {
					clockSub.Dispose();
				});
			});
		}
	}
}

