using AppRestAPIPadreFamiliaUI.Models;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using WebUIMaestra.Helpers;
using WebUIMaestra.Models;

namespace AppRestAPIPadreFamiliaUI.ViewModels
{
    public class AlumnoViewModel : INotifyPropertyChanged
    {
        HttpClient cliente=new HttpClient();
        public ObservableCollection<EvaluacionVm> evaluaciones { get; set; } = new ObservableCollection<EvaluacionVm>();
        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Nombre"));}
        }
        private string clave;

        public string Clave
        {
            get { return clave; }
            set { clave = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Clave")); }
        }

        public ICommand EnviarUsuarioCommand { get; set; }
        public ICommand SalirSesionCommand { get; set; }

        public AlumnoViewModel()
        {
            EnviarUsuarioCommand = new RelayCommand(Enviar);
            SalirSesionCommand = new RelayCommand(Salir);
        }

        private void Salir()
        {
            throw new NotImplementedException();
        }

        private async  void Enviar()
        {
            Usuario usuario = new Usuario();
            usuario.Nombre = Nombre;
            usuario.Clave = Cifrado.GetHash(clave);
            var json = JsonConvert.SerializeObject(usuario);
            HttpContent contenido = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await cliente.PostAsync("https://192.168.1.69:45455/api/Alumno", contenido);
            if (result.IsSuccessStatusCode)
            {
                var jsonp = await result.Content.ReadAsStringAsync();
                var listaevaluaciones = JsonConvert.DeserializeObject<IEnumerable<Evaluacion>>(jsonp);                              
                foreach (var evaluacion in listaevaluaciones)
                {
                    evaluaciones.Add(new EvaluacionVm()
                    {
                        Materia = evaluacion.IdMateria == 1 ? "Matemáticas" : evaluacion.IdMateria == 2 ? "Español" : evaluacion.IdMateria == 3 ? "Ciencias Naturales":null,
                        Alumno=Nombre,
                        PrimeraEvaluacion=evaluacion.PrimeraEvaluacion,
                        SegundaEvaluacion=evaluacion.SegundaEvaluacion,
                        TerceraEvaluacion=evaluacion.TerceraEvaluacion
                    }); 
                }                 
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
