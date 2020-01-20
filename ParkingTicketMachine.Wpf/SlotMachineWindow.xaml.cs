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
			if (ListBoxCoins.IsEnabled && ListBoxCoins.SelectedItem != null)
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
		public EventHandler<TicketVerkauftEventArgs> TicketVerkauftInfo;
		private void ButtonPrintTicket_Click(object sender, RoutedEventArgs e)
		{
			Ticket ticket = new Ticket(_startZeit, _endZeit, _slotMachine.SumActualInput, this.Title);
			if (ticket.Check)
			{
				SlotMachine.PrintTicket(ticket);
				MeldeAnZentrale(ticket);
			}
		}
		protected virtual void MeldeAnZentrale(Ticket gekaufteTicket)
		{
			TicketVerkauftInfo?.Invoke(this, new TicketVerkauftEventArgs(gekaufteTicket));
			MessageBox.Show(gekaufteTicket.ToString());
		}

		private void ButtonCancel_Click(object sender, RoutedEventArgs e)
		{
			int retour = SlotMachine.CancelOrder();
			double ret = (double)retour / 100;
			MessageBox.Show($"Rückgabe: {ret.ToString()} Euro.");
		}
	}
	public class TicketVerkauftEventArgs : EventArgs
	{
		private Ticket _gekaufteTicket;

		public TicketVerkauftEventArgs(Ticket gekaufteTicket)
		{
			GekaufteTicket = gekaufteTicket;
		}

		public Ticket GekaufteTicket { get => _gekaufteTicket; set => _gekaufteTicket = value; }
	}
}
