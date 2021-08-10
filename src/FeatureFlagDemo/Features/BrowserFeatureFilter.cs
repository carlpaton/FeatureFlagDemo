using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.FeatureManagement;
using System.Linq;
using System.Threading.Tasks;

namespace FeatureFlagDemo.Features
{
    [FilterAlias("BrowserFeatureFilter")]
    public class BrowserFeatureFilter : IFeatureFilter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BrowserFeatureFilter(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context)
        {
            var userAgent = _httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToString();
            var settings = context.Parameters.Get<BrowserFeatureOptions>();

            return Task.FromResult(settings.AllowedBrowsers.Any(userAgent.Contains));
        }
    }
}
