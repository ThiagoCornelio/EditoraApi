namespace ProjetoEditoraApi.ViewModels
{
    public class ResultViewModel<T>
    {
        public T Data { get; private set; }
        public List<string> Errors { get; private set; } = new(); //Pode se passar uma lista de erros.
        public ResultViewModel(T data, List<string> erros)
        {
            Data = data;
            Errors = erros;
        }

        public ResultViewModel(T data) => Data = data;

        public ResultViewModel(List<string> errors) => Errors = errors;

        public ResultViewModel(string error) => Errors.Add(error);
    }
}
