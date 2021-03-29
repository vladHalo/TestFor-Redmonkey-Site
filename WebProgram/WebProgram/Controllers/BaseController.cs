using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace WebProgram.Controllers
{
    public class BaseController : Controller
    {
        //1  
        //Начинает выполнение указанного контекста запроса
        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            return base.BeginExecute(requestContext, callback, state);
        }

        //2
        //Определяет, следует ли отключить поддержку асинхронных операций для контроллера(false)
        //значение true, чтобы отключить асинхронную поддержку контроллера; в противном случае — значение false.
        protected override bool DisableAsyncSupport => base.DisableAsyncSupport;

        //3
        //Инициализирует данные, которые могут быть недоступны на момент вызова конструктора.
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        //4
        //Начинает вызов действия в текущем контексте контроллера.
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            return base.BeginExecuteCore(callback, state);
        }

        //5
        //Создает поставщика временных данных.
        protected override ITempDataProvider CreateTempDataProvider()
        {
            return base.CreateTempDataProvider();
        }

        //6
        //Создает средство вызова действий.
        protected override IActionInvoker CreateActionInvoker()
        {
            return base.CreateActionInvoker();
        }

        //7
        //Вызывается при выполнении авторизации.
        protected override void OnAuthentication(AuthenticationContext filterContext)
        {
            base.OnAuthentication(filterContext);
        }

        //8
        //Вызывается при выполнении авторизации.
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
        }

        //9
        //Вызывается перед вызовом метода действия.
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }

        //10
        //Завершает вызов действия в текущем контексте контроллера.
        protected override void EndExecute(IAsyncResult asyncResult)
        {
            base.EndExecute(asyncResult);
        }

        //11
        //Завершает ядро выполнения.
        protected override void EndExecuteCore(IAsyncResult asyncResult)
        {
            base.EndExecuteCore(asyncResult);
        }

        //12
        //Вызывается после вызова метода действия.
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            //if (UsersDataProvider.UsersDataProvider.CurrentUser() == null)
            //    filterContext.Result = new RedirectResult("/Users/Login");
        }

        //13
        //Вызывается при запросе авторизации.
        protected override void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            base.OnAuthenticationChallenge(filterContext);
        }

        //14
        //Вызывается перед выполнением результата действия, возвращенного методом действия.
        protected override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
        }

        //15
        //Вызывается после выполнения результата действия, возвращенного методом действия.
        protected override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
        }

        //16
        //Освобождает все ресурсы, используемые текущим экземпляром класса Controller.
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        //Выполняет указанный контекст запроса.
        protected override void Execute(RequestContext requestContext)
        {
            base.Execute(requestContext);
        }

        //Вызывает действие в текущем контексте контроллера.
        protected override void ExecuteCore()
        {
            base.ExecuteCore();
        }

        //Вызывается, когда запрос соответствует этому контроллеру, но в контроллере не найден метод с указанным именем действия.
        protected override void HandleUnknownAction(string actionName)
        {
            base.HandleUnknownAction(actionName);
        }

        //Вызывается, когда в действии происходит необработанное исключение.
        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
        }
    }
}