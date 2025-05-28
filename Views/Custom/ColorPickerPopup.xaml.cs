using BTGPactualBrowniano.app.ViewModels;
using CommunityToolkit.Maui.Views;

namespace BTGPactualBrowniano.app.Views.Custom;

public partial class ColorPickerPopup : Popup
{
	ColorPickerViewModel _viewModel;
    public ColorPickerPopup(ColorPickerViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = _viewModel;
	}
}