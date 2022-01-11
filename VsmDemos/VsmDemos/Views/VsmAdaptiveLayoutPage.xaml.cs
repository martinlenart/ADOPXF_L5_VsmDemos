using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace VsmDemos.Views
{
    public partial class VsmAdaptiveLayoutPage : ContentPage
    {
        public ICommand SelectedCommand { private set; get; }
	    public VsmAdaptiveLayoutPage ()
	    {
		    InitializeComponent ();

            SizeChanged += (sender, args) =>
            {
                string visualState = Width > Height ? "Landscape" : "Portrait";
                VisualStateManager.GoToState(mainStack, visualState);
                VisualStateManager.GoToState(menuScroll, visualState);
                VisualStateManager.GoToState(menuStack, visualState);

                foreach (View child in menuStack.Children)
                {
                    VisualStateManager.GoToState(child, visualState);
                }
            };

            SelectedCommand = new Command<string>((filename) =>
            {
                image.Source = Device.RuntimePlatform == Device.UWP
                ? ImageSource.FromFile("Assets/"+filename)
                : ImageSource.FromFile(filename);
            });

            menuStack.BindingContext = this;
	    }
    }
}