using ParkingTicketMachine.Core;
using System;
using System.Windows;
namespace ParkingTicketMachine.Wpf
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		protected virtual void ErzeugeNeuenAutomat(String strassenName)
		{
			SlotMachineWindow smw = new SlotMachineWindow(strassenName);
			smw.TicketVerkauftInfo += WennTicketVerkauft;
		}

		public MainWindow()
		{
			InitializeComponent();
			Title = $"Parkticketverwaltung: {FastClock.Instance.Time.ToShortTimeString()}";
		}

		private void Window_Initialized(object sender, EventArgs e)
		{
			SlotMachineWindow smw1 = new SlotMachineWindow("Limmesstrasse");
			smw1.TicketVerkauftInfo += WennTicketVerkauft;
			SlotMachineWindow smw2 = new SlotMachineWindow("Landstrasse");
			smw2.TicketVerkauftInfo += WennTicketVerkauft;
		}

		private void ButtonNew_Click(object sender, RoutedEventArgs e)
		{
			ErzeugeNeuenAutomat(TextBoxAddress.Text);
		}
		public void WennTicketVerkauft(object sender, TicketVerkauftEventArgs e)
		{
			TextBlockLog.Text = e.GekaufteTicket.ToString();
		}
	}
}
