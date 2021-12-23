import clr
import sys

clr.AddReference("System")
clr.AddReference("Avalonia.Controls, Version=0.10.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b")
clr.AddReference("Avalonia.Visuals, Version=0.10.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b")
clr.AddReference("ScheduleGenerator")

from Avalonia import *
from Avalonia.Controls import *

from System import *
from System.IO import *
from System.Diagnostics import *
from ScheduleGenerator.ViewModels import *

__name__ = "Свобода Графика!"
__description__ = "Позволяет учитлеям выберать пару, которая им не удобна."

def observe_foo(element, viewModel):
    element.Margin = Thickness(0, 30, 0, 30)

def observe_path(element, viewModel):
    element.Text += Path.GetFullPath(__file__)

def observe_open_doc(element, viewModel):
    element.Click += lambda x,y : click_open_doc(viewModel) 

def click_open_doc(viewModel):
    viewModel.HostScreen.Router.Navigate.Execute(TraditionsDocVm(viewModel.HostScreen))