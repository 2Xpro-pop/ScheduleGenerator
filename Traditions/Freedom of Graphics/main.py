﻿from references import *

import webbrowser

from Avalonia import *
from Avalonia.Controls import *

from System import *
from System.IO import *
from System.Diagnostics import *
from ScheduleGenerator.ViewModels import *

__name__ = "Свобода Графика!"
__description__ = "Позволяет учитлеям выберать пару, которая им не удобна."
full_path = Path.GetFullPath(__file__)

def observe_foo(element, viewModel):
    element.Margin = Thickness(0, 30, 0, 30)

def observe_path(element, viewModel):
    element.Text += Path.GetDirectoryName(full_path)

def observe_open_doc(element, viewModel):
    element.Click += lambda x,y : click_open_doc(viewModel) 

def click_open_doc(viewModel):
    loop = asyncio.get_event_loop()
    results = loop.run_until_complete(asyncio.gather(*tasks))
    loop.close()
    webbrowser.open("https://github.com/2Xpro-pop/ScheduleGenerator/tree/master#%D1%82%D1%80%D0%B0%D0%B4%D0%B8%D1%86%D0%B8%D0%B8")