using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BubblesPage
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        ObservableCollection<Demo> demo = new ObservableCollection<Demo>();
        public MainPage()
        {
            InitializeComponent();
            IBubbles bubbles = DependencyService.Get<IBubbles>();
            demo.Add(new Demo { Image = "cocktail", Text = "Cocktel" });
            demo.Add(new Demo { Image = "hot_dog", Text = "HotDog" });
            demo.Add(new Demo { Image = "manzana", Text = "Manzana" });
            demo.Add(new Demo { Image = "pineapple", Text = "Piña" });
            demo.Add(new Demo { Image = "watermelon", Text = "Melon" });
            list.ItemsSource = demo;

            Device.StartTimer(TimeSpan.FromSeconds(15), () =>
            {
                Random random = new Random();
                var data = random.Next(1, demo.Count);
                var select = demo[data - 1];
                bubbles.Bubbles(select.Image);
                return false;
            });
        }
    }
    public class Demo
    {
        public string Image { get; set; }
        public string Text { get; set; }
    }
}
