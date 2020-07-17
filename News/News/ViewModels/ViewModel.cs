using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace News.ViewModels
{
	public abstract class ViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public INavigate Navigation { get; set; } = new Navigator();
	}
}
