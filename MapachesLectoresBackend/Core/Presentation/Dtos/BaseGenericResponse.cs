namespace MapachesLectoresBackend.Core.Presentation.Dtos
{
    public class BaseGenericResponse<TResult, TError>
    {
        public int StatusCode { get; set; }

        public bool IsValid { get; set; }

        public TError? Error { get; set; }

        public TResult? Result { get; set; }

    }
}
