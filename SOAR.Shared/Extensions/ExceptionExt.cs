using System;
using System.Collections.Generic;
using System.Linq;

namespace SOAR.Shared.Extensions
{
	public static class ExceptionExt
	{
		#region PrintDetails
		public static string PrintDetails(this Exception e)
		{
			return e.PrintDetails("EXCEPTION").Aggregate(string.Empty, (acc, line) => acc + Environment.NewLine + line);
		}

		private static IEnumerable<string> PrintDetails(this Exception e, string prefix)
		{
			if (e != null)
			{
				yield return prefix + ": " + e.Message;

				if (e.StackTrace != null)
				{
					foreach (string line in e.StackTrace.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
					{
						yield return "\t" + line;
					}
				}

				foreach (string line in e.InnerException.PrintDetails("INNER EXCEPTION"))
				{
					yield return line;
				}
			}
		}
		#endregion
	}
}
