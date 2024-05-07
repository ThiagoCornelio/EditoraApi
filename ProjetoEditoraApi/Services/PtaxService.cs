using Newtonsoft.Json;

namespace ProjetoEditoraApi.Services
{
    public class PtaxService
    {
        private readonly HttpClient _httpClient;

        public PtaxService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<decimal> ConsultarCotacaoDolar()
        {
            var dataConsulta = DateTime.Now;

            // Se for antes das 13:10, usa a cotação do dia anterior
            if (dataConsulta.TimeOfDay <= new TimeSpan(13, 10, 0))
            {
                dataConsulta = dataConsulta.AddDays(-1);
            }

            dataConsulta = dataConsulta.AddDays(-1);

            while (dataConsulta.DayOfWeek == DayOfWeek.Saturday || dataConsulta.DayOfWeek == DayOfWeek.Sunday)
            {
                // Se for final de semana, pega a cotação do dia útil anterior
                dataConsulta = dataConsulta.AddDays(-1);
            }

            var dataformatada = dataConsulta.ToString("MM/dd/yyyy");
            string url = $"https://olinda.bcb.gov.br/olinda/servico/PTAX/versao/v1/odata/CotacaoDolarDia(dataCotacao=@dataCotacao)?@dataCotacao='{dataformatada}'&$top=1&$format=json";
            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var dados = JsonConvert.DeserializeObject<dynamic>(jsonResponse);
                return dados.value[0].cotacaoCompra;
            }

            throw new Exception();
        }
    }
}
