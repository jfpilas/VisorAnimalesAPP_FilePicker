
using System.IO;
using VisorAnimalesAPP.Vista;

namespace VisorAnimalesAPP;

public partial class AddAnimal : ContentPage
{
    private string imagen = "defecto.jpg";
    public event Action<Animales> actualiza;
    public AddAnimal()
	{
		InitializeComponent();
        
	}

    private async void OnSeleccionarArchivo(object sender, EventArgs e)
    {

        var options = new PickOptions
        {
            PickerTitle = "Selecciona una imagen",
            FileTypes = FilePickerFileType.Images 
        };

        var file = await PickAndShow(options);
        if (file != null)
        {
            string destinoImagen = @"C:\Users\Juanfran\source\repos\VisorAnimalesAPP\VisorAnimalesAPP\Resources\Images\";
            Directory.CreateDirectory(destinoImagen);

            string archivo = Path.Combine(destinoImagen, file.FileName); 

            using (var sourceStream = await file.OpenReadAsync()) 
            using (var destinationStream = File.Create(archivo)) 
            {
                await sourceStream.CopyToAsync(destinationStream); 
            }

            imagen = archivo;

            await DisplayAlert("Archivo Seleccionado", $"Archivo: {file.FileName}", "OK");
        }
        else
        {
            await DisplayAlert("Cancelado", "No se seleccionó ningún archivo.", "OK");
        }
    }
    public async Task<FileResult> PickAndShow(PickOptions options)
    {
        try
        {
            var result = await FilePicker.Default.PickAsync(options);
            if (result != null)
            {
                if (result.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
                    result.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase))
                {
                    using var stream = await result.OpenReadAsync();
                    var imagen = ImageSource.FromStream(() => stream);
                }
            }

            return result;
        }
        catch (Exception ex)
        {
            
        }

        return null;
    }

    private async void OnGuardarAnimal(object sender, EventArgs e)
    {
     
        string nombre = txtNombreAnimal.Text;
        string descripcion = txtDescripcion.Text;
        string fecha = txtFecha.Text;
         


        
        if (string.IsNullOrEmpty(nombre) ||
            string.IsNullOrEmpty(descripcion) ||
            string.IsNullOrEmpty(fecha) ||
            imagen == "default.jpg") 
        {
            await DisplayAlert("Aviso", "No se puede guardar el animal porque uno o más campos están vacíos o no se seleccionó una imagen.", "OK");
            return;
        }

        try
        {
            DateTime fechaParseada = DateTime.Parse(fecha);
            
            
            var nuevoAnimal = new Animales
            {
                nombre = nombre,
                imagen = imagen,
                descripcion = descripcion,
                fecha = fechaParseada
            };

            actualiza?.Invoke(nuevoAnimal);
            await DisplayAlert("Mensaje", "El animal ha sido añadido correctamente.", "OK");
            await Navigation.PopAsync(); 
        }
        catch (FormatException)
        {
            await DisplayAlert("Error", "La fecha ingresada no es válida.", "OK");
        }
    }


}