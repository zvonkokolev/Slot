using System;
using System.Windows;
using ParkingTicketMachine.Core;

namespace ParkingTicketMachine.Wpf
{
	/// <summary>
	/// Interaction logic for SlotMachineWindow.xaml
	/// </summary>
	public partial class SlotMachineWindow 
	{
		//fields
		private readonly SlotMachine _slotMachine;
		private DateTime _startZeit;
		private DateTime _endZeit;
		//properties
		public SlotMachine SlotMachine => _slotMachine;
		public SlotMachineWindow(string name)
		{
			_slotMachine = new SlotMachine(name);
			InitializeComponent();
			Title = name;
			Show();
		}
		private void ButtonInsertCoin_Click(object sender, RoutedEventArgs e)
		{
			if (ListBoxCoins.IsEnabled)
			{
				string text;
				string[] parts = ListBoxCoins.SelectedItem.ToString().Split(' ');
				if (!_slotMachine.InsertCoin(int.Parse(parts[1]), out text))
				{
					MessageBox.Show(text);
				}
				else
				{
					_startZeit = FastClock.Instance.StartTime;
					TextBoxTimeUntil.Text = FastClock.Instance.Time.AddMinutes(SlotMachine.ParkTime).ToShortTimeString();
					_endZeit = FastClock.Instance.Time.AddMinutes(SlotMachine.ParkTime);
				}
			}
		}

		private void ButtonPrintTicket_Click(object sender, RoutedEventArgs e)
		{
			Ticket ticket = new Ticket(_startZeit, _endZeit, _slotMachine.SumActualInput, this.Title);
			if (ticket.Check)
			{
				MeldeAnZentrale(ticket);
			}
		}
		private void MeldeAnZentrale(Ticket gekaufteTicket)
		{
			MessageBox.Show(gekaufteTicket.ToString());
		}

		private void ButtonCancel_Click(object sender, RoutedEventArgs e)
		{
			int retour = SlotMachine.CancelOrder();
			double ret = (double)retour / 100;
			MessageBox.Show($"Rückgabe: {ret.ToString()} Euro.");
		}
	}
}
