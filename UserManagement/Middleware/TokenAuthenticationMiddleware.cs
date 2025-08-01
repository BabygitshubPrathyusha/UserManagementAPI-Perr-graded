namespace UserManagementAPI.Middleware
{
    public class TokenAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Replace("Bearer ", "");

            if (string.IsNullOrEmpty(token) || !IsValidToken(token))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsJsonAsync(new { error = "Unauthorized" });
                return;
            }

            await _next(context);
        }
        // Dummy token validation for demonstration
        private bool IsValidToken(string token)
        {
            // Replace with real validation logic
            return token == "valid-token";
        }
    }
}
