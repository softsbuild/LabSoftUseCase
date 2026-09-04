using AppTask.Models.Interfaces;

namespace AppTask.Models.Services
{
    public class RegraTarefa : IRegraTarefa
    {
        public bool validarDataFinal(DateTime? datainicial, DateTime? datafinal)
        {
            return datafinal>datainicial;
        }
    }
}
