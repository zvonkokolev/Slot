using ParkingTicketMachine.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
namespace ParkingTicketMachine.Wpf
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		public event EventHandler<SlotMachineWindow> AutomatDisplayErstellt;
		protected virtual void ErzeugeNeuenAutomat(String strassenName)
		{
			SlotMachineWindow slotMachineWindow = new SlotMachineWindow(strassenName);

			AutomatDisplayErstellt?.Invoke(this, slotMachineWindow);
		}

		public MainWindow()
		{
			InitializeComponent();
			Title = $"Parkticketverwaltung: {FastClock.Instance.Time.ToShortTimeString()}";
		}

		private void Window_Initialized(object sender, EventArgs e)
		{
			ErzeugeNeuenAutomat("Limmesstrasse");
			ErzeugeNeuenAutomat("Landstrasse");
		}

		private void ButtonNew_Click(object sender, RoutedEventArgs e)
		{
			ErzeugeNeuenAutomat(TextBoxAddress.Text);
		}
		public void BeimGekaufterTicket(object sender, Ticket ticket)
		{
			TextBlockLog.Text = ticket.ToString();
		}
	}
}
