using System.Globalization;

namespace MultiLanguageExamManagementSystem.Helpers
{
    public class CultureMiddleware
    {
        private readonly RequestDelegate _next;

        public CultureMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Your code here

            var cultureHeader = context.Request.Headers["Accept-Language"].FirstOrDefault();

            if (!string.IsNullOrEmpty(cultureHeader))
            {
                var cultures = cultureHeader.Split(',').Select(c => CultureInfo.GetCultureInfo(c.Split(';')[0]));
                var matchingCulture = GetSupportedCulture(cultures);

                if (matchingCulture != null)
                {
                    CultureInfo.CurrentCulture = matchingCulture;
                    CultureInfo.CurrentUICulture = matchingCulture;
            	}
        	}

        	await _next(context);
        }

		private CultureInfo GetSupportedCulture(IEnumerable<CultureInfo> cultures)
    	{
        	return cultures.FirstOrDefault();
    	}
    }

}
