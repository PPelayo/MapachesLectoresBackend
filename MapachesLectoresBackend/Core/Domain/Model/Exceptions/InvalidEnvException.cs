namespace MapachesLectoresBackend.Core.Domain.Model.Exceptions;

public class InvalidEnvException(string msg = "Error al intentar recuperar una variable de entorno", Exception? innerEx = null) : Exception(msg, innerEx);