using SeaBattle.Settings;

namespace SeaBattle.MapCreators.Extensions;

public static class SortShipConfigExtensions
{
	public static Dictionary<Configuration.ShipType, ShipConfiguration> Sort(
		this Dictionary<Configuration.ShipType, ShipConfiguration> dict, SortTypes type)
	{
		var sortedDict = new Dictionary<Configuration.ShipType, ShipConfiguration>();
		switch (type)
		{
			case SortTypes.FromLongestToShortest:
				foreach (var ship in dict.OrderByDescending(x => x.Value.length))
				{
					sortedDict.Add(ship.Key, ship.Value);
				}
				break;
			case SortTypes.FromShortestToLongest:
				foreach (var ship in dict.OrderBy(x => x.Value.length))
				{
					sortedDict.Add(ship.Key, ship.Value);
				}
				break;
			default:
				throw new ArgumentOutOfRangeException(nameof(type), type, "No such sort type");
		}

		return sortedDict;
	}
}