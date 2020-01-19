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
			_ = new SlotMachineWindow(strassenName);
		}

		public MainWindow()
		{
			InitializeComponent();
			Title = $"Parkticketverwaltung: {FastClock.Instance.Time.ToShortTimeString()}";
		}

		private void Window_Initialized(object sender, EventArgs e)
		{
			_ = new SlotMachineWindow("Limmesstrasse");
			_ = new SlotMachineWindow("Landstrasse");
		}

		private void ButtonNew_Click(object sender, RoutedEventArgs e)
		{
			ErzeugeNeuenAutomat(TextBoxAddress.Text);
		}
		public void BeimGekaufterTicket(object sender, Ticket e)
		{
			TextBlockLog.Text = e.ToString();
		}
	}
}
