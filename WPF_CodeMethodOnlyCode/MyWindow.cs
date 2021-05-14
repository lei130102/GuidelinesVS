using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace WPF_CodeMethodOnlyCode
{
    public class MyWindow : Window
    {
        private Button button1;

        public MyWindow()                        //注意一般是无参构造函数
        {
            InitializeComponent();               //注意需要自己定义InitializeComponent
        }

        private void InitializeComponent()
        {
            //Configure the form
            this.Width = this.Height = 285;
            this.Left = this.Top = 100;
            this.Title = "Code-Only Window";

            //Create a container to hold a button
            DockPanel panel = new DockPanel();

            //Create the button
            button1 = new Button();
            button1.Content = "Please click me.";
            button1.Margin = new Thickness(30);

            //Attach the event handler
            button1.Click += button1_Click;

            //Place the button in the panel.
            IAddChild container = panel;
            container.AddChild(button1);

            //Place the panel in the form.
            container = this;
            container.AddChild(panel);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            button1.Content = "Thank you.";
        }
    }
}
