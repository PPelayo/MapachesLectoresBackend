using MapachesLectoresBackend.Books.Domain.Model;
using MapachesLectoresBackend.Core.Domain.Model.Errors;
using MapachesLectoresBackend.Core.Domain.Model.ResultPattern;

namespace MapachesLectoresBackend.Books.Presentation.Utils
{
    public static class BookOrderSerializer
    {
        public static DataResult<BooksOrderEnum> Validate(string order)
        {
            return Enum.TryParse<BooksOrderEnum>(order, out var result)
                ? DataResult<BooksOrderEnum>.CreateSuccess(result)
                : DataResult<BooksOrderEnum>.CreateFailure(new MalformedItemError("order"));
        }
    }
}
