using VisorAnimalesAPP.Vista;

namespace VisorAnimalesAPP;

public partial class InformacionAnimal : ContentPage
{
	public InformacionAnimal(Animales animal)
	{
		InitializeComponent();
		BindingContext = animal;
	}
}