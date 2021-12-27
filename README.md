# ScheduleGenerator
It's my course project.

## Традиции
Вы должны понимать что используеться не обычный python, а IronPython, поэтому нельзя устоновить модули через pip, плюс это версия python 3.4

Для того что бы создать традицию нужно зайти в папку Traditions, создать в ней папку и создать в ней два файла main.py и markup.stack. 

### main.py
В этом файле идет логика традиции, для начала импортируйте references, это нужно для коректных работ всех сылок

```python
from references import *
```

Программа читает специальные переменные
```python
from references import *

__name__ = "Свобода Графика!"
__description__ = "Позволяет учитлеям выберать пару, которая им не удобны."
```

Прорамма сама вызывает специальные функции, одна из таких это:
```python
def count_point(schedule):
    return 1
```
schedule это массив с размером 48*количество_групп, где каждый элемент массива это id учителя, если такого id нет, значит урока в это время нет, каждые 8 элементов это день
(то есть первые 8 элементов это понедельник, а с 40 до 47 это суббота, затем все сначала), 
а каждые 48 это отдельная группа. Функция должна возвращать оценку от 0 до 1.

Пример как получить имя учителя:
```python
from references import *
import App

__name__ = App.Teachers[0].Name
```

Аналогично для группы:
```python
from references import *
import App

__name__ = App.Groups[0].Name
```

Свойства Teacher и Group
```csharp
class Teacher
{
    public string Name { get; set; }
    public string Lesson { get; set; }
    /// <summary>
    /// BadTime считаеться плохим временем от 0 до 47
    /// </summary>
    public ReadOnlyCollection<int> BadTimes { get; set; }
    public List<int> Conflicts { get; set; } = new List<int>();
}

public class Group
{
    public string Name { get; set; }
    public Dictionary<string,int> NeedLessons { get; set; }
    /// <summary>
    /// BadClock считаеться плохим временем от 0 до 7
    /// </summary>
    public ReadOnlyCollection<int> BadClock { get; set; }
}
```
 
Другое использование App это настройки
Аналогично для группы:
```python
from references import *
import App

enable = False
if App.Settings.ContainsKey("enable_my_tradition"):
    enable = App.Settings["enable_my_tradition"]
else:
    App.Settings["enable_my_tradition"] = False
__name__ = App.Groups[0].Name

def count_point(schedule):
    if not(enable):
        return 1 # Если функция count_point есть она все ровно будет вызываться, поэтому ставим 1
    #do your code
```

## markup.stack

У меня есть отдельный репозиторий StackMarkup. Да я создал отдельный язык разметки специально для курсовой

Простой синтакис

```
/имя_тега
/имя_тега контент
/имя_тега[свойства1=значение1;свойства2=значение2;....]
/имя_тега[свойства1=значение1;свойства2=значение2;....] контент
```

Все теги в рендериться в Avalonia Control их свойства можете прочитать [здесь](https://docs.avaloniaui.net/docs/controls)

### Бозовый тег
У всех тегов есть свойтса Content, Style, Name

Свойства style работыет на данный момент только с тегам text и это h1 и h2

Свойства Name нужно для привязки к python коду
например событие по нажатию кнокпи:

markup.stack:
```
/button[Name=my_button] Нажми сюда! 
```

main.py:
```python
from references import *

from Avalonia import *
from Avalonia.Controls import *

from System import *
from System.IO import *
from System.Diagnostics import *
from ScheduleGenerator.ViewModels import *

import webbrowser

def observe_my_button(element, viewModel):
    element.Click += lambda x,y : click_open_doc(viewModel) 

def click_open_doc(viewModel):
    webbrowser.open("https://github.com/2Xpro-pop/ScheduleGenerator/tree/master#%D1%82%D1%80%D0%B0%D0%B4%D0%B8%D1%86%D0%B8%D0%B8")
```

Тег name ищет в main.py(или в других импортируемых модулей) функцию observe_{name}(a,b) где "a" это элемент рендера, а "b" ..... в принципе по хорошему вы не должны знать)  

### Тег text
Добавляет простой текст, имеет только базовые свойства, единственные у кого работают стили.  
Тип Рендера: [TextBlock](https://docs.avaloniaui.net/docs/controls/textblock)

### Тег button
Добавляет кнопку, имеет только базовые свойства 
Тип Рендера: [Button](https://docs.avaloniaui.net/docs/controls/button)

### Тег numbox
Добавляет input поле котороне принимает только цифры  
Свойства Value-значение самого input, Min, Max, [NumberStyle](https://docs.microsoft.com/en-us/dotnet/api/system.globalization.numberstyles?view=net-5.0),
Тип Рендера: [NumericUpDown](https://docs.avaloniaui.net/docs/controls/numericupdown)

### Тег textbox
Добавляет input поле котороне принимает текст
Свойства Text-значение самого input, равносильно Content, Wattermark - в других языках это называют placeholder
