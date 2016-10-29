using System;
namespace AssemblyCSharp
{
	public class Character
	{
		private string guid;
		private IObservable<string> name;
		private IObservable<Sanity> sanity;
		private ReactiveDictionary<string, ReactiveRelationship> relationships;
		private IObservable<Tolerance> tolerance;
		private IObservable<Attraction> attraction;

		public Character ()
		{
		}
	}
}

