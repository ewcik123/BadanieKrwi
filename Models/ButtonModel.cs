using System.Windows.Input;

namespace BadanieKrwi.Models
{
    public class ButtonModel
    {
        public string Content { get; set; }
        public ICommand Command { get; set; }
        public object CommandParameter { get; set; }

        public ButtonModel() { }

        public ButtonModel(string content, ICommand command, object commandParameter) : this()
        {
            Content = content;
            Command = command;
            CommandParameter = commandParameter;
        }
    }
}