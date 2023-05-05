using NavbarAnimation.Maui.ViewModels.Pages;

namespace NavbarAnimation.Maui.Views.Pages;

public partial class RibbonPage : ContentPage
{

    public RibbonPage(RibbonViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}
