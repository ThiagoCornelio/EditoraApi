using System.ComponentModel.DataAnnotations;

namespace ProjetoEditoraApi.Extensions.Attributes
{
    public class DateLessThanTodayAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime data = Convert.ToDateTime(value);

            return data.Date <= DateTime.Now.Date;
        }
    }
}
