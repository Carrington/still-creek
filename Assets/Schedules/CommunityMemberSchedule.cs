using System;
using Managers;
using Entities;

namespace Schedules
{
	public struct Activity 
	{
		public Activity ()
		{
			
		}
	}

	/** 
	 * Activities should be defined as follows:
	 * {
	 *   activityName: "name",
	 *   activtyClass: "ClassName" (or null),
	 *   constraints: 
	 *     [
	 *	     {
	 *         constraintMethod: "methodName",
	 *         constraintParameters: 
	 *		     {
	 *			   parameter: [strings of containing properties, e.g. "date", "month" would be date.month] (see ActivityConstraint)
	 *			 }
	 *       }
	 *     ],
	 *	 location: (figure out how to denote scene and coords)
	 * }
	**/
	public class CommunityMemberSchedule
	{
		private readonly string scheduleFileLocation;

		public CommunityMemberSchedule (string fileLocation)
		{
			this.scheduleFileLocation = fileLocation;
		}
	}
}

