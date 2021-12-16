using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Text;
using Avalonia.ReactiveUI;
using ReactiveUI;
using ScheduleGenerator.Models;
using Avalonia.Controls;

namespace ScheduleGenerator.ViewModels
{
    using Traditions;

    public class TraditionsMoreVm : ViewModelBase, IRoutableViewModel
    {
        public IScreen HostScreen { get; }
        public string UrlPathSegment { get; } = "TraditionsMoreVm";

        public Control Control { get; }

        public string[] Bindings { get; set; }

        public TraditionsMoreVm(IScreen screen, TraditionMarkup markup)
        {
            HostScreen = screen;
            var stack = new StackPanel() {Spacing = 5};
            int valueIndex = 0;
            
            foreach(int mark in markup.markUp)
            {
                if(mark == TraditionMarkup.Text_H1)
                {
                    stack.Children.Add(
                        new TextBlock() { Text = markup.values[valueIndex++], Classes = new Classes("h1") }
                    );
                }

                if(mark == TraditionMarkup.Text_H2)
                {
                    stack.Children.Add(
                        new TextBlock() { Text = markup.values[valueIndex++], Classes = new Classes("h2") }
                    );
                }

                if(mark == TraditionMarkup.Text)
                {
                    stack.Children.Add(
                        new TextBlock() { Text = markup.values[valueIndex++] }
                    );
                }

                if(mark == TraditionMarkup.TextBox)
                {
                    stack.Children.Add(
                        new TextBox() { Watermark = markup.values[valueIndex++] }
                    );
                }
            }
            Control = stack;
        }
    }
}
