using Avalonia;
using Avalonia.Data;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

using ReactiveUI;

using System.Reactive;
using System.Collections;
using System.Windows.Input;

namespace ScheduleGenerator.Controls
{
    public partial class EditableList : UserControl
    {
        public static readonly StyledProperty<IEnumerable> ItemsProperty = 
            AvaloniaProperty.Register<EditableList, IEnumerable>(nameof(Items), defaultBindingMode:BindingMode.TwoWay);

        public static readonly StyledProperty<object> SelectedItemProperty =
            AvaloniaProperty.Register<EditableList, object>(nameof(SelectedItem), defaultBindingMode:BindingMode.TwoWay);

        public static readonly StyledProperty<ICommand> AddItemCommandProperty =
            AvaloniaProperty.Register<EditableList, ICommand>(nameof(AddItemCommand), defaultBindingMode:BindingMode.TwoWay);

        public static readonly StyledProperty<ICommand> EditItemCommandProperty =
            AvaloniaProperty.Register<EditableList, ICommand>(nameof(EditItemCommand), defaultBindingMode:BindingMode.TwoWay);

            public static readonly StyledProperty<ICommand> RemoveItemCommandProperty =
            AvaloniaProperty.Register<EditableList, ICommand>(nameof(RemoveItemCommand), defaultBindingMode:BindingMode.TwoWay);
        
        public static readonly StyledProperty<bool> CanEditOrRemoveProperty = 
            AvaloniaProperty.Register<EditableList, bool>(nameof(CanEditOrRemove), defaultBindingMode:BindingMode.TwoWay);
        
        public IEnumerable Items 
        { 
            get => GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }

        public object SelectedItem 
        {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        public ICommand AddItemCommand
        {
            get => GetValue(AddItemCommandProperty);
            set => SetValue(AddItemCommandProperty, value);
        }

        public ICommand EditItemCommand
        {
            get => GetValue(EditItemCommandProperty);
            set => SetValue(EditItemCommandProperty, value);
        }

        public ICommand RemoveItemCommand
        {
            get => GetValue(RemoveItemCommandProperty);
            set => SetValue(RemoveItemCommandProperty, value);
        }

        public bool CanEditOrRemove
        {
            get => GetValue(CanEditOrRemoveProperty);
            set => SetValue(CanEditOrRemoveProperty, value);
        }

        public EditableList()
        {
            DataContext = this;
            InitializeComponent();
            var stack = new StackPanel();
            var list = new ListBox()
            {
                [!ListBox.ItemsProperty] = new Binding(nameof(Items))
            };
            
        }

        public void SelectedChange(object sender, SelectionChangedEventArgs e)
        {
            if((sender as ListBox).SelectedItem != null)
            {
                CanEditOrRemove = true;
            }
            else
            {
                CanEditOrRemove = false;
            }
        }

        public void OnDoubleTapped(object sender, RoutedEventArgs args)
        {
            EditItemCommand.Execute(SelectedItem);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

    }
}