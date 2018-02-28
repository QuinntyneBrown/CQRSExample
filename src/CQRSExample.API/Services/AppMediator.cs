using CQRSExample.Core;
using CQRSExample.Infrastructure.Data;
using CQRSExample.Infrastructure.Requests;
using CQRSExample.Infrastructure.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSExample.Web.Services
{
    public class AppMediator: IMediator
    {
        private IMediator _inner;
        private IHttpContextAccessor _httpContextAccessor;
        private IAppDataContext _appDataContext;

        public AppMediator(MultiInstanceFactory multiInstanceFactory, SingleInstanceFactory singleInstanceFactory, IHttpContextAccessor httpContextAccessor, IAppDataContext appDataContext)
        {
            _inner = new Mediator(singleInstanceFactory, multiInstanceFactory);
            _httpContextAccessor = httpContextAccessor;
            _appDataContext = appDataContext;
        }
        public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default(CancellationToken)) where TNotification : INotification
        {
            return _inner.Publish(notification, cancellationToken);
        }

        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default(CancellationToken))
        {
            TryToSetUserIdFromHttpContext(request, _httpContextAccessor.HttpContext);

            return _inner.Send(request, cancellationToken);
        }

        public Task Send(IRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            TryToSetUserIdFromHttpContext(request, _httpContextAccessor.HttpContext);

            return _inner.Send(request, cancellationToken);
        }

        private void TryToSetUserIdFromHttpContext(dynamic request, HttpContext httpContext)
        {
            httpContext.Request.Query.TryGetValue("tenantId", out StringValues tenant);

            if (request.GetType().IsSubclassOf(typeof(BaseRequest)) && string.IsNullOrEmpty(tenant))
                (request as BaseRequest).TenantId = new Guid(httpContext.Request.GetHeaderValue("Tenant"));

            if (request.GetType().IsSubclassOf(typeof(BaseRequest)) && !string.IsNullOrEmpty(tenant))
                (request as BaseRequest).TenantId = new Guid(tenant);

            if (request.GetType().IsSubclassOf(typeof(BaseRequest)))
                _appDataContext.TenantId = (request as BaseRequest).TenantId;

            if (request.GetType().IsSubclassOf(typeof(BaseAuthenticatedRequest)))
                (request as BaseAuthenticatedRequest).Username = httpContext.User.Identity.Name;
        }
    }
}
