using System.Collections.ObjectModel;
using VisorAnimalesAPP.Vista;

namespace VisorAnimalesAPP
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<Animales> ListaAnimales { get; set; }

        public MainPage()
        {
            InitializeComponent();

            ListaAnimales = new ObservableCollection<Animales>() {
                new Animales{imagen="leon.jpg" ,descripcion="Grande, de pelo largo y naranja. Ubicado en la selva.",fecha=DateTime.Now,nombre="León"},
                new Animales{imagen="tigre.jpg" ,descripcion="Grande, con rayas. Ubicado en la selva.",fecha=DateTime.Now,nombre="Tigre"},
                new Animales{imagen="cebra.jpg" ,descripcion="Mediana, con rayas blancas y negras. Ubicado en la selva.",fecha=DateTime.Now,nombre="Cebra"},
                new Animales{imagen="panda.jpg" ,descripcion="Grande, de pelo largo y naranja. Ubicado en la selva.",fecha=DateTime.Now,nombre="Panda"},
                new Animales{imagen="lince.jpg" ,descripcion="Grande, de pelo largo y naranja. Ubicado en la selva.",fecha=DateTime.Now,nombre="Lince"},
                new Animales{imagen="aguila.jpg" ,descripcion="Grande, de pelo largo y naranja. Ubicado en la selva.",fecha=DateTime.Now,nombre="Águila"}
            };

            BindingContext =this;

        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is Animales animalSeleccionado) {
                await Navigation.PushAsync(new InformacionAnimal(animalSeleccionado));
                ((ListView)sender).SelectedItem = null;
            }
        }

        private async void OnClickAddImage(object sender, EventArgs e)
        {
            var agregarAnimal = new AddAnimal();
            agregarAnimal.actualiza += (nuevoanimal) =>
            {
                ListaAnimales.Add(nuevoanimal);
                BindingContext = null;
                BindingContext = this;
            };

            await Navigation.PushAsync(agregarAnimal);
        }
    }

}
