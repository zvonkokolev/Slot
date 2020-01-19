using System;
using System.Linq;

namespace ParkingTicketMachine.Core
{
	public class SlotMachine
	{
		//fields
		private int _parkTime;
		private const int _centProMinute = 6;
		private readonly string _strasseName;
		private int[] _validCoins = { 5, 10, 20, 50, 100, 200 };
		private int[] _startCash;
		private int[] _coinsInDepotArray;
		private int[] _actualCoinsInput = new int[6]; 
		private int _sumActualInput;

		//constructors
		public SlotMachine(String strassename)
		{
			_strasseName = strassename;
			_startCash = _coinsInDepotArray = new int[] { 3, 3, 3, 3, 3, 3 };
		}
		public SlotMachine(String strassename, int[] startCash)
		{
			_strasseName = strassename;
			_startCash = _coinsInDepotArray = startCash;
		}
		//properties
		public int CoinsInDepot { get; set; }
		public int ParkTime { get => _parkTime; private set => _parkTime = value; }
		public int SumActualInput { get => _sumActualInput; private set => _sumActualInput = value; }

		//methods
		public bool InsertCoin(int coinIn, out string textMessage)
		{
			bool check = false;
			textMessage = "OK";
			if(coinIn == 1 || coinIn == 2)
			{	// convert 1 & 2 Euro in cent
				coinIn *= 100;
			}
			SumActualInput += coinIn;
			if (!_validCoins.Contains(coinIn))
			{
				textMessage = "Muenze ist NICHT gueltig!!!\nBitte nochmal versuchen...";
			}
			else if (SumActualInput < 50)
			{
				textMessage = "Verlangte Summe noch nicht erreicht";
				return check;
			}
			else
			{
				ParkTime = SumActualInput / 10 * _centProMinute;
				for (int i = _actualCoinsInput.Length - 1; i >= 0; i--)
				{
					if (coinIn % _validCoins[i] == 0)
					{
						_actualCoinsInput[i] = _actualCoinsInput[i] + 1;
						break;
					}
				}
				check = true;
			}
			if (SumActualInput >= 150)
			{
				textMessage = "Verlangte Summe erreicht";
			}
			return check;
		}

		public int CancelOrder()	// []
		{
			_actualCoinsInput.Initialize();
			int rueckgabe = SumActualInput;	//del
			SumActualInput = 0;
			return rueckgabe;
		}
	}
}
