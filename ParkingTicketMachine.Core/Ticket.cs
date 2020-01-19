using System;

namespace ParkingTicketMachine.Core
{
	public class Ticket
	{
		private readonly DateTime _startZeit;
		private readonly DateTime _endZeit;
		private readonly int _bezahltePreis;
		private readonly string _strasse;
		private bool _check;
		public Ticket(DateTime startZeit, DateTime endZeit, int preis, string strasse)
		{
			_startZeit = startZeit;
			_endZeit = endZeit;
			_bezahltePreis = preis;
			_strasse = strasse;
			Check = true;
		}

		public bool Check { get => _check; set => _check = value; }
		public override string ToString()
		{
			return $"{_strasse} {_startZeit.ToShortTimeString()} {_bezahltePreis}";
		}
	}
}
