using System.Text.Json;

namespace API_de_pruebas.Middlewares
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue("X-Api-Key", out var apiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("API Key requerida");
                return;
            }

            var clientes = _configuration
                .GetSection("ApiSecurity:ApiKeys")
                .Get<List<ApiKeyCliente>>();

            var cliente = clientes?.FirstOrDefault(c => c.Key == apiKey);

            if (cliente == null)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("API Key incorrecta");
                return;
            }

            if (!cliente.Activo)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Cliente bloqueado");
                return;
            }

            context.Items["Cliente"] = cliente.Cliente;

            await _next(context);
        }
    }

    public class ApiKeyCliente
    {
        public string Cliente { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty;
        public bool Activo { get; set; }
    }
}