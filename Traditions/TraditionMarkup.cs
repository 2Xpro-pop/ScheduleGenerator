using Avalonia.Controls;
using StackMarkup;
using System.Globalization;

namespace ScheduleGenerator.Traditions
{

    public abstract class BaseMarkup
    {
        public static MarkupDocument Document { get; } = GetDocument();
        private static MarkupDocument GetDocument()
        {
            var doc = new MarkupDocument();

            doc.Register<TextMarkup>();
            doc.Register<ButtonMarkup>();
            doc.Register<NumericUpDownMarkup>();

            return doc;
        }

        public virtual string Name { get; set; }
        [MarkupContent]
        public virtual string Content { get; set; }
        public virtual string Style 
        { 
            get
            {
                return style == null ? "nothing" : style;
            }
            set => style = value;
        }
        private string style;

        public abstract Control Render();
    }

    [MarkupAliases("text")]
    public class TextMarkup : BaseMarkup
    {
        [MarkupAliases("text","txt","t")]
        public override string Content { get; set; }

        public override Control Render()
        {
            return new TextBlock()
            {
                Text = Content,
                Classes = Classes.Parse(Style)
            };
        }
    }

    [MarkupAliases("btn","button")]
    public class ButtonMarkup : BaseMarkup
    {
        public override Control Render()
        {
            return new Button()
            {
                Content = Content,
                Classes = Classes.Parse(Style),
            };
        }
    }

    [MarkupAliases("numbox")]
    public class NumericUpDownMarkup: BaseMarkup
    {
        public double Value { get; set; }
        [MarkupAliases("inc")]
        public double Increment { get; set; }
        public double Min { get; set; }
        public double Max { get; set; }
        [MarkupAliases("format")]
        public NumberStyles NumberStyle { get; set; }

        public override Control Render()
        {
            return new NumericUpDown()
            {
                ParsingNumberStyle = NumberStyle,
                Value = Value,
                Increment = Increment,
                Minimum = Min,
                Maximum = Max
            };
        }
    }

    [MarkupAliases("textbox")]
    public class TextBoxMarkup: BaseMarkup
    {
        [MarkupAliases("text", "txt", "t")]
        public override string Content { get; set; }
        [MarkupAliases("wtk")]
        public string Watermark { get; set; }

        public override Control Render()
        {
            return new TextBox()
            {
                Text = Content,
                Watermark = Watermark,
                Classes = Classes.Parse(Style),
            };
        }
    }

}