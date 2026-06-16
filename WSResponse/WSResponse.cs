namespace API_de_pruebas.WSResponse
{
    /// <summary>
    /// Respuesta estándar de la API.
    /// </summary>
    /// <typeparam name="T">Tipo de dato devuelto.</typeparam>
    public class WSResponse<T>
    {
        /// <summary>
        /// Código de error. 0 indica éxito.
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// Mensaje descriptivo de la operación.
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Datos devueltos por la operación.
        /// </summary>
        public T? Response { get; set; }
    }
}
